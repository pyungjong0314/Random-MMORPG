namespace WindowsFormsApp1
{
    partial class Map1Form
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
            this.shop = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.shop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // shop
            // 
            this.shop.BackColor = System.Drawing.Color.Transparent;
            this.shop.Image = global::WindowsFormsApp1.Properties.Resources.shop;
            this.shop.Location = new System.Drawing.Point(1052, -91);
            this.shop.Name = "shop";
            this.shop.Size = new System.Drawing.Size(431, 495);
            this.shop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.shop.TabIndex = 0;
            this.shop.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::WindowsFormsApp1.Properties.Resources.shopkeeper;
            this.pictureBox1.Location = new System.Drawing.Point(1027, 174);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(118, 153);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Map1Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.TestMap;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1482, 953);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.shop);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "Map1Form";
            this.Text = "Map1";
            ((System.ComponentModel.ISupportInitialize)(this.shop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox shop;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}