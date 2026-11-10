SET NOCOUNT ON;

INSERT INTO VeiculoTipos (Nome)
SELECT t.Nome
FROM (VALUES
    (N'Automóvel'),
    (N'Camioneta'),
    (N'Caminhonete'),
    (N'Utilitário'),
    (N'Motocicleta'),
    (N'Motoneta'),
    (N'Ciclomotor'),
    (N'Triciclo'),
    (N'Quadriciclo'),
    (N'Micro-ônibus'),
    (N'Ônibus'),
    (N'Caminhão'),
    (N'Caminhão-trator'),
    (N'Motor-casa'),
    (N'Reboque'),
    (N'Semirreboque')
) AS t(Nome)
WHERE NOT EXISTS (SELECT 1 FROM VeiculoTipos p WHERE p.Nome = t.Nome);

SELECT Id, Nome FROM VeiculoTipos ORDER BY Nome;