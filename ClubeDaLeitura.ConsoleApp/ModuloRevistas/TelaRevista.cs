using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.Modulo_de_Amigos;
using ClubeDaLeitura.ConsoleApp.Modulo_de_Caixas;

namespace ClubeDaLeitura.ConsoleApp.Modulo_de_Revistas
{
    public class TelaRevista : TelaBase
    {
        private RepositorioCaixa repositorioCaixa;

        public TelaRevista(RepositorioRevista repositorio, RepositorioCaixa repositorioCaixa)
            : base("Revista", repositorio)
        {
            this.repositorioCaixa = repositorioCaixa;
        }

        public override void VisualizarRegistros()
        {
            Console.Clear();
            Console.WriteLine("----------------------------");
            Console.WriteLine("Visualização de Revistas");
            Console.WriteLine("----------------------------");

            Console.WriteLine(
                "{0, -5} | {1, -25} | {2, -20} | {3, -20} | {4, -15} | {5, -10}",
                "Id", "Título", "Número de Edição", "Ano de Publicação", "Caixa", "Status"
            );

            EntidadeBase[] revistas = repositorio.SelecionarRegistros();

            for (int i = 0; i < revistas.Length; i++)
            {
                Revista r = (Revista)revistas[i];

                if (r == null)
                    continue;

                Console.WriteLine(
                    "{0, -5} | {1, -25} | {2, -20} | {3, -20} | {4, -15} | {5, -10}",
                    r.Id, r.Titulo, r.NumeroEdicao, r.AnoPublicacao, r.Caixa.Etiqueta, r.Status
                );
            }
            Console.ReadLine();
        }

        protected override EntidadeBase ObterDados()
        {
            Console.WriteLine("Qual é o título da revista: ");
            string titulo = Console.ReadLine();

            Console.WriteLine("Qual é número da edição da revista: ");
            int numeroEdicao = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Qual o ano de Publicação: ");
            int anoPublicacao = Convert.ToInt32(Console.ReadLine());

            VisualizarCaixas();

            Console.WriteLine("\n Digite o ID da caixa selecionada: ");

            int idCaixa = Convert.ToInt32(Console.ReadLine());

            Caixa caixaSelecionada = (Caixa)repositorioCaixa.SelecionarRegistroPorId(idCaixa);

            Revista revista = new Revista(titulo, numeroEdicao, anoPublicacao, caixaSelecionada);

            return revista;
        }

        public void VisualizarCaixas()  
        {
            Console.Clear();
            Console.WriteLine("----------------------------");
            Console.WriteLine("Visualização de Caixas");
            Console.WriteLine("----------------------------");

            Console.WriteLine(
                "{0, -6} | {1, -30} | {2, -30} | {3, -20}",
                "Id", "Etiqueta", "Cor", "Dias de Empréstimo"
            );

            EntidadeBase[] caixas = repositorioCaixa.SelecionarRegistros();

            for (int i = 0; i < caixas.Length; i++)
            {
                Caixa c = (Caixa)caixas[i];

                if (c == null)
                    continue;

                Console.WriteLine(
                    "{0, -6} | {1, -30} | {2, -30} | {3, -20}",
                    c.Id, c.Etiqueta, c.Cor, c.DiasEmprestimo
                );
            }
            Console.ReadLine();
        }
    }
}
