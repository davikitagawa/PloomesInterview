using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using PloomesInterview.Areas.Colaborador.Model;
using PloomesInterview.Utils.Enums;
using PloomesInterview.Areas.Departamento.Model;

namespace PloomesInterview.Areas.Colaborador.Controller
{
    public class ColaboradoresController
    {

        [FunctionName("Colaboradores")]
        public async Task<IActionResult> ListaColaborares(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<ColaboradorModel> listaColaboradores = null;
            try
            {
                listaColaboradores = await ColaboradorModel.ListaColaborador();
            }
            catch (Exception e)
            {

                log.LogError(e, "Erro ao buscar lista de colaboradores");

                return new OkObjectResult("Erro ao buscar lista de colaboradores");
            }

            if (listaColaboradores.Count == 0)
            {
                return new OkObjectResult("Nenhum colaborador cadastrado");
            }

            var retorno = JsonConvert.SerializeObject(listaColaboradores);

            return new OkObjectResult(retorno);
        }

        [FunctionName("CadastraColaborador")]
        public async Task<IActionResult> CadastraColaborador(
         [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
         ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            RetCode retorno = RetCode.Erro;
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            ColaboradorModel data = JsonConvert.DeserializeObject<ColaboradorModel>(requestBody);

            if (string.IsNullOrWhiteSpace(data.NomeColaborador))
            {
                return new OkObjectResult("Nome não informado");
            }

            if(data.DataNascimento == null)
            {
                return new OkObjectResult("Data de nascimento não informada");
            }

            if(data.Genero == null)
            {
                return new OkObjectResult("Genero não informada");
            }

            try
            {
                data.Departamento = await DepartamentoModel.BuscaDepartamento(data.Departamento.IdDepartamento);
            }
            catch (Exception e)
            {
                return new OkObjectResult("Departamento não encontrado");
            }

            if (data.Departamento == null)
            {
                return new OkObjectResult("Departamento não encontrado");
            };

            try
            {
                retorno = await ColaboradorModel.CadastraColaborador(data);

                if (retorno == RetCode.Erro)
                {
                    return new OkObjectResult("Erro ao cadastrar colaborador");
                }


            }
            catch (Exception e)
            {

                log.LogError(e, "Erro ao cadastrar colaborador");

                return new OkObjectResult("Erro ao cadastrar colaborador");
            }


            return new OkObjectResult("Colaborador cadastrado com sucesso");
        }
    }
}
