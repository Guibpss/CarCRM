namespace CarCRM.Models
{
    public class VendaProduto : EntidadeBase
    {
        public DateTime DataVenda { get; set; }

        public float ValorVenda { get; set; }

        public float Desconto { get; set; }

        public int ClienteId { get; set; }

        public Cliente Cliente { get; set; }

        public int StatusVendaId { get; set; }

        public StatusVenda StatusVenda { get; set; }
        public int ProdutoId { get; set; }

        public Produto Produto { get; set; }

        public float ValorFinal
        {
            get
            {
                return ValorVenda - Desconto;
            }
        }

        public virtual ICollection<Pagamento>? Pagamentos { get; set; }
    }
}
