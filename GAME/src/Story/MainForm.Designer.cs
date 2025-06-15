namespace WindowsFormsApp1
{
    partial class MainForm
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.NewGameButton = new System.Windows.Forms.Label();
            this.LoadGameButton = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NewGameButton
            // 
            this.NewGameButton.BackColor = System.Drawing.Color.Transparent;
            this.NewGameButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NewGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NewGameButton.ForeColor = System.Drawing.Color.White;
            this.NewGameButton.Location = new System.Drawing.Point(414, 331);
            this.NewGameButton.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.NewGameButton.Name = "NewGameButton";
            this.NewGameButton.Size = new System.Drawing.Size(190, 60);
            this.NewGameButton.TabIndex = 0;
            this.NewGameButton.Text = "새 게임";
            this.NewGameButton.UseCompatibleTextRendering = true;
            this.NewGameButton.Click += new System.EventHandler(this.NewGameButton_Click);
            // 
            // LoadGameButton
            // 
            this.LoadGameButton.BackColor = System.Drawing.Color.Transparent;
            this.LoadGameButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoadGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LoadGameButton.ForeColor = System.Drawing.Color.White;
            this.LoadGameButton.Location = new System.Drawing.Point(400, 420);
            this.LoadGameButton.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LoadGameButton.Name = "LoadGameButton";
            this.LoadGameButton.Size = new System.Drawing.Size(218, 60);
            this.LoadGameButton.TabIndex = 1;
            this.LoadGameButton.Text = "불러오기";
            this.LoadGameButton.UseCompatibleTextRendering = true;
            this.LoadGameButton.Click += new System.EventHandler(this.LoadGameButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.MainBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1000, 683);
            this.Controls.Add(this.LoadGameButton);
            this.Controls.Add(this.NewGameButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label NewGameButton;
        private System.Windows.Forms.Label LoadGameButton;
    }
}

