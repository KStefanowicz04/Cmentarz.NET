USE [GraveyardDB];
GO

-- Schema
CREATE SCHEMA schema_cment
GO

-- Procedura zapisująca info o grobach do tabeli z podsumowaniem; należy do Schema
-- Tabela z podsumowanium
CREATE TABLE GraveSummary
(
    GraveId INT PRIMARY KEY,
    DeceasedId INT,
    LastUpdated DATETIME DEFAULT GETDATE()
);
GO
-- Procedura
CREATE PROCEDURE FillGraveSummary
AS
BEGIN
    DECLARE @GraveId INT;
    DECLARE @DeceasedId INT;

    -- Kursor po grobach
    DECLARE grave_cursor CURSOR FOR
        SELECT Id, DeceasedId
        FROM Graves;

    -- Zapisanie danych z kursora
    OPEN grave_cursor;
    FETCH NEXT FROM grave_cursor INTO @GraveId, @DeceasedId;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Wstawienie do tabeli GraveSummary
        MERGE GraveSummary AS target
        USING (SELECT @GraveId AS GraveId, @DeceasedId AS DeceasedId) AS src
        ON target.GraveId = src.GraveId
        WHEN MATCHED THEN
            UPDATE SET 
                DeceasedId = src.DeceasedId,
                LastUpdated = GETDATE()
        WHEN NOT MATCHED THEN
            INSERT (GraveId, DeceasedId)
            VALUES (src.GraveId, src.DeceasedId);

        FETCH NEXT FROM grave_cursor INTO @GraveId, @DeceasedId;
    END

    CLOSE grave_cursor;
    DEALLOCATE grave_cursor;
END;
GO


-- Funkcja zwracająca sumę wszystkich płatności danego właściciela działki; należy do Schema 'schema_cment'
CREATE FUNCTION schema_cment.GetOwnerPaymentsSum(@OwnerId INT)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @Total DECIMAL(10,2);

    SELECT @Total = SUM(Price)
    FROM Payments
    WHERE PlotOwnerId = @OwnerId;

    RETURN ISNULL(@Total, 0);
END;
GO



-- Trigger który zapisuje informacje o zalogowaniu się do bazy danych.
-- Tabela-Audyt
CREATE TABLE LoginTriggerAudit
(
    AuditId INT IDENTITY PRIMARY KEY,
    LoginName NVARCHAR(200),
    LoginTime DATETIME DEFAULT GETDATE(),
    HostName NVARCHAR(200),
    AppName NVARCHAR(200)
);
-- Trigger
USE [master];
GO

CREATE TRIGGER LoginTrigger
ON ALL SERVER
FOR LOGON
AS
BEGIN
    INSERT INTO GraveyardDB.dbo.LoginTriggerAudit(LoginName, HostName, AppName)
    SELECT
        ORIGINAL_LOGIN(),
        HOST_NAME(),
        APP_NAME();
END;
GO



-- Indeksy na najczęściej używanych tabelach
USE [GraveyardDB];
GO

CREATE INDEX ind_deceased ON Deceaseds(Id);
CREATE INDEX ind_graves ON Graves(Id);
CREATE INDEX ind_gravestones ON Gravestones(Id);
CREATE INDEX ind_users ON Users(UserId);
CREATE INDEX ind_plots ON Plots(Id);
CREATE INDEX ind_parishes ON Parishes(Id);
