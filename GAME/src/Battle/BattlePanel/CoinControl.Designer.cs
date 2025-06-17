namespace WindowsFormsApp1.Battle
{
    partial class CoinControl
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
            this.CoinPanel = new System.Windows.Forms.Panel();
            this.Label1 = new System.Windows.Forms.Label();
            this.ThrowButton = new System.Windows.Forms.Label();
            this.CoinFront = new System.Windows.Forms.PictureBox();
            this.DiceLabel = new System.Windows.Forms.Label();
            this.CoinBack = new System.Windows.Forms.PictureBox();
            this.CoinLabel = new System.Windows.Forms.Label();
            this.CoinPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CoinFront)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CoinBack)).BeginInit();
            this.SuspendLayout();
            // 
            // CoinPanel
            // 
            this.CoinPanel.BackColor = System.Drawing.Color.Transparent;
            this.CoinPanel.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.GameBox;
            this.CoinPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CoinPanel.Controls.Add(this.Label1);
            this.CoinPanel.Controls.Add(this.ThrowButton);
            this.CoinPanel.Controls.Add(this.CoinFront);
            this.CoinPanel.Controls.Add(this.DiceLabel);
            this.CoinPanel.Controls.Add(this.CoinBack);
            this.CoinPanel.Controls.Add(this.CoinLabel);
            this.CoinPanel.Location = new System.Drawing.Point(86, 52);
            this.CoinPanel.Name = "CoinPanel";
            this.CoinPanel.Size = new System.Drawing.Size(330, 251);
            this.CoinPanel.TabIndex = 31;
            this.CoinPanel.Visible = false;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label1.ForeColor = System.Drawing.Color.White;
            this.Label1.Location = new System.Drawing.Point(52, 28);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(182, 25);
            this.Label1.TabIndex = 21;
            this.Label1.Text = "동전 앞, 뒤를 선택하세요";
            // 
            // ThrowButton
            // 
            this.ThrowButton.AutoSize = true;
            this.ThrowButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.ThrowButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ThrowButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ThrowButton.ForeColor = System.Drawing.Color.White;
            this.ThrowButton.Location = new System.Drawing.Point(131, 190);
            this.ThrowButton.Name = "ThrowButton";
            this.ThrowButton.Size = new System.Drawing.Size(57, 25);
            this.ThrowButton.TabIndex = 26;
            this.ThrowButton.Text = "던지기";
            this.ThrowButton.Click += new System.EventHandler(this.ThrowButton_Click);
            // 
            // CoinFront
            // 
            this.CoinFront.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.CoinFront.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CoinFront.Image = global::WindowsFormsApp1.Properties.Resources.CoinFront;
            this.CoinFront.Location = new System.Drawing.Point(45, 58);
            this.CoinFront.Name = "CoinFront";
            this.CoinFront.Size = new System.Drawing.Size(100, 94);
            this.CoinFront.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.CoinFront.TabIndex = 22;
            this.CoinFront.TabStop = false;
            // 
            // DiceLabel
            // 
            this.DiceLabel.AutoSize = true;
            this.DiceLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.DiceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DiceLabel.ForeColor = System.Drawing.Color.White;
            this.DiceLabel.Location = new System.Drawing.Point(219, 151);
            this.DiceLabel.Name = "DiceLabel";
            this.DiceLabel.Size = new System.Drawing.Size(21, 20);
            this.DiceLabel.TabIndex = 25;
            this.DiceLabel.Text = "뒤";
            // 
            // CoinBack
            // 
            this.CoinBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.CoinBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CoinBack.Image = global::WindowsFormsApp1.Properties.Resources.CoinBack;
            this.CoinBack.Location = new System.Drawing.Point(182, 61);
            this.CoinBack.Name = "CoinBack";
            this.CoinBack.Size = new System.Drawing.Size(91, 87);
            this.CoinBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.CoinBack.TabIndex = 23;
            this.CoinBack.TabStop = false;
            // 
            // CoinLabel
            // 
            this.CoinLabel.AutoSize = true;
            this.CoinLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.CoinLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.CoinLabel.ForeColor = System.Drawing.Color.White;
            this.CoinLabel.Location = new System.Drawing.Point(83, 156);
            this.CoinLabel.Name = "CoinLabel";
            this.CoinLabel.Size = new System.Drawing.Size(21, 20);
            this.CoinLabel.TabIndex = 24;
            this.CoinLabel.Text = "앞";
            // 
            // CoinControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CoinPanel);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "CoinControl";
            this.Size = new System.Drawing.Size(502, 355);
            this.CoinPanel.ResumeLayout(false);
            this.CoinPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CoinFront)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CoinBack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel CoinPanel;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label ThrowButton;
        private System.Windows.Forms.PictureBox CoinFront;
        private System.Windows.Forms.Label DiceLabel;
        private System.Windows.Forms.PictureBox CoinBack;
        private System.Windows.Forms.Label CoinLabel;
    }
}
