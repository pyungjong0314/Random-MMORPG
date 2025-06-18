namespace WindowsFormsApp1.Weapons.WeaponControl
{
    partial class WeaponUpgradeControl
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
            this.WeaponUpgradePanel = new System.Windows.Forms.Panel();
            this.CloseLabel = new System.Windows.Forms.Label();
            this.UpgradeWeapon = new System.Windows.Forms.PictureBox();
            this.UpgradeWeaponResult = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.UpgradeButton = new System.Windows.Forms.Button();
            this.WeaponUpgradePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpgradeWeapon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpgradeWeaponResult)).BeginInit();
            this.SuspendLayout();
            // 
            // WeaponUpgradePanel
            // 
            this.WeaponUpgradePanel.BackColor = System.Drawing.Color.Transparent;
            this.WeaponUpgradePanel.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.GameBox;
            this.WeaponUpgradePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.WeaponUpgradePanel.Controls.Add(this.CloseLabel);
            this.WeaponUpgradePanel.Controls.Add(this.UpgradeWeapon);
            this.WeaponUpgradePanel.Controls.Add(this.UpgradeWeaponResult);
            this.WeaponUpgradePanel.Controls.Add(this.label1);
            this.WeaponUpgradePanel.Controls.Add(this.UpgradeButton);
            this.WeaponUpgradePanel.Location = new System.Drawing.Point(284, 86);
            this.WeaponUpgradePanel.Margin = new System.Windows.Forms.Padding(4);
            this.WeaponUpgradePanel.Name = "WeaponUpgradePanel";
            this.WeaponUpgradePanel.Size = new System.Drawing.Size(600, 550);
            this.WeaponUpgradePanel.TabIndex = 33;
            this.WeaponUpgradePanel.Visible = false;
            // 
            // CloseLabel
            // 
            this.CloseLabel.AutoSize = true;
            this.CloseLabel.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.CloseLabel.Location = new System.Drawing.Point(537, 40);
            this.CloseLabel.Name = "CloseLabel";
            this.CloseLabel.Size = new System.Drawing.Size(31, 28);
            this.CloseLabel.TabIndex = 28;
            this.CloseLabel.Text = "X";
            this.CloseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CloseLabel.Click += new System.EventHandler(this.CloseLabel_Click);
            // 
            // UpgradeWeapon
            // 
            this.UpgradeWeapon.BackColor = System.Drawing.Color.White;
            this.UpgradeWeapon.Location = new System.Drawing.Point(56, 100);
            this.UpgradeWeapon.Name = "UpgradeWeapon";
            this.UpgradeWeapon.Size = new System.Drawing.Size(204, 138);
            this.UpgradeWeapon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.UpgradeWeapon.TabIndex = 27;
            this.UpgradeWeapon.TabStop = false;
            // 
            // UpgradeWeaponResult
            // 
            this.UpgradeWeaponResult.BackColor = System.Drawing.Color.White;
            this.UpgradeWeaponResult.Location = new System.Drawing.Point(342, 100);
            this.UpgradeWeaponResult.Name = "UpgradeWeaponResult";
            this.UpgradeWeaponResult.Size = new System.Drawing.Size(204, 138);
            this.UpgradeWeaponResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.UpgradeWeaponResult.TabIndex = 25;
            this.UpgradeWeaponResult.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("휴먼매직체", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(212, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 36);
            this.label1.TabIndex = 23;
            this.label1.Text = "ITEM 강화";
            // 
            // UpgradeButton
            // 
            this.UpgradeButton.BackColor = System.Drawing.Color.White;
            this.UpgradeButton.Font = new System.Drawing.Font("휴먼매직체", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.UpgradeButton.Location = new System.Drawing.Point(228, 474);
            this.UpgradeButton.Name = "UpgradeButton";
            this.UpgradeButton.Size = new System.Drawing.Size(149, 53);
            this.UpgradeButton.TabIndex = 22;
            this.UpgradeButton.Text = "강화하기";
            this.UpgradeButton.UseVisualStyleBackColor = false;
            this.UpgradeButton.Click += new System.EventHandler(this.UpgradeButton_Click);
            // 
            // WeaponUpgradeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.WeaponUpgradePanel);
            this.Name = "WeaponUpgradeControl";
            this.Size = new System.Drawing.Size(1169, 722);
            this.WeaponUpgradePanel.ResumeLayout(false);
            this.WeaponUpgradePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpgradeWeapon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpgradeWeaponResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel WeaponUpgradePanel;
        public System.Windows.Forms.PictureBox UpgradeWeaponResult;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button UpgradeButton;
        public System.Windows.Forms.PictureBox UpgradeWeapon;
        private System.Windows.Forms.Label CloseLabel;
    }
}
