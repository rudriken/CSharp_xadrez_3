using extra;

namespace tabuleiro
{
    class Peao(Cor cor) : Peca(cor)
    {
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
                    temInimigo = Tabuleiro.TemInimigo(this, posicaoMatriz.ToPosicaoXadrez());
                    posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                    if (posicaoValida && (estaVaga || temInimigo) && Movimentos == 0)
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                    // uma casa para o "NORTE":
                    posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                    posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                    posicaoMatriz.Linha--;
                    estaVaga = Tabuleiro.EstaVaga(posicaoMatriz.ToPosicaoXadrez());
                    temInimigo = Tabuleiro.TemInimigo(this, posicaoMatriz.ToPosicaoXadrez());
                    posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                    if (posicaoValida && (estaVaga || temInimigo))
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;
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
                    temInimigo = Tabuleiro.TemInimigo(this, posicaoMatriz.ToPosicaoXadrez());
                    posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                    if (posicaoValida && (estaVaga || temInimigo) && Movimentos == 0)
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                    // uma casa para o "SUL":
                    posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                    posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                    posicaoMatriz.Linha++;
                    estaVaga = Tabuleiro.EstaVaga(posicaoMatriz.ToPosicaoXadrez());
                    temInimigo = Tabuleiro.TemInimigo(this, posicaoMatriz.ToPosicaoXadrez());
                    posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                    if (posicaoValida && (estaVaga || temInimigo))
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;
                }
            }

            return matriz;
        }
    }
}
