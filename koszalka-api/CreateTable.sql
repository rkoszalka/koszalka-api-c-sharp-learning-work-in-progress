CREATE DATABASE csharpwebapi;

USE csharpwebapi;


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bike](
 [Id] [bigint] IDENTITY(1,1) NOT NULL,
 [Name] [nvarchar](max) NULL,
 [Description] [nvarchar](max) NULL,
 [Price] [nvarchar](max) NULL,
 [Model] [nvarchar](max) NULL,
 [Brand] [nvarchar](max) NULL,
 [CreatedDate] [datetime2](7) NOT NULL,
 [UpdatedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Bike] PRIMARY KEY CLUSTERED 
(
 [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO