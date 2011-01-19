
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 01/08/2011 16:08:48
-- Generated from EDMX file: C:\Users\julien\documents\visual studio 2010\Projects\CQRSWorkshop\ReadModel\ReadModel.edmx
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

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'MediaItems'
CREATE TABLE [dbo].[MediaItems] (
    [Id] uniqueidentifier  NOT NULL,
    [Title] nvarchar(max)  NOT NULL
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

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------