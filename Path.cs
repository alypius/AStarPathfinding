using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Grid;

namespace Algorithms.Pathfinding
{
    class Path<TCell> : IComparable<Path<TCell>>, IEnumerable<TCell> where TCell : class, ICell
    {
        private const int COMPARE_PRECISION = 100;

        private List<TCell> cells = new List<TCell>();
        private double distance = 0;
        private TCell goal;

        public double Distance { get { return this.distance; } }

        public Path(TCell goal) : base()
        {
            this.goal = goal;
        }

        public TCell GetLastCell()
        {
            return this.cells.Count > 0
                ? this.cells.Last()
                : null;
        }

        public double GetCost()
        {
            var lastCell = this.GetLastCell();
            var distanceToGoal = lastCell == null
                ? 0
                : lastCell.GetDistance(goal);
            return this.distance + distanceToGoal;
        }

        public bool GoalReached()
        {
            var lastCell = this.GetLastCell();
            return lastCell == null
                ? false
                : lastCell.GetPositionTuple().Equals(goal.GetPositionTuple());
        }

        public int CompareTo(Path<TCell> o)
        {
            return (int)Math.Floor(COMPARE_PRECISION * (this.GetCost() - o.GetCost()));
        }

        public Path<TCell> Add(TCell cell)
        {
            var lastCell = this.GetLastCell();
            this.cells.Add(cell);
            if (lastCell != null)
                this.distance += lastCell.GetDistance(cell);
            return this;
        }

        public Path<TCell> Clone()
        {
            var clone = new Path<TCell>(goal);
            foreach (var t in this.cells)
            {
                clone.Add(t);
            }
            return clone;
        }

        public IEnumerator<TCell> GetEnumerator()
        {
            return this.cells.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
