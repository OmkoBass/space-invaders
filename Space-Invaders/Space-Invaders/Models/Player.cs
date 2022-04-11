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
        internal Bitmap Sprite { get; } = Resources.player;

        private readonly int velocity = 10;

        internal readonly Projectile[] projectiles = new Projectile[20];

        internal Player(Point Position, Size Size)
        {
            this.Position = Position;
            this.Size = Size;
            this.Sprite = Sprite;

            for(int i = 0; i < projectiles.Length; i++)
            {
                projectiles[i] = new Projectile();
            }
        }

        internal void Draw(Graphics g)
        {
            g.DrawImage(this.Sprite, new Rectangle(Position, Size));

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
                    Point fixPointPosition = new Point(this.Position.X + this.Size.Width / 4, this.Position.Y);

                    p.Fire(fixPointPosition);
                    break;
                }
            }
        }
    }
}
