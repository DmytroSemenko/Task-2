using System;
using System.Collections.Generic;
using System.Linq;

namespace BFS
{
    class Program
    {
        static void Main(string[] args)
        {
            var whiteKnightPosition = new Cell(2, 1);
            var blackKnightPosition = new Cell(1, 5);

            try
            {
                var bestPathLength = new KnightBestPathFinder(8).FindBestPathLength(whiteKnightPosition, blackKnightPosition);
                Console.WriteLine(bestPathLength);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    public class Cell
    {
        public int x;
        public int y;
        public int distance;

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Cell(int x, int y, int distance) : this(x, y)
        {
            this.distance = distance;
        }

        public static Cell operator +(Cell c1, Cell c2)
        {
            return new Cell(c1.x + c2.x, c1.y + c2.y, c1.distance + c2.distance);
        }
    }

    public class KnightBestPathFinder
    {
        public KnightBestPathFinder(int chessboardSize)
        {
            ChessboardSize = chessboardSize;
        }

        public int ChessboardSize { get; }

        public int FindBestPathLength(Cell whiteKnightPosition, Cell blackKnightPosition)
        {
            var possibleWays = new Cell[8]
            {
                new Cell(1, 2, 1),
                new Cell(2, 1, 1),
                new Cell(-1, 2, 1),
                new Cell(-2, 1, 1),
                new Cell(1, -2, 1),
                new Cell(2, -1, 1),
                new Cell(-1, -2, 1),
                new Cell(-2, -1, 1)
            };

            var cellVisitCheck = new bool[ChessboardSize, ChessboardSize];

            Queue<Cell> queue = new Queue<Cell>();
            queue.Enqueue(whiteKnightPosition);
            cellVisitCheck[whiteKnightPosition.x, whiteKnightPosition.y] = true;

            while (queue.Count != 0)
            {
                var currentPosition = queue.Dequeue();

                if (currentPosition.x == blackKnightPosition.x &&
                    currentPosition.y == blackKnightPosition.y)
                    return currentPosition.distance;

                foreach (var possibleWay in possibleWays)
                {
                    var nextPosition = currentPosition + possibleWay;

                    if (IsOnChessboard(nextPosition) && !cellVisitCheck[nextPosition.x, nextPosition.y])
                    {
                        cellVisitCheck[nextPosition.x, nextPosition.y] = true;
                        queue.Enqueue(nextPosition);
                    }
                }

            }

            throw new PathNotFoundExeption();
        }

        private bool IsOnChessboard(Cell cell)
        {
            if (cell.x >= 1 && cell.x < ChessboardSize && 
                cell.y >= 1 && cell.y < ChessboardSize)
                return true;

            return false;
        }

        public class PathNotFoundExeption : Exception { }
    }
}
