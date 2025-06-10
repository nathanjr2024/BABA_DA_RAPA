using Interfaces;
using Models;
using System.Text.Json;

namespace Repositories
{
    public class JogoRepositorioJson : IJogoRepositorio
    {
        private readonly string _caminho = Path.Combine(AppContext.BaseDirectory, @"..\..\..\Data\jogos.json");
        private readonly List<Jogo> _jogos;

        public JogoRepositorioJson()
        {
            Console.WriteLine($"📁 Salvando arquivo em: {_caminho}");

            var pasta = Path.GetDirectoryName(_caminho)!;
            if (!Directory.Exists(pasta))
            {
                Directory.CreateDirectory(pasta);
            }

            if (!File.Exists(_caminho))
            {
                File.WriteAllText(_caminho, "[]");
            }

            var json = File.ReadAllText(_caminho, System.Text.Encoding.UTF8);
            _jogos = JsonSerializer.Deserialize<List<Jogo>>(json) ?? new List<Jogo>();
        }

        public void Adicionar(Jogo jogo)
        {
            _jogos.Add(jogo);
            Salvar();
        }

        public void Atualizar(Jogo jogo)
        {
            var existente = BuscarPorId(jogo.Id);
           if (existente != null)
            {
                existente.Data = jogo.Data;
                existente.Local = jogo.Local;
                existente.TipoCampo = jogo.TipoCampo;
                existente.JogadoresPorTime = jogo.JogadoresPorTime;
                existente.LimiteDeTimes = jogo.LimiteDeTimes;
                existente.Interessados = jogo.Interessados;
                Salvar();
            }
        }

        public List<Jogo> Listar() => _jogos;

        public Jogo? BuscarPorId(string id) => _jogos.FirstOrDefault(j => j.Id == id);

        public void Salvar()
        {
            var json = JsonSerializer.Serialize(_jogos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_caminho, json, System.Text.Encoding.UTF8);
        }

    }
}
