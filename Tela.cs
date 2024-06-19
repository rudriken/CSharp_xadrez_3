using extra;
using tabuleiro;

namespace xadrez_3
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, String? posicaoEscolhida)
        {
            Int32 i, j;
            Boolean impresso;
            List<Peca> pecasEmJogo;
            PosicaoMatriz? posicaoMatriz, posicaoEscolhidaMatriz;
            PosicaoXadrez? posicaoEscolhidaXadrez;
            ConsoleColor corFundoOriginal = Console.BackgroundColor;
            Boolean[,] movimentosPossiveis;
            Peca? pecaEscolhida;

            pecasEmJogo = tabuleiro.PecasEmJogo;
            posicaoEscolhidaXadrez = PosicaoXadrez.StringParaPosicaoXadrez(posicaoEscolhida);
            posicaoEscolhidaMatriz = posicaoEscolhidaXadrez?.ToPosicaoMatriz();
            pecaEscolhida = tabuleiro.RetornarAPecaEmJogo(posicaoEscolhidaXadrez);
            if (pecaEscolhida != null)
                movimentosPossiveis = pecaEscolhida.MovimentosPossiveis();
            else
                movimentosPossiveis = new Boolean[tabuleiro.Linhas, tabuleiro.Colunas];

            for (i = 0; i < tabuleiro.Linhas; i++)
            {
                for (j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (posicaoEscolhidaMatriz != null)
                    {
                        if (pecaEscolhida != null)
                        {
                            if (movimentosPossiveis[i, j])
                            {
                                Console.BackgroundColor = ConsoleColor.DarkGray;
                            }
                        }
                    }

                    impresso = false;
                    foreach (Peca item in pecasEmJogo)
                    {
                        posicaoMatriz = item.PosicaoXadrez?.ToPosicaoMatriz();

                        if (posicaoMatriz?.Linha == i && posicaoMatriz?.Coluna == j)
                        {
                            Console.Write(item + " ");
                            impresso = true;
                        }
                    }

                    if (!impresso)
                        Console.Write("- ");

                    if (Console.BackgroundColor != corFundoOriginal)
                        Console.BackgroundColor = corFundoOriginal;
                }

                Console.WriteLine();
            }
        }
    }
}
