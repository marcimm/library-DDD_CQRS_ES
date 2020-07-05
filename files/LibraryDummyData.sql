USE [TheosLibrary]
GO

INSERT INTO [dbo].[Authors]([Id],[IsDeleted],[Name],[BirthDate],[Nationality])
     VALUES ('e0fcd7b8-4bff-441a-ac03-fb5eb3cfe6b7', 0, 'Paul Rabbit', GETDATE(), 'Brazilian')
INSERT INTO [dbo].[Authors]([Id],[IsDeleted],[Name],[BirthDate],[Nationality])
     VALUES ('bec99151-3568-46a7-922a-ce2a4ebc5b96', 0, 'Eric Evans', GETDATE(), 'American')
INSERT INTO [dbo].[Authors]([Id],[IsDeleted],[Name],[BirthDate],[Nationality])
     VALUES ('999beeec-1fdb-4d5e-aa17-4891dee36164', 0, 'Jose Macoratti', GETDATE(), 'Brazilian')


INSERT INTO [dbo].[Categories]([Id],[IsDeleted],[Code],[CategoryName])
     VALUES('114ea14b-1663-4b1d-b85c-caa21b55b861', 0, '1001', 'Categoria 01')
INSERT INTO [dbo].[Categories]([Id],[IsDeleted],[Code],[CategoryName])
     VALUES('734aae73-512f-4bfe-8273-0e0a5c7cf261', 0, '1001', 'Categoria 01')
INSERT INTO [dbo].[Categories]([Id],[IsDeleted],[Code],[CategoryName])
     VALUES('8e328fe9-e866-482b-b8ef-94a6080b8671', 0, '1001', 'Categoria 01')


INSERT INTO [dbo].[Publishers]([Id],[IsDeleted],[Name],[Phone],[Address])
     VALUES ('a8c72841-e6e1-4d6f-8c11-bb75ac812d67', 0, 'Casa do Código', '789-654', 'Address 456')		   
INSERT INTO [dbo].[Publishers]([Id],[IsDeleted],[Name],[Phone],[Address])
     VALUES ('95a3bf3f-5cbe-4d43-8964-d12ada9f7abd', 0, 'Novatec', '445-6789', 'Address 445')		   
INSERT INTO [dbo].[Publishers]([Id],[IsDeleted],[Name],[Phone],[Address])
     VALUES ('851878f2-c5a1-4861-a0dc-c4217efaf829', 0, 'Brasport', '639-741', 'Address 141')


INSERT INTO [dbo].[Books]([Id],[IsDeleted],[CategoryId],[PublisherId],[ISBN],[Title],[Year],[Language])
     VALUES('c0ccb243-651c-4ee5-a4e2-6de91acd686f', 0, '114ea14b-1663-4b1d-b85c-caa21b55b861', 'a8c72841-e6e1-4d6f-8c11-bb75ac812d67'
           'ISBN-x1-10','Livro 01',2020,'pt-br')

INSERT INTO [dbo].[Books]([Id],[IsDeleted],[CategoryId],[PublisherId],[ISBN],[Title],[Year],[Language])
     VALUES('ca04c8da-9a48-4143-8b70-6a1e9463c35e', 0, '734aae73-512f-4bfe-8273-0e0a5c7cf261', '95a3bf3f-5cbe-4d43-8964-d12ada9f7abd'
           'ISBN-x1-20','Livro 02',2020,'pt-br')

INSERT INTO [dbo].[Books]([Id],[IsDeleted],[CategoryId],[PublisherId],[ISBN],[Title],[Year],[Language])
     VALUES('e5436a44-a7b5-4780-a262-35f6eb193c9a', 0, '8e328fe9-e866-482b-b8ef-94a6080b8671', '851878f2-c5a1-4861-a0dc-c4217efaf829'
           'ISBN-x1-30','Livro 03',2020,'pt-br')


INSERT INTO [dbo].[Stocks]([Id],[IsDeleted],[BookId],[Edition],[Code])
     VALUES('50c7288d-f332-4e8f-b82f-2f54bc0996d8', 0, 'c0ccb243-651c-4ee5-a4e2-6de91acd686f', 1, 1000)

INSERT INTO [dbo].[Stocks]([Id],[IsDeleted],[BookId],[Edition],[Code])
     VALUES('41b5751b-38a4-4cb7-9d92-998743786b53', 0, 'ca04c8da-9a48-4143-8b70-6a1e9463c35e', 1, 1002)

INSERT INTO [dbo].[Stocks]([Id],[IsDeleted],[BookId],[Edition],[Code])
     VALUES('b9b81700-dbf8-41d0-88db-1237b222a94a', 0, 'e5436a44-a7b5-4780-a262-35f6eb193c9a', 1, 1004)
GO