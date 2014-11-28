USE [Bakery]
GO

/****** Object:  Table [dbo].[Images]    Script Date: 14/11/2014 13:23:58 ******/
DROP TABLE [dbo].[Images]
GO

/****** Object:  Table [dbo].[Images]    Script Date: 14/11/2014 13:23:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Images](
	[Id] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Location] [nvarchar](250) NULL,
	[CommonName] [nvarchar](50) NULL,
	[Type] [int] NULL,
	[BakedOn] [datetime] NULL,
	[Provider] [int] NULL,
	[OS_Family] [int] NULL,
	[OS_Version] [nvarchar](50) NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


