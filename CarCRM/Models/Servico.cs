using Microsoft.EntityFrameworkCore;

namespace CarCRM.Models
{
    public class Servico
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        [Precision(18, 2)]
        public decimal Valor { get; set; }

        public int ServicoTipoId { get; set; }

        public ServicoTipo ServicoTipo { get; set; }

        
    }
}
