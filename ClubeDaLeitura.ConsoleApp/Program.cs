using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            TelaPrincipal telaPrincipal = new TelaPrincipal();

            while (true)
            {
                telaPrincipal.MenuPrincipal();

                TelaBase telaEscolhida = telaPrincipal.ObterTela();

                if (telaEscolhida == null)
                    break;

                char opcaoEscolhida = telaEscolhida.ApresentarMenu();

                if (opcaoEscolhida == 'S' || opcaoEscolhida == 's')
                    break;

                switch (opcaoEscolhida)
                {
                    case '1':
                        telaEscolhida.CadastrarRegistro();
                        break;

                    case '2':
                        telaEscolhida.CadastrarRegistro();
                        break;
                }
            }
        }
    }
}
