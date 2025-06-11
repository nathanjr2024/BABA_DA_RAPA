namespace Models
{
    public class TimeDeJogo
    {
        public string Nome { get; set; } = string.Empty;
        public List<Jogador> Jogadores { get; set; } = new();
        public override string ToString()
        {
            return $"{Nome}: " + string.Join(", ", Jogadores.Select(j => $"{j.Nome} ({j.Idade} anos - {j.Posicao})"));
        }
    }
}