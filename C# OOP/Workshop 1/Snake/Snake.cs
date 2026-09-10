using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Snake
    {
        public Snake(int startLeft, int startTop, int length)
        {
            this.Body = new Queue<Point>();
            this.Direction = Direction.Right;

            this.Head = new Point(startLeft, startTop);
            
            for (int i = startLeft - length + 1; i < startLeft; i++)
            {
                Point tailPoint = new Point(i, startTop);
                this.Body.Enqueue(tailPoint);
            }

            this.Body.Enqueue(this.Head);
        }

        public Point Head { get; private set; }

        public Queue<Point> Body { get; private set; }

        public Direction Direction { get; private set; }

        public Point Move()
        {
            Point removedBodyPart = this.Body.Dequeue();

            this.SetNewHead();

            this.Body.Enqueue(this.Head);

            return removedBodyPart;
        }

        public void ChangeDirection(Direction direction)
        {
            if (this.Direction == Direction.Left && direction == Direction.Right)
            {
                return;
            }
            if (this.Direction == Direction.Right && direction == Direction.Left)
            {
                return;
            }
            if (this.Direction == Direction.Up && direction == Direction.Down)
            {
                return;
            }
            if (this.Direction == Direction.Down && direction == Direction.Up)
            {
                return;
            }

            this.Direction = direction;
        }

        private Point GetDirectionPoint()
        {
            if (this.Direction == Direction.Right)
            {
                return new Point(2, 0);
            }
            else if (this.Direction == Direction.Left)
            {
                return new Point(-2, 0);
            }
            else if (this.Direction == Direction.Up)
            {
                return new Point(0, -1);
            }
            else if (this.Direction == Direction.Down)
            {
                return new Point(0, 1);
            }

            throw new InvalidOperationException($"Invalid Direction: {this.Direction}");
        }

        private void SetNewHead()
        {
            Point directionPoint = this.GetDirectionPoint();

            int newHeadLeft = this.Head.Left + directionPoint.Left;
            int newHeadTop = this.Head.Top + directionPoint.Top;

            this.Head = new Point(newHeadLeft, newHeadTop);
        }

        public void IncreaseLength()
        {
            this.SetNewHead();

            this.Body.Enqueue(this.Head);
        }
    }
}
