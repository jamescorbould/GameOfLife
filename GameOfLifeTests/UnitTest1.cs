using System;
using Xunit;
using GameOfLife;
using System.Linq;
using System.Collections.Generic;

namespace GameOfLifeTests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(3, 3)]
        [InlineData(5, 5)]
        [InlineData(9, 9)]
        [InlineData(1000, 1000)]
        public void CornersCorrectlyAssignedTest(int x, int y)
        {
            var board = new Board(x, y);
            Assert.True(board.Grid[0, 0].CellType == CellType.CornerTopLeft);
            Assert.True(board.Grid[x-1, 0].CellType == CellType.CornerTopRight);
            Assert.True(board.Grid[0, y-1].CellType == CellType.CornerBottomLeft);
            Assert.True(board.Grid[x-1, y-1].CellType == CellType.CornerBottomRight);
        }

        [Theory]
        [InlineData(3, 3, 0, 1)] // Edge Left
        public void EdgeLeftCorrectlyAssignedTest(int sizeX, int sizeY, int posX, int posY)
        {
            var board = new Board(sizeX, sizeY);
            Assert.True(board.Grid[posX, posY].CellType == CellType.EdgeLeft);
        }

        [Theory]
        [InlineData(5, 5, 4, 1)] // Edge Right
        public void EdgeRightCorrectlyAssignedTest(int sizeX, int sizeY, int posX, int posY)
        {
            var board = new Board(sizeX, sizeY);
            Assert.True(board.Grid[posX, posY].CellType == CellType.EdgeRight);
        }

        [Theory]
        [InlineData(9, 9, 1, 0)] // Edge Top
        public void EdgeTopCorrectlyAssignedTest(int sizeX, int sizeY, int posX, int posY)
        {
            var board = new Board(sizeX, sizeY);
            Assert.True(board.Grid[posX, posY].CellType == CellType.EdgeTop);
        }

        [Theory]
        [InlineData(1000, 1000, 1, 999)] // Edge Bottom
        public void EdgeBottomCorrectlyAssignedTest(int sizeX, int sizeY, int posX, int posY)
        {
            var board = new Board(sizeX, sizeY);
            Assert.True(board.Grid[posX, posY].CellType == CellType.EdgeBottom);
        }

        [Theory]
        [InlineData(3, 3, 0, 0)]
        public void CheckNeighboursAssignedCorrectly(int sizeX, int sizeY, int posX, int posY)
        {
            // Check that this cell has the correct neighbouring cells assigned to it.
            var board = new Board(sizeX, sizeY);
            var cell = board.Grid[posX, posY];
            var grid = board.Grid;

            var expectedNeighbouringCells = new List<Cell>()
            {
                grid[posX+1, posY],
                grid[posX+1, posY+1],
                grid[posX, posY+1]
            };

            Assert.True(cell.NeighbourCells.OrderBy(i => i.coordinates.x + i.coordinates.y)
                .SequenceEqual(expectedNeighbouringCells.OrderBy(i => i.coordinates.x + i.coordinates.y)));
        }
    }
}