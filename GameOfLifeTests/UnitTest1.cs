using System;
using Xunit;
using GameOfLife;

namespace GameOfLifeTests
{
    public class UnitTest1
    {
        [Fact]
        //[]
        public void CornersCorrectlyAssignedTest()
        {
            var game = new Game(new Board(5,5));
            var grid = game.board.grid;
            
            for (int x=0; x < grid.GetLength(0); x++)
            {
                for (int y=0; y < grid.GetLength(1); y++)
                {
                    if (x == y || (x + y == x && x == Grid.GetLength(0)) || (x + y == y && y == Grid.GetLength(1)))
                    { 
                        var isCorner = grid[x,y].CellType == CellType.Corner ? true : false;
                    }
                }
            }
        }
    }
}
