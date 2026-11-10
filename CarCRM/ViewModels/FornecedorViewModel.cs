using CarCRM.Models;

namespace CarCRM.ViewModels
{
    public class FornecedorViewModel : PessoaViewModel
    {
        public string Documento { get; set; }
        public int FornecedorTipoId { get; set; }

        public FornecedorTipoViewModel FornecedorTipo { get; set; }
    }
}
