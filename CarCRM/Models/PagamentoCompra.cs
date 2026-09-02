namespace CarCRM.Models
{
    public class PagamentoCompra
    {
        public int Id { get; set; }

        public int CompraId { get; set; }

        public Compra Compra { get; set; }

        public int PagamentoId { get; set; }

        public Pagamento Pagamento { get; set; }
    }
}
