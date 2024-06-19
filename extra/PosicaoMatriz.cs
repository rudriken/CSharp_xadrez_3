namespace extra
{
    class PosicaoMatriz(Int32 linha, Int32 coluna)
    {
        public Int32 Linha { get; set; } = linha;
        public Int32 Coluna { get; set; } = coluna;

        /* 
         * Converte uma posiçao de matriz para uma posição de xadrez.
         */
        public PosicaoXadrez ToPosicaoXadrez()
        {
            Char coluna;
            Int32 linha;

            linha = 8 - Linha;
            coluna = (Char)('a' + Coluna);

            return new PosicaoXadrez(coluna, linha);
        }       


        public override string ToString()
        {
            return Linha + ", " + Coluna;
        }
    }
}
