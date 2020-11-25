USE [Musicalog]
GO
/****** Object:  User [MusicalogUser]    Script Date: 25/11/2020 12:49:35 ******/
CREATE USER [MusicalogUser] FOR LOGIN [MusicalogUser] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [MusicalogUser]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [MusicalogUser]
GO
/****** Object:  Table [dbo].[Albums]    Script Date: 25/11/2020 12:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Albums](
	[AlbumID] [int] IDENTITY(1,1) NOT NULL,
	[AlbumName] [nvarchar](50) NOT NULL,
	[Artist] [nvarchar](50) NOT NULL,
	[AlbumLabel] [nvarchar](50) NOT NULL,
	[AlbumType] [int] NOT NULL,
	[Stock] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Albums] PRIMARY KEY CLUSTERED 
(
	[AlbumID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [Musicalog] SET  READ_WRITE 
GO
