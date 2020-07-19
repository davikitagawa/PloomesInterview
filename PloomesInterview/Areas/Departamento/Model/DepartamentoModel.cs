using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace PloomesInterview.Areas.Departamento.Model
{
    public class DepartamentoModel
    {

        public int IdDepartamento { get; set; }
        public string DescricaoDepartamento { get; set; }
        public bool AtivoDepartamento { get; set; }


        public async static Task<DepartamentoModel> BuscaDepartamento(int IdDepartamento)
        {

            DepartamentoModel departamento = null;

            string connecionString = Environment.GetEnvironmentVariable("DbConnectionString");

            using (SqlConnection connection = new SqlConnection(connecionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    connection.Open();

                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "[sp_buscaDepartamento]";
                    command.CommandTimeout = 0;

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        departamento = new DepartamentoModel
                        {
                            IdDepartamento = (int)reader["IdDepartamento"],
                            DescricaoDepartamento = reader["DescricaoDepartamento"].ToString(),
                            AtivoDepartamento = (bool)reader["AtivoDepartamento"]

                        };                        
                    }
                }
            }

            return departamento;

        }
    }
}
