namespace CarCRM.Models
{
    public class PagamentoVenda
    {
        public int Id { get; set; }

        public int VendaId { get; set; }

        public Venda Venda { get; set; }

        public int PagamentoId { get; set; }

        public Pagamento Pagamento { get; set; }
    }
}
