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


-- Inserção de dados para Customers e Address (Somente endereços brasileiros)
-- Inserir 10 Customers com dados fictícios
INSERT INTO [Customers] ([Id], [Name], [BirthDate], [GenderType], [CreatedAt])
VALUES
    (NEWID(), 'João Silva', '1985-03-15', 'Male', GETDATE()), -- 1
    (NEWID(), 'Maria Oliveira', '1990-07-22', 'Female', GETDATE()), -- 2
    (NEWID(), 'Carlos Souza', '1978-09-30', 'Male', GETDATE()), -- 3
    (NEWID(), 'Ana Pereira', '1995-12-01', 'Female', GETDATE()), -- 4
    (NEWID(), 'Pedro Gomes', '1982-11-05', 'Male', GETDATE()), -- 5
    (NEWID(), 'Lucas Alves', '2000-04-20', 'Male', GETDATE()), -- 6
    (NEWID(), 'Clara Mendes', '1988-08-14', 'Female', GETDATE()), -- 7
    (NEWID(), 'Rafael Lima', '1993-05-10', 'Male', GETDATE()), -- 8
    (NEWID(), 'Beatriz Duarte', '1997-01-17', 'Female', GETDATE()), -- 9
    (NEWID(), 'Miguel Costa', '1986-10-09', 'Male', GETDATE()); -- 10
GO

-- Inserção de Addresses para esses Customers (com endereços brasileiros)
-- João Silva (1 Address)
INSERT INTO [Address] ([Street], [Number], [Complement], [Neighborhood], [City], [State], [ZipCode], [CustomerId])
VALUES
    ('Rua das Flores', '123', 'Apto 101', 'Centro', 'São Paulo', 'SP', '01001-000', (SELECT [Id] FROM [Customers] WHERE [Name] = 'João Silva'));

-- Maria Oliveira (2 Addresses)
INSERT INTO [Address] ([Street], [Number], [Complement], [Neighborhood], [City], [State], [ZipCode], [CustomerId])
VALUES
    ('Avenida Brasil', '456', 'Casa', 'Jardim América', 'Rio de Janeiro', 'RJ', '22041-001', (SELECT [Id] FROM [Customers] WHERE [Name] = 'Maria Oliveira')),
    ('Rua do Comércio', '789', '', 'Centro', 'Rio de Janeiro', 'RJ', '20031-005', (SELECT [Id] FROM [Customers] WHERE [Name] = 'Maria Oliveira'));

-- Carlos Souza (3 Addresses)
INSERT INTO [Address] ([Street], [Number], [Complement], [Neighborhood], [City], [State], [ZipCode], [CustomerId])
VALUES
    ('Rua XV de Novembro', '100', 'Apto 202', 'Centro', 'Curitiba', 'PR', '80060-000', (SELECT [Id] FROM [Customers] WHERE [Name] = 'Carlos Souza')),
    ('Rua das Palmeiras', '500', 'Casa', 'Centro', 'Florianópolis', 'SC', '88010-001', (SELECT [Id] FROM [Customers] WHERE [Name] = 'Carlos Souza')),
    ('Rua dos Andradas', '300', 'Sala 5', 'Centro', 'Porto Alegre', 'RS', '90020-005', (SELECT [Id] FROM [Customers] WHERE [Name] = 'Carlos Souza'));

-- Ana Pereira (1 Address)
INSERT INTO [Address] ([Street], [Number], [Complement], [Neighborhood], [City], [State], [ZipCode], [CustomerId])
VALUES
    ('Rua Santa Clara', '150', 'Apto 12', 'Copacabana', 'Rio de Janeiro', 'RJ', '22041-012', (SELECT [Id] FROM [Customers] WHERE [Name] = 'Ana Pereira'));

-- Pedro Gomes (2 Addresses)
INSERT INTO [Address] ([Street], [Number], [Complement], [Neighborhood], [City], [State], [ZipCode], [CustomerId])
VALUES
    ('Rua Sete de Setembro', '200', 'Apto 3B', 'Centro', 'Belo Horizonte', 'MG', '30120-001', (SELECT [Id] FROM [Customers] WHERE [Name] = 'Pedro Gomes')),
    ('Rua das Acácias', '600', '', 'Jardim Botânico', 'Rio de Janeiro', 'RJ', '22460-035', (SELECT [Id] FROM [Customers] WHERE [Name] = 'Pedro Gomes'));

-- Lucas Alves (1 Address)
INSERT INTO [Address] ([Street], [Number], [Complement], [Neighborhood], [City], [State], [ZipCode], [CustomerId])
VALUES
    ('Rua Augusta', '1200', 'Cobertura', 'Consolação', 'São Paulo', 'SP', '01305-100', (SELECT [Id] FROM [Customers] WHERE [Name] = 'Lucas Alves'));

-- Clara Mendes (2 Addresses)
INSERT INTO [Address] ([Street], [Number], [Complement], [Neighborhood], [City], [State], [ZipCode], [CustomerId])
VALUES
    ('Avenida Paulista', '700', 'Sala 101', 'Bela Vista', 'São Paulo', 'SP', '01310-100', (SELECT [Id] FROM [Customers] WHERE [Name] = 'Clara Mendes')),
    ('Rua Frei Caneca', '800', '', 'Consolação', 'São Paulo', 'SP', '01307-001', (SELECT [Id] FROM [Customers] WHERE [Name] = 'Clara Mendes'));

-- Rafael Lima (1 Address)
INSERT INTO [Address] ([Street], [Number], [Complement], [Neighborhood], [City], [State], [ZipCode], [CustomerId])
VALUES
    ('Rua da Consolação', '900', 'Apto 5', 'Centro', 'São Paulo', 'SP', '01301-000', (SELECT [Id] FROM [Customers] WHERE [Name] = 'Rafael Lima'));

-- Beatriz Duarte (3 Addresses)
INSERT INTO [Address] ([Street], [Number], [Complement], [Neighborhood], [City], [State], [ZipCode], [CustomerId])
VALUES
    ('Rua Oscar Freire', '1000', 'Apto 9C', 'Jardim Paulista', 'São Paulo', 'SP', '01426-001', (SELECT [Id] FROM [Customers] WHERE [Name] = 'Beatriz Duarte')),
    ('Rua Harmonia', '1100', '', 'Vila Madalena', 'São Paulo', 'SP', '05435-001', (SELECT [Id] FROM [Customers] WHERE [Name] = 'Beatriz Duarte')),
    ('Rua Girassol', '1200', 'Apto 7A', 'Vila Madalena', 'São Paulo', 'SP', '05433-001', (SELECT [Id] FROM [Customers] WHERE [Name] = 'Beatriz Duarte'));

-- Miguel Costa (1 Address)
INSERT INTO [Address] ([Street], [Number], [Complement], [Neighborhood], [City], [State], [ZipCode], [CustomerId])
VALUES
    ('Rua das Palmeiras', '150', 'Casa', 'Centro', 'Belo Horizonte', 'MG', '30140-110', (SELECT [Id] FROM [Customers] WHERE [Name] = 'Miguel Costa'));
GO
