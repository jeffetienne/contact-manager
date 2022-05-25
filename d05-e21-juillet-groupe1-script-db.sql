USE MASTER

 

    GO
    DROP DATABASE IF EXISTS dbGestionnaire
    GO
    CREATE DATABASE dbGestionnaire
    GO

 

    --table contacts

 

    USE dbGestionnaire
    GO
    DROP TABLE IF EXISTS contacts
    GO

 

    CREATE TABLE [dbo].[contacts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nom] [varchar](25) NOT NULL,
	[prenom] [varchar](25) NOT NULL,
	[numeroGroupe] [int] NULL,
	[AdresseCourriel] [varchar](30) NULL,
	[Tel] [varchar](13) NOT NULL,
	[adresse] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_contacts_adresseCourriel] UNIQUE NONCLUSTERED 
(
	[AdresseCourriel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_contacts_tel] UNIQUE NONCLUSTERED 
(
	[Tel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[contacts]  WITH CHECK ADD  CONSTRAINT [C_Tel1] CHECK  (([Tel] like '([0-9][0-9][0-9])[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]'))
GO

ALTER TABLE [dbo].[contacts] CHECK CONSTRAINT [C_Tel1]

 
 --Table role
 USE [dbGestionnaire]

GO

CREATE TABLE [dbo].[role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nom] [varchar](20) NOT NULL,
 CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_role_nom] UNIQUE NONCLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

insert into role(nom) values('Utilisateur')
insert into role(nom) values('Gestionnaire')
insert into role(nom) values('Administrateur')

    
--table Usagers
    USE [dbGestionnaire]

GO

CREATE TABLE [dbo].[Usagers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[identifiant] [varchar](30) NOT NULL,
	[motDePasse] [nvarchar](10) NULL,
	[role] [int] NOT NULL,
 CONSTRAINT [PK__Usagers__3213E83FEFA36FC5] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Usagers_identifiant] UNIQUE NONCLUSTERED 
(
	[identifiant] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

	--Table de reference Groupe
USE [dbGestionnaire]

GO

CREATE TABLE [dbo].[groupe](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nom] [nvarchar](50) NULL,
 CONSTRAINT [PK_groupe] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

insert into groupe(nom) values('Famille')
insert into groupe(nom) values('Amis')
insert into groupe(nom) values('Collegue')

--TAble Message
USE [dbGestionnaire]
GO

CREATE TABLE [dbo].[message](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[destinataire] [int] NULL,
	[contenu] [nvarchar](max) NULL,
	[dateEnvoi] [datetime] NULL,
 CONSTRAINT [PK_message] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[message]  WITH CHECK ADD  CONSTRAINT [FK_message_contacts] FOREIGN KEY([destinataire])
REFERENCES [dbo].[contacts] ([id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[message] CHECK CONSTRAINT [FK_message_contacts]
GO

--Table Favori
USE [dbGestionnaire]
GO


CREATE TABLE [dbo].[Favori](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[contact] [int] NOT NULL,
 CONSTRAINT [PK_Favori] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Favori]  WITH CHECK ADD  CONSTRAINT [FK_Favori_contacts] FOREIGN KEY([contact])
REFERENCES [dbo].[contacts] ([id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Favori] CHECK CONSTRAINT [FK_Favori_contacts]
GO