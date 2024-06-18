using extra;

namespace tabuleiro
{
    abstract class Peca(
        PosicaoMatriz? posicaoMatriz, Cor cor, Boolean emJogo, Tabuleiro tabuleiro)
    {
        public PosicaoMatriz? PosicaoMatriz { get; protected set; } = posicaoMatriz;
        public Cor Cor { get; protected set; } = cor;
        public Boolean EmJogo { get; protected set; } = emJogo;
        public Tabuleiro Tabuleiro { get; protected set; } = tabuleiro;

        public abstract Boolean[,] MovimentosPossiveis();        
    }
}
