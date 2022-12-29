-- CREATE TABLE
create table RegistrationTimeline
(
    Id BIT PRIMARY KEY,
    StartDate DATE,
    EndDate DATE,
    Finished bit
);
GO
-- DROP TABLE
DROP TABLE IF EXISTS [dbo].[RegistrationTimeline]
GO

-- INSERT INITIAL VALUE
INSERT INTO [dbo].[RegistrationTimeline]
    (Id,StartDate,EndDate,Finished)
    VALUES
    (1,'01-01-2001','01-01-2001',0)
GO

create procedure SetRegistrationTimeline @StartDate DATE, @EndDate DATE
AS
    BEGIN
        UPDATE [dbo].[RegistrationTimeline] 
        SET StartDate = @StartDate, EndDate = @EndDate
        WHERE Id = 1
    END
GO

create procedure FinishRegistrationTimeline
AS 
    BEGIN
        UPDATE [dbo].[RegistrationTimeline] 
        SET Finished = 1
        WHERE Id = 1
    END
GO

create procedure ResetRegistrationTimeline
AS 
    BEGIN
        UPDATE [dbo].[RegistrationTimeline] 
        SET Finished = 0
        WHERE Id = 1
    END
GO
-- CHECK DATA
SELECT * FROM [dbo].[RegistrationTimeline]

-- TEST PROCEDURE
EXEC SetRegistrationTimeline @StartDate = '20221213' ,@EndDate = '20221215'

EXEC FinishRegistrationTimeline

EXEC ResetRegistrationTimeline