CREATE TABLE [dbo].[Comment]
(
	[Id] uniqueidentifier not null primary key,
	[Content] nvarchar(max) not null,
	[Date] datetime2 not null,
	[ArticleId] uniqueidentifier foreign key references Article(Id) null
)
