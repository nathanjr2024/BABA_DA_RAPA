namespace Models
{
    public class Jogo
    {
        public string Id { get; set; } = GerarIdSimplesJogo();
        public DateTime Data { get; set; }
        public string Local { get; set; } = string.Empty;
        public string TipoCampo { get; set; } = string.Empty;
        public int JogadoresPorTime { get; set; }
        public int? LimiteDeTimes { get; set; }
        public List<string> Interessados { get; set; } = new();
        public List<int> InteressadosIds { get; set; } = new();
        public bool Confirmado => Interessados.Count >= (JogadoresPorTime * 2);

        private static string GerarIdSimplesJogo()
        {
            var random = new Random();

            const string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numeros = "0123456789";

            char letra1 = letras[random.Next(letras.Length)];
            char numero = numeros[random.Next(numeros.Length)];
            char letra2 = letras[random.Next(letras.Length)];

            return $"{letra1}{numero}{letra2}";
        }

    }
}