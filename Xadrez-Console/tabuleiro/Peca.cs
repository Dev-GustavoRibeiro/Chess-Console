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
    }
}
