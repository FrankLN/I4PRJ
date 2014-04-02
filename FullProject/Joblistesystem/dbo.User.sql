﻿CREATE TABLE [dbo].[Customer]
(
	[Email] NVARCHAR(50) NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(MAX) NOT NULL, 
	[LastName] NVARCHAR(MAX) NOT NULL, 
    [PhoneNumber] NVARCHAR(8) NULL DEFAULT NULL, 
    [AdminRights] INT NULL DEFAULT NULL, 
    [Password] NVARCHAR(50) NOT NULL, 
)
