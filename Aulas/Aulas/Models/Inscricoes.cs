using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aulas.Models
{
    [PrimaryKey(nameof(UCFK), nameof(AlunoFK))]
    public class Inscricoes{

        public DateTime DataIncricao { get; set; }

        /**
        * Vamos criar as Relações (FKs) com outras tabelas
        *  tabelas UnidadesCurriculares e Alunos
        */

        
        // FK para tabela Unidades Curriculares
        [ForeignKey(nameof(UC))]
     //   [Key] // PK Composta, na EF <= 6.0
        public int UCFK { get; set; }

        public UnidadesCurriculares UC { get; set; }

        // FK para tabela Alunos
        [ForeignKey(nameof(Aluno))]
     //   [Key] // PK Composta
        public int AlunoFK { get; set;}

        public Alunos Aluno { get; set; }  
    }
}
