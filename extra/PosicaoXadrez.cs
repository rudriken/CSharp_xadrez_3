using extra;

namespace extra
{
    class PosicaoXadrez(Char coluna, Int32 linha)
    {
        public Char Coluna { get; private set; } = coluna;
        public Int32 Linha { get; private set; } = linha;

        /* 
         * Verifica se a string dada tem o formato de posição de xadrez.
         */
        public static Boolean VerificarFormatoPosicaoXadrez(String? posicaoXadrez)
        {
            Boolean valido;

            if (posicaoXadrez != null)
            {
                if (posicaoXadrez.Length == 2)
                {
                    posicaoXadrez = posicaoXadrez.ToLower();

                    valido = false;

                    switch (posicaoXadrez[0])
                    {
                        case 'a': valido = true; break;
                        case 'b': valido = true; break;
                        case 'c': valido = true; break;
                        case 'd': valido = true; break;
                        case 'e': valido = true; break;
                        case 'f': valido = true; break;
                        case 'g': valido = true; break;
                        case 'h': valido = true; break;
                    }

                    switch (posicaoXadrez[1])
                    {
                        case '1': valido = true; break;
                        case '2': valido = true; break;
                        case '3': valido = true; break;
                        case '4': valido = true; break;
                        case '5': valido = true; break;
                        case '6': valido = true; break;
                        case '7': valido = true; break;
                        case '8': valido = true; break;
                    }

                    if (valido)
                        return true;
                }
            }

            return false;
        }

        /* 
         * Converte a string dada para o formato de posição de xadrez.
         */
        public static PosicaoXadrez? ConverterEmPosicaoXadrez(String? entrada)
        {
            PosicaoXadrez? pos = null;

            if (entrada != null)
                if (VerificarFormatoPosicaoXadrez(entrada))                
                    pos = new(entrada[0], Int32.Parse(entrada[1] + ""));

            return pos;
        }

        /* 
         * Converte uma string para uma notação de posição de xadrez, se possível.
         */
        public static PosicaoXadrez? StringParaPosicaoXadrez(String? entrada)
        {
            Char coluna;
            Int32 linha;

            if (VerificarFormatoPosicaoXadrez(entrada))
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
