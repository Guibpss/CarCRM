-- Cadastra um fornecedor de teste (herança TPT: linha em Pessoas + linha em Fornecedores com o mesmo Id).
-- Idempotente: não duplica se já existir um fornecedor com o mesmo nome.
-- Uso: sqlcmd -S "localhost\SQLEXPRESS" -d CarCRM -E -C -i Scripts\SeedFornecedorTeste.sql

SET NOCOUNT ON;

DECLARE @Nome nvarchar(200) = N'Fornecedor Teste';
DECLARE @Email nvarchar(200) = N'fornecedor.teste@carcrm.local';
DECLARE @Documento nvarchar(50) = N'00.000.000/0001-00';
DECLARE @FornecedorTipoId int = (SELECT TOP 1 Id FROM FornecedorTipos ORDER BY Id);

IF @FornecedorTipoId IS NULL
BEGIN
    RAISERROR(N'Nenhum FornecedorTipo cadastrado. Cadastre um tipo antes.', 16, 1);
    RETURN;
END

IF NOT EXISTS (SELECT 1 FROM Fornecedores f JOIN Pessoas p ON p.Id = f.Id WHERE p.Nome = @Nome)
BEGIN
    BEGIN TRAN;

    INSERT INTO Pessoas (Nome, Email, CriadoEm, Excluido)
    VALUES (@Nome, @Email, GETDATE(), 0);

    DECLARE @Id int = SCOPE_IDENTITY();

    INSERT INTO Fornecedores (Id, FornecedorTipoId, Documento)
    VALUES (@Id, @FornecedorTipoId, @Documento);

    COMMIT;
END

SELECT f.Id, p.Nome, p.Email, f.Documento, f.FornecedorTipoId
FROM Fornecedores f
JOIN Pessoas p ON p.Id = f.Id;
