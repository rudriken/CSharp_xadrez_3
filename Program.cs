using extra;
using tabuleiro;

namespace xadrez_3
{
    class Program
    {
        static void Main()
        {
            Tabuleiro tab;
            String? origem, destino;

            tab = new(8, 8);

            Tela.ImprimirTabuleiro(tab);

            Console.Write("Origem: ");
            origem = Console.ReadLine();

            Console.Clear();
            Tela.ImprimirTabuleiro(tab);

            Console.Write("Destino: ");
            destino = Console.ReadLine();

            Console.Clear();
            Tela.ImprimirTabuleiro(tab);
        }
    }
}