using Interfaces;
using View;
using Repositories; // necessário para usar JogadorRepositorioJson
using Utils;        // necessário para usar SincronizadorDeIds
using Models;

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
                    case "4": SincronizarInteressadosComIds(); break; // ✅ Nova opção
                    case "0": return;
                    default: Console.WriteLine("Opção inválida."); break;
                }
            }
        }

        private void SincronizarInteressadosComIds()
        {
            var jogadorRepo = new JogadorRepositorioJson();
            var jogadores = jogadorRepo.Listar();

            var sincronizador = new SincronizadorDeIds(jogadores);
            sincronizador.Sincronizar();
        }
    }
}
