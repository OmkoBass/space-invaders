namespace Space_Invaders.Models
{
    internal class EnemyRegular : EnemyBase
    {
        public EnemyRegular(Point Position, Bitmap Sprite) : base(Position, Sprite) { }

        internal override void Die()
        {
            this.IsDead = true;
        }
    }
}
