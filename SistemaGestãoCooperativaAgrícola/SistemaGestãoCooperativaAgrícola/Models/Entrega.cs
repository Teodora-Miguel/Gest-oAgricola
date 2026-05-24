using System;
using System.Collections.Generic;
using SistemaGestãoCooperativaAgrícola.Interfaces;
using SistemaGestãoCooperativaAgrícola.Models;
using SistemaGestãoCooperativaAgrícola.Exceptions;

namespace SistemaGestãoCooperativaAgrícola.Models
{
    public class Entrega
    {
        public Cooperativista Socio { get; private set; }
        public ProdutoAgricola Produto { get; private set; }
        public double QuantidadeQuilos { get; private set; }
        public DateTime DataEntrega { get; private set; }
        public EpocaAgricola Epoca { get; private set; }

        public Entrega(Cooperativista socio, ProdutoAgricola produto, double quantidade, EpocaAgricola epoca)
        {
            if (quantidade <= 0)
                throw new QuantidadeInvalidaException("A quantidade entregue deve ser superior a zero quilos.");

            Socio = socio;
            Produto = produto;
            QuantidadeQuilos = quantidade;
            Epoca = epoca;
            DataEntrega = DateTime.Now;
        }

        public double CalcularValorBruto()
        {
            return QuantidadeQuilos * Produto.PrecoBasePorQuilo;
        }
    }
}
