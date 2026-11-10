SET NOCOUNT ON;

INSERT INTO VeiculoCombustivel (Nome)
SELECT t.Nome
FROM (VALUES
    (N'Flex'),
    (N'Gasolina'),
    (N'Etanol'),
    (N'Diesel'),
    (N'GNV'),
    (N'Híbrido'),
    (N'Híbrido Plug-in'),
    (N'Elétrico')
) AS t(Nome)
WHERE NOT EXISTS (SELECT 1 FROM VeiculoCombustivel p WHERE p.Nome = t.Nome);

SELECT Id, Nome FROM VeiculoCombustivel ORDER BY Nome;