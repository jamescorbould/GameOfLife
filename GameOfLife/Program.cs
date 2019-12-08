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
        Middle,
        Unassigned
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
                    switch (Grid[x,y].Colour)
                    {
                        case "red":
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case "orange":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            break;
                        case "green":
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
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
                        Grid[x,y].CellType = CellType.CornerBottomRight;
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

                    // A cell on the edge of the grid?
                    if (Grid[x,y].CellType == CellType.Unassigned)
                    {
                        if (x == 0)
                        {
                            Grid[x,y].CellType = CellType.EdgeLeft;
                            Grid[x,y].Colour = "orange";
                        }
                        else if (x == Grid.GetLength(0)-1)
                        {
                            Grid[x,y].CellType = CellType.EdgeRight;
                            Grid[x,y].Colour = "orange";
                        }
                        else if (y == 0)
                        {
                            Grid[x,y].CellType = CellType.EdgeTop;
                            Grid[x,y].Colour = "orange";
                        }
                        else if (y == Grid.GetLength(1)-1)
                        {
                            Grid[x,y].CellType = CellType.EdgeBottom;
                            Grid[x,y].Colour = "orange";
                        }
                    }

                    // Remaining cells should be middle type cells.
                    if (Grid[x,y].CellType == CellType.Unassigned)
                    {
                        Grid[x,y].CellType = CellType.Middle;
                        Grid[x,y].Colour = "green";
                    }
                }
            }
        }

        public void CalculateNeighbours()
        {
            for (int x=0; x < Grid.GetLength(0); x++)
            {
                for (int y=0; y < Grid.GetLength(1); y++)
                {
                    var cell = Grid[x,y];

                    switch (cell.CellType)
                    {
                        case CellType.CornerBottomLeft:
                            cell.NeighbourCells.Add(Grid[x, y-1]);
                            cell.NeighbourCells.Add(Grid[x+1, y-1]);
                            cell.NeighbourCells.Add(Grid[x+1, y]);
                            break;
                        case CellType.CornerTopLeft:
                            cell.NeighbourCells.Add(Grid[x+1, y]);
                            cell.NeighbourCells.Add(Grid[x+1, y+1]);
                            cell.NeighbourCells.Add(Grid[x, y+1]);
                            break;
                        case CellType.CornerTopRight:
                            cell.NeighbourCells.Add(Grid[x-1, y]);
                            cell.NeighbourCells.Add(Grid[x-1, y+1]);
                            cell.NeighbourCells.Add(Grid[x, y+1]);
                            break;
                        case CellType.CornerBottomRight:
                            cell.NeighbourCells.Add(Grid[x, y-1]);
                            cell.NeighbourCells.Add(Grid[x-1, y-1]);
                            cell.NeighbourCells.Add(Grid[x-1, y]);
                            break;
                        case CellType.EdgeTop:
                            cell.NeighbourCells.Add(Grid[x-1, y]);
                            cell.NeighbourCells.Add(Grid[x+1, y]);
                            cell.NeighbourCells.Add(Grid[x-1, y+1]);
                            cell.NeighbourCells.Add(Grid[x, y+1]);
                            cell.NeighbourCells.Add(Grid[x+1, y+1]);
                            break;
                        case CellType.EdgeLeft:
                            cell.NeighbourCells.Add(Grid[x, y-1]);
                            cell.NeighbourCells.Add(Grid[x, y+1]);
                            cell.NeighbourCells.Add(Grid[x+1, y]);
                            cell.NeighbourCells.Add(Grid[x+1, y-1]);
                            cell.NeighbourCells.Add(Grid[x+1, y+1]);
                            break;
                        case CellType.EdgeRight:
                            cell.NeighbourCells.Add(Grid[x, y-1]);
                            cell.NeighbourCells.Add(Grid[x, y+1]);
                            cell.NeighbourCells.Add(Grid[x-1, y]);
                            cell.NeighbourCells.Add(Grid[x-1, y-1]);
                            cell.NeighbourCells.Add(Grid[x-1, y+1]);
                            break;
                        case CellType.EdgeBottom:
                            cell.NeighbourCells.Add(Grid[x-1, y]);
                            cell.NeighbourCells.Add(Grid[x+1, y]);
                            cell.NeighbourCells.Add(Grid[x-1, y-1]);
                            cell.NeighbourCells.Add(Grid[x, y-1]);
                            cell.NeighbourCells.Add(Grid[x+1, y-1]);
                            break;
                        case CellType.Middle:
                            cell.NeighbourCells.Add(Grid[x, y-1]);
                            cell.NeighbourCells.Add(Grid[x, y+1]);
                            cell.NeighbourCells.Add(Grid[x-1, y]);
                            cell.NeighbourCells.Add(Grid[x+1, y]);
                            cell.NeighbourCells.Add(Grid[x-1, y-1]);
                            cell.NeighbourCells.Add(Grid[x+1, y-1]);
                            cell.NeighbourCells.Add(Grid[x-1, y+1]);
                            cell.NeighbourCells.Add(Grid[x+1, y-1]);
                            break;
                    }
                    
                    Grid[x,y] = new Cell(x,y);
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
            this.CellType = CellType.Unassigned;
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