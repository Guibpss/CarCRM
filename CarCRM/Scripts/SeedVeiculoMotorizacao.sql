SET NOCOUNT ON;

INSERT INTO VeiculoMotorizacao (Nome)
SELECT t.Nome
FROM (VALUES
    (N'1.0'),
    (N'1.0 Turbo'),
    (N'1.2'),
    (N'1.3'),
    (N'1.3 Turbo'),
    (N'1.4'),
    (N'1.4 Turbo'),
    (N'1.5'),
    (N'1.5 Turbo'),
    (N'1.6'),
    (N'1.6 Turbo'),
    (N'1.8'),
    (N'2.0'),
    (N'2.0 Turbo'),
    (N'2.4'),
    (N'2.7'),
    (N'2.8'),
    (N'3.0'),
    (N'3.2'),
    (N'Elétrico')
) AS t(Nome)
WHERE NOT EXISTS (SELECT 1 FROM VeiculoMotorizacao p WHERE p.Nome = t.Nome);

SELECT Id, Nome FROM VeiculoMotorizacao ORDER BY Nome;