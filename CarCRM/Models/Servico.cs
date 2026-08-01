namespace CarCRM.Models
{
    public class Servico
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public float Valor { get; set; }

        public int ServicoTipoId { get; set; }

        public ServicoTipo ServicoTipo { get; set; }

        public int ClienteId { get; set; }

        public Cliente Cliente { get; set; }
    }
}
