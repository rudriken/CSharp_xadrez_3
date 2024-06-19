using extra;

namespace tabuleiro
{
    class Tabuleiro
    {
        public Int32 Linhas { get; protected set; }
        public Int32 Colunas { get; protected set; }
        public List<Peca> PecasEmJogo { get; protected set; }
        public List<Peca> PecasCapturadas { get; protected set; }

        public Tabuleiro(Int32 linhas, Int32 colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            PecasEmJogo = [];
            PecasCapturadas = [];
            ColocarPecas();
        }

        public Peca? RetornarAPecaEmJogo(PosicaoXadrez? pos)
        {
            foreach (Peca peca in PecasEmJogo)
                if (peca.PosicaoXadrez != null)
                    if (
                        peca.PosicaoXadrez.Linha == pos?.Linha &&
                        peca.PosicaoXadrez.Coluna == pos?.Coluna
                    )
                        return peca;

            return null;
        }

        /* 
         * Verifica se há uma peça inimiga na posição dada do tabuleiro.
         */
        public Boolean TemInimigo(Peca peca, PosicaoXadrez pos)
        {
            Peca? p = RetornarAPecaEmJogo(pos);

            if (p != null)
                if (p.Cor != peca.Cor)
                    return true;

            return false;
        }

        /* 
         * Verifica se a posição dada está vaga no tabuleiro.
         */
        public Boolean EstaVaga(PosicaoXadrez pos)
        {
            Peca? p = RetornarAPecaEmJogo(pos);

            return p == null;
        }

        /* 
         * Verifica se a posição dada está dentro dos limites do tabuleiro.
         */
        public Boolean PosicaoValida(PosicaoXadrez pos)
        {
            PosicaoMatriz posicaoMatriz = pos.ToPosicaoMatriz();

            if (
                posicaoMatriz.Linha >= 0 && posicaoMatriz.Linha < Linhas &&
                posicaoMatriz.Coluna >= 0 && posicaoMatriz.Coluna < Colunas
            )
                return true;

            return false;
        }

        /* 
         * Coloca uma peça na tabuleiro se a posição dada for válida e estiver vaga.
         */
        public void ColocarPeca(Peca peca, Char coluna, Int32 linha)
        {
            Boolean posicaoValida, estaVaga;
            PosicaoXadrez posicaoXadrez;
            Int32 teste;

            teste = (Int32)coluna;

            if (!(teste >= 97 && teste <= 122))
                teste += 32;

            coluna = (Char)teste;
            teste = (Int32)coluna;

            if (!(teste >= 97 && teste <= 122))
                throw new TabuleiroException("Erro na colocação da peça: posição inválida! ");

            posicaoXadrez = new(coluna, linha);

            posicaoValida = PosicaoValida(posicaoXadrez);
            estaVaga = EstaVaga(posicaoXadrez);

            if (posicaoValida && estaVaga)
            {
                peca.SetPosicaoXadrez(this, posicaoXadrez);
                peca.SetEmJogo(this, true);
                peca.SetTabuleiro(this, this);
                PecasEmJogo.Add(peca);
            }
            else
            {
                if (!posicaoValida)
                    throw new TabuleiroException(
                        $"Posição ({posicaoXadrez}) não existe neste tabuleiro!"
                    );

                if (!estaVaga)
                    throw new TabuleiroException(
                        $"Posição ({posicaoXadrez}) está ocupada!"
                    );
            }
        }

        public void ColocarPecas()
        {
            ColocarPeca(new Torre(Cor.Branco), 'a', 2);
            ColocarPeca(new Torre(Cor.Branco), 'd', 6);
            ColocarPeca(new Torre(Cor.Preto), 'h', 6);
            ColocarPeca(new Bispo(Cor.Preto), 'D', 3);
            ColocarPeca(new Cavalo(Cor.Branco), 'c', 1);
            ColocarPeca(new Rei(Cor.Branco), 'e', 6);
        }
    }
}
