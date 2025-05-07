// Jogador.cs
using System;
using interfaces;

namespace BABA_DO_RAPA.Modelos
{
    public class Jogador : IJogador
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }

        private string posicao;
        public string Posicao
        {
            get { return posicao; }
            set
            {
                if (value == "Goleiro" || value == "Defesa" || value == "Atacante")
                    posicao = value;
                else
                    throw new Exception("Posição inválida. Use 'Goleiro', 'Defesa' ou 'Atacante'.");
            }
        }
    }
} 

