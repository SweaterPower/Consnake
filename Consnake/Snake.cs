using System;
using System.Collections.Generic;
using System.Text;

namespace Consnake
{
    class Snake
    {
        int length;
        SnakeNode headNode;
        SnakeNode tailNode;

        internal int Length { get => length; set => length = value; }
        internal SnakeNode HeadNode { get => headNode; set => headNode = value; }
        internal SnakeNode TailNode { get => tailNode; set => tailNode = value; }

        public void Move(Direction direction)
        {
            this.HeadNode.Move(direction);
            SnakeNode node = this.TailNode;
            while (node.Next != null)
            {
                SnakeNode next = node.Next;
                node.Move(node.Direction);
                node.Direction = next.Direction;
                node = next;
            }
        }

        public void AddNode()
        {
            SnakeNode tailTip = new SnakeNode(this.TailNode, Direction.None);
            tailTip.Point = this.TailNode.Point;
            this.TailNode.Previous = tailTip;
            this.TailNode = tailTip;
        }
    }
}
