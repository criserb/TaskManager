CREATE DATABASE TaskManager

GO

USE TaskManager

GO

CREATE TABLE [dbo].[Tasks](
	[Id] [int] NOT NULL,
	[Content] [nchar](1000) NULL,
	[Priority] [nchar](10) NULL,
	[Term] [nchar](10) NULL,
	[Status] [nchar](15) NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE PROCEDURE [dbo].[addRecord]
	@pId int,
	@pStatus varchar(20),
	@pPriority varchar(20),
	@pTerm VARCHAR(20),
	@pContent varchar(1000)
AS
	INSERT INTO Tasks (Id, Status, Priority, Term, Content) VALUES (@pId, @pStatus,@pPriority,@pTerm,@pContent);

GO

CREATE PROCEDURE [dbo].[updateRecord]
	@pID int,
	@pStatus varchar(20),
	@pPriority varchar(20),
	@pTerm VARCHAR(20),
	@pContent varchar(1000)
AS
	UPDATE Tasks
	SET Status = @pStatus, Content= @pContent, Priority= @pPriority, Term = @pTerm
	WHERE id = @pID;

GO

CREATE PROCEDURE [dbo].[deleteRecord]
	@pID int
AS
	DELETE Tasks
	WHERE id = @pID;
GO