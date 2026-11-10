using CarCRM.Models;

namespace CarCRM.ViewModels
{
    public class PagamentoVendaProdutoViewModel
    {
        public int Id { get; set; }

        public int VendaProdutoId { get; set; }

        public VendaProdutoViewModel VendaProduto { get; set; }

        public int PagamentoId { get; set; }

        public PagamentoViewModel Pagamento { get; set; }
    }
}
