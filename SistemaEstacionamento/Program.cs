using DesafioFundamentos.Models;
using System;

namespace DesafioFundamentos
{
    class Program
    {
        static void Main(string[] args)
        {
            // Coleta os preços iniciais
            Console.WriteLine("Seja bem-vindo(a) ao sistema de estacionamento.");
            Console.WriteLine("Digite o preço inicial:");
            decimal precoInicial = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Digite o preço por hora:");
            decimal precoPorHora = Convert.ToDecimal(Console.ReadLine());

            // Instancia a classe Estacionamento
            Estacionamento estacionamento = new Estacionamento(precoInicial, precoPorHora);

            string opcao = string.Empty;
            bool exibirMenu = true;

            // Loop do menu
            while (exibirMenu)
            {
                Console.Clear();
                Console.WriteLine("Digite a sua opção:");
                Console.WriteLine("1 - Cadastrar veículo");
                Console.WriteLine("2 - Remover veículo");
                Console.WriteLine("3 - Listar veículos");
                Console.WriteLine("4 - Encerrar");

                switch (Console.ReadLine())
                {
                    case "1":
                        estacionamento.AdicionarVeiculo();
                        break;

                    case "2":
                        estacionamento.RemoverVeiculo();
                        break;

                    case "3":
                        estacionamento.ListarVeiculos();
                        break;

                    case "4":
                        exibirMenu = false;
                        break;

                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }

                if (exibirMenu)
                {
                    Console.WriteLine("Pressione uma tecla para continuar");
                    Console.ReadLine();
                }
            }

            Console.WriteLine("O programa se encerrou");
        }
    }
}