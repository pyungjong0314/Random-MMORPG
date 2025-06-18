namespace WindowsFormsApp1.WeaponControls
{
    partial class WeaponControl
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
            this.WeaponPanel = new System.Windows.Forms.Panel();
            this.CloseLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.WeaponValue = new System.Windows.Forms.Label();
            this.WeaponPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // WeaponPanel
            // 
            this.WeaponPanel.BackColor = System.Drawing.Color.Transparent;
            this.WeaponPanel.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.GameBox;
            this.WeaponPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.WeaponPanel.Controls.Add(this.WeaponValue);
            this.WeaponPanel.Controls.Add(this.label2);
            this.WeaponPanel.Controls.Add(this.CloseLabel);
            this.WeaponPanel.Controls.Add(this.label1);
            this.WeaponPanel.Controls.Add(this.button1);
            this.WeaponPanel.Location = new System.Drawing.Point(248, 78);
            this.WeaponPanel.Margin = new System.Windows.Forms.Padding(4);
            this.WeaponPanel.Name = "WeaponPanel";
            this.WeaponPanel.Size = new System.Drawing.Size(600, 550);
            this.WeaponPanel.TabIndex = 32;
            this.WeaponPanel.Visible = false;
            // 
            // CloseLabel
            // 
            this.CloseLabel.AutoSize = true;
            this.CloseLabel.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.CloseLabel.Location = new System.Drawing.Point(530, 40);
            this.CloseLabel.Name = "CloseLabel";
            this.CloseLabel.Size = new System.Drawing.Size(31, 28);
            this.CloseLabel.TabIndex = 29;
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
            this.label1.Size = new System.Drawing.Size(172, 36);
            this.label1.TabIndex = 23;
            this.label1.Text = "ITEM 상점";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Font = new System.Drawing.Font("휴먼매직체", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(390, 485);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 42);
            this.button1.TabIndex = 22;
            this.button1.Text = "구매하기";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("휴먼매직체", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(58, 492);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 28);
            this.label2.TabIndex = 30;
            this.label2.Text = "COIN: ";
            // 
            // WeaponValue
            // 
            this.WeaponValue.AutoSize = true;
            this.WeaponValue.Font = new System.Drawing.Font("휴먼매직체", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.WeaponValue.ForeColor = System.Drawing.Color.Gold;
            this.WeaponValue.Location = new System.Drawing.Point(147, 492);
            this.WeaponValue.Name = "WeaponValue";
            this.WeaponValue.Size = new System.Drawing.Size(0, 28);
            this.WeaponValue.TabIndex = 31;
            // 
            // WeaponControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.WeaponPanel);
            this.Name = "WeaponControl";
            this.Size = new System.Drawing.Size(1048, 685);
            this.WeaponPanel.ResumeLayout(false);
            this.WeaponPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel WeaponPanel;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label CloseLabel;
        public System.Windows.Forms.Label WeaponValue;
        public System.Windows.Forms.Label label2;
    }
}
