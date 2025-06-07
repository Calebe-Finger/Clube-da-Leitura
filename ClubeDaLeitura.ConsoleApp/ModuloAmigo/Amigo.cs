using ClubeDaLeitura.ConsoleApp.Compartilhado;
using System.Text.RegularExpressions;

namespace ClubeDaLeitura.ConsoleApp.Modulo_de_Amigos
{
    public class Amigo : EntidadeBase
    {
        public string Nome { get; set; }
        public string NomeResponsavel { get; set; }
        public string Telefone { get; set; }

        public Amigo(string nome, string nomeResponsavel, string telefone)
        {
            Nome = nome;
            NomeResponsavel = nomeResponsavel;
            Telefone = telefone;
        }
        public override void AtualizarRegistro(EntidadeBase registroAtualizado)
        {
            Amigo chamadoAtualizado = (Amigo)registroAtualizado;

            this.Nome = chamadoAtualizado.Nome;
            this.NomeResponsavel = chamadoAtualizado.NomeResponsavel;
            this.Telefone = chamadoAtualizado.Telefone;
        }

        public override string Validar()
        {
            string erros = string.Empty;

            if (string.IsNullOrWhiteSpace(Nome))
                erros += "O nome é obrigatório!\n";

            else if (Nome.Length < 3 || Nome.Length > 100)
                erros += "O campo \"Nome\" deve conter entre 3 e 100 caracteres!\n";

            if (string.IsNullOrWhiteSpace(NomeResponsavel))
                erros += "O nome do responsável legal é obrigatório\n";

            else if (NomeResponsavel.Length < 3 || NomeResponsavel.Length > 100)
                erros += "O campo \"Nome do Responsável\" deve conter entre 3 e 100 caracteres!\n";

            if (!Regex.IsMatch(Telefone, @"^\(?\d{2}\)?\s?(9\d{4}|\d{4})-?\d{4}$"))
                erros += "O campo \"Telefone\" deve seguir o padrão (DDD) 99999-88888.";

            return erros;
        }
    }
}
