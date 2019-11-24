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
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branco;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public void executarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
        }

        public Cor adversario()
        {
            if (jogadorAtual == Cor.Amarelo)
            {
                return Cor.Branco;
            }
            else
            {
                return Cor.Amarelo;
            }
        }

        public void xequemate()
        {
            foreach (Peca x in capturadas)
            {
                if (x is Rei)
                {
                    terminada = true;
                }
            }
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
            if (!tab.peca(origem).movimentoPossivel(destino, origem))
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

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }
        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
            colocarNovaPeca('c', 1, new Torre(Cor.Branco, tab));
            colocarNovaPeca('c', 2, new Torre(Cor.Branco, tab));
            colocarNovaPeca('d', 2, new Torre(Cor.Branco, tab));
            colocarNovaPeca('e', 2, new Torre(Cor.Branco, tab));
            colocarNovaPeca('e', 1, new Torre(Cor.Branco, tab));
            colocarNovaPeca('d', 1, new Rei(Cor.Branco, tab));

            colocarNovaPeca('c', 7, new Torre(Cor.Amarelo, tab));
            colocarNovaPeca('c', 8, new Torre(Cor.Amarelo, tab));
            colocarNovaPeca('d', 7, new Torre(Cor.Amarelo, tab));
            colocarNovaPeca('e', 7, new Torre(Cor.Amarelo, tab));
            colocarNovaPeca('e', 8, new Torre(Cor.Amarelo, tab));
            colocarNovaPeca('d', 8, new Rei(Cor.Amarelo, tab));
        }
    }
}
