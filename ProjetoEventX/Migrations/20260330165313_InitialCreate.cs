using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjetoEventX.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TipoUsuario = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Auditorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TipoEntidade = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EntidadeId = table.Column<int>(type: "integer", nullable: false),
                    TipoAcao = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Usuario = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    DataAcao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EnderecoIP = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    Descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DadosAntigos = table.Column<string>(type: "TEXT", nullable: true),
                    DadosNovos = table.Column<string>(type: "TEXT", nullable: true),
                    Navegador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    SistemaOperacional = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Sucesso = table.Column<bool>(type: "boolean", nullable: true),
                    MensagemErro = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomeLocal = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    EnderecoLocal = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Capacidade = table.Column<int>(type: "integer", nullable: false),
                    TipoLocal = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogsAcessos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EnderecoIP = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    DataAcesso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Usuario = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    UrlAcesso = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    UserAgent = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    TipoAcesso = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    AcessoBloqueado = table.Column<bool>(type: "boolean", nullable: true),
                    MotivoBloqueio = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogsAcessos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Link = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Endereco = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Telefone = table.Column<string>(type: "text", nullable: false),
                    Cpf = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Cidade = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UF = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FotoPerfilUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemplatesEventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TituloTemplateEvento = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TipoEstilo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Categoria = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplatesEventos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Convidados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PessoaId = table.Column<int>(type: "integer", nullable: false),
                    ConfirmaPresenca = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Convidados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Convidados_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomeNegocio = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Categoria = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FaixaPreco = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ContatoComercial = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Disponibilidade = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    PessoaId = table.Column<int>(type: "integer", nullable: false),
                    Cnpj = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: false),
                    TipoServico = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Cidade = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UF = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    AvaliacaoMedia = table.Column<decimal>(type: "numeric", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FotoPerfilUrl = table.Column<string>(type: "text", nullable: false),
                    Regiao = table.Column<string>(type: "text", nullable: false),
                    Telefone = table.Column<string>(type: "text", nullable: false),
                    ServicosOferecidos = table.Column<List<string>>(type: "text[]", nullable: false),
                    Galeria = table.Column<List<string>>(type: "text[]", nullable: false),
                    TotalAvaliacoes = table.Column<int>(type: "integer", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fornecedores_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Organizadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PessoaId = table.Column<int>(type: "integer", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizadores_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Preco = table.Column<decimal>(type: "numeric", nullable: false),
                    Tipo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FornecedorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PermitirComentarios = table.Column<bool>(type: "boolean", nullable: false),
                    PermitirEnvioFotos = table.Column<bool>(type: "boolean", nullable: false),
                    PermitirCurtidas = table.Column<bool>(type: "boolean", nullable: false),
                    AprovarFotosAntesPublicar = table.Column<bool>(type: "boolean", nullable: false),
                    PermitirVisualizacaoMural = table.Column<bool>(type: "boolean", nullable: false),
                    NomeEvento = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DataEvento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DescricaoEvento = table.Column<string>(type: "text", nullable: false),
                    TipoEvento = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CustoEstimado = table.Column<decimal>(type: "numeric", nullable: false),
                    StatusEvento = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Planejado"),
                    IdTemplateEvento = table.Column<int>(type: "integer", nullable: true),
                    HoraInicio = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    HoraFim = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    PublicoEstimado = table.Column<int>(type: "integer", nullable: false),
                    OrganizadorId = table.Column<int>(type: "integer", nullable: false),
                    LocalId = table.Column<int>(type: "integer", nullable: true),
                    Slug = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    ImagemCapa = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventos_Locais_LocalId",
                        column: x => x.LocalId,
                        principalTable: "Locais",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Eventos_Organizadores_OrganizadorId",
                        column: x => x.OrganizadorId,
                        principalTable: "Organizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Eventos_TemplatesEventos_IdTemplateEvento",
                        column: x => x.IdTemplateEvento,
                        principalTable: "TemplatesEventos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Administracoes",
                columns: table => new
                {
                    IdAdministrar = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ValorTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    Orcamento = table.Column<decimal>(type: "numeric", nullable: false),
                    IdEvento = table.Column<int>(type: "integer", nullable: false),
                    OrganizadorId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administracoes", x => x.IdAdministrar);
                    table.ForeignKey(
                        name: "FK_Administracoes_Eventos_IdEvento",
                        column: x => x.IdEvento,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Administracoes_Organizadores_OrganizadorId",
                        column: x => x.OrganizadorId,
                        principalTable: "Organizadores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssistentesVirtuais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AlgoritmoIA = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Sugestoes = table.Column<string>(type: "text", nullable: false),
                    DataGeracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EventoId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssistentesVirtuais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssistentesVirtuais_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AvaliacoesFornecedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nota = table.Column<int>(type: "integer", nullable: false),
                    Comentario = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    FornecedorId = table.Column<int>(type: "integer", nullable: false),
                    OrganizadorId = table.Column<int>(type: "integer", nullable: false),
                    EventoId = table.Column<int>(type: "integer", nullable: false),
                    DataAvaliacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvaliacoesFornecedores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvaliacoesFornecedores_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AvaliacoesFornecedores_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvaliacoesFornecedores_Organizadores_OrganizadorId",
                        column: x => x.OrganizadorId,
                        principalTable: "Organizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistEventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventoId = table.Column<int>(type: "integer", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Concluido = table.Column<bool>(type: "boolean", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataConclusao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Categoria = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Ordem = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistEventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistEventos_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    ActionType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventLogs_Eventos_EventId",
                        column: x => x.EventId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AvaliacaoFeedback = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ComentarioFeedback = table.Column<string>(type: "text", nullable: true),
                    DataEnvioFeedback = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TipoFeedback = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FornecedorId = table.Column<int>(type: "integer", nullable: true),
                    EventoId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Feedbacks_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ListasConvidados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConvidadoId = table.Column<int>(type: "integer", nullable: false),
                    EventoId = table.Column<int>(type: "integer", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConfirmaPresenca = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CheckInRealizado = table.Column<bool>(type: "boolean", nullable: false),
                    DataCheckIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CodigoQR = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListasConvidados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListasConvidados_Convidados_ConvidadoId",
                        column: x => x.ConvidadoId,
                        principalTable: "Convidados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListasConvidados_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MensagemChats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RemetenteId = table.Column<int>(type: "integer", nullable: false),
                    DestinatarioId = table.Column<int>(type: "integer", nullable: false),
                    TipoDestinatario = table.Column<string>(type: "text", nullable: false),
                    Conteudo = table.Column<string>(type: "text", nullable: false),
                    EventoId = table.Column<int>(type: "integer", nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EhRespostaAssistente = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MensagemChats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MensagemChats_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MensagemChats_Pessoas_DestinatarioId",
                        column: x => x.DestinatarioId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MensagemChats_Pessoas_RemetenteId",
                        column: x => x.RemetenteId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notificacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MensagemNotificacao = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Lida = table.Column<bool>(type: "boolean", nullable: false),
                    PrioridadeNotificacao = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DestinatarioId = table.Column<int>(type: "integer", nullable: false),
                    EventoId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificacoes_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notificacoes_Pessoas_DestinatarioId",
                        column: x => x.DestinatarioId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrcamentosSimulados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventoId = table.Column<int>(type: "integer", nullable: false),
                    Categoria = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ValorEstimado = table.Column<decimal>(type: "numeric", nullable: false),
                    DataSimulacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrcamentosSimulados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrcamentosSimulados_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventoId = table.Column<int>(type: "integer", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    PrecoTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    StatusPedido = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Pendente"),
                    DataPedido = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DespesaGerada = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    FornecedorId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedidos_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pedidos_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "SolicitacoesOrcamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrazoProposto = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ObservacoesProposta = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    DataResposta = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OrganizadorId = table.Column<int>(type: "integer", nullable: false),
                    FornecedorId = table.Column<int>(type: "integer", nullable: false),
                    EventoId = table.Column<int>(type: "integer", nullable: true),
                    Titulo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    TipoServicoDesejado = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DataEvento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LocalEvento = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrcamentoEstimado = table.Column<decimal>(type: "numeric", nullable: true),
                    QuantidadeConvidados = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false, defaultValue: "Pendente"),
                    RespostaFornecedor = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    ValorProposto = table.Column<decimal>(type: "numeric", nullable: true),
                    DataSolicitacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacoesOrcamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitacoesOrcamento_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SolicitacoesOrcamento_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitacoesOrcamento_Organizadores_OrganizadorId",
                        column: x => x.OrganizadorId,
                        principalTable: "Organizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TarefasEventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DescricaoTarefaEvento = table.Column<string>(type: "text", nullable: false),
                    ResponsavelId = table.Column<int>(type: "integer", nullable: true),
                    StatusConclusao = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Pendente"),
                    DataLimite = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PrioridadeTarefaEvento = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataConclusao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EventoId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarefasEventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TarefasEventos_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TarefasEventos_Pessoas_ResponsavelId",
                        column: x => x.ResponsavelId,
                        principalTable: "Pessoas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TemplatesConvites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomeTemplate = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EventoId = table.Column<int>(type: "integer", nullable: false),
                    OrganizadorId = table.Column<int>(type: "integer", nullable: false),
                    TituloConvite = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MensagemPrincipal = table.Column<string>(type: "text", nullable: false),
                    MensagemSecundaria = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CorFundo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CorTexto = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CorPrimaria = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FonteTitulo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FonteTexto = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TamanhoFonteTitulo = table.Column<int>(type: "integer", nullable: false),
                    TamanhoFonteTexto = table.Column<int>(type: "integer", nullable: false),
                    MostrarLogo = table.Column<bool>(type: "boolean", nullable: false),
                    MostrarFotoEvento = table.Column<bool>(type: "boolean", nullable: false),
                    MostrarMapa = table.Column<bool>(type: "boolean", nullable: false),
                    MostrarQRCode = table.Column<bool>(type: "boolean", nullable: false),
                    ImagemCabecalho = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ImagemRodape = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    EstiloLayout = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CSSPersonalizado = table.Column<string>(type: "text", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    PadraoSistema = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplatesConvites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplatesConvites_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplatesConvites_Organizadores_OrganizadorId",
                        column: x => x.OrganizadorId,
                        principalTable: "Organizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimelineEventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventoId = table.Column<int>(type: "integer", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DiasAntesEvento = table.Column<int>(type: "integer", nullable: false),
                    Concluido = table.Column<bool>(type: "boolean", nullable: false),
                    Categoria = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Ordem = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimelineEventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimelineEventos_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Despesas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventoId = table.Column<int>(type: "integer", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    DataDespesa = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Origem = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PedidoId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Despesas_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Despesas_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ItensPedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DescricaoItemPedido = table.Column<string>(type: "text", nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "numeric", nullable: false),
                    CategoriaItemPedido = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PedidoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensPedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensPedidos_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ValorTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    StatusPagamento = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Pendente"),
                    MetodoPagamento = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Comprovante = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PedidoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagamentos_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    SupplierId = table.Column<int>(type: "integer", nullable: false),
                    OrganizadorId = table.Column<int>(type: "integer", nullable: false),
                    ServiceName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    EstimatedValue = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false, defaultValue: "Pendente"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ResponseMessage = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    ResponseValue = table.Column<decimal>(type: "numeric", nullable: true),
                    ResponseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ContraPropostaValor = table.Column<decimal>(type: "numeric", nullable: true),
                    ContraPropostaMensagem = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    DataContraProposta = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RodadaAtual = table.Column<int>(type: "integer", nullable: false),
                    PrazoValidade = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PedidoGeradoId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quotes_Eventos_EventId",
                        column: x => x.EventId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quotes_Fornecedores_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quotes_Organizadores_OrganizadorId",
                        column: x => x.OrganizadorId,
                        principalTable: "Organizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quotes_Pedidos_PedidoGeradoId",
                        column: x => x.PedidoGeradoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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

            migrationBuilder.CreateTable(
                name: "QuoteMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuoteId = table.Column<int>(type: "integer", nullable: false),
                    SenderUserId = table.Column<int>(type: "integer", nullable: false),
                    SenderType = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Message = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    SentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuoteMessages_Quotes_QuoteId",
                        column: x => x.QuoteId,
                        principalTable: "Quotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administracoes_IdEvento",
                table: "Administracoes",
                column: "IdEvento");

            migrationBuilder.CreateIndex(
                name: "IX_Administracoes_OrganizadorId",
                table: "Administracoes",
                column: "OrganizadorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssistentesVirtuais_EventoId",
                table: "AssistentesVirtuais",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacoesFornecedores_EventoId",
                table: "AvaliacoesFornecedores",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacoesFornecedores_FornecedorId_OrganizadorId_EventoId",
                table: "AvaliacoesFornecedores",
                columns: new[] { "FornecedorId", "OrganizadorId", "EventoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacoesFornecedores_OrganizadorId",
                table: "AvaliacoesFornecedores",
                column: "OrganizadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistEventos_EventoId",
                table: "ChecklistEventos",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_ComentariosFeed_AutorId",
                table: "ComentariosFeed",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_ComentariosFeed_PublicacaoFeedId",
                table: "ComentariosFeed",
                column: "PublicacaoFeedId");

            migrationBuilder.CreateIndex(
                name: "IX_Convidados_PessoaId",
                table: "Convidados",
                column: "PessoaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_EventoId",
                table: "Despesas",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_PedidoId",
                table: "Despesas",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_EventLogs_CreatedAt",
                table: "EventLogs",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_EventLogs_EventId",
                table: "EventLogs",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_IdTemplateEvento",
                table: "Eventos",
                column: "IdTemplateEvento");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_LocalId",
                table: "Eventos",
                column: "LocalId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_OrganizadorId",
                table: "Eventos",
                column: "OrganizadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_Slug",
                table: "Eventos",
                column: "Slug",
                unique: true,
                filter: "\"Slug\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_EventoId",
                table: "Feedbacks",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_FornecedorId",
                table: "Feedbacks",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_PessoaId",
                table: "Fornecedores",
                column: "PessoaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FornecedorRankings_FornecedorId",
                table: "FornecedorRankings",
                column: "FornecedorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FornecedorRankings_PontuacaoGeral",
                table: "FornecedorRankings",
                column: "PontuacaoGeral");

            migrationBuilder.CreateIndex(
                name: "IX_FotoPortfolio_FornecedorId",
                table: "FotoPortfolio",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedidos_PedidoId",
                table: "ItensPedidos",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ListasConvidados_ConvidadoId",
                table: "ListasConvidados",
                column: "ConvidadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ListasConvidados_EventoId",
                table: "ListasConvidados",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_MensagemChats_DestinatarioId",
                table: "MensagemChats",
                column: "DestinatarioId");

            migrationBuilder.CreateIndex(
                name: "IX_MensagemChats_EventoId",
                table: "MensagemChats",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_MensagemChats_RemetenteId",
                table: "MensagemChats",
                column: "RemetenteId");

            migrationBuilder.CreateIndex(
                name: "IX_NegociacaoHistoricos_QuoteId",
                table: "NegociacaoHistoricos",
                column: "QuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_NegociacaoHistoricos_QuoteId_Rodada",
                table: "NegociacaoHistoricos",
                columns: new[] { "QuoteId", "Rodada" });

            migrationBuilder.CreateIndex(
                name: "IX_Notificacoes_DestinatarioId",
                table: "Notificacoes",
                column: "DestinatarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacoes_EventoId",
                table: "Notificacoes",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId_IsRead",
                table: "Notifications",
                columns: new[] { "UserId", "IsRead" });

            migrationBuilder.CreateIndex(
                name: "IX_OrcamentosSimulados_EventoId",
                table: "OrcamentosSimulados",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizadores_PessoaId",
                table: "Organizadores",
                column: "PessoaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_PedidoId",
                table: "Pagamentos",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_EventoId",
                table: "Pedidos",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_FornecedorId",
                table: "Pedidos",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ProdutoId",
                table: "Pedidos",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_FornecedorId",
                table: "Produtos",
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
                name: "IX_QuoteMessages_QuoteId",
                table: "QuoteMessages",
                column: "QuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_EventId",
                table: "Quotes",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_OrganizadorId",
                table: "Quotes",
                column: "OrganizadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_PedidoGeradoId",
                table: "Quotes",
                column: "PedidoGeradoId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_SupplierId",
                table: "Quotes",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicoFornecedor_FornecedorId",
                table: "ServicoFornecedor",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacoesOrcamento_EventoId",
                table: "SolicitacoesOrcamento",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacoesOrcamento_FornecedorId",
                table: "SolicitacoesOrcamento",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacoesOrcamento_OrganizadorId",
                table: "SolicitacoesOrcamento",
                column: "OrganizadorId");

            migrationBuilder.CreateIndex(
                name: "IX_TarefasEventos_EventoId",
                table: "TarefasEventos",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_TarefasEventos_ResponsavelId",
                table: "TarefasEventos",
                column: "ResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplatesConvites_EventoId",
                table: "TemplatesConvites",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplatesConvites_OrganizadorId",
                table: "TemplatesConvites",
                column: "OrganizadorId");

            migrationBuilder.CreateIndex(
                name: "IX_TimelineEventos_EventoId",
                table: "TimelineEventos",
                column: "EventoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administracoes");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AssistentesVirtuais");

            migrationBuilder.DropTable(
                name: "Auditorias");

            migrationBuilder.DropTable(
                name: "AvaliacoesFornecedores");

            migrationBuilder.DropTable(
                name: "ChecklistEventos");

            migrationBuilder.DropTable(
                name: "ComentariosFeed");

            migrationBuilder.DropTable(
                name: "Despesas");

            migrationBuilder.DropTable(
                name: "EventLogs");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "FornecedorRankings");

            migrationBuilder.DropTable(
                name: "FotoPortfolio");

            migrationBuilder.DropTable(
                name: "ItensPedidos");

            migrationBuilder.DropTable(
                name: "ListasConvidados");

            migrationBuilder.DropTable(
                name: "LogsAcessos");

            migrationBuilder.DropTable(
                name: "MensagemChats");

            migrationBuilder.DropTable(
                name: "NegociacaoHistoricos");

            migrationBuilder.DropTable(
                name: "Notificacoes");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "OrcamentosSimulados");

            migrationBuilder.DropTable(
                name: "Pagamentos");

            migrationBuilder.DropTable(
                name: "QuoteMessages");

            migrationBuilder.DropTable(
                name: "ServicoFornecedor");

            migrationBuilder.DropTable(
                name: "SolicitacoesOrcamento");

            migrationBuilder.DropTable(
                name: "TarefasEventos");

            migrationBuilder.DropTable(
                name: "TemplatesConvites");

            migrationBuilder.DropTable(
                name: "TimelineEventos");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PublicacoesFeed");

            migrationBuilder.DropTable(
                name: "Convidados");

            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Locais");

            migrationBuilder.DropTable(
                name: "Organizadores");

            migrationBuilder.DropTable(
                name: "TemplatesEventos");

            migrationBuilder.DropTable(
                name: "Fornecedores");

            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
