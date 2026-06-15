namespace CarCRM.Models
{
    public class Veiculo : EntidadeBase
    {
        public int VeiculoCorId {  get; set; }

        public VeiculoCor VeiculoCor { get; set; }

        public int KilometragemAtual { get; set; }
        
        public string Placa { get; set; }//GDR4C69

        public string Combustivel { get; set; }

        public string Motorizacao { get; set; }

        public DateOnly AnoFabricacao { get; set; }

        public DateOnly AnoModelo { get; set; }

        public int VeiculoTipoId { get; set; }
        
        public VeiculoTipo VeiculoTipo { get; set; }

        public int VeiculoMarcaId { get; set; }

        public VeiculoMarca VeiculoMarca { get; set; }
    }
}