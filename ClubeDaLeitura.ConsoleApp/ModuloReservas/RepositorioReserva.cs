using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

namespace ClubeDaLeitura.ConsoleApp.ModuloReservas
{
    public class RepositorioReserva : RepositorioBase
    {
        public Reserva[] SelecionarReservasAtivas()
        {
            int contadorReservasAtivas = 0;

            for (int i = 0; i < registros.Length; i++)
            {
                Reserva reservaAtual = (Reserva)registros[i];

                if (reservaAtual == null)
                    continue;

                if (reservaAtual.EstaAtiva)
                    contadorReservasAtivas++;
            }

            Reserva[] reservasAtivas = new Reserva[contadorReservasAtivas];

            int contadorAuxiliar = 0;

            for (int i = 0; i < registros.Length; i++)
            {
                Reserva reservaAtual = (Reserva)registros[i];

                if (reservaAtual == null)
                    continue;

                if (reservaAtual.EstaAtiva)
                    reservasAtivas[contadorAuxiliar++] = (Reserva)registros[i];
            }

            return reservasAtivas;
        }
    }
}
