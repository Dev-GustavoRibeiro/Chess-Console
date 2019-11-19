namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int qteMovimentos { get; protected set; }
        public Tabuleiro tabuleiro { get; protected set; }

        public Peca(Cor cor, Tabuleiro tabuleiro)
        {
            this.posicao = null;
            this.cor = cor;
            this.qteMovimentos = 0;
            this.tabuleiro = tabuleiro;
        }

        public abstract bool[,] movimentosPossiveis(Posicao origem);

        public void incrementarQteMovimentos()
        {
            qteMovimentos++;
        }

        public bool existeMovimentosPossiveis(Posicao pos)
        {
            bool[,] mat = movimentosPossiveis(pos);
            for (int i = 0; i < tabuleiro.linhas; i++)
            {
                for (int j = 0; j < tabuleiro.colunas; j++)
                {
                    if (mat[i,j])
                    {
                        return true;
                    }
                }
            }
            return false;   
        }

        public bool podeMoverPara(Posicao pos, Posicao origem)
        {
            return movimentosPossiveis(origem)[pos.Linha, pos.Coluna];
        }
    }
}
