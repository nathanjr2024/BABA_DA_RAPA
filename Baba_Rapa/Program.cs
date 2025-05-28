using Models;
using System;
using BABA_DO_RAPA.Modelos;

class Program
{
    static void Main()
    {
        var repositorio = new RepositorioJogadores();
        var repositorioPartidas = new RepositorioPartidas();

        while (true)
        {
            Console.WriteLine("\n--- Menu Jogadores ---");
            Console.WriteLine("1. Cadastrar jogador");
            Console.WriteLine("2. Listar jogadores");
            Console.WriteLine("3. Editar jogador");
            Console.WriteLine("4. Remover jogador");
            Console.WriteLine("6. Cadastrar uma partida");
            Console.WriteLine("5. Sair");
            Console.Write("Escolha: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    try
                    {
                        var jogador = new Jogador();

                        Console.Write("ID (RA): ");
                        jogador.Id = Console.ReadLine();

                        Console.Write("Nome: ");
                        jogador.Nome = Console.ReadLine();

                        Console.Write("Idade: ");
                        jogador.Idade = int.Parse(Console.ReadLine());

                        Console.Write("Posição (Goleiro/Defesa/Atacante): ");
                        jogador.Posicao = Console.ReadLine();

                        repositorio.Adicionar(jogador);
                        Console.WriteLine("Jogador adicionado.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro: {ex.Message}");
                    }
                    break;

                case "2":
                    var lista = repositorio.ListarTodos();
                    foreach (var j in lista)
                    {
                        Console.WriteLine($"ID: {j.Id}, Nome: {j.Nome}, Idade: {j.Idade}, Posição: {j.Posicao}");
                    }
                    break;

                case "3":
                    Console.Write("Digite o ID do jogador para editar: ");
                    var idEditar = Console.ReadLine();
                    var jogadorEdit = repositorio.BuscarPorId(idEditar);
                    if (jogadorEdit != null)
                    {
                        Console.Write("Novo Nome: ");
                        jogadorEdit.Nome = Console.ReadLine();

                        Console.Write("Nova Idade: ");
                        jogadorEdit.Idade = int.Parse(Console.ReadLine());

                        Console.Write("Nova Posição: ");
                        jogadorEdit.Posicao = Console.ReadLine();

                        repositorio.Editar(jogadorEdit);
                        Console.WriteLine("Jogador atualizado.");
                    }
                    else
                    {
                        Console.WriteLine("Jogador não encontrado.");
                    }
                    break;

                case "4":
                    Console.Write("Digite o ID do jogador para remover: ");
                    var idRemover = Console.ReadLine();
                    repositorio.Remover(idRemover);
                    Console.WriteLine("Jogador removido.");
                    break;

                case "6":

                try
                {
                    var novaPartida = new Jogos();

                    Console.Write("Data do jogo (dd/mm/yyyy): ");
                    novaPartida.DataDoJogo = DateTime.Parse(Console.ReadLine());

                    Console.WriteLine("Locais disponíveis:");
                    foreach (var local in Regras.LocaisPermitidos.Lista)
                        Console.WriteLine($"- {local}");

                    Console.Write("Local: ");
                    novaPartida.Local = Console.ReadLine();

                    Console.WriteLine("Tipo de campo (1, 2, 3 ou 4): ");
                    novaPartida.TipoDeCampo = int.Parse(Console.ReadLine());

                    Console.Write("A partida esta sendo iniciada com 10 jogadores? (SIM/NÃO) ");
                    novaPartida.QuantidadeDeJogadores = (Console.ReadLine());

                    repositorioPartidas.Adicionar(novaPartida);
                    Console.WriteLine("Partida cadastrada com sucesso.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao cadastrar partida: {ex.Message}");
                }
                break;
                case "7":
                    return;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }
}
