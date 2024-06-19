using extra;

namespace extra
{
    class PosicaoXadrez(Char coluna, Int32 linha)
    {
        public Char Coluna { get; private set; } = coluna;
        public Int32 Linha { get; private set; } = linha;

        /* 
         * Verifica se a entrada pelo teclado corresponde a uma posição de xadrez "a1".
         */
        public static Boolean ValidarPosicaoXadrez(String? entrada)
        {
            Int32 tamanho;
            Char caractere1, caractere2;

            if (entrada != null)
            {
                tamanho = entrada.Length;

                if (tamanho == 2)
                {
                    caractere1 = entrada[0];

                    if (Char.TryParse(caractere1 + "", out _))
                    {
                        caractere2 = entrada[1];

                        if (Int32.TryParse(caractere2 + "", out _))
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }

            return false;
        }

        /* 
         * Converte uma string para uma notação de posição de xadrez, se possível.
         */
        public static PosicaoXadrez? StringParaPosicaoXadrez(String? entrada)
        {
            Char coluna;
            Int32 linha;

            if (ValidarPosicaoXadrez(entrada))
            {
                if (entrada != null)
                {
                    coluna = Funcoes.ConverterCaractereParaMinusculo(entrada[0]);
                    linha = Int32.Parse(entrada[1] + "");

                    return new PosicaoXadrez(coluna, linha);
                }

                return null;
            }

            return null;
        }

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
