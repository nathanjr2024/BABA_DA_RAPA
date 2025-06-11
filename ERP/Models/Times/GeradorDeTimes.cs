namespace Models.Times
{
    public abstract class GeradorDeTimes
    {
        public abstract List<TimeDeJogo> GerarTimes(List<Jogador> jogadoresDisponiveis, int jogadoresPorTime);
    }
}