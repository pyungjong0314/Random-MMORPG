namespace WindowsFormsApp1.Battle
{
    partial class SuccessFailure
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
            this.SuccessFailureLabel = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Label();
            this.ResultImage = new System.Windows.Forms.PictureBox();
            this.AttackPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultImage)).BeginInit();
            this.SuspendLayout();
            // 
            // AttackPanel
            // 
            this.AttackPanel.BackColor = System.Drawing.Color.Transparent;
            this.AttackPanel.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.GameBox;
            this.AttackPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AttackPanel.Controls.Add(this.SuccessFailureLabel);
            this.AttackPanel.Controls.Add(this.CloseButton);
            this.AttackPanel.Controls.Add(this.ResultImage);
            this.AttackPanel.Location = new System.Drawing.Point(164, 137);
            this.AttackPanel.Name = "AttackPanel";
            this.AttackPanel.Size = new System.Drawing.Size(330, 251);
            this.AttackPanel.TabIndex = 32;
            this.AttackPanel.Visible = false;
            // 
            // SuccessFailureLabel
            // 
            this.SuccessFailureLabel.AutoSize = true;
            this.SuccessFailureLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.SuccessFailureLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SuccessFailureLabel.ForeColor = System.Drawing.Color.White;
            this.SuccessFailureLabel.Location = new System.Drawing.Point(141, 32);
            this.SuccessFailureLabel.Name = "SuccessFailureLabel";
            this.SuccessFailureLabel.Size = new System.Drawing.Size(42, 25);
            this.SuccessFailureLabel.TabIndex = 21;
            this.SuccessFailureLabel.Text = "성공";
            // 
            // CloseButton
            // 
            this.CloseButton.AutoSize = true;
            this.CloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.CloseButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.CloseButton.ForeColor = System.Drawing.Color.White;
            this.CloseButton.Location = new System.Drawing.Point(141, 185);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(42, 25);
            this.CloseButton.TabIndex = 26;
            this.CloseButton.Text = "확인";
            // 
            // ResultImage
            // 
            this.ResultImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.ResultImage.Cursor = System.Windows.Forms.Cursors.Default;
            this.ResultImage.Image = global::WindowsFormsApp1.Properties.Resources.Dice6;
            this.ResultImage.Location = new System.Drawing.Point(119, 73);
            this.ResultImage.Name = "ResultImage";
            this.ResultImage.Size = new System.Drawing.Size(91, 87);
            this.ResultImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ResultImage.TabIndex = 23;
            this.ResultImage.TabStop = false;
            // 
            // SuccessFailure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AttackPanel);
            this.Name = "SuccessFailure";
            this.Size = new System.Drawing.Size(657, 524);
            this.AttackPanel.ResumeLayout(false);
            this.AttackPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel AttackPanel;
        private System.Windows.Forms.Label SuccessFailureLabel;
        private System.Windows.Forms.Label CloseButton;
        private System.Windows.Forms.PictureBox ResultImage;
    }
}
