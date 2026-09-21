using Microsoft.EntityFrameworkCore;

namespace CarCRM.Models
{
    public class Produto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        [Precision(18,2)]
        public decimal Valor { get; set; }

        public int ClienteId { get; set; }

        public Cliente Cliente { get; set; }
    }
}
