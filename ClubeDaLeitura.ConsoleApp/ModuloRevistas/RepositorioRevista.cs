using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.Modulo_de_Revistas
{
    public class RepositorioRevista : RepositorioBase
    {
        public Revista[] SelecionarRevistasDisponiveis()
        {
            int contadorRevistasDisponiveis = 0;

            for (int i = 0; i < registros.Length; i++)
            {
                Revista revistaAtual = (Revista)registros[i];

                if (revistaAtual == null)
                    continue;

                if (revistaAtual.Status == "Disponível")
                    contadorRevistasDisponiveis++;
            }

            Revista[] revistasDisponiveis = new Revista[contadorRevistasDisponiveis];

            int contadorAuxiliar = 0;

            for (int i = 0; i < registros.Length; i++)
            {
                Revista revistaAtual = (Revista)registros[i];

                if (revistaAtual == null)
                    continue;

                if (revistaAtual.Status == "Disponível")
                    revistasDisponiveis[contadorAuxiliar++] = (Revista)registros[i];
            }

            return revistasDisponiveis;
        }
    }
}
