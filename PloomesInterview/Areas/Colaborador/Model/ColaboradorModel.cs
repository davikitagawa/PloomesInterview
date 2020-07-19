using PloomesInterview.Areas.Departamento.Model;
using PloomesInterview.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PloomesInterview.Areas.Colaborador.Model
{
    public class ColaboradorModel
    {
        public int IdColaborador { get; set; }

        public string NomeColaborador { get; set; }

        public DateTime? DataNascimento { get; set; }

        public int Idade { get; set; }

        public bool AtivoColaborador { get; set; }

        public Genero? Genero { get; set; }

        public DepartamentoModel Departamento { get; set; }


        public async static Task<List<ColaboradorModel>> ListaColaborador()
        {
            // Buscando CLiente em base    
            List<ColaboradorModel> Listaclientes = new List<ColaboradorModel>();

            string connecionString = Environment.GetEnvironmentVariable("DbConnectionString");

            using (SqlConnection connection = new SqlConnection(connecionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    connection.Open();

                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "[sp_buscaColaboradores]";
                    command.CommandTimeout = 0;

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        var cliente = new ColaboradorModel
                        {
                            NomeColaborador = reader["NomeColaborador"].ToString(),
                            IdColaborador = (int)reader["IdColaborador"],
                            DataNascimento = (DateTime)reader["DataNascimento"],
                            AtivoColaborador = (bool)reader["AtivoColaborador"],
                            Idade = (int)reader["IdadeColaborador"],
                            Genero = (Genero)reader["GeneroColaborador"],
                            Departamento = new DepartamentoModel
                            {
                                IdDepartamento = (int)reader["IdDepartamento"],
                                DescricaoDepartamento = reader["DescricaoDepartamento"].ToString(),
                                AtivoDepartamento = (bool)reader["AtivoDepartamento"]                                
                            }
                        };

                        Listaclientes.Add(cliente);
                    }
                }
            }

            return Listaclientes;

        }

        public async static Task<RetCode> CadastraColaborador(ColaboradorModel colaborador)
        {
            string connecionString = Environment.GetEnvironmentVariable("DbConnectionString");

            RetCode retorno = RetCode.Erro;

            using (SqlConnection connection = new SqlConnection(connecionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    connection.Open();

                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "[sp_cadastraColaborador]";
                    command.CommandTimeout = 0;

                    command.Parameters.Add("@NomeColaborador", System.Data.SqlDbType.VarChar).Value = colaborador.NomeColaborador;
                    command.Parameters.Add("@DataNascimento", System.Data.SqlDbType.DateTime).Value = colaborador.DataNascimento;
                    command.Parameters.Add("@IdDepartamento", System.Data.SqlDbType.Int).Value = colaborador.Departamento.IdDepartamento;
                    command.Parameters.Add("@IdGenero", System.Data.SqlDbType.Int).Value = (int)colaborador.Genero;

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        retorno = (RetCode)reader["ret_code"];
                    }
                }
            }

            return retorno;
        }


    }
}
