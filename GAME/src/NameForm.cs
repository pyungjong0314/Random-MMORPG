using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game.Characters;
// 플레이어 이름 입력 화면
namespace WindowsFormsApp1
{
    public partial class NameForm : Form
    {
        public NameForm()
        {
            InitializeComponent();
        }

        private void NameSetButton_Click(object sender, EventArgs e)
        {
            Character newCharacter = CharacterFactory.CharacterCreate(NameTextBox.Text);
        }
    }
}
