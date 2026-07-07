using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace CarCRM.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string ConfirmaSenha { get; set; }
        public DateTime CriadoEm { get; set; }
        public bool Ativo { get; set; }
        public int PerfilId { get; set; }
        public Perfil? Perfil { get; set; }
        public int PessoaId { get; set; }
        public PessoaFisica Pessoa { get; set; } = new PessoaFisica();

    }
}
