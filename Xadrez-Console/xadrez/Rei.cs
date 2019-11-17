using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        public Rei(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro)
        {   
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = tabuleiro.peca(pos);
            return p == null || p.cor != cor;
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
            return mat;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
