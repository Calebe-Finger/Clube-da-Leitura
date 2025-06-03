namespace ClubeDaLeitura.ConsoleApp.Compartilhado
{
    public class TelaPrincipal
    {
        private char opcaoEscolhida;

        public void MenuPrincipal()
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("||       CLUBE DA LEITURA        ||");
            Console.WriteLine("Qual módulo deseja acessar:");
            Console.WriteLine("1 - Módulo Amigos");
            Console.WriteLine("2 - Módulo de Caixas");
            Console.WriteLine("3 - Módulo de Revistas");
            Console.WriteLine("4 - Módulo de Empréstimos");
            Console.WriteLine("-----------------------------------");
            opcaoEscolhida = Console.ReadLine()[0];
        }

        public TelaBase ObterTela()
        {
            if (opcaoEscolhida == '1')
                return telaEquip;

            else if (opcaoEscolhida == '2')
                return telaChamado;

            else if (opcaoEscolhida == '3')
                return telaFabricante;

            return null;
        }
    }
}
