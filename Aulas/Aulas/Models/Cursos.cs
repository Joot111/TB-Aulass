using System.ComponentModel.DataAnnotations;

namespace Aulas.Models
{
    public class Cursos{

        public Cursos() {
            ListaUCs = new HashSet<UnidadesCurriculares>();
            ListaAlunos = new HashSet<Alunos>();
        }

        [Key] // PK
        public int Id { get; set; }

        [StringLength(100)]
        public string Nome { get; set; }


        [Display(Name="Logótipo")] // altera o nome que aparece no ecrã
        [StringLength(50)]// define o tamanho máximo como 50 caracteres
        public string? Logotipo { get; set;}// o ? torna o preenchimento facultativo


        /**
        * Vamos criar as Relações (FKs) com outras tabelas
        * 
        */

        // relacionamento com as Unidades Curriculares

        public ICollection<UnidadesCurriculares> ListaUCs { get; set; }

        // relacionamento com os Alunos
        public ICollection<Alunos> ListaAlunos { get; set; }
    }
}
