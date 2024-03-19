using System.ComponentModel.DataAnnotations.Schema;

namespace Aulas.Models
{
    public class UnidadesCurriculares{

        //Vamos utilizar a Entity Framework

        // https://learn.microsoft.com/en-us/ef/

        public UnidadesCurriculares() { 
            ListaProfessores = new HashSet<Professores>();
        }   

        public int Id { get; set; }

        public string Nome { get; set; }

        public int AnoCurricular { get; set;}

        public int Semestre { get; set;}

        /**
         * Vamos criar as Relações (FKs) com outras tabelas
         * 
         */

        // relacionamento do tipo N - 1
        [ForeignKey(nameof(Curso))] // anotação que liga CursoFK a Curso
        public int CursoFK { get; set;} // Será FK para tabela Cursos

        public Cursos Curso {  get; set;} // em rigor esta inscrição está ligado á tabela cursos

        // relacionamento do tipo N - 1
        public ICollection<Professores> ListaProfessores { get; set; }
    }
}
