namespace CarCRM.Models
{
    public class FuncionarioCargo : EntidadeBase
    {
        public int FuncionarioId { get; set; }

        public Funcionario Funcionario { get; set; }

        public int CargoId { get; set; }

        public Cargo Cargo { get; set; }

        public bool Vigente { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataIFim { get; set; }
    }
}