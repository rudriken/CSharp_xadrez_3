using extra;
using tabuleiro;

namespace xadrez_3
{
    class Program
    {
        static void Main()
        {
            //Tabuleiro tab = new(8, 8);

            //tab.ColocarPeca(
            //    new Torre(new PosicaoMatriz(5,5), Cor.Branco), new PosicaoMatriz(5,5)
            //);
            //Tela.ImprimirTabuleiro(tab);

            //Console.WriteLine(new PosicaoXadrez('a', 2));

            PosicaoXadrez posXadrez = new('h', 1);
            PosicaoMatriz posMatriz = posXadrez.ToPosicaoMatriz();

            Console.WriteLine(posMatriz);
        }
    }
}