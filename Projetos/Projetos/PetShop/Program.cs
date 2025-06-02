using System;
using System.Collections.Generic;

namespace PetShopApp
{
    // Superclasse Animal
    public class Animal
    {
        public string Nome { get; set; }
        public string Raca { get; set; }
        public DateTime DataNascimento { get; set; }

        public Animal(string nome, string raca, DateTime dataNascimento)
        {
            Nome = nome;
            Raca = raca;
            DataNascimento = dataNascimento;
        }

        public override string ToString()
        {
            return $"Nome: {Nome}, Raça: {Raca}, Data de Nascimento: {DataNascimento.ToShortDateString()}";
        }
    }

    // Subclasse Cachorro
    public class Cachorro : Animal
    {
        public string Porte { get; set; } // Pequeno, Médio, Grande

        public Cachorro(string nome, string raca, DateTime dataNascimento, string porte) : base(nome, raca, dataNascimento)
        {
            Porte = porte;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Porte: {Porte}";
        }
    }

    // Subclasse Gato
    public class Gato : Animal
    {
        public string CorPelagem { get; set; }

        public Gato(string nome, string raca, DateTime dataNascimento, string corPelagem) : base(nome, raca, dataNascimento)
        {
            CorPelagem = corPelagem;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Cor da Pelagem: {CorPelagem}";
        }
    }

    // Subclasse Passaro
    public class Passaro : Animal
    {
        public string TipoBico { get; set; }

        public Passaro(string nome, string raca, DateTime dataNascimento, string tipoBico) : base(nome, raca, dataNascimento)
        {
            TipoBico = tipoBico;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Tipo de Bico: {TipoBico}";
        }
    }

