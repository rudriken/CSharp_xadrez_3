using extra;

namespace tabuleiro
{
    class Rei(Cor cor) : Peca(cor)
    {
        public override String ToString()
        {
            return "R";
        }

        /* 
         * Retorna uma matriz de movimentos possíveis para o Rei
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
            }

            return matriz;
        }
    }
}
