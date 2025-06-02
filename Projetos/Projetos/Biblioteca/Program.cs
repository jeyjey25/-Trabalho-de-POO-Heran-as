using System;
using System.Collections.Generic;

namespace SistemaBibliotecaInterativo
{
    // Classe Livro
    public class Livro
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public bool Disponivel { get; set; } = true; // Inicialmente disponível

        public Livro(string titulo, string autor)
        {
            Titulo = titulo;
            Autor = autor;
        }

        public override string ToString()
        {
            return $"Título: {Titulo}, Autor: {Autor}, Disponível: {(Disponivel ? "Sim" : "Não")}";
        }
    }

    // Classe Leitor
    public class Leitor
    {
        public string Nome { get; set; }
        public List<Emprestimo> Emprestimos { get; set; } = new List<Emprestimo>();

        public Leitor(string nome)
        {
            Nome = nome;
        }

        public override string ToString()
        {
            return $"Nome: {Nome}, Número de Empréstimos: {Emprestimos.Count}";
        }
    }

    // Classe Emprestimo
    public class Emprestimo
    {
        public Livro Livro { get; set; }
        public Leitor Leitor { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; } // Pode ser nulo se não devolvido

        public Emprestimo(Livro livro, Leitor leitor)
        {
            Livro = livro;
            Leitor = leitor;
            DataEmprestimo = DateTime.Now;
            DataDevolucao = null; // Inicialmente não devolvido
        }

        public override string ToString()
        {
            return $"Livro: {Livro.Titulo}, Leitor: {Leitor.Nome}, Empréstimo: {DataEmprestimo}, Devolução: {(DataDevolucao.HasValue ? DataDevolucao.Value.ToString() : "Pendente")}";
        }
    }

    // Classe para gerenciar a Biblioteca
    public class Biblioteca
    {
        public List<Livro> Livros { get; set; } = new List<Livro>();
        public List<Leitor> Leitores { get; set; } = new List<Leitor>();
        public List<Emprestimo> Emprestimos { get; set; } = new List<Emprestimo>();

        public void AdicionarLivro(Livro livro)
        {
            Livros.Add(livro);
        }

        public void AdicionarLeitor(Leitor leitor)
        {
            Leitores.Add(leitor);
        }

        public bool EmprestarLivro(Livro livro, Leitor leitor)
        {
            if (!livro.Disponivel)
            {
                Console.WriteLine($"Livro '{livro.Titulo}' não está disponível para empréstimo.");
                return false;
            }

            Emprestimo emprestimo = new Emprestimo(livro, leitor);
            Emprestimos.Add(emprestimo);
            leitor.Emprestimos.Add(emprestimo);
            livro.Disponivel = false; // Marca o livro como indisponível

            Console.WriteLine($"Livro '{livro.Titulo}' emprestado para '{leitor.Nome}' em {emprestimo.DataEmprestimo}.");
            return true;
        }

        public void DevolverLivro(Emprestimo emprestimo)
        {
            emprestimo.DataDevolucao = DateTime.Now;
            emprestimo.Livro.Disponivel = true; // Marca o livro como disponível

            Console.WriteLine($"Livro '{emprestimo.Livro.Titulo}' devolvido por '{emprestimo.Leitor.Nome}' em {emprestimo.DataDevolucao}.");
        }

        public void ListarLivrosDisponiveis()
        {
            Console.WriteLine("\nLivros Disponíveis:");
            foreach (var livro in Livros)
            {
                if (livro.Disponivel)
                {
                    Console.WriteLine(livro);
                }
            }
        }

        public void ListarEmprestimosAtivos()
        {
            Console.WriteLine("\nEmpréstimos Ativos:");
            foreach (var emprestimo in Emprestimos)
            {
                if (emprestimo.DataDevolucao == null)
                {
                    Console.WriteLine(emprestimo);
                }
            }
        }
    }

    // Classe Principal (Programa)
    class Program
    {
        static Biblioteca biblioteca = new Biblioteca(); // Instância da biblioteca acessível globalmente

        static void Main(string[] args)
        {
            ExibirMenu();
        }

        static void ExibirMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Menu da Biblioteca ---");
                Console.WriteLine("1. Adicionar Livro");
                Console.WriteLine("2. Adicionar Leitor");
                Console.WriteLine("3. Emprestar Livro");
                Console.WriteLine("4. Devolver Livro");
                Console.WriteLine("5. Listar Livros Disponíveis");
                Console.WriteLine("6. Listar Empréstimos Ativos");
                Console.WriteLine("7. Sair");

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarLivro();
                        break;
                    case "2":
                        AdicionarLeitor();
                        break;
                    case "3":
                        EmprestarLivro();
                        break;
                    case "4":
                        DevolverLivro();
                        break;
                    case "5":
                        biblioteca.ListarLivrosDisponiveis();
                        break;
                    case "6":
                        biblioteca.ListarEmprestimosAtivos();
                        break;
                    case "7":
                        Console.WriteLine("Saindo do sistema...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static void AdicionarLivro()
        {
            Console.Write("Digite o título do livro: ");
            string titulo = Console.ReadLine();
            Console.Write("Digite o autor do livro: ");
            string autor = Console.ReadLine();

            Livro novoLivro = new Livro(titulo, autor);
            biblioteca.AdicionarLivro(novoLivro);

            Console.WriteLine($"Livro '{novoLivro.Titulo}' adicionado com sucesso!");
        }

        static void AdicionarLeitor()
        {
            Console.Write("Digite o nome do leitor: ");
            string nome = Console.ReadLine();

            Leitor novoLeitor = new Leitor(nome);
            biblioteca.AdicionarLeitor(novoLeitor);

            Console.WriteLine($"Leitor '{novoLeitor.Nome}' adicionado com sucesso!");
        }

        static void EmprestarLivro()
        {
            Console.Write("Digite o título do livro a ser emprestado: ");
            string tituloLivro = Console.ReadLine();

            Livro livroParaEmprestar = biblioteca.Livros.Find(l => l.Titulo.Equals(tituloLivro, StringComparison.OrdinalIgnoreCase));

            if (livroParaEmprestar == null)
            {
                Console.WriteLine("Livro não encontrado.");
                return;
            }

            Console.Write("Digite o nome do leitor que irá emprestar o livro: ");
            string nomeLeitor = Console.ReadLine();

            Leitor leitorParaEmprestar = biblioteca.Leitores.Find(l => l.Nome.Equals(nomeLeitor, StringComparison.OrdinalIgnoreCase));

            if (leitorParaEmprestar == null)
            {
                Console.WriteLine("Leitor não encontrado.");
                return;
            }

            biblioteca.EmprestarLivro(livroParaEmprestar, leitorParaEmprestar);
        }

        static void DevolverLivro()
        {
            Console.Write("Digite o título do livro a ser devolvido: ");
            string tituloLivro = Console.ReadLine();

            // Encontrar o empréstimo ativo para o livro especificado
            Emprestimo emprestimoParaDevolver = biblioteca.Emprestimos.Find(e => e.Livro.Titulo.Equals(tituloLivro, StringComparison.OrdinalIgnoreCase) && e.DataDevolucao == null);

            if (emprestimoParaDevolver == null)
            {
                Console.WriteLine("Empréstimo não encontrado ou livro já devolvido.");
                return;
            }

            biblioteca.DevolverLivro(emprestimoParaDevolver);
        }
    }
}