CREATE TABLE [dbo].[Transaction]
(
	[Id] BIGINT NOT NULL IDENTITY(1,1),
	[CustomerId] BIGINT NOT NULL,
	[DateTimeUtc] DATETIME2 NOT NULL,
	[Amount] MONEY NOT NULL,
	[CurrencyId] SMALLINT NOT NULL,
	[StatusId] TINYINT NOT NULL,
	
	CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_Transaction_Customer] FOREIGN KEY([CustomerId]) REFERENCES [dbo].[Customer]([Id]),
	CONSTRAINT [FK_Transaction_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currency]([Id]),
	CONSTRAINT [FK_Transaction_Status] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status]([Id])
)
