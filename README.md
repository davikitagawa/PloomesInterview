# PloomesInterview
 Projeto Azure Functions C# com Banco Sql Server. Abaixo lista dos request e parametros disponiveis para utilzação:
 
 -- Ainda não publicado
 
 Manual de utilização API InterviewPloomes

Url Base:  -- Ainda não publicado

Cliente:

	REQUEST[GET] Listar clientes: api/Clientes
	
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

	REQUEST [POST] Cadastrar clientes: api/CadastraCliente
			Parameters 
			{		
				"NomeCliente" : ""
			}
	RESPONSE : "Cadastrado com sucesso"	
	
Colaborador:

	REQUEST [GET] Listar Colaboradores: api/Colaboradores
	
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

	REQUEST [POST] Cadastrar Colaborador: api/CadastraColaborador
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


