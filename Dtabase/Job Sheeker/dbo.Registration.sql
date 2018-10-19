CREATE TABLE [dbo].[Registration]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [E-Mail] VARCHAR(50) NOT NULL, 
    [Password] VARCHAR(50) NOT NULL, 
    [ConfrimPassword] VARCHAR(50) NOT NULL, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [State] VARCHAR(50) NOT NULL, 
    [City] VARCHAR(50) NULL, 
    [Gender] NCHAR(10) NOT NULL, 
    [Date Of Birth] DATETIME NULL, 
    [Date Of Entry] DATETIME NULL, 
    [Contact Number] INT NOT NULL, 
    [IsEmailVerried] BIT NOT NULL, 
    [ActivationCode] UNIQUEIDENTIFIER NOT NULL
)
