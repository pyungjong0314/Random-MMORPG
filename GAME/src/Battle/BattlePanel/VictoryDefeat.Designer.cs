namespace WindowsFormsApp1.Battle.BattlePanel
{
    partial class VictoryDefeat
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
            this.VictoryDefeatPanel = new System.Windows.Forms.Panel();
            this.CloseButton = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.VictoryDefeatLabel = new System.Windows.Forms.Label();
            this.PlayerImage = new System.Windows.Forms.PictureBox();
            this.VictoryDefeatPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerImage)).BeginInit();
            this.SuspendLayout();
            // 
            // VictoryDefeatPanel
            // 
            this.VictoryDefeatPanel.BackColor = System.Drawing.Color.Transparent;
            this.VictoryDefeatPanel.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.GameBox;
            this.VictoryDefeatPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VictoryDefeatPanel.Controls.Add(this.CloseButton);
            this.VictoryDefeatPanel.Controls.Add(this.NameLabel);
            this.VictoryDefeatPanel.Controls.Add(this.VictoryDefeatLabel);
            this.VictoryDefeatPanel.Controls.Add(this.PlayerImage);
            this.VictoryDefeatPanel.Location = new System.Drawing.Point(121, 108);
            this.VictoryDefeatPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.VictoryDefeatPanel.Name = "VictoryDefeatPanel";
            this.VictoryDefeatPanel.Size = new System.Drawing.Size(412, 301);
            this.VictoryDefeatPanel.TabIndex = 34;
            this.VictoryDefeatPanel.Visible = false;
            // 
            // CloseButton
            // 
            this.CloseButton.AutoSize = true;
            this.CloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.CloseButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.CloseButton.ForeColor = System.Drawing.Color.White;
            this.CloseButton.Location = new System.Drawing.Point(174, 244);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(51, 29);
            this.CloseButton.TabIndex = 27;
            this.CloseButton.Text = "확인";
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NameLabel.ForeColor = System.Drawing.Color.White;
            this.NameLabel.Location = new System.Drawing.Point(153, 202);
            this.NameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(102, 29);
            this.NameLabel.TabIndex = 24;
            this.NameLabel.Text = "플레이어1";
            this.NameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VictoryDefeatLabel
            // 
            this.VictoryDefeatLabel.AutoSize = true;
            this.VictoryDefeatLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.VictoryDefeatLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.VictoryDefeatLabel.ForeColor = System.Drawing.Color.White;
            this.VictoryDefeatLabel.Location = new System.Drawing.Point(176, 23);
            this.VictoryDefeatLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.VictoryDefeatLabel.Name = "VictoryDefeatLabel";
            this.VictoryDefeatLabel.Size = new System.Drawing.Size(51, 29);
            this.VictoryDefeatLabel.TabIndex = 21;
            this.VictoryDefeatLabel.Text = "승리";
            // 
            // PlayerImage
            // 
            this.PlayerImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.PlayerImage.Cursor = System.Windows.Forms.Cursors.Default;
            this.PlayerImage.Location = new System.Drawing.Point(149, 65);
            this.PlayerImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PlayerImage.Name = "PlayerImage";
            this.PlayerImage.Size = new System.Drawing.Size(106, 133);
            this.PlayerImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PlayerImage.TabIndex = 23;
            this.PlayerImage.TabStop = false;
            // 
            // VictoryDefeat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.VictoryDefeatPanel);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "VictoryDefeat";
            this.Size = new System.Drawing.Size(656, 516);
            this.VictoryDefeatPanel.ResumeLayout(false);
            this.VictoryDefeatPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel VictoryDefeatPanel;
        private System.Windows.Forms.Label CloseButton;
        public System.Windows.Forms.Label NameLabel;
        public System.Windows.Forms.Label VictoryDefeatLabel;
        public System.Windows.Forms.PictureBox PlayerImage;
    }
}
