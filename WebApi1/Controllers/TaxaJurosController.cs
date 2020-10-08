using Microsoft.AspNetCore.Mvc;
using WebApi1.Models;

namespace WebApi1.Controllers
{
    [ApiController]
    [Route("[controller]", Name = "taxaJuros")]
    public class TaxaJurosController : ControllerBase
    {
        /// <summary>
        /// Retorna Juros.
        /// </summary>
        /// <returns>Juros deve ser igual a 0.01</returns>
        /// <response code="200">Returna juros fixo</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult ObterJuros()
        {
            return Ok(new Taxa());       
        }
    }
}
