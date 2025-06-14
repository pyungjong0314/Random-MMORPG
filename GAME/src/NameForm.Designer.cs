namespace WindowsFormsApp1
{
    partial class NameForm
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
            this.AttackPanel = new System.Windows.Forms.Panel();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.NameSetButton = new System.Windows.Forms.Label();
            this.AttackPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // AttackPanel
            // 
            this.AttackPanel.BackColor = System.Drawing.Color.Transparent;
            this.AttackPanel.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.GameBox;
            this.AttackPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AttackPanel.Controls.Add(this.NameTextBox);
            this.AttackPanel.Controls.Add(this.NameLabel);
            this.AttackPanel.Controls.Add(this.NameSetButton);
            this.AttackPanel.Location = new System.Drawing.Point(489, 297);
            this.AttackPanel.Name = "AttackPanel";
            this.AttackPanel.Size = new System.Drawing.Size(517, 313);
            this.AttackPanel.TabIndex = 28;
            // 
            // NameTextBox
            // 
            this.NameTextBox.Font = new System.Drawing.Font("Galmuri11 Regular", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NameTextBox.Location = new System.Drawing.Point(116, 128);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(303, 51);
            this.NameTextBox.TabIndex = 27;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.NameLabel.Font = new System.Drawing.Font("Galmuri11 Regular", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NameLabel.ForeColor = System.Drawing.Color.White;
            this.NameLabel.Location = new System.Drawing.Point(116, 42);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(297, 43);
            this.NameLabel.TabIndex = 21;
            this.NameLabel.Text = "이름을 입력하세요";
            // 
            // NameSetButton
            // 
            this.NameSetButton.AutoSize = true;
            this.NameSetButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.NameSetButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NameSetButton.Font = new System.Drawing.Font("Galmuri11 Regular", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NameSetButton.ForeColor = System.Drawing.Color.White;
            this.NameSetButton.Location = new System.Drawing.Point(228, 213);
            this.NameSetButton.Name = "NameSetButton";
            this.NameSetButton.Size = new System.Drawing.Size(73, 38);
            this.NameSetButton.TabIndex = 26;
            this.NameSetButton.Text = "결정";
            this.NameSetButton.Click += new System.EventHandler(this.NameSetButton_Click);
            // 
            // NameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.Background11;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1482, 953);
            this.Controls.Add(this.AttackPanel);
            this.Name = "NameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NameForm";
            this.AttackPanel.ResumeLayout(false);
            this.AttackPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel AttackPanel;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label NameSetButton;
    }
}