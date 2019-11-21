using System;
using System.Collections.Generic;

namespace GameOfLife
{
    public class Game
    {

    }

    public class Board
    {
        public Board(int sizeX, int sizeY)
        {
            // TODO: given a board of dimensions x and y, create cells to fill it
            // and assign neighbouring cells to each cell.


        }
    }

    public class Cell
    {
        public bool IsCurrentlyAlive { get; set; }

        public bool IsFutureAlive { get; set; }

        public struct Coordinates
        {
            private int x;
            private int y;
        }

        public Coordinates Position { get; set; }

        public List<Cell> NeighbourCells { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
