SET IDENTITY_INSERT [dbo].[Customer] ON
	INSERT INTO [dbo].[Customer] ([Id], [Name], [Email], [MobileNumber]) VALUES (1, 'OBezhan', 'oleg.bezhan@gmail.com', '0660695737')
	INSERT INTO [dbo].[Customer] ([Id], [Name], [Email], [MobileNumber]) VALUES (2, 'TestUser', 'test.user@gmail.com', '0660695738')
	INSERT INTO [dbo].[Customer] ([Id], [Name], [Email], [MobileNumber]) VALUES (3, 'NoName', 'no.name@gmail.com', '0660695739')
SET IDENTITY_INSERT [dbo].[Customer] OFF

INSERT INTO [dbo].[Transaction] ([CustomerId], [DateTimeUtc], [Amount], [CurrencyId], [StatusId]) VALUES (1, GETUTCDATE(), 0.11, 1, 1)
INSERT INTO [dbo].[Transaction] ([CustomerId], [DateTimeUtc], [Amount], [CurrencyId], [StatusId]) VALUES (1, GETUTCDATE(), 0.30, 1, 2)

INSERT INTO [dbo].[Transaction] ([CustomerId], [DateTimeUtc], [Amount], [CurrencyId], [StatusId]) VALUES (2, GETUTCDATE(), 1.30, 2, 1)