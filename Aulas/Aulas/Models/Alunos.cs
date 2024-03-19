using System.ComponentModel.DataAnnotations.Schema;

namespace Aulas.Models
{
    public class Alunos{

        public int NumAluno { get; set; }

        public decimal Propinas { get; set; }

        public DateTime DataMatricula { get; set;}

        /**
        * Vamos criar as Relações (FKs) com outras tabelas
        * 
        */

        // relacionamento do tipo N - 1

        [ForeignKey(nameof(Curso))] // anotação que liga CursoFK a Curso
        public int CursoFK { get; set; } // Será FK para tabela Cursos

        public Cursos Curso { get; set; } // em rigor esta inscrição está ligado á tabela cursos
    }
}
