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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250306230434_Initial-Creation'
)
BEGIN
    CREATE TABLE [Categories] (
        [CategoryId] int NOT NULL IDENTITY,
        [UserId] nvarchar(32) NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250306230434_Initial-Creation'
)
BEGIN
    CREATE TABLE [Habits] (
        [HabitId] int NOT NULL IDENTITY,
        [UserId] nvarchar(32) NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Description] nvarchar(500) NULL,
        [CategoryId] int NOT NULL,
        [TargetAttempts] int NOT NULL,
        CONSTRAINT [PK_Habits] PRIMARY KEY ([HabitId]),
        CONSTRAINT [FK_Habits_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([CategoryId]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250306230434_Initial-Creation'
)
BEGIN
    CREATE TABLE [HabitCompletions] (
        [Id] int NOT NULL IDENTITY,
        [CompletionDate] datetimeoffset NOT NULL,
        [CompletionCount] int NOT NULL,
        [HabitId] int NULL,
        CONSTRAINT [PK_HabitCompletions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_HabitCompletions_Habits_HabitId] FOREIGN KEY ([HabitId]) REFERENCES [Habits] ([HabitId]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250306230434_Initial-Creation'
)
BEGIN
    CREATE INDEX [IX_HabitCompletions_HabitId] ON [HabitCompletions] ([HabitId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250306230434_Initial-Creation'
)
BEGIN
    CREATE INDEX [IX_Habits_CategoryId] ON [Habits] ([CategoryId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250306230434_Initial-Creation'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250306230434_Initial-Creation', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250318183300_Fix-UserId-Length'
)
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Habits]') AND [c].[name] = N'UserId');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [Habits] DROP CONSTRAINT [' + @var + '];');
    ALTER TABLE [Habits] ALTER COLUMN [UserId] nvarchar(48) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250318183300_Fix-UserId-Length'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Categories]') AND [c].[name] = N'UserId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Categories] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Categories] ALTER COLUMN [UserId] nvarchar(48) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250318183300_Fix-UserId-Length'
)
BEGIN
    CREATE INDEX [IX_HabitCompletions_CompletionDate] ON [HabitCompletions] ([CompletionDate]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250318183300_Fix-UserId-Length'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250318183300_Fix-UserId-Length', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321233237_Add-Weighing-Model'
)
BEGIN
    CREATE TABLE [Weighings] (
        [WeighingId] int NOT NULL IDENTITY,
        [UserId] nvarchar(48) NOT NULL,
        [Date] datetimeoffset NOT NULL,
        [Weight] decimal(5,2) NOT NULL,
        CONSTRAINT [PK_Weighings] PRIMARY KEY ([WeighingId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321233237_Add-Weighing-Model'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Weighings_UserId_Date] ON [Weighings] ([UserId], [Date]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321233237_Add-Weighing-Model'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250321233237_Add-Weighing-Model', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250325180746_Add-ContentLink-Model'
)
BEGIN
    CREATE TABLE [ContentLinks] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(100) NOT NULL,
        [Description] nvarchar(250) NULL,
        [Url] nvarchar(500) NOT NULL,
        [SortOrder] int NOT NULL,
        [Group] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_ContentLinks] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250325180746_Add-ContentLink-Model'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250325180746_Add-ContentLink-Model', N'9.0.3');
END;

COMMIT;
GO

