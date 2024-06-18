using extra;

namespace tabuleiro
{
    abstract class Peca(
        PosicaoMatriz? posicaoMatriz, Cor cor)
    {
        public PosicaoMatriz? PosicaoMatriz { get; private set; } = posicaoMatriz;
        public Cor Cor { get; protected set; } = cor;
        public Boolean EmJogo { get; protected set; } = false;
        public Tabuleiro? Tabuleiro { get; protected set; } = null;

        public void SetPosicaoMatriz(Object objeto, PosicaoMatriz pos)
        {
            if (objeto is Tabuleiro)
                PosicaoMatriz = pos;
            else
                throw new TabuleiroException(
                    "Sem permissão para alterar a posição da peça!"
                );
        }

        public abstract Boolean[,] MovimentosPossiveis();
    }
}
