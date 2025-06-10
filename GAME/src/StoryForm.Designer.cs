namespace WindowsFormsApp1
{
    partial class StoryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.NextStoryButton = new System.Windows.Forms.Label();
            this.StoryText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NextStoryButton
            // 
            this.NextStoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NextStoryButton.BackColor = System.Drawing.Color.Transparent;
            this.NextStoryButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NextStoryButton.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NextStoryButton.ForeColor = System.Drawing.Color.White;
            this.NextStoryButton.Location = new System.Drawing.Point(1182, 909);
            this.NextStoryButton.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.NextStoryButton.Name = "NextStoryButton";
            this.NextStoryButton.Size = new System.Drawing.Size(105, 26);
            this.NextStoryButton.TabIndex = 0;
            this.NextStoryButton.Text = "다음 ▶";
            this.NextStoryButton.Click += new System.EventHandler(this.NextStoryButton_Click);
            // 
            // StoryText
            // 
            this.StoryText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StoryText.BackColor = System.Drawing.Color.Transparent;
            this.StoryText.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.StoryText.ForeColor = System.Drawing.Color.White;
            this.StoryText.Location = new System.Drawing.Point(206, 758);
            this.StoryText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StoryText.Name = "StoryText";
            this.StoryText.Size = new System.Drawing.Size(944, 116);
            this.StoryText.TabIndex = 1;
            this.StoryText.Text = "Text";
            // 
            // StoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.Story1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1478, 944);
            this.Controls.Add(this.StoryText);
            this.Controls.Add(this.NextStoryButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "StoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StoryForm";
            this.Load += new System.EventHandler(this.StoryForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StoryForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label NextStoryButton;
        private System.Windows.Forms.Label StoryText;
    }
}