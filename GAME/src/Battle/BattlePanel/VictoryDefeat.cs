using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Battle.BattlePanel
{
    public partial class VictoryDefeat : UserControl
    {
        BattleForm parentForm;

        public VictoryDefeat(BattleForm parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (VictoryDefeatLabel.Text.Equals("패배"))
            {
                parentForm.parentForm.Close();
                DieForm dieForm = new DieForm();
                dieForm.Show();
            }

            this.Controls.Remove(this.VictoryDefeatPanel);
            parentForm.Close();
        }
    }
}
