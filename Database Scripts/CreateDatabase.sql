USE [master]
GO

/****** Object:  Database [Chef]    Script Date: 8/9/2025 10:19:20 AM ******/
CREATE DATABASE [Chef]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Chef', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Chef.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Chef_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Chef_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Chef].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [Chef] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [Chef] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [Chef] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [Chef] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [Chef] SET ARITHABORT OFF 
GO

ALTER DATABASE [Chef] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [Chef] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [Chef] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [Chef] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [Chef] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [Chef] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [Chef] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [Chef] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [Chef] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [Chef] SET  DISABLE_BROKER 
GO

ALTER DATABASE [Chef] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [Chef] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [Chef] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [Chef] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [Chef] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [Chef] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [Chef] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [Chef] SET RECOVERY FULL 
GO

ALTER DATABASE [Chef] SET  MULTI_USER 
GO

ALTER DATABASE [Chef] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [Chef] SET DB_CHAINING OFF 
GO

ALTER DATABASE [Chef] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [Chef] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [Chef] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [Chef] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [Chef] SET QUERY_STORE = ON
GO

ALTER DATABASE [Chef] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO

ALTER DATABASE [Chef] SET  READ_WRITE 
GO

USE [Chef]
GO

CREATE TABLE Chefs (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName nvarchar(50) NOT NULL,
    LastName nvarchar(50) NOT NULL,    
    ChefNumber nvarchar(20) NOT NULL

);

CREATE TABLE [Address] (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    StreetAddress nvarchar(50) NOT NULL,
    CityTownVillage nvarchar(50),
    PostalZipCode nvarchar(50),
	StateProvinceRegion nvarchar(50),
	ChefID INT,
	RestaurantID INT
);

GO

CREATE TABLE Phone (
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Number nvarchar(20) NOT NULL,
	ChefID INT,
	RestaurantID INT
);

GO

CREATE TABLE Restaurant (
	ID INT IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(20) NOT NULL,
	PhoneID INT,
	AddressID INT,
	ChefID INT
);

GO


CREATE TABLE [LogEvents] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Message] NVARCHAR(100),
    [MessageTemplate] NVARCHAR(100),
    [Level] NVARCHAR(100),
    [TimeStamp] DATETIME,
    [Exception] NVARCHAR(100),
    [Properties] NVARCHAR(100)
);

GO

ALTER TABLE Phone
ADD CONSTRAINT FK_Phone_Chefs
FOREIGN KEY (ChefID)
REFERENCES Chefs(ID)
ON DELETE CASCADE
ON UPDATE CASCADE;

GO

ALTER TABLE [Address]
ADD CONSTRAINT FK_Address_Chefs
FOREIGN KEY (ChefID)
REFERENCES Chefs(ID)
ON DELETE CASCADE
ON UPDATE CASCADE;

GO

ALTER TABLE Restaurant
ADD CONSTRAINT FK_Restaurant_Chefs
FOREIGN KEY (ChefID)
REFERENCES Chefs(ID)
ON DELETE CASCADE
ON UPDATE CASCADE;

GO

CREATE NONCLUSTERED INDEX IX_Address_ChefID
ON dbo.[Address] (ChefID);
GO

CREATE NONCLUSTERED INDEX IX_Phone_ChefID
ON dbo.Phone (ChefID);
GO

CREATE NONCLUSTERED INDEX IX_Restaurant_ChefID
ON dbo.Restaurant (ChefID);
GO

-- ================================
-- Create User-defined Table Type
-- ================================
USE [Chef]
GO

