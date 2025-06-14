using Game.Characters;
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
            // 캐릭터 생성
            Character newCharacter = CharacterFactory.CharacterCreate("강평종");

            StoryForm storyForm = new StoryForm();
            storyForm.Show();
            this.Hide();

            // StoryForm이 닫히면 TestMap이 열림
            storyForm.FormClosed += (s, args) =>
            {
                // storyForm 닫히면 MapForm 열기 
                TestMapForm testForm = new TestMapForm(newCharacter);
                testForm.Show();
            };
        }

        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            
        }


    }
}
