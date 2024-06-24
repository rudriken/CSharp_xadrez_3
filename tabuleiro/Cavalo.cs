using extra;

namespace tabuleiro
{
    class Cavalo(Cor cor) : Peca(cor)
    {
        public override String ToString()
        {
            return "C";
        }

        /* 
         * Retorna uma matriz de movimentos possíveis para o Cavalo
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
                // ##
                // #
                // #
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Linha -= 2;
                posicaoMatriz.Coluna++;
                temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                if (posicaoValida && (estaVaga || temInimigo))                
                    matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                // ###
                //   #
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Linha += 1;
                posicaoMatriz.Coluna += 2;
                temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                if (posicaoValida && (estaVaga || temInimigo))
                    matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                //  #
                //  #
                // ##
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Linha += 2;
                posicaoMatriz.Coluna -= 1;
                temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                if (posicaoValida && (estaVaga || temInimigo))
                    matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                // #
                // ###
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Linha -= 1;
                posicaoMatriz.Coluna -= 2;
                temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                if (posicaoValida && (estaVaga || temInimigo))
                    matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                // ##
                //  #
                //  #
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Linha -= 2;
                posicaoMatriz.Coluna -= 1;
                temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                if (posicaoValida && (estaVaga || temInimigo))
                    matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                //   #
                // ###
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Linha -= 1;
                posicaoMatriz.Coluna += 2;
                temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                if (posicaoValida && (estaVaga || temInimigo))
                    matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                // #
                // #
                // ##
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Linha += 2;
                posicaoMatriz.Coluna += 1;
                temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                if (posicaoValida && (estaVaga || temInimigo))
                    matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                // ###
                // #
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Linha += 1;
                posicaoMatriz.Coluna -= 2;
                temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                if (posicaoValida && (estaVaga || temInimigo))
                    matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;
            }

            return matriz;
        }

    }
}
