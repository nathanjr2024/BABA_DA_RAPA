using Models;

namespace Interfaces
{
    public interface IPartidaRepositorio
    {
        void Adicionar(Partida partida);
        List<Partida> Listar();
        List<Partida> ListarPorJogo(string jogoId);
    }
}