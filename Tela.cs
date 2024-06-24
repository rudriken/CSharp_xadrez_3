using extra;
using tabuleiro;

namespace xadrez_3
{
    class Tela
    {
        /* 
         * Responsável por imprimir o tabuleiro com as peças em jogo.
         */
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            Int32 i, j;
            Boolean impresso;
            List<Peca> pecasEmJogo;
            PosicaoMatriz? posicaoMatriz;
            ConsoleColor corFundoOriginal = Console.BackgroundColor;
            ConsoleColor corLetraOriginal = Console.ForegroundColor;
            Boolean[,] movimentosPossiveis;

            pecasEmJogo = tabuleiro.PecasEmJogo;
            movimentosPossiveis = new Boolean[tabuleiro.Linhas, tabuleiro.Colunas];

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  a b c d e f g h \n");
            Console.ForegroundColor = corLetraOriginal;

            for (i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write((8 - i) + " ");
                Console.ForegroundColor = corLetraOriginal;

                for (j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (movimentosPossiveis[i, j])
                        Console.BackgroundColor = ConsoleColor.DarkGray;

                    impresso = false;
                    foreach (Peca item in pecasEmJogo)
                    {
                        posicaoMatriz = item.PosicaoXadrez?.ToPosicaoMatriz();

                        if (posicaoMatriz?.Linha == i && posicaoMatriz?.Coluna == j)
                        {
                            if (item.Cor == Cor.Branco)
                                Console.ForegroundColor = ConsoleColor.Yellow;

                            if (item.Cor == Cor.Preto)
                                Console.ForegroundColor = ConsoleColor.Blue;

                            Console.Write(item + " ");
                            impresso = true;

                            Console.ForegroundColor = corLetraOriginal;
                        }
                    }

                    if (!impresso)
                        Console.Write("- ");

                    if (Console.BackgroundColor != corFundoOriginal)
                        Console.BackgroundColor = corFundoOriginal;
                }


                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine((8 - i) + " ");
                Console.ForegroundColor = corLetraOriginal;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  a b c d e f g h \n");
            Console.ForegroundColor = corLetraOriginal;
        }
    }
}
