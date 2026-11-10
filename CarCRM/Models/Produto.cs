using Microsoft.EntityFrameworkCore;

namespace CarCRM.Models
{
    public class Produto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int Quantidade { get; set; }

        [Precision(18,2)]
        public decimal Valor { get; set; }
        public int ProdutoTipoId { get; set; }

        public ProdutoTipo ProdutoTipo { get; set; }

        public decimal ValorFinal
        {
            get
            {
                return Valor * Quantidade;
            }
        }

    }
}
