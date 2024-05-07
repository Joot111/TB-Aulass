using System.ComponentModel.DataAnnotations;

namespace Aulas.Models
{
    public class Utilizadores{

        [Key] // PK
        public int Id { get; set; }

        /// <summary>
        /// Nome do Utilizador
        /// </summary>
        [Required(ErrorMessage ="O {0} é de preenchimento obrigatório.")]
        public string Nome { get; set;}

        /// <summary>
        /// Data de Nascimento
        /// </summary>
        [DataType(DataType.Date)] // informa a View como deve ser faito a Data
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        public DateOnly DataNascimento { get; set;}

        ///<summary>
        ///número de telemóvel do Utilizador
        /// </summary>
        [Display (Name="Telemóvel")] // permite setar o valor com o português correto
        [StringLength(40)] // permite escrever 9 caracteres
        ///<summary>
        /// 9[1236] [0-9] [0-9] [0-9] [0-9] [0-9] [0-9] [0-9]
        /// 9[1236] [0-9] {7}
        /// </summary>
        [RegularExpression("9[1236][0-9]{7}", 
            ErrorMessage = "o {0} só aceita 9 dígitos ")] // uma forma de predefinir um certo valor na expressão
        public string Telemovel { get; set;} 

        /// <summary>
        /// atributo para funconar como FK no relacionamento entre a 
        /// base de dados do 'negócio' e a base de dados 'autenticação'
        /// </summary>
        public string UserID { get; set;} 
    }
}
