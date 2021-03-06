USE [master]
GO
/****** Object:  Database [EnjoyLearn]    Script Date: 27.05.2013 8:12:37 ******/
CREATE DATABASE [EnjoyLearn]
GO

USE [EnjoyLearn]
GO
/****** Object:  Table [dbo].[LearnWord]    Script Date: 27.05.2013 8:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LearnWord](
	[Id] [uniqueidentifier] NOT NULL,
	[english] [char](200) NULL,
	[translate] [char](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[LearnWord] ([Id], [english], [translate]) VALUES (N'68c43fbf-2782-420d-a84c-019bf54a19ad', N'instance                                                                                                                                                                                                ', N'экземпляр                                                                                                                                                                                               ')
INSERT [dbo].[LearnWord] ([Id], [english], [translate]) VALUES (N'b2afaf33-a99e-42ac-a688-0ca68eba84fb', N'sterling                                                                                                                                                                                                ', N'безупречный                                                                                                                                                                                             ')
INSERT [dbo].[LearnWord] ([Id], [english], [translate]) VALUES (N'b29d26fd-babc-47f6-a9ab-2d2efa1b0154', N'proper place                                                                                                                                                                                            ', N'нужном месте                                                                                                                                                                                            ')
INSERT [dbo].[LearnWord] ([Id], [english], [translate]) VALUES (N'1038f340-fe60-4c09-abea-b010d32f73d6', N'encapsulate                                                                                                                                                                                             ', N'внедрить                                                                                                                                                                                                ')
INSERT [dbo].[LearnWord] ([Id], [english], [translate]) VALUES (N'81c718c3-9c30-4899-86a0-e92810650c73', N'hoax                                                                                                                                                                                                    ', N'обман                                                                                                                                                                                                   ')
ALTER TABLE [dbo].[LearnWord] ADD  DEFAULT (newid()) FOR [Id]
GO
USE [master]
GO
ALTER DATABASE [EnjoyLearn] SET  READ_WRITE 
GO
