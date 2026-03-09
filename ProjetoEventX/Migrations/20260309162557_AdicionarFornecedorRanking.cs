using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjetoEventX.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarFornecedorRanking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FornecedorRankings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FornecedorId = table.Column<int>(type: "integer", nullable: false),
                    MediaAvaliacoes = table.Column<decimal>(type: "numeric", nullable: false),
                    QuantidadeAvaliacoes = table.Column<int>(type: "integer", nullable: false),
                    PedidosConcluidos = table.Column<int>(type: "integer", nullable: false),
                    TempoMedioRespostaHoras = table.Column<decimal>(type: "numeric", nullable: false),
                    TaxaAceitacao = table.Column<decimal>(type: "numeric", nullable: false),
                    TaxaCancelamento = table.Column<decimal>(type: "numeric", nullable: false),
                    EventosAtendidos = table.Column<int>(type: "integer", nullable: false),
                    PontuacaoGeral = table.Column<decimal>(type: "numeric", nullable: false),
                    PosicaoRanking = table.Column<int>(type: "integer", nullable: false),
                    IsTopFornecedor = table.Column<bool>(type: "boolean", nullable: false),
                    UltimaAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FornecedorRankings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FornecedorRankings_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FornecedorRankings_FornecedorId",
                table: "FornecedorRankings",
                column: "FornecedorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FornecedorRankings_PontuacaoGeral",
                table: "FornecedorRankings",
                column: "PontuacaoGeral");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FornecedorRankings");
        }
    }
}
