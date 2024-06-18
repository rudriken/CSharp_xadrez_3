namespace extra
{
    class PosicaoXadrez(Char coluna, Int32 linha)
    {
        public Char Coluna { get; private set; } = coluna;
        public Int32 Linha { get; private set; } = linha;

        /* 
         * Converte uma posiçao de xadrez para uma posição de matriz.
         */
        public PosicaoMatriz ToPosicaoMatriz()
        {
            Int32 linha, coluna;

            linha = 8 - Linha;
            coluna = Coluna - 'a';

            return new PosicaoMatriz(linha, coluna);
        }

        public override String ToString()
        {
            return Coluna + "" + Linha + "";
        }
    }
}
