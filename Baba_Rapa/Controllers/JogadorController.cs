using Repository;
using Models;
using System;

namespace BABA_DO_RAPA.Modelos
{
    public class JogadorController
    {
        private readonly RepositorioJogadores _repositorio = new RepositorioJogadores();

        public void CadastrarJogador()
{
    try
    {
        var jogador = new Jogador();
        Console.Write("Nome: ");
        jogador.Nome = Console.ReadLine();
        Console.Write("Idade: ");
        if (!int.TryParse(Console.ReadLine(), out int idade))
        {
            Console.WriteLine("Idade inválida.");
            return;
        }
        jogador.Idade = idade;
        Console.Write("Posição (Goleiro/Defesa/Atacante): ");
        jogador.Posicao = Console.ReadLine();
        _repositorio.Adicionar(jogador);
        Console.WriteLine("Jogador adicionado.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }
}
        public void ListarJogadores()
        {
            var lista = _repositorio.ListarTodos();
            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum jogador cadastrado.");
                return;
            }
            foreach (var j in lista)
                Console.WriteLine($"ID: {j.Id}, Nome: {j.Nome}, Idade: {j.Idade}, Posição: {j.Posicao}");
        }

        public void EditarJogador()
        {
            Console.Write("Digite o ID do jogador para editar: ");
            var idEditar = Console.ReadLine();
            var jogadorEdit = _repositorio.BuscarPorId(idEditar);
            if (jogadorEdit != null)
            {
                Console.Write("Novo Nome: ");
                jogadorEdit.Nome = Console.ReadLine();
                Console.Write("Nova Idade: ");
                if (!int.TryParse(Console.ReadLine(), out int novaIdade))
                {
                    Console.WriteLine("Idade inválida.");
                    return;
                }
                jogadorEdit.Idade = novaIdade;
                Console.Write("Nova Posição: ");
                jogadorEdit.Posicao = Console.ReadLine();
                _repositorio.Editar(jogadorEdit);
                Console.WriteLine("Jogador atualizado.");
            }
            else
            {
                Console.WriteLine("Jogador não encontrado.");
            }
        }

        public void RemoverJogador()
        {
            Console.Write("Digite o ID do jogador para remover: ");
            var idRemover = Console.ReadLine();
            _repositorio.Remover(idRemover);
            Console.WriteLine("Jogador removido.");
        }
    }
}