SET NOCOUNT ON;

INSERT INTO StatusPagamentos (Nome, Ativo)
SELECT t.Nome,t.Ativo
FROM (VALUES
    (N'Pago', 1),
    (N'Parcialmente pago', 1),
    (N'Pendente', 1),
    (N'Cancelado',1),
    (N'Agendado',1),
    (N'Estornado',1),
    (N'Recebido',1),
    (N'Em análise',1),
    (N'Atrasado',1)
) AS t(Nome, Ativo)
WHERE NOT EXISTS (SELECT 1 FROM StatusPagamentos p WHERE p.Nome = t.Nome);

SELECT Id, Nome, Ativo FROM StatusPagamentos ORDER BY Nome;