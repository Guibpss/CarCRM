SET NOCOUNT ON;

INSERT INTO VeiculoTransmissao (Nome)
SELECT t.Nome
FROM (VALUES
    (N'Manual'),
    (N'Automática'),
    (N'Automatizada'),
    (N'CVT'),
    (N'Dupla Embreagem')
) AS t(Nome)
WHERE NOT EXISTS (SELECT 1 FROM VeiculoTransmissao p WHERE p.Nome = t.Nome);

SELECT Id, Nome FROM VeiculoTransmissao ORDER BY Nome;