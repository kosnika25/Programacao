using System.ComponentModel.DataAnnotations;

namespace ProjetoEventX.Models
{
    public class TemplateConvite
    {
        public int Id { get; set; }

        [Required]
        public string NomeTemplate { get; set; } = string.Empty;

        public string? EstiloLayout { get; set; }
        public bool Ativo { get; set; } = true;

        public string? CorFundo { get; set; }
        public string? CorTexto { get; set; }
        public string? CorPrimaria { get; set; }
        public string? Fonte { get; set; }
        public string? TamanhoFonte { get; set; }

        public string? TituloConvite { get; set; }
        public string? Titulo { get; set; }
        public string? CSSPersonalizado { get; set; }
        public string? FonteTitulo { get; set; }
        public string? MensagemPrincipal { get; set; }
        public string? MensagemSecundaria { get; set; }
        public bool PadraoSistema { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool MostrarMapa { get; set; } = true;

        public bool MostrarQRCode { get; set; } = true;

        public bool MostrarFotoEvento { get; set; } = true;

        public bool MostrarLogo { get; set; } = true;
        public string? Saudacao { get; set; }
        public string? Mensagem { get; set; }
        public string? TextoBotao { get; set; }

        public int? EventoId { get; set; }
        public virtual Evento? Evento { get; set; }

        public string? LayoutJson { get; set; }


        public string GerarHTMLConvite(string nomeConvidado, string linkConfirmacao)
        {
            var saudacaoFormatada = Saudacao?.Replace("Nome do Convidado", nomeConvidado)
                                     ?? $"Olá {nomeConvidado},";

            var mensagemFinal = Mensagem ?? "Você está convidado!";

            return $@"
        <div style='font-family: {Fonte}; background-color: {CorFundo}; color: {CorTexto}; padding: 20px; border-radius: 10px;'>
            <h1 style='color: {CorPrimaria};'>{FonteTitulo}</h1>
            <p>{saudacaoFormatada}</p>
            <p>{mensagemFinal}</p>

            <div style='text-align: center; margin-top: 20px;'>
                <a href='{linkConfirmacao}' 
                   style='background-color: {CorPrimaria}; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>
                   {TextoBotao ?? "Confirmar Presença"}
                </a>
            </div>
        </div>
    ";
        }
    }
}