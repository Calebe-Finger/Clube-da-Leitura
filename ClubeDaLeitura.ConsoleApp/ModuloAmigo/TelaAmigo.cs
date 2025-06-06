using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.Modulo_de_Amigos
{
    public class TelaAmigo : TelaBase
    {
        private RepositorioAmigo repositAmigos;

        public TelaAmigo(RepositorioAmigo repositorio) : base ("Amigo", repositorio)
        {

        }

        public override void VisualizarRegistros()
        {
            Console.Clear();
            Console.WriteLine("----------------------------");
            Console.WriteLine("Visualização de Amigos");
            Console.WriteLine("----------------------------");

            Console.WriteLine(
                "{0, -4} | {1, -26} | {2, -16} | {3, -14}",
                "Id", "Nome", "Nome do Responsável", "Telefone"
            );

            EntidadeBase[] chamados = repositAmigos.SelecionarRegistros();

            for (int i = 0; i < chamados.Length; i++)
            {
                Amigo a = (Amigo)chamados[i];

                if (a == null)
                    continue;

                Console.WriteLine(
                    "{0, -4} | {1, -26} | {2, -16} | {3, -14}",
                    a.id, a.Nome, a.NomeResponsavel, a.Telefone
                );
            }
            Console.ReadLine();
        }

        protected override Amigo ObterDados()
        {
            Console.WriteLine("Digite o nome do amigo: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite o nome do responsável do amigo: ");
            string nomeResponsavel = Console.ReadLine();

            Console.WriteLine("Digite o telefone do amigo: ");
            string telefone = Console.ReadLine();

            Amigo amigos = new Amigo(nome, nomeResponsavel, telefone);

            return amigos;
        }
    }
}