    // Classe Dono
    public class Dono
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }

        public Dono(string nome, string telefone)
        {
            Nome = nome;
            Telefone = telefone;
        }

        public override string ToString()
        {
            return $"Nome: {Nome}, Telefone: {Telefone}";
        }
    }

    // Classe Serviço
    public class Servico
    {
        public Animal Animal { get; set; }
        public string TipoServico { get; set; } // Banho, Tosa, etc.
        public DateTime DataServico { get; set; }
        public double Preco { get; set; }

        public Servico(Animal animal, string tipoServico, double preco)
        {
            Animal = animal;
            TipoServico = tipoServico;
            DataServico = DateTime.Now;
            Preco = preco;
        }

        public override string ToString()
        {
            return $"Animal: {Animal.Nome}, Serviço: {TipoServico}, Data: {DataServico.ToShortDateString()}, Preço: {Preco:C}";
        }
    }

    // Classe para gerenciar o Pet Shop
    public class PetShop
    {
        public List<Animal> Animais { get; set; } = new List<Animal>();
        public List<Dono> Donos { get; set; } = new List<Dono>();
        public List<Servico> Servicos { get; set; } = new List<Servico>();

        public void AdicionarAnimal(Animal animal)
        {
            Animais.Add(animal);
        }

        public void AdicionarDono(Dono dono)
        {
            Donos.Add(dono);
        }

        public void AgendarServico(Animal animal, string tipoServico, double preco)
        {
            // Validação: Tosa só pode ser agendada para cachorros
            if (tipoServico.Equals("Tosa", StringComparison.OrdinalIgnoreCase) && !(animal is Cachorro))
            {
                Console.WriteLine("Tosa só pode ser agendada para cachorros.");
                return;
            }

            Servico servico = new Servico(animal, tipoServico, preco);
            Servicos.Add(servico);
            Console.WriteLine($"Serviço agendado para {animal.Nome} em {servico.DataServico.ToShortDateString()}.");
        }

        public void ListarAnimais()
        {
            Console.WriteLine("\nLista de Animais Cadastrados:");
            foreach (var animal in Animais)
            {
                Console.WriteLine(animal); // Polimorfismo: ToString() é chamado de acordo com o tipo do animal
            }
        }

        public void ListarDonos()
        {
            Console.WriteLine("\nLista de Donos Cadastrados:");
            foreach (var dono in Donos)
            {
                Console.WriteLine(dono);
            }
        }

        public void ListarServicos()
        {
            Console.WriteLine("\nLista de Serviços Agendados:");
            foreach (var servico in Servicos)
            {
                Console.WriteLine(servico);
            }
        }
    }

    // Classe Principal (Programa)
    class Program
    {
        static PetShop petShop = new PetShop();

        static void Main(string[] args)
        {
            ExibirMenu();
        }

        static void ExibirMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Menu do Pet Shop ---");
                Console.WriteLine("1. Adicionar Cachorro");
                Console.WriteLine("2. Adicionar Gato");
                Console.WriteLine("3. Adicionar Pássaro");
                Console.WriteLine("4. Adicionar Dono");
                Console.WriteLine("5. Agendar Serviço");
                Console.WriteLine("6. Listar Animais");
                Console.WriteLine("7. Listar Donos");
                Console.WriteLine("8. Listar Serviços");
                Console.WriteLine("9. Sair");

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarCachorro();
                        break;
                    case "2":
                        AdicionarGato();
                        break;
                    case "3":
                        AdicionarPassaro();
                        break;
                    case "4":
                        AdicionarDono();
                        break;
                    case "5":
                        AgendarServico();
                        break;
                    case "6":
                        petShop.ListarAnimais();
                        break;
                    case "7":
                        petShop.ListarDonos();
                        break;
                    case "8":
                        petShop.ListarServicos();
                        break;
                    case "9":
                        Console.WriteLine("Saindo do sistema...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static void AdicionarCachorro()
        {
            Console.Write("Nome do cachorro: ");
            string nome = Console.ReadLine();
            Console.Write("Raça do cachorro: ");
            string raca = Console.ReadLine();
            Console.Write("Data de nascimento do cachorro (dd/MM/yyyy): ");
            DateTime dataNascimento = DateTime.Parse(Console.ReadLine()); // Tratar exceção se a data for inválida
            Console.Write("Porte do cachorro (Pequeno, Médio, Grande): ");
            string porte = Console.ReadLine();

            Cachorro novoCachorro = new Cachorro(nome, raca, dataNascimento, porte);
            petShop.AdicionarAnimal(novoCachorro);

            Console.WriteLine("Cachorro adicionado com sucesso!");
        }

        static void AdicionarGato()
        {
            Console.Write("Nome do gato: ");
            string nome = Console.ReadLine();
            Console.Write("Raça do gato: ");
            string raca = Console.ReadLine();
            Console.Write("Data de nascimento do gato (dd/MM/yyyy): ");
            DateTime dataNascimento = DateTime.Parse(Console.ReadLine()); // Tratar exceção se a data for inválida
            Console.Write("Cor da pelagem do gato: ");
            string corPelagem = Console.ReadLine();

            Gato novoGato = new Gato(nome, raca, dataNascimento, corPelagem);
            petShop.AdicionarAnimal(novoGato);

            Console.WriteLine("Gato adicionado com sucesso!");
        }

        static void AdicionarPassaro()
        {
            Console.Write("Nome do pássaro: ");
            string nome = Console.ReadLine();
            Console.Write("Raça do pássaro: ");
            string raca = Console.ReadLine();
            Console.Write("Data de nascimento do pássaro (dd/MM/yyyy): ");
            DateTime dataNascimento = DateTime.Parse(Console.ReadLine()); // Tratar exceção se a data for inválida
            Console.Write("Tipo de bico do pássaro: ");
            string tipoBico = Console.ReadLine();

            Passaro novoPassaro = new Passaro(nome, raca, dataNascimento, tipoBico);
            petShop.AdicionarAnimal(novoPassaro);

            Console.WriteLine("Pássaro adicionado com sucesso!");
        }

        static void AdicionarDono()
        {
            Console.Write("Nome do dono: ");
            string nome = Console.ReadLine();
            Console.Write("Telefone do dono: ");
            string telefone = Console.ReadLine();

            Dono novoDono = new Dono(nome, telefone);
            petShop.AdicionarDono(novoDono);

            Console.WriteLine("Dono adicionado com sucesso!");
        }

        static void AgendarServico()
        {
            Console.Write("Nome do animal para agendar o serviço: ");
            string nomeAnimal = Console.ReadLine();

            Animal animalParaServico = petShop.Animais.Find(a => a.Nome.Equals(nomeAnimal, StringComparison.OrdinalIgnoreCase));

            if (animalParaServico == null)
            {
                Console.WriteLine("Animal não encontrado.");
                return;
            }

            Console.Write("Tipo de serviço (Banho, Tosa, etc.): ");
            string tipoServico = Console.ReadLine();
            Console.Write("Preço do serviço: ");
            double precoServico = double.Parse(Console.ReadLine()); // Tratar exceção se a entrada não for um número

            petShop.AgendarServico(animalParaServico, tipoServico, precoServico);
        }
    }
}