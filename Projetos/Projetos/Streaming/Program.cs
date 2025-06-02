using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaStreaming
{
    // Enum para Gênero
    public enum Genero
    {
        Acao,
        Comedia,
        Drama,
        Suspense,
        Documentario
    }

    // Classe base Midia
    public class Midia
    {
        public string Titulo { get; set; }
        public int Duracao { get; set; } // em minutos
        public Genero Genero { get; set; }

        public Midia(string titulo, int duracao, Genero genero)
        {
            Titulo = titulo;
            Duracao = duracao;
            Genero = genero;
        }

        public virtual void ExibirResumo()
        {
            Console.WriteLine($"Título: {Titulo}, Duração: {Duracao} minutos, Gênero: {Genero}");
        }

        public override string ToString()
        {
            return $"Título: {Titulo}, Duração: {Duracao} minutos, Gênero: {Genero}";
        }
    }

    // Subclasse Filme
    public class Filme : Midia
    {
        public string Diretor { get; set; }
        public int Ano { get; set; }
        public List<string> Elenco { get; set; } = new List<string>();

        public Filme(string titulo, int duracao, Genero genero, string diretor, int ano) : base(titulo, duracao, genero)
        {
            Diretor = diretor;
            Ano = ano;
        }

        public override void ExibirResumo()
        {
            base.ExibirResumo();
            Console.WriteLine($"Diretor: {Diretor}, Ano: {Ano}, Elenco: {string.Join(", ", Elenco)}");
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Diretor: {Diretor}, Ano: {Ano}, Elenco: {string.Join(", ", Elenco)}";
        }
    }

    // Subclasse Serie
    public class Serie : Midia
    {
        public int Temporadas { get; set; }
        public int NumeroEpisodios { get; set; }
        public List<Episodio> Episodios { get; set; } = new List<Episodio>(); // Composição

        public Serie(string titulo, Genero genero, int temporadas, int numeroEpisodios) : base(titulo, 0, genero) // Duração 0 para série
        {
            Temporadas = temporadas;
            NumeroEpisodios = numeroEpisodios;
        }

        public override void ExibirResumo()
        {
            base.ExibirResumo();
            Console.WriteLine($"Temporadas: {Temporadas}, Número de Episódios: {NumeroEpisodios}");
            Console.WriteLine("Episódios:");
            foreach (var episodio in Episodios)
            {
                Console.WriteLine($"  - {episodio}");
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Temporadas: {Temporadas}, Número de Episódios: {NumeroEpisodios}";
        }
    }

    // Classe Episodio (para composição com Serie)
    public class Episodio
    {
        public string Titulo { get; set; }
        public int Duracao { get; set; }

        public Episodio(string titulo, int duracao)
        {
            Titulo = titulo;
            Duracao = duracao;
        }

        public override string ToString()
        {
            return $"Título: {Titulo}, Duração: {Duracao} minutos";
        }
    }

    // Subclasse Documentario
    public class Documentario : Midia
    {
        public string Tema { get; set; }
        public string Narrador { get; set; }

        public Documentario(string titulo, int duracao, Genero genero, string tema, string narrador) : base(titulo, duracao, genero)
        {
            Tema = tema;
            Narrador = narrador;
        }

        public override void ExibirResumo()
        {
            base.ExibirResumo();
            Console.WriteLine($"Tema: {Tema}, Narrador: {Narrador}");
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Tema: {Tema}, Narrador: {Narrador}";
        }
    }

    // Classe para gerenciar o Catálogo de Mídia
    public class CatalogoMidia
    {
        public List<Midia> Midias { get; set; } = new List<Midia>();

        public void AdicionarMidia(Midia midia)
        {
            Midias.Add(midia);
        }

        public void ListarMidias()
        {
            Console.WriteLine("\nCatálogo de Mídia:");
            foreach (var midia in Midias)
            {
                midia.ExibirResumo();
                Console.WriteLine();
            }
        }
    }

    // Classe Principal (Programa)
    class Program
    {
        static CatalogoMidia catalogo = new CatalogoMidia();

        static void Main(string[] args)
        {
            ExibirMenu();
        }

        static void ExibirMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Menu da Plataforma de Streaming ---");
                Console.WriteLine("1. Adicionar Mídia");
                Console.WriteLine("2. Listar Mídias");
                Console.WriteLine("3. Sair");

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarMidia();
                        break;
                    case "2":
                        catalogo.ListarMidias();
                        break;
                    case "3":
                        Console.WriteLine("Saindo do sistema...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static void AdicionarMidia()
        {
            Console.WriteLine("\n--- Adicionar Mídia ---");
            Console.WriteLine("1. Filme");
            Console.WriteLine("2. Série");
            Console.WriteLine("3. Documentário");

            Console.Write("Escolha o tipo de mídia (1-3): ");
            if (!int.TryParse(Console.ReadLine(), out int tipoEscolhido) || tipoEscolhido < 1 || tipoEscolhido > 3)
            {
                Console.WriteLine("Tipo inválido.");
                return;
            }

            switch (tipoEscolhido)
            {
                case 1:
                    AdicionarFilme();
                    break;
                case 2:
                    AdicionarSerie();
                    break;
                case 3:
                    AdicionarDocumentario();
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }

        static void AdicionarFilme()
        {
            Console.Write("Título: ");
            string titulo = Console.ReadLine();

            Console.Write("Duração (minutos): ");
            if (!int.TryParse(Console.ReadLine(), out int duracao))
            {
                Console.WriteLine("Duração inválida.");
                return;
            }

            Console.WriteLine("Gêneros:");
            Console.WriteLine("0. Ação");
            Console.WriteLine("1. Comédia");
            Console.WriteLine("2. Drama");
            Console.WriteLine("3. Suspense");
            Console.WriteLine("4. Documentário");
            Console.Write("Escolha o gênero (0-4): ");
            if (!int.TryParse(Console.ReadLine(), out int generoEscolhido) || generoEscolhido < 0 || generoEscolhido > 4)
            {
                Console.WriteLine("Gênero inválido.");
                return;
            }
            Genero genero = (Genero)generoEscolhido;

            Console.Write("Diretor: ");
            string diretor = Console.ReadLine();

            Console.Write("Ano: ");
            if (!int.TryParse(Console.ReadLine(), out int ano))
            {
                Console.WriteLine("Ano inválido.");
                return;
            }

            Filme novoFilme = new Filme(titulo, duracao, genero, diretor, ano);

            while (true)
            {
                Console.Write("Adicionar ator ao elenco? (s/n): ");
                string resposta = Console.ReadLine().ToLower();
                if (resposta == "s")
                {
                    Console.Write("Nome do ator: ");
                    string ator = Console.ReadLine();
                    novoFilme.Elenco.Add(ator);
                }
                else
                {
                    break;
                }
            }

            catalogo.AdicionarMidia(novoFilme);
            Console.WriteLine("Filme adicionado com sucesso!");
        }

        static void AdicionarSerie()
        {
            Console.Write("Título: ");
            string titulo = Console.ReadLine();

            Console.WriteLine("Gêneros:");
            Console.WriteLine("0. Ação");
            Console.WriteLine("1. Comédia");
            Console.WriteLine("2. Drama");
            Console.WriteLine("3. Suspense");
            Console.WriteLine("4. Documentário");
            Console.Write("Escolha o gênero (0-4): ");
            if (!int.TryParse(Console.ReadLine(), out int generoEscolhido) || generoEscolhido < 0 || generoEscolhido > 4)
            {
                Console.WriteLine("Gênero inválido.");
                return;
            }
            Genero genero = (Genero)generoEscolhido;

            Console.Write("Número de temporadas: ");
            if (!int.TryParse(Console.ReadLine(), out int temporadas))
            {
                Console.WriteLine("Número de temporadas inválido.");
                return;
            }

            Console.Write("Número total de episódios: ");
            if (!int.TryParse(Console.ReadLine(), out int numeroEpisodios))
            {
                Console.WriteLine("Número de episódios inválido.");
                return;
            }

            Serie novaSerie = new Serie(titulo, genero, temporadas, numeroEpisodios);

            while (true)
            {
                Console.Write("Adicionar episódio? (s/n): ");
                string resposta = Console.ReadLine().ToLower();
                if (resposta == "s")
                {
                    Console.Write("Título do episódio: ");
                    string tituloEpisodio = Console.ReadLine();

                    Console.Write("Duração do episódio (minutos): ");
                    if (!int.TryParse(Console.ReadLine(), out int duracaoEpisodio))
                    {
                        Console.WriteLine("Duração inválida.");
                        continue;
                    }

                    Episodio novoEpisodio = new Episodio(tituloEpisodio, duracaoEpisodio);
                    novaSerie.Episodios.Add(novoEpisodio);
                }
                else
                {
                    break;
                }
            }

            catalogo.AdicionarMidia(novaSerie);
            Console.WriteLine("Série adicionada com sucesso!");
        }

        static void AdicionarDocumentario()
        {
            Console.Write("Título: ");
            string titulo = Console.ReadLine();

            Console.Write("Duração (minutos): ");
            if (!int.TryParse(Console.ReadLine(), out int duracao))
            {
                Console.WriteLine("Duração inválida.");
                return;
            }

            Console.WriteLine("Gêneros:");
            Console.WriteLine("0. Ação");
            Console.WriteLine("1. Comédia");
            Console.WriteLine("2. Drama");
            Console.WriteLine("3. Suspense");
            Console.WriteLine("4. Documentário");
            Console.Write("Escolha o gênero (0-4): ");
            if (!int.TryParse(Console.ReadLine(), out int generoEscolhido) || generoEscolhido < 0 || generoEscolhido > 4)
            {
                Console.WriteLine("Gênero inválido.");
                return;
            }
            Genero genero = (Genero)generoEscolhido;

            Console.Write("Tema: ");
            string tema = Console.ReadLine();

            Console.Write("Narrador: ");
            string narrador = Console.ReadLine();

            Documentario novoDocumentario = new Documentario(titulo, duracao, genero, tema, narrador);
            catalogo.AdicionarMidia(novoDocumentario);

            Console.WriteLine("Documentário adicionado com sucesso!");
        }
    }
}