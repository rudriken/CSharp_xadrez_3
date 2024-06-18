using extra;

namespace tabuleiro
{
    abstract class Peca(Cor cor)
    {
        public PosicaoXadrez? PosicaoXadrez { get; private set; } = null;
        public Cor Cor { get; protected set; } = cor;
        public Boolean EmJogo { get; protected set; } = false;
        public Tabuleiro? Tabuleiro { get; protected set; } = null;

        public void SetPosicaoXadrez(Object objeto, PosicaoXadrez pos)
        {
            if (objeto is Tabuleiro)
                PosicaoXadrez = pos;
            else
                throw new TabuleiroException(
                    "Sem permissão para alterar a posição da peça!"
                );
        }

        public void SetEmJogo(Object objeto, Boolean emJogo)
        {
            if (objeto is Tabuleiro)
                EmJogo = emJogo;
            else
                throw new TabuleiroException(
                    "Sem permissão para alterar a situação da peça!"
                );
        }

        public void SetTabuleiro(Object objeto, Tabuleiro tabuleiro)
        {
            if (objeto is Tabuleiro)
                Tabuleiro = tabuleiro;
            else
                throw new TabuleiroException(
                    "Sem permissão para alterar o tabuleiro da peça!"
                );
        }

        public abstract Boolean[,] MovimentosPossiveis();
    }
}
