namespace Space_Invaders.Models
{
    internal class EnemyBomb : EnemyBase
    {
        internal List<EnemyBase> Neighbours { get; set; }
        public EnemyBomb(Point Position, List<EnemyBase> neighbours) : base(Position, null)
        {
            Neighbours = neighbours;
        }

        internal override void Die()
        {
            this.IsDead = true;

            foreach (var enemy in this.Neighbours)
            {
                if(enemy != null && !enemy.IsDead)
                {
                    enemy.Die();
                }
            }
        }
    }
}
