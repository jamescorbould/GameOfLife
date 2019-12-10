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
        public Board Board { get; set; }

        public Game(Board board)
        {
            Board = board;
            Board.Print();
            //Board.PrintCoords();
            Console.ReadKey();

            while (this.NextGen())
            {
                Console.Clear();
                Board.Print();

                int aliveCount = 0;

                for (int x=0; x < board.Grid.GetLength(0); x++)
                {
                    for (int y=0; y < board.Grid.GetLength(1); y++)
                    {
                        if (board.Grid[x,y].IsCurrentlyAlive)
                        {
                            aliveCount++;
                        }
                    }
                }

                if (aliveCount == 0)
                {
                    Console.WriteLine("Game Over");
                    break;
                }

                AssignNextGen();
                Console.ReadKey();
            }

            Console.ReadKey();
        }

        public void PrintMenu()
        {

        }

        public bool NextGen()
        {
            // Work out the next generation.
            // Loop through the cells and check if each cell should be alive or dead in the next gen,
            // depending on the cells current state and that of it's neighbours.

            // GOL rules:
            // ==========
            // Any live cell with fewer than two live neighbours dies (referred to as underpopulation or exposure).
            // Any live cell with more than three live neighbours dies (referred to as overpopulation or overcrowding).
            // Any live cell with two or three live neighbours lives, unchanged, to the next generation.
            // Any dead cell with exactly three live neighbours will come to life.

            var grid = Board.Grid;

            for (int x=0; x < grid.GetLength(0); x++)
            {
                for (int y=0; y < grid.GetLength(1); y++)
                {
                    var cell = grid[x,y];

                    var aliveNeighboursCount = cell.NeighbourCells.FindAll(c => c.IsCurrentlyAlive).Count;

                    if (cell.IsCurrentlyAlive)
                    {
                        if (aliveNeighboursCount < 2)
                        {
                            cell.IsFutureAlive = false;
                        }
                        else if (aliveNeighboursCount > 3)
                        {
                            cell.IsFutureAlive = false;
                        }
                        else if (aliveNeighboursCount == 2 || aliveNeighboursCount == 3)
                        {
                            cell.IsFutureAlive = true;
                        }
                    }
                    else
                    {
                        if (aliveNeighboursCount == 3)
                        {
                            cell.IsFutureAlive = true;   
                        }
                        else
                        {
                            cell.IsFutureAlive = false;
                        }
                    }
                }
            }

            return true;
        }

        public void AssignNextGen()
        {
            for (int x=0; x < Board.Grid.GetLength(0); x++)
            {
                for (int y=0; y < Board.Grid.GetLength(1); y++)
                {
                    var cell = Board.Grid[x,y];
                    cell.IsCurrentlyAlive = cell.IsFutureAlive;
                    cell.IsFutureAlive = false;
                }
            }
        }
    }

    public class Board
    {
        public Cell[,] Grid { get; set; }

        public Board(int sizeX, int sizeY)
        {
            // Given a board of dimensions x cells by y cells, create cells to fill it
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
                    var cell = Grid[x,y];

                    // A corner type cell?
                    if (x + y == 0)
                    {
                        cell.CellType = CellType.CornerTopLeft;
                        cell.Colour = "red";
                    }
                    else if (x == y && x == Grid.GetLength(0)-1 && y == Grid.GetLength(1)-1)
                    {
                        cell.CellType = CellType.CornerBottomRight;
                        cell.Colour = "red";
                    }
                    else if (x == Grid.GetLength(0)-1 && y == 0)
                    { 
                        cell.CellType = CellType.CornerTopRight;
                        cell.Colour = "red";
                    }
                    else if (y == Grid.GetLength(1)-1 && x == 0)
                    {
                        cell.CellType = CellType.CornerBottomLeft;
                        cell.Colour = "red";
                    }

                    // A cell on the edge of the grid?
                    if (cell.CellType == CellType.Unassigned)
                    {
                        if (x == 0)
                        {
                            cell.CellType = CellType.EdgeLeft;
                            cell.Colour = "orange";
                        }
                        else if (x == Grid.GetLength(0)-1)
                        {
                            cell.CellType = CellType.EdgeRight;
                            cell.Colour = "orange";
                        }
                        else if (y == 0)
                        {
                            cell.CellType = CellType.EdgeTop;
                            cell.Colour = "orange";
                        }
                        else if (y == Grid.GetLength(1)-1)
                        {
                            cell.CellType = CellType.EdgeBottom;
                            cell.Colour = "orange";
                        }
                    }

                    // Remaining cells should be middle type cells.
                    if (cell.CellType == CellType.Unassigned)
                    {
                        cell.CellType = CellType.Middle;
                        cell.Colour = "green";
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
                }
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
            this.NeighbourCells = new List<Cell>();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(new Board(30,30));
        }
    }
}