
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 01/14/2011 15:25:14
-- Generated from EDMX file: C:\Users\julien\Documents\Visual Studio 2010\Projects\CQRSWorkshopFullApplicationCorrection\ReadModel\ReadModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CQRSWorkshopReadModel];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[MediaItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MediaItems];
GO
IF OBJECT_ID(N'[dbo].[CustomerItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerItems];
GO
IF OBJECT_ID(N'[dbo].[CustomerRentedItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerRentedItems];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'MediaItems'
CREATE TABLE [dbo].[MediaItems] (
    [Id] uniqueidentifier  NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CustomerItems'
CREATE TABLE [dbo].[CustomerItems] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [CanRent] bit  NOT NULL,
    [CanReturn] bit  NOT NULL
);
GO

-- Creating table 'CustomerRentedItems'
CREATE TABLE [dbo].[CustomerRentedItems] (
    [MediaId] uniqueidentifier  NOT NULL,
    [CustomerId] uniqueidentifier  NOT NULL,
    [CustomerName] nvarchar(max)  NOT NULL,
    [MediaTitle] nvarchar(max)  NOT NULL,
    [DueDate] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'MediaItems'
ALTER TABLE [dbo].[MediaItems]
ADD CONSTRAINT [PK_MediaItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CustomerItems'
ALTER TABLE [dbo].[CustomerItems]
ADD CONSTRAINT [PK_CustomerItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [MediaId], [CustomerId] in table 'CustomerRentedItems'
ALTER TABLE [dbo].[CustomerRentedItems]
ADD CONSTRAINT [PK_CustomerRentedItems]
    PRIMARY KEY CLUSTERED ([MediaId], [CustomerId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------