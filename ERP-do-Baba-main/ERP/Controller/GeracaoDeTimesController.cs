using Interfaces;
using Models;
using Models.Times;
using View;

namespace Controller
{
    public class GeracaoDeTimesController
    {
        private readonly IJogoRepositorio _jogoRepositorio;
        private readonly IJogadorRepositorio _jogadorRepositorio;
        private readonly GeracaoDeTimesView _view;

        public GeracaoDeTimesController(
            IJogoRepositorio jogoRepositorio,
            IJogadorRepositorio jogadorRepositorio,
            GeracaoDeTimesView view)
        {
            _jogoRepositorio = jogoRepositorio;
            _jogadorRepositorio = jogadorRepositorio;
            _view = view;
        }

        public void Menu()
        {
            while (true)
            {
                var opcao = _view.ExibirMenu();

                switch (opcao)
                {
                    case "1":
                        GerarTimes();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }

        private void GerarTimes()
        {
            try
            {
                Console.Write("Informe o ID do Jogo para gerar os times: ");


                string jogoId = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrEmpty(jogoId))
                {
                    Console.WriteLine("ID inválido.");
                    return;
                }

                var jogo = _jogoRepositorio.BuscarPorId(jogoId);

                if (jogo != null)
                {
                    Console.WriteLine("\n[DEBUG] Verificando jogadores interessados por ID:");
                    foreach (var id in jogo.InteressadosIds)
                    {
                        var jogador = _jogadorRepositorio.BuscarPorId(id);
                        if (jogador == null)
                            Console.WriteLine($"[⚠] Jogador com ID {id} não encontrado no repositório.");
                        else
                            Console.WriteLine($"[✔] Jogador encontrado: {jogador.Nome} (ID: {jogador.Id})");
                    }

                    var jogadoresDisponiveis = jogo.InteressadosIds
                        .Select(id => _jogadorRepositorio.BuscarPorId(id))
                        .Where(j => j != null)
                        .Cast<Jogador>()
                        .ToList();

                    if (jogadoresDisponiveis.Count < jogo.JogadoresPorTime * 2)
                    {
                        Console.WriteLine("Não há jogadores suficientes para formar dois times completos.");
                        return;
                    }

                    int metodo = _view.EscolherMetodoGeracao();

                    GeradorDeTimes gerador = metodo switch
                    {
                        1 => new GeradorPorOrdemDeChegada(),
                        2 => new GeradorPorPosicao(),
                        3 => new GeradorPorFaixaEtaria(_view.DefinirFaixaEtaria()),
                        _ => new GeradorPorOrdemDeChegada()
                    };

                    var timesGerados = gerador.GerarTimes(jogadoresDisponiveis, jogo.JogadoresPorTime);

                    if (!timesGerados.Any())
                    {
                        Console.WriteLine("Não foi possível gerar times com os jogadores disponíveis.");
                        return;
                    }

                    _view.ListarTimes(timesGerados);
                }
                else
                {
                    Console.WriteLine("Jogo não encontrado.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao gerar times: {ex.Message}");
            }
        }
    }
}
