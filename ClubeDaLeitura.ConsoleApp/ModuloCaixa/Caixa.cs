using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.Modulo_de_Caixas
{
    public class Caixa : EntidadeBase
    {
        public string Etiqueta {  get; set; }
        public string Cor {  get; set; }
        public int DiasEmprestimo { get; set; }

        public Caixa(string etiqueta, string cor)
        {
            Etiqueta = etiqueta;
            Cor = cor;
            DiasEmprestimo = 7;
        }

        public Caixa (string etiqueta, string cor, int diasEmprestimo)
        {
            Etiqueta = etiqueta;
            Cor = cor;
            DiasEmprestimo = diasEmprestimo;
        }

        public override void AtualizarRegistro(EntidadeBase registroAtualizado)
        {
            Caixa caixaAtualizada = (Caixa)registroAtualizado;

            Etiqueta = caixaAtualizada.Etiqueta;
            Cor = caixaAtualizada.Cor;
            DiasEmprestimo = caixaAtualizada.DiasEmprestimo;
        }

        public override string Validar()
        {
            string erros = string.Empty;

            if (Etiqueta.Length > 50 || Etiqueta.Length < 2 || string.IsNullOrEmpty(Etiqueta))
                erros += "O campo \"Etiqueta\" deve conter entre 2 e 50 caracteres";

            if (string.IsNullOrEmpty(Cor))
                erros += "O campo \"Cor\" é obrigatório";

            if (DiasEmprestimo < 1)
                erros += "O campo \"Dias de Empréstimo\" deve conter um valor maior que 0";

            return erros;
        }
    }
}
