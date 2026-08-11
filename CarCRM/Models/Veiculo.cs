namespace CarCRM.Models
{
    public class Veiculo : EntidadeBase
    {
        public int VeiculoCorId {  get; set; }

        public VeiculoCor VeiculoCor { get; set; }

        public int KilometragemAtual { get; set; }
        
        public string Placa { get; set; }//GDR4C69

        public string Renavam { get; set; }

        public int VeiculoCombustivelId { get; set; }

        public VeiculoCombustivel VeiculoCombustivel { get; set; }

        public int VeiculoMotorizacaoId { get; set; }

        public VeiculoMotorizacao VeiculoMotorizacao { get; set; }

        public int? VeiculoTransmissaoId { get; set; }

        public VeiculoTransmissao? VeiculoTransmissao { get; set; }

        public int AnoFabricacao { get; set; }

        public int AnoModelo { get; set; }

        public int VeiculoTipoId { get; set; }
        
        public VeiculoTipo VeiculoTipo { get; set; }

        public int VeiculoMarcaId { get; set; }

        public VeiculoMarca VeiculoMarca { get; set; }
        public int VeiculoModeloId { get; set; }

        public VeiculoModelo VeiculoModelo { get; set; }
        public int VeiculoVersaoId { get; set; }
        public VeiculoVersao VeiculoVersao { get; set; }
    }
}