using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaRecrutamento
{
    // Enum para Status da Candidatura
    public enum StatusCandidatura
    {
        Enviada,
        EmAnalise,
        Aprovada,
        Rejeitada
    }

    // Classe Candidatura
    public class Candidatura
    {
        public Candidato Candidato { get; set; }
        public Vaga Vaga { get; set; }
        public DateTime DataEnvio { get; set; }
        public StatusCandidatura Status { get; set; }

        public Candidatura(Candidato candidato, Vaga vaga, DateTime dataEnvio)
        {
            Candidato = candidato;
            Vaga = vaga;
            DataEnvio = dataEnvio;
            Status = StatusCandidatura.Enviada; // Status inicial
        }

        public override string ToString()
        {
            return $"Candidato: {Candidato.Nome}, Vaga: {Vaga.Titulo}, Data: {DataEnvio.ToShortDateString()}, Status: {Status}";
        }
    }

    // Classe base Pessoa (para Candidato)
    public class Pessoa
    {
        public string Nome { get; set; }
        public string Email { get; set; }

        public Pessoa(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

        public override string ToString()
        {
            return $"Nome: {Nome}, Email: {Email}";
        }
    }

    // Classe base Candidato
    public class Candidato : Pessoa
    {
        public string Curriculo { get; set; }

        public Candidato(string nome, string email, string curriculo) : base(nome, email)
        {
            Curriculo = curriculo;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Currículo: {Curriculo}";
        }
    }

    // Subclasse CandidatoExperiente
    public class CandidatoExperiente : Candidato
    {
        public int AnosExperiencia { get; set; }

        public CandidatoExperiente(string nome, string email, string curriculo, int anosExperiencia) : base(nome, email, curriculo)
        {
            AnosExperiencia = anosExperiencia;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Anos de Experiência: {AnosExperiencia}";
        }
    }

    // Subclasse CandidatoRecemFormado
    public class CandidatoRecemFormado : Candidato
    {
        public string Formacao { get; set; }

        public CandidatoRecemFormado(string nome, string email, string curriculo, string formacao) : base(nome, email, curriculo)
        {
            Formacao = formacao;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Formação: {Formacao}";
        }
    }

    // Classe base Vaga
    public class Vaga
    {
        public string Titulo { get; set; }
        public string Empresa { get; set; }
        public string Descricao { get; set; }

        public Vaga(string titulo, string empresa, string descricao)
        {
            Titulo = titulo;
            Empresa = empresa;
            Descricao = descricao;
        }

        public override string ToString()
        {
            return $"Título: {Titulo}, Empresa: {Empresa}, Descrição: {Descricao}";
        }
    }

    // Subclasse VagaSenior
    public class VagaSenior : Vaga
    {
        public double Salario { get; set; }

        public VagaSenior(string titulo, string empresa, string descricao, double salario) : base(titulo, empresa, descricao)
        {
            Salario = salario;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Salário: {Salario:C}";
        }
    }

    // Subclasse VagaEstagio
    public class VagaEstagio : Vaga
    {
        public string Area { get; set; }

        public VagaEstagio(string titulo, string empresa, string descricao, string area) : base(titulo, empresa, descricao)
        {
            Area = area;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Área: {Area}";
        }
    }

    // Classe para gerenciar o Sistema de Recrutamento
    public class SistemaRecrutamento
    {
        public List<Vaga> Vagas { get; set; } = new List<Vaga>();
        public List<Candidato> Candidatos { get; set; } = new List<Candidato>();
        public List<Candidatura> Candidaturas { get; set; } = new List<Candidatura>();

        public void AdicionarVaga(Vaga vaga)
        {
            Vagas.Add(vaga);
        }

        public void AdicionarCandidato(Candidato candidato)
        {
            Candidatos.Add(candidato);
        }

        public void CriarCandidatura(Candidato candidato, Vaga vaga, DateTime dataEnvio)
        {
            Candidatura candidatura = new Candidatura(candidato, vaga, dataEnvio);
            Candidaturas.Add(candidatura);
        }

        public void ListarVagas()
        {
            Console.WriteLine("\nLista de Vagas:");
            foreach (var vaga in Vagas)
            {
                Console.WriteLine(vaga);
            }
        }

        public void ListarCandidatos()
        {
            Console.WriteLine("\nLista de Candidatos:");
            foreach (var candidato in Candidatos)
            {
                Console.WriteLine(candidato);
            }
        }

        public void ListarCandidaturas()
        {
            Console.WriteLine("\nLista de Candidaturas:");
            foreach (var candidatura in Candidaturas)
            {
                Console.WriteLine(candidatura);
            }
        }

        public List<Vaga> ListarVagasPorCandidato(Candidato candidato)
        {
            return Candidaturas.Where(c => c.Candidato == candidato).Select(c => c.Vaga).ToList();
        }

        public List<Candidato> ListarCandidatosPorVaga(Vaga vaga)
        {
            return Candidaturas.Where(c => c.Vaga == vaga).Select(c => c.Candidato).ToList();
        }
    }

    // Classe Principal (Programa)
    class Program
    {
        static SistemaRecrutamento sistema = new SistemaRecrutamento();

        static void Main(string[] args)
        {
            ExibirMenu();
        }

        static void ExibirMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Menu do Sistema de Recrutamento ---");
                Console.WriteLine("1. Adicionar Vaga");
                Console.WriteLine("2. Adicionar Candidato");
                Console.WriteLine("3. Criar Candidatura");
                Console.WriteLine("4. Listar Vagas");
                Console.WriteLine("5. Listar Candidatos");
                Console.WriteLine("6. Listar Candidaturas");
                Console.WriteLine("7. Listar Vagas por Candidato");
                Console.WriteLine("8. Listar Candidatos por Vaga");
                Console.WriteLine("9. Sair");

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarVaga();
                        break;
                    case "2":
                        AdicionarCandidato();
                        break;
                    case "3":
                        CriarCandidatura();
                        break;
                    case "4":
                        sistema.ListarVagas();
                        break;
                    case "5":
                        sistema.ListarCandidatos();
                        break;
                    case "6":
                        sistema.ListarCandidaturas();
                        break;
                    case "7":
                        ListarVagasPorCandidato();
                        break;
                    case "8":
                        ListarCandidatosPorVaga();
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

        static void AdicionarVaga()
        {
            Console.WriteLine("\n--- Adicionar Vaga ---");
            Console.WriteLine("1. Vaga Sênior");
            Console.WriteLine("2. Vaga de Estágio");

            Console.Write("Escolha o tipo de vaga (1-2): ");
            if (!int.TryParse(Console.ReadLine(), out int tipoEscolhido) || tipoEscolhido < 1 || tipoEscolhido > 2)
            {
                Console.WriteLine("Tipo inválido.");
                return;
            }

            switch (tipoEscolhido)
            {
                case 1:
                    AdicionarVagaSenior();
                    break;
                case 2:
                    AdicionarVagaEstagio();
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }

        static void AdicionarVagaSenior()
        {
            Console.Write("Título: ");
            string titulo = Console.ReadLine();

            Console.Write("Empresa: ");
            string empresa = Console.ReadLine();

            Console.Write("Descrição: ");
            string descricao = Console.ReadLine();

            Console.Write("Salário: ");
            if (!double.TryParse(Console.ReadLine(), out double salario))
            {
                Console.WriteLine("Salário inválido.");
                return;
            }

            VagaSenior novaVaga = new VagaSenior(titulo, empresa, descricao, salario);
            sistema.AdicionarVaga(novaVaga);

            Console.WriteLine("Vaga Sênior adicionada com sucesso!");
        }

        static void AdicionarVagaEstagio()
        {
            Console.Write("Título: ");
            string titulo = Console.ReadLine();

            Console.Write("Empresa: ");
            string empresa = Console.ReadLine();

            Console.Write("Descrição: ");
            string descricao = Console.ReadLine();

            Console.Write("Área: ");
            string area = Console.ReadLine();

            VagaEstagio novaVaga = new VagaEstagio(titulo, empresa, descricao, area);
            sistema.AdicionarVaga(novaVaga);

            Console.WriteLine("Vaga de Estágio adicionada com sucesso!");
        }

        static void AdicionarCandidato()
        {
            Console.WriteLine("\n--- Adicionar Candidato ---");
            Console.WriteLine("1. Candidato Experiente");
            Console.WriteLine("2. Candidato Recém-Formado");

            Console.Write("Escolha o tipo de candidato (1-2): ");
            if (!int.TryParse(Console.ReadLine(), out int tipoEscolhido) || tipoEscolhido < 1 || tipoEscolhido > 2)
            {
                Console.WriteLine("Tipo inválido.");
                return;
            }

            switch (tipoEscolhido)
            {
                case 1:
                    AdicionarCandidatoExperiente();
                    break;
                case 2:
                    AdicionarCandidatoRecemFormado();
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }

        static void AdicionarCandidatoExperiente()
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Currículo: ");
            string curriculo = Console.ReadLine();

            Console.Write("Anos de Experiência: ");
            if (!int.TryParse(Console.ReadLine(), out int anosExperiencia))
            {
                Console.WriteLine("Anos de experiência inválidos.");
                return;
            }

            CandidatoExperiente novoCandidato = new CandidatoExperiente(nome, email, curriculo, anosExperiencia);
            sistema.AdicionarCandidato(novoCandidato);

            Console.WriteLine("Candidato Experiente adicionado com sucesso!");
        }

        static void AdicionarCandidatoRecemFormado()
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Currículo: ");
            string curriculo = Console.ReadLine();

            Console.Write("Formação: ");
            string formacao = Console.ReadLine();

            CandidatoRecemFormado novoCandidato = new CandidatoRecemFormado(nome, email, curriculo, formacao);
            sistema.AdicionarCandidato(novoCandidato);

            Console.WriteLine("Candidato Recém-Formado adicionado com sucesso!");
        }

        static void CriarCandidatura()
        {
            Console.WriteLine("\n--- Criar Candidatura ---");

            Console.Write("Email do candidato: ");
            string emailCandidato = Console.ReadLine();

            Candidato candidato = sistema.Candidatos.Find(c => c.Email == emailCandidato);
            if (candidato == null)
            {
                Console.WriteLine("Candidato não encontrado.");
                return;
            }

            Console.Write("Título da vaga: ");
            string tituloVaga = Console.ReadLine();

            Vaga vaga = sistema.Vagas.Find(v => v.Titulo == tituloVaga);
            if (vaga == null)
            {
                Console.WriteLine("Vaga não encontrada.");
                return;
            }

            Console.Write("Data de envio (dd/mm/aaaa): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataEnvio))
            {
                Console.WriteLine("Data inválida.");
                return;
            }

            sistema.CriarCandidatura(candidato, vaga, dataEnvio);
            Console.WriteLine("Candidatura criada com sucesso!");
        }

        static void ListarVagasPorCandidato()
        {
            Console.Write("Email do candidato: ");
            string emailCandidato = Console.ReadLine();

            Candidato candidato = sistema.Candidatos.Find(c => c.Email == emailCandidato);
            if (candidato == null)
            {
                Console.WriteLine("Candidato não encontrado.");
                return;
            }

            List<Vaga> vagas = sistema.ListarVagasPorCandidato(candidato);

            Console.WriteLine($"\nVagas às quais {candidato.Nome} se candidatou:");
            foreach (var vaga in vagas)
            {
                Console.WriteLine(vaga);
            }
        }

        static void ListarCandidatosPorVaga()
        {
            Console.Write("Título da vaga: ");
            string tituloVaga = Console.ReadLine();

            Vaga vaga = sistema.Vagas.Find(v => v.Titulo == tituloVaga);
            if (vaga == null)
            {
                Console.WriteLine("Vaga não encontrada.");
                return;
            }

            List<Candidato> candidatos = sistema.ListarCandidatosPorVaga(vaga);

            Console.WriteLine($"\nCandidatos que se candidataram à vaga {vaga.Titulo}:");
            foreach (var candidato in candidatos)
            {
                Console.WriteLine(candidato);
            }
        }
    }
}
