using Models;

namespace Interfaces
{
    public interface IJogadorRepositorio
    {
        void Adicionar(Jogador jogador);
        Jogador? BuscarPorId(int id);
        List<Jogador> Listar();
        void Atualizar(Jogador jogador);
        void Remover(int id);

    }
}

