using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.Modulo_de_Revistas;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    public class RepositorioEmprestimo : RepositorioBase
    {
        public Emprestimo[] SelecionarEmprestimosAtivos()
        {
            int contadorEmprestimosAtivos = 0;

            for (int i = 0; i < registros.Length; i++)
            {
                Emprestimo emprestimoAtual = (Emprestimo)registros[i];

                if (emprestimoAtual == null)
                    continue;

                if (emprestimoAtual.Status == "Aberto" || emprestimoAtual.Status == "Atrasado")
                    contadorEmprestimosAtivos++;
            }

            Emprestimo[] emprestimosAtivos = new Emprestimo[contadorEmprestimosAtivos];

            int contadorAuxiliar = 0;

            for (int i = 0; i < registros.Length; i++)
            {
                Emprestimo emprestimoAtual = (Emprestimo)registros[i];

                if (emprestimoAtual == null)
                    continue;

                if (emprestimoAtual.Status == "Aberto" || emprestimoAtual.Status == "Atrasado")
                    emprestimosAtivos[contadorAuxiliar++] = (Emprestimo)registros[i];
            }

            return emprestimosAtivos;
        }

        public Emprestimo[] SelecionarEmprestimosComMulta()
        {
            int contadorEmprestimosComMulta = 0;

            for (int i = 0; i < registros.Length; i++)
            {
                Emprestimo emprestimoAtual = (Emprestimo)registros[i];

                if (emprestimoAtual == null)
                    continue;

                if (emprestimoAtual.Status == "Aberto" && emprestimoAtual.Multa != null && !emprestimoAtual.MultaPaga)
                    contadorEmprestimosComMulta++;
            }

            Emprestimo[] emprestimosComMulta = new Emprestimo[contadorEmprestimosComMulta];

            int contadorAuxiliar = 0;

            for (int i = 0; i < registros.Length; i++)
            {
                Emprestimo emprestimoAtual = (Emprestimo)registros[i];

                if (emprestimoAtual == null)
                    continue;

                if (emprestimoAtual.Status == "Aberto" && emprestimoAtual.Multa != null && !emprestimoAtual.MultaPaga)
                    emprestimosComMulta[contadorAuxiliar++] = (Emprestimo)registros[i];
            }

            return emprestimosComMulta;
        }
    }
}
