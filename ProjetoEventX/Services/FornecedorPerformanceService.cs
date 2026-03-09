using Microsoft.EntityFrameworkCore;
using ProjetoEventX.Data;
using ProjetoEventX.Models;

namespace ProjetoEventX.Services
{
    public class FornecedorPerformanceService
    {
        private readonly EventXContext _context;

        public FornecedorPerformanceService(EventXContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Recalcula todas as métricas de performance de um fornecedor específico.
        /// </summary>
        public async Task<FornecedorRanking> RecalcularAsync(int fornecedorId)
        {
            var fornecedor = await _context.Fornecedores
                .Include(f => f.Avaliacoes)
                .Include(f => f.Produtos)
                .FirstOrDefaultAsync(f => f.Id == fornecedorId);

            if (fornecedor == null)
                throw new InvalidOperationException($"Fornecedor {fornecedorId} não encontrado.");

            var produtoIds = fornecedor.Produtos.Select(p => p.Id).ToList();

            // Média das avaliações
            var avaliacoes = fornecedor.Avaliacoes.ToList();
            decimal mediaAvaliacoes = avaliacoes.Any() ? (decimal)avaliacoes.Average(a => a.Nota) : 0;
            int qtdAvaliacoes = avaliacoes.Count;

            // Pedidos concluídos
            int pedidosConcluidos = await _context.Pedidos
                .CountAsync(p => produtoIds.Contains(p.ProdutoId)
                    && (p.StatusPedido == "Pago" || p.StatusPedido == "Entregue"));

            // Tempo médio de resposta (em horas) baseado nos Quotes
            var quotesRespondidos = await _context.Quotes
                .Where(q => q.SupplierId == fornecedorId && q.ResponseDate.HasValue)
                .Select(q => new { q.CreatedAt, ResponseDate = q.ResponseDate!.Value })
                .ToListAsync();

            decimal tempoMedioResposta = 0;
            if (quotesRespondidos.Any())
            {
                tempoMedioResposta = (decimal)quotesRespondidos
                    .Average(q => (q.ResponseDate - q.CreatedAt).TotalHours);
                if (tempoMedioResposta < 0) tempoMedioResposta = 0;
            }

            // Taxa de aceitação
            var totalQuotes = await _context.Quotes.CountAsync(q => q.SupplierId == fornecedorId);
            var quotesAceitos = await _context.Quotes.CountAsync(q => q.SupplierId == fornecedorId && q.Status == "Aceito");
            decimal taxaAceitacao = totalQuotes > 0 ? ((decimal)quotesAceitos / totalQuotes) * 100 : 0;

            // Taxa de cancelamento
            var quotesCancelados = await _context.Quotes.CountAsync(q => q.SupplierId == fornecedorId
                && (q.Status == "Cancelado" || q.Status == "Recusado"));
            decimal taxaCancelamento = totalQuotes > 0 ? ((decimal)quotesCancelados / totalQuotes) * 100 : 0;

            // Eventos atendidos (distintos)
            int eventosAtendidos = await _context.Pedidos
                .Where(p => produtoIds.Contains(p.ProdutoId)
                    && (p.StatusPedido == "Pago" || p.StatusPedido == "Entregue"))
                .Select(p => p.EventoId)
                .Distinct()
                .CountAsync();

            // Calcular pontuação geral (0 a 100)
            // Pesos: avaliação (30%), pedidos concluídos (20%), tempo resposta (15%),
            //        taxa aceitação (15%), taxa cancelamento (10%), eventos (10%)
            decimal pontuacao = 0;

            pontuacao += (mediaAvaliacoes / 5m) * 30m;

            decimal pedidosPontos = pedidosConcluidos >= 20 ? 20m : (pedidosConcluidos / 20m) * 20m;
            pontuacao += pedidosPontos;

            if (quotesRespondidos.Any())
            {
                decimal tempoScore = tempoMedioResposta <= 2 ? 15m
                    : tempoMedioResposta >= 72 ? 0m
                    : 15m * (1m - ((tempoMedioResposta - 2m) / 70m));
                pontuacao += Math.Max(0, tempoScore);
            }

            pontuacao += (taxaAceitacao / 100m) * 15m;
            pontuacao += ((100m - taxaCancelamento) / 100m) * 10m;

            decimal eventosPontos = eventosAtendidos >= 10 ? 10m : (eventosAtendidos / 10m) * 10m;
            pontuacao += eventosPontos;

            pontuacao = Math.Round(Math.Min(100, Math.Max(0, pontuacao)), 1);

            // Atualizar ou criar ranking
            var ranking = await _context.FornecedorRankings.FirstOrDefaultAsync(r => r.FornecedorId == fornecedorId);
            if (ranking == null)
            {
                ranking = new FornecedorRanking { FornecedorId = fornecedorId };
                _context.FornecedorRankings.Add(ranking);
            }

            ranking.MediaAvaliacoes = Math.Round(mediaAvaliacoes, 2);
            ranking.QuantidadeAvaliacoes = qtdAvaliacoes;
            ranking.PedidosConcluidos = pedidosConcluidos;
            ranking.TempoMedioRespostaHoras = Math.Round(tempoMedioResposta, 1);
            ranking.TaxaAceitacao = Math.Round(taxaAceitacao, 1);
            ranking.TaxaCancelamento = Math.Round(taxaCancelamento, 1);
            ranking.EventosAtendidos = eventosAtendidos;
            ranking.PontuacaoGeral = pontuacao;
            ranking.UltimaAtualizacao = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Atualizar AvaliacaoMedia do fornecedor
            fornecedor.AvaliacaoMedia = Math.Round(mediaAvaliacoes, 2);
            await _context.SaveChangesAsync();

            return ranking;
        }

        /// <summary>
        /// Recalcula o ranking de todos os fornecedores e atribui posições e badges.
        /// </summary>
        public async Task RecalcularTodosAsync()
        {
            var fornecedores = await _context.Fornecedores.Select(f => f.Id).ToListAsync();

            foreach (var id in fornecedores)
            {
                await RecalcularAsync(id);
            }

            var rankings = await _context.FornecedorRankings
                .OrderByDescending(r => r.PontuacaoGeral)
                .ToListAsync();

            for (int i = 0; i < rankings.Count; i++)
            {
                rankings[i].PosicaoRanking = i + 1;
                int topCount = Math.Max(3, (int)Math.Ceiling(rankings.Count * 0.1));
                rankings[i].IsTopFornecedor = (i + 1) <= topCount && rankings[i].PontuacaoGeral > 0;
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retorna o ranking de um fornecedor, recalculando se necessário.
        /// </summary>
        public async Task<FornecedorRanking?> ObterRankingAsync(int fornecedorId)
        {
            var ranking = await _context.FornecedorRankings
                .Include(r => r.Fornecedor)
                    .ThenInclude(f => f!.Pessoa)
                .FirstOrDefaultAsync(r => r.FornecedorId == fornecedorId);

            if (ranking == null || ranking.UltimaAtualizacao < DateTime.UtcNow.AddHours(-1))
            {
                ranking = await RecalcularAsync(fornecedorId);
                ranking = await _context.FornecedorRankings
                    .Include(r => r.Fornecedor)
                        .ThenInclude(f => f!.Pessoa)
                    .FirstOrDefaultAsync(r => r.FornecedorId == fornecedorId);
            }

            return ranking;
        }
    }
}
