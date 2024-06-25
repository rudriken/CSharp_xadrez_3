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
            Boolean colunaValida, linhaValida;

            if (posicaoXadrez != null)
            {
                if (posicaoXadrez.Length == 2)
                {
                    posicaoXadrez = posicaoXadrez.ToLower();

                    switch (posicaoXadrez[0])
                    {
                        case 'a': colunaValida = true; break;
                        case 'b': colunaValida = true; break;
                        case 'c': colunaValida = true; break;
                        case 'd': colunaValida = true; break;
                        case 'e': colunaValida = true; break;
                        case 'f': colunaValida = true; break;
                        case 'g': colunaValida = true; break;
                        case 'h': colunaValida = true; break;

                        default: colunaValida = false; break;
                    }

                    switch (posicaoXadrez[1])
                    {
                        case '1': linhaValida = true; break;
                        case '2': linhaValida = true; break;
                        case '3': linhaValida = true; break;
                        case '4': linhaValida = true; break;
                        case '5': linhaValida = true; break;
                        case '6': linhaValida = true; break;
                        case '7': linhaValida = true; break;
                        case '8': linhaValida = true; break;

                        default: linhaValida = false; break;
                    }

                    if (colunaValida && linhaValida)
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
            Boolean posicaoValida;

            if (entrada != null)
            {
                posicaoValida = VerificarFormatoPosicaoXadrez(entrada);

                if (posicaoValida)
                {
                    entrada = entrada.ToLower();
                    pos = new(entrada[0], Int32.Parse(entrada[1] + ""));
                }
                else
                    throw new TabuleiroException("Posição inválida! ");
            }

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
