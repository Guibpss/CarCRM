using CarCRM.Models;
using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class VendaViewModel : ViewModelBase
    {
        [Required(ErrorMessage = "O campo Data da Venda é obrigatório")]
        [DataType(DataType.Date)]
        public DateTime DataVenda { get; set; }

        [Required(ErrorMessage = "O campo Valor da Venda é obrigatório")]
        [Range(0.01, 99_999_999, ErrorMessage = "O Valor da Venda deve estar entre {1} e {2}")]
        public float ValorVenda { get; set; }

        [Required(ErrorMessage = "O campo Desconto é obrigatório")]
        [Range(0, 99_999_999, ErrorMessage = "O Desconto deve estar entre {1} e {2}")]
        public float Desconto { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecione o Cliente")]
        public int ClienteId { get; set; }

        public ClienteViewModel? Cliente { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecione o Veiculo")]
        public int VeiculoId { get; set; }

        public VeiculoViewModel Veiculo { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecione o Status da Venda")]
        public int StatusVendaId { get; set; }

        public StatusVendaViewModel? StatusVenda { get; set; }

        public float ValorFinal
        {
            get
            {
                return ValorVenda - Desconto;
            }
        }
    }
}
