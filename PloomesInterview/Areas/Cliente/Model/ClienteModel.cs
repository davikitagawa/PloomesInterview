using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PloomesInterview.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PloomesInterview.Areas.Cliente.Model
{
    public class ClienteModel
    {
        [JsonProperty("CodigoDoCliente")]
        public int IdCliente { get; set; }

        [JsonProperty("NomeCliente")]
        public string NomeCliente { get; set; }


        public async static Task<List<ClienteModel>> ListaClientes()
        {
            // Buscando CLiente em base    
            List<ClienteModel> Listaclientes = new List<ClienteModel>();

            string connecionString = Environment.GetEnvironmentVariable("DbConnectionString");

            using (SqlConnection connection = new SqlConnection(connecionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    connection.Open();

                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "[sp_buscaClientes]";
                    command.CommandTimeout = 0;

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        var cliente = new ClienteModel
                        {
                            NomeCliente = reader["NomeCliente"].ToString(),
                            IdCliente = (int)reader["IdCliente"]
                        };

                        Listaclientes.Add(cliente);
                    }
                }
            }

            return Listaclientes;
        }

        public async static Task<ClienteModel> BuscaCliente(int id)
        {
            string connecionString = Environment.GetEnvironmentVariable("DbConnectionString");

            ClienteModel cliente = null;
            using (SqlConnection connection = new SqlConnection(connecionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    connection.Open();

                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "[sp_buscaCliente]";
                    command.CommandTimeout = 0;

                    command.Parameters.Add("@IdCliente", System.Data.SqlDbType.Int).Value = id;

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        cliente = new ClienteModel
                        {
                            NomeCliente = reader["NomeCliente"].ToString(),
                            IdCliente = (int)reader["IdCliente"]
                        };
                    }
                }
            }

            return cliente;
        }

        public async static Task<RetCode> CadastraCliente(string nomeCliente)
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
                    command.CommandText = "[sp_cadastraCliente]";
                    command.CommandTimeout = 0;

                    command.Parameters.Add("@NomeCliente", System.Data.SqlDbType.VarChar).Value = nomeCliente;

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
