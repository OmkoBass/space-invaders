namespace Space_Invaders
{
    partial class Game
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.EnemyTimer = new System.Windows.Forms.Timer(this.components);
            this.CollisionTimer = new System.Windows.Forms.Timer(this.components);
            this.LabelScore = new System.Windows.Forms.Label();
            this.ButtonRestart = new System.Windows.Forms.Button();
            this.GameArea = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.GameArea)).BeginInit();
            this.SuspendLayout();
            // 
            // GameTimer
            // 
            this.GameTimer.Interval = 66;
            this.GameTimer.Tick += new System.EventHandler(this.GameTimer_Tick);
            // 
            // EnemyTimer
            // 
            this.EnemyTimer.Interval = 1000;
            this.EnemyTimer.Tick += new System.EventHandler(this.EnemyTimer_Tick);
            // 
            // CollisionTimer
            // 
            this.CollisionTimer.Tick += new System.EventHandler(this.CollisionTimer_Tick);
            // 
            // LabelScore
            // 
            this.LabelScore.AutoSize = true;
            this.LabelScore.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LabelScore.ForeColor = System.Drawing.Color.Transparent;
            this.LabelScore.Location = new System.Drawing.Point(789, 12);
            this.LabelScore.Name = "LabelScore";
            this.LabelScore.Size = new System.Drawing.Size(83, 25);
            this.LabelScore.TabIndex = 2;
            this.LabelScore.Text = "Score: 0";
            // 
            // ButtonRestart
            // 
            this.ButtonRestart.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonRestart.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonRestart.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ButtonRestart.Location = new System.Drawing.Point(778, 40);
            this.ButtonRestart.Name = "ButtonRestart";
            this.ButtonRestart.Size = new System.Drawing.Size(99, 44);
            this.ButtonRestart.TabIndex = 3;
            this.ButtonRestart.Text = "Restart";
            this.ButtonRestart.UseVisualStyleBackColor = false;
            this.ButtonRestart.Click += new System.EventHandler(this.ButtonRestart_Click_1);
            // 
            // GameArea
            // 
            this.GameArea.Location = new System.Drawing.Point(11, 12);
            this.GameArea.Name = "GameArea";
            this.GameArea.Size = new System.Drawing.Size(761, 537);
            this.GameArea.TabIndex = 4;
            this.GameArea.TabStop = false;
            this.GameArea.Paint += new System.Windows.Forms.PaintEventHandler(this.GameArea_Paint);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.GameArea);
            this.Controls.Add(this.ButtonRestart);
            this.Controls.Add(this.LabelScore);
            this.Name = "Game";
            this.Text = "Space Invaders";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown_1);
            ((System.ComponentModel.ISupportInitialize)(this.GameArea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.Timer EnemyTimer;
        private System.Windows.Forms.Timer CollisionTimer;
        private Label LabelScore;
        private Button ButtonRestart;
        private PictureBox GameArea;
    }
}