using extra;
using tabuleiro;

namespace xadrez_3
{
    class Program
    {
        static void Main()
        {
            Tabuleiro tab;
            String? origem;

            tab = new(8, 8);

            Tela.ImprimirTabuleiro(tab, null);

            Console.Write("Origem: ");
            origem = Console.ReadLine();

            Console.Clear();
            Tela.ImprimirTabuleiro(tab, origem);
        }
    }
}