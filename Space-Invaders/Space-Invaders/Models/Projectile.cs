using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders.Models
{
    internal class Projectile
    {
        internal bool Fired { get; set; } = false;
        internal Point Position = new(0, 0);
        private Size Size = new(4, 8);
        private int velocity = 10;

        internal void Fire(Point PlayerPosition)
        {
            // 24 is the player size
            // 12 is half of it for the offset
            Point newPosition = new(PlayerPosition.X + 9, PlayerPosition.Y - 12);

            Position = newPosition;
            Fired = true;
        }

        internal void Draw(Graphics g)
        {
            if(Fired)
            {
                Point newPosition = new(Position.X, Position.Y - velocity);
                Position = newPosition;

                g.FillRectangle(Brushes.Red, new Rectangle(Position, Size));
            }

            if(Position.Y <= 0 && Fired)
            {
                Fired = false;
            }
        }
    }
}
