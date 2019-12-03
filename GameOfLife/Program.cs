using System;
using System.Collections.Generic;
using System.Threading;

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

    public enum CellType
    {
        CornerTopLeft,
        CornerBottomLeft,
        CornerTopRight,
        CornerBottomRight,
        Corner,
        EdgeTop,
        EdgeBottom,
        EdgeLeft,
        EdgeRight,
        Middle
    }

    public class Game
    {
        public Game(Board board)
        {
            board.Print();
            board.PrintCoords();

            while (this.NextGen())
            {
                board.Print();
                //await Task.Sleep(1000);
            }
        }

        public void PrintMenu()
        {

        }

        public bool NextGen()
        {
            // Work out the next generation.
            return false;
        }
    }

    public class Board
    {
        public Cell[,] Grid { get; set; }

        public Board(int sizeX, int sizeY)
        {
            // TODO: given a board of dimensions x cells by y cells, create cells to fill it
            // and work out each cells neighbours.
            Grid = new Cell[sizeX, sizeY];

            for (int x=0; x < sizeX; x++)
            {
                for (int y=0; y < sizeY; y++)
                {
                    Grid[x,y] = new Cell(x,y);
                }
            }

            CalculateCellTypes();
            CalculateNeighbours();
        }

        public void Print()
        {
            for (int y=0; y < Grid.GetLength(0); y++)
            {
                for (int x=0; x < Grid.GetLength(1); x++)
                {
                    Console.Write(Grid[x,y].IsCurrentlyAlive ? "*" : " ");
                }

                Console.Write("\n");
            }
        }

        public void PrintCoords()
        {
            for (int y=0; y < Grid.GetLength(0); y++)
            {
                for (int x=0; x < Grid.GetLength(1); x++)
                {
                    if (Grid[x,y].Colour == "red")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write(x + "," + y + "|");
                }

                Console.Write("\n");
            }
        }

        private void CalculateCellTypes()
        {
            for (int x=0; x < Grid.GetLength(0); x++)
            {
                for (int y=0; y < Grid.GetLength(1); y++)
                {
                    Grid[x,y] = new Cell(x,y);

                    // A corner type cell?
                    if (x + y == 0)
                    {
                        Grid[x,y].CellType = CellType.CornerTopLeft;
                        Grid[x,y].Colour = "red";
                    }
                    else if (x == y && x == Grid.GetLength(0)-1 && y == Grid.GetLength(1)-1)
                    {
                        Grid[x,y].CellType = CellType.CornerBottomRight;                        Grid[x,y].Colour = "red";
                        Grid[x,y].Colour = "red";
                    }
                    else if (x == Grid.GetLength(0)-1 && y == 0)
                    { 
                        Grid[x,y].CellType = CellType.CornerTopRight;
                        Grid[x,y].Colour = "red";
                    }
                    else if (y == Grid.GetLength(1)-1 && x == 0)
                    {
                        Grid[x,y].CellType = CellType.CornerBottomLeft;
                        Grid[x,y].Colour = "red";
                    }
                }
            }
        }

        public void CalculateNeighbours()
        {
            
        }
    }

    public class Cell
    {
        public bool IsCurrentlyAlive { get; set; }
        public bool IsFutureAlive { get; set; }
        public Coordinates Position { get; set; }
        public List<Cell> NeighbourCells { get; set; }
        public Coordinates coordinates { get; set; }
        public CellType CellType { get; set; }
        public string Colour { get; set; }

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
            var game = new Game(new Board(5,5));
        }
    }
}