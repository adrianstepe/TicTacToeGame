using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TicTacToeGame
{
    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }

    public class ComputerAI
    {
        private GameBoard gameBoard;
        private GameLogic gameLogic;
        private DifficultyLevel difficulty;
        private Random random;
        private string computerSymbol;
        private string playerSymbol;

        public ComputerAI(GameBoard board, GameLogic logic, DifficultyLevel difficulty, string computerSymbol, string playerSymbol)
        {
            this.gameBoard = board;
            this.gameLogic = logic;
            this.difficulty = difficulty;
            this.computerSymbol = computerSymbol;
            this.playerSymbol = playerSymbol;
            this.random = new Random();
        }

        public Point GetBestMove()
        {
            switch (difficulty)
            {
                case DifficultyLevel.Easy:
                    return GetRandomMove();
                case DifficultyLevel.Medium:
                    return GetMediumMove();
                case DifficultyLevel.Hard:
                    return GetHardMove();
                default:
                    return GetRandomMove();
            }
        }

        private Point GetRandomMove()
        {
            List<Point> availableMoves = GetAvailableMoves();
            if (availableMoves.Count > 0)
            {
                return availableMoves[random.Next(availableMoves.Count)];
            }
            return new Point(-1, -1);
        }

        private Point GetMediumMove()
        {
            if (random.Next(100) < 50)
            {
                return GetSmartMove();
            }
            return GetRandomMove();
        }

        private Point GetHardMove()
        {
            return GetSmartMove();
        }

        private Point GetSmartMove()
        {
            // 1. Mēģina uzvarēt
            Point winMove = FindWinningMove(computerSymbol);
            if (winMove.X != -1)
                return winMove;

            // 2. Bloķē pretinieka uzvaru
            Point blockMove = FindWinningMove(playerSymbol);
            if (blockMove.X != -1)
                return blockMove;

            // 3. Aizņem centru (ja pieejams)
            int center = gameBoard.Size / 2;
            if (gameBoard.IsEmpty(center, center))
                return new Point(center, center);

            // 4. Aizņem stūri
            Point cornerMove = GetBestCorner();
            if (cornerMove.X != -1)
                return cornerMove;

            // 5. Jebkurš pieejamais gājiens
            List<Point> available = GetAvailableMoves();
            if (available.Count > 0)
                return available[random.Next(available.Count)];

            return new Point(-1, -1);
        }

        private Point FindWinningMove(string symbol)
        {
            List<Point> availableMoves = GetAvailableMoves();

            foreach (Point move in availableMoves)
            {
                // Saglabā oriģinālo vērtību
                string originalValue = gameBoard.GetCell(move.X, move.Y);

                // Simulē gājienu (izmantojam SetCell, lai apietu pārbaudi)
                gameBoard.SetCell(move.X, move.Y, symbol);

                // Pārbauda vai tas ir uzvarošs gājiens
                bool isWinning = gameLogic.CheckWinner(symbol);

                // Atjauno uz oriģinālo stāvokli
                gameBoard.SetCell(move.X, move.Y, originalValue);

                if (isWinning)
                    return move;
            }

            return new Point(-1, -1);
        }

        private Point GetBestCorner()
        {
            List<Point> corners = new List<Point>
            {
                new Point(0, 0),
                new Point(0, gameBoard.Size - 1),
                new Point(gameBoard.Size - 1, 0),
                new Point(gameBoard.Size - 1, gameBoard.Size - 1)
            };

            List<Point> availableCorners = corners.Where(c => gameBoard.IsEmpty(c.X, c.Y)).ToList();

            if (availableCorners.Count > 0)
                return availableCorners[random.Next(availableCorners.Count)];

            return new Point(-1, -1);
        }

        private List<Point> GetAvailableMoves()
        {
            List<Point> moves = new List<Point>();

            for (int i = 0; i < gameBoard.Size; i++)
            {
                for (int j = 0; j < gameBoard.Size; j++)
                {
                    if (gameBoard.IsEmpty(i, j))
                    {
                        moves.Add(new Point(i, j));
                    }
                }
            }

            return moves;
        }
    }
}