namespace ClubeDaLeitura.ConsoleApp.Compartilhado
{
    public class RepositorioBase
    {
        protected EntidadeBase[] registros = new EntidadeBase[100];
        protected int contadorRegistros = 0;

        public void CadastrarRegistro(EntidadeBase novoRegistro)
        {
            registros[contadorRegistros] = novoRegistro;

            contadorRegistros++;
        }

        public bool EditarRegistro(int idSelecionado, EntidadeBase registroAtualizado)
        {
            EntidadeBase registroSelecionado = SelecionarRegistroPorId(idSelecionado);

            if (registroSelecionado == null)
                return false;

            registroSelecionado.AtualizarRegistro(registroAtualizado);

            return true;
        }

        public bool ExcluirRegistro(int idSelecionado)
        {
            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] == null)
                    continue;

                else if (registros[i].Id == idSelecionado)
                {
                    registros[i] = null;

                    return true;
                }
            }

            return false;
        }

        public EntidadeBase[] SelecionarRegistros()
        {
            return registros;
        }

        public EntidadeBase SelecionarRegistroPorId(int idSelecionado)
        {
            for (int i = 0; i < registros.Length; i++)
            {
                EntidadeBase registro = registros[i];

                if (registro == null)
                    continue;

                if (registro.Id == idSelecionado)
                    return registro;
            }

            return null;
        }   
    }
}
