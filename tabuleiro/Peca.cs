using extra;

namespace tabuleiro
{
    abstract class Peca(Cor cor)
    {
        public PosicaoXadrez? PosicaoXadrez { get; private set; } = null;
        public Cor Cor { get; protected set; } = cor;
        public Tabuleiro? Tabuleiro { get; protected set; } = null;
        public Int32 Movimentos { get; protected set; } = 0;
        public Boolean Promovida { get; protected set; } = false;
        public Int32 MovPromocao { get; protected set; } = -1;

        public virtual void SetPromovida(Object objeto, Boolean promovido)
        {
            throw new TabuleiroException(
                "Sem permissão para alterar a situação de 'promovido' da peça!" + 
                "Somente a peça 'Peão' que pode ser promovida a Dama! "
            );
        }

        public virtual void IncrementarMovPromocao()
        {
            throw new TabuleiroException(
                "Sem permissão para alterar a quantidade de movimentos da peça" +
                "depois de sua promoção! "
            );
        }

        public virtual void DecrementarMovPromocao()
        {
            throw new TabuleiroException(
                "Sem permissão para alterar a quantidade de movimentos da peça" +
                "depois de sua promoção! "
            );
        }

        public void SetMovimentos(Object objeto, Int32 movimentos)
        {
            if (objeto is Tabuleiro)
                Movimentos = movimentos;
            else
                throw new TabuleiroException(
                    "Sem permissão para alterar a quantidade de movimentos da peça!"
                );
        }

        public void SetPosicaoXadrez(Object objeto, PosicaoXadrez? pos)
        {
            if (objeto is Tabuleiro)
                PosicaoXadrez = pos;
            else
                throw new TabuleiroException(
                    "Sem permissão para alterar a posição da peça!"
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

        public void IncrementarMovimento(Object objeto)
        {
            if (objeto is Tabuleiro)
                Movimentos++;
            else
                throw new TabuleiroException("Não foi permitido o incremento de movimento!");
        }

        public void DecrementarMovimento(Object objeto)
        {
            if (objeto is Tabuleiro)
                Movimentos--;
            else
                throw new TabuleiroException("Não foi permitido o decremento de movimento!");
        }

        public abstract Boolean[,] MovimentosPossiveis();
    }
}
