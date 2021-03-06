USE [master]
GO
/****** Object:  Database [PointOfSale]    Script Date: 5/2/2016 4:39:24 PM ******/
CREATE DATABASE [PointOfSale]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PointOfSale', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.ADMIN\MSSQL\DATA\PointOfSale.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PointOfSale_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.ADMIN\MSSQL\DATA\PointOfSale_log.ldf' , SIZE = 5184KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [PointOfSale] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PointOfSale].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PointOfSale] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PointOfSale] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PointOfSale] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PointOfSale] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PointOfSale] SET ARITHABORT OFF 
GO
ALTER DATABASE [PointOfSale] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PointOfSale] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [PointOfSale] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PointOfSale] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PointOfSale] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PointOfSale] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PointOfSale] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PointOfSale] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PointOfSale] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PointOfSale] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PointOfSale] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PointOfSale] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PointOfSale] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PointOfSale] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PointOfSale] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PointOfSale] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PointOfSale] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PointOfSale] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PointOfSale] SET RECOVERY FULL 
GO
ALTER DATABASE [PointOfSale] SET  MULTI_USER 
GO
ALTER DATABASE [PointOfSale] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PointOfSale] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PointOfSale] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PointOfSale] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'PointOfSale', N'ON'
GO
USE [PointOfSale]
GO
/****** Object:  StoredProcedure [dbo].[spBranchGet]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spBranchGet]
as
begin
select * from Branch
end

GO
/****** Object:  StoredProcedure [dbo].[spBranchGetById]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spBranchGetById]
@BranchId int
as
begin
select * from Branch where BranchId=@BranchId
end

GO
/****** Object:  StoredProcedure [dbo].[spGetDataByCurrentDate]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc  [dbo].[spGetDataByCurrentDate]

@BranchId int ,
@StartDate date  
as 
begin	  
declare @dtt date = cast (@StartDate as date);
select ProductName, SUM(TotalUnits)as TotalUnits , SUM(TotalPrice) as TotalPrice
 from DailyReport 
where  
 BranchId =@BranchId  and  cast ([CreatedDate] as date) = @dtt  group by ProductName
end

GO
/****** Object:  StoredProcedure [dbo].[spGetDataByDate]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc  [dbo].[spGetDataByDate]

@BranchId int ,
@StartDate date ,
@EndDate date 
as 
begin	 
select ProductName, SUM(TotalUnits)as TotalUnits , SUM(TotalPrice) as TotalPrice
 from DailyReport 
where  
 BranchId =@BranchId  and  CreatedDate between @StartDate and @EndDate group by ProductName
end

GO
/****** Object:  StoredProcedure [dbo].[spGetRecietDataById]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[spGetRecietDataById]
@SaleId int
as
begin
select * from RecietData where SaleId=@SaleId
end

GO
/****** Object:  StoredProcedure [dbo].[spGetRecietDataTest]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spGetRecietDataTest]

as
begin
select * from RecietData 
end

GO
/****** Object:  StoredProcedure [dbo].[spProductDeleteById]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spProductDeleteById]
@ProductId int
as
begin
delete from Product where ProductId=@ProductId
end

GO
/****** Object:  StoredProcedure [dbo].[spProductDeleteCheckById]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[spProductDeleteCheckById]
@ProductId int
as
begin
select ProductId from SaleDetail where ProductId=@ProductId
end

GO
/****** Object:  StoredProcedure [dbo].[spProductGet]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spProductGet]
as
begin
select * from Product
end

GO
/****** Object:  StoredProcedure [dbo].[spProductGetById]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spProductGetById]
@ProductId int
as
begin
select * from Product where ProductId=@ProductId
end

GO
/****** Object:  StoredProcedure [dbo].[spProductGetList]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spProductGetList]
as
begin
select * from Product where ProductId>1
end

GO
/****** Object:  StoredProcedure [dbo].[spProductInsert]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spProductInsert]
@ProductName nvarchar(50),
@UnitPrice decimal(18, 2)
as
begin
insert into Product (ProductName,UnitPrice) values (@ProductName,@UnitPrice)
end

GO
/****** Object:  StoredProcedure [dbo].[spProductPriceGetById]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spProductPriceGetById]
@ProductId int
as
begin
select UnitPrice from Product where ProductId=@ProductId
end

GO
/****** Object:  StoredProcedure [dbo].[spProductUpdate]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spProductUpdate]
@ProductId int,
@ProductName nvarchar(50),
@UnitPrice decimal(18, 2)
as
begin
update Product set
 ProductName=@ProductName,UnitPrice=@UnitPrice 
 where
 ProductId=@ProductId
end

GO
/****** Object:  StoredProcedure [dbo].[spRecentSaleGetById]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[spRecentSaleGetById]
@Datetime datetime
as
begin
select * from Sale  where CAST(CreatedDate as DATE)=@Datetime
end

GO
/****** Object:  StoredProcedure [dbo].[spRecipeDelById]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spRecipeDelById]  
@RecipeId int  
as
begin
 
delete from RecipeDef where RecipeId=@RecipeId ;
delete from RecipeDetail where RecipeId=@RecipeId ;
 
end

GO
/****** Object:  StoredProcedure [dbo].[spRecipeGet]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spRecipeGet]
as
begin
select * from RecipeDef
end

GO
/****** Object:  StoredProcedure [dbo].[spRecipeGetSaleList]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spRecipeGetSaleList]
@RecipeId int
as
begin
select 
RecipeDetail.ProductId,
Product.ProductName,
Product.UnitPrice,
RecipeDef.RecipeId
from RecipeDef
inner join RecipeDetail
on RecipeDef.RecipeId=RecipeDetail.RecipeId
inner join Product
on RecipeDetail.ProductId=Product.ProductId
where RecipeDef.RecipeId=@RecipeId

end

GO
/****** Object:  StoredProcedure [dbo].[spRecipeInsert]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spRecipeInsert] 
 
@RecipeName	nvarchar(100)

as
begin
insert into RecipeDef 
(
RecipeName
) 
values  
(
@RecipeName
)

select ident_current('RecipeDef')
end

GO
/****** Object:  StoredProcedure [dbo].[spSaleCheckEdit]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[spSaleCheckEdit]
@SaleId int
as
begin 
select SaleId from Sale where SaleId =@SaleId and DATEDIFF(MINUTE,CreatedDate,getdate())<30
end

GO
/****** Object:  StoredProcedure [dbo].[spSaleDeleteById]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spSaleDeleteById]
@SaleId int
as
begin
select * from Sale where SaleId=@SaleId
end

GO
/****** Object:  StoredProcedure [dbo].[spSaleDetailDelByIdEdit]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[spSaleDetailDelByIdEdit]  
@SaleDetailId int  
as
begin

declare @SalId int= (select SaleId from SaleDetail where SaleDetailId=@SaleDetailId);
delete from SaleDetail where SaleDetailId=@SaleDetailId ;
update Sale set NetSale=(select  SUM(CONVERT(money,TotalPrice)) from SaleDetail
where SaleId=@SalId)
 where
  SaleId=@SalId ;
select * from SaleDetail where SaleId=@SalId;
 
end

GO
/****** Object:  StoredProcedure [dbo].[spSaleDetailDeleteById]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spSaleDetailDeleteById]
@SaleDetailId int
as
begin
select * from SaleDetail where SaleDetailId=@SaleDetailId
end

GO
/****** Object:  StoredProcedure [dbo].[spSaleDetailGet]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spSaleDetailGet]
as
begin
select * from SaleDetail
end

GO
/****** Object:  StoredProcedure [dbo].[spSaleDetailGetById]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spSaleDetailGetById]
@SaleDetailId int
as
begin
select * from SaleDetail where SaleDetailId=@SaleDetailId
end

GO
/****** Object:  StoredProcedure [dbo].[spSaleDetailGetByIdEdit]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[spSaleDetailGetByIdEdit] 
 
@SaleId int
as 
begin
select SaleDetail.SaleDetailId, SaleDetail.SaleId,SaleDetail.ProductId,SaleDetail.UnitPrice,
SaleDetail.TotalUnits,SaleDetail.TotalPrice,Product.ProductName
from SaleDetail
inner join Product
on SaleDetail.ProductId=Product.ProductId
where SaleId=@SaleId and DATEDIFF(MINUTE,CreatedDate,getdate())<30
end

GO
/****** Object:  StoredProcedure [dbo].[spSaleDetailInsert]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spSaleDetailInsert]
@SaleId	int	,
@ProductId	int,
@UnitPrice	decimal(18, 2)	,
@TotalUnits	decimal(18, 2)	,
@TotalPrice	decimal(18, 2)	,
@CreatedDate	datetime	 
as
begin
insert into SaleDetail
(SaleId,ProductId,UnitPrice,TotalUnits,TotalPrice,CreatedDate)
values
(@SaleId,@ProductId,@UnitPrice,@TotalUnits,@TotalPrice,@CreatedDate)
end

GO
/****** Object:  StoredProcedure [dbo].[spSaleDetailUpdate]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spSaleDetailUpdate]
@SaleDetailId	int	,
@SaleId	int	,
@ProductId	int,
@UnitPrice	decimal(18, 2)	,
@TotalUnits	decimal(18, 2)	,
@TotalPrice	decimal(18, 2)	,
@CreatedDate	datetime	 
as
begin
update SaleDetail set
SaleId=@SaleId,
ProductId=@ProductId,
UnitPrice=@UnitPrice,
TotalUnits=@TotalUnits,
TotalPrice=@TotalPrice,
CreatedDate=@CreatedDate
where SaleDetailId=@SaleDetailId
end

GO
/****** Object:  StoredProcedure [dbo].[spSaleGet]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spSaleGet]
as
begin
select * from Sale
end

GO
/****** Object:  StoredProcedure [dbo].[spSaleGetById]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spSaleGetById]
@SaleId int
as
begin
select * from Sale where SaleId=@SaleId
end

GO
/****** Object:  StoredProcedure [dbo].[spSaleInsert]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[spSaleInsert] 
@TokenId int,
@TotalSale	decimal(18, 2),
@Discount	decimal(18, 2),
@NetSale	decimal(18, 2),
@CreatedDate	datetime,
@BranchId	int	, 
@CashRecieved	decimal(18, 2),
@CashReturn	decimal(18, 2)

as
begin
insert into Sale 
(
TokenId,
TotalSale,
Discount,
NetSale,
CreatedDate,
BranchId,
CashRecieved,
CashReturn
) 
values  
(
@TokenId,
@TotalSale,
@Discount,
@NetSale,
@CreatedDate,
@BranchId,
@CashRecieved,
@CashReturn
)

select ident_current('Sale')
end

GO
/****** Object:  StoredProcedure [dbo].[spSaleInsertEdit]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[spSaleInsertEdit] 
@SaleId int,
@TotalSale	decimal(18, 2),
@Discount	decimal(18, 2),
@NetSale	decimal(18, 2),
@CreatedDate	datetime,
@BranchId	int	, 
@CashRecieved	decimal(18, 2),
@CashReturn	decimal(18, 2)

as
begin
insert into SaleDeleted 
(SaleId,TotalSale,Discount,NetSale,CreatedDate,BranchId,CashRecieved,CashReturn) 
(select * from Sale where SaleId=@SaleId);
delete from Sale where SaleId=@SaleId;
delete from SaleDetail where SaleId=@SaleId; 
end
begin

insert into Sale 
(
TotalSale,
Discount,
NetSale,
CreatedDate,
BranchId,
CashRecieved,
CashReturn
) 
values  
(
@TotalSale,
@Discount,
@NetSale,
@CreatedDate,
@BranchId,
@CashRecieved,
@CashReturn
)

select ident_current('Sale')
end

GO
/****** Object:  StoredProcedure [dbo].[spSaleReturn]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spSaleReturn]
@SaleId int
as
begin
insert into SaleDeleted 
(SaleId,TokenId,TotalSale,Discount,NetSale,CreatedDate,BranchId,CashRecieved,CashReturn) 
(select * from Sale where SaleId=@SaleId);
delete from Sale where SaleId=@SaleId;
delete from SaleDetail where SaleId=@SaleId;  
end
GO
/****** Object:  StoredProcedure [dbo].[spSaleUpdate]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spSaleUpdate]
@SaleId int, 
@TotalSale	decimal(18, 2),
@Discount	decimal(18, 2),
@NetSale	decimal(18, 2),
@CreatedDate	datetime,
@BranchId	int	 
as
begin
update sale set
TotalSale=@TotalSale,
Discount=@Discount,
NetSale=@NetSale,
CreatedDate=@CreatedDate,
BranchId=@BranchId
where SaleId=@SaleId
end

GO
/****** Object:  StoredProcedure [dbo].[spTokenIdSelect]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[spTokenIdSelect]
@DateNow date 
as
begin 
select max(TokenId) from Sale 
where @DateNow=CAST(CreatedDate as DATE)

end  



GO
/****** Object:  Table [dbo].[Branch]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branch](
	[BranchId] [int] IDENTITY(1,1) NOT NULL,
	[BranchName] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Mobile] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED 
(
	[BranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](50) NULL,
	[UnitPrice] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RecipeDef]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipeDef](
	[RecipeId] [int] IDENTITY(1,1) NOT NULL,
	[RecipeName] [nvarchar](50) NULL,
 CONSTRAINT [PK_RecipeDef] PRIMARY KEY CLUSTERED 
(
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RecipeDetail]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipeDetail](
	[RecipeDetailId] [int] IDENTITY(1,1) NOT NULL,
	[RecipeId] [int] NULL,
	[ProductId] [int] NULL,
 CONSTRAINT [PK_RecipeDetail] PRIMARY KEY CLUSTERED 
(
	[RecipeDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sale]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sale](
	[SaleId] [bigint] IDENTITY(1,1) NOT NULL,
	[TokenId] [nvarchar](50) NULL,
	[TotalSale] [decimal](18, 2) NULL,
	[Discount] [decimal](18, 2) NULL,
	[NetSale] [decimal](18, 2) NULL,
	[CreatedDate] [datetime] NULL,
	[BranchId] [int] NOT NULL,
	[CashRecieved] [decimal](18, 2) NULL,
	[CashReturn] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Sale_1] PRIMARY KEY CLUSTERED 
(
	[SaleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SaleDeleted]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleDeleted](
	[SaleId] [bigint] NULL,
	[TokenId] [nvarchar](50) NULL,
	[TotalSale] [decimal](18, 2) NULL,
	[Discount] [decimal](18, 2) NULL,
	[NetSale] [decimal](18, 2) NULL,
	[CreatedDate] [datetime] NULL,
	[BranchId] [int] NOT NULL,
	[CashRecieved] [decimal](18, 2) NULL,
	[CashReturn] [decimal](18, 2) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SaleDetail]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleDetail](
	[SaleDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[TokenId] [nvarchar](50) NULL,
	[SaleId] [bigint] NULL,
	[ProductId] [int] NULL,
	[UnitPrice] [decimal](18, 2) NULL,
	[TotalUnits] [decimal](18, 3) NULL,
	[TotalPrice] [decimal](18, 2) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_SaleDetail] PRIMARY KEY CLUSTERED 
(
	[SaleDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[DailyReport]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DailyReport]
AS
SELECT        dbo.Sale.SaleId, dbo.Sale.BranchId, dbo.SaleDetail.ProductId, dbo.SaleDetail.TotalUnits, dbo.SaleDetail.TotalPrice, dbo.SaleDetail.CreatedDate, dbo.Product.ProductName, dbo.Sale.TokenId
FROM            dbo.Sale INNER JOIN
                         dbo.SaleDetail ON dbo.Sale.SaleId = dbo.SaleDetail.SaleId INNER JOIN
                         dbo.Product ON dbo.SaleDetail.ProductId = dbo.Product.ProductId

GO
/****** Object:  View [dbo].[RecietData]    Script Date: 5/2/2016 4:39:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[RecietData]
AS
SELECT        dbo.SaleDetail.SaleId, dbo.SaleDetail.ProductId, dbo.SaleDetail.TotalUnits, dbo.SaleDetail.UnitPrice, dbo.SaleDetail.TotalPrice, dbo.Sale.TotalSale, dbo.Sale.Discount, dbo.Sale.NetSale, dbo.Sale.BranchId, 
                         dbo.Product.ProductName, dbo.Sale.CashRecieved, dbo.Sale.CashReturn, dbo.Sale.TokenId
FROM            dbo.SaleDetail INNER JOIN
                         dbo.Sale ON dbo.SaleDetail.SaleId = dbo.Sale.SaleId INNER JOIN
                         dbo.Product ON dbo.SaleDetail.ProductId = dbo.Product.ProductId

GO
ALTER TABLE [dbo].[SaleDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_SaleDetail_Sale] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[SaleDetail] CHECK CONSTRAINT [FK_SaleDetail_Sale]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Sale"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "SaleDetail"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "Product"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 251
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2610
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DailyReport'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DailyReport'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[21] 2[19] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "SaleDetail"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Sale"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Product"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 251
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 1245
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RecietData'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RecietData'
GO
USE [master]
GO
ALTER DATABASE [PointOfSale] SET  READ_WRITE 
GO
