SET NOCOUNT ON;

DECLARE @Modelos TABLE (Marca nvarchar(100) NOT NULL, Modelo nvarchar(100) NOT NULL);
INSERT INTO @Modelos (Marca, Modelo) VALUES
    (N'Audi', N'A3'),
    (N'Audi', N'Q3'),
    (N'BMW', N'320i'),
    (N'BMW', N'X1'),
    (N'Chery', N'Arrizo 6'),
    (N'Chery', N'Tiggo 5x'),
    (N'Citroen', N'C3'),
    (N'Citroen', N'C4 Cactus'),
    (N'Fiat', N'Palio'),
    (N'Fiat', N'Siena'),
    (N'Ford', N'Fiesta'),
    (N'Ford', N'Ka'),
    (N'GM/Chevrolet', N'Onix'),
    (N'GM/Chevrolet', N'Cruze'),
    (N'GM/Chevrolet', N'Agile'),
    (N'Honda', N'Civic'),
    (N'Honda', N'Fit'),
    (N'Hyundai', N'Creta'),
    (N'Hyundai', N'HB20'),
    (N'Jeep', N'Compass'),
    (N'Jeep', N'Renegade'),
    (N'Kia', N'Cerato'),
    (N'Kia', N'Sportage'),
    (N'Land Rover', N'Discovery Sport'),
    (N'Land Rover', N'Range Rover Evoque'),
    (N'Mercedes-Benz', N'Classe A'),
    (N'Mercedes-Benz', N'GLA'),
    (N'Mitsubishi', N'Lancer'),
    (N'Mitsubishi', N'Pajero'),
    (N'Nissan', N'Kicks'),
    (N'Nissan', N'Versa'),
    (N'Peugeot', N'2008'),
    (N'Peugeot', N'208'),
    (N'Renault', N'Duster'),
    (N'Renault', N'Sandero'),
    (N'Toyota', N'Corolla'),
    (N'Toyota', N'Hilux'),
    (N'Volvo', N'XC40'),
    (N'Volvo', N'XC60'),
    (N'VW/Volkswagen', N'Fox'),
    (N'VW/Volkswagen', N'Gol'),
    (N'VW/Volkswagen', N'Golf'),
    (N'VW/Volkswagen', N'Parati'),
    (N'VW/Volkswagen', N'Tiguan'),
    (N'VW/Volkswagen', N'Voyage');

-- 1) Marcas que ainda não existem
INSERT INTO veiculoMarcas (Nome)
SELECT DISTINCT m.Marca
FROM @Modelos m
WHERE NOT EXISTS (SELECT 1 FROM veiculoMarcas ma WHERE ma.Nome = m.Marca);

-- 2) Modelos que ainda não existem, ligados à marca pelo nome
INSERT INTO VeiculoModelo (Nome, VeiculoMarcaId)
SELECT m.Modelo, ma.Id
FROM @Modelos m
JOIN veiculoMarcas ma ON ma.Nome = m.Marca
WHERE NOT EXISTS (
    SELECT 1 FROM VeiculoModelo mo
    WHERE mo.Nome = m.Modelo AND mo.VeiculoMarcaId = ma.Id
);

SELECT ma.Nome AS Marca, mo.Nome AS Modelo
FROM VeiculoModelo mo
JOIN veiculoMarcas ma ON ma.Id = mo.VeiculoMarcaId
ORDER BY ma.Nome, mo.Nome;