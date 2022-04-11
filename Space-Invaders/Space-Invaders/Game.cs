using Space_Invaders.Models;

namespace Space_Invaders
{
    public partial class Game : Form
    {
        Player Player;

        readonly Enemy[,] enemies = new Enemy[3, 20];
        MoveDirection enemyDirection = MoveDirection.Left;

        Keys? key;
        readonly Bitmap backgroundSprite = Resources.background;

        bool gameOver = false;

        uint score = 0;
        public Game()
        {
            InitializeComponent();

            InitializeValues();
        }

        private void InitializeValues()
        {
            score = 0;
            LabelScore.Text = $"Score: {score}";

            Player = new Player(new Point(0, 0), new Size(48, 48));

            // Give the player the bottom left position
            Player.Position = new Point(GameArea.Left, GameArea.Height - (Player.Size.Height * 2));

            // Walk through all the enemies and give them
            // starting positions

            var resourceManager = Resources.ResourceManager;
            Random random = new Random();

            for (int i = 0; i < enemies.GetLength(0); i++)
            {
                // random.Next(3) because i have 3 enemy sprites
                // to choose from
                int randomNumber = random.Next(1, 4);

                // This should be checked for null values
                for (int j = 0; j < enemies.GetLength(1); j++)
                {
                    int randomBomb = random.Next(1, 20);
                    bool shouldBeBomb = false;

                    if(randomBomb == 1)
                    {
                        shouldBeBomb = true;
                    }

                    // 32 is the size of the enemy + offset
                    // 50 is the offset so that the enemies
                    // are centered
                    enemies[i, j] = new Enemy(new Point(50 + j * 32, i * 32), shouldBeBomb);
                }
            }

            ButtonRestart.TabStop = false;
            GameArea.Focus();

            StartTimers();
        }

        private void StartTimers()
        {
            GameTimer.Start();
            EnemyTimer.Start();
            CollisionTimer.Start();
        }

        private void StopTimers()
        {
            GameTimer.Stop();
            EnemyTimer.Stop();
            CollisionTimer.Stop();
        }

        private void ControlPlayer()
        {
            switch(key)
            {
                case Keys.A:
                    Player.Move(MoveDirection.Left);
                    break;
                case Keys.D:
                    Player.Move(MoveDirection.Right);
                    break;
                case Keys.Space:
                    Player.Shoot();
                    key = null;
                    break;
            }
        }

        private void GameArea_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(backgroundSprite, new Rectangle(0, 0, 760, 537));

            Player.Draw(e.Graphics);

            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(e.Graphics);
            }
        }

        private void MoveEnemies()
        {
            foreach(Enemy enemy in enemies)
            {
                enemy.Move(enemyDirection);
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            ControlPlayer();

            GameArea.Invalidate();
        }

        private void ChangeEnemyDirection()
        {
            // Throw a random number
            // The number determines the direction
            // the enemies are heading
            // we have to check wether the enemies are
            // close to the left or right side
            // if they are, we don't want to move them
            // further that way to avoid them going
            // out of bounds

            Random r = new();

            int random = r.Next(0, 3);

            if (random == 0)
            {
                if (enemies[0, 0].Position.X - 28 <= 0)
                {
                    enemyDirection = MoveDirection.Still;
                    return;
                }

                enemyDirection = MoveDirection.Left;

            }
            else if (random == 1)
            {
                if (enemies[0, enemies.GetLength(1) - 1].Position.X + 28 >= 730)
                {
                    enemyDirection = MoveDirection.Still;
                    return;
                }

                enemyDirection = MoveDirection.Right;
            }
            else
            {
                enemyDirection = MoveDirection.Still;
            }
        }

        private void EnemyTimer_Tick(object sender, EventArgs e)
        {
            if(gameOver == true)
            {
                StopTimers();

                MessageBox.Show("Game Over!");
            }

            ChangeEnemyDirection();
            MoveEnemies();

            // Check if the enemies came too close
            // if they did we're dead
            if(enemies[enemies.GetLength(0) - 1, 0].Position.Y >= 400)
            {
                gameOver = true;
            }
        }

        private void CheckCollisions()
        {
            // Checks collision for each bullet
            // that has been fired
            // bullets in the chamber are not counted
            // each bullet checks if it hits an enemy
            // if it does, they both dissapear
            // and the score increments
            foreach (Projectile projectile in Player.projectiles)
            {
                if(projectile.Fired)
                {
                    for (int i = 0; i < enemies.GetLength(0); i++)
                    {
                        for (int j = 0; j < enemies.GetLength(1); j++)
                        {
                            Enemy enemy = enemies[i, j];

                            if (enemy.IsDead == false)
                            {
                                if (projectile.Position.X >= enemy.Position.X && projectile.Position.X <= enemy.Position.X + enemy.Size.Width)
                                {
                                    if (projectile.Position.Y >= enemy.Position.Y && projectile.Position.Y <= enemy.Position.Y + enemy.Size.Height)
                                    {
                                        projectile.Fired = false;
                                        enemy.IsDead = true;

                                        // If it's a bomb enemy
                                        // kill everyone around him
                                        if(enemy.IsBomb)
                                        {
                                            if(i == 0)
                                            {
                                                enemies[i + 1, j].IsDead = true;
                                            }
                                            else if(i == enemies.GetLength(0) - 1)
                                            {
                                                enemies[i - 1, j].IsDead = true;
                                            }
                                            else
                                            {
                                                enemies[i + 1, j].IsDead = true;
                                                enemies[i - 1, j].IsDead = true;
                                            }

                                            if (j == 0)
                                            {
                                                enemies[i, j + 1].IsDead = true;
                                            }
                                            else if(j == enemies.GetLength(1) - 1)
                                            {
                                                enemies[i, j - 1].IsDead = true;
                                            }
                                            else
                                            {
                                                enemies[i, j + 1].IsDead = true;
                                                enemies[i, j - 1].IsDead = true;
                                            }
                                        }

                                        LabelScore.Text = $"Score: {++score}";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool CheckVictory()
        {
            foreach(Enemy enemy in enemies)
            {
                if(enemy.IsDead == false)
                {
                    return false;
                }
            }

            return true;
        }

        private void CollisionTimer_Tick(object sender, EventArgs e)
        {
            CheckCollisions();

            if(CheckVictory() == true)
            {
                StopTimers();

                MessageBox.Show("You win!");
            }
        }

        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            key = e.KeyCode;
        }

        private void Form1_KeyUp_1(object sender, KeyEventArgs e)
        {
            key = null;
        }

        private void ButtonRestart_Click_1(object sender, EventArgs e)
        {
            InitializeValues();
        }

        private void GameArea_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            key = e.KeyCode;
        }
    }
}