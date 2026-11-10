SET NOCOUNT ON;

INSERT INTO StatusCompras (Nome, Ativo)
SELECT t.Nome,t.Ativo
FROM (VALUES
    (N'Em negociação', 1),
    (N'Concluída', 1),
    (N'Cancelada', 1),
    (N'Aprovada',1),
    (N'Aguardando aprovação',1),
    (N'Aguardando entrega',1),
    (N'Pedido realizado',1),
    (N'Recusada',1),
    (N'Suspensa',1)
) AS t(Nome, Ativo)
WHERE NOT EXISTS (SELECT 1 FROM StatusCompras p WHERE p.Nome = t.Nome);

SELECT Id, Nome, Ativo FROM StatusCompras ORDER BY Nome;