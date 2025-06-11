using Models;

namespace Interfaces
{
    public interface IJogoRepositorio
    {
        void Adicionar(Jogo jogo);
        void Atualizar(Jogo jogo);
        List<Jogo> Listar();
        Jogo? BuscarPorId(string id);
        void Salvar();
    }
}