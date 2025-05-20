using System;
using BABA_DO_RAPA.Modelos;
namespace Controllers
{
    public class MenuController
    {
        private readonly JogadorController _jogadorController = new JogadorController();
        private readonly JogosController _jogosController = new JogosController();

        public void ExibirMenu()
        {
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
                Console.WriteLine("9. Ver ranking de vitorias");
                Console.WriteLine("10 Sair");
                Console.Write("Escolha: ");
                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": _jogadorController.CadastrarJogador(); break;
                    case "2": _jogadorController.ListarJogadores(); break;
                    case "3": _jogadorController.EditarJogador(); break;
                    case "4": _jogadorController.RemoverJogador(); break;
                    case "5": _jogosController.CadastrarPartida(); break;
                    case "6": _jogosController.ListarPartidas(); break;
                    case "7": _jogosController.EditarPartida(); break;
                    case "8": _jogosController.RemoverPartida(); break;
                    case "9": _jogosController.ExibirRanking(); break;
                    case "10": return;
                    default: Console.WriteLine("Opção inválida."); break;
                }
            }
        }
    }
}