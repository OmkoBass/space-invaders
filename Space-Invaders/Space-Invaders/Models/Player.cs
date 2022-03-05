namespace Space_Invaders.Models
{
    internal enum MoveDirection
    {
        Left,
        Right,
        Still
    }

    internal class Player
    {
        internal Point Position { get; set; }
        internal Size Size { get; set; }

        private readonly int velocity = 10;

        internal readonly Projectile[] projectiles = new Projectile[20];

        internal Player(Point Position, Size Size)
        {
            this.Position = Position;
            this.Size = Size;

            for(int i = 0; i < projectiles.Length; i++)
            {
                projectiles[i] = new Projectile();
            }
        }

        internal void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Black, new Rectangle(Position, Size));
            g.FillRectangle(Brushes.Black, new Rectangle(Position.X + Size.Width / 3, Position.Y - Size.Height / 2, Size.Width / 4, Size.Height));

            foreach(Projectile p in projectiles)
            {
                p.Draw(g);
            }
        }

        internal void Move(MoveDirection moveDirection)
        {
            if (moveDirection == MoveDirection.Left)
            {
                if (Position.X <= 10)
                {
                    return;
                }

                Point newPosition = new(Position.X - velocity, Position.Y);
                Position = newPosition;
            } else if(moveDirection == MoveDirection.Right)
            {
                if (Position.X >= 730)
                {
                    return;
                }

                Point newPosition = new(Position.X + velocity, Position.Y);
                Position = newPosition;
            }
        }

        internal void Shoot()
        {
            // Check if the projectile is fired
            // If it isn't fire it
            // If it is then find a new one
            foreach (Projectile p in projectiles)
            {
                if(p.Fired == false)
                {
                    p.Fire(Position);
                    break;
                }
            }
        }
    }
}
