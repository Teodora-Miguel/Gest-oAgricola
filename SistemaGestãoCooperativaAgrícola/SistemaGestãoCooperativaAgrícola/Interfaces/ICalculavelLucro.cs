using System.Collections.Generic;
using SistemaGestãoCooperativaAgrícola.Models; 

namespace SistemaGestãoCooperativaAgrícola.Interfaces
{
    public interface ICalculavelLucro
    {
        void AdicionarCooperativista(Cooperativista socio);
        void AdicionarProduto(ProdutoAgricola produto);
        void RegistarEntrega(string numSocio, string codProduto, double qtd, EpocaAgricola epoca);
        void ProcessarEGerarDistribuiçãoLucro(string ano, double lucroTotalDaCooperativa);
        List<Cooperativista> ListarSocios();
    }
}