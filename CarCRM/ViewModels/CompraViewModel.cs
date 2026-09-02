using CarCRM.Models;

namespace CarCRM.ViewModels
{
    public class CompraViewModel : ViewModelBase
    {
        public DateTime DataCompra { get; set; }

        public float ValorCompra { get; set; }

        public float Desconto { get; set; }

        public int VendedorId { get; set; }

        public UsuarioViewModel? Vendedor { get; set; }

        public int VeiculoId { get; set; }

        public VeiculoViewModel? Veiculo { get; set; }

        public int StatusCompraId { get; set; }

        public StatusCompraViewModel? StatusCompra { get; set; }

        public float ValorFinal
        {
            get
            {
                return ValorCompra - Desconto;
            }
        }
    }
}
