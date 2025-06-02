using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleVeiculos
{
    // Enum para Tipo de Manutenção
    public enum TipoManutencao
    {
        Preventiva,
        Corretiva
    }

    // Classe Manutenção
    public class Manutencao
    {
        public DateTime DataServico { get; set; }
        public string Descricao { get; set; }
        public TipoManutencao Tipo { get; set; }

        public Manutencao(DateTime dataServico, string descricao, TipoManutencao tipo)
        {
            DataServico = dataServico;
            Descricao = descricao;
            Tipo = tipo;
        }

        public override string ToString()
        {
            return $"Data: {DataServico.ToShortDateString()}, Descrição: {Descricao}, Tipo: {Tipo}";
        }
    }

    // Classe base Veiculo
    public class Veiculo
    {
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public string Tipo { get; set; } // Tipo genérico para a classe base
        public List<Manutencao> Manutencoes { get; set; } = new List<Manutencao>();

        public Veiculo(string modelo, string placa, string tipo)
        {
            Modelo = modelo;
            Placa = placa;
            Tipo = tipo;
        }

        public bool AdicionarManutencao(Manutencao manutencao)
        {
            if (Manutencoes.Any(m => m.DataServico.Date == manutencao.DataServico.Date))
            {
                Console.WriteLine($"Erro: Já existe uma manutenção registrada para este veículo na data {manutencao.DataServico.ToShortDateString()}.");
                return false; // Não adiciona se já existe manutenção na mesma data
            }

            Manutencoes.Add(manutencao);
            return true;
        }

        public override string ToString()
        {
            return $"Modelo: {Modelo}, Placa: {Placa}, Tipo: {Tipo}";
        }
    }

    // Subclasse VeiculoPasseio
    public class VeiculoPasseio : Veiculo
    {
        public int NumeroPortas { get; set; }

        public VeiculoPasseio(string modelo, string placa, int numeroPortas) : base(modelo, placa, "Passeio")
        {
            NumeroPortas = numeroPortas;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Número de Portas: {NumeroPortas}";
        }
    }

    // Subclasse VeiculoUtilitario
    public class VeiculoUtilitario : Veiculo
    {
        public double CapacidadeCarga { get; set; }

        public VeiculoUtilitario(string modelo, string placa, double capacidadeCarga) : base(modelo, placa, "Utilitário")
        {
            CapacidadeCarga = capacidadeCarga;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Capacidade de Carga: {CapacidadeCarga} kg";
        }
    }

    // Subclasse VeiculoCarga
    public class VeiculoCarga : Veiculo
    {
        public int NumeroEixos { get; set; }

        public VeiculoCarga(string modelo, string placa, int numeroEixos) : base(modelo, placa, "Carga")
        {
            NumeroEixos = numeroEixos;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Número de Eixos: {NumeroEixos}";
        }
    }

    // Subclasse VeiculoTransporte
    public class VeiculoTransporte : Veiculo
    {
        public int NumeroPassageiros { get; set; }

        public VeiculoTransporte(string modelo, string placa, int numeroPassageiros) : base(modelo, placa, "Transporte")
        {
            NumeroPassageiros = numeroPassageiros;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Número de Passageiros: {NumeroPassageiros}";
        }
    }

    // Classe para gerenciar o Sistema de Controle de Veículos
    public class SistemaControle
    {
        public List<Veiculo> Veiculos { get; set; } = new List<Veiculo>();

        public void AdicionarVeiculo(Veiculo veiculo)
        {
            Veiculos.Add(veiculo);
        }

        public void ListarVeiculos()
        {
            Console.WriteLine("\nLista de Veículos:");
            foreach (var veiculo in Veiculos)
            {
                Console.WriteLine(veiculo);
                Console.WriteLine("Manutenções:");
                foreach (var manutencao in veiculo.Manutencoes)
                {
                    Console.WriteLine($"  - {manutencao}");
                }
            }
        }
    }

    // Classe Principal (Programa)
    class Program
    {
        static SistemaControle sistema = new SistemaControle();

        static void Main(string[] args)
        {
            ExibirMenu();
        }

        static void ExibirMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Menu do Sistema de Controle de Veículos ---");
                Console.WriteLine("1. Adicionar Veículo");
                Console.WriteLine("2. Adicionar Manutenção a Veículo");
                Console.WriteLine("3. Listar Veículos");
                Console.WriteLine("4. Sair");

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarVeiculo();
                        break;
                    case "2":
                        AdicionarManutencao();
                        break;
                    case "3":
                        sistema.ListarVeiculos();
                        break;
                    case "4":
                        Console.WriteLine("Saindo do sistema...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static void AdicionarVeiculo()
        {
            Console.WriteLine("\n--- Adicionar Veículo ---");
            Console.WriteLine("1. Veículo de Passeio");
            Console.WriteLine("2. Veículo Utilitário");
            Console.WriteLine("3. Veículo de Carga");
            Console.WriteLine("4. Veículo de Transporte");

            Console.Write("Escolha o tipo de veículo (1-4): ");
            if (!int.TryParse(Console.ReadLine(), out int tipoEscolhido) || tipoEscolhido < 1 || tipoEscolhido > 4)
            {
                Console.WriteLine("Tipo inválido.");
                return;
            }

            switch (tipoEscolhido)
            {
                case 1:
                    AdicionarVeiculoPasseio();
                    break;
                case 2:
                    AdicionarVeiculoUtilitario();
                    break;
                case 3:
                    AdicionarVeiculoCarga();
                    break;
                case 4:
                    AdicionarVeiculoTransporte();
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }

        static void AdicionarVeiculoPasseio()
        {
            Console.Write("Modelo: ");
            string modelo = Console.ReadLine();

            Console.Write("Placa: ");
            string placa = Console.ReadLine();

            Console.Write("Número de Portas: ");
            if (!int.TryParse(Console.ReadLine(), out int numeroPortas))
            {
                Console.WriteLine("Número de portas inválido.");
                return;
            }

            VeiculoPasseio novoVeiculo = new VeiculoPasseio(modelo, placa, numeroPortas);
            sistema.AdicionarVeiculo(novoVeiculo);

            Console.WriteLine("Veículo de Passeio adicionado com sucesso!");
        }

        static void AdicionarVeiculoUtilitario()
        {
            Console.Write("Modelo: ");
            string modelo = Console.ReadLine();

            Console.Write("Placa: ");
            string placa = Console.ReadLine();

            Console.Write("Capacidade de Carga (kg): ");
            if (!double.TryParse(Console.ReadLine(), out double capacidadeCarga))
            {
                Console.WriteLine("Capacidade de carga inválida.");
                return;
            }

            VeiculoUtilitario novoVeiculo = new VeiculoUtilitario(modelo, placa, capacidadeCarga);
            sistema.AdicionarVeiculo(novoVeiculo);

            Console.WriteLine("Veículo Utilitário adicionado com sucesso!");
        }

        static void AdicionarVeiculoCarga()
        {
            Console.Write("Modelo: ");
            string modelo = Console.ReadLine();

            Console.Write("Placa: ");
            string placa = Console.ReadLine();

            Console.Write("Número de Eixos: ");
            if (!int.TryParse(Console.ReadLine(), out int numeroEixos))
            {
                Console.WriteLine("Número de eixos inválido.");
                return;
            }

            VeiculoCarga novoVeiculo = new VeiculoCarga(modelo, placa, numeroEixos);
            sistema.AdicionarVeiculo(novoVeiculo);

            Console.WriteLine("Veículo de Carga adicionado com sucesso!");
        }

        static void AdicionarVeiculoTransporte()
        {
            Console.Write("Modelo: ");
            string modelo = Console.ReadLine();

            Console.Write("Placa: ");
            string placa = Console.ReadLine();

            Console.Write("Número de Passageiros: ");
            if (!int.TryParse(Console.ReadLine(), out int numeroPassageiros))
            {
                Console.WriteLine("Número de passageiros inválido.");
                return;
            }

            VeiculoTransporte novoVeiculo = new VeiculoTransporte(modelo, placa, numeroPassageiros);
            sistema.AdicionarVeiculo(novoVeiculo);

            Console.WriteLine("Veículo de Transporte adicionado com sucesso!");
        }

        static void AdicionarManutencao()
        {
            Console.WriteLine("\n--- Adicionar Manutenção ---");

            Console.Write("Placa do veículo: ");
            string placa = Console.ReadLine();

            Veiculo veiculo = sistema.Veiculos.Find(v => v.Placa == placa);
            if (veiculo == null)
            {
                Console.WriteLine("Veículo não encontrado.");
                return;
            }

            Console.Write("Data do serviço (dd/mm/aaaa): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataServico))
            {
                Console.WriteLine("Data inválida.");
                return;
            }

            Console.Write("Descrição detalhada: ");
            string descricao = Console.ReadLine();

            Console.WriteLine("Tipos de Manutenção:");
            Console.WriteLine("1. Preventiva");
            Console.WriteLine("2. Corretiva");
            Console.Write("Escolha o tipo (1-2): ");
            if (!int.TryParse(Console.ReadLine(), out int tipoEscolhido) || tipoEscolhido < 1 || tipoEscolhido > 2)
            {
                Console.WriteLine("Tipo inválido.");
                return;
            }
            TipoManutencao tipo = (TipoManutencao)(tipoEscolhido - 1);

            Manutencao novaManutencao = new Manutencao(dataServico, descricao, tipo);
            if (veiculo.AdicionarManutencao(novaManutencao))
            {
                Console.WriteLine("Manutenção adicionada com sucesso!");
            }
        }
    }
}
