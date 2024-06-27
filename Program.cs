using extra;
using jogo;

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
                    Tela.ImprimirTabuleiro(partida, null);

                    Console.Write("Origem: ");
                    origem = Console.ReadLine();

                    Tela.ImprimirTabuleiro(partida, origem);
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida, origem);

                    Console.Write("Destino: ");
                    destino = Console.ReadLine();

                    partida.ExecutarMovimento(
                        PosicaoXadrez.ConverterEmPosicaoXadrez(origem),
                        PosicaoXadrez.ConverterEmPosicaoXadrez(destino)
                    );

                    Tela.ImprimirTabuleiro(partida, null);

                    Console.Clear();
                }
                catch (TabuleiroException erro)
                {
                    Console.WriteLine(erro.Message);
                    Console.ReadLine();
                    Console.Clear();
                }
            }

            Tela.ImprimirTabuleiro(partida, null);
        }
    }
}