using CarCRM.Models;

namespace CarCRM.ViewModels
{
    public class CompraProdutoViewModel : ViewModelBase
    {
        public DateTime DataCompra { get; set; }

        public float ValorCompra { get; set; }

        public float Desconto { get; set; }

        public int FornecedorId { get; set; }

        public FornecedorViewModel Fornecedor { get; set; }

        public int StatusCompraId { get; set; }

        public StatusCompraViewModel StatusCompra { get; set; }
        public int ProdutoId { get; set; }

        public float ValorFinal
        {
            get
            {
                return ValorCompra - Desconto;
            }
        }
    }
}
