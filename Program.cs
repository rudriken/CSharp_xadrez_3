using extra;
using tabuleiro;

namespace xadrez_3
{
    class Program
    {
        static void Main()
        {
            Tabuleiro tab;

            tab = new(8, 8);

            Tela.ImprimirTabuleiro(tab);
        }
    }
}