using CarCRM.Models;
using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(120, MinimumLength = 2, ErrorMessage = "O Nome deve ter entre {2} e {1} caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Valor é obrigatório")]
        [Range(0.01, 99_999_999, ErrorMessage = "O Valor deve estar entre {1} e {2}")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O campo Quantidade é obrigatório")]
        public int Quantidade { get; set; }

        //[Range(1, int.MaxValue, ErrorMessage = "Selecione o Cliente")]
        //public int ClienteId { get; set; }

        //public ClienteViewModel? Cliente { get; set; }
        public decimal ValorFinal
        {
            get
            {
                return Valor * Quantidade;
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Selecione o tipo do Produto")]
        public int ProdutoTipoId { get; set; }

        public ProdutoTipoViewModel? ProdutoTipo { get; set; }

        public CompraProdutoViewModel? CompraProduto { get; set; } 

        public VendaProdutoViewModel? VendaProduto { get; set;} 

        public List<PagamentoViewModel>? PagamentosProdutoCompra { get; set; }

        public List<PagamentoViewModel>? PagamentosProdutoVenda { get; set; }
    }
}
