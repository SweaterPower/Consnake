using System;
using System.Collections.Generic;
using System.Text;

namespace Consnake
{
    class SnakeNode
    {
        SnakeNode previous;
        SnakeNode next;
        Direction direction;
        Point point;

        public SnakeNode(Direction direction)
        {
            this.Previous = null;
            this.Next = null;
            this.Direction = direction;
        }

        public SnakeNode(SnakeNode next, Direction direction)
        {
            this.Previous = null;
            this.Next = next;
            this.Direction = direction;
        }

        internal SnakeNode Previous { get => previous; set => previous = value; }
        internal SnakeNode Next { get => next; set => next = value; }
        internal Direction Direction { get => direction; set => direction = value; }
        internal Point Point { get => point; set => point = value; }

        public Point GetStepBackPosition()
        {
            Direction opposite = Game.GetOppositeDirection(this.Direction);
            return Game.MakeStepInDirection(this.Point, opposite);
        }

        public void Move(Direction direction)
        {
            this.Direction = direction;
            this.Point = Game.MakeStepInDirection(this.Point, this.Direction);
        }
    }
}
