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
        public Peca vulneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branco;
            terminada = false;
            vulneravelEnPassant = null;
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

            // #JogadaEspecial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQteMovimentos();
                tab.ColocarPeca(T, destinoT);
            }

            // #JogadaEspecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQteMovimentos();
                tab.ColocarPeca(T, destinoT);
            }

            // #JogadaEspecial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.cor == Cor.Branco)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = tab.retirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
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

            Peca p = tab.peca(destino);

            // #JogadaEspecial en passant
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                vulneravelEnPassant = p;
            }
            else
            {
                vulneravelEnPassant = null;
            }
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
            colocarNovaPeca('a', 1, new Torre(Cor.Branco, tab));
            colocarNovaPeca('b', 1, new Cavalo(Cor.Branco, tab));
            colocarNovaPeca('c', 1, new Bispo(Cor.Branco, tab));
            colocarNovaPeca('d', 1, new Dama(Cor.Branco, tab));
            colocarNovaPeca('e', 1, new Rei(Cor.Branco, tab, this));
            colocarNovaPeca('f', 1, new Bispo(Cor.Branco, tab));
            colocarNovaPeca('g', 1, new Cavalo(Cor.Branco, tab));
            colocarNovaPeca('h', 1, new Torre(Cor.Branco, tab));
            colocarNovaPeca('a', 2, new Peao(Cor.Branco, tab, this));
            colocarNovaPeca('b', 2, new Peao(Cor.Branco, tab, this));
            colocarNovaPeca('c', 2, new Peao(Cor.Branco, tab, this));
            colocarNovaPeca('d', 2, new Peao(Cor.Branco, tab, this));
            colocarNovaPeca('e', 2, new Peao(Cor.Branco, tab, this));
            colocarNovaPeca('f', 2, new Peao(Cor.Branco, tab, this));
            colocarNovaPeca('g', 2, new Peao(Cor.Branco, tab, this));
            colocarNovaPeca('h', 2, new Peao(Cor.Branco, tab, this));

            colocarNovaPeca('a', 8, new Torre(Cor.Amarelo, tab));
            colocarNovaPeca('b', 8, new Cavalo(Cor.Amarelo, tab));
            colocarNovaPeca('c', 8, new Bispo(Cor.Amarelo, tab));
            colocarNovaPeca('d', 8, new Dama(Cor.Amarelo, tab));
            colocarNovaPeca('e', 8, new Rei(Cor.Amarelo, tab, this));   
            colocarNovaPeca('f', 8, new Bispo(Cor.Amarelo, tab));
            colocarNovaPeca('g', 8, new Cavalo(Cor.Amarelo, tab));
            colocarNovaPeca('h', 8, new Torre(Cor.Amarelo, tab));
            colocarNovaPeca('a', 7, new Peao(Cor.Amarelo, tab, this));
            colocarNovaPeca('b', 7, new Peao(Cor.Amarelo, tab, this));
            colocarNovaPeca('c', 7, new Peao(Cor.Amarelo, tab, this));
            colocarNovaPeca('d', 7, new Peao(Cor.Amarelo, tab, this));
            colocarNovaPeca('e', 7, new Peao(Cor.Amarelo, tab, this));
            colocarNovaPeca('f', 7, new Peao(Cor.Amarelo, tab, this));
            colocarNovaPeca('g', 7, new Peao(Cor.Amarelo, tab, this));
            colocarNovaPeca('h', 7, new Peao(Cor.Amarelo, tab, this));
        }
    }
}
