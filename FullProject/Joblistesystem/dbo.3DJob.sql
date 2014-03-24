﻿CREATE TABLE [dbo].[My3DJob]
(
	[OrderId] BIGINT NOT NULL PRIMARY KEY, 
    [Material] INT NOT NULL, 
    [Owner] NVARCHAR(50) NOT NULL, 
    [Deadline] NVARCHAR(50) NULL DEFAULT NULL, 
    [MyFile] NVARCHAR(MAX) NOT NULL, 
    [CreationTime] NVARCHAR(50) NOT NULL, 
    [Hollow] INT NOT NULL, 
    [Comment] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_3DJob_Material] FOREIGN KEY ([Material]) REFERENCES [Material]([MaterialId]), 
    CONSTRAINT [FK_3DJob_User] FOREIGN KEY ([Owner]) REFERENCES [Customer]([Email])
)