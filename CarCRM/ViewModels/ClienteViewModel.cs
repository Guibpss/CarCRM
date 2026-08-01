using CarCRM.Models;
using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        public DateTime CriadoEm { get; set; }
        public bool Excluido { get; set; }
        public int PessoaId { get; set; }
        public PessoaViewModel? Pessoa { get; set; }
        public string? CPF { get; set; }
        public string? RG { get; set; }
        public DateTime DataNascimento { get; set; }
        public string? RazaoSocial { get; set; }
        public string? NomeFantasia { get; set; }
        public string? NomeInterno { get; set; }
        public string? CNPJ { get; set; }
    }
}
