# PloomesInterview
 Projeto Azure Functions C# com Banco Sql Server. Abaixo lista dos request e parametros disponiveis para utilzação:
 
 -- Ainda não publicado
 
 Manual de utilização API InterviewPloomes

Url Base:  -- Ainda não publicado

Cliente:

	REQUEST[GET] Listar clientes: ploomesinterview.azurewebsites.net/api/Clientes?code=PZR/k66gpfKkT3WTEvVpuPTOzwn3CXuWnpkqtmDg8gHay7g7uVz1Uw==
	
	RESPONSE:
			[
				{
					"CodigoDoCliente":1,
					"NomeCliente":"Davi Bertolli"
				},
				{
					"CodigoDoCliente":2,
					"NomeCliente":"Emerson Silva"
				},
				{
					"CodigoDoCliente":3,
					"NomeCliente":"David Luis"
				},
				{
					"CodigoDoCliente":4,
					"NomeCliente":"Michael Jackson"
				},
				{
					"CodigoDoCliente":5,
					"NomeCliente":"Obama Silva"
				},
				{	"CodigoDoCliente":6,
					"NomeCliente":"Cristiane Bertolli"
				}
			]	

	REQUEST [POST] Cadastrar clientes: https://ploomesinterview.azurewebsites.net/api/CadastraColaborador?code=VBXyRmUyv65QMeTsWSF7FKh8uIVGsUQZMsSwfWAnbUYC3lIK8kqFMw==
			Parameters 
			{		
				"NomeCliente" : ""
			}
	RESPONSE : "Cadastrado com sucesso"	
	
Colaborador:

	REQUEST [GET] Listar Colaboradores: https://ploomesinterview.azurewebsites.net/api/Colaboradores?code=hT9nAjJ8gaIzzCqW/KgEla5HpaIZs56Equtxdv/ZqKaJeaouWmnpuA==
	
	RESPONSE [
				{
					"IdColaborador": 1,
					"NomeColaborador": "André Rosa",
					"DataNascimento": "1995-01-01T00:00:00",
					"Idade": 25,
					"AtivoColaborador": true,
					"Genero": 1,
					"Departamento": {
						"IdDepartamento": 3,
						"DescricaoDepartamento": "Tecnologia",
						"AtivoDepartamento": true
					}
				}  
			]

	REQUEST [POST] Cadastrar Colaborador: https://ploomesinterview.azurewebsites.net/api/CadastraColaborador?code=VBXyRmUyv65QMeTsWSF7FKh8uIVGsUQZMsSwfWAnbUYC3lIK8kqFMw==
			Parameters
			{
				"NomeColaborador" : "",
				"DataNascimento" : "1990-01-01",
				"Genero" : 1, -- Vide Enum
				"Departamento" : {"IdDepartamento" : 1 -- Vide ids departamento}
			}
	RESPONSE :	- "Cadastrado com sucesso"
				- "Campo [NomeDoCampo] não informado"


Enums para facilitar o cadastro:

	Genero:
		MASCULINO = 1,
        FEMININO = 2,
        OUTROS = 3,
        PrefiroNaoInformar = 4


Ids da tabela departamento:
	1	= Saúde
	2	= Esportes
	3	= Tecnologia


