using System;
using Xunit;
using GameOfLife;

namespace GameOfLifeTests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(3, 3)]
        [InlineData(5, 5)]
        [InlineData(9, 9)]
        public void CornersCorrectlyAssignedTest(int x, int y)
        {
            var board = new Board(x, y);
            Assert.True(board.Grid[0, 0].CellType == CellType.CornerTopLeft);
            Assert.True(board.Grid[x-1, 0].CellType == CellType.CornerTopRight);
            Assert.True(board.Grid[0, y-1].CellType == CellType.CornerBottomLeft);
            Assert.True(board.Grid[x-1, y-1].CellType == CellType.CornerBottomRight);
        }
    }
}