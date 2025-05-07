using System;
using System.Collections.Generic;
using System.Linq;
using Models;  

namespace Repository  
{
    public class JogosRepository
    {
        private List<Jogos> _jogos = new List<Jogos>();

        public void Adicionar(Jogos jogo)
        {
            _jogos.Add(jogo);
        }

        public List<Jogos> ListarTodos()
        {
            return _jogos;
        }

        public Jogos BuscarPorData(DateTime data)
        {
            return _jogos.FirstOrDefault(j => j.DataDoJogo.Date == data.Date);
        }

        public void Editar(Jogos jogo)
        {
            var existente = BuscarPorData(jogo.DataDoJogo);
            if (existente != null)
            {
                existente.Local = jogo.Local;
                existente.TipoDeCampo = jogo.TipoDeCampo;
                existente.QuantidadeDeJogadores = jogo.QuantidadeDeJogadores;
            }
        }

        public void Remover(DateTime data)
        {
            var jogo = BuscarPorData(data);
            if (jogo != null)
            {
                _jogos.Remove(jogo);
            }
        }
    }
}
