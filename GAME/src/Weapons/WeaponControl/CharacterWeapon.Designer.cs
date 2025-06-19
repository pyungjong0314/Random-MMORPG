namespace WindowsFormsApp1.Weapons.WeaponControl
{
    partial class CharacterWeapon
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
            this.CharacterWeaponPanel = new System.Windows.Forms.Panel();
            this.deleteWeapon = new System.Windows.Forms.Button();
            this.EquipedSword = new System.Windows.Forms.PictureBox();
            this.EquipedShield = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.CloseLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.equipWeapon = new System.Windows.Forms.Button();
            this.CharacterWeaponPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EquipedSword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EquipedShield)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // CharacterWeaponPanel
            // 
            this.CharacterWeaponPanel.BackColor = System.Drawing.Color.Transparent;
            this.CharacterWeaponPanel.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.GameBox;
            this.CharacterWeaponPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CharacterWeaponPanel.Controls.Add(this.deleteWeapon);
            this.CharacterWeaponPanel.Controls.Add(this.EquipedSword);
            this.CharacterWeaponPanel.Controls.Add(this.EquipedShield);
            this.CharacterWeaponPanel.Controls.Add(this.pictureBox1);
            this.CharacterWeaponPanel.Controls.Add(this.CloseLabel);
            this.CharacterWeaponPanel.Controls.Add(this.label1);
            this.CharacterWeaponPanel.Controls.Add(this.equipWeapon);
            this.CharacterWeaponPanel.Location = new System.Drawing.Point(279, 56);
            this.CharacterWeaponPanel.Margin = new System.Windows.Forms.Padding(4);
            this.CharacterWeaponPanel.Name = "CharacterWeaponPanel";
            this.CharacterWeaponPanel.Size = new System.Drawing.Size(600, 550);
            this.CharacterWeaponPanel.TabIndex = 34;
            this.CharacterWeaponPanel.Visible = false;
            // 
            // deleteWeapon
            // 
            this.deleteWeapon.BackColor = System.Drawing.Color.White;
            this.deleteWeapon.Font = new System.Drawing.Font("휴먼매직체", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.deleteWeapon.Location = new System.Drawing.Point(111, 474);
            this.deleteWeapon.Name = "deleteWeapon";
            this.deleteWeapon.Size = new System.Drawing.Size(149, 53);
            this.deleteWeapon.TabIndex = 32;
            this.deleteWeapon.Text = "삭제하기";
            this.deleteWeapon.UseVisualStyleBackColor = false;
            this.deleteWeapon.Click += new System.EventHandler(this.deleteWeapon_Click);
            // 
            // EquipedSword
            // 
            this.EquipedSword.BackColor = System.Drawing.Color.White;
            this.EquipedSword.Location = new System.Drawing.Point(111, 129);
            this.EquipedSword.Name = "EquipedSword";
            this.EquipedSword.Size = new System.Drawing.Size(80, 80);
            this.EquipedSword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.EquipedSword.TabIndex = 31;
            this.EquipedSword.TabStop = false;
            this.EquipedSword.Click += new System.EventHandler(this.EquipedSword_Click);
            // 
            // EquipedShield
            // 
            this.EquipedShield.BackColor = System.Drawing.Color.White;
            this.EquipedShield.Location = new System.Drawing.Point(389, 129);
            this.EquipedShield.Name = "EquipedShield";
            this.EquipedShield.Size = new System.Drawing.Size(80, 80);
            this.EquipedShield.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.EquipedShield.TabIndex = 30;
            this.EquipedShield.TabStop = false;
            this.EquipedShield.Click += new System.EventHandler(this.EquipedShield_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::WindowsFormsApp1.Properties.Resources.Player1Character_left;
            this.pictureBox1.Location = new System.Drawing.Point(218, 100);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(141, 138);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("휴먼매직체", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(212, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 36);
            this.label1.TabIndex = 23;
            this.label1.Text = "보유 아이템";
            // 
            // equipWeapon
            // 
            this.equipWeapon.BackColor = System.Drawing.Color.White;
            this.equipWeapon.Font = new System.Drawing.Font("휴먼매직체", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.equipWeapon.Location = new System.Drawing.Point(337, 474);
            this.equipWeapon.Name = "equipWeapon";
            this.equipWeapon.Size = new System.Drawing.Size(149, 53);
            this.equipWeapon.TabIndex = 22;
            this.equipWeapon.Text = "장착하기";
            this.equipWeapon.UseVisualStyleBackColor = false;
            this.equipWeapon.Click += new System.EventHandler(this.equipWeapon_Click);
            // 
            // CharacterWeapon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CharacterWeaponPanel);
            this.Name = "CharacterWeapon";
            this.Size = new System.Drawing.Size(1105, 700);
            this.CharacterWeaponPanel.ResumeLayout(false);
            this.CharacterWeaponPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EquipedSword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EquipedShield)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel CharacterWeaponPanel;
        private System.Windows.Forms.Label CloseLabel;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button equipWeapon;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.PictureBox EquipedSword;
        public System.Windows.Forms.PictureBox EquipedShield;
        public System.Windows.Forms.Button deleteWeapon;
    }
}
