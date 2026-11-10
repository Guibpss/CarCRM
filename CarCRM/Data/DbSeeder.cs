using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace CarCRM.Data
{
    // Roda os scripts de Scripts/*.sql na inicialização da aplicação.
    // Os scripts são idempotentes, então rodar a cada start não duplica nada.
    public static class DbSeeder
    {
        // A ordem importa: scripts que dependem de outros vêm depois (ex.: FornecedorTeste usa FornecedorTipos).

        public static async Task SeedAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CarCRMContext>();

            var pasta = Path.Combine(AppContext.BaseDirectory, "Scripts");
            var conexao = context.Database.GetDbConnection();
            await conexao.OpenAsync();


            foreach (var arquivo in new[]
            {
                    "SeedPerfis.sql",
                    "SeedFornecedorTipos.sql",
                    "SeedProdutoTipos.sql",
                    "SeedMetodosPagamento.sql",
                    "SeedStatusCompras.sql",
                    "SeedStatusPagamentos.sql",
                    "SeedVeiculoCores.sql",
                    "SeedVeiculoTipos.sql",
                    "SeedVeiculoCombustiveis.sql",
                    "SeedVeiculoMotorizacao.sql",
                    "SeedVeiculoTransmissao.sql",
                    "SeedVeiculoMarcasModelos.sql",
                    "SeedVeiculoVersoes.sql",
                    "SeedFornecedorTeste.sql"
                })
            {
                var caminho = Path.Combine(pasta, arquivo);

                // ReadAllText lê em UTF-8, então acentos como "Funcionário" chegam certos no banco.
                using var comando = conexao.CreateCommand();
                comando.CommandText = await File.ReadAllTextAsync(caminho);
                comando.CommandTimeout = 120;
                await comando.ExecuteNonQueryAsync();
            }
            await conexao.CloseAsync();
        }
    }
}
