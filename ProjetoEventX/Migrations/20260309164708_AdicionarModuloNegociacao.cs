using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjetoEventX.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarModuloNegociacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContraPropostaMensagem",
                table: "Quotes",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ContraPropostaValor",
                table: "Quotes",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataContraProposta",
                table: "Quotes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PrazoValidade",
                table: "Quotes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RodadaAtual",
                table: "Quotes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NegociacaoHistoricos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuoteId = table.Column<int>(type: "integer", nullable: false),
                    Rodada = table.Column<int>(type: "integer", nullable: false),
                    TipoAcao = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: true),
                    Mensagem = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    TipoUsuario = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    DataAcao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NegociacaoHistoricos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NegociacaoHistoricos_Quotes_QuoteId",
                        column: x => x.QuoteId,
                        principalTable: "Quotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NegociacaoHistoricos_QuoteId",
                table: "NegociacaoHistoricos",
                column: "QuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_NegociacaoHistoricos_QuoteId_Rodada",
                table: "NegociacaoHistoricos",
                columns: new[] { "QuoteId", "Rodada" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NegociacaoHistoricos");

            migrationBuilder.DropColumn(
                name: "ContraPropostaMensagem",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "ContraPropostaValor",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "DataContraProposta",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "PrazoValidade",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "RodadaAtual",
                table: "Quotes");
        }
    }
}
