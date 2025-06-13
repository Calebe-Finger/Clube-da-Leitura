using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.Modulo_de_Amigos;
using ClubeDaLeitura.ConsoleApp.Modulo_de_Caixas;
using ClubeDaLeitura.ConsoleApp.Modulo_de_Revistas;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    public class TelaEmprestimo : TelaBase
    {
        private RepositorioEmprestimo repositorioEmprestimo;
        private RepositorioAmigo repositorioAmigo;
        private RepositorioRevista repositorioRevista;
        public TelaEmprestimo(RepositorioEmprestimo repositorio, 
            RepositorioAmigo repositorioAmigo,
            RepositorioRevista repositorioRevista) 
            : base("Empréstimo", repositorio)
        {
            repositorioEmprestimo = repositorio;
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
            Console.WriteLine($"2 - Devolução de {nomeEntidade}");
            Console.WriteLine($"3 - Visualizar {nomeEntidade}s");
            Console.WriteLine($"4 - Voltar para o Menu Principal");
            Console.WriteLine("S - Sair");
            Console.WriteLine("----------------------------");
            Console.WriteLine("Digite uma opção válida: ");

            char OpcaoEscolhida = Console.ReadLine().ToUpper()[0];
            return OpcaoEscolhida;
        }

        public void CadastrarEmprestimo()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Cadastro de {nomeEntidade}");
            Console.WriteLine("--------------------------------------");

            Emprestimo novoRegistro = (Emprestimo)ObterDados();

            string erros = novoRegistro.Validar();

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

            Emprestimo[] emprestimosAtivos = repositorioEmprestimo.SelecionarEmprestimosAtivos();

            for (int i = 0; i < emprestimosAtivos.Length; i++)
            {
                Emprestimo emprestimoAtivo = emprestimosAtivos[i];

                if (novoRegistro.Amigo.Id == emprestimoAtivo.Amigo.Id)
                {
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("O amigo selecionado já tem um emprestimo ativo!");
                    Console.ResetColor();

                    Console.Write("\nDigite ENTER para cadastrar novamente...");
                    Console.ReadLine();

                    return;
                }
            }

            novoRegistro.Revista.Status = "Emprestada";

            repositorio.CadastrarRegistro(novoRegistro);

            Console.WriteLine($"{nomeEntidade} cadastrado(a) com sucesso!");
            Console.ReadLine();

            ApresentarMenu();
        }

        public void DevolverEmprestimo()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Devolução de Empréstimo");
            Console.WriteLine("--------------------------------------");

            VisualizarEmprestimosAtivos();

            Console.WriteLine("\nDigite o ID do emprestimo que deseja encerrar: ");
            int idEmprestimo = Convert.ToInt32(Console.ReadLine());

            Emprestimo emprestimoSelecionado = (Emprestimo)repositorio.SelecionarRegistroPorId(idEmprestimo);

            if (idEmprestimo == null)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("Insira o valor de um ID válido!");
                Console.ResetColor();

                Console.WriteLine("\n Digite ENTER para continuar...");
                Console.ReadLine();

                return;
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\nDeseja confirmar a conclusão do empréstimo? (S/N): ");
                Console.ResetColor();

                string resposta = Console.ReadLine()!;

                if (resposta.ToUpper() == "S")
                {
                    emprestimoSelecionado.Status = "Concluído";
                    emprestimoSelecionado.Revista.Status = "Disponível";

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{nomeEntidade} concluído com sucesso!");
                    Console.ResetColor();
                }
            }
        }

        private void VisualizarEmprestimosAtivos()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Visualização de Empréstimos Ativos");
            Console.WriteLine("------------------------------------------");

            Console.WriteLine(
                "{0, -6} | {1, -30} | {2, -30} | {3, -10} | {4, -10} | {5, -10}",
                "Id", "Amigo", "Revista", "Data de Emprestimo", "Data de Devolução", "Status"
            );

            EntidadeBase[] emprestimosAtivos = repositorioEmprestimo.SelecionarEmprestimosAtivos();

            for (int i = 0; i < emprestimosAtivos.Length; i++)
            {
                Emprestimo e = (Emprestimo)emprestimosAtivos[i];

                if (e == null)
                    continue;

                if (e.Status == "Atrasado")
                    Console.ForegroundColor = ConsoleColor.DarkYellow;

                Console.WriteLine(
                    "{0, -6} | {1, -30} | {2, -30} | {3, -10} | {4, -10} | {5, -10}",
                    e.Id, e.Amigo.Nome, e.Revista.Titulo, e.DataEmprestimo.ToShortTimeString(),
                    e.DataDevolucao.ToShortTimeString(), e.Status
                );

                Console.ResetColor();
            }
            Console.ReadLine();
        }

        public override void VisualizarRegistros()
        {
            Console.Clear();
            Console.WriteLine("----------------------------");
            Console.WriteLine("Visualização de Empréstimos");
            Console.WriteLine("----------------------------");

            Console.WriteLine(
                "{0, -6} | {1, -30} | {2, -30} | {3, -10} | {4, -10} | {5, -10}",
                "Id", "Amigo", "Revista", "Data de Emprestimo", "Data de Devolução", "Status"
            );

            EntidadeBase[] emprestimos = repositorio.SelecionarRegistros();

            for (int i = 0; i < emprestimos.Length; i++)
            {
                Emprestimo e = (Emprestimo)emprestimos[i];

                if (e == null)
                    continue;

                if (e.Status == "Atrasado")
                    Console.ForegroundColor = ConsoleColor.DarkYellow;

                Console.WriteLine(
                    "{0, -6} | {1, -30} | {2, -30} | {3, -10} | {4, -10} | {5, -10}",
                    e.Id, e.Amigo.Nome, e.Revista.Titulo, e.DataEmprestimo.ToShortTimeString(), 
                    e.DataDevolucao.ToShortTimeString(), e.Status
                );

                Console.ResetColor();
            }
        }

        protected override EntidadeBase ObterDados()
        {
            VisualizarAmigos();

            Console.WriteLine("\nDigite o ID do amigo que irá receber a revista: ");
            int idAmigo = Convert.ToInt32(Console.ReadLine());

            Amigo amigoSelecionado = (Amigo)repositorioAmigo.SelecionarRegistroPorId(idAmigo);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nAmigo selecionado com sucesso!\n");
            Console.ResetColor();

            VisualizarRevistasDisponiveis();

            Console.WriteLine("\nDigite o ID da revista que irá ser emprestada: ");
            int idRevista = Convert.ToInt32(Console.ReadLine());

            Revista revistaSelecionada = (Revista)repositorioRevista.SelecionarRegistroPorId(idRevista);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nRevista selecionada com sucesso!\n");
            Console.ResetColor();

            Emprestimo emprestimo = new Emprestimo(amigoSelecionado, revistaSelecionada);

            return emprestimo;
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
