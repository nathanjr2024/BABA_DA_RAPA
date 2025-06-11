using Interfaces;
using View;

namespace Controller
{
    public class PartidaController
    {
        private readonly IPartidaRepositorio _repositorio;
        private readonly IJogoRepositorio _jogoRepositorio;
        private readonly IJogadorRepositorio _jogadorRepositorio; // NOVO
        private readonly PartidaView _view;

        public PartidaController(
            IPartidaRepositorio repositorio,
            IJogoRepositorio jogoRepositorio,
            IJogadorRepositorio jogadorRepositorio, //  NOVO
            PartidaView view)
        {
            _repositorio = repositorio;
            _jogoRepositorio = jogoRepositorio;
            _jogadorRepositorio = jogadorRepositorio; // NOVO
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
                        _view.Adicionar(_repositorio, _jogoRepositorio, _jogadorRepositorio); // Passando o repositório de jogadores
                        break;
                    case "2":
                        _view.Listar(_repositorio);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }
    }
}
