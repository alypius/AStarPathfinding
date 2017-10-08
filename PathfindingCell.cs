using System;
using Algorithms.Grid;

namespace Algorithms.Pathfinding
{
    enum PathfindingCellState { Blocked, Empty, Goal, Init, Path }

    class PathfindingCell : BaseCell
    {
        private PathfindingCellState _state;

        public PathfindingCell(int row, int col, PathfindingCellState state) : base(row, col)
        {
            this._state = state;
        }

        public override char ToChar()
        {
            switch (this._state)
            {
                case (PathfindingCellState.Blocked):
                    return 'X';
                case PathfindingCellState.Empty:
                    return ' ';
                case PathfindingCellState.Goal:
                    return '*';
                case PathfindingCellState.Init:
                    return '+';
                case PathfindingCellState.Path:
                    return '.';
                default:
                    throw new InvalidOperationException("Unknown cell state");
            }
        }

        public void SetState(PathfindingCellState state)
        {
            this._state = state;
        }

        public bool IsEmpty()
        {
            return this._state == PathfindingCellState.Empty;
        }
    }
}
