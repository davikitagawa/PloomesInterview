using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Data.SqlClient;

namespace PloomesInterview
{
    public  class Function1
    {       

        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");            

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string cliente = Environment.GetEnvironmentVariable("DbConnectionString");

            try
            {
                using (SqlConnection connection = new SqlConnection(cliente))
                {
                    using (SqlCommand command = new SqlCommand())
                    {

                        connection.Open();

                        command.Connection = connection;
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "SELECT* FROM tb_Genero";
                        command.CommandTimeout = 0;                  

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            cliente = reader["descricaoGenero"].ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {

                throw;
            }
          
            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {cliente}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
