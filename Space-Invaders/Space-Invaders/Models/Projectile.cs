namespace Space_Invaders.Models
{
    internal class Projectile
    {
        internal bool Fired { get; set; } = false;
        internal bool IsBuffed { get; set; } = false;
        internal Point Position = new(0, 0);
        internal Size Size = new(4, 8);
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
                if(IsBuffed)
                {
                    this.Size = new Size(12, 24);
                } else
                {
                    this.Size = new Size(4, 8);
                }

                Point newPosition = new(Position.X, Position.Y - velocity);
                Position = newPosition;

                g.FillRectangle(Brushes.Red, new Rectangle(Position, Size));
            }
        }

        internal void Buff()
        {
            // Buffs the bullet, increasing it's size
            this.IsBuffed = true;
        }

        internal void Stop()
        {
            // Stops the bullet and resets it's position
            this.Fired = false;
            this.IsBuffed = false;

            Position = new(Position.X, Position.Y - velocity);
        }

        internal void OutOfBounds()
        {
            if(this.Position.Y < 0)
            {
                this.Stop();
            }
        }

        // Checks hit with enemies and buffs
        internal bool CheckHit(EnemyBase enemy)
        {
            if (this.Position.X < enemy.Position.X + enemy.Size.Width &&
                this.Position.X + this.Size.Width > enemy.Position.X &&
                this.Position.Y < enemy.Position.Y + enemy.Size.Height &&
                this.Size.Height + this.Position.Y > enemy.Position.Y)
            {
                return true;
            }

            return false;
        }

        internal bool CheckHit(Buff buff)
        {
            if (this.Position.X < buff.Position.X + buff.Size.Width &&
                this.Position.X + this.Size.Width > buff.Position.X &&
                this.Position.Y < buff.Position.Y + buff.Size.Height &&
                this.Size.Height + this.Position.Y > buff.Position.Y)
            {
                return true;
            }

            return false;
        }
    }
}
