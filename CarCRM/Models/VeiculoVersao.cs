namespace CarCRM.Models
{
    public class VeiculoVersao
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int VeiculoModeloId { get; set; }

        public VeiculoModelo VeiculoModelo { get; set; }
    }
}
