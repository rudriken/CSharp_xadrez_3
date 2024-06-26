using extra;

namespace tabuleiro
{
    class Peao(Cor cor) : Peca(cor)
    {
        public Boolean CapturadoEnPassant { get; private set; }

        public void SetCapturadoEnPassant(Object objeto, Boolean capturadoEnPassant)
        {
            if (objeto is Tabuleiro)
                CapturadoEnPassant = capturadoEnPassant;
        }

        public override String ToString()
        {
            return "P";
        }

        /* 
         * Retorna uma matriz de movimentos possíveis para o Peao
         */
        public override Boolean[,] MovimentosPossiveis()
        {
            Boolean[,] matriz;
            PosicaoMatriz posicaoMatriz;
            PosicaoXadrez posicaoXadrez;
            Boolean temInimigo, estaVaga, posicaoValida;
            Peca? pecaInimiga;

            if (Tabuleiro != null)
                matriz = new Boolean[Tabuleiro.Linhas, Tabuleiro.Colunas];
            else
                matriz = new Boolean[0, 0];

            if (PosicaoXadrez != null && Tabuleiro != null)
            {
                if (Cor == Cor.Branco)
                {
                    // duas casas para o "NORTE":
                    posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                    posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                    posicaoMatriz.Linha--;
                    estaVaga = Tabuleiro.EstaVaga(posicaoMatriz.ToPosicaoXadrez());
                    if (estaVaga)
                    {
                        posicaoMatriz.Linha--;
                        estaVaga = Tabuleiro.EstaVaga(posicaoMatriz.ToPosicaoXadrez());
                    }
                    posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                    if (posicaoValida && estaVaga && Movimentos == 0)
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                    // uma casa para o "NORTE":
                    posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                    posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                    posicaoMatriz.Linha--;
                    estaVaga = Tabuleiro.EstaVaga(posicaoMatriz.ToPosicaoXadrez());
                    posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                    if (posicaoValida && estaVaga)
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                    // se tem inimigo a "NOROESTE":
                    posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                    posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                    posicaoMatriz.Linha--;
                    posicaoMatriz.Coluna--;
                    posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                    temInimigo = Tabuleiro.TemInimigo(this, posicaoMatriz.ToPosicaoXadrez());
                    if (posicaoValida && temInimigo)
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                    // se tem inimigo a "NORDESTE":
                    posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                    posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                    posicaoMatriz.Linha--;
                    posicaoMatriz.Coluna++;
                    posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                    temInimigo = Tabuleiro.TemInimigo(this, posicaoMatriz.ToPosicaoXadrez());
                    if (posicaoValida && temInimigo)
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                    // #jogadaEspecial: En-Passant (linha 3 na matriz, linha 5 no xadrez)                    
                    if (PosicaoXadrez.Linha == 5)
                    {
                        // Peão inimigo a "LESTE", casa vazia a "NORDESTE",
                        // movimento a "NORDESTE"
                        posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                        posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                        posicaoMatriz.Coluna++;
                        posicaoValida = Tabuleiro.PosicaoValida(
                            posicaoMatriz.ToPosicaoXadrez()
                        );
                        if (posicaoValida)
                        {
                            pecaInimiga = Tabuleiro.RetornarAPecaEmJogo(
                                posicaoMatriz.ToPosicaoXadrez()
                            );
                            posicaoMatriz.Linha--;
                            posicaoValida = Tabuleiro.PosicaoValida(
                                posicaoMatriz.ToPosicaoXadrez()
                            );
                            if (posicaoValida)
                            {
                                estaVaga = Tabuleiro.EstaVaga(
                                    posicaoMatriz.ToPosicaoXadrez()
                                );
                                if (
                                    estaVaga &&
                                    pecaInimiga != null &&
                                    pecaInimiga is Peao &&
                                    pecaInimiga.Cor != Cor
                                )
                                    matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;
                            }
                        }

                        // Peão inimigo a "OESTE", casa vazia a "NOROESTE",
                        // movimento a "NORDESTE"
                        posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                        posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                        posicaoMatriz.Coluna--;
                        posicaoValida = Tabuleiro.PosicaoValida(
                            posicaoMatriz.ToPosicaoXadrez()
                        );
                        if (posicaoValida)
                        {
                            pecaInimiga = Tabuleiro.RetornarAPecaEmJogo(
                                posicaoMatriz.ToPosicaoXadrez()
                            );
                            posicaoMatriz.Linha--;
                            posicaoValida = Tabuleiro.PosicaoValida(
                                posicaoMatriz.ToPosicaoXadrez()
                            );
                            if (posicaoValida)
                            {
                                estaVaga = Tabuleiro.EstaVaga(
                                    posicaoMatriz.ToPosicaoXadrez()
                                );
                                if (
                                    estaVaga &&
                                    pecaInimiga != null &&
                                    pecaInimiga is Peao &&
                                    pecaInimiga.Cor != Cor
                                )
                                    matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;
                            }
                        }
                    }
                }
                else
                {
                    // duas casas para o "SUL":
                    posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                    posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                    posicaoMatriz.Linha++;
                    estaVaga = Tabuleiro.EstaVaga(posicaoMatriz.ToPosicaoXadrez());
                    if (estaVaga)
                    {
                        posicaoMatriz.Linha++;
                        estaVaga = Tabuleiro.EstaVaga(posicaoMatriz.ToPosicaoXadrez());
                    }
                    posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                    if (posicaoValida && estaVaga && Movimentos == 0)
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                    // uma casa para o "SUL":
                    posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                    posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                    posicaoMatriz.Linha++;
                    estaVaga = Tabuleiro.EstaVaga(posicaoMatriz.ToPosicaoXadrez());
                    posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                    if (posicaoValida && estaVaga)
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                    // se tem inimigo a "SUDOESTE":
                    posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                    posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                    posicaoMatriz.Linha++;
                    posicaoMatriz.Coluna--;
                    posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                    temInimigo = Tabuleiro.TemInimigo(this, posicaoMatriz.ToPosicaoXadrez());
                    if (posicaoValida && temInimigo)
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                    // se tem inimigo a "SUDESTE":
                    posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                    posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                    posicaoMatriz.Linha++;
                    posicaoMatriz.Coluna++;
                    posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                    temInimigo = Tabuleiro.TemInimigo(this, posicaoMatriz.ToPosicaoXadrez());
                    if (posicaoValida && temInimigo)
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                    // #jogadaEspecial: En-Passant (linha 4 na matriz, linha 4 no xadrez)                    
                    if (PosicaoXadrez.Linha == 4)
                    {
                        // Peão inimigo a "LESTE", casa vazia a "SUDESTE",
                        // movimento a "SUDESTE"
                        posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                        posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                        posicaoMatriz.Coluna++;
                        posicaoValida = Tabuleiro.PosicaoValida(
                            posicaoMatriz.ToPosicaoXadrez()
                        );
                        if (posicaoValida)
                        {
                            pecaInimiga = Tabuleiro.RetornarAPecaEmJogo(
                                posicaoMatriz.ToPosicaoXadrez()
                            );
                            posicaoMatriz.Linha++;
                            posicaoValida = Tabuleiro.PosicaoValida(
                                posicaoMatriz.ToPosicaoXadrez()
                            );
                            if (posicaoValida)
                            {
                                estaVaga = Tabuleiro.EstaVaga(
                                    posicaoMatriz.ToPosicaoXadrez()
                                );
                                if (
                                    estaVaga &&
                                    pecaInimiga != null &&
                                    pecaInimiga is Peao &&
                                    pecaInimiga.Cor != Cor
                                )
                                    matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;
                            }
                        }

                        // Peão inimigo a "OESTE", casa vazia a "SUDOESTE",
                        // movimento a "SUDOESTE"
                        posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                        posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                        posicaoMatriz.Coluna--;
                        posicaoValida = Tabuleiro.PosicaoValida(
                            posicaoMatriz.ToPosicaoXadrez()
                        );
                        if (posicaoValida)
                        {
                            pecaInimiga = Tabuleiro.RetornarAPecaEmJogo(
                                posicaoMatriz.ToPosicaoXadrez()
                            );
                            posicaoMatriz.Linha++;
                            posicaoValida = Tabuleiro.PosicaoValida(
                                posicaoMatriz.ToPosicaoXadrez()
                            );
                            if (posicaoValida)
                            {
                                estaVaga = Tabuleiro.EstaVaga(
                                    posicaoMatriz.ToPosicaoXadrez()
                                );
                                if (
                                    estaVaga &&
                                    pecaInimiga != null &&
                                    pecaInimiga is Peao &&
                                    pecaInimiga.Cor != Cor
                                )
                                    matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;
                            }
                        }
                    }
                }
            }

            return matriz;
        }
    }
}
