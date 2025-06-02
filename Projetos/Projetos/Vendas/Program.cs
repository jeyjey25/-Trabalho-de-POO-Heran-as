using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaVendas
{
    // Classe base Produto
    public class Produto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }

        public Produto(int codigo, string nome, double preco)
        {
            Codigo = codigo;
            Nome = nome;
            Preco = preco;
        }

        public override string ToString()
        {
            return $"Código: {Codigo}, Nome: {Nome}, Preço: {Preco:C}";
        }
    }

    // Subclasse ProdutoAlimento
    public class ProdutoAlimento : Produto
    {
        public string Marca { get; set; }
        public string Tipo { get; set; }

        public ProdutoAlimento(int codigo, string nome, double preco, string marca, string tipo) : base(codigo, nome, preco)
        {
            Marca = marca;
            Tipo = tipo;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Marca: {Marca}, Tipo: {Tipo}";
        }
    }

    // Subclasse ProdutoEletronico
    public class ProdutoEletronico : Produto
    {
        public string Fabricante { get; set; }
        public int Voltagem { get; set; }

        public ProdutoEletronico(int codigo, string nome, double preco, string fabricante, int voltagem) : base(codigo, nome, preco)
        {
            Fabricante = fabricante;
            Voltagem = voltagem;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Fabricante: {Fabricante}, Voltagem: {Voltagem}V";
        }
    }

    // Classe ItemPedido
    public class ItemPedido
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }

        public ItemPedido(Produto produto, int quantidade)
        {
            Produto = produto;
            Quantidade = quantidade;
        }

        public double Subtotal()
        {
            return Produto.Preco * Quantidade;
        }

        public override string ToString()
        {
            return $"Produto: {Produto.Nome}, Quantidade: {Quantidade}, Subtotal: {Subtotal():C}";
        }
    }

    // Classe Pedido
    public class Pedido
    {
        public List<ItemPedido> Itens { get; set; } = new List<ItemPedido>(); // Composição: Itens existem apenas dentro do Pedido
        public DateTime DataPedido { get; set; }

        public Pedido()
        {
            DataPedido = DateTime.Now;
        }

        public void AdicionarItem(ItemPedido item)
        {
            Itens.Add(item);
        }

        public void RemoverItem(ItemPedido item)
        {
            Itens.Remove(item);
        }

        public double CalcularTotal()
        {
            return Itens.Sum(item => item.Subtotal());
        }

        public override string ToString()
        {
            return $"Data do Pedido: {DataPedido.ToShortDateString()}, Total: {CalcularTotal():C}, Número de Itens: {Itens.Count}";
        }
    }

    // Classe para gerenciar o Sistema de Vendas
    public class SistemaVendas
    {
        public List<Produto> Produtos { get; set; } = new List<Produto>();
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();

        public void AdicionarProduto(Produto produto)
        {
            Produtos.Add(produto);
        }

        public void CriarPedido(Pedido pedido)
        {
            Pedidos.Add(pedido);
        }

        public void ListarProdutos()
        {
            Console.WriteLine("\nLista de Produtos Disponíveis:");
            foreach (var produto in Produtos)
            {
                Console.WriteLine(produto);
            }
        }

        public void ListarPedidos()
        {
            Console.WriteLine("\nLista de Pedidos:");
            foreach (var pedido in Pedidos)
            {
                Console.WriteLine(pedido);
                foreach (var item in pedido.Itens)
                {
                    Console.WriteLine($"  - {item}");
                }
            }
        }
    }

    // Classe Principal (Programa)
    class Program
    {
        static SistemaVendas sistema = new SistemaVendas();

        static void Main(string[] args)
        {
            ExibirMenu();
        }

        static void ExibirMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Menu do Sistema de Vendas ---");
                Console.WriteLine("1. Adicionar Produto");
                Console.WriteLine("2. Criar Pedido");
                Console.WriteLine("3. Listar Produtos");
                Console.WriteLine("4. Listar Pedidos");
                Console.WriteLine("5. Sair");

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarProduto();
                        break;
                    case "2":
                        CriarPedido();
                        break;
                    case "3":
                        sistema.ListarProdutos();
                        break;
                    case "4":
                        sistema.ListarPedidos();
                        break;
                    case "5":
                        Console.WriteLine("Saindo do sistema...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static void AdicionarProduto()
        {
            Console.WriteLine("\n--- Adicionar Produto ---");
            Console.WriteLine("1. Produto Comum");
            Console.WriteLine("2. Produto Alimento");
            Console.WriteLine("3. Produto Eletrônico");

            Console.Write("Escolha o tipo de produto: ");
            string tipoProduto = Console.ReadLine();

            switch (tipoProduto)
            {
                case "1":
                    AdicionarProdutoComum();
                    break;
                case "2":
                    AdicionarProdutoAlimento();
                    break;
                case "3":
                    AdicionarProdutoEletronico();
                    break;
                default:
                    Console.WriteLine("Tipo de produto inválido.");
                    break;
            }
        }

        static void AdicionarProdutoComum()
        {
            Console.Write("Código do produto: ");
            if (!int.TryParse(Console.ReadLine(), out int codigo))
            {
                Console.WriteLine("Código inválido. Insira um número inteiro.");
                return;
            }

            Console.Write("Nome do produto: ");
            string nome = Console.ReadLine();

            Console.Write("Preço do produto: ");
            if (!double.TryParse(Console.ReadLine(), out double preco))
            {
                Console.WriteLine("Preço inválido. Insira um número.");
                return;
            }

            Produto novoProduto = new Produto(codigo, nome, preco);
            sistema.AdicionarProduto(novoProduto);

            Console.WriteLine("Produto adicionado com sucesso!");
        }

        static void AdicionarProdutoAlimento()
        {
            Console.Write("Código do produto: ");
            if (!int.TryParse(Console.ReadLine(), out int codigo))
            {
                Console.WriteLine("Código inválido. Insira um número inteiro.");
                return;
            }

            Console.Write("Nome do produto: ");
            string nome = Console.ReadLine();

            Console.Write("Preço do produto: ");
            if (!double.TryParse(Console.ReadLine(), out double preco))
            {
                Console.WriteLine("Preço inválido. Insira um número.");
                return;
            }

            Console.Write("Marca do alimento: ");
            string marca = Console.ReadLine();

            Console.Write("Tipo do alimento: ");
            string tipo = Console.ReadLine();

            ProdutoAlimento novoProduto = new ProdutoAlimento(codigo, nome, preco, marca, tipo);
            sistema.AdicionarProduto(novoProduto);

            Console.WriteLine("Produto (Alimento) adicionado com sucesso!");
        }

        static void AdicionarProdutoEletronico()
        {
            Console.Write("Código do produto: ");
            if (!int.TryParse(Console.ReadLine(), out int codigo))
            {
                Console.WriteLine("Código inválido. Insira um número inteiro.");
                return;
            }

            Console.Write("Nome do produto: ");
            string nome = Console.ReadLine();

            Console.Write("Preço do produto: ");
            if (!double.TryParse(Console.ReadLine(), out double preco))
            {
                Console.WriteLine("Preço inválido. Insira um número.");
                return;
            }

            Console.Write("Fabricante do eletrônico: ");
            string fabricante = Console.ReadLine();

            Console.Write("Voltagem do eletrônico: ");
            if (!int.TryParse(Console.ReadLine(), out int voltagem))
            {
                Console.WriteLine("Voltagem inválida. Insira um número inteiro.");
                return;
            }

            ProdutoEletronico novoProduto = new ProdutoEletronico(codigo, nome, preco, fabricante, voltagem);
            sistema.AdicionarProduto(novoProduto);

            Console.WriteLine("Produto (Eletrônico) adicionado com sucesso!");
        }

        static void CriarPedido()
        {
            Pedido novoPedido = new Pedido();

            while (true)
            {
                Console.WriteLine("\n--- Adicionar Item ao Pedido ---");
                Console.Write("Código do produto (ou '0' para finalizar o pedido): ");
                string codigoProdutoStr = Console.ReadLine();
                if (codigoProdutoStr == "0")
                {
                    break;
                }

                if (!int.TryParse(codigoProdutoStr, out int codigoProduto))
                {
                    Console.WriteLine("Código inválido. Insira um número inteiro ou '0' para finalizar.");
                    continue;
                }

                Produto produto = sistema.Produtos.Find(p => p.Codigo == codigoProduto);
                if (produto == null)
                {
                    Console.WriteLine("Produto não encontrado.");
                    continue;
                }

                Console.Write("Quantidade: ");
                if (!int.TryParse(Console.ReadLine(), out int quantidade))
                {
                    Console.WriteLine("Quantidade inválida. Insira um número inteiro.");
                    continue;
                }

                ItemPedido novoItem = new ItemPedido(produto, quantidade);
                novoPedido.AdicionarItem(novoItem);

                Console.WriteLine("Item adicionado ao pedido.");
            }

            sistema.CriarPedido(novoPedido);
            Console.WriteLine("Pedido criado com sucesso!");
        }
    }
}
