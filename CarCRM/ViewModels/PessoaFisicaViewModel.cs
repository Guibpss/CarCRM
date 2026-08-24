using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class PessoaFisicaViewModel : PessoaViewModel
    {
        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "O CPF deve ter entre {2} e {1} caracteres")]
        [RegularExpression(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$", ErrorMessage = "CPF inválido (ex.: 12345678901 ou 123.456.789-01)")]
        public string CPF { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo RG é obrigatório")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "O RG deve ter entre {2} e {1} caracteres")]
        public string RG { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
    }
}
