using System;
using System.Collections.Generic;
using SistemaGestãoCooperativaAgrícola.Interfaces;
using SistemaGestãoCooperativaAgrícola.Repositories;
using SistemaGestãoCooperativaAgrícola.Models;
using SistemaGestãoCooperativaAgrícola.Exceptions;

namespace SistemaGestãoCooperativaAgrícola
{
    class Program
    {
        static List<Entrega> listaGlobalEntregas = new List<Entrega>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            ICalculavelLucro cooperativa = new GestorCooperativa();

            InicializarDadosPadrao(cooperativa);

            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("====================================================");
                Console.WriteLine("    COOPERATIVA AGRÍCOLA - MENU PRINCIPAL (POO II)  ");
                Console.WriteLine("====================================================");
                Console.WriteLine(" 1. Registar Novo Sócio (Cooperativista)");
                Console.WriteLine(" 2. Registar Entrega de Produto Agrícola");
                Console.WriteLine(" 3. Processar e Distribuir Lucros Anuais");
                Console.WriteLine(" 4. Listar Sócios e Histórico de Lucros");
                Console.WriteLine(" 5. Relatório de Produção Geral");
                Console.WriteLine(" 6. Sair do Sistema");
                Console.WriteLine("====================================================");
                Console.Write(" Escolha uma opção (1-6): ");

                string opcao = Console.ReadLine();
                Console.WriteLine();

