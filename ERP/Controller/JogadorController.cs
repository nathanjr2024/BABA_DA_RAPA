using Interfaces;
using View;

namespace Controller
{
    public class JogadorController
    {
        private readonly IJogadorRepositorio _repositorio;
        private readonly JogadorView _view;

        public JogadorController(IJogadorRepositorio repositorio, JogadorView view)
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
                    case "3": _view.Atualizar(_repositorio); break;
                    case "4": _view.Remover(_repositorio); break;
                
                    case "0": return;
                    default: Console.WriteLine("Opção inválida."); break;
                }
            }
        }
    }
}
