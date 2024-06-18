using extra;
using tabuleiro;

namespace xadrez_3
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            Int32 i, j;
            Boolean impresso;
            List<Peca> pecasEmJogo;
            PosicaoMatriz? posicaoMatriz;

            pecasEmJogo = tabuleiro.PecasEmJogo;

            for (i = 0; i < tabuleiro.Linhas; i++)
            {
                for (j = 0; j < tabuleiro.Colunas; j++)
                {
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
                }

                Console.WriteLine();
            }
        }
    }
}
