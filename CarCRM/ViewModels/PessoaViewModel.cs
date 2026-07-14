using CarCRM.Models;

namespace CarCRM.ViewModels
{
    public class PessoaViewModel : ViewModelBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }

        public ICollection<TelefoneViewModel> Telefones { get; set; } = new List<TelefoneViewModel>();
    }
}
