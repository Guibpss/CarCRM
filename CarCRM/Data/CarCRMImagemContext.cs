using CarCRM.Models.Imagens;
using Microsoft.EntityFrameworkCore;

namespace CarCRM.Data
{
    public class CarCRMImagemContext : DbContext
    {
        public CarCRMImagemContext(DbContextOptions<CarCRMImagemContext> options) : base(options) { }

        public DbSet<UsuarioImagem> UsuarioImagem { get; set; }
    }
}
