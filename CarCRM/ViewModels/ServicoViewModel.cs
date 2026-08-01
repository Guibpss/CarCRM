using CarCRM.Models;

namespace CarCRM.ViewModels
{
    public class ServicoViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public float Valor { get; set; }

        public int ServicoTipoId { get; set; }

        public ServicoTipoViewModel ServicoTipo { get; set; }

        public int ClienteId { get; set; }

        public ClienteViewModel Cliente { get; set; }
    }
}
