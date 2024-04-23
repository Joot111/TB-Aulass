using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aulas.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdeusUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inscricoes_Utilizadores_AlunoFK",
                table: "Inscricoes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessoresUnidadesCurriculares_Utilizadores_ListaProfessoresId",
                table: "ProfessoresUnidadesCurriculares");

            migrationBuilder.DropForeignKey(
                name: "FK_Utilizadores_Cursos_CursoFK",
                table: "Utilizadores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Utilizadores",
                table: "Utilizadores");

            migrationBuilder.DropIndex(
                name: "IX_Utilizadores_CursoFK",
                table: "Utilizadores");

            migrationBuilder.DropColumn(
                name: "CursoFK",
                table: "Utilizadores");

            migrationBuilder.DropColumn(
                name: "DataMatricula",
                table: "Utilizadores");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Utilizadores");

            migrationBuilder.DropColumn(
                name: "NumAluno",
                table: "Utilizadores");

            migrationBuilder.DropColumn(
                name: "Propinas",
                table: "Utilizadores");

            migrationBuilder.RenameTable(
                name: "Utilizadores",
                newName: "Professores");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Professores",
                table: "Professores",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumAluno = table.Column<int>(type: "int", nullable: false),
                    Propinas = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataMatricula = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CursoFK = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    Telemovel = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alunos_Cursos_CursoFK",
                        column: x => x.CursoFK,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_CursoFK",
                table: "Alunos",
                column: "CursoFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Inscricoes_Alunos_AlunoFK",
                table: "Inscricoes",
                column: "AlunoFK",
                principalTable: "Alunos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessoresUnidadesCurriculares_Professores_ListaProfessoresId",
                table: "ProfessoresUnidadesCurriculares",
                column: "ListaProfessoresId",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inscricoes_Alunos_AlunoFK",
                table: "Inscricoes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessoresUnidadesCurriculares_Professores_ListaProfessoresId",
                table: "ProfessoresUnidadesCurriculares");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Professores",
                table: "Professores");

            migrationBuilder.RenameTable(
                name: "Professores",
                newName: "Utilizadores");

            migrationBuilder.AddColumn<int>(
                name: "CursoFK",
                table: "Utilizadores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataMatricula",
                table: "Utilizadores",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Utilizadores",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumAluno",
                table: "Utilizadores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Propinas",
                table: "Utilizadores",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Utilizadores",
                table: "Utilizadores",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Utilizadores_CursoFK",
                table: "Utilizadores",
                column: "CursoFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Inscricoes_Utilizadores_AlunoFK",
                table: "Inscricoes",
                column: "AlunoFK",
                principalTable: "Utilizadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessoresUnidadesCurriculares_Utilizadores_ListaProfessoresId",
                table: "ProfessoresUnidadesCurriculares",
                column: "ListaProfessoresId",
                principalTable: "Utilizadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Utilizadores_Cursos_CursoFK",
                table: "Utilizadores",
                column: "CursoFK",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
