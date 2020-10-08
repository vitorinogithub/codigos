using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi2.Helpers;
using WebApi2.Models;
using Xunit;

namespace WebApiJurosTestes
{
    public class TestesUnit
    {
       private readonly string urlLaunch = "https://localhost:44370/taxaJuros";
        [Theory]
        [InlineData(0.01)]
        public void TaxajurosErro(double juros)
        {
            double resultado = new WebApi1.Models.Taxa().Juros;
            Assert.Equal(juros, resultado);
        }

        [Fact]
        public async Task IntegracaoWeabJuros()
        {
            using (var client = new HttpClient())
            { 
                client.BaseAddress = new Uri(urlLaunch);
                using (var response = await client.GetAsync(urlLaunch))
                {
                     response.EnsureSuccessStatusCode();
                     response.StatusCode.Should().Be(HttpStatusCode.OK);
                }
            }
        }

        [Theory]
        [InlineData("105,10")]
        public async void CalcularJuros(string valorinicial)
        {
             int meses = 5;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlLaunch);
                using (var response = await client.GetAsync(urlLaunch))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        Taxa _taxa = JsonConvert.DeserializeObject<Taxa>(await response.Content.ReadAsStringAsync());
                        _taxa.valorinicial =Convert.ToDouble(100);
                        _taxa.meses = meses;
                        string resultado = Juros.Calcular(_taxa).ToString();
                        Assert.Equal(valorinicial,resultado);        
                    }
                    else
                    {
                        throw new InvalidCastException("Internal server error - APIWEBJUROS Fora do ar.");
                    }
                }
            }
        }
    }
}
