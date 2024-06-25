using extra;

namespace tabuleiro
{
    class Rei(Cor cor) : Peca(cor)
    {
        public override String ToString()
        {
            return "R";
        }

        private Boolean VerTorreParaRoque(Peca? peca)
        {
            PosicaoMatriz? posicaoMatrizTorre;
            int? linhaRei, colunaRei;

            posicaoMatrizTorre = peca?.PosicaoXadrez?.ToPosicaoMatriz();

            if (PosicaoXadrez != null)
            {
                linhaRei = PosicaoXadrez.ToPosicaoMatriz().Linha;
                colunaRei = PosicaoXadrez.ToPosicaoMatriz().Coluna;
            }
            else
            {
                linhaRei = null;
                colunaRei = null;
            }

            return
                peca != null &&
                peca is Torre &&
                peca.Cor == Cor &&
                peca.Movimentos == 0 &&
                Movimentos == 0 &&
                posicaoMatrizTorre != null &&
                linhaRei != null &&
                linhaRei == posicaoMatrizTorre.Linha &&
                colunaRei != null &&
                (
                    colunaRei == posicaoMatrizTorre.Coluna - 3 ||
                    colunaRei == posicaoMatrizTorre.Coluna + 4
                );
        }

        /* 
         * Retorna uma matriz de movimentos possíveis para o Rei
         */
        public override Boolean[,] MovimentosPossiveis()
        {
            Boolean[,] matriz;
            PosicaoMatriz posicaoMatriz;
            PosicaoXadrez posicaoXadrez;
            Boolean temInimigo, estaVaga, posicaoValida, roque;

            if (Tabuleiro != null)
                matriz = new Boolean[Tabuleiro.Linhas, Tabuleiro.Colunas];
            else
                matriz = new Boolean[0, 0];

            if (PosicaoXadrez != null && Tabuleiro != null)
            {
                // NORTE:
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Linha--;
                temInimigo = Tabuleiro.TemInimigo(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVaga(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                if (posicaoValida && (estaVaga || temInimigo))
                    matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                // NORDESTE:
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Linha--;
                posicaoMatriz.Coluna++;
                temInimigo = Tabuleiro.TemInimigo(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVaga(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                if (posicaoValida && (estaVaga || temInimigo))
                    matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                // LESTE:
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Coluna++;
                temInimigo = Tabuleiro.TemInimigo(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVaga(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                if (posicaoValida && (estaVaga || temInimigo))
                    matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                // SUDESTE:
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Linha++;
                posicaoMatriz.Coluna++;
                temInimigo = Tabuleiro.TemInimigo(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVaga(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                if (posicaoValida && (estaVaga || temInimigo))
                    matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                // SUL:
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Linha++;
                temInimigo = Tabuleiro.TemInimigo(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVaga(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                if (posicaoValida && (estaVaga || temInimigo))
                    matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                // SUDESTE:
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Linha++;
                posicaoMatriz.Coluna--;
                temInimigo = Tabuleiro.TemInimigo(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVaga(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                if (posicaoValida && (estaVaga || temInimigo))
                    matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                // LESTE:
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Coluna--;
                temInimigo = Tabuleiro.TemInimigo(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVaga(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                if (posicaoValida && (estaVaga || temInimigo))
                    matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                // NOROESTE:
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Linha--;
                posicaoMatriz.Coluna--;
                temInimigo = Tabuleiro.TemInimigo(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVaga(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                if (posicaoValida && (estaVaga || temInimigo))
                    matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                // #jogadaEspecial: Roque Pequeno
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Coluna++;
                estaVaga = Tabuleiro.EstaVaga(posicaoMatriz.ToPosicaoXadrez());
                roque = false;
                if (estaVaga)
                {
                    posicaoMatriz.Coluna++;
                    estaVaga = Tabuleiro.EstaVaga(posicaoMatriz.ToPosicaoXadrez());
                    if (estaVaga)
                    {
                        posicaoMatriz.Coluna++;
                        roque = VerTorreParaRoque(
                            Tabuleiro.RetornarAPecaEmJogo(posicaoMatriz.ToPosicaoXadrez())
                        );
                        posicaoMatriz.Coluna--;
                    }
                }
                if (roque)
                    matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                // #jogadaEspecial: Roque Grande
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Coluna--;
                estaVaga = Tabuleiro.EstaVaga(posicaoMatriz.ToPosicaoXadrez());
                roque = false;
                if (estaVaga)
                {
                    posicaoMatriz.Coluna--;
                    estaVaga = Tabuleiro.EstaVaga(posicaoMatriz.ToPosicaoXadrez());
                    if (estaVaga)
                    {
                        posicaoMatriz.Coluna--;
                        estaVaga = Tabuleiro.EstaVaga(posicaoMatriz.ToPosicaoXadrez());

                        if (estaVaga)
                        {
                            posicaoMatriz.Coluna--;
                            roque = VerTorreParaRoque(
                                    Tabuleiro.RetornarAPecaEmJogo(
                                        posicaoMatriz.ToPosicaoXadrez()
                                    )
                                );
                            posicaoMatriz.Coluna += 2;
                        }
                    }
                }
                if (roque)
                    matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;
            }

            return matriz;
        }
    }
}
