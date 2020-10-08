using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi2.Helpers;
using WebApi2.Models;


namespace WebApi2.Controllers
{
    [ApiController]
    [Route("[controller]", Name = "calculaJuros")]
    public class CalculaJurosController : ControllerBase
    {
        /// <summary>
        /// Exibe url do codigo no GITHUB
        /// código em c# com AspNetCore
        /// </summary>      
        /// <returns>Objeto contendo url do GITHUB</returns>
        [HttpGet("/showMeTheCode")]
        public IActionResult showMeTheCode()
        {
            return Ok("https://github.com/vitorinogithub/codigos");
        }

        /// <summary>
        /// Ela faz um cálculo em memória, de juros compostos, 
        /// conforme abaixo: Valor Final = Valor Inicial * (1 + juros) ^ Tempo. Sem arredondamento
        /// </summary>
        /// <returns>Valor do Juros deve ser consultado na WEBAPI1.</returns>

        [HttpGet("/calculaJuros")]
        public async Task<IActionResult> CalcularJuros(double valorinicial, int meses)
        {
            string urlLaunch = "https://localhost:44370/taxaJuros";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlLaunch);
                using (var response = await client.GetAsync(urlLaunch))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        Taxa _taxa = JsonConvert.DeserializeObject<Taxa>(await response.Content.ReadAsStringAsync());
                        _taxa.valorinicial = valorinicial;
                        _taxa.meses = meses;
                        return Ok(Juros.Calcular(_taxa));
                    }
                    else
                    {
                        return StatusCode(500, "Internal server error - APIWEBJUROS Fora do ar.");
                    }
                }
            }
        }
    }   
}
