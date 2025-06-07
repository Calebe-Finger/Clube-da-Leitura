using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.Modulo_de_Caixas
{
    public class Caixa : EntidadeBase
    {
        public string Etiqueta {  get; set; }
        public string Cor {  get; set; }
        public int DiasEmprestimo { get; set; }

        public Caixa (string etiqueta, string cor, int DiasEmprestimo)
        {
            Etiqueta = etiqueta;
            Cor = cor;
            this.DiasEmprestimo = DiasEmprestimo;
        }

        public override void AtualizarRegistro(EntidadeBase registroAtualizado)
        {
            Caixa caixaAtualizado = (Caixa)registroAtualizado;

            Etiqueta = caixaAtualizado.Etiqueta;
            Cor = caixaAtualizado.Cor;
            DiasEmprestimo = caixaAtualizado.DiasEmprestimo;
        }

        public override string Validar()
        {
            string erros = string.Empty;

            if (Etiqueta.Length > 50 || Etiqueta.Length <= 0)
                erros += "O campo Etiqueta deve conter entre 1 e 50 caracteres";

            return erros;
        }
    }
}
