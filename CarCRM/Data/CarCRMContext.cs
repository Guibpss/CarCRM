using Microsoft.EntityFrameworkCore;
using CarCRM.Models;

namespace CarCRM.Data
{
    public class CarCRMContext : DbContext
    {
        public DbSet<CarCRM.Models.VeiculoModelo> VeiculoModelo { get; set; } = default!;
        public DbSet<CarCRM.Models.VeiculoCor> VeiculoCor { get; set; } = default!;
        public DbSet<CarCRM.Models.FuncionarioCargo> FuncionarioCargo { get; set; } = default!;
        public CarCRMContext(DbContextOptions<CarCRMContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Heranca de Pessoa usando TPT (uma tabela por tipo).
            modelBuilder.Entity<Pessoa>().UseTptMappingStrategy();
            modelBuilder.Entity<Pessoa>().ToTable("Pessoas");
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<Fornecedor>().ToTable("Fornecedores");
            modelBuilder.Entity<Funcionario>().ToTable("Funcionarios");
            modelBuilder.Entity<PessoaFisica>().ToTable("PessoasFisica");
            modelBuilder.Entity<PessoaJuridica>().ToTable("PessoasJuridica");

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())
                .Where(fk => fk.DeleteBehavior == DeleteBehavior.Cascade))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Comissao> Comissoes { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<FornecedorTipo> FornecedorTipos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<MetodoPagamento> MetodosPagamento { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Perfil> Perfis {  get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<PessoaFisica> PessoasFisica { get; set; }
        public DbSet<PessoaJuridica> PessoasJuridica { get; set; }
        public DbSet<StatusComissao> StatusComissoes { get; set; }
        public DbSet<StatusEstoque> StatusEstoques { get; set; }
        public DbSet<StatusPagamento> StatusPagamentos { get; set; }
        public DbSet<StatusVenda> StatusVendas { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<TelefoneTipo> TelefonesTipo { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<VeiculoMarca> veiculoMarcas { get; set; }
        public DbSet<VeiculoTipo> VeiculoTipos { get; set; }
        public DbSet<Venda> Vendas { get; set; }

    }
}
