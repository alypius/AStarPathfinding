using System;
using System.Linq;
using Algorithms.Grid;

namespace Algorithms.Pathfinding
{
    class PathfindingGrid : BaseGrid<PathfindingCell>
    {
        private static Random random = new Random();

        public PathfindingGrid(int rowCount, int colCount, double obstactlePercentage)
            : base(rowCount, colCount, CreateInitialCell(obstactlePercentage)) { }

        public PathfindingCell[] GetEmptyNeighbors(PathfindingCell cell)
        {
            return this.GetNeighbors(cell)
                .Where(it => it.IsEmpty())
                .ToArray();
        }

        public PathfindingCell GetRandomEmptyCell(int maxIterations = 100)
        {
            for (var i = 0; i < maxIterations; i++)
            {
                var cell = this.GetCell(random.Next(this.RowCount), random.Next(this.ColCount));
                if (cell.IsEmpty())
                    return cell;
            }
            return null;
        }

        public void SetCellState(Tuple<int, int> position, PathfindingCellState cellState)
        {
            this.GetCell(position).SetState(cellState);
        }

        protected static Func<int, int, PathfindingCell> CreateInitialCell(double obstaclePercentage)
        {
            return (int row, int col) =>
                new PathfindingCell(row, col, (random.NextDouble() < obstaclePercentage) ? PathfindingCellState.Blocked : PathfindingCellState.Empty);
        }
    }
}
