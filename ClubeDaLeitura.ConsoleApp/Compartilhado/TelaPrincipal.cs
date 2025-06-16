using ClubeDaLeitura.ConsoleApp.Modulo_de_Amigos;
using ClubeDaLeitura.ConsoleApp.Modulo_de_Caixas;
using ClubeDaLeitura.ConsoleApp.Modulo_de_Revistas;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloReservas;

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

        private RepositorioEmprestimo repositorioEmprestimo;
        private TelaEmprestimo telaEmprestimo;

        public TelaPrincipal()
        {
            repositorioAmigo = new RepositorioAmigo();
            repositorioCaixa = new RepositorioCaixa();
            repositorioRevista = new RepositorioRevista();
            repositorioEmprestimo = new RepositorioEmprestimo();

            telaAmigo = new TelaAmigo(repositorioAmigo, repositorioEmprestimo);
            telaCaixa = new TelaCaixa(repositorioCaixa);
            telaRevista = new TelaRevista(repositorioRevista, repositorioCaixa);
            telaEmprestimo = new TelaEmprestimo(repositorioEmprestimo, repositorioAmigo, repositorioRevista);

            Amigo amigo = new Amigo("Júnior", "Amanda", "49 99999-3333");
            repositorioAmigo.CadastrarRegistro(amigo);

            Caixa caixa = new Caixa("Ação", "Vermelha", 1);
            repositorioCaixa.CadastrarRegistro(caixa);

            Revista revista = new Revista("Superman", 150, 1995, caixa);
            repositorioRevista.CadastrarRegistro(revista);
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
                return telaEmprestimo;

            return null;
        }
    }
}
