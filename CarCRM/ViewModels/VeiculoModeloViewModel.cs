using CarCRM.Models;
using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class VeiculoModeloViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }
        public int VeiculoMarcaId { get; set; }

        public VeiculoMarcaViewModel VeiculoMarca { get; set; }
    }
}
