using System;
using System.Collections.Generic;
using SistemaGestãoCooperativaAgrícola.Interfaces;
using SistemaGestãoCooperativaAgrícola.Models;
using SistemaGestãoCooperativaAgrícola.Exceptions;

namespace SistemaGestãoCooperativaAgrícola.Repositories
{
    public class GestorCooperativa : ICalculavelLucro
    {
        private readonly List<Cooperativista> _socios = new List<Cooperativista>();
        private readonly List<ProdutoAgricola> _produtos = new List<ProdutoAgricola>();
        private readonly List<Entrega> _todasEntregas = new List<Entrega>();
        private bool _periodoEncerrado = false;

        public void AdicionarCooperativista(Cooperativista socio)
        {
            _socios.Add(socio);
        }

        public void AdicionarProduto(ProdutoAgricola produto)
        {
            _produtos.Add(produto);
        }

        public void RegistarEntrega(string numSocio, string codProduto, double qtd, EpocaAgricola epoca)
        {
            if (_periodoEncerrado)
                throw new PeriodoEncerradoException("Operação Recusada: O período de movimentos desta época já foi encerrado.");

            Cooperativista socioEncontrado = null;
            foreach (var s in _socios)
            {
                if (s.NumeroSocio.Equals(numSocio)) { socioEncontrado = s; break; }
            }

            if (socioEncontrado == null)
                throw new CooperativistaNaoEncontradoException("Erro: O sócio número " + numSocio + " não existe no registo.");

            ProdutoAgricola prodEncontrado = null;
            foreach (var p in _produtos)
            {
                if (p.Codigo.Equals(codProduto.ToUpper())) { prodEncontrado = p; break; }
            }

            if (prodEncontrado == null)
                prodEncontrado = new ProdutoAgricola(codProduto, "Produto Genérico", 150.0);

            Entrega novaEntrega = new Entrega(socioEncontrado, prodEncontrado, qtd, epoca);
            _todasEntregas.Add(novaEntrega);
        }

        public void ProcessarEGerarDistribuiçãoLucro(string ano, double lucroTotalDaCooperativa)
        {
            foreach (var socio in _socios)
            {
                double parcelaDoLucro = lucroTotalDaCooperativa * (socio.QuotaParticipacao / 100.0);
                socio.AdicionarLucro(ano, parcelaDoLucro);
            }
            _periodoEncerrado = true;
        }

        public List<Cooperativista> ListarSocios()
        {
            return _socios;
        }
    }
}