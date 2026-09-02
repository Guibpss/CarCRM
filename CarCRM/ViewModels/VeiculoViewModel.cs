using CarCRM.Models;
using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class VeiculoViewModel: ViewModelBase
    {
        [Range(1, int.MaxValue, ErrorMessage = "Selecione a Cor")]
        public int VeiculoCorId { get; set; }

        public VeiculoCorViewModel? VeiculoCor { get; set; }

        [Required(ErrorMessage = "O campo Quilometragem Atual é obrigatório")]
        [Range(0, 9_999_999, ErrorMessage = "A Quilometragem deve estar entre {1} e {2}")]
        public int KilometragemAtual { get; set; }

        [Required(ErrorMessage = "O campo Placa é obrigatório")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "A Placa deve ter {1} caracteres")]
        [RegularExpression(@"^[A-Z]{3}[0-9][A-Z0-9][0-9]{2}$", ErrorMessage = "Placa inválida (ex.: ABC1234 ou ABC1D23)")]
        public string Placa { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Renavam é obrigatório")]
        [StringLength(11, MinimumLength = 9, ErrorMessage = "O Renavam deve ter entre {2} e {1} dígitos")]
        [RegularExpression(@"^\d{9,11}$", ErrorMessage = "O Renavam deve conter apenas números e até 11 caracteres")]
        public string Renavam { get; set; } = string.Empty;
        [Range(1, int.MaxValue, ErrorMessage = "Selecione o Combustível")]
        public int VeiculoCombustivelId { get; set; }

        public VeiculoCombustivelViewModel? VeiculoCombustivel { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Selecione a Motorização")]
        public int VeiculoMotorizacaoId { get; set; }
        
        public VeiculoMotorizacaoViewModel? VeiculoMotorizacao { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Selecione a Transmissão")]
        public int? VeiculoTransmissaoId { get; set; }

        public VeiculoTransmissaoViewModel? VeiculoTransmissao { get; set; }

        [Required(ErrorMessage = "O campo Ano de Fabricação é obrigatório")]
        [Range(1900, 2100, ErrorMessage = "O Ano de Fabricação deve estar entre {1} e {2}")]
        public int AnoFabricacao { get; set; }

        [Required(ErrorMessage = "O campo Ano do Modelo é obrigatório")]
        [Range(1900, 2100, ErrorMessage = "O Ano de Fabricação deve estar entre {1} e {2}")]
        public int AnoModelo { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Selecione o Tipo do Veículo")]
        public int VeiculoTipoId { get; set; }

        public VeiculoTipoViewModel? VeiculoTipo { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Selecione a Marca")]
        public int VeiculoMarcaId { get; set; }

        public VeiculoMarcaViewModel? VeiculoMarca { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Selecione o Modelo")]
        public int VeiculoModeloId { get; set; }

        public VeiculoModeloViewModel? VeiculoModelo { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Selecione a Versão")]
        public int VeiculoVersaoId { get; set; }
        public VeiculoVersaoViewModel? VeiculoVersao { get; set; }
        public CompraViewModel? Compra { get; set; } = new();
        public VendaViewModel? Venda { get; set; } = new();
        public List<PagamentoViewModel>? PagamentosCompra { get; set; } = new();
        public List<PagamentoViewModel>? PagamentosVenda { get; set; } = new();
    }
}
