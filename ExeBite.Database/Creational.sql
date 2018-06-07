IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308090318_Init')
BEGIN
    CREATE TABLE [Location] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Address] nvarchar(max) NULL,
        CONSTRAINT [PK_Location] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308090318_Init')
BEGIN
    CREATE TABLE [Meal] (
        [Id] int NOT NULL IDENTITY,
        [Price] decimal(18, 2) NOT NULL,
        CONSTRAINT [PK_Meal] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308090318_Init')
BEGIN
    CREATE TABLE [Restaurant] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_Restaurant] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308090318_Init')
BEGIN
    CREATE TABLE [Customer] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Balance] decimal(18, 2) NOT NULL,
        [AppUserId] nvarchar(max) NULL,
        [LocationId] int NOT NULL,
        CONSTRAINT [PK_Customer] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Customer_Location_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Location] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308090318_Init')
BEGIN
    CREATE TABLE [Food] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Type] int NOT NULL,
        [Price] decimal(18, 2) NOT NULL,
        [Description] nvarchar(max) NULL,
        [IsInactive] bit NOT NULL,
        [RestaurantId] int NOT NULL,
        [RestaurantEntityId] int NULL,
        CONSTRAINT [PK_Food] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Food_Restaurant_RestaurantEntityId] FOREIGN KEY ([RestaurantEntityId]) REFERENCES [Restaurant] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Food_Restaurant_RestaurantId] FOREIGN KEY ([RestaurantId]) REFERENCES [Restaurant] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308090318_Init')
BEGIN
    CREATE TABLE [CustomerAliasesEntities] (
        [Id] int NOT NULL IDENTITY,
        [Alias] nvarchar(max) NULL,
        [CustomerId] int NOT NULL,
        [RestaurantId] int NOT NULL,
        CONSTRAINT [PK_CustomerAliasesEntities] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CustomerAliasesEntities_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_CustomerAliasesEntities_Restaurant_RestaurantId] FOREIGN KEY ([RestaurantId]) REFERENCES [Restaurant] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308090318_Init')
BEGIN
    CREATE TABLE [Order] (
        [Id] int NOT NULL IDENTITY,
        [Price] decimal(18, 2) NOT NULL,
        [Date] datetime2 NOT NULL,
        [Note] nvarchar(max) NULL,
        [MealId] int NOT NULL,
        [CustomerId] int NOT NULL,
        CONSTRAINT [PK_Order] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Order_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Order_Meal_MealId] FOREIGN KEY ([MealId]) REFERENCES [Meal] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308090318_Init')
BEGIN
    CREATE TABLE [FoodEntityMealEntities] (
        [FoodEntityId] int NOT NULL,
        [MealEntityId] int NOT NULL,
        CONSTRAINT [PK_FoodEntityMealEntities] PRIMARY KEY ([FoodEntityId], [MealEntityId]),
        CONSTRAINT [FK_FoodEntityMealEntities_Food_FoodEntityId] FOREIGN KEY ([FoodEntityId]) REFERENCES [Food] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_FoodEntityMealEntities_Meal_MealEntityId] FOREIGN KEY ([MealEntityId]) REFERENCES [Meal] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308090318_Init')
BEGIN
    CREATE TABLE [Recipe] (
        [Id] int NOT NULL IDENTITY,
        [MainCourseId] int NOT NULL,
        CONSTRAINT [PK_Recipe] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Recipe_Food_MainCourseId] FOREIGN KEY ([MainCourseId]) REFERENCES [Food] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308090318_Init')
BEGIN
    CREATE TABLE [FoodEntityRecipeEntity] (
        [FoodEntityId] int NOT NULL,
        [RecepieEntityId] int NOT NULL,
        CONSTRAINT [PK_FoodEntityRecipeEntity] PRIMARY KEY ([FoodEntityId], [RecepieEntityId]),
        CONSTRAINT [FK_FoodEntityRecipeEntity_Food_FoodEntityId] FOREIGN KEY ([FoodEntityId]) REFERENCES [Food] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_FoodEntityRecipeEntity_Recipe_RecepieEntityId] FOREIGN KEY ([RecepieEntityId]) REFERENCES [Recipe] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308090318_Init')
