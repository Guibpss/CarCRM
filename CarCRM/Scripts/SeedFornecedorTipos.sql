-- Cadastra os tipos de fornecedor (pré-requisito do SeedFornecedorTeste.sql).
-- Idempotente: só insere os nomes que ainda não existem.
-- Uso: sqlcmd -S "localhost\SQLEXPRESS" -d CarCRM -E -C -i Scripts\SeedFornecedorTipos.sql

SET NOCOUNT ON;

INSERT INTO FornecedorTipos (Nome)
SELECT t.Nome
FROM (VALUES
    (N'Autopeças'),
    (N'Concessionária'),
    (N'Distribuidora'),
    (N'Fabricante'),
    (N'Leilão'),
    (N'Pessoa Física'),
    (N'Prestador de Serviço'),
    (N'Revenda de Veículos')
) AS t(Nome)
WHERE NOT EXISTS (SELECT 1 FROM FornecedorTipos f WHERE f.Nome = t.Nome);

SELECT Id, Nome FROM FornecedorTipos ORDER BY Id;
