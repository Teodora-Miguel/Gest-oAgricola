using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestãoCooperativaAgrícola.Models
{
    public class Cooperativista
    {
        public string NumeroSocio { get; private set; }
        public string Nome { get; private set; }
        public double QuotaParticipacao { get; private set; }
        private readonly List<DistribuicaoLucro> _historicoLucros = new List<DistribuicaoLucro>();

        public Cooperativista(string numeroSocio, string nome, double quota)
        {
            NumeroSocio = numeroSocio;
            Nome = nome;
            QuotaParticipacao = quota;
        }

        public void AdicionarLucro(string ano, double valor)
        {
            _historicoLucros.Add(new DistribuicaoLucro(ano, valor));
        }

        public List<DistribuicaoLucro> ObterHistoricoLucros()
        {
            return new List<DistribuicaoLucro>(_historicoLucros);
        }
    }
}
