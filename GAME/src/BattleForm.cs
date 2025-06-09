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
    public partial class BattleForm : Form
    {
        public BattleForm()
        {
            InitializeComponent();
        }

        private void AttackButton_Click(object sender, EventArgs e)
        {
            AttackPanel.Visible = true;
        }

        private void Coin_Click(object sender, EventArgs e)
        {

        }

        private void Dice_Click(object sender, EventArgs e)
        {

        }

        private void ThrowButton_Click(object sender, EventArgs e)
        {

        }
    }
}
