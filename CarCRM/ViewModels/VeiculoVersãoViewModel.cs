using CarCRM.Models;
using System.ComponentModel.DataAnnotations;
namespace CarCRM.ViewModels
{
    public class VeiculoVersãoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }

        public int VeiculoModeloId { get; set; }

        public VeiculoModeloViewModel VeiculoModelo { get; set; }
    }
}
