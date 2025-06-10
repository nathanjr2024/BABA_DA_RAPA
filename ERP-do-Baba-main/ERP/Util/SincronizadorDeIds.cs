using Models;
using System.Text.Json;

namespace Utils
{
    public class SincronizadorDeIds
    {
        private readonly List<Jogador> _jogadores;
        private readonly string _caminhoJogos;

        public SincronizadorDeIds(List<Jogador> jogadores)
        {
            _jogadores = jogadores;
            _caminhoJogos = Path.Combine(AppContext.BaseDirectory, @"..\..\..\Data\jogos.json");
        }

        public void Sincronizar()
        {
            if (!File.Exists(_caminhoJogos))
            {
                Console.WriteLine("Arquivo de jogos não encontrado.");
                return;
            }

            var json = File.ReadAllText(_caminhoJogos);
            var jogos = JsonSerializer.Deserialize<List<Jogo>>(json) ?? new();

            foreach (var jogo in jogos)
            {
                var idsSincronizados = new List<int>();

                foreach (var nome in jogo.Interessados)
                {
                    var jogador = _jogadores.FirstOrDefault(j => string.Equals(j.Nome.Trim(), nome.Trim(), StringComparison.OrdinalIgnoreCase));
                    if (jogador != null && !idsSincronizados.Contains(jogador.Id))
                    {
                        idsSincronizados.Add(jogador.Id);
                    }
                }

                jogo.InteressadosIds = idsSincronizados;
            }

            var novoJson = JsonSerializer.Serialize(jogos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_caminhoJogos, novoJson);
            Console.WriteLine("✅ Sincronização concluída.");
        }
    }
}
