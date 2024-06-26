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
        public Boolean Xeque { get; private set; }

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

            VerificarXeque();
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

        public List<Peca> EmJogoBrancas()
        {
            List<Peca> pecas = [];

            foreach (Peca item in PecasEmJogo)
                if (item.Cor == Cor.Branco)
                    pecas.Add(item);

            return pecas;
        }

        public List<Peca> EmJogoPretas()
        {
            List<Peca> pecas = [];

            foreach (Peca item in PecasEmJogo)
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
         * Verifica se existe, pelo menos, um movimento possível de qualquer peça inimiga em 
         * jogo.
         */
        public Boolean VerificarXeque()
        {
            Boolean[,] movimentosPossiveis;
            List<Peca> pecasInimigasEmJogo;
            Peca meuRei = new Rei(JogadorAtual);

            // procurando a posição do Rei do jogador atual
            foreach (Peca peca in PecasEmJogo)
                if (peca is Rei && peca.Cor == JogadorAtual)
                {
                    meuRei = peca;
                    break;
                }

            // definindo as peças inimigas em jogo 
            if (JogadorAtual == Cor.Branco)
                pecasInimigasEmJogo = EmJogoPretas();
            else
                pecasInimigasEmJogo = EmJogoBrancas();

            // verificando se existe pelo menos um movimento possível
            // para pegar o Rei do jogador atual
            foreach (Peca pecaInimiga in pecasInimigasEmJogo)
            {
                movimentosPossiveis = pecaInimiga.MovimentosPossiveis();

                for (Int32 i = 0; i < Tabuleiro.Linhas; i++)
                    for (Int32 j = 0; j < Tabuleiro.Colunas; j++)
                        if (meuRei.PosicaoXadrez != null)
                            if (
                                movimentosPossiveis[
                                    meuRei.PosicaoXadrez.ToPosicaoMatriz().Linha,
                                    meuRei.PosicaoXadrez.ToPosicaoMatriz().Coluna
                                ]
                            )
                            {
                                Xeque = true;
                                return true;
                            }
            }

            Xeque = false;
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
            Boolean podeMover, estaEmXeque;
            Peca? peca, pecaCapturada, torre, pecaEnpassant;
            Peao peaoEnpassant;
            PosicaoMatriz origemMatriz, destinoMatriz, posicaoEnpassant;

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
                    origemMatriz = origem.ToPosicaoMatriz();
                    destinoMatriz = destino.ToPosicaoMatriz();

                    Tabuleiro.RetirarPeca(origem);
                    pecaCapturada = Tabuleiro.RetirarPeca(destino);

                    if (pecaCapturada != null && pecaCapturada.Cor != peca.Cor)
                    {
                        pecaCapturada.SetPosicaoXadrez(Tabuleiro, null);
                        PecasEmJogo.Remove(pecaCapturada);
                        PecasCapturadas.Add(pecaCapturada);
                    }

                    if (peca is Rei)
                    {
                        // #jogadaEspecial: Roque Pequeno
                        if (destinoMatriz.Coluna == origemMatriz.Coluna + 2)
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
                        if (destinoMatriz.Coluna == origemMatriz.Coluna - 2)
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

                    if (peca is Peao)
                    {
                        // #jogadaEspecial: En-Passant
                        // a coluna de destino é diferente da coluna de origem,
                        // e não houve a captura normal esperada.
                        if (
                            destinoMatriz.Coluna != origemMatriz.Coluna &&
                            pecaCapturada == null
                        )
                        {
                            if (origemMatriz.Linha == 3)
                            {
                                // andou a "NORDESTE", capturar a "LESTE"
                                if (destinoMatriz.Coluna == origemMatriz.Coluna + 1)
                                {
                                    posicaoEnpassant = new('a', 1)
                                    {
                                        Linha = origemMatriz.Linha,
                                        Coluna = destinoMatriz.Coluna
                                    };
                                    pecaEnpassant = Tabuleiro.RetornarAPecaEmJogo(
                                        posicaoEnpassant.ToPosicaoXadrez()
                                    );

                                    if (pecaEnpassant != null)
                                        if (pecaEnpassant is Peao peao)
                                        {
                                            peaoEnpassant = peao;
                                            peaoEnpassant.SetCapturadoEnPassant(
                                                Tabuleiro, true
                                            );
                                            pecaCapturada = Tabuleiro.RetirarPeca(
                                                posicaoEnpassant.ToPosicaoXadrez()
                                            );
                                        }

                                    if (pecaCapturada != null)
                                    {
                                        pecaCapturada.SetPosicaoXadrez(Tabuleiro, null);
                                        PecasEmJogo.Remove(pecaCapturada);
                                        PecasCapturadas.Add(pecaCapturada);
                                    }
                                }

                                // andou a "NOROESTE", capturar a "OESTE"
                                if (destinoMatriz.Coluna == origemMatriz.Coluna - 1)
                                {
                                    posicaoEnpassant = new('a', 1)
                                    {
                                        Linha = origemMatriz.Linha,
                                        Coluna = destinoMatriz.Coluna
                                    };
                                    pecaEnpassant = Tabuleiro.RetornarAPecaEmJogo(
                                        posicaoEnpassant.ToPosicaoXadrez()
                                    );

                                    if (pecaEnpassant != null)
                                        if (pecaEnpassant is Peao peao)
                                        {
                                            peaoEnpassant = peao;
                                            peaoEnpassant.SetCapturadoEnPassant(Tabuleiro, true);
                                            pecaCapturada = Tabuleiro.RetirarPeca(
                                                posicaoEnpassant.ToPosicaoXadrez()
                                            );
                                        }

                                    if (pecaCapturada != null)
                                    {
                                        pecaCapturada.SetPosicaoXadrez(Tabuleiro, null);
                                        PecasEmJogo.Remove(pecaCapturada);
                                        PecasCapturadas.Add(pecaCapturada);
                                    }
                                }
                            }

                            if (origemMatriz.Linha == 4)
                            {
                                // andou a "SUDESTE", capturar a "LESTE"
                                if (destinoMatriz.Coluna == origemMatriz.Coluna + 1)
                                {
                                    posicaoEnpassant = new('a', 1)
                                    {
                                        Linha = origemMatriz.Linha,
                                        Coluna = destinoMatriz.Coluna
                                    };
                                    pecaEnpassant = Tabuleiro.RetornarAPecaEmJogo(
                                        posicaoEnpassant.ToPosicaoXadrez()
                                    );

                                    if (pecaEnpassant != null)
                                        if (pecaEnpassant is Peao peao)
                                        {
                                            peaoEnpassant = peao;
                                            peaoEnpassant.SetCapturadoEnPassant(Tabuleiro, true);
                                            pecaCapturada = Tabuleiro.RetirarPeca(
                                                posicaoEnpassant.ToPosicaoXadrez()
                                            );
                                        }

                                    if (pecaCapturada != null)
                                    {
                                        pecaCapturada.SetPosicaoXadrez(Tabuleiro, null);
                                        PecasEmJogo.Remove(pecaCapturada);
                                        PecasCapturadas.Add(pecaCapturada);
                                    }
                                }

                                // andou a "SUDOESTE", capturar a "OESTE"
                                if (destinoMatriz.Coluna == origemMatriz.Coluna - 1)
                                {
                                    posicaoEnpassant = new('a', 1)
                                    {
                                        Linha = origemMatriz.Linha,
                                        Coluna = destinoMatriz.Coluna
                                    };
                                    pecaEnpassant = Tabuleiro.RetornarAPecaEmJogo(
                                        posicaoEnpassant.ToPosicaoXadrez()
                                    );

                                    if (pecaEnpassant != null)
                                        if (pecaEnpassant is Peao peao)
                                        {
                                            peaoEnpassant = peao;
                                            peaoEnpassant.SetCapturadoEnPassant(Tabuleiro, true);
                                            pecaCapturada = Tabuleiro.RetirarPeca(
                                                posicaoEnpassant.ToPosicaoXadrez()
                                            );
                                        }

                                    if (pecaCapturada != null)
                                    {
                                        pecaCapturada.SetPosicaoXadrez(Tabuleiro, null);
                                        PecasEmJogo.Remove(pecaCapturada);
                                        PecasCapturadas.Add(pecaCapturada);
                                    }
                                }
                            }
                        }
                    }

                    Tabuleiro.ColocarPeca(peca, destino.Coluna, destino.Linha);
                    peca.SetPosicaoXadrez(Tabuleiro, destino);
                    peca.IncrementarMovimento(Tabuleiro);
                    Turno++;

                    estaEmXeque = VerificarXeque();

                    if (estaEmXeque)
                    {
                        // não posso me colocar em XEQUE, então minha jogada é desfeita.
                        DesfazerMovimento(origem, destino, pecaCapturada);
                        AlternarJogadorAtual();
                        throw new TabuleiroException(
                            "Você colocou o seu Rei em XEQUE! Jogada desfeita! "
                        );
                    }
                    else
                    {
                        AlternarJogadorAtual();
                        VerificarXeque();
                    }
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
         *      2.4-> contudo, se a captura foi por "en-passant", também executar:
         *          2.4.1-> a peça capturada será restaurada:
         *              2.4.1.1-> na linha de origem da peça que a capturou;
         *              2.4.1.2-> na coluna de destino da peça que a capturou.
         *          2.4.2-> atualizar a sua posição (linha de origem, coluna de destino)
         *      2.5-> colocá-la no tabuleiro de volta.
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
            Peao peaoCapturadoEnpassant;
            PosicaoMatriz? origemMatriz, destinoMatriz, destinoEnpassant;

            peca = Tabuleiro.RetornarAPecaEmJogo(destino);

            if (
                origem != null &&
                destino != null &&
                peca != null &&
                peca.PosicaoXadrez != null
            )
            {
                Tabuleiro.RetirarPeca(destino);

                // se houve peça capturada ...
                if (pecaCapturada != null)
                {
                    // se a peça capturada é um peão ...
                    if (peca is Peao && pecaCapturada is Peao peao)
                    {
                        peaoCapturadoEnpassant = peao;

                        // se o peão foi capturado por "en-passant" ...
                        if (peaoCapturadoEnpassant.CapturadoEnPassant)
                        {
                            origemMatriz = origem.ToPosicaoMatriz();
                            destinoMatriz = destino.ToPosicaoMatriz();

                            destinoEnpassant = new(0, 0)
                            {
                                Linha = origemMatriz.Linha,
                                Coluna = destinoMatriz.Coluna
                            };

                            pecaCapturada.SetPosicaoXadrez(
                                Tabuleiro, destinoEnpassant.ToPosicaoXadrez()
                            );

                            peaoCapturadoEnpassant.SetCapturadoEnPassant(Tabuleiro, false);
                        }
                        // se a captura do peão foi normal ...
                        else
                            pecaCapturada.SetPosicaoXadrez(Tabuleiro, destino);
                    }
                    // se a peça capturada não foi um peão ...
                    else
                        pecaCapturada.SetPosicaoXadrez(Tabuleiro, destino);

                    // remove a peça capturada das peças capturadas,
                    // e a coloca nas peças em jogo ...
                    PecasCapturadas.Remove(pecaCapturada);
                    PecasEmJogo.Add(pecaCapturada);

                    if (pecaCapturada.PosicaoXadrez != null)
                        Tabuleiro.ColocarPeca(
                            pecaCapturada,
                            pecaCapturada.PosicaoXadrez.Coluna,
                            pecaCapturada.PosicaoXadrez.Linha
                        );
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
