namespace CarCRM.Models.Imagens
{
    public class UsuarioImagem
    {
        public int Id { get; set; }

        public byte[] Imagem { get; set; }

        public int UsuarioId { get; set; }
    }
}
