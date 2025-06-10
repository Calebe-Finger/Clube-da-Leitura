using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.Modulo_de_Amigos
{
    public class TelaAmigo : TelaBase
    {
        public TelaAmigo(RepositorioAmigo repositorio) : base ("Amigo", repositorio)
        {
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

        public void EditarRegistro()
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Edição de {nomeEntidade}");
            Console.WriteLine("--------------------------------------");

            VisualizarRegistros();

            Console.WriteLine("Digite o ID no registro que deseja editar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            EntidadeBase registroAtualizado = ObterDados();

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
                "{0, -6} | {1, -30} | {2, -30} | {3, -20}",
                "Id", "Nome", "Nome do Responsável", "Telefone"
            );

            EntidadeBase[] amigos = repositorio.SelecionarRegistros();

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
