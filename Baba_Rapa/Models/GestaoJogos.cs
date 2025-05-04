namespace Models;
using Regras;

public class Jogos
{

    private DateTime _dataDoJogo;
    private string _local;
    private int _tipoDeCampo;
    private int _quantidadeDeJogadores;

    public int QuantidadeDeJogadores
    {
        get { return _quantidadeDeJogadores; }

        set
        {
            if (value != 10)
            {
                throw new Exception("A quantidade de jogadores não está de acordo com as normas. Por favor, comecem a partida com 10 jogadores.");
            }

            _quantidadeDeJogadores = value;
        }
    }

    public int TipoDeCampo
    {
        get { return _tipoDeCampo; }

        set
        {
            if (value != 1 && value != 2 && value != 3 && value != 4)
            {
                throw new Exception("Opção inválida. Escolha um valor entre 1 e 4.");
            }

            _tipoDeCampo = value;
        }
    }
    
    
    public DateTime DataDoJogo
    {
        get { return _dataDoJogo; }

        set
        {
            if (value < DateTime.Today)
            {
                throw new Exception("A data do jogo não pode ser marcada no passado!");
            }

            if (value > DateTime.Today.AddDays(90))
            {
                throw new Exception("A partida deve estar dentro dos próximos 90 dias!");
            }

            _dataDoJogo = value;
        }
    }

    public string Local
    {
        get { return _local; }

        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception("O campo de local é obrigatório.");
            }

            if (!LocaisPermitidos.Lista.Any(l => l.Equals(value, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("Local inválido. Escolha um dos locais permitidos.");
            }

            _local = value;
        }
    }
}
