using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

namespace ClubeDaLeitura.ConsoleApp.Modulo_de_Amigos
{
    public class TelaAmigo : TelaBase
    {
        private RepositorioEmprestimo repositorioEmprestimo;

        public TelaAmigo(RepositorioAmigo repositorio, RepositorioEmprestimo repositorioEmprestimo) 
            : base ("Amigo", repositorio)
        {
            this.repositorioEmprestimo = repositorioEmprestimo; 
        }

        public override void CadastrarRegistro()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Cadastro de {nomeEntidade}");
            Console.WriteLine("--------------------------------------");

            Amigo novoRegistro = (Amigo)ObterDados();

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

            EntidadeBase[] registros = repositorio.SelecionarRegistros();

            for (int i = 0; i < registros.Length; i++)
            {
                Amigo amigoRegistrado = (Amigo)registros[i];

                if (amigoRegistrado == null)
                    continue;

                if (amigoRegistrado.Nome == novoRegistro.Nome)
                {
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Já foi cadastrado um amigo com esse nome!");
                    Console.ResetColor();

                    Console.Write("\nDigite ENTER para cadastrar novamente...");
                    Console.ReadLine();

                    CadastrarRegistro();
                    return;
                }

                if (amigoRegistrado.Telefone == novoRegistro.Telefone)
                {
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Já foi cadastrado um amigo com esse telefone!");
                    Console.ResetColor();

                    Console.Write("\nDigite ENTER para cadastrar novamente...");
                    Console.ReadLine();

                    CadastrarRegistro();
                    return;
                }
            }

            repositorio.CadastrarRegistro(novoRegistro);

            Console.WriteLine($"{nomeEntidade} cadastrado(a) com sucesso!");
            Console.ReadLine();

            ApresentarMenu();
        }

        public override void EditarRegistro()
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Edição de {nomeEntidade}");
            Console.WriteLine("--------------------------------------");

            VisualizarRegistros();

            Console.WriteLine("Digite o ID no registro que deseja editar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            Amigo registroAtualizado = (Amigo)ObterDados();

            string erros = registroAtualizado.Validar();

            if (erros.Length > 0)
            {
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;      //muda a cor da fonte para vermelho
                Console.WriteLine($"Erros: \n{erros}");
                Console.ResetColor();                               //volta a cor para a original

                Console.Write("\nDigite ENTER para cadastrar novamente...");
                Console.ReadLine();

                //Recursão: Quando um método chama ele mesmo
                EditarRegistro();
                return;
            }

            EntidadeBase[] registros = repositorio.SelecionarRegistros();

            for (int i = 0; i < registros.Length; i++)
            {
                Amigo amigoRegistrado = (Amigo)registros[i];

                if (amigoRegistrado == null)
                    continue;

                if (amigoRegistrado.Id != idSelecionado && 
                    (amigoRegistrado.Nome == registroAtualizado.Nome))
                {
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Já foi cadastrado um amigo com esse nome!");
                    Console.ResetColor();

                    Console.Write("\nDigite ENTER para editar novamente...");
                    Console.ReadLine();

                    EditarRegistro();
                    return;
                }

                if (amigoRegistrado.Id != idSelecionado && 
                    (amigoRegistrado.Telefone == registroAtualizado.Telefone))
                {
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Já foi cadastrado um amigo com esse telefone!");
                    Console.ResetColor();

                    Console.Write("\nDigite ENTER para cadastrar novamente...");
                    Console.ReadLine();

                    CadastrarRegistro();
                    return;
                }
            }

            repositorio.EditarRegistro(idSelecionado, registroAtualizado);

            Console.WriteLine($"{nomeEntidade} editado(a) com sucesso!");
            Console.ReadLine();
        }

        public override void VisualizarRegistros()
        {
            Console.Clear();
            Console.WriteLine("----------------------------");
            Console.WriteLine("Visualização de Amigos");
            Console.WriteLine("----------------------------");

            Console.WriteLine(
                "{0, -6} | {1, -30} | {2, -30} | {3, -20} | {4, -10}",
                "Id", "Nome", "Responsável", "Telefone", "Multa Ativa"
            );

            EntidadeBase[] amigos = repositorio.SelecionarRegistros();

            EntidadeBase[] emprestimos = repositorioEmprestimo.SelecionarRegistros();

            for (int i = 0; i < amigos.Length; i++)
            {
                Amigo a = (Amigo)amigos[i];

                if (a == null)
                    continue;

                bool amigoTemMultaAtiva = false;

                for (int j = 0; j <= emprestimos.Length; j++)
                {
                    Emprestimo e = (Emprestimo)emprestimos[j];

                    if (e == null)
                        continue;

                    if (a == e.Amigo && e.Multa != null)
                    {
                        if (!e.Multa.EstaPaga)
                            amigoTemMultaAtiva = true;
                    }
                }

                string temMultaAtiva = amigoTemMultaAtiva ? "Sim" : "Não";

                Console.WriteLine(
                    "{0, -6} | {1, -30} | {2, -30} | {3, -20} | {4, -10}",
                    a.Id, a.Nome, a.NomeResponsavel, a.Telefone, temMultaAtiva
                );
            }
            Console.ReadLine();
        }

        protected override Amigo ObterDados()
        {
            Console.WriteLine("Digite o nome do amigo: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite o nome do responsável pelo amigo: ");
            string nomeResponsavel = Console.ReadLine();

            Console.WriteLine("Digite o telefone do amigo ou responsável: ");
            string telefone = Console.ReadLine();

            Amigo amigo = new Amigo(nome, nomeResponsavel, telefone);

            return amigo;
        }
    }
}
