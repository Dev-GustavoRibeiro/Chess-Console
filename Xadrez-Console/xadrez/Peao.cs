﻿using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {
        public Peao(Cor cor, Tabuleiro tab) : base(cor, tab)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        private bool existeInimigo(Posicao pos)
        {
            Peca p = tabuleiro.peca(pos);
            return p != null && p.cor != cor;
        }

        private bool livre(Posicao pos)
        {
            return tabuleiro.peca(pos) == null;
        }

        public override bool[,] movimentosPossiveis(Posicao origem)
        {
            bool[,] mat = new bool[tabuleiro.linhas, tabuleiro.colunas];

            Posicao pos = new Posicao(origem.Linha, origem.Coluna);

            if (cor == Cor.Branco)
            {
                pos.definirValores(pos.Linha - 1, pos.Coluna);
                if (tabuleiro.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos = new Posicao(origem.Linha, origem.Coluna);
                pos.definirValores(pos.Linha - 2, pos.Coluna);
                if (tabuleiro.posicaoValida(pos) && livre(pos) && qteMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos = new Posicao(origem.Linha, origem.Coluna);
                pos.definirValores(pos.Linha - 1, pos.Coluna -1);
                if (tabuleiro.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos = new Posicao(origem.Linha, origem.Coluna);
                pos.definirValores(pos.Linha - 1, pos.Coluna + 1);
                if (tabuleiro.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
            }
            else
            {
                pos.definirValores(pos.Linha + 1, pos.Coluna);
                if (tabuleiro.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos = new Posicao(origem.Linha, origem.Coluna);
                pos.definirValores(pos.Linha + 2, pos.Coluna);
                if (tabuleiro.posicaoValida(pos) && livre(pos) && qteMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos = new Posicao(origem.Linha, origem.Coluna);
                pos.definirValores(pos.Linha + 1, pos.Coluna - 1);
                if (tabuleiro.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos = new Posicao(origem.Linha, origem.Coluna);
                pos.definirValores(pos.Linha + 1, pos.Coluna + 1);
                if (tabuleiro.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
            }
            return mat;
        }
    }
}
