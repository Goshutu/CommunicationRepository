﻿/*

DROP TABLE EMPLOYEES
DROP TABLE COMPANIES

CREATE TABLE [dbo].[COMPANIES]
(
	[ID] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[NAME] CHAR(64) NOT NULL
)
GO

CREATE TABLE [dbo].[EMPLOYEES]
(
	[ID] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[NAME] CHAR(64) NOT NULL,
	[COMPANY_ID] INT NOT NULL,
	[EXPERIENCE] CHAR NOT NULL, 
    [STARTING_DATE] DATETIME NOT NULL, 
    [SALARY] FLOAT NOT NULL, 
    [VACATION_DAYS] INT NOT NULL, 
    CONSTRAINT [FK_EMPLOYEES_ToCOMPANIES] FOREIGN KEY ([COMPANY_ID]) REFERENCES [COMPANIES]([ID]),
)
GO

INSERT INTO COMPANIES
VALUES('Facebook')
GO

INSERT INTO EMPLOYEES
VALUES('Mark Zuckerberg', 1, 3, GETDATE(), 1000000, 1)
GO

INSERT INTO EMPLOYEES
VALUES('Facebook Employee', 1, 0, GETDATE(), 1000, 20)
GO

INSERT INTO EMPLOYEES
VALUES('John Doe', 1, 1, GETDATE(), 5000, 10)
GO

INSERT INTO COMPANIES
VALUES('Google')
GO

INSERT INTO EMPLOYEES
VALUES('Larry Page', 2, 3, GETDATE(), 2000, 1)
GO

INSERT INTO EMPLOYEES
VALUES('Sergey Brin', 2, 3, GETDATE(), 3000, 1)
GO

INSERT INTO EMPLOYEES
VALUES('Google Employee', 2, 0, GETDATE(), 4000, 10)
GO

SELECT * FROM COMPANIES
SELECT * FROM EMPLOYEES

*/