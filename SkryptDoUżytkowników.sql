USE [master];
GO

-- Utworzenie loginów
-- Utworzenie loginu Admina
CREATE LOGIN [Admin] WITH PASSWORD = 'Adm123!Password@#';
GO

-- Utworzenie loginu Server dla aplikacji
CREATE LOGIN [Server] WITH PASSWORD = 'Serv123!Password@#';
GO

-- Utworzenie loginu Developera
CREATE LOGIN [DevKC] WITH PASSWORD = 'DeveloperPass123!';
GO
-- Utworzenie loginu Developera
CREATE LOGIN [DevKS] WITH PASSWORD = 'DeveloperPass123!';
GO



-- Utworzenie użytkowników
USE [GraveyardDB];
GO

CREATE USER [Admin] FOR LOGIN [Admin];
CREATE USER [Server] FOR LOGIN [Server];
CREATE USER [DevKC] FOR LOGIN [DevKC];
CREATE USER [DevKS] FOR LOGIN [DevKS];


-- Przydzielenie ról kontom
-- Admin jest właścicielem bazy danych, ma pełen dostęp
ALTER ROLE [db_owner] ADD MEMBER [Admin];
GO

-- Server ma dostęp do WRITE, READ, EXECUTE
ALTER ROLE db_datareader ADD MEMBER [Server];
ALTER ROLE db_datawriter ADD MEMBER [Server];
GRANT EXECUTE TO [Server];
GO

-- Server może również tworzyć baże danych, żeby update-database zadziałało
ALTER SERVER ROLE dbcreator ADD MEMBER [Server];
GO


-- Developerzy mają dostęp do READ
ALTER ROLE db_datareader ADD MEMBER [DevKC];
ALTER ROLE db_datareader ADD MEMBER [DevKS];
GO


-- Audyt
USE [master];
GO
-- Utworzenie nowego Audytu
CREATE SERVER AUDIT Audit_Cment
TO FILE (FILEPATH = '/var/opt/mssql/audit/')
WITH (ON_FAILURE = CONTINUE);
GO

-- Uruchomienie audytu
ALTER SERVER AUDIT Audit_Cment WITH (STATE = ON);

-- Specyfikacja audytu
USE [GraveyardDB];
GO

CREATE DATABASE AUDIT SPECIFICATION Audit_Cment_Spec
FOR SERVER AUDIT Audit_Cment
ADD (SELECT ON DATABASE::GraveyardDB BY PUBLIC),
ADD (INSERT ON DATABASE::GraveyardDB BY PUBLIC),
ADD (UPDATE ON DATABASE::GraveyardDB BY PUBLIC),
ADD (DELETE ON DATABASE::GraveyardDB BY PUBLIC)
WITH (STATE = ON);
GO

-- Wypisanie logów audytu
SELECT *
FROM sys.fn_get_audit_file('/var/opt/mssql/audit/*', DEFAULT, DEFAULT);