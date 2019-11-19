using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branco;
            terminada = false;
            colocarPecas();
        }

        public void executarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.ColocarPeca(p, destino);
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            executarMovimento(origem, destino);
            turno++;
            mudaJogador();
        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição escolhida!");
            }
            if (jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("Essa peça não é sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis(pos))
            {
                throw new TabuleiroException("Não existe movimentos possíveis para essa peça!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).podeMoverPara(destino, origem))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void mudaJogador()
        {
            if (jogadorAtual == Cor.Branco)
            {
                jogadorAtual = Cor.Amarelo;
            }
            else
            {
                jogadorAtual = Cor.Branco;
            }
        }

        private void colocarPecas()
        {
            tab.ColocarPeca(new Torre(Cor.Branco, tab), new PosicaoXadrez('c', 1).toPosicao());
            tab.ColocarPeca(new Torre(Cor.Branco, tab), new PosicaoXadrez('c', 2).toPosicao());
            tab.ColocarPeca(new Torre(Cor.Branco, tab), new PosicaoXadrez('d', 2).toPosicao());
            tab.ColocarPeca(new Torre(Cor.Branco, tab), new PosicaoXadrez('e', 1).toPosicao());
            tab.ColocarPeca(new Torre(Cor.Branco, tab), new PosicaoXadrez('e', 2).toPosicao());
            tab.ColocarPeca(new Rei(Cor.Branco, tab), new PosicaoXadrez('d', 1).toPosicao());

            tab.ColocarPeca(new Torre(Cor.Amarelo, tab), new PosicaoXadrez('c', 7).toPosicao());
            tab.ColocarPeca(new Torre(Cor.Amarelo, tab), new PosicaoXadrez('c', 8).toPosicao());
            tab.ColocarPeca(new Torre(Cor.Amarelo, tab), new PosicaoXadrez('d', 7).toPosicao());
            tab.ColocarPeca(new Torre(Cor.Amarelo, tab), new PosicaoXadrez('e', 7).toPosicao());
            tab.ColocarPeca(new Torre(Cor.Amarelo, tab), new PosicaoXadrez('e', 8).toPosicao());
            tab.ColocarPeca(new Rei(Cor.Amarelo, tab), new PosicaoXadrez('d', 8).toPosicao());
        }
    }
}
