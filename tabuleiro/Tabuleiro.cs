using extra;

namespace tabuleiro
{
    class Tabuleiro(
        Int32 linhas, Int32 colunas, List<Peca> pecasEmJogo, List<Peca> pecasCapturadas
    )
    {
        public Int32 Linhas { get; protected set; } = linhas;
        public Int32 Colunas { get; protected set; } = colunas;
        public List<Peca> PecasEmJogo { get; protected set; } = pecasEmJogo;
        public List<Peca> PecasCapturadas { get; protected set; } = pecasCapturadas;

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

        //public void ColocarPeca(Peca peca, PosicaoMatriz posicaoMatriz)
        //{

        //}
    }
}
