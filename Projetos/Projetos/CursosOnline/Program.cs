using System;
using System.Collections.Generic;

namespace PlataformaCursosOnline
{
    // Classe Aula
    public class Aula
    {
        public string Titulo { get; set; }
        public int Duracao { get; set; } // em minutos
        public string ProfessorResponsavel { get; set; }

        public Aula(string titulo, int duracao, string professorResponsavel)
        {
            Titulo = titulo;
            Duracao = duracao;
            ProfessorResponsavel = professorResponsavel;
        }

        public override string ToString()
        {
            return $"Título: {Titulo}, Duração: {Duracao} minutos, Professor: {ProfessorResponsavel}";
        }
    }

    // Classe Curso
    public class Curso
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<Aula> Aulas { get; set; } = new List<Aula>(); // Composição: Aulas existem apenas dentro do Curso

        public Curso(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
        }

        public void AdicionarAula(Aula aula)
        {
            Aulas.Add(aula);
        }

        public void RemoverAula(Aula aula)
        {
            Aulas.Remove(aula);
        }

        public override string ToString()
        {
            return $"Nome: {Nome}, Descrição: {Descricao}, Número de Aulas: {Aulas.Count}";
        }
    }

    // Classe Aluno
    public class Aluno
    {
        public string Nome { get; set; }
        public string Email { get; set; }

        public Aluno(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

        public override string ToString()
        {
            return $"Nome: {Nome}, Email: {Email}";
        }
    }

    // Classe Matricula (Classe Intermediária para relação N:N entre Aluno e Curso)
    public class Matricula
    {
        public Aluno Aluno { get; set; }
        public Curso Curso { get; set; }
        public DateTime DataInscricao { get; set; }
        public double Progresso { get; set; } // Porcentagem de progresso (0 a 100)

        public Matricula(Aluno aluno, Curso curso)
        {
            Aluno = aluno;
            Curso = curso;
            DataInscricao = DateTime.Now;
            Progresso = 0;
        }

        public override string ToString()
        {
            return $"Aluno: {Aluno.Nome}, Curso: {Curso.Nome}, Inscrição: {DataInscricao.ToShortDateString()}, Progresso: {Progresso}%";
        }
    }

    // Classe para gerenciar a Plataforma de Cursos Online
    public class Plataforma
    {
        public List<Curso> Cursos { get; set; } = new List<Curso>();
        public List<Aluno> Alunos { get; set; } = new List<Aluno>();
        public List<Matricula> Matriculas { get; set; } = new List<Matricula>();

        public void AdicionarCurso(Curso curso)
        {
            Cursos.Add(curso);
        }

        public void RemoverCurso(Curso curso)
        {
            // Composição: Ao remover o curso, remove também as aulas
            Cursos.Remove(curso);
            //Remover as aulas não é necessário pois elas são removidas automaticamente quando o curso é removido.
        }

        public void AdicionarAluno(Aluno aluno)
        {
            Alunos.Add(aluno);
        }

        public void MatricularAluno(Aluno aluno, Curso curso)
        {
            Matricula matricula = new Matricula(aluno, curso);
            Matriculas.Add(matricula);
            Console.WriteLine($"Aluno '{aluno.Nome}' matriculado no curso '{curso.Nome}' em {matricula.DataInscricao.ToShortDateString()}.");
        }

        public void ListarCursos()
        {
            Console.WriteLine("\nLista de Cursos Disponíveis:");
            foreach (var curso in Cursos)
            {
                Console.WriteLine(curso);
            }
        }

        public void ListarAlunos()
        {
            Console.WriteLine("\nLista de Alunos Cadastrados:");
            foreach (var aluno in Alunos)
            {
                Console.WriteLine(aluno);
            }
        }

        public void ListarMatriculas()
        {
            Console.WriteLine("\nLista de Matrículas:");
            foreach (var matricula in Matriculas)
            {
                Console.WriteLine(matricula);
            }
        }
    }

    // Classe Principal (Programa)
    class Program
    {
        static Plataforma plataforma = new Plataforma();

        static void Main(string[] args)
        {
            ExibirMenu();
        }

        static void ExibirMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Menu da Plataforma de Cursos Online ---");
                Console.WriteLine("1. Adicionar Curso");
                Console.WriteLine("2. Adicionar Aluno");
                Console.WriteLine("3. Adicionar Aula a um Curso");
                Console.WriteLine("4. Matricular Aluno em Curso");
                Console.WriteLine("5. Listar Cursos");
                Console.WriteLine("6. Listar Alunos");
                Console.WriteLine("7. Listar Matrículas");
                Console.WriteLine("8. Sair");

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarCurso();
                        break;
                    case "2":
                        AdicionarAluno();
                        break;
                    case "3":
                        AdicionarAulaAoCurso();
                        break;
                    case "4":
                        MatricularAlunoEmCurso();
                        break;
                    case "5":
                        plataforma.ListarCursos();
                        break;
                    case "6":
                        plataforma.ListarAlunos();
                        break;
                    case "7":
                        plataforma.ListarMatriculas();
                        break;
                    case "8":
                        Console.WriteLine("Saindo do sistema...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static void AdicionarCurso()
        {
            Console.Write("Nome do curso: ");
            string nome = Console.ReadLine();
            Console.Write("Descrição do curso: ");
            string descricao = Console.ReadLine();

            Curso novoCurso = new Curso(nome, descricao);
            plataforma.AdicionarCurso(novoCurso);

            Console.WriteLine("Curso adicionado com sucesso!");
        }

        static void AdicionarAluno()
        {
            Console.Write("Nome do aluno: ");
            string nome = Console.ReadLine();
            Console.Write("Email do aluno: ");
            string email = Console.ReadLine();

            Aluno novoAluno = new Aluno(nome, email);
            plataforma.AdicionarAluno(novoAluno);

            Console.WriteLine("Aluno adicionado com sucesso!");
        }

        static void AdicionarAulaAoCurso()
        {
            Console.Write("Nome do curso para adicionar a aula: ");
            string nomeCurso = Console.ReadLine();

            Curso cursoParaAdicionarAula = plataforma.Cursos.Find(c => c.Nome.Equals(nomeCurso, StringComparison.OrdinalIgnoreCase));

            if (cursoParaAdicionarAula == null)
            {
                Console.WriteLine("Curso não encontrado.");
                return;
            }

            Console.Write("Título da aula: ");
            string tituloAula = Console.ReadLine();
            Console.Write("Duração da aula (em minutos): ");
            int duracaoAula = int.Parse(Console.ReadLine()); // Tratar exceção se a entrada não for um número
            Console.Write("Professor responsável pela aula: ");
            string professorAula = Console.ReadLine();

            Aula novaAula = new Aula(tituloAula, duracaoAula, professorAula);
            cursoParaAdicionarAula.AdicionarAula(novaAula);

            Console.WriteLine("Aula adicionada ao curso com sucesso!");
        }

        static void MatricularAlunoEmCurso()
        {
            Console.Write("Nome do aluno para matricular: ");
            string nomeAluno = Console.ReadLine();

            Aluno alunoParaMatricular = plataforma.Alunos.Find(a => a.Nome.Equals(nomeAluno, StringComparison.OrdinalIgnoreCase));

            if (alunoParaMatricular == null)
            {
                Console.WriteLine("Aluno não encontrado.");
                return;
            }

            Console.Write("Nome do curso para matricular o aluno: ");
            string nomeCurso = Console.ReadLine();

            Curso cursoParaMatricular = plataforma.Cursos.Find(c => c.Nome.Equals(nomeCurso, StringComparison.OrdinalIgnoreCase));

            if (cursoParaMatricular == null)
            {
                Console.WriteLine("Curso não encontrado.");
                return;
            }

            plataforma.MatricularAluno(alunoParaMatricular, cursoParaMatricular);
        }
    }
}
