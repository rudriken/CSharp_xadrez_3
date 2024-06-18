using extra;

namespace tabuleiro
{
    class Tabuleiro(
        Int32 linhas, Int32 colunas
    )
    {
        public Int32 Linhas { get; protected set; } = linhas;
        public Int32 Colunas { get; protected set; } = colunas;
        public List<Peca> PecasEmJogo { get; protected set; } = [];
        public List<Peca> PecasCapturadas { get; protected set; } = [];

        public Peca? RetornarAPecaEmJogo(PosicaoMatriz pos)
        {
            foreach (Peca peca in PecasEmJogo)
                if (peca.PosicaoMatriz == pos)
                    return peca;

            return null;
        }

        /* 
         * Verifica se há uma peça inimiga na posição dada do tabuleiro.
         */
        public Boolean TemInimigo(Peca peca, PosicaoMatriz pos)
        {
            Peca? p = RetornarAPecaEmJogo(pos);

            if (p?.Cor != peca.Cor)
                return true;

            return false;
        }

        /* 
         * Verifica se a posição dada está vaga no tabuleiro.
         */
        public Boolean EstaVaga(PosicaoMatriz pos)
        {
            Peca? p = RetornarAPecaEmJogo(pos);

            return p == null;
        }

        /* 
         * Verifica se a posição dada está dentro dos limites do tabuleiro.
         */
        public Boolean PosicaoValida(PosicaoMatriz pos)
        {
            if (
                pos.Linha >= 0 && pos.Linha < Linhas &&
                pos.Coluna >= 0 && pos.Coluna < Colunas
            )
                return true;

            return false;
        }

        /* 
         * Coloca uma peça na tabuleiro se a posição dada for válida e estiver vaga.
         */
        public void ColocarPeca(Peca peca, PosicaoMatriz posicaoMatriz)
        {
            Boolean posicaoValida, estaVaga;

            posicaoValida = PosicaoValida(posicaoMatriz);
            estaVaga = EstaVaga(posicaoMatriz);

            if (posicaoValida && estaVaga)
            {
                if (peca.PosicaoMatriz == null)
                    peca.SetPosicaoMatriz(this, posicaoMatriz);
                else
                {
                    peca.PosicaoMatriz.Linha = posicaoMatriz.Linha;
                    peca.PosicaoMatriz.Coluna = posicaoMatriz.Coluna;
                }

                PecasEmJogo.Add(peca);
            }
            else
            {
                if (!posicaoValida)
                    throw new TabuleiroException(
                        $"Posição ({posicaoMatriz.Linha}, {posicaoMatriz.Coluna}) " +
                        $"não existe neste tabuleiro!"
                    );

                if (!estaVaga)
                    throw new TabuleiroException(
                        $"Posição ({posicaoMatriz.Linha}, {posicaoMatriz.Coluna}) " +
                        $"está ocupada!"
                    );
            }
        }

        public void ColocarPecas()
        {

        }
    }
}
