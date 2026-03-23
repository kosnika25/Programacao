using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjetoEventX.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarFotoPerfilUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ObservacoesProposta",
                table: "SolicitacoesOrcamento",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrazoProposto",
                table: "SolicitacoesOrcamento",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FotoPerfilUrl",
                table: "Pessoas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Fornecedores",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContatoComercial",
                table: "Fornecedores",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Fornecedores",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Disponibilidade",
                table: "Fornecedores",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FaixaPreco",
                table: "Fornecedores",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FotoPerfilUrl",
                table: "Fornecedores",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<List<string>>(
                name: "Galeria",
                table: "Fornecedores",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeNegocio",
                table: "Fornecedores",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Regiao",
                table: "Fornecedores",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<List<string>>(
                name: "ServicosOferecidos",
                table: "Fornecedores",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Fornecedores",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TotalAvaliacoes",
                table: "Fornecedores",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "AprovarFotosAntesPublicar",
                table: "Eventos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PermitirComentarios",
                table: "Eventos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PermitirCurtidas",
                table: "Eventos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PermitirEnvioFotos",
                table: "Eventos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PermitirVisualizacaoMural",
                table: "Eventos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "FotoPortfolio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    FornecedorId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FotoPortfolio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FotoPortfolio_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PublicacoesFeed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventoId = table.Column<int>(type: "integer", nullable: false),
                    AutorId = table.Column<int>(type: "integer", nullable: false),
                    Texto = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    ImagemUrl = table.Column<string>(type: "text", nullable: true),
                    DataHora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Fixado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicacoesFeed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicacoesFeed_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicacoesFeed_Organizadores_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Organizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicoFornecedor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Preco = table.Column<decimal>(type: "numeric", nullable: true),
                    FornecedorId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicoFornecedor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicoFornecedor_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComentariosFeed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PublicacaoFeedId = table.Column<int>(type: "integer", nullable: false),
                    AutorId = table.Column<int>(type: "integer", nullable: false),
                    Texto = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DataHora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComentariosFeed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComentariosFeed_Convidados_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Convidados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComentariosFeed_PublicacoesFeed_PublicacaoFeedId",
                        column: x => x.PublicacaoFeedId,
                        principalTable: "PublicacoesFeed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComentariosFeed_AutorId",
                table: "ComentariosFeed",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_ComentariosFeed_PublicacaoFeedId",
                table: "ComentariosFeed",
                column: "PublicacaoFeedId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoPortfolio_FornecedorId",
                table: "FotoPortfolio",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicacoesFeed_AutorId",
                table: "PublicacoesFeed",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicacoesFeed_EventoId",
                table: "PublicacoesFeed",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicoFornecedor_FornecedorId",
                table: "ServicoFornecedor",
                column: "FornecedorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComentariosFeed");

            migrationBuilder.DropTable(
                name: "FotoPortfolio");

            migrationBuilder.DropTable(
                name: "ServicoFornecedor");

            migrationBuilder.DropTable(
                name: "PublicacoesFeed");

            migrationBuilder.DropColumn(
                name: "ObservacoesProposta",
                table: "SolicitacoesOrcamento");

            migrationBuilder.DropColumn(
                name: "PrazoProposto",
                table: "SolicitacoesOrcamento");

            migrationBuilder.DropColumn(
                name: "FotoPerfilUrl",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "ContatoComercial",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Disponibilidade",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "FaixaPreco",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "FotoPerfilUrl",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Galeria",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "NomeNegocio",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Regiao",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "ServicosOferecidos",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "TotalAvaliacoes",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "AprovarFotosAntesPublicar",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "PermitirComentarios",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "PermitirCurtidas",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "PermitirEnvioFotos",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "PermitirVisualizacaoMural",
                table: "Eventos");
        }
    }
}
