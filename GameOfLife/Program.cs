using System;
using System.Collections.Generic;

namespace GameOfLife
{
    public struct Coordinates
    {
        public int x, y;
        public Coordinates(int p1, int p2)
        {
            x = p1;
            y = p2;
        }
    }

    public class Game
    {
        public Game()
        {
            var board = new Board(5,5);
            board.Print();
            board.PrintCoords();
        }

        public void PrintMenu()
        {

        }

        public void NextGen()
        {
            // Work out the next generation.

        }
    }

    public class Board
    {
        private Cell[,] _grid;

        public Board(int sizeX, int sizeY)
        {
            // TODO: given a board of dimensions x cells by y cells, create cells to fill it
            // and work out each cells neighbours.
            _grid = new Cell[sizeX, sizeY];

            for (int x=0; x < sizeX; x++)
            {
                for (int y=0; y < sizeY; y++)
                {
                    _grid[x,y] = new Cell(x,y);
                }
            }
        }

        public void Print()
        {
            for (int y=0; y < _grid.GetLength(0); y++)
            {
                for (int x=0; x < _grid.GetLength(1); x++)
                {
                    Console.Write(_grid[x,y].IsCurrentlyAlive ? "*" : " ");
                }

                Console.Write("\n");
            }
        }

        public void PrintCoords()
        {
            for (int y=0; y < _grid.GetLength(0); y++)
            {
                for (int x=0; x < _grid.GetLength(1); x++)
                {
                    Console.Write(x + "," + y + "|");
                }

                Console.Write("\n");
            }
        }
    }

    public class Cell
    {
        public bool IsCurrentlyAlive { get; set; }
        public bool IsFutureAlive { get; set; }
        public Coordinates Position { get; set; }
        public List<Cell> NeighbourCells { get; set; }
        public Coordinates coordinates { get; set; }

        public Cell(int x, int y)
        {
            var coordinates = new Coordinates
            {
                x = x,
                y = y
            };

            Random random = new Random();
            this.IsCurrentlyAlive = random.Next(2) == 0 ? false : true;
            this.IsFutureAlive = false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
        }
    }
}
