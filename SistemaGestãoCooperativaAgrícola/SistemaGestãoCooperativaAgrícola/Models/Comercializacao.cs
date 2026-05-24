using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestãoCooperativaAgrícola.Models
{
    public class Comercializacao
    {
        // Agregação: Junta várias entregas independentes para vender no mercado externo
        private readonly List<Entrega> _entregasComercializadas = new List<Entrega>();
        public double PrecoVendaFinalPorQuilo { get; private set; }

        public Comercializacao(double precoVendaFinal)
        {
            PrecoVendaFinalPorQuilo = precoVendaFinal;
        }

        public void AssociarEntrega(Entrega entrega)
        {
            _entregasComercializadas.Add(entrega);
        }

        public double CalcularLucroTotalGerado()
        {
            double lucroTotal = 0;
            foreach (var e in _entregasComercializadas)
            {
                double custoMinimo = e.CalcularValorBruto();
                double valorVendaReal = e.QuantidadeQuilos * PrecoVendaFinalPorQuilo;
                lucroTotal += (valorVendaReal - custoMinimo);
            }
            return lucroTotal;
        }
    }
}
