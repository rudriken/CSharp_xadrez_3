namespace extra
{
    class Funcoes
    {
        /* 
         * Converte um caractere em maiúsculo.
         * Se esse caractere já estiver em maiúsculo ou não for uma letra, retornará 
         * o próprio caractere.
         */
        public static Char ConverterCaractereParaMaiusculo(Char caractere)
        {
            Int32 teste;

            teste = (Int32)caractere;

            if (teste >= 97 && teste <= 122)
            {
                teste -= 32;
                return (Char)teste;
            }
            else
                return caractere;
        }

        /* 
         * Converte um caractere em minúsculo.
         * Se esse caractere já estiver em minúsculo ou não for uma letra, retornará 
         * o próprio caractere.
         */
        public static Char ConverterCaractereParaMinusculo(Char caractere)
        {
            Int32 teste;

            teste = (Int32)caractere;

            if (teste >= 65 && teste <= 90)
            {
                teste += 32;
                return (Char)teste;
            }
            else
                return caractere;
        }
    }
}
