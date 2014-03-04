CREATE TABLE [dbo].[3DJob]
(
	[OrdreId] BIGINT NOT NULL PRIMARY KEY, 
    [Materiale] NVARCHAR(50) NOT NULL, 
    [Ejer] NVARCHAR(50) NOT NULL, 
    [Deadline] NVARCHAR(50) NULL DEFAULT NULL, 
    [Fil] NVARCHAR(MAX) NOT NULL, 
    [OprettelsesTidspunkt] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_3DJob_Materiale] FOREIGN KEY ([Materiale]) REFERENCES [Materiale]([MaterialeType]), 
    CONSTRAINT [FK_3DJob_Bruger] FOREIGN KEY ([Ejer]) REFERENCES [Bruger]([Email])
)
