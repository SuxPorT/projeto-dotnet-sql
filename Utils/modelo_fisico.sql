DROP DATABASE IF EXISTS [ConcessionariaModelo];
GO

CREATE DATABASE [ConcessionariaModelo];
GO

USE [ConcessionariaModelo];
GO

CREATE TABLE [Proprietario] (
	CpfCnpj VARCHAR(20) UNIQUE NOT NULL,
	IndicadorPessoa VARCHAR(1) NOT NULL,
	Nome VARCHAR(50) NOT NULL,
	Email VARCHAR(50) NOT NULL,
	DataNascimento DATETIME NOT NULL,
	Cidade VARCHAR(50),
	UF VARCHAR(40),
	CEP VARCHAR(10),

	CONSTRAINT PK_Proprietario PRIMARY KEY(CpfCnpj)
);

CREATE TABLE [Telefone] (
	TelefoneId INT IDENTITY(1, 1) NOT NULL,
	ProprietarioCpfCnpj VARCHAR(20) NOT NULL,
	Codigo VARCHAR(20) NOT NULL,

	CONSTRAINT PK_Telefone PRIMARY KEY(TelefoneId),
	CONSTRAINT FK_Telefone_ProprietarioCpfCnpj FOREIGN KEY(ProprietarioCpfCnpj)
	REFERENCES [Proprietario](CpfCnpj)
);

CREATE TABLE [Veiculo] (
	NumeroChassi VARCHAR(17) NOT NULL,
	Modelo VARCHAR(30) NOT NULL,
	Ano int NOT NULL,
	Cor VARCHAR(30),
	Valor MONEY NOT NULL,
	Quilometragem FLOAT NOT NULL,
	VersaoSistema VARCHAR(10),
	ProprietarioCpfCnpj VARCHAR(20) NOT NULL,

	CONSTRAINT PK_Veiculo PRIMARY KEY(NumeroChassi),
	CONSTRAINT FK_Veiculo_ProprietarioCpfCnpj FOREIGN KEY(ProprietarioCpfCnpj)
	REFERENCES Proprietario(CpfCnpj)
);

CREATE TABLE [Acessorio] (
	AcessorioId INT IDENTITY(1, 1) NOT NULL,
	VeiculoNumeroChassi VARCHAR(17) NOT NULL,
	Descricao VARCHAR(50) NOT NULL,

	CONSTRAINT PK_Acessorio PRIMARY KEY(AcessorioId),
	CONSTRAINT FK_Acessorio_VeiculoNumeroChassi FOREIGN KEY(VeiculoNumeroChassi)
	REFERENCES [Veiculo](NumeroChassi)
);


CREATE TABLE [Vendedor] (
	VendedorId INT IDENTITY(1, 1) NOT NULL,
	Nome VARCHAR(50) NOT NULL,
	SalarioBase MONEY NOT NULL,

	CONSTRAINT PK_Vendedor PRIMARY KEY(VendedorId)
);

CREATE TABLE [Venda] (
	VendaId INT IDENTITY(1, 1) NOT NULL,
	DataVenda DATETIME NOT NULL,
	ValorVenda MONEY NOT NULL,
	VeiculoNumeroChassi VARCHAR(17) NOT NULL,
	VendedorId INT NOT NULL,
	
	CONSTRAINT PK_Venda PRIMARY KEY(VendaId),
	CONSTRAINT FK_Venda_VeiculoNumeroChassi FOREIGN KEY(VeiculoNumeroChassi)
	REFERENCES [Veiculo](NumeroChassi),
	CONSTRAINT FK_Venda_VendedorId FOREIGN KEY(VendedorId)
	REFERENCES [Vendedor](VendedorId)
);
