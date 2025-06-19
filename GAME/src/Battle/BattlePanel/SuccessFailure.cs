using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Battle.BattlePanel
{
    public partial class SuccessFailure : UserControl
    {
        BattleForm parentForm;
        int damage;

        public SuccessFailure(BattleForm parentForm, int damage)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            this.damage = damage;
            
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            parentForm.Attack(damage);
            parentForm.Controls.Remove(this.SuccessFailurePanel);

            parentForm.Player1Character.Image = Properties.Resources.AttackPlayer;
            parentForm.DamageLabel.Text = "-" + damage.ToString();
            parentForm.DamageLabel.Visible = true;
            parentForm.DefenseButton.Enabled = true;
            parentForm.AttackButton.Enabled = false;

            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += (s, e2) =>
            {
                parentForm.Player1Character.Image = Properties.Resources.Player1Character_right;
                parentForm.DamageLabel.Visible = false;
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }
    }
}
