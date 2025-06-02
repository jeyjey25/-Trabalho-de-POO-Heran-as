using System;
using System.Collections.Generic;

namespace SistemaEscolarInterativo
{
    // Superclasse Pessoa
    public class Pessoa
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }

        public Pessoa(string nome, string cpf, DateTime dataNascimento)
        {
            Nome = nome;
            CPF = cpf;
            DataNascimento = dataNascimento;
        }

        public override string ToString()
        {
            return $"Nome: {Nome}, CPF: {CPF}, Data de Nascimento: {DataNascimento.ToShortDateString()}";
        }
    }

    // Subclasse Aluno
    public class Aluno : Pessoa
    {
        public string Matricula { get; set; }
        public string Turma { get; set; }

        public Aluno(string nome, string cpf, DateTime dataNascimento, string matricula, string turma) : base(nome, cpf, dataNascimento)
        {
            Matricula = matricula;
            Turma = turma;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Matrícula: {Matricula}, Turma: {Turma}";
        }
    }

    // Subclasse Professor
    public class Professor : Pessoa
    {
        public string Disciplinas { get; set; }

        public Professor(string nome, string cpf, DateTime dataNascimento, string disciplinas) : base(nome, cpf, dataNascimento)
        {
            Disciplinas = disciplinas;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Disciplinas: {Disciplinas}";
        }
    }

    // Subclasse Funcionario
    public class Funcionario : Pessoa
    {
        public string Setor { get; set; }

        public Funcionario(string nome, string cpf, DateTime dataNascimento, string setor) : base(nome, cpf, dataNascimento)
        {
            Setor = setor;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Setor: {Setor}";
        }
    }

    // Classe para gerenciar o Sistema Escolar
    public class SistemaEscolar
    {
        public List<Pessoa> Pessoas { get; set; } = new List<Pessoa>();

        public void AdicionarPessoa(Pessoa pessoa)
        {
            Pessoas.Add(pessoa);
        }

        public void ListarPessoas()
        {
            Console.WriteLine("\nLista de Pessoas no Sistema Escolar:");
            foreach (var pessoa in Pessoas)
            {
                Console.WriteLine(pessoa); // Polimorfismo: ToString() é chamado de acordo com o tipo da pessoa
            }
        }
    }

    // Classe Principal (Programa)
    class Program
    {
        static SistemaEscolar sistema = new SistemaEscolar();

        static void Main(string[] args)
        {
            ExibirMenu();
        }

        static void ExibirMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Menu do Sistema Escolar ---");
                Console.WriteLine("1. Adicionar Aluno");
                Console.WriteLine("2. Adicionar Professor");
                Console.WriteLine("3. Adicionar Funcionário");
                Console.WriteLine("4. Listar Pessoas");
                Console.WriteLine("5. Sair");

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarAluno();
                        break;
                    case "2":
                        AdicionarProfessor();
                        break;
                    case "3":
                        AdicionarFuncionario();
                        break;
                    case "4":
                        sistema.ListarPessoas();
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
            Console.Write("Nome do aluno: ");
            string nome = Console.ReadLine();
            Console.Write("CPF do aluno: ");
            string cpf = Console.ReadLine();
            Console.Write("Data de nascimento do aluno (dd/MM/yyyy): ");
            DateTime dataNascimento = DateTime.Parse(Console.ReadLine()); // Tratar exceção se a data for inválida
            Console.Write("Matrícula do aluno: ");
            string matricula = Console.ReadLine();
            Console.Write("Turma do aluno: ");
            string turma = Console.ReadLine();

            Aluno novoAluno = new Aluno(nome, cpf, dataNascimento, matricula, turma);
            sistema.AdicionarPessoa(novoAluno);

            Console.WriteLine("Aluno adicionado com sucesso!");
        }

        static void AdicionarProfessor()
        {
            Console.Write("Nome do professor: ");
            string nome = Console.ReadLine();
            Console.Write("CPF do professor: ");
            string cpf = Console.ReadLine();
            Console.Write("Data de nascimento do professor (dd/MM/yyyy): ");
            DateTime dataNascimento = DateTime.Parse(Console.ReadLine()); // Tratar exceção se a data for inválida
            Console.Write("Disciplinas do professor: ");
            string disciplinas = Console.ReadLine();

            Professor novoProfessor = new Professor(nome, cpf, dataNascimento, disciplinas);
            sistema.AdicionarPessoa(novoProfessor);

            Console.WriteLine("Professor adicionado com sucesso!");
        }

        static void AdicionarFuncionario()
        {
            Console.Write("Nome do funcionário: ");
            string nome = Console.ReadLine();
            Console.Write("CPF do funcionário: ");
            string cpf = Console.ReadLine();
            Console.Write("Data de nascimento do funcionário (dd/MM/yyyy): ");
            DateTime dataNascimento = DateTime.Parse(Console.ReadLine()); // Tratar exceção se a data for inválida
            Console.Write("Setor do funcionário: ");
            string setor = Console.ReadLine();

            Funcionario novoFuncionario = new Funcionario(nome, cpf, dataNascimento, setor);
            sistema.AdicionarPessoa(novoFuncionario);

            Console.WriteLine("Funcionário adicionado com sucesso!");
        }
    }
}
