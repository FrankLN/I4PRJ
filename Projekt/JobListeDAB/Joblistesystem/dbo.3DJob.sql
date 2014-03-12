CREATE TABLE [dbo].[3DJob]
(
	[OrderId] BIGINT NOT NULL PRIMARY KEY, 
    [Material] NVARCHAR(50) NOT NULL, 
    [Owner] NVARCHAR(50) NOT NULL, 
    [Deadline] NVARCHAR(50) NULL DEFAULT NULL, 
    [File] NVARCHAR(MAX) NOT NULL, 
    [CreationTime] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_3DJob_Material] FOREIGN KEY ([Material]) REFERENCES [Material]([MaterialType]), 
    CONSTRAINT [FK_3DJob_User] FOREIGN KEY ([Owner]) REFERENCES [User]([Email])
)
