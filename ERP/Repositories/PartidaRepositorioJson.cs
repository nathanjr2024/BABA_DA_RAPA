using Interfaces;
using Models;
using System.Text.Json;

namespace Repositories
{
    public class PartidaRepositorioJson : IPartidaRepositorio
    {
        private readonly string _caminho = Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\Data\\partidas.json");
        private readonly List<Partida> _partidas;

        public PartidaRepositorioJson()
        {
            if (!File.Exists(_caminho))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_caminho)!);
                File.WriteAllText(_caminho, "[]", System.Text.Encoding.UTF8);
            }

            var json = File.ReadAllText(_caminho, System.Text.Encoding.UTF8);
            _partidas = JsonSerializer.Deserialize<List<Partida>>(json) ?? new();
        }

        public void Adicionar(Partida partida)
        {
            _partidas.Add(partida);
            Salvar();
        }

        public List<Partida> Listar() => _partidas;

        public List<Partida> ListarPorJogo(string jogoId)
        {
            return _partidas.Where(p => p.JogoId == jogoId).ToList();
        }

        private void Salvar()
        {
            var json = JsonSerializer.Serialize(_partidas, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_caminho, json, System.Text.Encoding.UTF8);
        }
    }
}