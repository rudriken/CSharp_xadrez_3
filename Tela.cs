using extra;
using jogo;
using tabuleiro;

namespace xadrez_3
{
    class Tela
    {
        /* 
         * Responsável por, apenas, imprimir o tabuleiro com as peças em jogo.
         */
        public static void ImprimirTabuleiro(PartidaDeXadrez partida, String? origemString)
        {
            Int32 i, j;
            Boolean impresso;
            List<Peca> pecasEmJogo;
            PosicaoMatriz? posicaoMatriz;
            PosicaoXadrez? origemXadrez;
            ConsoleColor corFundoOriginal = Console.BackgroundColor;
            ConsoleColor corLetraOriginal = Console.ForegroundColor;
            Boolean[,] movimentosPossiveis;
            Peca? peca;
            Boolean movimentoPossivel = false;

            pecasEmJogo = partida.PecasEmJogo;

            origemXadrez = PosicaoXadrez.ConverterEmPosicaoXadrez(origemString);

            if (origemXadrez != null)
            {
                peca = partida.Tabuleiro.RetornarAPecaEmJogo(origemXadrez);

                if (peca == null)
                    throw new TabuleiroException("Posição sem peça!");

                if (peca.Cor != partida.JogadorAtual)
                    throw new TabuleiroException("Peça não correspondente ao jogador atual! ");

                movimentosPossiveis = peca.MovimentosPossiveis();

                foreach (Boolean possivel in movimentosPossiveis)
                    if (possivel)
                    {
                        movimentoPossivel = true;
                        break;
                    }

                if (!movimentoPossivel)
                    throw new TabuleiroException("Nenhum movimento possível! ");
            }
            else
                movimentosPossiveis = new Boolean[
                    partida.Tabuleiro.Linhas, partida.Tabuleiro.Colunas
                ];

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  a b c d e f g h \n");
            Console.ForegroundColor = corLetraOriginal;

            for (i = 0; i < partida.Tabuleiro.Linhas; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write((8 - i) + " ");
                Console.ForegroundColor = corLetraOriginal;

                for (j = 0; j < partida.Tabuleiro.Colunas; j++)
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

            Console.WriteLine("\nTurno: " + partida.Turno);
            Console.Write("Jogador atual: ");

            if (partida.JogadorAtual == Cor.Branco)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine(partida.JogadorAtual + "\n");

            Console.ForegroundColor = corLetraOriginal;

            if (partida.Xeque)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("XEQUE! \n");
                Console.ForegroundColor = corLetraOriginal;
            }

            Console.WriteLine("Peças capturadas: ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Brancas: [");
            foreach (Peca item in partida.CapturadasBrancas())
                Console.Write(item + " ");
            Console.WriteLine("]");
            Console.ForegroundColor = corLetraOriginal;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Pretas: [");
            foreach (Peca item in partida.CapturadasPretas())
                Console.Write(item + " ");
            Console.WriteLine("]\n");
            Console.ForegroundColor = corLetraOriginal;
        }
    }
}
