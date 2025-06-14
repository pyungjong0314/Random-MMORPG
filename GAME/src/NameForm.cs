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
        Character newCharacter;
        
        public NameForm()
        {
            InitializeComponent();
        }

        private void NameSetButton_Click(object sender, EventArgs e)
        {
            newCharacter = CharacterFactory.CharacterCreate(NameTextBox.Text);
            MessageBox.Show($"캐릭터 이름이 '{newCharacter.GetCharacterName()}'(으)로 설정되었습니다.", "캐릭터 이름 설정 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
