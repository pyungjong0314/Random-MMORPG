namespace WindowsFormsApp1.Battle
{
    partial class DiceControl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.AttackPanel = new System.Windows.Forms.Panel();
            this.Dice6 = new System.Windows.Forms.PictureBox();
            this.Dice5 = new System.Windows.Forms.PictureBox();
            this.Dice4 = new System.Windows.Forms.PictureBox();
            this.Dice3 = new System.Windows.Forms.PictureBox();
            this.Dice2 = new System.Windows.Forms.PictureBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.ThrowButton = new System.Windows.Forms.Label();
            this.Dice1 = new System.Windows.Forms.PictureBox();
            this.AttackPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dice6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice1)).BeginInit();
            this.SuspendLayout();
            // 
            // AttackPanel
            // 
            this.AttackPanel.BackColor = System.Drawing.Color.Transparent;
            this.AttackPanel.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.GameBox;
            this.AttackPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AttackPanel.Controls.Add(this.Dice6);
            this.AttackPanel.Controls.Add(this.Dice5);
            this.AttackPanel.Controls.Add(this.Dice4);
            this.AttackPanel.Controls.Add(this.Dice3);
            this.AttackPanel.Controls.Add(this.Dice2);
            this.AttackPanel.Controls.Add(this.Label1);
            this.AttackPanel.Controls.Add(this.ThrowButton);
            this.AttackPanel.Controls.Add(this.Dice1);
            this.AttackPanel.Location = new System.Drawing.Point(97, 55);
            this.AttackPanel.Name = "AttackPanel";
            this.AttackPanel.Size = new System.Drawing.Size(330, 251);
            this.AttackPanel.TabIndex = 31;
            this.AttackPanel.Visible = false;
            // 
            // Dice6
            // 
            this.Dice6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.Dice6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Dice6.Image = global::WindowsFormsApp1.Properties.Resources.Dice66;
            this.Dice6.Location = new System.Drawing.Point(210, 118);
            this.Dice6.Name = "Dice6";
            this.Dice6.Size = new System.Drawing.Size(71, 67);
            this.Dice6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Dice6.TabIndex = 31;
            this.Dice6.TabStop = false;
            // 
            // Dice5
            // 
            this.Dice5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.Dice5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Dice5.Image = global::WindowsFormsApp1.Properties.Resources.Dice55;
            this.Dice5.Location = new System.Drawing.Point(128, 124);
            this.Dice5.Name = "Dice5";
            this.Dice5.Size = new System.Drawing.Size(76, 66);
            this.Dice5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Dice5.TabIndex = 30;
            this.Dice5.TabStop = false;
            // 
            // Dice4
            // 
            this.Dice4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.Dice4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Dice4.Image = global::WindowsFormsApp1.Properties.Resources.Dice44;
            this.Dice4.Location = new System.Drawing.Point(45, 122);
            this.Dice4.Name = "Dice4";
            this.Dice4.Size = new System.Drawing.Size(71, 67);
            this.Dice4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Dice4.TabIndex = 29;
            this.Dice4.TabStop = false;
            // 
            // Dice3
            // 
            this.Dice3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.Dice3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Dice3.Image = global::WindowsFormsApp1.Properties.Resources.Dice33;
            this.Dice3.Location = new System.Drawing.Point(210, 58);
            this.Dice3.Name = "Dice3";
            this.Dice3.Size = new System.Drawing.Size(71, 67);
            this.Dice3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Dice3.TabIndex = 28;
            this.Dice3.TabStop = false;
            // 
            // Dice2
            // 
            this.Dice2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.Dice2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Dice2.Image = global::WindowsFormsApp1.Properties.Resources.Dice22;
            this.Dice2.Location = new System.Drawing.Point(128, 55);
            this.Dice2.Name = "Dice2";
            this.Dice2.Size = new System.Drawing.Size(71, 67);
            this.Dice2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Dice2.TabIndex = 27;
            this.Dice2.TabStop = false;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label1.ForeColor = System.Drawing.Color.White;
            this.Label1.Location = new System.Drawing.Point(82, 20);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(172, 25);
            this.Label1.TabIndex = 21;
            this.Label1.Text = "주사위 눈을 선택하세요";
            // 
            // ThrowButton
            // 
            this.ThrowButton.AutoSize = true;
            this.ThrowButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.ThrowButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ThrowButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ThrowButton.ForeColor = System.Drawing.Color.White;
            this.ThrowButton.Location = new System.Drawing.Point(137, 200);
            this.ThrowButton.Name = "ThrowButton";
            this.ThrowButton.Size = new System.Drawing.Size(57, 25);
            this.ThrowButton.TabIndex = 26;
            this.ThrowButton.Text = "던지기";
            // 
            // Dice1
            // 
            this.Dice1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.Dice1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Dice1.Image = global::WindowsFormsApp1.Properties.Resources.Dice11;
            this.Dice1.Location = new System.Drawing.Point(45, 55);
            this.Dice1.Name = "Dice1";
            this.Dice1.Size = new System.Drawing.Size(71, 67);
            this.Dice1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Dice1.TabIndex = 23;
            this.Dice1.TabStop = false;
            // 
            // DiceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AttackPanel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DiceControl";
            this.Size = new System.Drawing.Size(523, 360);
            this.AttackPanel.ResumeLayout(false);
            this.AttackPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dice6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel AttackPanel;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label ThrowButton;
        private System.Windows.Forms.PictureBox Dice1;
        private System.Windows.Forms.PictureBox Dice4;
        private System.Windows.Forms.PictureBox Dice3;
        private System.Windows.Forms.PictureBox Dice2;
        private System.Windows.Forms.PictureBox Dice6;
        private System.Windows.Forms.PictureBox Dice5;
    }
}
