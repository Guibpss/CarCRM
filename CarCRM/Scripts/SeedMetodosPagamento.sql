SET NOCOUNT ON;

INSERT INTO MetodosPagamento (Nome, Ativo)
SELECT t.Nome,t.Ativo
FROM (VALUES
    (N'PIX', 1),
    (N'Boleto', 1),
    (N'Dinheiro', 1),
    (N'Crédito',1),
    (N'Débito',1),
    (N'Voucher',1),
    (N'Cheque',1)
) AS t(Nome, Ativo)
WHERE NOT EXISTS (SELECT 1 FROM MetodosPagamento p WHERE p.Nome = t.Nome);

SELECT Id, Nome, Ativo FROM MetodosPagamento ORDER BY Nome;