﻿using extra;
using tabuleiro;

namespace jogo
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public Int32 Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public Boolean Terminada { get; private set; }
        public List<Peca> PecasEmJogo { get; private set; }
        public List<Peca> PecasCapturadas { get; private set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;

            PecasEmJogo = [];
            foreach (Peca item in Tabuleiro.PecasNoTabuleiro)
                PecasEmJogo.Add(item);

            PecasCapturadas = [];
        }

        private void AlternarJogadorAtual()
        {
            if (JogadorAtual == Cor.Branco)
                JogadorAtual = Cor.Preto;
            else
                JogadorAtual = Cor.Branco;
        }

        public List<Peca> CapturadasBrancas()
        {
            List<Peca> pecas = [];

            foreach (Peca item in PecasCapturadas)
                if (item.Cor == Cor.Branco)
                    pecas.Add(item);

            return pecas;
        }

        public List<Peca> CapturadasPretas()
        {
            List<Peca> pecas = [];

            foreach (Peca item in PecasCapturadas)
                if (item.Cor == Cor.Preto)
                    pecas.Add(item);

            return pecas;
        }

        /* 
         * Verifica se tem peça na origem, se essa peça é da cor do jogador atual, 
         * e se pode mover essa peça para o destino.
         */
        private Boolean PodeMoverPeca(PosicaoXadrez? origem, PosicaoXadrez? destino)
        {
            Boolean[,] movimentosPossiveis;
            Peca? peca;
            PosicaoMatriz destinoMatriz;

            peca = Tabuleiro.RetornarAPecaEmJogo(origem);

            if (peca != null && destino != null && peca.Cor == JogadorAtual)
            {
                movimentosPossiveis = peca.MovimentosPossiveis();
                destinoMatriz = destino.ToPosicaoMatriz();

                if (movimentosPossiveis[destinoMatriz.Linha, destinoMatriz.Coluna])
                    return true;
            }

            return false;
        }

        /* 
         * Mecânica da movimentação de uma peça:
         *  1-> retirar de sua origem;
         *  2-> capturar a peça que está no destino, se houver e se for inimiga:
         *      2.1-> atualizar a sua posição (null);
         *      2.2-> retirá-la das peças em jogo;
         *      2.3-> colocá-la nas peças capturadas.
         *  3-> colocar no seu destino;
         *  4-> atualizar a sua posição;
         *  5-> incrementar o seu movimento;
         *  6-> incrementar o turno;
         *  7-> alternar o jogador atual.
         */
        public void MoverPeca(PosicaoXadrez? origem, PosicaoXadrez? destino)
        {
            Boolean podeMover;
            Peca? peca, pecaCapturada, torre;
            PosicaoMatriz origemReiMatriz, destinoReiMatriz;

            peca = Tabuleiro.RetornarAPecaEmJogo(origem);
            podeMover = PodeMoverPeca(origem, destino);

            if (
                origem != null &&
                destino != null &&
                peca != null &&
                peca.PosicaoXadrez != null
            )
            {
                if (podeMover)
                {
                    Tabuleiro.RetirarPeca(origem);
                    pecaCapturada = Tabuleiro.RetirarPeca(destino);

                    if (pecaCapturada != null && pecaCapturada.Cor != peca.Cor)
                    {
                        pecaCapturada.SetPosicaoXadrez(Tabuleiro, null);
                        PecasEmJogo.Remove(pecaCapturada);
                        PecasCapturadas.Add(pecaCapturada);
                    }

                    Tabuleiro.ColocarPeca(peca, destino.Coluna, destino.Linha);
                    peca.SetPosicaoXadrez(Tabuleiro, destino);

                    if (peca is Rei)
                    {
                        origemReiMatriz = origem.ToPosicaoMatriz();
                        destinoReiMatriz = destino.ToPosicaoMatriz();

                        // #jogadaEspecial: Roque Pequeno
                        if (destinoReiMatriz.Coluna == origemReiMatriz.Coluna + 2)
                        {
                            if (peca.Cor == Cor.Branco)
                            {
                                torre = Tabuleiro.RetornarAPecaEmJogo(
                                    new PosicaoXadrez('h', 1)
                                );

                                if (torre != null && torre.PosicaoXadrez != null)
                                {
                                    Tabuleiro.RetirarPeca(torre.PosicaoXadrez);
                                    Tabuleiro.ColocarPeca(torre, 'f', 1);
                                    torre.SetPosicaoXadrez(
                                        Tabuleiro, new PosicaoXadrez('f', 1)
                                    );
                                }
                            }
                            else
                            {
                                torre = Tabuleiro.RetornarAPecaEmJogo(
                                    new PosicaoXadrez('h', 8)
                                );

                                if (torre != null && torre.PosicaoXadrez != null)
                                {
                                    Tabuleiro.RetirarPeca(torre.PosicaoXadrez);
                                    Tabuleiro.ColocarPeca(torre, 'f', 8);
                                    torre.SetPosicaoXadrez(
                                        Tabuleiro, new PosicaoXadrez('f', 8)
                                    );
                                }
                            }
                        }

                        // #jogadaEspecial: Roque Grande
                        if (destinoReiMatriz.Coluna == origemReiMatriz.Coluna - 2)
                        {
                            if (peca.Cor == Cor.Branco)
                            {
                                torre = Tabuleiro.RetornarAPecaEmJogo(
                                    new PosicaoXadrez('a', 1)
                                );

                                if (torre != null && torre.PosicaoXadrez != null)
                                {
                                    Tabuleiro.RetirarPeca(torre.PosicaoXadrez);
                                    Tabuleiro.ColocarPeca(torre, 'd', 1);
                                    torre.SetPosicaoXadrez(
                                        Tabuleiro, new PosicaoXadrez('d', 1)
                                    );
                                }
                            }
                            else
                            {
                                torre = Tabuleiro.RetornarAPecaEmJogo(
                                    new PosicaoXadrez('a', 8)
                                );

                                if (torre != null && torre.PosicaoXadrez != null)
                                {
                                    Tabuleiro.RetirarPeca(torre.PosicaoXadrez);
                                    Tabuleiro.ColocarPeca(torre, 'd', 8);
                                    torre.SetPosicaoXadrez(
                                        Tabuleiro, new PosicaoXadrez('d', 8)
                                    );
                                }
                            }
                        }
                    }

                    peca.IncrementarMovimento(Tabuleiro);
                    Turno++;
                    AlternarJogadorAtual();
                }
                else
                    throw new TabuleiroException("Posição de destino não permitida! ");
            }
        }

        /* 
         * Mecânica da reversão de movimento de uma peça:
         *  1-> retirar de seu destino;
         *  2-> devolver a peça capturada no destino, se houve:
         *      2.1-> atualizar a sua posição (destino);
         *      2.2-> retirá-la das peças capturadas;
         *      2.3-> colocá-la nas peças em jogo.
         *  3-> colocar na sua origem;
         *  4-> atualizar a sua posição;
         *  5-> decrementar o seu movimento;
         *  6-> decrementar o turno;
         *  7-> voltar ao jogador original.
         */
        private void DesfazerMovimento(
            PosicaoXadrez? origem, PosicaoXadrez? destino, Peca? pecaCapturada
        )
        {
            Peca? peca;

            peca = Tabuleiro.RetornarAPecaEmJogo(destino);

            if (origem != null && peca != null && peca.PosicaoXadrez != null)
            {
                Tabuleiro.RetirarPeca(destino);

                if (pecaCapturada != null)
                {
                    pecaCapturada.SetPosicaoXadrez(Tabuleiro, destino);
                    PecasCapturadas.Remove(pecaCapturada);
                    PecasEmJogo.Add(pecaCapturada);
                }

                Tabuleiro.ColocarPeca(peca, origem.Coluna, origem.Linha);
                peca.SetPosicaoXadrez(Tabuleiro, origem);
                peca.DecrementarMovimento(Tabuleiro);
                Turno--;
                AlternarJogadorAtual();
            }
        }
    }
}
