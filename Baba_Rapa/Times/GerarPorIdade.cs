namespace Models.Times
{
     public class GeradorPorFaixaEtaria : GeradorDeTimes
    {
        private readonly int faixaMaxima;

        /// Construtor permite definir a diferença máxima de idade entre os jogadores do mesmo time
        public GeradorPorFaixaEtaria(int faixaMaxima = 5)
        {
            this.faixaMaxima = faixaMaxima;
        }

        public override List<TimeDeJogo> GerarTimes(List<Jogador> jogadoresDisponiveis, int jogadoresPorTime)
        {
            var times = new List<TimeDeJogo>();

            // Ordena os jogadores por idade
            var ordenados = jogadoresDisponiveis.OrderBy(j => j.Idade).ToList();
            int contador = 1;

            while (ordenados.Count >= jogadoresPorTime)
            {
                var time = new TimeDeJogo { Nome = $"Time {contador++}" };

                // Primeiro jogador do time
                var baseJogador = ordenados[0];
                time.Jogadores.Add(baseJogador);
                ordenados.RemoveAt(0);

                // Seleciona jogadores com idade próxima
                var proximos = ordenados
                    .Where(j => Math.Abs(j.Idade - baseJogador.Idade) <= faixaMaxima)
                    .Take(jogadoresPorTime - 1)  
                    .ToList();

                foreach (var jogador in proximos)
                {
                    time.Jogadores.Add(jogador);
                    ordenados.Remove(jogador);
                }

                times.Add(time);
            }

            return times;
        }
    }
}