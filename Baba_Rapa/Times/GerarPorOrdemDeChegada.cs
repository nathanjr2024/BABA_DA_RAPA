using Models;
namespace Models.Times{
    public class GeradorPorOrdemDeChegada : GeradorDeTimes
    {
        public override List<TimeDeJogo> GerarTimes(List<Jogador> jogadoresDisponiveis, int jogadoresPorTime)
        {
            var times = new List<TimeDeJogo>();
            //Calcula quantos times completos podem ser formados
            int totalTimes = jogadoresDisponiveis.Count / jogadoresPorTime;

            for (int i = 0; i < totalTimes; i++)
            {
                //pega um subconjunto da lista com a quantidade de jogadores necessárias
                var jogadores = jogadoresDisponiveis
                .Skip(i * jogadoresPorTime) // pula os jogadores já alocados em times anteriores
                .Take(jogadoresPorTime) // pega a quantidade necessária para formar um time
                .ToList();

                //cria o time com um nome e adiciona os jogadores
                var time = new TimeDeJogo
                {
                    Nome = $"Time{i + 1}",
                    Jogadores = jogadores
                };
                times.Add(time);
            }
            return times;
        }
    }
}