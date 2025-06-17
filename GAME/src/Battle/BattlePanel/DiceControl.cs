using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace WindowsFormsApp1.Battle
{
    public partial class DiceControl : UserControl
    {
        private BattleForm parentForm;
        Random rand = new Random();
        public DiceControl(BattleForm parent)
        {
            InitializeComponent();

            parentForm = parent;
            DiceComboBox.SelectedIndex = 0;
        }

        private void ThrowButton_Click(object sender, EventArgs e)
        {
            if (DiceComboBox.SelectedIndex == rand.Next(6))
            {
                parentForm.DiceAttackButtonSuccess(DicePanel);
            }
            else
            {
                parentForm.DiceAttackButtonFail(DicePanel);
            }
        }

        private void DiceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
