CREATE TABLE [dbo].[MediaItems](
	[Id] [uniqueidentifier] PRIMARY KEY,
	[Title] [varchar](250) NULL
) ON [PRIMARY];

CREATE TABLE [dbo].[CustomerItems] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [CanRent] bit  NOT NULL,
    [CanReturn] bit  NOT NULL
);


CREATE TABLE [dbo].[CustomerRentedItems] (
    [MediaId] uniqueidentifier  NOT NULL,
    [CustomerId] uniqueidentifier  NOT NULL,
    [CustomerName] nvarchar(max)  NOT NULL,
    [MediaTitle] nvarchar(max)  NOT NULL,
    [DueDate] datetime2  NOT NULL
)
