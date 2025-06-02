using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleTreinos
{
    // Enum para Objetivos Físicos
    public enum ObjetivoFisico
    {
        Hipertrofia,
        Resistencia,
        Emagrecimento,
        Forca
    }

    // Classe Aluno
    public class Aluno
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public ObjetivoFisico Objetivo { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
        public List<Treino> Treinos { get; set; } = new List<Treino>();

        public Aluno(string nome, int idade, ObjetivoFisico objetivo, double peso, double altura)
        {
            Nome = nome;
            Idade = idade;
            Objetivo = objetivo;
            Peso = peso;
            Altura = altura;
        }

        public void AdicionarTreino(Treino treino)
        {
            Treinos.Add(treino);
        }

        public override string ToString()
        {
            return $"Nome: {Nome}, Idade: {Idade}, Objetivo: {Objetivo}, Peso: {Peso} kg, Altura: {Altura} m";
        }
    }

    // Enum para Tipo de Treino
    public enum TipoTreino
    {
        Superior,
        Inferior,
        Cardio,
        Abdominal
    }

    // Classe Treino
    public class Treino
    {
        public DateTime DataCriacao { get; set; }
        public ObjetivoFisico Objetivo { get; set; }
        public TipoTreino Tipo { get; set; }
        public List<Exercicio> Exercicios { get; set; } = new List<Exercicio>(); // Composição

        public Treino(DateTime dataCriacao, ObjetivoFisico objetivo, TipoTreino tipo)
        {
            DataCriacao = dataCriacao;
            Objetivo = objetivo;
            Tipo = tipo;
        }

        public void AdicionarExercicio(Exercicio exercicio)
        {
            Exercicios.Add(exercicio);
        }

        public double CalcularCargaTotal()
        {
            return Exercicios.Sum(e => e.Series * e.Repeticoes * e.Carga);
        }

        public override string ToString()
        {
            return $"Data: {DataCriacao.ToShortDateString()}, Objetivo: {Objetivo}, Tipo: {Tipo}, Carga Total: {CalcularCargaTotal()}";
        }
    }

    // Classe base Exercicio
    public class Exercicio
    {
        public string Nome { get; set; }
        public int Series { get; set; }
        public int Repeticoes { get; set; }
        public double Carga { get; set; }

        public Exercicio(string nome, int series, int repeticoes, double carga)
        {
            Nome = nome;
            Series = series;
            Repeticoes = repeticoes;
            Carga = carga;
        }

        public override string ToString()
        {
            return $"Nome: {Nome}, Séries: {Series}, Repetições: {Repeticoes}, Carga: {Carga} kg";
        }
    }

    // Subclasse ExercicioCardio
    public class ExercicioCardio : Exercicio
    {
        public int DuracaoMinutos { get; set; }

        public ExercicioCardio(string nome, int duracaoMinutos) : base(nome, 1, 1, 0) // Valores padrão para séries, repetições e carga
        {
            DuracaoMinutos = duracaoMinutos;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Duração: {DuracaoMinutos} minutos";
        }
    }

    // Subclasse ExercicioMusculacao
    public class ExercicioMusculacao : Exercicio
    {
        public ExercicioMusculacao(string nome, int series, int repeticoes, double carga) : base(nome, series, repeticoes, carga)
        {
        }
    }

    // Classe para gerenciar o Sistema de Treinos
    public class SistemaTreinos
    {
        public List<Aluno> Alunos { get; set; } = new List<Aluno>();

        public void AdicionarAluno(Aluno aluno)
        {
            Alunos.Add(aluno);
        }

        public void ListarAlunos()
        {
            Console.WriteLine("\nLista de Alunos:");
            foreach (var aluno in Alunos)
            {
                Console.WriteLine(aluno);
                Console.WriteLine("Treinos:");
                foreach (var treino in aluno.Treinos)
                {
                    Console.WriteLine($"  - {treino}");
                    Console.WriteLine("  Exercícios:");
                    foreach (var exercicio in treino.Exercicios)
                    {
                        Console.WriteLine($"    - {exercicio}");
                    }
                }
            }
        }
    }

    // Classe Principal (Programa)
    class Program
    {
        static SistemaTreinos sistema = new SistemaTreinos();

        static void Main(string[] args)
        {
            ExibirMenu();
        }

        static void ExibirMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Menu do Sistema de Controle de Treinos ---");
                Console.WriteLine("1. Adicionar Aluno");
                Console.WriteLine("2. Adicionar Treino a Aluno");
                Console.WriteLine("3. Adicionar Exercício a Treino");
                Console.WriteLine("4. Listar Alunos");
                Console.WriteLine("5. Sair");

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarAluno();
                        break;
                    case "2":
                        AdicionarTreino();
                        break;
                    case "3":
                        AdicionarExercicio();
                        break;
                    case "4":
                        sistema.ListarAlunos();
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

        static void AdicionarAluno()
        {
            Console.WriteLine("\n--- Adicionar Aluno ---");

            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Idade: ");
            if (!int.TryParse(Console.ReadLine(), out int idade))
            {
                Console.WriteLine("Idade inválida.");
                return;
            }

            Console.WriteLine("Objetivos Físicos:");
            Console.WriteLine("0. Hipertrofia");
            Console.WriteLine("1. Resistência");
            Console.WriteLine("2. Emagrecimento");
            Console.WriteLine("3. Força");
            Console.Write("Escolha o objetivo (0-3): ");
            if (!int.TryParse(Console.ReadLine(), out int objetivoEscolhido) || objetivoEscolhido < 0 || objetivoEscolhido > 3)
            {
                Console.WriteLine("Objetivo inválido.");
                return;
            }
            ObjetivoFisico objetivo = (ObjetivoFisico)objetivoEscolhido;

            Console.Write("Peso (kg): ");
            if (!double.TryParse(Console.ReadLine(), out double peso))
            {
                Console.WriteLine("Peso inválido.");
                return;
            }

            Console.Write("Altura (m): ");
            if (!double.TryParse(Console.ReadLine(), out double altura))
            {
                Console.WriteLine("Altura inválida.");
                return;
            }

            Aluno novoAluno = new Aluno(nome, idade, objetivo, peso, altura);
            sistema.AdicionarAluno(novoAluno);

            Console.WriteLine("Aluno adicionado com sucesso!");
        }

        static void AdicionarTreino()
        {
            Console.WriteLine("\n--- Adicionar Treino ---");

            Console.Write("Nome do aluno: ");
            string nomeAluno = Console.ReadLine();

            Aluno aluno = sistema.Alunos.Find(a => a.Nome == nomeAluno);
            if (aluno == null)
            {
                Console.WriteLine("Aluno não encontrado.");
                return;
            }

            Console.Write("Data de criação (dd/mm/aaaa): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataCriacao))
            {
                Console.WriteLine("Data inválida.");
                return;
            }

            Console.WriteLine("Objetivos Físicos:");
            Console.WriteLine("0. Hipertrofia");
            Console.WriteLine("1. Resistência");
            Console.WriteLine("2. Emagrecimento");
            Console.WriteLine("3. Força");
            Console.Write("Escolha o objetivo (0-3): ");
            if (!int.TryParse(Console.ReadLine(), out int objetivoEscolhido) || objetivoEscolhido < 0 || objetivoEscolhido > 3)
            {
                Console.WriteLine("Objetivo inválido.");
                return;
            }
            ObjetivoFisico objetivo = (ObjetivoFisico)objetivoEscolhido;

            Console.WriteLine("Tipos de Treino:");
            Console.WriteLine("0. Superior");
            Console.WriteLine("1. Inferior");
            Console.WriteLine("2. Cardio");
            Console.WriteLine("3. Abdominal");
            Console.Write("Escolha o tipo (0-3): ");
            if (!int.TryParse(Console.ReadLine(), out int tipoEscolhido) || tipoEscolhido < 0 || tipoEscolhido > 3)
            {
                Console.WriteLine("Tipo inválido.");
                return;
            }
            TipoTreino tipo = (TipoTreino)tipoEscolhido;

            Treino novoTreino = new Treino(dataCriacao, objetivo, tipo);
            aluno.AdicionarTreino(novoTreino);

            Console.WriteLine("Treino adicionado com sucesso!");
        }

        static void AdicionarExercicio()
        {
            Console.WriteLine("\n--- Adicionar Exercício ---");

            Console.Write("Nome do aluno: ");
            string nomeAluno = Console.ReadLine();

            Aluno aluno = sistema.Alunos.Find(a => a.Nome == nomeAluno);
            if (aluno == null)
            {
                Console.WriteLine("Aluno não encontrado.");
                return;
            }

            Console.Write("Data do treino (dd/mm/aaaa): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataTreino))
            {
                Console.WriteLine("Data inválida.");
                return;
            }

            Treino treino = aluno.Treinos.Find(t => t.DataCriacao.Date == dataTreino.Date);
            if (treino == null)
            {
                Console.WriteLine("Treino não encontrado.");
                return;
            }

            Console.WriteLine("Tipos de Exercício:");
            Console.WriteLine("1. Exercício de Musculação");
            Console.WriteLine("2. Exercício de Cardio");
            Console.Write("Escolha o tipo (1-2): ");
            if (!int.TryParse(Console.ReadLine(), out int tipoExercicioEscolhido) || tipoExercicioEscolhido < 1 || tipoExercicioEscolhido > 2)
            {
                Console.WriteLine("Tipo inválido.");
                return;
            }

            switch (tipoExercicioEscolhido)
            {
                case 1:
                    AdicionarExercicioMusculacao(treino);
                    break;
                case 2:
                    AdicionarExercicioCardio(treino);
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }

        static void AdicionarExercicioMusculacao(Treino treino)
        {
            Console.Write("Nome do exercício: ");
            string nomeExercicio = Console.ReadLine();

            Console.Write("Número de séries: ");
            if (!int.TryParse(Console.ReadLine(), out int series))
            {
                Console.WriteLine("Número de séries inválido.");
                return;
            }

            Console.Write("Número de repetições: ");
            if (!int.TryParse(Console.ReadLine(), out int repeticoes))
            {
                Console.WriteLine("Número de repetições inválido.");
                return;
            }

            Console.Write("Carga (kg): ");
            if (!double.TryParse(Console.ReadLine(), out double carga))
            {
                Console.WriteLine("Carga inválida.");
                return;
            }

            ExercicioMusculacao novoExercicio = new ExercicioMusculacao(nomeExercicio, series, repeticoes, carga);
            treino.AdicionarExercicio(novoExercicio);

            Console.WriteLine("Exercício de musculação adicionado com sucesso!");
        }

        static void AdicionarExercicioCardio(Treino treino)
        {
            Console.Write("Nome do exercício: ");
            string nomeExercicio = Console.ReadLine();

            Console.Write("Duração (minutos): ");
            if (!int.TryParse(Console.ReadLine(), out int duracao))
            {
                Console.WriteLine("Duração inválida.");
                return;
            }

            ExercicioCardio novoExercicio = new ExercicioCardio(nomeExercicio, duracao);
            treino.AdicionarExercicio(novoExercicio);

            Console.WriteLine("Exercício de cardio adicionado com sucesso!");
        }
    }
}