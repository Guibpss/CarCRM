using CarCRM.Models;

namespace CarCRM.ViewModels
{
    public class VeiculoViewModel: ViewModelBase
    {
        public int VeiculoCorId { get; set; }

        public VeiculoCorViewModel VeiculoCor { get; set; }

        public int KilometragemAtual { get; set; }

        public string Placa { get; set; }//GDR4C69

        public int VeiculoCombustivelId { get; set; }

        public VeiculoCombustivelViewModel VeiculoCombustivel { get; set; }

        public int VeiculoMotorizacaoId { get; set; }

        public VeiculoMotorizacaoViewModel VeiculoMotorizacao { get; set; }

        public int AnoFabricacao { get; set; }

        public int AnoModelo { get; set; }

        public int VeiculoTipoId { get; set; }

        public VeiculoTipoViewModel VeiculoTipo { get; set; }

        public int VeiculoMarcaId { get; set; }

        public VeiculoMarcaViewModel VeiculoMarca { get; set; }
        public int VeiculoModeloId { get; set; }

        public VeiculoModeloViewModel VeiculoModelo { get; set; }
    }
}
