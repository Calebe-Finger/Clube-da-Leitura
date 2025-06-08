using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.Modulo_de_Amigos;
using ClubeDaLeitura.ConsoleApp.Modulo_de_Caixas;

namespace ClubeDaLeitura.ConsoleApp.Modulo_de_Revistas
{
    public class TelaRevista : TelaBase
    {
        public TelaRevista(RepositorioRevista repositorio) : base("Revista", repositorio)
        {
        }

        public override void VisualizarRegistros()
        {
            Console.Clear();
            Console.WriteLine("----------------------------");
            Console.WriteLine("Visualização de Revistas");
            Console.WriteLine("----------------------------");

            Console.WriteLine(
                "{0, -6} | {1, -30} | {2, -30} | {3, -20} | {4, -20} | {5, -10}",
                "Id", "Título", "Número de Edição", "Ano de Publicação", "Caixa", "Status"
            );

            EntidadeBase[] revistas = repositorio.SelecionarRegistros();

            for (int i = 0; i < revistas.Length; i++)
            {
                Revista r = (Revista)revistas[i];

                if (r == null)
                    continue;

                Console.WriteLine(
                    "{0, -6} | {1, -30} | {2, -30} | {3, -20} | {4, -20} | {5, -10}",
                    r.Id, r.Titulo, r.NumeroEdicao, r.AnoPublicacao, r.Caixa
                );
            }
            Console.ReadLine();
        }

        protected override EntidadeBase ObterDados()
        {
            Console.WriteLine("Qual é o Título da Revista: ");
            string titulo = Console.ReadLine();

            Console.WriteLine("Qual é Número da Edição dessa Revista: ");
            int numeroEdicao = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Qual o ano de Publicação: ");
            DateTime anoPublicacao = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Qual o ID da caixa da revista: ");
            Caixa caixa = ;

            Revista revista = new(titulo, numeroEdicao, anoPublicacao, caixa);

            return revista;
        }
    }
}
