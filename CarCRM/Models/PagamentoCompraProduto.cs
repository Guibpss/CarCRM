namespace CarCRM.Models
{
    public class PagamentoCompraProduto
    {
        public int Id { get; set; }

        public int CompraProdutoId { get; set; }

        public CompraProduto CompraProduto { get; set; }

        public int PagamentoId { get; set; }

        public Pagamento Pagamento { get; set; }
    }
}
