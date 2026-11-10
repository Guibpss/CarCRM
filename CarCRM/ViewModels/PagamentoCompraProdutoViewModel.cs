using CarCRM.Models;

namespace CarCRM.ViewModels
{
    public class PagamentoCompraProdutoViewModel
    {
        public int Id { get; set; }

        public int CompraProdutoId { get; set; }

        public CompraProdutoViewModel CompraProduto { get; set; }

        public int PagamentoId { get; set; }

        public PagamentoViewModel Pagamento { get; set; }
    }
}
