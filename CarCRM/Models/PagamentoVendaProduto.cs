namespace CarCRM.Models
{
    public class PagamentoVendaProduto
    {
        public int Id { get; set; }

        public int VendaProdutoId { get; set; }

        public VendaProduto VendaProduto { get; set; }

        public int PagamentoId { get; set; }

        public Pagamento Pagamento { get; set; }
    }
}