                switch (opcao)
                {
                    case "1":
                        MenuRegistarSocio(cooperativa);
                        break;
                    case "2":
                        MenuRegistarEntrega(cooperativa);
                        break;
                    case "3":
                        MenuProcessarLucro(cooperativa);
                        break;
                    case "4":
                        MenuListarSocios(cooperativa);
                        break;
                    case "5":
                        MenuRelatorioProducao();
                        break;
                    case "6":
                        continuar = false;
                        Console.WriteLine("A encerrar o sistema... Bom trabalho académico!");
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Carregue em qualquer tecla para tentar de novo.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        

        static void MenuRegistarSocio(ICalculavelLucro cooperativa)
        {
            Console.WriteLine("--- REGISTAR COOPERATIVISTA ---");
            Console.Write("Número de Sócio (Ex: S003): ");
            string num = Console.ReadLine().Trim();

            Console.Write("Nome do Sócio: ");
            string nome = Console.ReadLine().Trim();

            Console.Write("Quota de Participação % (Ex: 15,5): ");
            double quota;
            if (!double.TryParse(Console.ReadLine(), out quota))
            {
                Console.WriteLine("Erro: Valor de quota inválido.");
                AguardarTecla();
                return;
            }

            try
            {
                cooperativa.AdicionarCooperativista(new Cooperativista(num, nome, quota));
                Console.WriteLine("\n>>> Sócio registado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nErro ao registar: " + ex.Message);
            }
            AguardarTecla();
        }

        static void MenuRegistarEntrega(ICalculavelLucro cooperativa)
        {
            Console.WriteLine("--- REGISTAR ENTREGA AGRÍCOLA ---");
            Console.Write("Número do Sócio que vai entregar: ");
            string numSocio = Console.ReadLine().Trim();

            Console.Write("Código do Produto (Ex: CAF01): ");
            string codProd = Console.ReadLine().Trim();

            Console.Write("Quantidade em Quilos (Ex: 250,8): ");
            double qtd;
            if (!double.TryParse(Console.ReadLine(), out qtd))
            {
                Console.WriteLine("Erro: Quantidade inválida.");
                AguardarTecla();
                return;
            }

            Console.WriteLine("Época Agrícola:");
            Console.WriteLine(" 1. Primeira Época (Sementeira)");
            Console.WriteLine(" 2. Segunda Época (Cacimbo / Colheita)");
            Console.Write("Escolha (1-2): ");
            string escolhaEpoca = Console.ReadLine();
            EpocaAgricola epoca = escolhaEpoca == "1" ? EpocaAgricola.PrimeiraEpoca : EpocaAgricola.SegundaEpoca;

            try
            {
                
                Cooperativista socioTemp = null;
                foreach (var s in cooperativa.ListarSocios())
                {
                    if (s.NumeroSocio.Equals(numSocio)) { socioTemp = s; break; }
                }

                cooperativa.RegistarEntrega(numSocio, codProd, qtd, epoca);

                if (socioTemp != null)
                {
                    ProdutoAgricola prodTemp = new ProdutoAgricola(codProd, codProd == "CAF01" ? "Café Amboim" : "Produto Geral", 1200.0);
                    listaGlobalEntregas.Add(new Entrega(socioTemp, prodTemp, qtd, epoca));
                }

                Console.WriteLine("\n>>> Entrega registada e validada com sucesso!");
            }
            catch (CooperativaException ex)
            {
                Console.WriteLine("\n[ERRO DE NEGÓCIO]: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n[ERRO INESPERADO]: " + ex.Message);
            }
            AguardarTecla();
        }

        static void MenuProcessarLucro(ICalculavelLucro cooperativa)
        {
            Console.WriteLine("--- ENCERRAMENTO DE PERÍODO E DISTRIBUIÇÃO DE LUCROS ---");
            Console.Write("Ano de Exercício (Ex: 2026): ");
            string ano = Console.ReadLine().Trim();

            Console.Write("Lucro Total Líquido a Distribuir (Kz): ");
            double lucroTotal;
            if (!double.TryParse(Console.ReadLine(), out lucroTotal))
            {
                Console.WriteLine("Erro: Valor de lucro inválido.");
                AguardarTecla();
                return;
            }

            try
            {
                cooperativa.ProcessarEGerarDistribuiçãoLucro(ano, lucroTotal);
                Console.WriteLine("\n>>> Sucesso! O período foi encerrado e os lucros divididos pelas quotas.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nErro no processamento: " + ex.Message);
            }
            AguardarTecla();
        }

        static void MenuListarSocios(ICalculavelLucro cooperativa)
        {
            Console.WriteLine("--- LISTAGEM ATUAL DE SÓCIOS E LUCROS ---");
            var lista = cooperativa.ListarSocios();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum sócio cadastrado até ao momento.");
            }
            else
            {
                foreach (var socio in lista)
                {
                    Console.WriteLine("\nSócio: [" + socio.NumeroSocio + "] " + socio.Nome + " | Quota: " + socio.QuotaParticipacao + "%");
                    var historico = socio.ObterHistoricoLucros();
                    if (historico.Count == 0)
                    {
                        Console.WriteLine("   (Sem lucros atribuídos neste ciclo)");
                    }
                    else
                    {
                        foreach (var lucro in historico)
                        {
                            Console.WriteLine("   -> Ano: " + lucro.Ano + " | Recebido: " + lucro.ValorRecebido.ToString("N2") + " Kz");
                        }
                    }
                }
            }
            AguardarTecla();
        }

        
        static void MenuRelatorioProducao()
        {
            Console.WriteLine("====================================================");
            Console.WriteLine("          RELATÓRIO DE PRODUÇÃO DA COOPERATIVA      ");
            Console.WriteLine("====================================================");

            if (listaGlobalEntregas.Count == 0)
            {
                Console.WriteLine("Nenhuma entrega de produção registada neste período.");
            }
            else
            {
                double totalQuilos = 0;
                double receitaTotalBruta = 0;

                Console.WriteLine("{0,-12} {1,-15} {2,-12} {3,-15}", "SÓCIO", "PRODUTO", "QTD (KG)", "VALOR BRUTO");
                Console.WriteLine("----------------------------------------------------");

                foreach (var entrega in listaGlobalEntregas)
                {
                    double valorBruto = entrega.CalcularValorBruto();
                    Console.WriteLine("{0,-12} {1,-15} {2,-12:N1} {3,-15:N2} Kz",
                        entrega.Socio.Nome,
                        entrega.Produto.Nome,
                        entrega.QuantidadeQuilos,
                        valorBruto);

                    totalQuilos += entrega.QuantidadeQuilos;
                    receitaTotalBruta += valorBruto;
                }

                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("Total de Quilos Acumulados: " + totalQuilos.ToString("N1") + " Kg");
                Console.WriteLine("Volume de Negócio Bruto:    " + receitaTotalBruta.ToString("N2") + " Kz");
            }
            AguardarTecla();
        }

        static void InicializarDadosPadrao(ICalculavelLucro cooperativa)
        {
            Cooperativista s1 = new Cooperativista("S001", "Fernando", 40.0);
            Cooperativista s2 = new Cooperativista("S002", "Teodora", 60.0);
            cooperativa.AdicionarCooperativista(s1);
            cooperativa.AdicionarCooperativista(s2);

            ProdutoAgricola cafe = new ProdutoAgricola("CAF01", "Café Amboim", 1200.0);
            cooperativa.AdicionarProduto(cafe);

            
            cooperativa.RegistarEntrega("S001", "CAF01", 150.0, EpocaAgricola.SegundaEpoca);
            cooperativa.RegistarEntrega("S002", "CAF01", 300.0, EpocaAgricola.SegundaEpoca);

            listaGlobalEntregas.Add(new Entrega(s1, cafe, 150.0, EpocaAgricola.SegundaEpoca));
            listaGlobalEntregas.Add(new Entrega(s2, cafe, 300.0, EpocaAgricola.SegundaEpoca));
        }

        static void AguardarTecla()
        {
            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
        }
    }
}