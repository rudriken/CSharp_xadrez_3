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

            while (!partida.Terminada)
            {
                try
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida, null);

                    Console.Write("Origem: ");
                    origem = Console.ReadLine();

                    Tela.ImprimirTabuleiro(partida, origem);
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida, origem);

                    Console.Write("Destino: ");
                    destino = Console.ReadLine();

                    partida.MoverPeca(
                        PosicaoXadrez.ConverterEmPosicaoXadrez(origem),
                        PosicaoXadrez.ConverterEmPosicaoXadrez(destino)
                    );

                    Tela.ImprimirTabuleiro(partida, null);
                }
                catch (TabuleiroException erro)
                {
                    Console.WriteLine(erro.Message);
                    Console.ReadLine();
                }
            }
        }
    }
}