using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestãoCooperativaAgrícola.Models
{
    public class DistribuicaoLucro
    {
        public string Ano { get; private set; }
        public double ValorRecebido { get; private set; }

        public DistribuicaoLucro(string ano, double valorRecebido)
        {
            Ano = ano;
            ValorRecebido = valorRecebido;
        }
    }
}
