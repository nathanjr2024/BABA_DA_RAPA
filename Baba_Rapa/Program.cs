﻿using Models;
using System;
using BABA_DO_RAPA.Modelos;
using Repository;

class Program
{
    static void Main()
    {
        var repositorio = new RepositorioJogadores();
        var repositorioPartidas = new JogosRepository();

        while (true)
        {
            Console.WriteLine("\n--- Menu ---");
            Console.WriteLine("1. Cadastrar jogador");
            Console.WriteLine("2. Listar jogadores");
            Console.WriteLine("3. Editar jogador");
            Console.WriteLine("4. Remover jogador");
            Console.WriteLine("5. Cadastrar uma partida");
            Console.WriteLine("6. Listar partidas");
            Console.WriteLine("7. Editar partida");
            Console.WriteLine("8. Remover partida");
            Console.WriteLine("9. Sair");
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

                case "5":
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
                        Console.Write("Tipo de campo (1, 2, 3 ou 4): ");
                        novaPartida.TipoDeCampo = int.Parse(Console.ReadLine());
                        Console.Write("Quantidade de jogadores (deve ser 10): ");
                        novaPartida.QuantidadeDeJogadores = int.Parse(Console.ReadLine());
                        repositorioPartidas.Adicionar(novaPartida);
                        Console.WriteLine("Partida cadastrada com sucesso.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao cadastrar partida: {ex.Message}");
                    }
                    break;

                case "6":
                    var partidas = repositorioPartidas.ListarTodos();
                    foreach (var p in partidas)
                    {
                        Console.WriteLine($"Data: {p.DataDoJogo:dd/MM/yyyy}, Local: {p.Local}, Tipo de campo: {p.TipoDeCampo}, Jogadores: {p.QuantidadeDeJogadores}");
                    }
                    break;

                case "7":
                    Console.Write("Digite a data da partida para editar (dd/mm/yyyy): ");
                    var dataEditarStr = Console.ReadLine();
                    if (DateTime.TryParse(dataEditarStr, out var dataEditar))
                    {
                        var partidaEdit = repositorioPartidas.BuscarPorData(dataEditar);
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
                                repositorioPartidas.Editar(partidaEdit);
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
                    break;

                case "8":
                    Console.Write("Digite a data da partida para remover (dd/mm/yyyy): ");
                    var dataRemoverStr = Console.ReadLine();
                    if (DateTime.TryParse(dataRemoverStr, out var dataRemover))
                    {
                        repositorioPartidas.Remover(dataRemover);
                        Console.WriteLine("Partida removida.");
                    }
                    else
                    {
                        Console.WriteLine("Data inválida.");
                    }
                    break;

                case "9":
                    return;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }
}
