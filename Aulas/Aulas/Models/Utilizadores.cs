using System.ComponentModel.DataAnnotations;

namespace Aulas.Models
{
    public class Utilizadores{

        [Key] // PK
        public int Id { get; set; }

        /// <summary>
        /// Nome do Utilizador
        /// </summary>
        public string Nome { get; set;}

        /// <summary>
        /// Data de Nascimento
        /// </summary>
        public DateOnly DataNascimento { get; set;}

        ///<summary>
        ///número de telemóvel do Utilizador
        /// </summary>
        [Display (Name="Telemóvel")] // permite setar o valor com o português correto
        [StringLength(9)] // permite escrever 9 caracteres
        ///<summary>
        /// 9[1236] [0-9] [0-9] [0-9] [0-9] [0-9] [0-9] [0-9]
        /// 9[1236] [0-9] {7}
        /// </summary>
        [RegularExpression("9[1236] [0-9] {7}", ErrorMessage = "o {0} só aceita 9 dígitos ")] // uma forma de predefinir um certo valor na expressão
        public string Telemovel { get; set;} 


    }
}
