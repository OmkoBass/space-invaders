namespace Space_Invaders.Models
{
    internal class Buff
    {
        // If i wanted to have multiple buffs
        // i would implement this in another,
        // smarter way
        internal Point Position { get; set; }

        internal Size Size = new(48, 48);
        internal bool IsPickedUp { get; set; } = false;

        internal Bitmap Sprite { get; } = Resources.bulletUpgrade;

        private readonly int velocity = 12;

        internal Buff(Point Position)
        {
            this.Position = Position;
        }

        internal void Move(MoveDirection MoveDirection)
        {
            if (MoveDirection == MoveDirection.Left)
            {
                Position = new Point(Position.X - velocity - 4, Position.Y);
            }
            else
            {
                Position = new Point(Position.X + velocity + 4, Position.Y);
            }
        }

        internal void Move()
        {
            if(this.Position.X < 0 - this.Size.Width * 2)
            {
                // 800 should be window size
                // but i'm too lazy to do it properly
                // or use proper patterns for this
                this.Position = new Point(800, Position.Y);
            } else
            {
                this.Position = new Point(Position.X - velocity, Position.Y);
            }
        }

        internal void Draw(Graphics g)
        {
            if (IsPickedUp == true)
            {
                return;
            }

            g.DrawImage(this.Sprite, new Rectangle(Position, Size));
        }
    }
}
