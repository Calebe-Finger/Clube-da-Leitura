using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.Modulo_de_Amigos;
using System.Globalization;

namespace ClubeDaLeitura.ConsoleApp.Modulo_de_Caixas
{
    public class TelaCaixa : TelaBase
    {
        public TelaCaixa(RepositorioCaixa repositorio) : base("Caixa", repositorio)
        {
        }

        public override void VisualizarRegistros()
        {
            Console.Clear();
            Console.WriteLine("----------------------------");
            Console.WriteLine("Visualização de Caixas");
            Console.WriteLine("----------------------------");

            Console.WriteLine(
                "{0, -6} | {1, -30} | {2, -30} | {3, -20}",
                "Id", "Etiqueta", "Cor", "Dias de Empréstimo"
            );

            EntidadeBase[] caixas = repositorio.SelecionarRegistros();

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

        protected override EntidadeBase ObterDados()
        {
            Console.WriteLine("Digite o código da Etiqueta: ");
            string etiqueta = Console.ReadLine();

            Console.WriteLine("Digite a cor da caixa: ");
            string cor = Console.ReadLine();

            Console.WriteLine("Digite o número de dias de empréstimo da caixa (opcional): ");

            bool ConseguiuConverter = int.TryParse(Console.ReadLine(), out int diasEmprestimo);

            Caixa caixa;

            if (ConseguiuConverter)
                caixa = new Caixa(etiqueta, cor, diasEmprestimo);

            else
                caixa = new Caixa(etiqueta, cor);

                return caixa;
        }
    }
}
