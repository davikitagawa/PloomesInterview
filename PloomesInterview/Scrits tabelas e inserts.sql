CREATE TABLE [dbo].[tb_Genero](
	idGenero INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	descricaoGenero VARCHAR(50) NOT NULL,
	ativoGenero BIT DEFAULT 1 NOT NULL
)

INSERT INTO [dbo].[tb_Genero] (descricaoGenero) VALUES('Masculino'), ('FEMININO'), ('OUTROS'), ('Prefiro não informar')

CREATE TABLE [dbo].[tb_Departamento]( --departamento é a area de vendas do colaborador, exemplo (moda, itens esportivos etc)
	idDepartamento INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	descricaoDepartamento VARCHAR (100) NOT NULL,
	dataCriacaoDepartamento DATETIME DEFAULT GETDATE(),
	ativoDepartamento BIT DEFAULT 1
) 

INSERT INTO [dbo].[tb_Departamento] (descricaoDepartamento) VALUES ('Saúde'), ('Esportes'), ('Tecnologia')


CREATE TABLE [dbo].[tb_Colaborador]( -- colaborador representa o vendedor no contexto da base
	idColaborador INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	nomeColaborador VARCHAR(100) NOT NULL,
	dataCriacaoColaborador DATETIME DEFAULT GETDATE(),
	dataNascimento DATETIME NOT NULL,
	idadeColaborador as DATEDIFF(YEAR, dataNascimento, GETDATE()),
	ativoColaborador BIT DEFAULT 1 NOT NULL,
	idGenero INT NOT NULL FOREIGN KEY REFERENCES tb_Genero(idGenero),
	idDepartamento INT NOT NULL FOREIGN KEY REFERENCES tb_Departamento(idDepartamento),
); 

INSERT INTO [dbo].[tb_Colaborador](nomeColaborador, dataNascimento, idGenero, idDepartamento) VALUES ('André Rosa', '1995-01-01', 1, 3)

CREATE TABLE [dbo].[tb_TipoVenda]( -- tipos de venda exemplo (boleto, cartão de crédito etc) *levando em conta que o sistema é de e-commerce*
	idTipoVenda INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	descricaoTipoVenda VARCHAR(50) NOT NULL,
	ativoTipoVenda BIT DEFAULT 1 NOT NULL
)

INSERT INTO [dbo].[tb_TipoVenda] (descricaoTipoVenda) VALUES ('Cartão de Crédito'), ('Boleto')

CREATE TABLE [dbo].[tb_Cliente]( -- não coloquei muitas colunas pois o foco é ter informações sobre vendas e não clientes
	idCliente INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	NomeCliente VARCHAR(50) NOT NULL
)

INSERT INTO [dbo].[tb_Cliente] (NomeCliente) VALUES ('Davi Bertolli');

CREATE TABLE [dbo].[tb_Venda](
	idVenda INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	dataVenda DATETIME DEFAULT GETDATE(),
	idTipoVenda INT NOT NULL FOREIGN KEY REFERENCES tb_TipoVenda(idTipoVenda),
	idColaborador INT NOT NULL FOREIGN KEY REFERENCES tb_Colaborador(idColaborador),
	idCliente INT NOT NULL FOREIGN KEY REFERENCES tb_Cliente(idCliente)
)

CREATE TABLE [dbo].[tb_Produto](
	idProduto INT NOT NULL PRIMARY KEY,
	DescricaoProduto VARCHAR(100),
	precoUnitarioProduto SMALLMONEY,
	idDepartamento INT NOT NULL FOREIGN KEY REFERENCES tb_Departamento(idDepartamento)
)

CREATE TABLE [dbo].[tb_ProdutoVendas](
	idVenda INT NOT NULL FOREIGN KEY REFERENCES tb_Venda(idVenda),
	idProduto INT NOT NULL FOREIGN KEY REFERENCES tb_Produto(idProduto)
)




