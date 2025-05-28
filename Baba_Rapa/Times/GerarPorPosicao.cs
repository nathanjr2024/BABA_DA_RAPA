namespace Models.Times
{
    // Gera times equilibrando as posições
    public class GeradorPorPosicao : GeradorDeTimes
    {
        public override List<TimeDeJogo> GerarTimes(List<Jogador> jogadoresDisponiveis, int jogadoresPorTime)
        {
            var times = new List<TimeDeJogo>();

            // Separa os jogadores por posição
            var goleiros = jogadoresDisponiveis.Where(j => j.Posicao == Enums.Posicao.Goleiro).ToList();
            var defensores = jogadoresDisponiveis.Where(j => j.Posicao == Enums.Posicao.Defesa).ToList();
            var atacantes = jogadoresDisponiveis.Where(j => j.Posicao == Enums.Posicao.Ataque).ToList();

            // Determina quantos times podem ser formados com base na quantidade de goleiros
            int numeroDeTimes = Math.Min(goleiros.Count, jogadoresDisponiveis.Count / jogadoresPorTime);

            for (int i = 0; i < numeroDeTimes; i++)
            {
                var time = new TimeDeJogo { Nome = $"Time {i + 1}" };

                // Coloca um goleiro se houver
                if (goleiros.Any())
                {
                    time.Jogadores.Add(goleiros[0]);
                    goleiros.RemoveAt(0);  // Remove o goleiro usado
                }

                // Adiciona defensores
                int defensoresPorTime = (jogadoresPorTime - time.Jogadores.Count) / 2;
                time.Jogadores.AddRange(defensores.Take(defensoresPorTime));
                defensores = defensores.Skip(defensoresPorTime).ToList();

                // Adiciona atacantes
                int atacantesRestantes = jogadoresPorTime - time.Jogadores.Count;
                time.Jogadores.AddRange(atacantes.Take(atacantesRestantes));
                atacantes = atacantes.Skip(atacantesRestantes).ToList();

                times.Add(time);
            }

            return times;
        }
    }
}
