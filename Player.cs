using System;

namespace TicTacToeGame
{
    public class Player
    {
        public string Symbol { get; private set; }
        public int Wins { get; private set; }
        public bool IsComputer { get; private set; }

        public Player(string symbol, bool isComputer = false)
        {
            Symbol = symbol;
            Wins = 0;
            IsComputer = isComputer;
        }

        public void IncrementWins()
        {
            Wins++;
        }

        public void ResetWins()
        {
            Wins = 0;
        }
    }
}
