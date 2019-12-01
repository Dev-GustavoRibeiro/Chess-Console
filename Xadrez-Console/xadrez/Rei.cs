using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez partida;

        public Rei(Cor cor, Tabuleiro tabuleiro, PartidaDeXadrez partida) : base(cor, tabuleiro)
        {
            this.partida = partida;
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = tabuleiro.peca(pos);
            return p == null || p.cor != cor;
        }

        private bool testeTorreParaRoque(Posicao pos)
        {
            Peca p = tabuleiro.peca(pos);
            return p != null && p is Torre && p.cor == cor && qteMovimentos == 0;
        }

        public override bool[,] movimentosPossiveis(Posicao origem)
        {
            bool[,] mat = new bool[tabuleiro.linhas, tabuleiro.colunas];

            Posicao pos = new Posicao(origem.Linha, origem.Coluna);

            // acima
            pos.definirValores(pos.Linha - 1, pos.Coluna);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos = new Posicao(origem.Linha, origem.Coluna);
            // ne
            pos.definirValores(pos.Linha - 1, pos.Coluna + 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos = new Posicao(origem.Linha, origem.Coluna);
            // direita
            pos.definirValores(pos.Linha, pos.Coluna + 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos = new Posicao(origem.Linha, origem.Coluna);
            // se
            pos.definirValores(pos.Linha + 1, pos.Coluna + 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos = new Posicao(origem.Linha, origem.Coluna);
            // abaixo
            pos.definirValores(pos.Linha + 1, pos.Coluna);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos = new Posicao(origem.Linha, origem.Coluna);
            // so
            pos.definirValores(pos.Linha +1, pos.Coluna - 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos = new Posicao(origem.Linha, origem.Coluna);
            // esquerda
            pos.definirValores(pos.Linha, pos.Coluna - 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos = new Posicao(origem.Linha, origem.Coluna);
            // no
            pos.definirValores(pos.Linha - 1, pos.Coluna -1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // #JogadaEspecial roque
            if (qteMovimentos == 0)
            {
                pos = new Posicao(origem.Linha, origem.Coluna);
                // #Roque pequeno
                Posicao posT1 = new Posicao(pos.Linha, pos.Coluna + 3);
                if (testeTorreParaRoque(posT1))
                {
                    Posicao p1 = new Posicao(pos.Linha, pos.Coluna + 1);
                    Posicao p2 = new Posicao(pos.Linha, pos.Coluna + 2);
                    if (tabuleiro.peca(p1) == null && tabuleiro.peca(p2) == null)
                    {
                        mat[pos.Linha, pos.Coluna + 2] = true;
                    }
                }

                pos = new Posicao(origem.Linha, origem.Coluna);
                // #Roque grande
                Posicao posT2 = new Posicao(pos.Linha, pos.Coluna - 4);
                if (testeTorreParaRoque(posT2))
                {
                    Posicao p1 = new Posicao(pos.Linha, pos.Coluna - 1);
                    Posicao p2 = new Posicao(pos.Linha, pos.Coluna - 2);
                    Posicao p3 = new Posicao(pos.Linha, pos.Coluna - 3);
                    if (tabuleiro.peca(p1) == null && tabuleiro.peca(p2) == null && tabuleiro.peca(p3) == null)
                    {
                        mat[pos.Linha, pos.Coluna - 2] = true;
                    }
                }
            }
            return mat;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
