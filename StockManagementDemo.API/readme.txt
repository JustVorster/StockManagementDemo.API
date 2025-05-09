Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (415ms) [Parameters=[], CommandType='Text', CommandTimeout='60']
      CREATE DATABASE [StockManagementDemo];
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (98ms) [Parameters=[], CommandType='Text', CommandTimeout='60']
      IF SERVERPROPERTY('EngineEdition') <> 5
      BEGIN
          ALTER DATABASE [StockManagementDemo] SET READ_COMMITTED_SNAPSHOT ON;
      END;
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (10ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT 1
Microsoft.EntityFrameworkCore.Migrations[20411]
      Acquiring an exclusive lock for migration application. See https://aka.ms/efcore-docs-migrations-lock for more information if this takes too long.
Acquiring an exclusive lock for migration application. See https://aka.ms/efcore-docs-migrations-lock for more information if this takes too long.
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (24ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      DECLARE @result int;
      EXEC @result = sp_getapplock @Resource = '__EFMigrationsLock', @LockOwner = 'Session', @LockMode = 'Exclusive';
      SELECT @result
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (9ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
      BEGIN
          CREATE TABLE [__EFMigrationsHistory] (
              [MigrationId] nvarchar(150) NOT NULL,
              [ProductVersion] nvarchar(32) NOT NULL,
              CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
          );
      END;
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT 1
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT OBJECT_ID(N'[__EFMigrationsHistory]');
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT [MigrationId], [ProductVersion]
      FROM [__EFMigrationsHistory]
      ORDER BY [MigrationId];
Microsoft.EntityFrameworkCore.Migrations[20402]
      Applying migration '20250508084157_InitialCreate'.
Applying migration '20250508084157_InitialCreate'.
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [Users] (
          [Id] int NOT NULL IDENTITY,
          [Username] nvarchar(max) NOT NULL,
          [Email] nvarchar(max) NOT NULL,
          [PasswordHash] nvarchar(max) NOT NULL,
          [Role] nvarchar(max) NOT NULL,
          CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
      );
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
      VALUES (N'20250508084157_InitialCreate', N'9.0.4');
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      DECLARE @result int;
      EXEC @result = sp_releaseapplock @Resource = '__EFMigrationsLock', @LockOwner = 'Session';
      SELECT @result