BEGIN
    CREATE INDEX [IX_Customer_LocationId] ON [Customer] ([LocationId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308090318_Init')
BEGIN
    CREATE INDEX [IX_CustomerAliasesEntities_CustomerId] ON [CustomerAliasesEntities] ([CustomerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308090318_Init')
BEGIN
    CREATE INDEX [IX_CustomerAliasesEntities_RestaurantId] ON [CustomerAliasesEntities] ([RestaurantId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308090318_Init')
BEGIN
    CREATE INDEX [IX_Food_RestaurantEntityId] ON [Food] ([RestaurantEntityId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308090318_Init')
BEGIN
    CREATE INDEX [IX_Food_RestaurantId] ON [Food] ([RestaurantId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308090318_Init')
BEGIN
    CREATE INDEX [IX_FoodEntityMealEntities_MealEntityId] ON [FoodEntityMealEntities] ([MealEntityId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308090318_Init')
BEGIN
    CREATE INDEX [IX_FoodEntityRecipeEntity_RecepieEntityId] ON [FoodEntityRecipeEntity] ([RecepieEntityId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308090318_Init')
BEGIN
    CREATE INDEX [IX_Order_CustomerId] ON [Order] ([CustomerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308090318_Init')
BEGIN
    CREATE UNIQUE INDEX [IX_Order_MealId] ON [Order] ([MealId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308090318_Init')
BEGIN
    CREATE INDEX [IX_Recipe_MainCourseId] ON [Recipe] ([MainCourseId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308090318_Init')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180308090318_Init', N'2.1.0-preview1-28290');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308093520_Seed')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [object_id] = OBJECT_ID(N'Restaurant'))
        SET IDENTITY_INSERT [Restaurant] ON;
    INSERT INTO [Restaurant] ([Id], [Name])
    VALUES (1, N'Restoran pod Lipom');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [object_id] = OBJECT_ID(N'Restaurant'))
        SET IDENTITY_INSERT [Restaurant] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308093520_Seed')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [object_id] = OBJECT_ID(N'Restaurant'))
        SET IDENTITY_INSERT [Restaurant] ON;
    INSERT INTO [Restaurant] ([Id], [Name])
    VALUES (2, N'Hedone');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [object_id] = OBJECT_ID(N'Restaurant'))
        SET IDENTITY_INSERT [Restaurant] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308093520_Seed')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [object_id] = OBJECT_ID(N'Restaurant'))
        SET IDENTITY_INSERT [Restaurant] ON;
    INSERT INTO [Restaurant] ([Id], [Name])
    VALUES (3, N'Index House');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [object_id] = OBJECT_ID(N'Restaurant'))
        SET IDENTITY_INSERT [Restaurant] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308093520_Seed')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [object_id] = OBJECT_ID(N'Restaurant'))
        SET IDENTITY_INSERT [Restaurant] ON;
    INSERT INTO [Restaurant] ([Id], [Name])
    VALUES (4, N'Teglas');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [object_id] = OBJECT_ID(N'Restaurant'))
        SET IDENTITY_INSERT [Restaurant] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308093520_Seed')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [object_id] = OBJECT_ID(N'Restaurant'))
        SET IDENTITY_INSERT [Restaurant] ON;
    INSERT INTO [Restaurant] ([Id], [Name])
    VALUES (5, N'Extra Food');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [object_id] = OBJECT_ID(N'Restaurant'))
        SET IDENTITY_INSERT [Restaurant] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308093520_Seed')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [object_id] = OBJECT_ID(N'Location'))
        SET IDENTITY_INSERT [Location] ON;
    INSERT INTO [Location] ([Id], [Name], [Address])
    VALUES (1, N'Bulevar', N'Bulevar Vojvode Stjepe 50');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [object_id] = OBJECT_ID(N'Location'))
        SET IDENTITY_INSERT [Location] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308093520_Seed')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [object_id] = OBJECT_ID(N'Location'))
        SET IDENTITY_INSERT [Location] ON;
    INSERT INTO [Location] ([Id], [Name], [Address])
    VALUES (2, N'JD', N'Jovana Ducica');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [object_id] = OBJECT_ID(N'Location'))
        SET IDENTITY_INSERT [Location] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308093520_Seed')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180308093520_Seed', N'2.1.0-preview1-28290');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180308125141_FoodFix1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180308125141_FoodFix1', N'2.1.0-preview1-28290');
END;

GO

