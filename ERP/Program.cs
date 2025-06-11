using Controller;
using Interfaces;
using Repositories;
using View;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        // Instância dos repositórios
        IJogadorRepositorio jogadorRepositorio = new JogadorRepositorioJson();
        IJogoRepositorio jogoRepositorio = new JogoRepositorioJson();
        IPartidaRepositorio partidaRepositorio = new PartidaRepositorioJson();

        // Instância das Views
        var jogadorView = new JogadorView();
        var jogoView = new JogoView();
        var partidaView = new PartidaView();
        var geracaoDeTimesView = new GeracaoDeTimesView();

        // Instância dos Controllers
        var jogadorController = new JogadorController(jogadorRepositorio, jogadorView);
        var jogoController = new JogoController(jogoRepositorio, jogoView);
        var partidaController = new PartidaController(partidaRepositorio, jogoRepositorio, jogadorRepositorio, partidaView); // ✅ Correto
        var geracaoDeTimesController = new GeracaoDeTimesController(
            jogoRepositorio,
            jogadorRepositorio,
            geracaoDeTimesView
        );
        // Loop do menu principal
        while (true)
        {
            Console.WriteLine("\n=== MENU PRINCIPAL ===");
            Console.WriteLine("1 - Gestão de Jogadores");
            Console.WriteLine("2 - Gestão de Jogos");
            Console.WriteLine("3 - Gestão de Partidas");
            Console.WriteLine("4 - Gestão de Times");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    jogadorController.Menu();
                    break;
                case "2":
                    jogoController.Menu();
                    break;
                case "3":
                    partidaController.Menu();
                    break;
                case "4":
                    geracaoDeTimesController.Menu();
                    break;
                case "0":
                    Console.WriteLine("Encerrando aplicação...");
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }
}

