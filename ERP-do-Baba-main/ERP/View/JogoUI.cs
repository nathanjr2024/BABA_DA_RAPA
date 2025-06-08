using Interfaces;
using Models;

namespace View
{
    public class JogoView
    {
        public string ExibirMenu()
        {
            Console.WriteLine("\n=== GESTÃO DE JOGOS ===");
            Console.WriteLine("1 - Adicionar Jogo");
            Console.WriteLine("2 - Listar Jogos");
            Console.WriteLine("3 - Registrar Interessado");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha uma opção: ");
            return Console.ReadLine() ?? "0";
        }

        public void Adicionar(IJogoRepositorio repositorio)
        {
            try
            {
                var jogo = new Jogo();

                Console.Write("Data do Jogo (yyyy-MM-dd): ");
                var inputData = Console.ReadLine();
                if (!DateTime.TryParse(inputData, out var data))
                {
                    Console.WriteLine("Data inválida.");
                    return;
                }
                jogo.Data = data;

                Console.Write("Local: ");
                var local = Console.ReadLine();
                jogo.Local = local ?? "";

                Console.Write("Tipo de campo: ");
                var tipoCampo = Console.ReadLine();
                jogo.TipoCampo = tipoCampo ?? "";

                Console.Write("Jogadores por time: ");
                var jogadoresStr = Console.ReadLine();
                if (!int.TryParse(jogadoresStr, out int jogadores))
                {
                    Console.WriteLine("Número de jogadores inválido.");
                    return;
                }
                jogo.JogadoresPorTime = jogadores;

                Console.Write("Limite de times (opcional): ");
                var limite = Console.ReadLine();
                if (int.TryParse(limite, out int limiteInt))
                {
                    jogo.LimiteDeTimes = limiteInt;
                }

                repositorio.Adicionar(jogo);
                Console.WriteLine("✅ Jogo adicionado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao adicionar jogo: {ex.Message}");
            }
        }

        public void Listar(IJogoRepositorio repositorio)
        {
            var jogos = repositorio.Listar();
            if (!jogos.Any())
            {
                Console.WriteLine("Nenhum jogo cadastrado.");
                return;
            }

            Console.WriteLine("\n=== JOGOS CADASTRADOS ===");
            foreach (var j in jogos)
            {
                Console.WriteLine($"\nID: {j.Id}\nData: {j.Data:dd/MM/yyyy}\nLocal: {j.Local}\nTipo: {j.TipoCampo}\nJogadores/time: {j.JogadoresPorTime}\nInteressados: {j.Interessados.Count}\nConfirmado: {j.Confirmado}");
                if (j.Interessados.Any())
                {
                    Console.WriteLine("Interessados:");
                    foreach (var nome in j.Interessados)
                    {
                        Console.WriteLine($"- {nome}");
                    }
                }
            }
        }

        public void RegistrarInteressado(IJogoRepositorio repositorio)
        {
            Console.Write("ID do jogo: ");
            var id = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var jogo = repositorio.BuscarPorId(id);
            
            if (jogo == null)
            {
                Console.WriteLine("Jogo não encontrado.");
                return;
            }

            Console.Write("Nome do interessado: ");
            var nome = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("Nome não pode ser vazio.");
                return;
            }
            if (jogo.Interessados.Contains(nome, StringComparer.OrdinalIgnoreCase))
            {
                Console.WriteLine("Este nome já está registrado como interessado.");
                return;
            }

            jogo.Interessados.Add(nome);
            repositorio.Atualizar(jogo);
            Console.WriteLine("✅ Interessado adicionado com sucesso!");
        }
    }
}
