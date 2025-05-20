namespace interfaces
{
    public interface IGestaoJogos
    {
        DateTime DataDoJogo { get; set; }
        string Local { get; set; }
        int TipoDeCampo { get; set; }
        int QuantidadeDeJogadores { get; set; }
    }
}