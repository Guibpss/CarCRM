namespace CarCRM.Models
{
    public class CompraProduto : EntidadeBase
    {
        public DateTime DataCompra { get; set; }

        public float ValorCompra { get; set; }

        public float Desconto { get; set; }

        public int FornecedorId { get; set; }

        public Fornecedor Fornecedor { get; set; }

        public int StatusCompraId { get; set; }

        public StatusCompra StatusCompra { get; set; }
        public int ProdutoId { get; set; }

        public Produto Produto { get; set; }

        public float ValorFinal
        {
            get
            {
                return ValorCompra - Desconto;
            }
        }

        public virtual ICollection<Pagamento>? Pagamentos { get; set; }
    }
}
