CREATE TABLE [Article]
(
	[Id] uniqueidentifier not null primary key,
	[Title] nvarchar(300) not null,
	[Description] nvarchar(1000) null,
	[Content] nvarchar(max) not null,
	[Date] datetime2 not null
)
