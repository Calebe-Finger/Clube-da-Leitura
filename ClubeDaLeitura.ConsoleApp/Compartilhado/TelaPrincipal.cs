using ClubeDaLeitura.ConsoleApp.Modulo_de_Amigos;
using ClubeDaLeitura.ConsoleApp.Modulo_de_Caixas;
using ClubeDaLeitura.ConsoleApp.Modulo_de_Revistas;

namespace ClubeDaLeitura.ConsoleApp.Compartilhado
{
    public class TelaPrincipal
    {
        private char opcaoEscolhida;

        private RepositorioAmigo repositorioAmigo;
        private TelaAmigo telaAmigo;

        private RepositorioCaixa repositorioCaixa;
        private TelaCaixa telaCaixa;

        private RepositorioRevista repositorioRevista;
        private TelaRevista telaRevista;

        public TelaPrincipal()
        {
            repositorioAmigo = new RepositorioAmigo();
            telaAmigo = new TelaAmigo(repositorioAmigo);

            repositorioCaixa = new RepositorioCaixa();
            telaCaixa = new TelaCaixa(repositorioCaixa);

            repositorioRevista = new RepositorioRevista();
            telaRevista = new TelaRevista(repositorioRevista, repositorioCaixa);
        }

        public void MenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("|        CLUBE DA LEITURA         |");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Qual módulo deseja acessar:");
            Console.WriteLine("1 - Módulo Amigos");
            Console.WriteLine("2 - Módulo de Caixas");
            Console.WriteLine("3 - Módulo de Revistas");
            Console.WriteLine("4 - Módulo de Empréstimos");
            Console.WriteLine("S - Sair\n");
            opcaoEscolhida = Console.ReadLine()[0];
        }

        public TelaBase ObterTela()
        {
            if (opcaoEscolhida == '1')
                return telaAmigo;

            else if (opcaoEscolhida == '2')
                return telaCaixa;

            else if (opcaoEscolhida == '3')
                return telaRevista;

            else if (opcaoEscolhida == '4')
                return null;

            return null;
        }
    }
}
