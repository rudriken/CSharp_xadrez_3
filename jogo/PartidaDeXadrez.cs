using extra;
using tabuleiro;

namespace jogo
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public Int32 Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public Boolean Terminada { get; private set; }
        public List<Peca> PecasEmJogo { get; private set; }
        public List<Peca> PecasCapturadas { get; private set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;

            PecasEmJogo = [];
            foreach (Peca item in Tabuleiro.PecasNoTabuleiro)
                PecasEmJogo.Add(item);

            PecasCapturadas = [];
        }

        private void AlternarJogadorAtual()
        {
            if (JogadorAtual == Cor.Branco)
                JogadorAtual = Cor.Preto;
            else
                JogadorAtual = Cor.Branco;
        }

        public List<Peca> CapturadasBrancas()
        {
            List<Peca> pecas = [];

            foreach (Peca item in PecasCapturadas)
                if (item.Cor == Cor.Branco)
                    pecas.Add(item);

            return pecas;
        }

        public List<Peca> CapturadasPretas()
        {
            List<Peca> pecas = [];

            foreach (Peca item in PecasCapturadas)
                if (item.Cor == Cor.Preto)
                    pecas.Add(item);

            return pecas;
        }

        /* 
         * Verifica se tem peça na origem, se essa peça é da cor do jogador atual, 
         * e se pode mover essa peça para o destino.
         */
        private Boolean PodeMoverPeca(PosicaoXadrez? origem, PosicaoXadrez? destino)
        {
            Boolean[,] movimentosPossiveis;
            Peca? peca;

            peca = Tabuleiro.RetornarAPecaEmJogo(origem);

            if (peca != null && destino != null && peca.Cor == JogadorAtual)
            {
                movimentosPossiveis = peca.MovimentosPossiveis();

                foreach (Boolean casa in movimentosPossiveis)
                    if (casa)
                        return true;
            }

            return false;
        }

        /* 
         * Mecânica da movimentação de uma peça:
         *  1-> retirar de sua origem;
         *  2-> capturar a peça que está no destino, se houver:
         *      2.1-> retirá-la das peças em jogo;
         *      2.2-> colocá-la nas peças capturadas.
         *  3-> colocar no seu destino;
         *  4-> atualizar a sua posição;
         *  5-> incrementar o seu movimento;
         *  6-> incrementar o turno;
         *  7-> alternar o jogador atual.
         */
        public void MoverPeca(PosicaoXadrez? origem, PosicaoXadrez? destino)
        {
            Boolean podeMover = PodeMoverPeca(origem, destino);
            Peca? peca, pecaCapturada;

            peca = Tabuleiro.RetornarAPecaEmJogo(origem);

            if (podeMover && destino != null && peca != null && peca.PosicaoXadrez != null)
            {
                Tabuleiro.RetirarPeca(origem);
                pecaCapturada = Tabuleiro.RetirarPeca(destino);

                if (pecaCapturada != null)
                {
                    PecasEmJogo.Remove(pecaCapturada);
                    PecasCapturadas.Add(pecaCapturada);
                }

                Tabuleiro.ColocarPeca(peca, destino.Coluna, destino.Linha);
                peca.SetPosicaoXadrez(Tabuleiro, destino);
                peca.IncrementarMovimento(Tabuleiro);
                Turno++;
                AlternarJogadorAtual();
            }
        }
    }
}
