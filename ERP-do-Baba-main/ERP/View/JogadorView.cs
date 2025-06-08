using Interfaces;
using Models;
using Models.Enums;

namespace View
{
    public class JogadorView
    {
        public string ExibirMenu()
        {
            Console.WriteLine("\n=== GESTÃO DE JOGADORES ===");
            Console.WriteLine("1 - Adicionar Jogador");
            Console.WriteLine("2 - Listar Jogadores");
            Console.WriteLine("3 - Atualizar Jogador");
            Console.WriteLine("4 - Remover Jogador");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha uma opção: ");
            return Console.ReadLine() ?? "0";
        }

        public void Adicionar(IJogadorRepositorio repositorio)
        {
            try
            {
                Console.Write("Nome: ");
                var nome = Console.ReadLine();
                Console.Write("Idade: ");
                var idade = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Posição (0 = Goleiro, 1 = Defesa, 2 = Ataque): ");
                var posicao = (Posicao)int.Parse(Console.ReadLine() ?? "0");

                var jogador = new Jogador
                {
                    Nome = nome ?? "",
                    Idade = idade,
                    Posicao = posicao
                };
                jogador.Validar();

                repositorio.Adicionar(jogador);
                Console.WriteLine("✅ Jogador adicionado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao adicionar jogador: {ex.Message}");
            }
        }

        public void Listar(IJogadorRepositorio repositorio)
        {
            var jogadores = repositorio.Listar();
            if (!jogadores.Any())
            {
                Console.WriteLine("Nenhum jogador cadastrado.");
                return;
            }

            Console.WriteLine("\n=== JOGADORES CADASTRADOS ===");
            foreach (var jogador in jogadores)
            {
                Console.WriteLine($"ID: {jogador.Id} | Nome: {jogador.Nome} | Idade: {jogador.Idade} | Posição: {jogador.Posicao}");
            }
        }

        public void Atualizar(IJogadorRepositorio repositorio)
        {
            try
            {
                Console.Write("ID do jogador a atualizar: ");
                int id = int.Parse(Console.ReadLine() ?? "0");
                var jogador = repositorio.BuscarPorId(id);

                if (jogador == null)
                {
                    Console.WriteLine("❌ Jogador não encontrado.");
                    return;
                }

                Console.Write("Novo nome (Enter para manter): ");
                var nome = Console.ReadLine();
                Console.Write("Nova idade (Enter para manter): ");
                var idadeStr = Console.ReadLine();
                Console.Write("Nova posição (0=Goleiro, 1=Defesa, 2=Ataque) (Enter para manter): ");
                var posicaoStr = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(nome)) jogador.Nome = nome;
                if (int.TryParse(idadeStr, out int novaIdade)) jogador.Idade = novaIdade;
                if (int.TryParse(posicaoStr, out int novaPosicao)) jogador.Posicao = (Posicao)novaPosicao;

                jogador.Validar();
                repositorio.Atualizar(jogador);
                Console.WriteLine("✅ Jogador atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao atualizar jogador: {ex.Message}");
            }
        }

        public void Remover(IJogadorRepositorio repositorio)
        {
            try
            {
                
                Console.Write("ID do jogador a remover: ");
                int id = int.Parse(Console.ReadLine() ?? "0");
                repositorio.Remover(id);
                Console.WriteLine("✅ Jogador removido com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao remover jogador: {ex.Message}");
            }
        }
    }
}
