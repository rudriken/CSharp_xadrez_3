using extra;
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
        public Cor Vencedor {  get; private set; }

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
        private void VerificarXeque()
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
                                Xeque = true;
            }
        }

        /* 
         * Verifica se existe, pelo menos, um movimento possível para que o jogador atual 
         * saia do XEQUE, para não ocorrer o XEQUE-MATE.
         */
        private Boolean VerificarXequeMate()
        {
            Boolean[,] movimentosPossiveis;
            PosicaoMatriz? origemMatriz, destinoMatriz;
            Peca? pecaCapturada;

            if (Xeque)
            {
                if (JogadorAtual == Cor.Branco)
                {
                    foreach (Peca peca in EmJogoBrancas())
                    {
                        movimentosPossiveis = peca.MovimentosPossiveis();
                        origemMatriz = peca.PosicaoXadrez?.ToPosicaoMatriz();

                        for (Int32 i = 0; i < Tabuleiro.Linhas; i++)
                        {
                            for (Int32 j = 0; j < Tabuleiro.Colunas; j++)
                            {
                                if (movimentosPossiveis[i, j])
                                {
                                    destinoMatriz = new(i, j);

                                    pecaCapturada = MoverPeca(
                                        origemMatriz?.ToPosicaoXadrez(),
                                        destinoMatriz.ToPosicaoXadrez()
                                    );

                                    VerificarXeque();

                                    DesfazerMovimento(
                                        origemMatriz?.ToPosicaoXadrez(),
                                        destinoMatriz.ToPosicaoXadrez(),
                                        pecaCapturada
                                    );

                                    if (!Xeque)
                                        return false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (Peca peca in EmJogoPretas())
                    {
                        movimentosPossiveis = peca.MovimentosPossiveis();
                        origemMatriz = peca.PosicaoXadrez?.ToPosicaoMatriz();

                        for (Int32 i = 0; i < Tabuleiro.Linhas; i++)
                        {
                            for (Int32 j = 0; j < Tabuleiro.Colunas; j++)
                            {
                                if (movimentosPossiveis[i, j])
                                {
                                    destinoMatriz = new(i, j);

                                    MoverPeca(
                                        origemMatriz?.ToPosicaoXadrez(),
                                        destinoMatriz.ToPosicaoXadrez()
                                    );

                                    VerificarXeque();

                                    DesfazerMovimento(
                                        origemMatriz?.ToPosicaoXadrez(),
                                        destinoMatriz.ToPosicaoXadrez(),
                                        null
                                    );

                                    if (!Xeque)
                                        return false;
                                }
                            }
                        }
                    }
                }
            }
            else
                return false;

            return true;
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
        public Peca? MoverPeca(PosicaoXadrez? origem, PosicaoXadrez? destino)
        {
            Boolean podeMover;
            Peca? peca, pecaCapturada, torre, pecaEnpassant, promocao;
            Peao peaoEnpassant;
            PosicaoMatriz origemMatriz, destinoMatriz, posicaoEnpassant;
            Int32 guardaMovimentos;

            peca = Tabuleiro.RetornarAPecaEmJogo(origem);
            podeMover = PodeMoverPeca(origem, destino);
            pecaCapturada = null;

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

                    // se a peça mexida é um Rei ...
                    if (peca is Rei)
                    {
                        // #jogadaEspecial: Roque Pequeno
                        // a coluna de destino tem duas casas a mais da sua origem
                        // e a Torre está a 3 colunas a mais de sua origem (coluna 'H') ...
                        if (destinoMatriz.Coluna == origemMatriz.Coluna + 2)
                        {
                            // se o Rei mexido for branco ...
                            if (peca.Cor == Cor.Branco)
                            {
                                torre = Tabuleiro.RetornarAPecaEmJogo(
                                    new PosicaoXadrez('h', 1)
                                );

                                // se a Torre está na sua posição original no tabuleiro
                                // e ainda sem movimento ...
                                if (
                                    torre != null &&
                                    torre is Torre &&
                                    torre.Movimentos == 0 &&
                                    torre.PosicaoXadrez != null
                                )
                                {
                                    Tabuleiro.RetirarPeca(torre.PosicaoXadrez);
                                    Tabuleiro.ColocarPeca(torre, 'f', 1);
                                    torre.SetPosicaoXadrez(
                                        Tabuleiro, new PosicaoXadrez('f', 1)
                                    );
                                    torre.IncrementarMovimento(Tabuleiro);
                                }
                            }
                            // se o Rei mexido for preto ...
                            else
                            {
                                torre = Tabuleiro.RetornarAPecaEmJogo(
                                    new PosicaoXadrez('h', 8)
                                );

                                // se a Torre está na sua posição original no tabuleiro
                                // e ainda sem movimento ...
                                if (
                                    torre != null &&
                                    torre is Torre &&
                                    torre.Movimentos == 0 &&
                                    torre.PosicaoXadrez != null
                                )
                                {
                                    Tabuleiro.RetirarPeca(torre.PosicaoXadrez);
                                    Tabuleiro.ColocarPeca(torre, 'f', 8);
                                    torre.SetPosicaoXadrez(
                                        Tabuleiro, new PosicaoXadrez('f', 8)
                                    );
                                    torre.IncrementarMovimento(Tabuleiro);
                                }
                            }
                        }

                        // #jogadaEspecial: Roque Grande
                        // a coluna de destino tem duas casas a menos da sua origem
                        // e a Torre está a 4 colunas a menos de sua origem (coluna 'A') ...
                        if (destinoMatriz.Coluna == origemMatriz.Coluna - 2)
                        {
                            if (peca.Cor == Cor.Branco)
                            {
                                torre = Tabuleiro.RetornarAPecaEmJogo(
                                    new PosicaoXadrez('a', 1)
                                );

                                // se a Torre está na sua posição original no tabuleiro
                                // e ainda sem movimento ...
                                if (
                                    torre != null &&
                                    torre is Torre &&
                                    torre.Movimentos == 0 &&
                                    torre.PosicaoXadrez != null
                                )
                                {
                                    Tabuleiro.RetirarPeca(torre.PosicaoXadrez);
                                    Tabuleiro.ColocarPeca(torre, 'd', 1);
                                    torre.SetPosicaoXadrez(
                                        Tabuleiro, new PosicaoXadrez('d', 1)
                                    );
                                    torre.IncrementarMovimento(Tabuleiro);
                                }
                            }
                            else
                            {
                                torre = Tabuleiro.RetornarAPecaEmJogo(
                                    new PosicaoXadrez('a', 8)
                                );

                                // se a Torre está na sua posição original no tabuleiro
                                // e ainda sem movimento ...
                                if (
                                    torre != null &&
                                    torre is Torre &&
                                    torre.Movimentos == 0 &&
                                    torre.PosicaoXadrez != null
                                )
                                {
                                    Tabuleiro.RetirarPeca(torre.PosicaoXadrez);
                                    Tabuleiro.ColocarPeca(torre, 'd', 8);
                                    torre.SetPosicaoXadrez(
                                        Tabuleiro, new PosicaoXadrez('d', 8)
                                    );
                                    torre.IncrementarMovimento(Tabuleiro);
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
                            }
                        }

                        // #jogadaEspecial: Promoção
                        /* Quando o Peão chega na última linha, ele se tornará uma Dama.
                         * Mecânica do processo:
                         *   1-> O Peão é retirado das peças em jogo;
                         *   2-> O Peão é retirado do tabuleiro;
                         *   3-> é criada uma nova Dama;
                         *   4-> a nova Dama tem seu tabuleiro definido;
                         *   5-> a nova Dama tem sua quantidade de movimentos atualizada;
                         *   6-> a nova Dama é inseridas nas peças em jogo;
                         *   7-> a peça, anteriormente um peão, agora será Dama;
                         *   8-> a nova peça é inserida no tabuleiro;
                         *   9-> a nova peça tem sua posição no xadrez atualizada;
                         *  10-> a nova peça tem seu movimento incrementado;
                         *  11-> o turno é incrementado
                         */
                        if (destino.Linha == 8 || destino.Linha == 1)
                        {
                            guardaMovimentos = peca.Movimentos;

                            PecasEmJogo.Remove(peca);
                            Tabuleiro.RetirarPeca(origem);
                            promocao = new Dama(JogadorAtual);
                            promocao.SetTabuleiro(Tabuleiro, Tabuleiro);
                            promocao.SetMovimentos(Tabuleiro, guardaMovimentos);
                            PecasEmJogo.Add(promocao);
                            peca = promocao;
                        }
                    }

                    Tabuleiro.ColocarPeca(peca, destino.Coluna, destino.Linha);
                    peca.SetPosicaoXadrez(Tabuleiro, destino);
                    peca.IncrementarMovimento(Tabuleiro);
                    Turno++;                    
                }
                else
                    throw new TabuleiroException("Posição de destino não permitida! ");
            }

            return pecaCapturada;
        }

        /* 
         * Move a peça escolhida de sua origem para o seu destino, verifica se o jogador 
         * atual entre em XEQUE com essa jogada. 
         * Se entrar em XEQUE, é desfeita (revertida) essa jogada. 
         * Se não entrar em XEQUE, é alternado o jogador atual e verifica se ele entra em 
         * XEQUE com a execução desse movimento.
         */
        public void ExecutarMovimento(PosicaoXadrez? origem, PosicaoXadrez? destino)
        {
            Peca? pecaCapturada;
            Boolean xequeMate;

            pecaCapturada = MoverPeca(origem, destino);

            // verifica se, com o movimento do jogador atual, ele entra em XEQUE ...
            VerificarXeque();

            if (Xeque)
            {
                // se o jogador atual se colocar em XEQUE sua jogada é desfeita ...
                DesfazerMovimento(origem, destino, pecaCapturada);

                // e sai do XEQUE ...
                Xeque = false;

                throw new TabuleiroException(
                    "O seu Rei está em XEQUE! Jogada desfeita! "
                );
            }
            // se o jogador atual não se colocou em XEQUE, é alternado o jogador ...
            else
            {
                AlternarJogadorAtual();

                // verificar se o jogador atual está em XEQUE com a jogada
                // do outro ...
                VerificarXeque();

                if (Xeque)
                {
                    xequeMate = VerificarXequeMate();

                    // e se não há mais o que se possa fazer para sair do XEQUE ...
                    if (xequeMate)
                    {
                        Terminada = true;
                        AlternarJogadorAtual();
                        Vencedor = JogadorAtual;
                    }
                }
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
                // retirar a peça mexida de seu destino ...
                Tabuleiro.RetirarPeca(destino);

                // se há peça capturada ...
                if (pecaCapturada != null)
                {
                    // se a peça mexida é um Peão ...
                    if (peca is Peao)
                    {
                        // se a peça capturada é um Peão ...
                        if (pecaCapturada is Peao peao)
                        {
                            peaoCapturadoEnpassant = peao;

                            // se o Peão foi capturado por "en-passant", a sua origem será
                            // a mesma linha de origem e a mesma coluna de destino da peça
                            // mexida ...
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

                                peaoCapturadoEnpassant.SetCapturadoEnPassant(
                                    Tabuleiro, false
                                );
                            }
                            // se a captura do Peão foi normal, a sua origem será a mesma
                            // do destino da peça mexida ...
                            else
                                pecaCapturada.SetPosicaoXadrez(Tabuleiro, destino);
                        }
                        // se a peça capturada não é um Peão, a sua origem será a mesma
                        // do destino da peça mexida ...
                        else
                            pecaCapturada.SetPosicaoXadrez(Tabuleiro, destino);


                    }

                    // remove a peça capturada das peças capturadas,
                    // a coloca nas peças em jogo ...
                    PecasCapturadas.Remove(pecaCapturada);
                    PecasEmJogo.Add(pecaCapturada);

                    // e coloca a peça anteriormente capturada de volta no tabuleiro ...
                    if (pecaCapturada.PosicaoXadrez != null)
                        Tabuleiro.ColocarPeca(
                            pecaCapturada,
                            pecaCapturada.PosicaoXadrez.Coluna,
                            pecaCapturada.PosicaoXadrez.Linha
                        );
                }

                // colocar a peça mexida, que foi retirada acima, de volta no tabuleiro ...
                Tabuleiro.ColocarPeca(peca, origem.Coluna, origem.Linha);
                peca.SetPosicaoXadrez(Tabuleiro, origem);

                // decrementa o movimento da peça mexida ...
                peca.DecrementarMovimento(Tabuleiro);

                // decrementa o turno, pois na mexida o turno foi incrementado ...
                Turno--;

                // mas não alterna o jogador atual.
            }
        }
    }
}
