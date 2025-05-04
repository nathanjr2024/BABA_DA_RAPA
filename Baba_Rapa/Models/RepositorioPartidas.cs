using System.Text.Json;
using Models;

public class RepositorioPartidas
{
    private readonly string caminhoArquivo = "partidas.json";

    public List<Jogos> ListarTodas()
    {
        if (!File.Exists(caminhoArquivo)) return new List<Jogos>();
        var json = File.ReadAllText(caminhoArquivo);
        return JsonSerializer.Deserialize<List<Jogos>>(json) ?? new List<Jogos>();
    }

    public void Salvar(List<Jogos> partidas)
    {
        var json = JsonSerializer.Serialize(partidas, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(caminhoArquivo, json);
    }

    public void Adicionar(Jogos novaPartida)
    {
        var partidas = ListarTodas();
        partidas.Add(novaPartida);
        Salvar(partidas);
    }
}
