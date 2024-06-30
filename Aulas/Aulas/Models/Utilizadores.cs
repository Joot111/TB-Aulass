using System.ComponentModel.DataAnnotations;

namespace Aulas.Models
{
    /// <summary>
    /// Classe genérica dos Utilizadores
    /// </summary>
    public class Utilizadores
    {


        [Key] // PK
        public int Id { get; set; }
        /// <summary>
        /// Nome do Utilizador
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        public string Nome { get; set; }

        /// <summary>
        /// Data de Nascimento
        /// </summary>
        [DataType(DataType.Date)] // informa a view de como deve tratar este atributo
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateOnly DataNascimento { get; set; }

        /// <summary>
        /// Número do Telemóvel do Utilizador
        /// </summary>

        [Display(Name = "Telemóvel")] // permite o uso de acentos/caracteres especiais quando é dado display deste atributo
        /*
         *  913456789 - sem indicativo
         *  
         *  +351913456789 - ambito internacional
         *        =
         *  00351913456789
         */
        [StringLength(9)] // define o tamanho máximo da string
                          // é um filtro 
        [RegularExpression("9[1236][0-9]{7}",
              /*
               * Ambas as formas estão certas porém a forma em baixo é mais eficiente
               * 9[1236][0-9][0-9][0-9][0-9][0-9][0-9][0-9]
               * 9[1236][0-9]{7}
               */
              ErrorMessage = "o {0} só aceita 9 dígitos")] // o {0} é substituido pelo nome do atributo
        public string Telemovel { get; set; }

        /// <summary>
        /// atributo para funcionar como FK 
        /// no relacionamento entre a
        /// base de dados do 'negócio' e a base de dados do 'autenticação'
        /// </summary>
        public string UserId { get; set; }

    }
}
