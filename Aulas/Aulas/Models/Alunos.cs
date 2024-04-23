using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aulas.Models
{
    /// <summary>
    /// Alunos é extensão de Utilizadores 
    /// </summary>
    public class Alunos : Utilizadores {

        public Alunos() { 
            ListaInscricoes = new HashSet<Inscricoes>();
        }
        /// <summary>
        /// Número atributo ao Aluno
        /// </summary>
        public int NumAluno { get; set; }
        /// <summary>
        /// Valor a pagar pelo Aluno pela frequência do Curso
        /// </summary>
        public decimal Propinas { get; set; }
        /// <summary>
        /// Data da Matrícula
        /// </summary>
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DataMatricula { get; set;}

        /**
        * Vamos criar as Relações (FKs) com outras tabelas
        * 
        */

        // relacionamento do tipo N - 1
        ///<summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(Curso))] // anotação que liga CursoFK a Curso
        public int CursoFK { get; set; } // Será FK para tabela Cursos

        public Cursos Curso { get; set; } // em rigor esta inscrição está ligado á tabela cursos

        // relacionamento do tipo N - M, COM atributos de relacionamento
        // não vou referenciar a tabela 'final',
        // mas a tabela 'meio' do relacionamento
        // vamos referenciar o relacionamento N - M á custa
        /// <summary>
        /// Lista de Inscrição
        /// </summary>
        public ICollection<Inscricoes> ListaInscricoes { get; set; }
    }
}
