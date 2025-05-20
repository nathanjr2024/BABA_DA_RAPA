using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using BABA_DO_RAPA.Modelos;

namespace Controllers
{
    public class JogosController
    {
        private readonly JogosRepository _repositorioPartidas = new JogosRepository();

        public void CadastrarPartida()
        {
            try
            {
                Console.Write("Nome do Time A: ");
                var timeA = Console.ReadLine();
                Console.Write("Nome do Time B: ");
                var timeB = Console.ReadLine();

                Console.Write("Data do jogo (dd/mm/yyyy): ");
                var data = DateTime.Parse(Console.ReadLine());

                Console.Write("Tipo de campo \n 1: Campo \n 2: Quadra \n 3: Society\nEscolha: ");
                int tipoCampo = int.Parse(Console.ReadLine());

                List<string> locaisDisponiveis;
                switch (tipoCampo)
                {
                    case 1: locaisDisponiveis = Locais.Campos.Lista; break;
                    case 2: locaisDisponiveis = Locais.Quadras.Lista; break;
                    case 3: locaisDisponiveis = Locais.Society.Lista; break;
                    default: Console.WriteLine("Tipo de campo inválido."); return;
                }

                Console.WriteLine("Locais disponíveis:");
                foreach (var local in locaisDisponiveis)
                    Console.WriteLine($"- {local}");

                Console.Write("Local: ");
                var localJogo = Console.ReadLine();

                if (!locaisDisponiveis.Contains(localJogo))
                {
                    Console.WriteLine("Local inválido para o tipo de campo escolhido.");
                    return;
                }

                Console.Write($"Gols de {timeA}: ");
                int golsA = int.Parse(Console.ReadLine());
                Console.Write($"Gols de {timeB}: ");
                int golsB = int.Parse(Console.ReadLine());

                Console.WriteLine("Modo de jogo:");
                Console.WriteLine("1. Quem ganha fica");
                Console.WriteLine("2. Dois jogos por time");
                Console.Write("Escolha: ");
                var modoEscolha = Console.ReadLine();

                var modo = modoEscolha == "2" ? ModoDeJogo.DoisJogosPorTime : ModoDeJogo.QuemGanhaFica;

                var gestor = new GestorDePartidas(new List<string> { timeA, timeB }, modo);

                var partida = gestor.JogarPartida(timeA, timeB, golsA, golsB, data, localJogo, tipoCampo);

                _repositorioPartidas.Adicionar(partida);
                Console.WriteLine($"Partida registrada com sucesso! Resultado: {partida.Resultado}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao cadastrar partida: {ex.Message}");
            }
        }

        public void ListarPartidas()
        {
            var partidas = _repositorioPartidas.ListarTodos();
            foreach (var p in partidas)
                Console.WriteLine($"Data: {p.DataDoJogo:dd/MM/yyyy}, Local: {p.Local}, Tipo de campo: {p.TipoDeCampo}, Jogadores: {p.QuantidadeDeJogadores}");
        }

        public void EditarPartida()
        {
            Console.Write("Digite a data da partida para editar (dd/mm/yyyy): ");
            var dataEditarStr = Console.ReadLine();
            if (DateTime.TryParse(dataEditarStr, out var dataEditar))
            {
                var partidaEdit = _repositorioPartidas.BuscarPorData(dataEditar);
                if (partidaEdit != null)
                {
                    try
                    {
                        Console.Write("Nova Data (dd/mm/yyyy): ");
                        partidaEdit.DataDoJogo = DateTime.Parse(Console.ReadLine());
                        Console.Write("Novo Local: ");
                        partidaEdit.Local = Console.ReadLine();
                        Console.Write("Novo Tipo de campo (1, 2, 3 ou 4): ");
                        partidaEdit.TipoDeCampo = int.Parse(Console.ReadLine());
                        Console.Write("Nova quantidade de jogadores: ");
                        partidaEdit.QuantidadeDeJogadores = int.Parse(Console.ReadLine());
                        _repositorioPartidas.Editar(partidaEdit);
                        Console.WriteLine("Partida atualizada.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao editar partida: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Partida não encontrada.");
                }
            }
            else
            {
                Console.WriteLine("Data inválida.");
            }
        }

        public void RemoverPartida()
        {
            Console.Write("Digite a data da partida para remover (dd/mm/yyyy): ");
            var dataRemoverStr = Console.ReadLine();
            if (DateTime.TryParse(dataRemoverStr, out var dataRemover))
            {
                _repositorioPartidas.Remover(dataRemover);
                Console.WriteLine("Partida removida.");
            }
            else
            {
                Console.WriteLine("Data inválida.");
            }
        }

        public void ExibirRanking()
        {
            var partidasRegistradas = _repositorioPartidas.ListarTodos();
            var gestorRanking = new GestorDePartidas(partidasRegistradas.Select(p => p.TimeA).Concat(partidasRegistradas.Select(p => p.TimeB)), ModoDeJogo.QuemGanhaFica);
            foreach (var p in partidasRegistradas)
                gestorRanking.JogarPartida(p.TimeA, p.TimeB, p.GolsTimeA, p.GolsTimeB, p.DataDoJogo, p.Local, p.TipoDeCampo);

            var ranking = gestorRanking.ObterRankingVitorias();
            Console.WriteLine("--- Ranking de Vitórias ---");
            foreach (var item in ranking)
                Console.WriteLine($"{item.Key}: {item.Value} vitórias");
        }
    }
}