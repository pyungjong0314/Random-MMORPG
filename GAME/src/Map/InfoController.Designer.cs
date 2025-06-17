namespace WindowsFormsApp1.Map
{
    partial class InfoController
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
            this.InfoPanel = new System.Windows.Forms.Panel();
            this.lbClose = new System.Windows.Forms.Label();
            this.lbHealth = new System.Windows.Forms.Label();
            this.lbAttack = new System.Windows.Forms.Label();
            this.lbCoin = new System.Windows.Forms.Label();
            this.lbLevel = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.lbLevelInfo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pbInfo = new System.Windows.Forms.PictureBox();
            this.InfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // InfoPanel
            // 
            this.InfoPanel.BackColor = System.Drawing.Color.Transparent;
            this.InfoPanel.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.GameBox;
            this.InfoPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.InfoPanel.Controls.Add(this.pbInfo);
            this.InfoPanel.Controls.Add(this.lbClose);
            this.InfoPanel.Controls.Add(this.lbHealth);
            this.InfoPanel.Controls.Add(this.lbAttack);
            this.InfoPanel.Controls.Add(this.lbCoin);
            this.InfoPanel.Controls.Add(this.lbLevel);
            this.InfoPanel.Controls.Add(this.lbName);
            this.InfoPanel.Controls.Add(this.lbLevelInfo);
            this.InfoPanel.Controls.Add(this.label3);
            this.InfoPanel.Controls.Add(this.label2);
            this.InfoPanel.Controls.Add(this.label1);
            this.InfoPanel.Controls.Add(this.labelTitle);
            this.InfoPanel.Location = new System.Drawing.Point(209, 152);
            this.InfoPanel.Name = "InfoPanel";
            this.InfoPanel.Size = new System.Drawing.Size(330, 251);
            this.InfoPanel.TabIndex = 35;
            this.InfoPanel.Visible = false;
            // 
            // lbClose
            // 
            this.lbClose.AutoSize = true;
            this.lbClose.Cursor = System.Windows.Forms.Cursors.Cross;
            this.lbClose.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbClose.Location = new System.Drawing.Point(264, 214);
            this.lbClose.Name = "lbClose";
            this.lbClose.Size = new System.Drawing.Size(37, 15);
            this.lbClose.TabIndex = 10;
            this.lbClose.Text = "닫기";
            this.lbClose.Click += new System.EventHandler(this.lbClose_Click);
            // 
            // lbHealth
            // 
            this.lbHealth.AutoSize = true;
            this.lbHealth.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbHealth.Location = new System.Drawing.Point(209, 190);
            this.lbHealth.Name = "lbHealth";
            this.lbHealth.Size = new System.Drawing.Size(0, 15);
            this.lbHealth.TabIndex = 9;
            // 
            // lbAttack
            // 
            this.lbAttack.AutoSize = true;
            this.lbAttack.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbAttack.Location = new System.Drawing.Point(209, 155);
            this.lbAttack.Name = "lbAttack";
            this.lbAttack.Size = new System.Drawing.Size(0, 15);
            this.lbAttack.TabIndex = 8;
            // 
            // lbCoin
            // 
            this.lbCoin.AutoSize = true;
            this.lbCoin.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbCoin.Location = new System.Drawing.Point(209, 115);
            this.lbCoin.Name = "lbCoin";
            this.lbCoin.Size = new System.Drawing.Size(0, 15);
            this.lbCoin.TabIndex = 7;
            // 
            // lbLevel
            // 
            this.lbLevel.AutoSize = true;
            this.lbLevel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbLevel.Location = new System.Drawing.Point(209, 85);
            this.lbLevel.Name = "lbLevel";
            this.lbLevel.Size = new System.Drawing.Size(0, 15);
            this.lbLevel.TabIndex = 6;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbName.Location = new System.Drawing.Point(209, 53);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(0, 15);
            this.lbName.TabIndex = 5;
            // 
            // lbLevelInfo
            // 
            this.lbLevelInfo.AutoSize = true;
            this.lbLevelInfo.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lbLevelInfo.Location = new System.Drawing.Point(121, 85);
            this.lbLevelInfo.Name = "lbLevelInfo";
            this.lbLevelInfo.Size = new System.Drawing.Size(82, 15);
            this.lbLevelInfo.TabIndex = 4;
            this.lbLevelInfo.Text = "현재 레벨 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(121, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "보유 코인 :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(121, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "체력 :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(121, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "공격력 :";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelTitle.Location = new System.Drawing.Point(121, 53);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(47, 15);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "이름 :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 15);
            this.label5.TabIndex = 36;
            this.label5.Text = "label5";
            // 
            // pbInfo
            // 
            this.pbInfo.BackColor = System.Drawing.Color.Transparent;
            this.pbInfo.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.Player1Character;
            this.pbInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbInfo.Location = new System.Drawing.Point(15, 53);
            this.pbInfo.Name = "pbInfo";
            this.pbInfo.Size = new System.Drawing.Size(100, 137);
            this.pbInfo.TabIndex = 11;
            this.pbInfo.TabStop = false;
            this.pbInfo.Click += new System.EventHandler(this.pbInfo_Click);
            // 
            // InfoController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.InfoPanel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "InfoController";
            this.Size = new System.Drawing.Size(748, 553);
            this.InfoPanel.ResumeLayout(false);
            this.InfoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel InfoPanel;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label lbHealth;
        private System.Windows.Forms.Label lbAttack;
        private System.Windows.Forms.Label lbCoin;
        private System.Windows.Forms.Label lbLevel;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label lbLevelInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbClose;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pbInfo;
    }
}
