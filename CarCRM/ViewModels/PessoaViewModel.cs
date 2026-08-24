using CarCRM.Models;
using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class PessoaViewModel : ViewModelBase
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(120, MinimumLength = 3, ErrorMessage = "O Nome deve ter entre {2} e {1} caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Email é obrigatório")]
        [StringLength(150, ErrorMessage = "O Email deve ter no máximo {1} caracteres")]
        [EmailAddress(ErrorMessage = "Email inválido (ex.: nome@empresa.com.br)")]
        public string Email { get; set; } = string.Empty;

        public ICollection<TelefoneViewModel> Telefones { get; set; } = new List<TelefoneViewModel>();
    }
}
