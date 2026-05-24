using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestãoCooperativaAgrícola.Models
{
    public class ProdutoAgricola
    {
        public string Codigo { get; private set; }
        public string Nome { get; private set; }
        public double PrecoBasePorQuilo { get; private set; }

        public ProdutoAgricola(string codigo, string nome, double precoBase)
        {
            Codigo = codigo.ToUpper();
            Nome = nome;
            PrecoBasePorQuilo = precoBase;
        }
    }
}
