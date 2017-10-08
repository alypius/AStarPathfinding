using System;
using System.Collections.Generic;
using Algorithms.DataStructures;

namespace Algorithms.Pathfinding
{
    public class AStar
    {
        public static void Test(int gridSize = 25, double obstaclePercentage = 0.2)
        {
            AStar.Run(new PathfindingGrid(gridSize, gridSize, obstaclePercentage));
        }

        private static void Run(PathfindingGrid grid)
        {
            var initCell = grid.GetRandomEmptyCell();
            var goalCell = grid.GetRandomEmptyCell();

            if (initCell == null || goalCell == null)
            {
                Console.Write(grid.ToString());
                Console.WriteLine("Error: unable to find open cells. Consider choosing a lower obstacle percentage.");
                return;
            }

            var path = GetPath(grid, initCell, goalCell);
            if (path != null)
            {
                foreach (var i in path)
                    grid.SetCellState(i.GetPositionTuple(), PathfindingCellState.Path);

                grid.SetCellState(initCell.GetPositionTuple(), PathfindingCellState.Init);
                grid.SetCellState(goalCell.GetPositionTuple(), PathfindingCellState.Goal);

                Console.Write(grid.ToString());
                Console.WriteLine("Path Length: {0}", Math.Round(path.Distance, 2));
                Console.WriteLine("Straight line distance: {0}", Math.Round(initCell.GetDistance(goalCell), 2));
            }
            else
            {
                Run(grid);
            }
        }

        private static Path<PathfindingCell> GetPath(PathfindingGrid grid, PathfindingCell startCell, PathfindingCell goalCell)
        {
            var positionsExplored = new Dictionary<Tuple<int, int>, bool>() { { startCell.GetPositionTuple(), true } };

            var pathsToExplore = new PriorityQueue<Path<PathfindingCell>>();
            pathsToExplore.Enqueue(new Path<PathfindingCell>(goalCell).Add(startCell));

            while (pathsToExplore.Count > 0)
            {
                var currentPath = pathsToExplore.Dequeue();
                if (currentPath.GoalReached())
                    return currentPath;

                foreach (var e in grid.GetEmptyNeighbors(currentPath.GetLastCell()))
                {
                    if (!positionsExplored.ContainsKey(e.GetPositionTuple()))
                    {
                        positionsExplored.Add(e.GetPositionTuple(), true);
                        pathsToExplore.Enqueue(currentPath.Clone().Add(e));
                    }
                }
            }

            return null;
        }
    }
}
