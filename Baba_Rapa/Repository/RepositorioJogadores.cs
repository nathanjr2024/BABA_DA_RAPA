using System;
using System.Collections.Generic;
using MySqlConnector;
using BABA_DO_RAPA.Modelos;

namespace Repository
{
    public class RepositorioJogadores
    {
        private readonly string _connectionString = "Server=localhost;Database=erp do baba;User ID=root;Password=Qcy@2025;";

        public void Adicionar(Jogador jogador)
        {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = @"
        INSERT INTO Jogadores (Nome, Idade, Posicao)
        VALUES (@nome, @idade, @posicao)";
        cmd.Parameters.AddWithValue("@nome", jogador.Nome);
        cmd.Parameters.AddWithValue("@idade", jogador.Idade);
        cmd.Parameters.AddWithValue("@posicao", jogador.Posicao);
        cmd.ExecuteNonQuery();
        }

        public List<Jogador> ListarTodos()
        {
            var lista = new List<Jogador>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Jogadores";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Jogador
                {
                    Id = reader.GetInt32("Id"),
                    Nome = reader.GetString("Nome"),
                    Idade = reader.GetInt32("Idade"),
                    Posicao = reader.GetString("Posicao")
                });
            }
            return lista;
        }

        public Jogador BuscarPorId(string id)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Jogadores WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Jogador
                {
                    Id = reader.GetInt32("Id"),
                    Nome = reader.GetString("Nome"),
                    Idade = reader.GetInt32("Idade"),
                    Posicao = reader.GetString("Posicao")
                };
            }
            return null;
        }

        public void Editar(Jogador jogador)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE Jogadores 
                SET Nome = @nome, Idade = @idade, Posicao = @posicao
                WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", jogador.Id);
            cmd.Parameters.AddWithValue("@nome", jogador.Nome);
            cmd.Parameters.AddWithValue("@idade", jogador.Idade);
            cmd.Parameters.AddWithValue("@posicao", jogador.Posicao);
            cmd.ExecuteNonQuery();
        }

        public void Remover(string id)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Jogadores WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}