using CarCRM.Models;

namespace CarCRM.ViewModels
{
    public class PagamentoCompraViewModel
    {
        public int Id { get; set; }

        public int CompraId { get; set; }

        public CompraViewModel Compra { get; set; }

        public int PagamentoId { get; set; }

        public PagamentoViewModel Pagamento { get; set; }
    }
}
