using extra;
using jogo;

namespace tabuleiro
{
    class Tabuleiro
    {
        public Int32 Linhas { get; protected set; }
        public Int32 Colunas { get; protected set; }
        public List<Peca> PecasNoTabuleiro { get; protected set; }

        public Tabuleiro(Int32 linhas, Int32 colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            PecasNoTabuleiro = [];
            ColocarPecas();
        }

        /* 
         * Retorna a peça que estiver na posição dada no tabuleiro.
         */
        public Peca? RetornarAPecaEmJogo(PosicaoXadrez? pos)
        {
            foreach (Peca peca in PecasNoTabuleiro)
                if (peca.PosicaoXadrez != null)
                    if (
                        peca.PosicaoXadrez.Linha == pos?.Linha &&
                        peca.PosicaoXadrez.Coluna == pos?.Coluna
                    )
                        return peca;

            return null;
        }

        /* 
         * Verifica se há uma peça inimiga na posição dada no tabuleiro.
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
         * Retira a peça do tabuleiro, anula a sua posição, e a retorna.
         */
        public Peca? RetirarPeca(PosicaoXadrez? pos)
        {
            Peca? peca = RetornarAPecaEmJogo(pos);

            if (peca != null)
            {
                PecasNoTabuleiro.Remove(peca);
                peca.SetPosicaoXadrez(this, null);
            }

            return peca;
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
                throw new TabuleiroException(
                    "Erro na colocação da peça: posição inválida! "
                );

            posicaoXadrez = new(coluna, linha);

            posicaoValida = PosicaoValida(posicaoXadrez);
            estaVaga = EstaVaga(posicaoXadrez);

            if (posicaoValida && estaVaga)
            {
                peca.SetPosicaoXadrez(this, posicaoXadrez);
                peca.SetTabuleiro(this, this);
                PecasNoTabuleiro.Add(peca);
            }
            else
            {
                if (!posicaoValida)
                    throw new TabuleiroException(
                        $"Posição ({posicaoXadrez}) não existe neste tabuleiro!"
                    );

                if (!estaVaga)
                    throw new TabuleiroException(
                        $"Posição '{posicaoXadrez}' está ocupada!"
                    );
            }
        }

        public void ColocarPecas()
        {
            ColocarPeca(new Torre(Cor.Branco), 'A', 1);
            ColocarPeca(new Cavalo(Cor.Branco), 'B', 1);
            ColocarPeca(new Bispo(Cor.Branco), 'C', 1);
            ColocarPeca(new Dama(Cor.Branco), 'D', 1);
            ColocarPeca(new Rei(Cor.Branco), 'E', 1);
            ColocarPeca(new Bispo(Cor.Branco), 'F', 1);
            ColocarPeca(new Cavalo(Cor.Branco), 'G', 1);
            ColocarPeca(new Torre(Cor.Branco), 'H', 1);
            ColocarPeca(new Peao(Cor.Branco), 'A', 2);
            ColocarPeca(new Peao(Cor.Branco), 'B', 2);
            ColocarPeca(new Peao(Cor.Branco), 'C', 2);
            ColocarPeca(new Peao(Cor.Branco), 'D', 2);
            ColocarPeca(new Peao(Cor.Branco), 'E', 2);
            ColocarPeca(new Peao(Cor.Branco), 'F', 2);
            ColocarPeca(new Peao(Cor.Branco), 'G', 2);
            ColocarPeca(new Peao(Cor.Branco), 'H', 2);

            ColocarPeca(new Torre(Cor.Preto), 'A', 8);
            ColocarPeca(new Cavalo(Cor.Preto), 'B', 8);
            ColocarPeca(new Bispo(Cor.Preto), 'C', 8);
            ColocarPeca(new Dama(Cor.Preto), 'D', 8);
            ColocarPeca(new Rei(Cor.Preto), 'E', 8);
            ColocarPeca(new Bispo(Cor.Preto), 'F', 8);
            ColocarPeca(new Cavalo(Cor.Preto), 'G', 8);
            ColocarPeca(new Torre(Cor.Preto), 'H', 8);
            ColocarPeca(new Peao(Cor.Preto), 'A', 7);
            ColocarPeca(new Peao(Cor.Preto), 'B', 7);
            ColocarPeca(new Peao(Cor.Preto), 'C', 7);
            ColocarPeca(new Peao(Cor.Preto), 'D', 7);
            ColocarPeca(new Peao(Cor.Preto), 'E', 7);
            ColocarPeca(new Peao(Cor.Preto), 'F', 7);
            ColocarPeca(new Peao(Cor.Preto), 'G', 7);
            ColocarPeca(new Peao(Cor.Preto), 'H', 7);


            //ColocarPeca(new Rei(Cor.Branco), 'a', 4);
            //ColocarPeca(new Peao(Cor.Branco), 'B', 7);

            //ColocarPeca(new Rei(Cor.Preto), 'h', 7);
            //ColocarPeca(new Torre(Cor.Preto), 'H', 8);
        }
    }
}
