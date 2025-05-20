namespace interfaces
{
    public interface IJogador
    {
        int Id { get; set; }
        string Nome { get; set; }
        int Idade { get; set; }
        string Posicao { get; set; }
    }
}
