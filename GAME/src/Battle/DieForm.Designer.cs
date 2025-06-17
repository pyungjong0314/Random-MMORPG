namespace WindowsFormsApp1.Battle
{
    partial class DieForm
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
            this.DieLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DieLabel
            // 
            this.DieLabel.AutoSize = true;
            this.DieLabel.Font = new System.Drawing.Font("굴림", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DieLabel.ForeColor = System.Drawing.Color.White;
            this.DieLabel.Location = new System.Drawing.Point(340, 180);
            this.DieLabel.Name = "DieLabel";
            this.DieLabel.Size = new System.Drawing.Size(97, 40);
            this.DieLabel.TabIndex = 0;
            this.DieLabel.Text = "사망";
            // 
            // DieForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(800, 569);
            this.Controls.Add(this.DieLabel);
            this.Name = "DieForm";
            this.Text = "DieForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DieLabel;
    }
}