using Models.Enums;

namespace Models
{
    public class Partida
    {
        public string Id { get; set; } = GerarIdSimplesPartida();
        public string JogoId { get; set; } = string.Empty;
        public List<string> TimeA { get; set; } = new();
        public List<string> TimeB { get; set; } = new();
        public string Vencedor { get; set; } = string.Empty; // "A", "B" ou "Empate"
        public DateTime DataHora { get; set; } = DateTime.Now;
        public int NumeroDaRodada { get; set; }
        public ModoPartida Modo { get; set; }

        private static string GerarIdSimplesPartida()
        {
            var random = new Random();

            const string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numeros = "0123456789";

            char letra = letras[random.Next(letras.Length)];
            char numero = numeros[random.Next(numeros.Length)];

            return $"{letra}{numero}";
        }
    
    }

   

}