USE [Concessionaria];
GO

INSERT INTO [Vendedores]
VALUES
	('Joao', 1200.00),
	('Maria', 1333.99),
	('Bernardo', 999.99);

INSERT INTO [Proprietarios]
VALUES
	('11122233344', 'F', 'Joao das Couves', 'joao@email.com', '2000-01-01', 'Curitiba', 'PR', '123'),
	('99922233344', 'F', 'Jose Silva', 'jose@email.com', '1999-01-25', 'Sao Paulo', 'SP', '321'),
	('99988844455', 'J', 'Maria das Gracas', 'maria@email.com', '1985-01-01', 'Cascavel', 'PR', '111');

INSERT INTO [Telefones]
VALUES
	('11122233344', '41 91111-2222'),
	('11122233344', '42 4212-3221'),
	('99922233344', '5541933334444');

INSERT INTO [Veiculos]
VALUES
	('abc', 'Logan', 2008, 'vermelho', 34000, 12000 , '1.0', '11122233344'),
	('abcde', 'Palio', 2012, 'cinza', 24700, 10100, '1.0', '99922233344'),
	('wxyz', 'Kombi', 2000, 'branco', 29750, 12000, '1.5', '99988844455');

INSERT INTO [Acessorios]
VALUES
	('abc', 'Farol'),
	('abc', 'Radio'),
	('wxyz', 'Capa de assento');

INSERT INTO [Vendas]
VALUES
	(GETDATE(), 12000, 'abc', 1),
	(GETDATE(), 13000, 'abcde', 2),
	(GETDATE(), 10500, 'wxyz', 3);

SELECT * FROM [Telefones];
SELECT * FROM [Acessorios];
SELECT * FROM [Vendedores];
SELECT * FROM [Proprietarios];
SELECT * FROM [Veiculos];
SELECT * FROM [Vendas];