-- Create the data type
CREATE TYPE RegistryModel AS TABLE 
(
	FirstName NVARCHAR(30),
	LastName NVARCHAR(50),	
	ChefNumber NVARCHAR(50),
	[Name] NVARCHAR(20),  --Restaurant Name	
	StreetAddress NVARCHAR(50),
	CityTownVillage NVARCHAR(50),
	PostalZipCode NVARCHAR(50),
	StateProvinceRegion NVARCHAR(50),
	Number NVARCHAR(20) --Restaurant Number
)
GO
USE [Chef]
GO
/****** Object:  StoredProcedure [dbo].[CreateChef]    Script Date: 8/18/2025 11:10:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Eric T Van Helden
-- Create date: 
-- Description:	Create Chef into mutiple tables with Parent - Child relationships
--              using TSQL
-- =============================================
CREATE OR ALTER PROCEDURE CreateChef 

	@RegModel RegistryModel READONLY
AS
BEGIN

DECLARE @ChefID INT;
DECLARE @RestaurantID INT;
DECLARE @AddressID INT;
DECLARE @PhoneID INT;

	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRANSACTION;


			INSERT INTO Chefs (FirstName, LastName, ChefNumber)
			SELECT FirstName, LastName, ChefNumber
			FROM @RegModel;

			SET @ChefID = SCOPE_IDENTITY();

			INSERT INTO Restaurant ([Name], ChefID)
			SELECT [Name], @ChefID
			FROM @RegModel;

			SET @RestaurantID = SCOPE_IDENTITY();

			INSERT INTO [Address] (StreetAddress, CityTownVillage, PostalZipCode, StateProvinceRegion, ChefID, RestaurantID)
			SELECT StreetAddress, CityTownVillage, PostalZipCode, StateProvinceRegion, @ChefID, @RestaurantID
			from @RegModel;

			SET @AddressID = SCOPE_IDENTITY();

			INSERT INTO Phone (Number, ChefID, RestaurantID)
			SELECT [Number], @ChefID, @RestaurantID
			FROM @RegModel;

			SET @PhoneID = SCOPE_IDENTITY();

			UPDATE Restaurant
			SET PhoneID = @PhoneID, AddressID = @AddressID
			WHERE ChefID = @ChefID;

		COMMIT TRANSACTION;

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION

	END CATCH;

    
END

GO

USE [Chef]
GO
/****** Object:  StoredProcedure [dbo].[GetChef]    Script Date: 8/18/2025 5:17:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Eric T Van Helden
-- Create date: 
-- Description:	Get Chef By ID and return in User Defined Table using TSQL
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[GetChef] 

	 @ChefID INT
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @RegModel dbo.RegistryModel;;

	BEGIN TRY

		BEGIN TRANSACTION;

			INSERT INTO @RegModel(
				FirstName,
				LastName,	
				ChefNumber,
				[Name],
				StreetAddress,
				CityTownVillage,
				PostalZipCode,
				StateProvinceRegion,
				Number) 
			SELECT
					c.FirstName,
					c.LastName,
					c.ChefNumber,
					r.[Name],
					a.StreetAddress,
					a.CityTownVillage,
					a.PostalZipCode,
					a.StateProvinceRegion,
					p.Number
			FROM	Chefs AS c
			JOIN	Restaurant AS r ON r.ChefID = c.ID
			JOIN	[Address] AS a ON a.ChefID = c.ID
			JOIN	Phone AS p ON p.ChefID = c.ID
			WHERE c.ID = @ChefID
					AND r.ChefID = @ChefID
					AND a.ChefID = @ChefID
					AND p.ChefID = @ChefID;

			SELECT * FROM @RegModel;

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH;    
END

GO


USE [Chef]
GO
/****** Object:  StoredProcedure [dbo].[UpdateChef]    Script Date: 8/18/2025 5:17:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Eric T Van Helden
-- Create date: 
-- Description:	Update Chef By ID with passing in User Defined Table using TSQL
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[UpdateChef] 
	 @RegModel RegistryModel READONLY,
	 @ChefID INT
AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRY

			UPDATE	P
			SET		P.Number = R.Number
			FROM Phone P
			INNER JOIN @RegModel R ON P.ChefID = @ChefID;

			UPDATE	RE 
			SET		RE.[Name] = R.[Name]
			FROM Restaurant RE
			INNER JOIN @RegModel R ON RE.ChefID = @ChefID;

			UPDATE	A
			SET		A.StreetAddress = R.StreetAddress,	
					A.CityTownVillage = R.CityTownVillage, 
					A.PostalZipCode = R.PostalZipCode, 
					A.StateProvinceRegion = R.StateProvinceRegion
			FROM [Address] A
			INNER JOIN @RegModel R ON A.ChefID = @ChefID;

			UPDATE C
			SET 
					C.FirstName = R.FirstName,
					C.LastName = R.LastName,
					C.ChefNumber = R.ChefNumber
			FROM Chefs C
			INNER JOIN @RegModel R ON C.ID = @ChefID;

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH;    
END



GO

USE [Chef]
GO
/****** Object:  StoredProcedure [dbo].[DeleteChef]    Script Date: 8/19/2025 2:42:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Eric T Van Helden
-- Create date: 
-- Description:	Delete Chef By ID from all tables
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[DeleteChef] 

	 @ChefID INT
AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRANSACTION;

			-- Delete from child tables
			DELETE FROM Phone WHERE ChefID = @ChefID;
			DELETE FROM Restaurant WHERE ChefID = @ChefID;
			DELETE FROM Address WHERE ChefID = @ChefID;

			-- Delete from parent table
			DELETE FROM Chefs WHERE ID = @ChefID;
			
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH;    
END


GO

