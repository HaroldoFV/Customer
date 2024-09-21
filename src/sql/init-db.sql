CREATE DATABASE [customerdb];
GO 

USE [customerdb];
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
CREATE TABLE [__EFMigrationsHistory] (
    [MigrationId] nvarchar(150) NOT NULL,
    [ProductVersion] nvarchar(32) NOT NULL,
    CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Customers] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [BirthDate] datetime2 NOT NULL,
    [GenderType] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id])
    );
GO

CREATE TABLE [Address] (
    [Id] int NOT NULL IDENTITY,
    [Street] nvarchar(255) NOT NULL,
    [Number] nvarchar(20) NOT NULL,
    [Complement] nvarchar(255) NOT NULL,
    [Neighborhood] nvarchar(255) NOT NULL,
    [City] nvarchar(100) NOT NULL,
    [State] nvarchar(50) NOT NULL,
    [ZipCode] nvarchar(9) NOT NULL,
    [CustomerId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Address_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE
    );
GO

CREATE INDEX [IX_Address_CustomerId] ON [Address] ([CustomerId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240920162641_CreateCustomerAndAddress', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Address] DROP CONSTRAINT [PK_Address];
GO

DROP INDEX [IX_Address_CustomerId] ON [Address];
GO

ALTER TABLE [Address] ADD CONSTRAINT [PK_Address] PRIMARY KEY ([CustomerId], [Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240921020607_AjustaCustomerAndAddress', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Address].[Id]', N'id', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240921022341_AjustaKeyCustomerAddress', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Address] DROP CONSTRAINT [PK_Address];
GO

EXEC sp_rename N'[Address].[id]', N'Id', N'COLUMN';
GO

ALTER TABLE [Address] ADD CONSTRAINT [PK_Address] PRIMARY KEY ([Id]);
GO

CREATE INDEX [IX_Address_CustomerId] ON [Address] ([CustomerId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240921023007_AddIdToAddress', N'8.0.8');
GO

COMMIT;
GO
