using Interfaces;
using View;

namespace Controller
{
    public class JogoController
    {
        private readonly IJogoRepositorio _repositorio;
        private readonly JogoView _view;

        public JogoController(IJogoRepositorio repositorio, JogoView view)
        {
            _repositorio = repositorio;
            _view = view;
        }

        public void Menu()
        {
            while (true)
            {
                var opcao = _view.ExibirMenu();
                switch (opcao)
                {
                    case "1": _view.Adicionar(_repositorio); break;
                    case "2": _view.Listar(_repositorio); break;
                    case "3": _view.RegistrarInteressado(_repositorio); break;
                    case "0": return;
                    default: Console.WriteLine("Opção inválida."); break;
                }
            }
        }
    }
}
