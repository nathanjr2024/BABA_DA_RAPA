
using System;
using System.Collections.Generic;
using System.Linq;
using Models;

public enum ModoDeJogo
{
    QuemGanhaFica,
    DoisJogosPorTime
}

public class GestorDePartidas
{
    private Queue<string> filaTimes;
    private Dictionary<string, int> jogosPorTime = new();
    private ModoDeJogo modo;
    private List<Jogos> historico = new();

    public GestorDePartidas(IEnumerable<string> nomesTimes, ModoDeJogo modoSelecionado)
    {
        filaTimes = new Queue<string>(nomesTimes);
        modo = modoSelecionado;
    }

    public Jogos JogarPartida(string timeA, string timeB, int golsA, int golsB, DateTime data, string local, int tipoCampo)
    {
        var jogo = new Jogos
        {
            TimeA = timeA,
            TimeB = timeB,
            GolsTimeA = golsA,
            GolsTimeB = golsB,
            DataDoJogo = data,
            TipoDeCampo = tipoCampo,
            Local = local,
            QuantidadeDeJogadores = 10
        };

        historico.Add(jogo);

        jogosPorTime.TryAdd(timeA, 0);
        jogosPorTime.TryAdd(timeB, 0);
        jogosPorTime[timeA]++;
        jogosPorTime[timeB]++;

        if (modo == ModoDeJogo.QuemGanhaFica)
        {
            string vencedor = golsA > golsB ? timeA : golsB > golsA ? timeB : null;
            if (vencedor != null) filaTimes.Enqueue(vencedor);
        }
        else if (modo == ModoDeJogo.DoisJogosPorTime)
        {
            if (jogosPorTime[timeA] < 2) filaTimes.Enqueue(timeA);
            if (jogosPorTime[timeB] < 2) filaTimes.Enqueue(timeB);
        }

        return jogo;
    }

    public List<Jogos> ObterHistorico() => historico;

    public Dictionary<string, int> ObterRankingVitorias()
    {
        var ranking = new Dictionary<string, int>();
        foreach (var jogo in historico)
        {
            string vencedor = jogo.GolsTimeA > jogo.GolsTimeB ? jogo.TimeA :
                              jogo.GolsTimeB > jogo.GolsTimeA ? jogo.TimeB : null;

            if (vencedor != null)
            {
                ranking.TryAdd(vencedor, 0);
                ranking[vencedor]++;
            }
        }
        return ranking.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
    }
}
