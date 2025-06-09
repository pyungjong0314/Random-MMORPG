using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            StoryForm storyForm = new StoryForm();
            storyForm.Show();
            this.Hide();
        }

        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            
        }


    }
}
