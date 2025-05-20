using System;
using System.Collections.Generic;
using MySqlConnector;
using Models;

namespace Repository
{
    public class JogosRepository
    {
        private readonly string _connectionString = "Server=localhost;Database=erp do baba;User ID=root;Password=Qcy@2025;";

        public void Adicionar(Jogos jogo)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO Partidas 
                (TimeA, TimeB, GolsTimeA, GolsTimeB, DataDoJogo, Local, TipoDeCampo, QuantidadeDeJogadores)
                VALUES (@timeA, @timeB, @golsA, @golsB, @data, @local, @tipoCampo, @qtdJogadores)";
            cmd.Parameters.AddWithValue("@timeA", jogo.TimeA);
            cmd.Parameters.AddWithValue("@timeB", jogo.TimeB);
            cmd.Parameters.AddWithValue("@golsA", jogo.GolsTimeA);
            cmd.Parameters.AddWithValue("@golsB", jogo.GolsTimeB);
            cmd.Parameters.AddWithValue("@data", jogo.DataDoJogo);
            cmd.Parameters.AddWithValue("@local", jogo.Local);
            cmd.Parameters.AddWithValue("@tipoCampo", jogo.TipoDeCampo);
            cmd.Parameters.AddWithValue("@qtdJogadores", jogo.QuantidadeDeJogadores);
            cmd.ExecuteNonQuery();
        }

        public List<Jogos> ListarTodos()
        {
            var lista = new List<Jogos>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Partidas";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Jogos
                {
                    TimeA = reader.GetString("TimeA"),
                    TimeB = reader.GetString("TimeB"),
                    GolsTimeA = reader.GetInt32("GolsTimeA"),
                    GolsTimeB = reader.GetInt32("GolsTimeB"),
                    DataDoJogo = reader.GetDateTime("DataDoJogo"),
                    Local = reader.GetString("Local"),
                    TipoDeCampo = reader.GetInt32("TipoDeCampo"),
                    QuantidadeDeJogadores = reader.GetInt32("QuantidadeDeJogadores")
                });
            }
            return lista;
        }

        public Jogos BuscarPorData(DateTime data)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Partidas WHERE DataDoJogo = @data";
            cmd.Parameters.AddWithValue("@data", data);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Jogos
                {
                    TimeA = reader.GetString("TimeA"),
                    TimeB = reader.GetString("TimeB"),
                    GolsTimeA = reader.GetInt32("GolsTimeA"),
                    GolsTimeB = reader.GetInt32("GolsTimeB"),
                    DataDoJogo = reader.GetDateTime("DataDoJogo"),
                    Local = reader.GetString("Local"),
                    TipoDeCampo = reader.GetInt32("TipoDeCampo"),
                    QuantidadeDeJogadores = reader.GetInt32("QuantidadeDeJogadores")
                };
            }
            return null;
        }

        public void Editar(Jogos jogo)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE Partidas 
                SET Local = @local, TipoDeCampo = @tipoCampo, QuantidadeDeJogadores = @qtdJogadores
                WHERE DataDoJogo = @data";
            cmd.Parameters.AddWithValue("@local", jogo.Local);
            cmd.Parameters.AddWithValue("@tipoCampo", jogo.TipoDeCampo);
            cmd.Parameters.AddWithValue("@qtdJogadores", jogo.QuantidadeDeJogadores);
            cmd.Parameters.AddWithValue("@data", jogo.DataDoJogo);
            cmd.ExecuteNonQuery();
        }

        public void Remover(DateTime data)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Partidas WHERE DataDoJogo = @data";
            cmd.Parameters.AddWithValue("@data", data);
            cmd.ExecuteNonQuery();
        }
    }
}