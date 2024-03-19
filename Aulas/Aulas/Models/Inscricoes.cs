using System.ComponentModel.DataAnnotations.Schema;

namespace Aulas.Models
{
    public class Inscricoes{
        public DateTime DataIncricao { get; set; }

        /**
        * Vamos criar as Relações (FKs) com outras tabelas
        *  tabelas UnidadesCurriculares e Alunos
        */

        
        // FK para tabela Unidades Curriculares
        [ForeignKey(nameof(UC))]

        public int UCFK { get; set; }

        public UnidadesCurriculares UC { get; set; }

        // FK para tabela Alunos
        [ForeignKey(nameof(Aluno))]

        public int AlunoFK { get; set;}

        public Alunos Aluno { get; set; }  
    }
}
