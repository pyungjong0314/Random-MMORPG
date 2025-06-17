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
            this.DicePanel = new System.Windows.Forms.Panel();
            this.Label1 = new System.Windows.Forms.Label();
            this.ThrowButton = new System.Windows.Forms.Label();
            this.Dice = new System.Windows.Forms.PictureBox();
            this.DiceComboBox = new System.Windows.Forms.ComboBox();
            this.DicePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dice)).BeginInit();
            this.SuspendLayout();
            // 
            // DicePanel
            // 
            this.DicePanel.BackColor = System.Drawing.Color.Transparent;
            this.DicePanel.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.GameBox;
            this.DicePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DicePanel.Controls.Add(this.DiceComboBox);
            this.DicePanel.Controls.Add(this.Label1);
            this.DicePanel.Controls.Add(this.ThrowButton);
            this.DicePanel.Controls.Add(this.Dice);
            this.DicePanel.Location = new System.Drawing.Point(121, 66);
            this.DicePanel.Margin = new System.Windows.Forms.Padding(4);
            this.DicePanel.Name = "DicePanel";
            this.DicePanel.Size = new System.Drawing.Size(412, 301);
            this.DicePanel.TabIndex = 31;
            this.DicePanel.Visible = false;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label1.ForeColor = System.Drawing.Color.White;
            this.Label1.Location = new System.Drawing.Point(108, 32);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(215, 29);
            this.Label1.TabIndex = 21;
            this.Label1.Text = "주사위 값을 선택하세요";
            // 
            // ThrowButton
            // 
            this.ThrowButton.AutoSize = true;
            this.ThrowButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.ThrowButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ThrowButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ThrowButton.ForeColor = System.Drawing.Color.White;
            this.ThrowButton.Location = new System.Drawing.Point(164, 226);
            this.ThrowButton.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ThrowButton.Name = "ThrowButton";
            this.ThrowButton.Size = new System.Drawing.Size(70, 29);
            this.ThrowButton.TabIndex = 26;
            this.ThrowButton.Text = "굴리기";
            this.ThrowButton.Click += new System.EventHandler(this.ThrowButton_Click);
            // 
            // Dice
            // 
            this.Dice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.Dice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Dice.Image = global::WindowsFormsApp1.Properties.Resources.Dice1;
            this.Dice.Location = new System.Drawing.Point(147, 77);
            this.Dice.Margin = new System.Windows.Forms.Padding(4);
            this.Dice.Name = "Dice";
            this.Dice.Size = new System.Drawing.Size(114, 104);
            this.Dice.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Dice.TabIndex = 23;
            this.Dice.TabStop = false;
            // 
            // DiceComboBox
            // 
            this.DiceComboBox.FormattingEnabled = true;
            this.DiceComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.DiceComboBox.Location = new System.Drawing.Point(142, 188);
            this.DiceComboBox.Name = "DiceComboBox";
            this.DiceComboBox.Size = new System.Drawing.Size(121, 26);
            this.DiceComboBox.TabIndex = 27;
            // 
            // DiceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DicePanel);
            this.Name = "DiceControl";
            this.Size = new System.Drawing.Size(654, 432);
            this.DicePanel.ResumeLayout(false);
            this.DicePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dice)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel DicePanel;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label ThrowButton;
        private System.Windows.Forms.PictureBox Dice;
        private System.Windows.Forms.ComboBox DiceComboBox;
    }
}
