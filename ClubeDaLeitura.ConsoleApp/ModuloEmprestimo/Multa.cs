namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    public class Multa
    {
        public decimal Valor { get; set; }
        public DateTime DataOcorrencia { get; set; }

        public Multa(decimal valor)
        {
            Valor = valor;
            DataOcorrencia = DateTime.Now;
        }
    }
}
