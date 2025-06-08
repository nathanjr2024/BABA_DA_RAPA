using Interfaces;
using Models;
using System.Text.Json;

namespace Repositories
{
    public class JogadorRepositorioJson : IJogadorRepositorio
    {
        private readonly string _caminho = Path.Combine(AppContext.BaseDirectory, @"..\..\..\Data\jogadores.json");
        private readonly List<Jogador> _jogadores;

        public JogadorRepositorioJson()
        {
            // Apenas para debug: mostra onde o arquivo será salvo
            Console.WriteLine($"📁 Salvando arquivo em: {_caminho}");

            // Garante que a pasta exista
            var pasta = Path.GetDirectoryName(_caminho)!;
            if (!Directory.Exists(pasta))
            {
                Directory.CreateDirectory(pasta);
            }

            // Se o arquivo não existir, cria um arquivo vazio com uma lista
            if (!File.Exists(_caminho))
            {
                File.WriteAllText(_caminho, "[]");
            }

            // Lê e desserializa os dados salvos
            var json = File.ReadAllText(_caminho, System.Text.Encoding.UTF8);
            _jogadores = JsonSerializer.Deserialize<List<Jogador>>(json) ?? new();
        }

        private void Salvar()
        {
            var json = JsonSerializer.Serialize(_jogadores, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_caminho, json, System.Text.Encoding.UTF8);
        }

        public Jogador? BuscarPorId(int id)
        {
            return _jogadores.FirstOrDefault(j => j.Id == id);
        }

        public List<Jogador> Listar()
        {
            return _jogadores;
        }

        public void Adicionar(Jogador jogador)
        {
            jogador.Validar();
            jogador.Id = _jogadores.Any() ? _jogadores.Max(j => j.Id) + 1 : 1;
            _jogadores.Add(jogador);
            Salvar();
        }

        public void Atualizar(Jogador jogador)
        {
            var existente = BuscarPorId(jogador.Id);
            if (existente != null)
            {
                existente.Nome = jogador.Nome;
                existente.Idade = jogador.Idade;
                existente.Posicao = jogador.Posicao;
                Salvar();
            }
        }

        public void Remover(int id)
        {
            var jogador = BuscarPorId(id);
            if (jogador != null)
            {
                _jogadores.Remove(jogador);
                Salvar();
            }
        }
        
    }
}
