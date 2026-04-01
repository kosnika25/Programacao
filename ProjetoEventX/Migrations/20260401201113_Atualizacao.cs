using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoEventX.Migrations
{
    /// <inheritdoc />
    public partial class Atualizacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemplatesConvites_Eventos_EventoId",
                table: "TemplatesConvites");

            migrationBuilder.DropForeignKey(
                name: "FK_TemplatesConvites_Organizadores_OrganizadorId",
                table: "TemplatesConvites");

            migrationBuilder.DropIndex(
                name: "IX_TemplatesConvites_OrganizadorId",
                table: "TemplatesConvites");

            migrationBuilder.DropColumn(
                name: "FonteTexto",
                table: "TemplatesConvites");

            migrationBuilder.DropColumn(
                name: "ImagemCabecalho",
                table: "TemplatesConvites");

            migrationBuilder.DropColumn(
                name: "ImagemRodape",
                table: "TemplatesConvites");

            migrationBuilder.DropColumn(
                name: "OrganizadorId",
                table: "TemplatesConvites");

            migrationBuilder.DropColumn(
                name: "TamanhoFonteTexto",
                table: "TemplatesConvites");

            migrationBuilder.DropColumn(
                name: "TamanhoFonteTitulo",
                table: "TemplatesConvites");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TemplatesConvites");

            migrationBuilder.AlterColumn<string>(
                name: "TituloConvite",
                table: "TemplatesConvites",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "NomeTemplate",
                table: "TemplatesConvites",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "MensagemSecundaria",
                table: "TemplatesConvites",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MensagemPrincipal",
                table: "TemplatesConvites",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FonteTitulo",
                table: "TemplatesConvites",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventoId",
                table: "TemplatesConvites",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "EstiloLayout",
                table: "TemplatesConvites",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "CorTexto",
                table: "TemplatesConvites",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "CorPrimaria",
                table: "TemplatesConvites",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "CorFundo",
                table: "TemplatesConvites",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Fonte",
                table: "TemplatesConvites",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LayoutJson",
                table: "TemplatesConvites",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mensagem",
                table: "TemplatesConvites",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Saudacao",
                table: "TemplatesConvites",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TamanhoFonte",
                table: "TemplatesConvites",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextoBotao",
                table: "TemplatesConvites",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "TemplatesConvites",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TemplatesConvites_Eventos_EventoId",
                table: "TemplatesConvites",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemplatesConvites_Eventos_EventoId",
                table: "TemplatesConvites");

            migrationBuilder.DropColumn(
                name: "Fonte",
                table: "TemplatesConvites");

            migrationBuilder.DropColumn(
                name: "LayoutJson",
                table: "TemplatesConvites");

            migrationBuilder.DropColumn(
                name: "Mensagem",
                table: "TemplatesConvites");

            migrationBuilder.DropColumn(
                name: "Saudacao",
                table: "TemplatesConvites");

            migrationBuilder.DropColumn(
                name: "TamanhoFonte",
                table: "TemplatesConvites");

            migrationBuilder.DropColumn(
                name: "TextoBotao",
                table: "TemplatesConvites");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "TemplatesConvites");

            migrationBuilder.AlterColumn<string>(
                name: "TituloConvite",
                table: "TemplatesConvites",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeTemplate",
                table: "TemplatesConvites",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "MensagemSecundaria",
                table: "TemplatesConvites",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MensagemPrincipal",
                table: "TemplatesConvites",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FonteTitulo",
                table: "TemplatesConvites",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventoId",
                table: "TemplatesConvites",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EstiloLayout",
                table: "TemplatesConvites",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CorTexto",
                table: "TemplatesConvites",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CorPrimaria",
                table: "TemplatesConvites",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CorFundo",
                table: "TemplatesConvites",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FonteTexto",
                table: "TemplatesConvites",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagemCabecalho",
                table: "TemplatesConvites",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagemRodape",
                table: "TemplatesConvites",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizadorId",
                table: "TemplatesConvites",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TamanhoFonteTexto",
                table: "TemplatesConvites",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TamanhoFonteTitulo",
                table: "TemplatesConvites",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TemplatesConvites",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_TemplatesConvites_OrganizadorId",
                table: "TemplatesConvites",
                column: "OrganizadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TemplatesConvites_Eventos_EventoId",
                table: "TemplatesConvites",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TemplatesConvites_Organizadores_OrganizadorId",
                table: "TemplatesConvites",
                column: "OrganizadorId",
                principalTable: "Organizadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
