using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PloomesInterview.Areas.Cliente.Model;
using System.Collections.Generic;
using PloomesInterview.Utils.Enums;

namespace PloomesInterview.Areas.Cliente.Controller
{
    public class ClienteController
    {

        [FunctionName("Clientes")]
        public async Task<IActionResult> ListaClientes(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<ClienteModel> listaCliente = null;
            try
            {
                listaCliente = await ClienteModel.ListaClientes();
            }
            catch (Exception e)
            {

                log.LogError(e, "Erro ao buscar lista de clientes");

                return new OkObjectResult("Erro ao buscar lista de clientes");
            }            

            if(listaCliente.Count == 0)
            {
                return new OkObjectResult("Nenhum cliente cadastrado");
            }

            var retorno = JsonConvert.SerializeObject(listaCliente);

            return new OkObjectResult(retorno);
        }


        [FunctionName("CadastraCliente")]
        public async Task<IActionResult> CadastraCliente(
           [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
           ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            RetCode retorno = RetCode.Erro;
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            ClienteModel data = JsonConvert.DeserializeObject<ClienteModel>(requestBody);

            if (string.IsNullOrWhiteSpace(data.NomeCliente))
            {
                return new OkObjectResult("Nome não informado, cliente não cadastrado");
            }

            try
            {
                retorno = await ClienteModel.CadastraCliente(data.NomeCliente);

                if(retorno == RetCode.Erro)
                {
                    return new OkObjectResult("Erro ao cadastrar cliente");
                }


            }
            catch (Exception e)
            {

                log.LogError(e, "Erro ao cadastrar cliente");

                return new OkObjectResult("Erro ao cadastrar cliente");
            }


            return new OkObjectResult("Cliente cadastrado com sucesso");
        }
    }
}
