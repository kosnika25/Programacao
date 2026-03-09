using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoEventX.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoModeloPendente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MensagemChat_Eventos_EventoId",
                table: "MensagemChat");

            migrationBuilder.DropForeignKey(
                name: "FK_MensagemChat_Pessoas_DestinatarioId",
                table: "MensagemChat");

            migrationBuilder.DropForeignKey(
                name: "FK_MensagemChat_Pessoas_RemetenteId",
                table: "MensagemChat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MensagemChat",
                table: "MensagemChat");

            migrationBuilder.RenameTable(
                name: "MensagemChat",
                newName: "MensagemChats");

            migrationBuilder.RenameIndex(
                name: "IX_MensagemChat_RemetenteId",
                table: "MensagemChats",
                newName: "IX_MensagemChats_RemetenteId");

            migrationBuilder.RenameIndex(
                name: "IX_MensagemChat_EventoId",
                table: "MensagemChats",
                newName: "IX_MensagemChats_EventoId");

            migrationBuilder.RenameIndex(
                name: "IX_MensagemChat_DestinatarioId",
                table: "MensagemChats",
                newName: "IX_MensagemChats_DestinatarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MensagemChats",
                table: "MensagemChats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MensagemChats_Eventos_EventoId",
                table: "MensagemChats",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MensagemChats_Pessoas_DestinatarioId",
                table: "MensagemChats",
                column: "DestinatarioId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MensagemChats_Pessoas_RemetenteId",
                table: "MensagemChats",
                column: "RemetenteId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MensagemChats_Eventos_EventoId",
                table: "MensagemChats");

            migrationBuilder.DropForeignKey(
                name: "FK_MensagemChats_Pessoas_DestinatarioId",
                table: "MensagemChats");

            migrationBuilder.DropForeignKey(
                name: "FK_MensagemChats_Pessoas_RemetenteId",
                table: "MensagemChats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MensagemChats",
                table: "MensagemChats");

            migrationBuilder.RenameTable(
                name: "MensagemChats",
                newName: "MensagemChat");

            migrationBuilder.RenameIndex(
                name: "IX_MensagemChats_RemetenteId",
                table: "MensagemChat",
                newName: "IX_MensagemChat_RemetenteId");

            migrationBuilder.RenameIndex(
                name: "IX_MensagemChats_EventoId",
                table: "MensagemChat",
                newName: "IX_MensagemChat_EventoId");

            migrationBuilder.RenameIndex(
                name: "IX_MensagemChats_DestinatarioId",
                table: "MensagemChat",
                newName: "IX_MensagemChat_DestinatarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MensagemChat",
                table: "MensagemChat",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MensagemChat_Eventos_EventoId",
                table: "MensagemChat",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MensagemChat_Pessoas_DestinatarioId",
                table: "MensagemChat",
                column: "DestinatarioId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MensagemChat_Pessoas_RemetenteId",
                table: "MensagemChat",
                column: "RemetenteId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
