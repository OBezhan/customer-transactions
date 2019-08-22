﻿CREATE TABLE [dbo].[Customer]
(
	[Id] BIGINT NOT NULL IDENTITY(1,1),
	[Name] NVARCHAR(30) NOT NULL,
	[Email] NVARCHAR(25) NOT NULL,
	[MobileNumber] NVARCHAR(10),

	CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([Id])
)
