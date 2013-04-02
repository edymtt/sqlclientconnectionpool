DECLARE @dbname nvarchar(128)
SET @dbname = N'txTry'

IF (EXISTS (SELECT name 
FROM master.dbo.sysdatabases 
WHERE ('[' + name + ']' = @dbname 
OR name = @dbname)))
DROP DATABASE txTry

CREATE DATABASE txTry

CREATE TABLE TxTable1
(
Id int
)

CREATE TABLE TxTable2
(
Id int
)