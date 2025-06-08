using Models.Enums;

namespace Models
{
    public class Jogador
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public Posicao Posicao { get; set; }

    // Criado método para validar a criação de novo Jogador pelas regras onde Nome não pode estar vazio e Idade maior que 10 anos e menor que 70
    public void Validar()
    {
        if (string.IsNullOrWhiteSpace(Nome))
            throw new ArgumentException("O nome do jogador é obrigatório.");


        if (Idade < 10 || Idade > 70)
            throw new ArgumentException("A idade do jogador deve estar entre 10 e 70 anos.");
    }

}
}

