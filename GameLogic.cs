using System;

namespace TicTacToeGame
{
    public class GameLogic
    {
        private GameBoard gameBoard;

        public GameLogic(GameBoard board)
        {
            gameBoard = board;
        }

        public bool CheckWinner(string symbol)
        {
            for (int i = 0; i < gameBoard.Size; i++)
            {
                if (CheckRow(i, symbol))
                    return true;
            }

            for (int j = 0; j < gameBoard.Size; j++)
            {
                if (CheckColumn(j, symbol))
                    return true;
            }

            if (CheckDiagonals(symbol))
                return true;

            return false;
        }

        private bool CheckRow(int row, string symbol)
        {
            for (int col = 0; col < gameBoard.Size; col++)
            {
                if (gameBoard.GetCell(row, col) != symbol)
                    return false;
            }
            return true;
        }

        private bool CheckColumn(int col, string symbol)
        {
            for (int row = 0; row < gameBoard.Size; row++)
            {
                if (gameBoard.GetCell(row, col) != symbol)
                    return false;
            }
            return true;
        }

        private bool CheckDiagonals(string symbol)
        {
            bool mainDiagonal = true;
            for (int i = 0; i < gameBoard.Size; i++)
            {
                if (gameBoard.GetCell(i, i) != symbol)
                {
                    mainDiagonal = false;
                    break;
                }
            }

            bool antiDiagonal = true;
            for (int i = 0; i < gameBoard.Size; i++)
            {
                if (gameBoard.GetCell(i, gameBoard.Size - 1 - i) != symbol)
                {
                    antiDiagonal = false;
                    break;
                }
            }

            return mainDiagonal || antiDiagonal;
        }

        public bool IsDraw()
        {
            return gameBoard.IsFull();
        }
    }
}