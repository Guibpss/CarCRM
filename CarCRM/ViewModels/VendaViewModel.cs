using CarCRM.Models;
using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class VendaViewModel : ViewModelBase
    {
        [Required(ErrorMessage = "O campo Data da Venda é obrigatório")]
        public DateTime DataVenda { get; set; }

        [Required(ErrorMessage = "O campo Valor da Venda é obrigatório")]
        public float ValorVenda { get; set; }

        [Required(ErrorMessage = "O campo Desconto é obrigatório")]
        public float Desconto { get; set; }

        public int ClienteId { get; set; }

        public ClienteViewModel? Cliente { get; set; }

        public int VendedorId { get; set; }

        public UsuarioViewModel? Vendedor { get; set; }

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
