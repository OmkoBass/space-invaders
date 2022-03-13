using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders.Models
{
    internal class Enemy
    {
        internal Point Position { get; set; }

        internal bool IsDead { get; set; } = false;

        internal Size Size = new(24, 24);

        private readonly int velocity = 12;

        internal Enemy(Point Position)
        {
            this.Position = Position;
        }

        internal void Move(MoveDirection MoveDirection)
        {
            if(MoveDirection == MoveDirection.Left)
            {
                Position = new Point(Position.X - velocity - 4, Position.Y + velocity);
            } else if(MoveDirection == MoveDirection.Right)
            {
                Position = new Point(Position.X + velocity + 4, Position.Y + velocity);
            } else
            {
                Position = new Point(Position.X, Position.Y + velocity);
            }
        }

        internal void Draw(Graphics g)
        {
            if(IsDead == true)
            {
                return;
            }

            g.FillRectangle(Brushes.Green, new Rectangle(Position, Size));
        }
    }
}
