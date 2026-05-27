namespace CarCRM.Models
{
    public class Veiculo : EntidadeBase
    {
        public string VeiculoCorId {  get; set; }

        public VeiculoCor VeiculoCor { get; set; }

        public int KilometragemAtual { get; set; }
        
        public string Placa { get; set; }//GDR4C69
        
        public int VeiculoTipoId { get; set; }
        
        public VeiculoTipo VeiculoTipo { get; set; }

        public int VeiculoMarcaId { get; set; }

        public VeiculoMarca VeiculoMarca { get; set; }
    }
}