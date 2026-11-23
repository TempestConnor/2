-- Run manually once on SQL Server. Existing databases are not overwritten.
USE master;
IF DB_ID(N'PRN222_PE01_2') IS NOT NULL
    THROW 50001, 'Database already exists.', 1;
EXEC(N'CREATE DATABASE [PRN222_PE01_2]');
EXEC(N'USE [PRN222_PE01_2]; SET XACT_ABORT ON; BEGIN TRANSACTION;
CREATE TABLE dbo.LookupGroup(GroupId int IDENTITY(1,1) PRIMARY KEY, Name nvarchar(50) NOT NULL);
CREATE TABLE dbo.Item(
 ItemId int IDENTITY(1,1) PRIMARY KEY,
 Name nvarchar(50) NOT NULL,
 GroupId int NOT NULL REFERENCES dbo.LookupGroup(GroupId),
 Amount decimal(10,2) NOT NULL CHECK(Amount BETWEEN 0 AND 800),
 EventDate date NOT NULL,
 Status nvarchar(20) NOT NULL CHECK(Status IN (N''Pending'',N''Approved'',N''Rejected''))
);

SET IDENTITY_INSERT dbo.LookupGroup ON;
INSERT dbo.LookupGroup(GroupId,Name) VALUES (1,N''DSLR''),(2,N''Mirrorless''),(3,N''Video'');
SET IDENTITY_INSERT dbo.LookupGroup OFF;
SET IDENTITY_INSERT dbo.Item ON;
INSERT dbo.Item(ItemId,Name,GroupId,Amount,EventDate,Status) VALUES
(1,N''EOS 90D'',1,11.50,''20260401'',N''Pending''),
(2,N''Z fc'',2,24.00,''20260402'',N''Approved''),
(3,N''D7500'',1,36.25,''20260403'',N''Pending''),
(4,N''X-T30'',2,24.00,''20260404'',N''Rejected''),
(5,N''A6400'',2,0.00,''20260405'',N''Pending'');
SET IDENTITY_INSERT dbo.Item OFF;

COMMIT;');
