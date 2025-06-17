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
            int diceResult = 0;
            switch (rand.Next(6))
            {
                case 0:
                    diceResult = 0;
                    break;
                case 1:
                    diceResult = 1;
                    break;
                case 2:
                    diceResult = 2;
                    break;
                case 3:
                    diceResult = 3;
                    break;
                case 4:
                    diceResult = 4;
                    break;
                case 5:
                    diceResult = 5;
                    break;
            }

            if (DiceComboBox.SelectedIndex == diceResult)
            {
                parentForm.DiceAttackButtonSuccess(DicePanel, diceResult);
            }
            else
            {
                parentForm.DiceAttackButtonFail(DicePanel, diceResult);
            }
        }

        private void DiceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
