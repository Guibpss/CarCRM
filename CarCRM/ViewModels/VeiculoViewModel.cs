using CarCRM.Models;
using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class VeiculoViewModel: ViewModelBase
    {
        public int VeiculoCorId { get; set; }

        public VeiculoCorViewModel? VeiculoCor { get; set; }

        [Required(ErrorMessage = "O campo Quilometragem Atual é obrigatório")]
        public int KilometragemAtual { get; set; }

        [Required(ErrorMessage = "O campo Placa é obrigatório")]
        public string Placa { get; set; }//GDR4C69

        [Required(ErrorMessage = "O campo Renavam é obrigatório")]
        public string Renavam { get; set; }

        public int VeiculoCombustivelId { get; set; }

        public VeiculoCombustivelViewModel? VeiculoCombustivel { get; set; }

        public int VeiculoMotorizacaoId { get; set; }

        public VeiculoMotorizacaoViewModel? VeiculoMotorizacao { get; set; }

        public int? VeiculoTransmissaoId { get; set; }

        public VeiculoTransmissaoViewModel? VeiculoTransmissao { get; set; }

        [Required(ErrorMessage = "O campo Ano de Fabricação é obrigatório")]
        public int AnoFabricacao { get; set; }

        [Required(ErrorMessage = "O campo Ano do Modelo é obrigatório")]
        public int AnoModelo { get; set; }

        public int VeiculoTipoId { get; set; }

        public VeiculoTipoViewModel? VeiculoTipo { get; set; }

        public int VeiculoMarcaId { get; set; }

        public VeiculoMarcaViewModel? VeiculoMarca { get; set; }
        public int VeiculoModeloId { get; set; }

        public VeiculoModeloViewModel? VeiculoModelo { get; set; }
    }
}
