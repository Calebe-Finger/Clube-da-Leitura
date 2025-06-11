using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.Modulo_de_Caixas;

namespace ClubeDaLeitura.ConsoleApp.Modulo_de_Revistas
{
    public class Revista : EntidadeBase
    {
        public string Titulo { get; set; }
        public int NumeroEdicao { get; set;}
        public int AnoPublicacao { get; set;}
        public Caixa Caixa { get; set;}

        public string Status { get; set;}

        public Revista(string titulo, int numeroEdicao, int anoPublicacao, Caixa caixa)
        {
            Titulo = titulo;
            NumeroEdicao = numeroEdicao;
            AnoPublicacao = anoPublicacao;
            Caixa = caixa;
            Status = "Disponível";  
        }

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
                erros += "O campo \"Título\" deve conter entre 2 e 100 caracteres.";

            if (NumeroEdicao < 0)
                erros += "O campo \"Número de Edição\" deve ser positívo.";

            if (AnoPublicacao < DateTime.MinValue.Year || AnoPublicacao > DateTime.MaxValue.Year)
                erros += "O campo \"Ano de Publicação\" deve conter um ano válido no passado e no presente.";

            if (Caixa == null)
                erros += "O campo \"Caixa\" é obrigatório";

            return erros;
        }
    }
}
