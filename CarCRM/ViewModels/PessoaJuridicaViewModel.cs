using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class PessoaJuridicaViewModel : PessoaViewModel
    {
        [Required(ErrorMessage = "O campo Razão Social é obrigatório")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "A Razão Social deve ter entre {2} e {1} caracteres")]
        public string RazaoSocial { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Nome Fantasia é obrigatório")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "O Nome Fantasia deve ter entre {2} e {1} caracteres")]
        public string NomeFantasia { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Nome Interno é obrigatório")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "O Nome Interno deve ter entre {2} e {1} caracteres")]
        public string NomeInterno { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo CNPJ é obrigatório")]
        [StringLength(18, MinimumLength = 14, ErrorMessage = "O CNPJ deve ter entre {2} e {1} caracteres")]
        [RegularExpression(@"^\d{2}\.?\d{3}\.?\d{3}/?\d{4}-?\d{2}$", ErrorMessage = "CNPJ inválido (ex.: 12345678000199 ou 12.345.678/0001-99)")]
        public string CNPJ { get; set; } = string.Empty;
    }
}
