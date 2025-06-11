using Interfaces;
using Models;
using Models.Enums;

namespace View
{
    public class PartidaView
    {
        public string ExibirMenu()
        {
            Console.WriteLine("\n=== GESTÃO DE PARTIDAS ===");
            Console.WriteLine("1 - Adicionar Partida");
            Console.WriteLine("2 - Listar Partidas");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha uma opção: ");
            return Console.ReadLine() ?? "0";
        }

        public void Adicionar(IPartidaRepositorio repositorio, IJogoRepositorio jogoRepositorio, IJogadorRepositorio jogadorRepositorio)
        {
            try
            {
                var partida = new Partida();

                var jogos = jogoRepositorio.Listar();
                if (!jogos.Any())
                {
                    Console.WriteLine("❌ Nenhum jogo encontrado.");
                    return;
                }

                Console.WriteLine("Selecione o jogo para vincular à partida:");
                for (int i = 0; i < jogos.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {jogos[i].Data:dd/MM/yyyy} - {jogos[i].Local} ({jogos[i].Id})");
                }

                Console.Write("Escolha o número do jogo: ");
                if (!int.TryParse(Console.ReadLine(), out int escolha) || escolha < 1 || escolha > jogos.Count)
                {
                    Console.WriteLine("Escolha inválida.");
                    return;
                }

                partida.JogoId = jogos[escolha - 1].Id;

                var jogadores = jogadorRepositorio.Listar();
                if (!jogadores.Any())
                {
                    Console.WriteLine("❌ Nenhum jogador disponível.");
                    return;
                }

                Console.WriteLine("\n--- Jogadores Disponíveis ---");
                foreach (var jogador in jogadores)
                {
                    Console.WriteLine($"{jogador.Id} - {jogador.Nome} - {jogador.Posicao}");
                }

                List<string> timeA = new();
                List<string> timeB = new();
                HashSet<int> usados = new();

                // Adicionando jogadores ao Time A
                Console.WriteLine("\nSelecione jogadores para o Time A (IDs separados por vírgula):");
                var entradaA = Console.ReadLine()?.Split(',') ?? Array.Empty<string>();

                foreach (var idStr in entradaA)
                {
                    if (int.TryParse(idStr.Trim(), out int id))
                    {
                        var jogador = jogadores.FirstOrDefault(j => j.Id == id);
                        if (jogador != null && !usados.Contains(id))
                        {
                            timeA.Add(jogador.Nome);
                            usados.Add(id);
                        }
                    }
                }

                // Adicionando jogadores ao Time B
                Console.WriteLine("\nSelecione jogadores para o Time B (IDs separados por vírgula):");
                var entradaB = Console.ReadLine()?.Split(',') ?? Array.Empty<string>();

                foreach (var idStr in entradaB)
                {
                    if (int.TryParse(idStr.Trim(), out int id))
                    {
                        if (usados.Contains(id))
                        {
                            Console.WriteLine($"❌ Jogador ID {id} já está no Time A. Ignorado.");
                            continue;
                        }

                        var jogador = jogadores.FirstOrDefault(j => j.Id == id);
                        if (jogador != null)
                        {
                            timeB.Add(jogador.Nome);
                            usados.Add(id);
                        }
                    }
                }

                partida.TimeA = timeA;
                partida.TimeB = timeB;

                Console.Write("Vencedor (A / B / Empate): ");
                partida.Vencedor = Console.ReadLine() ?? "Empate";

                Console.Write("Número da rodada: ");
                partida.NumeroDaRodada = int.Parse(Console.ReadLine() ?? "1");

                Console.WriteLine("Modo da partida (1 = Quem Ganha Fica, 2 = Dois Jogos Cada): ");
                partida.Modo = (ModoPartida)int.Parse(Console.ReadLine() ?? "1");

                repositorio.Adicionar(partida);
                Console.WriteLine("✅ Partida registrada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao adicionar partida: {ex.Message}");
            }
        }


        public void Listar(IPartidaRepositorio repositorio)
        {
            var partidas = repositorio.Listar();
            if (!partidas.Any())
            {
                Console.WriteLine("Nenhuma partida registrada.");
                return;
            }

            foreach (var p in partidas)
            {
                Console.WriteLine($"\nID: {p.Id}\nJogo: {p.JogoId}\nRodada: {p.NumeroDaRodada}\nModo: {p.Modo}\nVencedor: {p.Vencedor}");
                Console.WriteLine("Time A: " + string.Join(", ", p.TimeA));
                Console.WriteLine("Time B: " + string.Join(", ", p.TimeB));
                Console.WriteLine("Data/Hora: " + p.DataHora);
            }
        }
        
    }
}
