using CarCRM.Models;
using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class ClienteViewModel : ViewModelBase
    {
        public int PessoaId { get; set; }

        public PessoaViewModel? Pessoa { get; set; }

        [StringLength(14, MinimumLength = 11, ErrorMessage = "O CPF deve ter entre {2} e {1} caracteres")]
        [RegularExpression(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$", ErrorMessage = "CPF inválido (ex.: 12345678901 ou 123.456.789-01)")]
        public string? CPF { get; set; }

        [StringLength(20, MinimumLength = 5, ErrorMessage = "O RG deve ter entre {2} e {1} caracteres")]
        public string? RG { get; set; }

        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [StringLength(150, MinimumLength = 2, ErrorMessage = "A Razão Social deve ter entre {2} e {1} caracteres")]
        public string? RazaoSocial { get; set; }

        [StringLength(150, MinimumLength = 2, ErrorMessage = "O Nome Fantasia deve ter entre {2} e {1} caracteres")]
        public string? NomeFantasia { get; set; }

        [StringLength(150, MinimumLength = 2, ErrorMessage = "O Nome Interno deve ter entre {2} e {1} caracteres")]
        public string? NomeInterno { get; set; }

        [StringLength(18, MinimumLength = 14, ErrorMessage = "O CNPJ deve ter entre {2} e {1} caracteres")]
        [RegularExpression(@"^\d{2}\.?\d{3}\.?\d{3}/?\d{4}-?\d{2}$", ErrorMessage = "CNPJ inválido (ex.: 12345678000199 ou 12.345.678/0001-99)")]
        public string? CNPJ { get; set; }
    }
}
