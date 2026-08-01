using CarCRM.Models;

namespace CarCRM.ViewModels
{
    public class FornecedorViewModel : PessoaViewModel
    {
        public int FornecedorTipoId { get; set; }

        public FornecedorTipoViewModel FornecedorTipo { get; set; }
    }
}
