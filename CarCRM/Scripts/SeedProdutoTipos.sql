-- Cadastra os tipos de produto usados no cadastro de Produtos.
-- Idempotente: só insere os nomes que ainda não existem.
-- Uso: sqlcmd -S "localhost\SQLEXPRESS" -d CarCRM -E -C -i Scripts\SeedProdutoTipos.sql

SET NOCOUNT ON;

INSERT INTO ProdutoTipos (Nome)
SELECT t.Nome
FROM (VALUES
    (N'Acessórios'),
    (N'Baterias'),
    (N'Fluidos e Aditivos'),
    (N'Filtros'),
    (N'Freios'),
    (N'Iluminação'),
    (N'Lubrificantes'),
    (N'Peças de Motor'),
    (N'Pneus'),
    (N'Produtos de Limpeza'),
    (N'Suspensão')
) AS t(Nome)
WHERE NOT EXISTS (SELECT 1 FROM ProdutoTipos p WHERE p.Nome = t.Nome);

SELECT Id, Nome FROM ProdutoTipos ORDER BY Nome;
