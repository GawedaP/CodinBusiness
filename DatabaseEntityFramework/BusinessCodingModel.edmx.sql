
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/02/2013 20:38:09
-- Generated from EDMX file: E:\Projekty\BusinessCoding\DatabaseEntityFramework\BusinessCodingModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BusinessCoding];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UsersCompanies]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_UsersCompanies];
GO
IF OBJECT_ID(N'[dbo].[FK_TypesOfOfficesCompanies]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Companies] DROP CONSTRAINT [FK_TypesOfOfficesCompanies];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectsDescriptions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK_ProjectsDescriptions];
GO
IF OBJECT_ID(N'[dbo].[FK_EmpolyeesTypesOfEmployees]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Empolyees] DROP CONSTRAINT [FK_EmpolyeesTypesOfEmployees];
GO
IF OBJECT_ID(N'[dbo].[FK_CompaniesProjects]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK_CompaniesProjects];
GO
IF OBJECT_ID(N'[dbo].[FK_CompaniesEmpolyees]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Empolyees] DROP CONSTRAINT [FK_CompaniesEmpolyees];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Companies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Companies];
GO
IF OBJECT_ID(N'[dbo].[Empolyees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Empolyees];
GO
IF OBJECT_ID(N'[dbo].[TypesOfEmployees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypesOfEmployees];
GO
IF OBJECT_ID(N'[dbo].[Projects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Projects];
GO
IF OBJECT_ID(N'[dbo].[Descriptions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Descriptions];
GO
IF OBJECT_ID(N'[dbo].[TypesOfOffices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypesOfOffices];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(50)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [ActivationDate] datetime  NOT NULL,
    [LastLogin] datetime  NOT NULL
);
GO

-- Creating table 'Companies'
CREATE TABLE [dbo].[Companies] (
    [CompanyID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Respect] int  NOT NULL,
    [Money] float  NOT NULL,
    [CostOfLiving] float  NOT NULL,
    [TypesOfOfficesTOOfficeID] int  NOT NULL,
    [UsersUserID] int  NOT NULL
);
GO

-- Creating table 'Empolyees'
CREATE TABLE [dbo].[Empolyees] (
    [EmployeeID] int IDENTITY(1,1) NOT NULL,
    [DateOfHiring] datetime  NULL,
    [TypesOfEmployeesTOEmployeeID] int  NOT NULL,
    [CompaniesCompanyID] int  NULL
);
GO

-- Creating table 'TypesOfEmployees'
CREATE TABLE [dbo].[TypesOfEmployees] (
    [TOEmployeeID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Salary] float  NOT NULL,
    [Codity] smallint  NOT NULL
);
GO

-- Creating table 'Projects'
CREATE TABLE [dbo].[Projects] (
    [ProjectID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Complexity] smallint  NOT NULL,
    [TimeRounds] smallint  NOT NULL,
    [TimeToEnd] smallint  NULL,
    [Gratification] float  NOT NULL,
    [MinRespect] int  NOT NULL,
    [MinCodity] smallint  NOT NULL,
    [CompaniesCompanyID] int  NULL
);
GO

-- Creating table 'Descriptions'
CREATE TABLE [dbo].[Descriptions] (
    [DescriptionID] int IDENTITY(1,1) NOT NULL,
    [String] nvarchar(max)  NOT NULL,
    [ProjectsProjectID] int  NOT NULL
);
GO

-- Creating table 'TypesOfOffices'
CREATE TABLE [dbo].[TypesOfOffices] (
    [TOOfficeID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Capacity] smallint  NOT NULL,
    [CostOfHiring] float  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [UserID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [CompanyID] in table 'Companies'
ALTER TABLE [dbo].[Companies]
ADD CONSTRAINT [PK_Companies]
    PRIMARY KEY CLUSTERED ([CompanyID] ASC);
GO

-- Creating primary key on [EmployeeID] in table 'Empolyees'
ALTER TABLE [dbo].[Empolyees]
ADD CONSTRAINT [PK_Empolyees]
    PRIMARY KEY CLUSTERED ([EmployeeID] ASC);
GO

-- Creating primary key on [TOEmployeeID] in table 'TypesOfEmployees'
ALTER TABLE [dbo].[TypesOfEmployees]
ADD CONSTRAINT [PK_TypesOfEmployees]
    PRIMARY KEY CLUSTERED ([TOEmployeeID] ASC);
GO

-- Creating primary key on [ProjectID] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [PK_Projects]
    PRIMARY KEY CLUSTERED ([ProjectID] ASC);
GO

-- Creating primary key on [DescriptionID] in table 'Descriptions'
ALTER TABLE [dbo].[Descriptions]
ADD CONSTRAINT [PK_Descriptions]
    PRIMARY KEY CLUSTERED ([DescriptionID] ASC);
GO

-- Creating primary key on [TOOfficeID] in table 'TypesOfOffices'
ALTER TABLE [dbo].[TypesOfOffices]
ADD CONSTRAINT [PK_TypesOfOffices]
    PRIMARY KEY CLUSTERED ([TOOfficeID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TypesOfOfficesTOOfficeID] in table 'Companies'
ALTER TABLE [dbo].[Companies]
ADD CONSTRAINT [FK_TypesOfOfficesCompanies]
    FOREIGN KEY ([TypesOfOfficesTOOfficeID])
    REFERENCES [dbo].[TypesOfOffices]
        ([TOOfficeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TypesOfOfficesCompanies'
CREATE INDEX [IX_FK_TypesOfOfficesCompanies]
ON [dbo].[Companies]
    ([TypesOfOfficesTOOfficeID]);
GO

-- Creating foreign key on [TypesOfEmployeesTOEmployeeID] in table 'Empolyees'
ALTER TABLE [dbo].[Empolyees]
ADD CONSTRAINT [FK_EmpolyeesTypesOfEmployees]
    FOREIGN KEY ([TypesOfEmployeesTOEmployeeID])
    REFERENCES [dbo].[TypesOfEmployees]
        ([TOEmployeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmpolyeesTypesOfEmployees'
CREATE INDEX [IX_FK_EmpolyeesTypesOfEmployees]
ON [dbo].[Empolyees]
    ([TypesOfEmployeesTOEmployeeID]);
GO

-- Creating foreign key on [CompaniesCompanyID] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [FK_CompaniesProjects]
    FOREIGN KEY ([CompaniesCompanyID])
    REFERENCES [dbo].[Companies]
        ([CompanyID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CompaniesProjects'
CREATE INDEX [IX_FK_CompaniesProjects]
ON [dbo].[Projects]
    ([CompaniesCompanyID]);
GO

-- Creating foreign key on [CompaniesCompanyID] in table 'Empolyees'
ALTER TABLE [dbo].[Empolyees]
ADD CONSTRAINT [FK_CompaniesEmpolyees]
    FOREIGN KEY ([CompaniesCompanyID])
    REFERENCES [dbo].[Companies]
        ([CompanyID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CompaniesEmpolyees'
CREATE INDEX [IX_FK_CompaniesEmpolyees]
ON [dbo].[Empolyees]
    ([CompaniesCompanyID]);
GO

-- Creating foreign key on [UsersUserID] in table 'Companies'
ALTER TABLE [dbo].[Companies]
ADD CONSTRAINT [FK_UsersCompanies]
    FOREIGN KEY ([UsersUserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersCompanies'
CREATE INDEX [IX_FK_UsersCompanies]
ON [dbo].[Companies]
    ([UsersUserID]);
GO

-- Creating foreign key on [ProjectsProjectID] in table 'Descriptions'
ALTER TABLE [dbo].[Descriptions]
ADD CONSTRAINT [FK_ProjectsDescriptions]
    FOREIGN KEY ([ProjectsProjectID])
    REFERENCES [dbo].[Projects]
        ([ProjectID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectsDescriptions'
CREATE INDEX [IX_FK_ProjectsDescriptions]
ON [dbo].[Descriptions]
    ([ProjectsProjectID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------