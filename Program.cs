using extra;
using jogo;
using tabuleiro;

namespace xadrez_3
{
    class Program
    {
        static void Main()
        {
            PartidaDeXadrez partida;
            String? origem, destino;

            partida = new();

            Tela.ImprimirTabuleiro(partida.Tabuleiro, null);

            Console.Write("Origem: ");
            origem = Console.ReadLine();

            Console.Clear();
            Tela.ImprimirTabuleiro(partida.Tabuleiro, origem);

            Console.Write("Destino: ");
            destino = Console.ReadLine();



            Console.Clear();
            Tela.ImprimirTabuleiro(partida.Tabuleiro, null);
        }
    }
}