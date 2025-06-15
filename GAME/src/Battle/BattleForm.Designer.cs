namespace WindowsFormsApp1
{
    partial class BattleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleForm));
            this.Player1Box = new System.Windows.Forms.PictureBox();
            this.Player2Box = new System.Windows.Forms.PictureBox();
            this.Player1Name = new System.Windows.Forms.Label();
            this.Player2Name = new System.Windows.Forms.Label();
            this.LevelLabel1 = new System.Windows.Forms.Label();
            this.HpLabel1 = new System.Windows.Forms.Label();
            this.AttackLabel1 = new System.Windows.Forms.Label();
            this.LevelLabel2 = new System.Windows.Forms.Label();
            this.HpLabel2 = new System.Windows.Forms.Label();
            this.AttackLabel2 = new System.Windows.Forms.Label();
            this.Player1Level = new System.Windows.Forms.Label();
            this.Player2Level = new System.Windows.Forms.Label();
            this.Player1Hp = new System.Windows.Forms.Label();
            this.Player1Attack = new System.Windows.Forms.Label();
            this.Player2Hp = new System.Windows.Forms.Label();
            this.Player2Attack = new System.Windows.Forms.Label();
            this.Player1Character = new System.Windows.Forms.PictureBox();
            this.Player2Character = new System.Windows.Forms.PictureBox();
            this.AttackBox = new System.Windows.Forms.PictureBox();
            this.AttackButton = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Player1Box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player2Box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player1Character)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player2Character)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttackBox)).BeginInit();
            this.SuspendLayout();
            // 
            // Player1Box
            // 
            this.Player1Box.BackColor = System.Drawing.Color.Transparent;
            this.Player1Box.Image = global::WindowsFormsApp1.Properties.Resources.GameBox;
            this.Player1Box.Location = new System.Drawing.Point(15, 14);
            this.Player1Box.Margin = new System.Windows.Forms.Padding(4);
            this.Player1Box.Name = "Player1Box";
            this.Player1Box.Size = new System.Drawing.Size(264, 191);
            this.Player1Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Player1Box.TabIndex = 0;
            this.Player1Box.TabStop = false;
            // 
            // Player2Box
            // 
            this.Player2Box.BackColor = System.Drawing.Color.Transparent;
            this.Player2Box.Image = global::WindowsFormsApp1.Properties.Resources.GameBox;
            this.Player2Box.Location = new System.Drawing.Point(721, 14);
            this.Player2Box.Margin = new System.Windows.Forms.Padding(4);
            this.Player2Box.Name = "Player2Box";
            this.Player2Box.Size = new System.Drawing.Size(264, 191);
            this.Player2Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Player2Box.TabIndex = 1;
            this.Player2Box.TabStop = false;
            // 
            // Player1Name
            // 
            this.Player1Name.AutoSize = true;
            this.Player1Name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.Player1Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Player1Name.ForeColor = System.Drawing.Color.White;
            this.Player1Name.Location = new System.Drawing.Point(39, 38);
            this.Player1Name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Player1Name.Name = "Player1Name";
            this.Player1Name.Size = new System.Drawing.Size(102, 29);
            this.Player1Name.TabIndex = 2;
            this.Player1Name.Text = "플레이어1";
            // 
            // Player2Name
            // 
            this.Player2Name.AutoSize = true;
            this.Player2Name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.Player2Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Player2Name.ForeColor = System.Drawing.Color.White;
            this.Player2Name.Location = new System.Drawing.Point(745, 38);
            this.Player2Name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Player2Name.Name = "Player2Name";
            this.Player2Name.Size = new System.Drawing.Size(102, 29);
            this.Player2Name.TabIndex = 3;
            this.Player2Name.Text = "플레이어2";
            // 
            // LevelLabel1
            // 
            this.LevelLabel1.AutoSize = true;
            this.LevelLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.LevelLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LevelLabel1.ForeColor = System.Drawing.Color.White;
            this.LevelLabel1.Location = new System.Drawing.Point(40, 97);
            this.LevelLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LevelLabel1.Name = "LevelLabel1";
            this.LevelLabel1.Size = new System.Drawing.Size(41, 25);
            this.LevelLabel1.TabIndex = 4;
            this.LevelLabel1.Text = "Lv.";
            // 
            // HpLabel1
            // 
            this.HpLabel1.AutoSize = true;
            this.HpLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.HpLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.HpLabel1.ForeColor = System.Drawing.Color.White;
            this.HpLabel1.Location = new System.Drawing.Point(40, 125);
            this.HpLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.HpLabel1.Name = "HpLabel1";
            this.HpLabel1.Size = new System.Drawing.Size(56, 25);
            this.HpLabel1.TabIndex = 5;
            this.HpLabel1.Text = "체력 :";
            // 
            // AttackLabel1
            // 
            this.AttackLabel1.AutoSize = true;
            this.AttackLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.AttackLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.AttackLabel1.ForeColor = System.Drawing.Color.White;
            this.AttackLabel1.Location = new System.Drawing.Point(40, 152);
            this.AttackLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.AttackLabel1.Name = "AttackLabel1";
            this.AttackLabel1.Size = new System.Drawing.Size(72, 25);
            this.AttackLabel1.TabIndex = 6;
            this.AttackLabel1.Text = "공격력 :";
            // 
            // LevelLabel2
            // 
            this.LevelLabel2.AutoSize = true;
            this.LevelLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.LevelLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LevelLabel2.ForeColor = System.Drawing.Color.White;
            this.LevelLabel2.Location = new System.Drawing.Point(746, 97);
            this.LevelLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LevelLabel2.Name = "LevelLabel2";
            this.LevelLabel2.Size = new System.Drawing.Size(41, 25);
            this.LevelLabel2.TabIndex = 7;
            this.LevelLabel2.Text = "Lv.";
            // 
            // HpLabel2
            // 
            this.HpLabel2.AutoSize = true;
            this.HpLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.HpLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.HpLabel2.ForeColor = System.Drawing.Color.White;
            this.HpLabel2.Location = new System.Drawing.Point(746, 125);
            this.HpLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.HpLabel2.Name = "HpLabel2";
            this.HpLabel2.Size = new System.Drawing.Size(56, 25);
            this.HpLabel2.TabIndex = 8;
            this.HpLabel2.Text = "체력 :";
            // 
            // AttackLabel2
            // 
            this.AttackLabel2.AutoSize = true;
            this.AttackLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.AttackLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.AttackLabel2.ForeColor = System.Drawing.Color.White;
            this.AttackLabel2.Location = new System.Drawing.Point(746, 152);
            this.AttackLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.AttackLabel2.Name = "AttackLabel2";
            this.AttackLabel2.Size = new System.Drawing.Size(72, 25);
            this.AttackLabel2.TabIndex = 9;
            this.AttackLabel2.Text = "공격력 :";
            // 
            // Player1Level
            // 
            this.Player1Level.AutoSize = true;
            this.Player1Level.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.Player1Level.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Player1Level.ForeColor = System.Drawing.Color.White;
            this.Player1Level.Location = new System.Drawing.Point(94, 97);
            this.Player1Level.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Player1Level.Name = "Player1Level";
            this.Player1Level.Size = new System.Drawing.Size(24, 25);
            this.Player1Level.TabIndex = 10;
            this.Player1Level.Text = "1";
            // 
            // Player2Level
            // 
            this.Player2Level.AutoSize = true;
            this.Player2Level.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.Player2Level.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Player2Level.ForeColor = System.Drawing.Color.White;
            this.Player2Level.Location = new System.Drawing.Point(800, 97);
            this.Player2Level.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Player2Level.Name = "Player2Level";
            this.Player2Level.Size = new System.Drawing.Size(24, 25);
            this.Player2Level.TabIndex = 11;
            this.Player2Level.Text = "1";
            // 
            // Player1Hp
            // 
            this.Player1Hp.AutoSize = true;
            this.Player1Hp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.Player1Hp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Player1Hp.ForeColor = System.Drawing.Color.White;
            this.Player1Hp.Location = new System.Drawing.Point(116, 125);
            this.Player1Hp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Player1Hp.Name = "Player1Hp";
            this.Player1Hp.Size = new System.Drawing.Size(48, 25);
            this.Player1Hp.TabIndex = 12;
            this.Player1Hp.Text = "100";
            // 
            // Player1Attack
            // 
            this.Player1Attack.AutoSize = true;
            this.Player1Attack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.Player1Attack.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Player1Attack.ForeColor = System.Drawing.Color.White;
            this.Player1Attack.Location = new System.Drawing.Point(140, 152);
            this.Player1Attack.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Player1Attack.Name = "Player1Attack";
            this.Player1Attack.Size = new System.Drawing.Size(48, 25);
            this.Player1Attack.TabIndex = 13;
            this.Player1Attack.Text = "100";
            // 
            // Player2Hp
            // 
            this.Player2Hp.AutoSize = true;
            this.Player2Hp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.Player2Hp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Player2Hp.ForeColor = System.Drawing.Color.White;
            this.Player2Hp.Location = new System.Drawing.Point(825, 125);
            this.Player2Hp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Player2Hp.Name = "Player2Hp";
            this.Player2Hp.Size = new System.Drawing.Size(48, 25);
            this.Player2Hp.TabIndex = 14;
            this.Player2Hp.Text = "100";
            // 
            // Player2Attack
            // 
            this.Player2Attack.AutoSize = true;
            this.Player2Attack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.Player2Attack.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Player2Attack.ForeColor = System.Drawing.Color.White;
            this.Player2Attack.Location = new System.Drawing.Point(846, 152);
            this.Player2Attack.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Player2Attack.Name = "Player2Attack";
            this.Player2Attack.Size = new System.Drawing.Size(48, 25);
            this.Player2Attack.TabIndex = 15;
            this.Player2Attack.Text = "100";
            // 
            // Player1Character
            // 
            this.Player1Character.BackColor = System.Drawing.Color.Transparent;
            this.Player1Character.Image = ((System.Drawing.Image)(resources.GetObject("Player1Character.Image")));
            this.Player1Character.Location = new System.Drawing.Point(175, 281);
            this.Player1Character.Margin = new System.Windows.Forms.Padding(4);
            this.Player1Character.Name = "Player1Character";
            this.Player1Character.Size = new System.Drawing.Size(196, 188);
            this.Player1Character.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Player1Character.TabIndex = 16;
            this.Player1Character.TabStop = false;
            // 
            // Player2Character
            // 
            this.Player2Character.BackColor = System.Drawing.Color.Transparent;
            this.Player2Character.Location = new System.Drawing.Point(642, 281);
            this.Player2Character.Margin = new System.Windows.Forms.Padding(4);
            this.Player2Character.Name = "Player2Character";
            this.Player2Character.Size = new System.Drawing.Size(196, 188);
            this.Player2Character.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Player2Character.TabIndex = 17;
            this.Player2Character.TabStop = false;
            // 
            // AttackBox
            // 
            this.AttackBox.BackColor = System.Drawing.Color.Transparent;
            this.AttackBox.Image = global::WindowsFormsApp1.Properties.Resources.GameBox;
            this.AttackBox.Location = new System.Drawing.Point(416, 511);
            this.AttackBox.Margin = new System.Windows.Forms.Padding(4);
            this.AttackBox.Name = "AttackBox";
            this.AttackBox.Size = new System.Drawing.Size(164, 119);
            this.AttackBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AttackBox.TabIndex = 18;
            this.AttackBox.TabStop = false;
            // 
            // AttackButton
            // 
            this.AttackButton.AutoSize = true;
            this.AttackButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.AttackButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AttackButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.AttackButton.ForeColor = System.Drawing.Color.White;
            this.AttackButton.Location = new System.Drawing.Point(441, 554);
            this.AttackButton.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.AttackButton.Name = "AttackButton";
            this.AttackButton.Size = new System.Drawing.Size(89, 29);
            this.AttackButton.TabIndex = 19;
            this.AttackButton.Text = "공격하기";
            this.AttackButton.Click += new System.EventHandler(this.AttackButton_Click);
            // 
            // BattleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1000, 683);
            this.Controls.Add(this.AttackButton);
            this.Controls.Add(this.AttackBox);
            this.Controls.Add(this.Player2Character);
            this.Controls.Add(this.Player1Character);
            this.Controls.Add(this.Player2Attack);
            this.Controls.Add(this.Player2Hp);
            this.Controls.Add(this.Player1Attack);
            this.Controls.Add(this.Player1Hp);
            this.Controls.Add(this.Player2Level);
            this.Controls.Add(this.Player1Level);
            this.Controls.Add(this.AttackLabel2);
            this.Controls.Add(this.HpLabel2);
            this.Controls.Add(this.LevelLabel2);
            this.Controls.Add(this.AttackLabel1);
            this.Controls.Add(this.HpLabel1);
            this.Controls.Add(this.LevelLabel1);
            this.Controls.Add(this.Player2Name);
            this.Controls.Add(this.Player1Name);
            this.Controls.Add(this.Player2Box);
            this.Controls.Add(this.Player1Box);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "BattleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MonsterBattleForm";
            ((System.ComponentModel.ISupportInitialize)(this.Player1Box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player2Box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player1Character)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player2Character)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttackBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Player1Box;
        private System.Windows.Forms.PictureBox Player2Box;
        private System.Windows.Forms.Label Player1Name;
        private System.Windows.Forms.Label Player2Name;
        private System.Windows.Forms.Label LevelLabel1;
        private System.Windows.Forms.Label HpLabel1;
        private System.Windows.Forms.Label AttackLabel1;
        private System.Windows.Forms.Label LevelLabel2;
        private System.Windows.Forms.Label HpLabel2;
        private System.Windows.Forms.Label AttackLabel2;
        private System.Windows.Forms.Label Player1Level;
        private System.Windows.Forms.Label Player2Level;
        private System.Windows.Forms.Label Player1Hp;
        private System.Windows.Forms.Label Player1Attack;
        private System.Windows.Forms.Label Player2Hp;
        private System.Windows.Forms.Label Player2Attack;
        private System.Windows.Forms.PictureBox Player1Character;
        private System.Windows.Forms.PictureBox Player2Character;
        private System.Windows.Forms.PictureBox AttackBox;
        private System.Windows.Forms.Label AttackButton;
    }
}