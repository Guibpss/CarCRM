namespace CarCRM.Models
{
    public class VeiculoMarca
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        
    }

    public class VeiculoModelo
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int VeiculoMarcaId { get; set; }

        public VeiculoMarca VeiculoMarca { get; set; }

        //public string Combustivel { get; set; }

        //public string Motorizacao { get; set; }

        //public DateTime AnoFabricacao { get; set; }

        //public DateTime AnoModelo { get; set; }
    }

}
