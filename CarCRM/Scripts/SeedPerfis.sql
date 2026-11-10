-- Cadastra os perfis de acesso (roles do ASP.NET Identity) em AspNetRoles com Ids fixos ('1'..'6').
-- Idempotente: apaga qualquer perfil fora da lista (ex.: os antigos com GUID) e insere os que faltam.
-- Uso: sqlcmd -S "localhost\SQLEXPRESS" -d CarCRM -E -C -f 65001 -i Scripts\SeedPerfis.sql

SET NOCOUNT ON;
-- Obrigatório: a AspNetRoles tem índice filtrado em NormalizedName, e o sqlcmd vem com essa opção desligada.
SET QUOTED_IDENTIFIER ON;

DECLARE @Perfis TABLE (Id nvarchar(450) PRIMARY KEY, Nome nvarchar(256) NOT NULL);
INSERT INTO @Perfis (Id, Nome) VALUES
    (N'1', N'Administrador'),
    (N'2', N'Funcionário'),
    (N'3', N'Vendedor'),
    (N'4', N'Cliente'),
    (N'5', N'Financeiro'),
    (N'6', N'Suporte');

DELETE FROM AspNetRoles
WHERE Id NOT IN (SELECT Id FROM @Perfis);

INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp)
SELECT p.Id, p.Nome, UPPER(p.Nome), CONVERT(nvarchar(450), NEWID())
FROM @Perfis p
WHERE NOT EXISTS (SELECT 1 FROM AspNetRoles r WHERE r.Id = p.Id);

SELECT Id, Name, NormalizedName FROM AspNetRoles ORDER BY Id;
