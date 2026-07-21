using CarCRM.Models;
using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class PessoaViewModel : ViewModelBase
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório")]
        public string Email { get; set; }

        public ICollection<TelefoneViewModel> Telefones { get; set; } = new List<TelefoneViewModel>();
    }
}
