// Repositorio.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace BABA_DO_RAPA.Modelos
{
    public class RepositorioJogadores
    {
        private readonly string caminhoArquivo = "jogadores.json";

        public List<Jogador> ListarTodos()
        {
            if (!File.Exists(caminhoArquivo)) return new List<Jogador>();
            var json = File.ReadAllText(caminhoArquivo);
            return JsonSerializer.Deserialize<List<Jogador>>(json) ?? new List<Jogador>();
        }

        public void Salvar(List<Jogador> jogadores)
        {
            var json = JsonSerializer.Serialize(jogadores, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(caminhoArquivo, json);
        }

        public void Adicionar(Jogador jogador)
        {
            var jogadores = ListarTodos();
            if (jogadores.Any(j => j.Id == jogador.Id))
                throw new Exception("ID já existente. Escolha outro ID.");
            jogadores.Add(jogador);
            Salvar(jogadores);
        }

        public Jogador BuscarPorId(string id)
        {
            return ListarTodos().FirstOrDefault(j => j.Id == id);
        }

        public void Editar(Jogador jogadorEditado)
        {
            var jogadores = ListarTodos();
            var index = jogadores.FindIndex(j => j.Id == jogadorEditado.Id);
            if (index >= 0)
            {
                jogadores[index] = jogadorEditado;
                Salvar(jogadores);
            }
        }

        public void Remover(string id)
        {
            var jogadores = ListarTodos();
            var jogador = jogadores.FirstOrDefault(j => j.Id == id);
            if (jogador != null)
            {
                jogadores.Remove(jogador);
                Salvar(jogadores);
            }
        }
    }
}

