using CarCRM.Models;

namespace CarCRM.ViewModels
{
    public class PagamentoVendaViewModel
    {
        public int Id { get; set; }

        public int VendaId { get; set; }

        public VendaViewModel Venda { get; set; }

        public int PagamentoId { get; set; }

        public PagamentoViewModel Pagamento { get; set; }
    }
}
