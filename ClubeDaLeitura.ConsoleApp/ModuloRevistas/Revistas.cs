using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.Modulo_de_Caixas;

namespace ClubeDaLeitura.ConsoleApp.Modulo_de_Revistas
{
    public class Revista : EntidadeBase
    {
        public Revista(string titulo, int numeroEdicao, DateTime anoPublicacao, Caixa caixa)
        {
            Titulo = titulo;
            NumeroEdicao = numeroEdicao;
            AnoPublicacao = anoPublicacao;
            Caixa = caixa;
        }

        public string Titulo { get; set; }
        public int NumeroEdicao { get; set;}
        public DateTime AnoPublicacao { get; set;}
        public Caixa Caixa { get; set;}

        public override void AtualizarRegistro(EntidadeBase registroAtualizado)
        {
            Revista revista = (Revista)registroAtualizado;

            Titulo = revista.Titulo;
            NumeroEdicao = revista.NumeroEdicao;
            AnoPublicacao = revista.AnoPublicacao;
            Caixa = revista.Caixa;  
        }

        public override string Validar()
        {
            string erros = string.Empty;

            if (Titulo.Length < 2 || Titulo.Length > 100)
                erros += "O campo Título deve conter entre 2 e 100 caracteres";

            if (NumeroEdicao < 0)
                erros += "O campo Número de Edição deve ser positívo";

            return erros;
        }
    }
}
