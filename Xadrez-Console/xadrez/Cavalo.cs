using tabuleiro;

namespace xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Cor cor, Tabuleiro tab) : base(cor, tab)
        {
        }

        public override string ToString()
        {
            return "C";
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

            pos.definirValores(pos.Linha - 1, pos.Coluna -2);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos = new Posicao(origem.Linha, origem.Coluna);
            pos.definirValores(pos.Linha - 2, pos.Coluna - 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos = new Posicao(origem.Linha, origem.Coluna);
            pos.definirValores(pos.Linha - 2, pos.Coluna + 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos = new Posicao(origem.Linha, origem.Coluna);
            pos.definirValores(pos.Linha - 1, pos.Coluna + 2);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos = new Posicao(origem.Linha, origem.Coluna);
            pos.definirValores(pos.Linha + 1, pos.Coluna + 2);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos = new Posicao(origem.Linha, origem.Coluna);
            pos.definirValores(pos.Linha + 2, pos.Coluna + 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos = new Posicao(origem.Linha, origem.Coluna);
            pos.definirValores(pos.Linha +2, pos.Coluna - 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos = new Posicao(origem.Linha, origem.Coluna);
            pos.definirValores(pos.Linha + 1, pos.Coluna - 2);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            return mat;
        }
    }
}
