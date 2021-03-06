USE [master]
GO

/****** Object:  Database [Bakery]    Script Date: 14/11/2014 13:22:20 ******/
DROP DATABASE [Bakery]
GO

/****** Object:  Database [Bakery]    Script Date: 14/11/2014 13:22:20 ******/
CREATE DATABASE [Bakery]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Bakery', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Bakery.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Bakery_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Bakery_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [Bakery] SET COMPATIBILITY_LEVEL = 110
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Bakery].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [Bakery] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [Bakery] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [Bakery] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [Bakery] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [Bakery] SET ARITHABORT OFF 
GO

ALTER DATABASE [Bakery] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [Bakery] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [Bakery] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [Bakery] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [Bakery] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [Bakery] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [Bakery] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [Bakery] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [Bakery] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [Bakery] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [Bakery] SET  DISABLE_BROKER 
GO

ALTER DATABASE [Bakery] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [Bakery] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [Bakery] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [Bakery] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [Bakery] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [Bakery] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [Bakery] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [Bakery] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [Bakery] SET  MULTI_USER 
GO

ALTER DATABASE [Bakery] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [Bakery] SET DB_CHAINING OFF 
GO

ALTER DATABASE [Bakery] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [Bakery] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [Bakery] SET  READ_WRITE 
GO


