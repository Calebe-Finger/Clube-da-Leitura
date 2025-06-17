using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.Modulo_de_Amigos;
using ClubeDaLeitura.ConsoleApp.Modulo_de_Revistas;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

namespace ClubeDaLeitura.ConsoleApp.ModuloReservas
{
    public class TelaReserva : TelaBase
    {
        private RepositorioReserva repositorioReserva;
        private RepositorioAmigo repositorioAmigo;
        private RepositorioRevista repositorioRevista;

        public TelaReserva(RepositorioReserva repositorio, RepositorioAmigo repositorioAmigo, 
            RepositorioRevista repois) : base("Reserva", repositorio)
        {
            repositorioReserva = repositorio;
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioRevista = repositorioRevista;
        }

        public override char ApresentarMenu()
        {
            Console.Clear();
            Console.WriteLine("----------------------------");
            Console.WriteLine($"Gestão de {nomeEntidade}s");
            Console.WriteLine("----------------------------");
            Console.WriteLine($"1 - Cadastro de {nomeEntidade}");
            Console.WriteLine($"2 - Cancelamento de {nomeEntidade}");
            Console.WriteLine($"3 - Visualizar {nomeEntidade}s Ativas");
            Console.WriteLine($"4 - Voltar para o Menu Principal");
            Console.WriteLine("S - Sair");
            Console.WriteLine("----------------------------");
            Console.WriteLine("Digite uma opção válida: ");

            char OpcaoEscolhida = Console.ReadLine().ToUpper()[0];
            return OpcaoEscolhida;
        }

        public void CadastrarReserva()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Cadastro de {nomeEntidade}");
            Console.WriteLine("--------------------------------------");

            Reserva novaReserva = (Reserva)ObterDados();

            string erros = novaReserva.Validar();

            if (erros.Length > 0)
            {
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;      //muda a cor da fonte para vermelho
                Console.WriteLine($"Erros: \n{erros}");
                Console.ResetColor();                               //volta a cor para a original

                Console.Write("\nDigite ENTER para cadastrar novamente...");
                Console.ReadLine();

                //Recursão: Quando um método chama ele mesmo
                CadastrarRegistro();
                return;
            }

            Reserva[] reservasAtivas = repositorioReserva.SelecionarReservasAtivas();

            for (int i = 0; i < reservasAtivas.Length; i++)
            {
                Reserva reservaAtiva = reservasAtivas[i];

                if (novaReserva.Amigo.Id == reservaAtiva.Amigo.Id)
                {
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("O amigo selecionado já tem uma reserva ativa!");
                    Console.ResetColor();

                    Console.Write("\nDigite ENTER para cadastrar novamente...");
                    Console.ReadLine();

                    return;
                }
            }

            novaReserva.Iniciar();

            repositorio.CadastrarRegistro(novaReserva);

            Console.WriteLine($"{nomeEntidade} cadastrado(a) com sucesso!");
            Console.ReadLine();

            ApresentarMenu();
        }

        public void CancelarReserva()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Cancelamento de Reserva");
            Console.WriteLine("--------------------------------------");

            VisualizarRegistros();

            Console.WriteLine("\nDigite o ID da reserva que deseja cancelar: ");
            int idReserva = Convert.ToInt32(Console.ReadLine());

            Reserva reservaSelecionada = (Reserva)repositorio.SelecionarRegistroPorId(idReserva);

            if (idReserva == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("A reserva selecionada não existe!");
                Console.ResetColor();

                Console.WriteLine("\n Digite ENTER para continuar...");
                Console.ReadLine();

                return;
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\nDeseja confirmar o cancelamento da reserva? (S/N): ");
                Console.ResetColor();

                string resposta = Console.ReadLine()!;

                if (resposta.ToUpper() != "S")
                    return;

                reservaSelecionada.Concluir();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nCancelamento de {nomeEntidade} concluído com sucesso!");
                Console.ResetColor();

            }
        }

        public override void VisualizarRegistros()
        {
            Console.Clear();
            Console.WriteLine("----------------------------");
            Console.WriteLine("Visualização de Reservas Ativas");
            Console.WriteLine("----------------------------");

            Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -20} | {3, -10} | {4, -10}",
                "Id", "Amigo", "Revista", "Data da Reserva", "Status"
            );

            Reserva[] reservas = repositorioReserva.SelecionarReservasAtivas();

            for (int i = 0; i < reservas.Length; i++)
            {
                Reserva r = (Reserva)reservas[i];

                if (r == null)
                    continue;

                string statusReserva = r.EstaAtiva ? "Ativa" : "Concluída";

                Console.WriteLine(
                    "{0, -6} | {1, -20} | {2, -230} | {3, -10} | {4, -10}",
                    r.Id, r.Amigo.Nome, r.Revista.Titulo, r.DataAbertura.ToShortTimeString(), statusReserva
                );

                Console.ResetColor();
            }

            Console.ReadLine();
        }

        protected override EntidadeBase ObterDados()
        {
            VisualizarAmigos();

            Console.WriteLine("\nDigite o ID do amigo que irá reservar a revista: ");
            int idAmigo = Convert.ToInt32(Console.ReadLine());

            Amigo amigoSelecionado = (Amigo)repositorioAmigo.SelecionarRegistroPorId(idAmigo);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nAmigo selecionado com sucesso!\n");
            Console.ResetColor();

            VisualizarRevistasDisponiveis();

            Console.WriteLine("\nDigite o ID da revista que irá ser reservada: ");
            int idRevista = Convert.ToInt32(Console.ReadLine());

            Revista revistaSelecionada = (Revista)repositorioRevista.SelecionarRegistroPorId(idRevista);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nRevista selecionada com sucesso!\n");
            Console.ResetColor();

            Reserva reserva = new Reserva(amigoSelecionado, revistaSelecionada);

            return reserva;
        }

        private void VisualizarAmigos()
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine("Visualização de Amigos");
            Console.WriteLine("----------------------------");

            Console.WriteLine(
                "{0, -6} | {1, -30} | {2, -30} | {3, -20}",
                "Id", "Nome", "Nome do Responsável", "Telefone"
            );

            EntidadeBase[] amigos = repositorioAmigo.SelecionarRegistros();

            for (int i = 0; i < amigos.Length; i++)
            {
                Amigo a = (Amigo)amigos[i];

                if (a == null)
                    continue;

                Console.WriteLine(
                    "{0, -6} | {1, -30} | {2, -30} | {3, -20}",
                    a.Id, a.Nome, a.NomeResponsavel, a.Telefone
                );
            }
        }

        private void VisualizarRevistasDisponiveis()
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine("Visualização de Revistas");
            Console.WriteLine("----------------------------");

            Console.WriteLine(
                "{0, -6} | {1, -25} | {2, -10} | {3, -10} | {4, -25} | {5, -10}",
                "Id", "Título", "Número de Edição", "Ano de Publicação", "Caixa", "Status"
            );

            EntidadeBase[] revistasDisponiveis = repositorioRevista.SelecionarRevistasDisponiveis();

            for (int i = 0; i < revistasDisponiveis.Length; i++)
            {
                Revista r = (Revista)revistasDisponiveis[i];

                if (r == null)
                    continue;

                Console.WriteLine(
                    "{0, -6} | {1, -25} | {2, -10} | {3, -10} | {4, -25} | {5, -10}",
                    r.Id, r.Titulo, r.NumeroEdicao, r.AnoPublicacao, r.Caixa.Etiqueta, r.Status
                );
            }
        }
    }
}
