namespace Space_Invaders.Models
{
    abstract internal class EnemyBase
    {
        internal Point Position { get; set; }
        internal Size Size = new(24, 24);
        internal Bitmap Sprite { get; } = Resources.enemy1;
        internal bool IsDead { get; set; } = false;
        private readonly int velocity = 12;

        internal EnemyBase(Point Position, Bitmap? Sprite)
        {
            this.Position = Position;

            if(Sprite == null)
            {
                this.Sprite = Resources.enemyBomb;
            } else
            {
                this.Sprite = Sprite;
            }
        }

        internal virtual void Move(MoveDirection MoveDirection)
        {
            if (MoveDirection == MoveDirection.Left)
            {
                Position = new Point(Position.X - velocity, Position.Y + velocity);
            }
            else if (MoveDirection == MoveDirection.Right)
            {
                Position = new Point(Position.X + velocity, Position.Y + velocity);
            }
            else
            {
                Position = new Point(Position.X, Position.Y + velocity);
            }
        }

        internal void Draw(Graphics g)
        {
            if (IsDead == true)
            {
                return;
            }

            g.DrawImage(this.Sprite, new Rectangle(Position, Size));
        }

        internal abstract void Die();
    }
}
