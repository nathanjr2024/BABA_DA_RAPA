using Models;
namespace View
{
    public class GeracaoDeTimesView
    {

        //Exibe o menu de geração de times
        public string ExibirMenu()
        {
            Console.WriteLine("\n=== GESTÃO DE TIMES ===");
            Console.WriteLine("1 - Gerar Times");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha uma opção: ");
            return Console.ReadLine() ?? "0";
        }
        //permite escolher o método da geração de times
        public int EscolherMetodoGeracao()
        {
            Console.WriteLine("\n=== GESTÃO DE TIMES ===");
            Console.WriteLine("1 - Ordem de Chegada");
            Console.WriteLine("2 - Equilíbrio por Posição");
            Console.WriteLine("3 - Faixa Etária");
            Console.WriteLine("4 - Remover Jogador");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha uma opção: ");
            return int.Parse(Console.ReadLine() ?? "0");
        }

        public int DefinirFaixaEtaria()
        {
            Console.Write("Informe a faixa etária máxima de diferença: ");
            return int.Parse(Console.ReadLine() ?? "10");
        }

        public void ListarTimes(List<TimeDeJogo> timesGerados)
        {
            for (int i = 0; i < timesGerados.Count; i++)
            {
                Console.WriteLine($"\n Time {i + 1}:");
                foreach (var jogador in timesGerados[i].Jogadores)
                {
                    Console.WriteLine($"- {jogador.Nome} | Idade: {jogador.Idade} | Posição: {jogador.Posicao}");
                }

            }
        }
    }
}