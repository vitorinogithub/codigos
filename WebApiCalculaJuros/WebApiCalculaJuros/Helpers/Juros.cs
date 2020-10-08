using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi2.Models;

namespace WebApi2.Helpers
{
    public class Juros
    {
        public static decimal Calcular( Taxa taxa)
        {
            object juros_calculado = (Math.Pow(((taxa.valorinicial / 10) * (1 + taxa.Juros)), taxa.meses) / 1000);
            return Convert.ToDecimal(string.Format("{0:0.00}", juros_calculado));
        }
    }
}
