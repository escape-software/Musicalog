USE [master]
GO

/****** SQL SERVER 2017 ******/

/****** Object:  Database [Musicalog]    Script Date: 25/11/2020 11:25:54 ******/
CREATE DATABASE [Musicalog]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Musicalog', FILENAME = N'C:\Program Files\Microsoft SQL Server\XXXXXXXXXXX\MSSQL\DATA\Musicalog.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Musicalog_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\XXXXXXXXXX\MSSQL\DATA\Musicalog_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Musicalog].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [Musicalog] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [Musicalog] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [Musicalog] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [Musicalog] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [Musicalog] SET ARITHABORT OFF 
GO

ALTER DATABASE [Musicalog] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [Musicalog] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [Musicalog] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [Musicalog] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [Musicalog] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [Musicalog] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [Musicalog] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [Musicalog] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [Musicalog] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [Musicalog] SET  DISABLE_BROKER 
GO

ALTER DATABASE [Musicalog] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [Musicalog] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [Musicalog] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [Musicalog] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [Musicalog] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [Musicalog] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [Musicalog] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [Musicalog] SET RECOVERY FULL 
GO

ALTER DATABASE [Musicalog] SET  MULTI_USER 
GO

ALTER DATABASE [Musicalog] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [Musicalog] SET DB_CHAINING OFF 
GO

ALTER DATABASE [Musicalog] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [Musicalog] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [Musicalog] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [Musicalog] SET QUERY_STORE = OFF
GO

ALTER DATABASE [Musicalog] SET  READ_WRITE 
GO


