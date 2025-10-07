using System;

namespace TicTacToeGame
{
    public class GameBoard
    {
        private string[,] board;
        public int Size { get; private set; }
        public int WinLength { get; private set; }

        public GameBoard(int size)
        {
            Size = size;
            WinLength = size;
            board = new string[Size, Size];
            Reset();
        }

        public void Reset()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    board[i, j] = "";
                }
            }
        }

        public bool MakeMove(int row, int col, string symbol)
        {
            if (row < 0 || row >= Size || col < 0 || col >= Size)
                return false;

            if (string.IsNullOrEmpty(board[row, col]))
            {
                board[row, col] = symbol;
                return true;
            }
            return false;
        }

        // JAUNA METODE: Ļauj iestatīt jebkuru vērtību (arī tukšu) bez pārbaudēm
        // Izmanto tikai AI simulācijai!
        public void SetCell(int row, int col, string value)
        {
            if (row >= 0 && row < Size && col >= 0 && col < Size)
            {
                board[row, col] = value;
            }
        }

        public string GetCell(int row, int col)
        {
            if (row < 0 || row >= Size || col < 0 || col >= Size)
                return null;
            return board[row, col];
        }

        public bool IsFull()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (string.IsNullOrEmpty(board[i, j]))
                        return false;
                }
            }
            return true;
        }

        public bool IsEmpty(int row, int col)
        {
            return string.IsNullOrEmpty(GetCell(row, col));
        }
    }
}