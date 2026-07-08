using CarCRM.Models;

namespace CarCRM.ViewModels
{
    public class VeiculoModeloViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        public int VeiculoMarcaId { get; set; }

        public VeiculoMarcaViewModel VeiculoMarca { get; set; }
    }
}
