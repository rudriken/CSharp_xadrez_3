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
            Turno = 0;
            JogadorAtual = Cor.Branco;
            Terminada = false;

            PecasEmJogo = [];
            foreach (Peca item in Tabuleiro.PecasEmJogo)            
                PecasEmJogo.Add(item);
            
            PecasCapturadas = [];
        }

        /* 
         * Retorna a peça que estiver na posição dada durante a partida.
         */
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
         * Verifica se há uma peça inimiga na posição dada durante a partida.
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
         * Verifica se a posição dada está vaga durante a partida.
         */
        public Boolean EstaVaga(PosicaoXadrez pos)
        {
            Peca? p = RetornarAPecaEmJogo(pos);

            return p == null;
        }

        
    }
}
