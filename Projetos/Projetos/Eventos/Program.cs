using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaEventosCulturais
{
    // Classe base Evento
    public class Evento
    {
        public string Titulo { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Horario { get; set; }
        public string Local { get; set; }
        public int Capacidade { get; set; }
        public List<Participante> ParticipantesInscritos { get; set; } = new List<Participante>(); // Associação muitos-para-muitos

        public Evento(string titulo, DateTime data, TimeSpan horario, string local, int capacidade)
        {
            Titulo = titulo;
            Data = data;
            Horario = horario;
            Local = local;
            Capacidade = capacidade;
        }

        public virtual void ExibirInformacoes()
        {
            Console.WriteLine($"Título: {Titulo}");
            Console.WriteLine($"Data: {Data.ToShortDateString()}");
            Console.WriteLine($"Horário: {Horario}");
            Console.WriteLine($"Local: {Local}");
            Console.WriteLine($"Capacidade: {Capacidade}");
        }

        public override string ToString()
        {
            return $"Título: {Titulo}, Data: {Data.ToShortDateString()}, Horário: {Horario}, Local: {Local}, Capacidade: {Capacidade}";
        }

        public void AdicionarParticipante(Participante participante)
        {
            if (ParticipantesInscritos.Count < Capacidade)
            {
                ParticipantesInscritos.Add(participante);
                Console.WriteLine($"Participante {participante.Nome} inscrito no evento {Titulo} com sucesso!");
            }
            else
            {
                Console.WriteLine($"Não foi possível inscrever {participante.Nome} no evento {Titulo}. Capacidade máxima atingida.");
            }
        }
    }

    // Subclasse Oficina
    public class Oficina : Evento
    {
        public string MaterialNecessario { get; set; }
        public int NumeroMaximoParticipantes { get; set; }

        public Oficina(string titulo, DateTime data, TimeSpan horario, string local, int capacidade, string materialNecessario, int numeroMaximoParticipantes) : base(titulo, data, horario, local, capacidade)
        {
            MaterialNecessario = materialNecessario;
            NumeroMaximoParticipantes = numeroMaximoParticipantes;
        }

        public override void ExibirInformacoes()
        {
            base.ExibirInformacoes();
            Console.WriteLine($"Material Necessário: {MaterialNecessario}");
            Console.WriteLine($"Número Máximo de Participantes: {NumeroMaximoParticipantes}");
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Material Necessário: {MaterialNecessario}, Número Máximo de Participantes: {NumeroMaximoParticipantes}";
        }
    }

    // Subclasse Palestra
    public class Palestra : Evento
    {
        public string Palestrante { get; set; }
        public string Topico { get; set; }
        public TimeSpan DuracaoPrevista { get; set; }

        public Palestra(string titulo, DateTime data, TimeSpan horario, string local, int capacidade, string palestrante, string topico, TimeSpan duracaoPrevista) : base(titulo, data, horario, local, capacidade)
        {
            Palestrante = palestrante;
            Topico = topico;
            DuracaoPrevista = duracaoPrevista;
        }

        public override void ExibirInformacoes()
        {
            base.ExibirInformacoes();
            Console.WriteLine($"Palestrante: {Palestrante}");
            Console.WriteLine($"Tópico: {Topico}");
            Console.WriteLine($"Duração Prevista: {DuracaoPrevista}");
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Palestrante: {Palestrante}, Tópico: {Topico}, Duração Prevista: {DuracaoPrevista}";
        }
    }

    // Subclasse Show
    public class Show : Evento
    {
        public string BandaArtista { get; set; }
        public string EstiloMusical { get; set; }
        public int ClassificacaoEtaria { get; set; }

        public Show(string titulo, DateTime data, TimeSpan horario, string local, int capacidade, string bandaArtista, string estiloMusical, int classificacaoEtaria) : base(titulo, data, horario, local, capacidade)
        {
            BandaArtista = bandaArtista;
            EstiloMusical = estiloMusical;
            ClassificacaoEtaria = classificacaoEtaria;
        }

        public override void ExibirInformacoes()
        {
            base.ExibirInformacoes();
            Console.WriteLine($"Banda/Artista: {BandaArtista}");
            Console.WriteLine($"Estilo Musical: {EstiloMusical}");
            Console.WriteLine($"Classificação Etária: {ClassificacaoEtaria}");
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Banda/Artista: {BandaArtista}, Estilo Musical: {EstiloMusical}, Classificação Etária: {ClassificacaoEtaria}";
        }
    }

    // Classe Participante
    public class Participante
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<Evento> EventosInscritos { get; set; } = new List<Evento>(); // Associação muitos-para-muitos

        public Participante(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

        public void InscreverEmEvento(Evento evento)
        {
            if (!EventosInscritos.Contains(evento))
            {
                evento.AdicionarParticipante(this);
                EventosInscritos.Add(evento);
            }
            else
            {
                Console.WriteLine($"Participante {Nome} já está inscrito no evento {evento.Titulo}.");
            }
        }

        public override string ToString()
        {
            return $"Nome: {Nome}, Email: {Email}";
        }
    }

    // Classe para gerenciar o Sistema de Eventos
    public class SistemaEventos
    {
        public List<Evento> Eventos { get; set; } = new List<Evento>();
        public List<Participante> Participantes { get; set; } = new List<Participante>();

        public void AdicionarEvento(Evento evento)
        {
            Eventos.Add(evento);
        }

        public void AdicionarParticipante(Participante participante)
        {
            Participantes.Add(participante);
        }

        public void ListarEventos()
        {
            Console.WriteLine("\nLista de Eventos:");
            foreach (var evento in Eventos)
            {
                evento.ExibirInformacoes();
                Console.WriteLine("Participantes Inscritos:");
                foreach (var participante in evento.ParticipantesInscritos)
                {
                    Console.WriteLine($"  - {participante.Nome}");
                }
                Console.WriteLine();
            }
        }

        public void ListarParticipantes()
        {
            Console.WriteLine("\nLista de Participantes:");
            foreach (var participante in Participantes)
            {
                Console.WriteLine(participante);
                Console.WriteLine("Eventos Inscritos:");
                foreach (var evento in participante.EventosInscritos)
                {
                    Console.WriteLine($"  - {evento.Titulo}");
                }
                Console.WriteLine();
            }
        }
    }

    // Classe Principal (Programa)
    class Program
    {
        static SistemaEventos sistema = new SistemaEventos();

        static void Main(string[] args)
        {
            ExibirMenu();
        }

        static void ExibirMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Menu do Sistema de Eventos Culturais ---");
                Console.WriteLine("1. Adicionar Evento");
                Console.WriteLine("2. Adicionar Participante");
                Console.WriteLine("3. Inscrever Participante em Evento");
                Console.WriteLine("4. Listar Eventos");
                Console.WriteLine("5. Listar Participantes");
                Console.WriteLine("6. Sair");

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarEvento();
                        break;
                    case "2":
                        AdicionarParticipante();
                        break;
                    case "3":
                        InscreverParticipanteEmEvento();
                        break;
                    case "4":
                        sistema.ListarEventos();
                        break;
                    case "5":
                        sistema.ListarParticipantes();
                        break;
                    case "6":
                        Console.WriteLine("Saindo do sistema...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static void AdicionarEvento()
        {
            Console.WriteLine("\n--- Adicionar Evento ---");
            Console.WriteLine("1. Oficina");
            Console.WriteLine("2. Palestra");
            Console.WriteLine("3. Show");

            Console.Write("Escolha o tipo de evento (1-3): ");
            if (!int.TryParse(Console.ReadLine(), out int tipoEscolhido) || tipoEscolhido < 1 || tipoEscolhido > 3)
            {
                Console.WriteLine("Tipo inválido.");
                return;
            }

            switch (tipoEscolhido)
            {
                case 1:
                    AdicionarOficina();
                    break;
                case 2:
                    AdicionarPalestra();
                    break;
                case 3:
                    AdicionarShow();
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }

        static void AdicionarOficina()
        {
            Console.Write("Título: ");
            string titulo = Console.ReadLine();

            Console.Write("Data (dd/mm/aaaa): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime data))
            {
                Console.WriteLine("Data inválida.");
                return;
            }

            Console.Write("Horário (hh:mm): ");
            if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan horario))
            {
                Console.WriteLine("Horário inválido.");
                return;
            }

            Console.Write("Local: ");
            string local = Console.ReadLine();

            Console.Write("Capacidade: ");
            if (!int.TryParse(Console.ReadLine(), out int capacidade))
            {
                Console.WriteLine("Capacidade inválida.");
                return;
            }

            Console.Write("Material Necessário: ");
            string materialNecessario = Console.ReadLine();

            Console.Write("Número Máximo de Participantes: ");
            if (!int.TryParse(Console.ReadLine(), out int numeroMaximoParticipantes))
            {
                Console.WriteLine("Número máximo de participantes inválido.");
                return;
            }

            Oficina novaOficina = new Oficina(titulo, data, horario, local, capacidade, materialNecessario, numeroMaximoParticipantes);
            sistema.AdicionarEvento(novaOficina);

            Console.WriteLine("Oficina adicionada com sucesso!");
        }

        static void AdicionarPalestra()
        {
            Console.Write("Título: ");
            string titulo = Console.ReadLine();

            Console.Write("Data (dd/mm/aaaa): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime data))
            {
                Console.WriteLine("Data inválida.");
                return;
            }

            Console.Write("Horário (hh:mm): ");
            if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan horario))
            {
                Console.WriteLine("Horário inválido.");
                return;
            }

            Console.Write("Local: ");
            string local = Console.ReadLine();

            Console.Write("Capacidade: ");
            if (!int.TryParse(Console.ReadLine(), out int capacidade))
            {
                Console.WriteLine("Capacidade inválida.");
                return;
            }

            Console.Write("Palestrante: ");
            string palestrante = Console.ReadLine();

            Console.Write("Tópico: ");
            string topico = Console.ReadLine();

            Console.Write("Duração Prevista (hh:mm): ");
            if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan duracaoPrevista))
            {
                Console.WriteLine("Duração prevista inválida.");
                return;
            }

            Palestra novaPalestra = new Palestra(titulo, data, horario, local, capacidade, palestrante, topico, duracaoPrevista);
            sistema.AdicionarEvento(novaPalestra);

            Console.WriteLine("Palestra adicionada com sucesso!");
        }

        static void AdicionarShow()
        {
            Console.Write("Título: ");
            string titulo = Console.ReadLine();

            Console.Write("Data (dd/mm/aaaa): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime data))
            {
                Console.WriteLine("Data inválida.");
                return;
            }

            Console.Write("Horário (hh:mm): ");
            if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan horario))
            {
                Console.WriteLine("Horário inválido.");
                return;
            }

            Console.Write("Local: ");
            string local = Console.ReadLine();

            Console.Write("Capacidade: ");
            if (!int.TryParse(Console.ReadLine(), out int capacidade))
            {
                Console.WriteLine("Capacidade inválida.");
                return;
            }

            Console.Write("Banda/Artista: ");
            string bandaArtista = Console.ReadLine();

            Console.Write("Estilo Musical: ");
            string estiloMusical = Console.ReadLine();

            Console.Write("Classificação Etária: ");
            if (!int.TryParse(Console.ReadLine(), out int classificacaoEtaria))
            {
                Console.WriteLine("Classificação etária inválida.");
                return;
            }

            Show novoShow = new Show(titulo, data, horario, local, capacidade, bandaArtista, estiloMusical, classificacaoEtaria);
            sistema.AdicionarEvento(novoShow);

            Console.WriteLine("Show adicionado com sucesso!");
        }

        static void AdicionarParticipante()
        {
            Console.WriteLine("\n--- Adicionar Participante ---");

            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Participante novoParticipante = new Participante(nome, email);
            sistema.AdicionarParticipante(novoParticipante);

            Console.WriteLine("Participante adicionado com sucesso!");
        }

        static void InscreverParticipanteEmEvento()
        {
            Console.WriteLine("\n--- Inscrever Participante em Evento ---");

            Console.Write("Email do participante: ");
            string emailParticipante = Console.ReadLine();

            Participante participante = sistema.Participantes.Find(p => p.Email == emailParticipante);
            if (participante == null)
            {
                Console.WriteLine("Participante não encontrado.");
                return;
            }

            Console.Write("Título do evento: ");
            string tituloEvento = Console.ReadLine();

            Evento evento = sistema.Eventos.Find(e => e.Titulo == tituloEvento);
            if (evento == null)
            {
                Console.WriteLine("Evento não encontrado.");
                return;
            }

            participante.InscreverEmEvento(evento);
        }
    }
}