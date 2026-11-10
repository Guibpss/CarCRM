SET NOCOUNT ON;

INSERT INTO VeiculoCor (Nome)
SELECT t.Nome
FROM (VALUES
    (N'Amarela'),
     (N'Azul'),
    (N'Bege'),
    (N'Branca'),
    (N'Cinza'),
    (N'Dourada'),
    (N'Fantasia'),
    (N'Grená'),
    (N'Laranja'),
    (N'Marrom'),
    (N'Prata'),
    (N'Preta'),
    (N'Rosa'),
    (N'Roxa'),
    (N'Verde'),
    (N'Vermelha')
) AS t(Nome)
WHERE NOT EXISTS (SELECT 1 FROM VeiculoCor p WHERE p.Nome = t.Nome);

SELECT Id, Nome FROM VeiculoCor ORDER BY Nome;