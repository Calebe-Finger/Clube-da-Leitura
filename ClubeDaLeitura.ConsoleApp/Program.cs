using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

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

                if (telaEscolhida is TelaEmprestimo)
                {
                    TelaEmprestimo telaEmprestimo = (TelaEmprestimo)telaEscolhida;

                    switch (opcaoEscolhida)
                    {
                        case '1':
                            telaEmprestimo.CadastrarEmprestimo();
                            break;

                        case '2':
                            telaEmprestimo.DevolverEmprestimo();
                            break;

                        case '3':
                            telaEmprestimo.VisualizarRegistros();
                            break;

                        case '4':
                            continue;
                    }
                }

                else
                {
                    switch (opcaoEscolhida)
                    {
                        case '1':
                            telaEscolhida.CadastrarRegistro();
                            break;

                        case '2':
                            telaEscolhida.VisualizarRegistros();
                            break;

                        case '3':
                            telaEscolhida.EditarRegistro();
                            break;

                        case '4':
                            telaEscolhida.ExcluirRegistro();
                            break;

                        case '5':
                            continue;
                    }
                }
            }
        }
    }
}
