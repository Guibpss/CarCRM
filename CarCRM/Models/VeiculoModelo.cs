namespace CarCRM.Models
{
    public class VeiculoModelo
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int VeiculoMarcaId { get; set; }

        public VeiculoMarca VeiculoMarca { get; set; }
    }
}
