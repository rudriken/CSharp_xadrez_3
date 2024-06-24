using extra;

namespace tabuleiro
{
    class Dama(Cor cor) : Peca(cor)
    {
        public override String ToString()
        {
            return "D";
        }

        /* 
         * Retorna uma matriz de movimentos possíveis para a Dama
         */
        public override Boolean[,] MovimentosPossiveis()
        {
            Boolean[,] matriz;
            PosicaoMatriz posicaoMatriz;
            PosicaoXadrez posicaoXadrez;
            Boolean temInimigo, estaVaga, posicaoValida, inimigoContado;

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
                temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                inimigoContado = false;
                while (true)
                {
                    if (!posicaoValida)
                        break;

                    if (estaVaga)
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                    if (temInimigo && !inimigoContado)
                    {
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;
                        inimigoContado = true;
                    }

                    if (inimigoContado || !estaVaga)
                        break;

                    posicaoMatriz.Linha--;
                    temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                    estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                    posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                }

                // NORDESTE:
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Linha--;
                posicaoMatriz.Coluna++;
                temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                inimigoContado = false;
                while (true)
                {
                    if (!posicaoValida)
                        break;

                    if (estaVaga)
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                    if (temInimigo && !inimigoContado)
                    {
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;
                        inimigoContado = true;
                    }

                    if (inimigoContado || !estaVaga)
                        break;

                    posicaoMatriz.Linha--;
                    posicaoMatriz.Coluna++;
                    temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                    estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                    posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                }

                // LESTE:
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Coluna++;
                temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                inimigoContado = false;
                while (true)
                {
                    if (!posicaoValida)
                        break;

                    if (estaVaga)
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                    if (temInimigo && !inimigoContado)
                    {
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;
                        inimigoContado = true;
                    }

                    if (inimigoContado || !estaVaga)
                        break;

                    posicaoMatriz.Coluna++;
                    temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                    estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                    posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                }

                // SUDESTE:
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Linha++;
                posicaoMatriz.Coluna++;
                temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                inimigoContado = false;
                while (true)
                {
                    if (!posicaoValida)
                        break;

                    if (estaVaga)
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                    if (temInimigo && !inimigoContado)
                    {
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;
                        inimigoContado = true;
                    }

                    if (inimigoContado || !estaVaga)
                        break;

                    posicaoMatriz.Linha++;
                    posicaoMatriz.Coluna++;
                    temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                    estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                    posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                }

                // SUL:
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Linha++;
                temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                inimigoContado = false;
                while (true)
                {
                    if (!posicaoValida)
                        break;

                    if (estaVaga)
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                    if (temInimigo && !inimigoContado)
                    {
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;
                        inimigoContado = true;
                    }

                    if (inimigoContado || !estaVaga)
                        break;

                    posicaoMatriz.Linha++;
                    temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                    estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                    posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                }

                // SUDOESTE:
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Linha++;
                posicaoMatriz.Coluna--;
                temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                inimigoContado = false;
                while (true)
                {
                    if (!posicaoValida)
                        break;

                    if (estaVaga)
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                    if (temInimigo && !inimigoContado)
                    {
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;
                        inimigoContado = true;
                    }

                    if (inimigoContado || !estaVaga)
                        break;

                    posicaoMatriz.Linha++;
                    posicaoMatriz.Coluna--;
                    temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                    estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                    posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                }

                // OESTE:
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Coluna--;
                temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                inimigoContado = false;
                while (true)
                {
                    if (!posicaoValida)
                        break;

                    if (estaVaga)
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                    if (temInimigo && !inimigoContado)
                    {
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;
                        inimigoContado = true;
                    }

                    if (inimigoContado || !estaVaga)
                        break;

                    posicaoMatriz.Coluna--;
                    temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                    estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                    posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                }

                // NOROESTE:
                posicaoXadrez = new(PosicaoXadrez.Coluna, PosicaoXadrez.Linha);
                posicaoMatriz = posicaoXadrez.ToPosicaoMatriz();
                posicaoMatriz.Linha--;
                posicaoMatriz.Coluna--;
                temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                inimigoContado = false;
                while (true)
                {
                    if (!posicaoValida)
                        break;

                    if (estaVaga)
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;

                    if (temInimigo && !inimigoContado)
                    {
                        matriz[posicaoMatriz.Linha, posicaoMatriz.Coluna] = true;
                        inimigoContado = true;
                    }

                    if (inimigoContado || !estaVaga)
                        break;

                    posicaoMatriz.Linha--;
                    posicaoMatriz.Coluna--;
                    temInimigo = Tabuleiro.TemInimigoNoTabuleiro(this, posicaoMatriz.ToPosicaoXadrez());
                    estaVaga = Tabuleiro.EstaVagaNoTabuleiro(posicaoMatriz.ToPosicaoXadrez());
                    posicaoValida = Tabuleiro.PosicaoValida(posicaoMatriz.ToPosicaoXadrez());
                }
            }

            return matriz;
        }

    }
}
