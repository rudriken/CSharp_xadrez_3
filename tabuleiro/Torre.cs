using extra;

namespace tabuleiro
{
    class Torre(PosicaoMatriz? posicaoMatriz, Cor cor)
        : Peca(posicaoMatriz, cor)
    {
        public override String ToString()
        {
            return "T";
        }

        /* 
         * Retorna uma matriz de movimentos possíveis para a Torre
         */
        public override Boolean[,] MovimentosPossiveis()
        {
            Boolean[,] matriz;
            PosicaoMatriz posicao;
            Boolean temInimigo, estaVaga, posicaoValida;

            if (Tabuleiro != null)
                matriz = new Boolean[Tabuleiro.Linhas, Tabuleiro.Colunas];
            else
                matriz = new Boolean[0, 0];

            if (PosicaoMatriz != null && Tabuleiro != null)
            {
                // NORTE:
                posicao = new(PosicaoMatriz.Linha, PosicaoMatriz.Coluna);
                posicao.Linha--;
                temInimigo = Tabuleiro.TemInimigo(this, posicao);
                estaVaga = Tabuleiro.EstaVaga(posicao);
                posicaoValida = Tabuleiro.PosicaoValida(posicao);
                while ((!temInimigo || estaVaga) && posicaoValida)
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;

                    posicao.Linha--;
                    temInimigo = Tabuleiro.TemInimigo(this, posicao);
                    estaVaga = Tabuleiro.EstaVaga(posicao);
                    posicaoValida = Tabuleiro.PosicaoValida(posicao);
                }

                // LESTE:
                posicao = new(PosicaoMatriz.Linha, PosicaoMatriz.Coluna);
                posicao.Coluna++;
                temInimigo = Tabuleiro.TemInimigo(this, posicao);
                estaVaga = Tabuleiro.EstaVaga(posicao);
                posicaoValida = Tabuleiro.PosicaoValida(posicao);
                while ((!temInimigo || estaVaga) && posicaoValida)
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;

                    posicao.Coluna++;
                    temInimigo = Tabuleiro.TemInimigo(this, posicao);
                    estaVaga = Tabuleiro.EstaVaga(posicao);
                    posicaoValida = Tabuleiro.PosicaoValida(posicao);
                }

                // SUL:
                posicao = new(PosicaoMatriz.Linha, PosicaoMatriz.Coluna);
                posicao.Linha++;
                temInimigo = Tabuleiro.TemInimigo(this, posicao);
                estaVaga = Tabuleiro.EstaVaga(posicao);
                posicaoValida = Tabuleiro.PosicaoValida(posicao);
                while ((!temInimigo || estaVaga) && posicaoValida)
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;

                    posicao.Linha++;
                    temInimigo = Tabuleiro.TemInimigo(this, posicao);
                    estaVaga = Tabuleiro.EstaVaga(posicao);
                    posicaoValida = Tabuleiro.PosicaoValida(posicao);
                }

                // OESTE:
                posicao = new(PosicaoMatriz.Linha, PosicaoMatriz.Coluna);
                posicao.Coluna--;
                temInimigo = Tabuleiro.TemInimigo(this, posicao);
                estaVaga = Tabuleiro.EstaVaga(posicao);
                posicaoValida = Tabuleiro.PosicaoValida(posicao);
                while ((!temInimigo || estaVaga) && posicaoValida)
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;

                    posicao.Coluna--;
                    temInimigo = Tabuleiro.TemInimigo(this, posicao);
                    estaVaga = Tabuleiro.EstaVaga(posicao);
                    posicaoValida = Tabuleiro.PosicaoValida(posicao);
                }
            }

            return matriz;
        }
    }
}
