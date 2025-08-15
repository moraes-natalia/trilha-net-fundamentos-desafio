
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        // Método para validar placas brasileiras (formato antigo e Mercosul)
        private bool ValidarPlacaBrasileira(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
                return false;

            // Remove espaços e hífen para padronizar
            string placaLimpa = placa.Replace("-", "").Replace(" ", "").ToUpper();

            // Formato antigo: ABC1234 (3 letras + 4 números)
            string padraoAntigo = @"^[A-Z]{3}[0-9]{4}$";

            // Formato Mercosul: ABC1D23 (3 letras + 1 número + 1 letra + 2 números)
            string padraoMercosul = @"^[A-Z]{3}[0-9]{1}[A-Z]{1}[0-9]{2}$";

            return Regex.IsMatch(placaLimpa, padraoAntigo) || Regex.IsMatch(placaLimpa, padraoMercosul);
        }

        // Método para formatar placa no padrão brasileiro
        private string FormatarPlaca(string placa)
        {
            string placaLimpa = placa.Replace("-", "").Replace(" ", "").ToUpper();

            // Se tem 7 caracteres, é formato antigo (ABC-1234)
            if (placaLimpa.Length == 7 && Regex.IsMatch(placaLimpa, @"^[A-Z]{3}[0-9]{4}$"))
            {
                return $"{placaLimpa.Substring(0, 3)}-{placaLimpa.Substring(3, 4)}";
            }
            // Se tem 7 caracteres e é Mercosul (ABC1D23)
            else if (placaLimpa.Length == 7 && Regex.IsMatch(placaLimpa, @"^[A-Z]{3}[0-9]{1}[A-Z]{1}[0-9]{2}$"))
            {
                return $"{placaLimpa.Substring(0, 3)}{placaLimpa.Substring(3, 1)}{placaLimpa.Substring(4, 1)}{placaLimpa.Substring(5, 2)}";
            }

            return placaLimpa;
        }

        public void AdicionarVeiculo()
        {
            // Pedir para o usuário digitar uma placa (ReadLine) e adicionar na lista "veículos"
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            Console.WriteLine("Formatos aceitos:");
            Console.WriteLine("- Formato antigo: ABC-1234 ou ABC1234");
            Console.WriteLine("- Formato Mercosul: ABC1D23");

            string placaDigitada = Console.ReadLine();

            // Verifica se a placa é válida no formato brasileiro
            if (ValidarPlacaBrasileira(placaDigitada))
            {
                string placaFormatada = FormatarPlaca(placaDigitada);

                if (!veiculos.Any(x => x == placaFormatada))
                {
                    veiculos.Add(placaFormatada);
                    Console.WriteLine($"Veículo {placaFormatada} foi adicionado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Este veículo já está estacionado.");
                }
            }
            else
            {
                Console.WriteLine("Placa inválida! Utilize o formato brasileiro:");
                Console.WriteLine("- Formato antigo: ABC-1234 ou ABC1234 (3 letras + 4 números)");
                Console.WriteLine("- Formato Mercosul: ABC1D23 (3 letras + 1 número + 1 letra + 2 números)");
            }
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");
            Console.WriteLine("Formatos aceitos: ABC-1234, ABC1234 ou ABC1D23");

            // Pede para o usuário digitar a placa e armazenar na variável placa
            string placaDigitada = Console.ReadLine();

            // Valida placa -> formato brasileiro
            if (!ValidarPlacaBrasileira(placaDigitada))
            {
                Console.WriteLine("Placa inválida! Use o formato brasileiro válido.");
                return;
            }

            string placaFormatada = FormatarPlaca(placaDigitada);

            // Verifica -> veículo existe
            if (veiculos.Any(x => x == placaFormatada))
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                // Pede para o usuário digitar a quantidade de horas que o veículo permaneceu estacionado,
                // Realiza o cálculo: "precoInicial + precoPorHora * horas" para a variável valorTotal
                if (int.TryParse(Console.ReadLine(), out int horas) && horas >= 0)
                {
                    decimal valorTotal = precoInicial + (precoPorHora * horas);

                    // Remove a placa digitada da lista de veículos
                    veiculos.Remove(placaFormatada);

                    Console.WriteLine($"O veículo {placaFormatada} foi removido com sucesso. O preço total foi de: R$ {valorTotal:F2}");
                }
                else
                {
                    Console.WriteLine("Quantidade de horas inválida. Operação cancelada.");
                }
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                // Realizar um laço de repetição, exibindo os veículos estacionados
                for (int i = 0; i < veiculos.Count; i++)
                {
                    Console.WriteLine($"{i + 1}º veículo: {veiculos[i]}");
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
    }
}
