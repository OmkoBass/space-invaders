using Space_Invaders.Models;
using System.Drawing.Drawing2D;

namespace Space_Invaders
{
    public partial class Game : Form
    {
        Player Player;
        Buff Buff;

        readonly EnemyBase[,] enemies = new EnemyBase[3, 20];
        MoveDirection enemyDirection = MoveDirection.Left;

        Keys? key;
        readonly Bitmap backgroundSprite = Resources.background;

        bool gameOver = false;

        uint score = 0;

        private int shakeScreen = 2;
        private int shakeAngle = 2;

        private readonly Pen whitePen = new Pen(Color.White, 4);
        private readonly Pen yellowPen = new Pen(Color.Yellow, 8);
        private readonly Pen greenPen = new Pen(Color.Green, 8);
        private readonly Pen redPen = new Pen(Color.Red, 8);
        private readonly Pen purplePen = new Pen(Color.Purple, 8);

        Effect effect1 = new Effect(new Point(25, 300));
        Effect effect2 = new Effect(new Point(700, 350));
        Effect effect3 = new Effect(new Point(25, 350));
        Effect effect4 = new Effect(new Point(700, 300));

        private int stringTimer = 4;

        string animationsText = "These are animations";
        Font font = new Font("Times New Roman", 18);

        public Game()
        {
            InitializeComponent();

            InitializeValues();
        }

        private void InitializeValues()
        {
            score = 0;
            LabelScore.Text = $"Score: {score}";
            gameOver = false;

            Player = new Player(new Point(0, 0), new Size(48, 48));
            Buff = new Buff(new Point(0, 0));

            whitePen.StartCap = LineCap.RoundAnchor;
            whitePen.EndCap = LineCap.ArrowAnchor;

            // Give the player the bottom left position
            Player.Position = new Point(GameArea.Left, GameArea.Height - (Player.Size.Height * 2));

            Buff.Position = new Point(GameArea.Left + GameArea.Width / 2, GameArea.Height - (Player.Size.Height * 4));
            
            // Walk through all the enemies and give them
            // starting positions

            var resourceManager = Resources.ResourceManager;
            Random random = new Random();

            for (int i = 0; i < enemies.GetLength(0); i++)
            {
                // random.Next(4) because i have 3 enemy sprites
                // to choose from
                int randomNumber = random.Next(1, 4);
                Bitmap? enemySprite = resourceManager.GetObject($"enemy{randomNumber}") as Bitmap;

                if (enemySprite == null)
                {
                    enemySprite = Resources.enemy2;
                }

                for (int j = 0; j < enemies.GetLength(1); j++)
                {
                    int randomBomb = random.Next(1, 20);

                    // 32 is the size of the enemy + offset
                    // 50 is the offset so that the enemies
                    // are centered
                    if (randomBomb == 1)
                    {
                        // I need to find the nighbours after this loop
                        // because all the enemies need to be
                        // initialized before adding them

                        List<EnemyBase> neighbours = new List<EnemyBase>();

                        enemies[i, j] = new EnemyBomb(new Point(50 + (j * 32), i * 32), neighbours);
                    }
                    else
                    {
                        enemies[i, j] = new EnemyRegular(new Point(50 + (j * 32), i * 32), enemySprite);
                    }
                }
            }

            for (int i = 0; i < enemies.GetLength(0); i++)
            {
                for (int j = 0; j < enemies.GetLength(1); j++)
                {
                    if (enemies[i, j].GetType() == typeof(EnemyBomb))
                    {
                        EnemyBomb enemyBomb = (EnemyBomb)enemies[i, j];

                        List<EnemyBase> neighbours = new List<EnemyBase>();

                        if (i == 0)
                        {
                            neighbours.Add(enemies[i + 1, j]);
                        }
                        else if (i == enemies.GetLength(0) - 1)
                        {
                            neighbours.Add(enemies[i - 1, j]);
                        }
                        else
                        {
                            neighbours.Add(enemies[i + 1, j]);
                            neighbours.Add(enemies[i - 1, j]);
                        }

                        if (j == 0)
                        {
                            neighbours.Add(enemies[i, j + 1]);
                        }
                        else if (j == enemies.GetLength(1) - 1)
                        {
                            neighbours.Add(enemies[i, j - 1]);
                        }
                        else
                        {
                            neighbours.Add(enemies[i, j + 1]);
                            neighbours.Add(enemies[i, j - 1]);
                        }

                        enemyBomb.Neighbours = neighbours;
                    }
                }
            }

            ButtonRestart.TabStop = false;
            this.KeyPreview = true;
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
            switch (key)
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
            Graphics g = e.Graphics;

            g.DrawImage(backgroundSprite, new Rectangle(0, 0, 760, 537));

            Player.Draw(g);

            Buff.Draw(g);

            effect1.Draw(g, yellowPen);
            effect2.Draw(g, greenPen);
            effect3.Draw(g, redPen);
            effect4.Draw(g, purplePen);

            if (stringTimer > 0)
            {
                // Hard coded the positions
                // of the arrows and text
                g.DrawLine(whitePen, new Point(GameArea.Width / 2 - 200, 315), new Point(GameArea.Width / 2 - 300, 315));
                g.DrawLine(whitePen, new Point(GameArea.Width / 2 + 125, 315), new Point(GameArea.Width / 2 + 225, 315));
                g.DrawString(animationsText, font, Brushes.White, new Point(GameArea.Width / 2 - 150, 300));
            }

            if (shakeScreen == 2)
            {
                g.RotateTransform(shakeAngle);
            }
            else if (shakeScreen == 1)
            {
                g.RotateTransform(-shakeAngle);
            }

            foreach (EnemyBase enemy in enemies)
            {
                enemy.Draw(g);
            }

            g.ResetTransform();
        }

        private void MoveEnemies()
        {
            foreach (EnemyBase enemy in enemies)
            {
                enemy.Move(enemyDirection);
            }
        }

        private void MoveBuff()
        {
            Buff.Move();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            ControlPlayer();
            MoveBuff();

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
            if(stringTimer > 0)
            {
                stringTimer--;
            }

            if (shakeScreen == 0)
            {
                shakeScreen = 2;
            } else {
                shakeScreen--;
            }

            if (gameOver == true)
            {
                StopTimers();

                MessageBox.Show("Game Over!");
            }

            ChangeEnemyDirection();
            MoveEnemies();

            foreach (Projectile p in Player.projectiles)
            {
                p.OutOfBounds();
            }

            // Check if the enemies came too close
            // if they are we're dead
            if (enemies[enemies.GetLength(0) - 1, 0].Position.Y >= 400)
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
                if (projectile.Fired)
                {
                    foreach (EnemyBase enemy in enemies)
                    {
                        if (enemy.IsDead == true)
                        {
                            continue;
                        }

                        if (projectile.CheckHit(enemy))
                        {
                            projectile.Stop();
                            enemy.Die();

                            LabelScore.Text = $"Score: {++score}";
                        }
                    }

                    if (projectile.CheckHit(Buff))
                    {
                        projectile.Buff();
                    }
                }
            }
        }

        private bool CheckVictory()
        {
            foreach (EnemyBase enemy in enemies)
            {
                if (!enemy.IsDead)
                {
                    return false;
                }
            }

            return true;
        }

        private void CollisionTimer_Tick(object sender, EventArgs e)
        {
            CheckCollisions();

            if (CheckVictory() == true)
            {
                StopTimers();

                MessageBox.Show("You win!");
            }
        }

        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            key = e.KeyCode;
        }

        private void ButtonRestart_Click_1(object sender, EventArgs e)
        {
            InitializeValues();
        }
    }
}