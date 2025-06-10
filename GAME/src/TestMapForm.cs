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
    public partial class TestMapForm : Form
    {
        private Character character;
        private Image characterImage = Properties.Resources.Player1Character;

        public TestMapForm(Character InitCharacter)
        {
            InitializeComponent();
            character = InitCharacter;

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (characterImage != null)
            {
                e.Graphics.DrawImage(characterImage, character.GetCharacterLocation().x, character.GetCharacterLocation().y, 64, 64);
            }
        }

        private void TestForm_KeyDown(object sender, KeyEventArgs e)
        {
            int moveAmount = 5;  // 한 번에 이동할 픽셀 수

            switch (e.KeyCode)
            {
                case Keys.Up:
                    character.MoveLocation(0, -moveAmount);
                    break;
                case Keys.Down:
                    character.MoveLocation(0, moveAmount);
                    break;
                case Keys.Left:
                    character.MoveLocation(-moveAmount, 0);
                    break;
                case Keys.Right:
                    character.MoveLocation(moveAmount, 0);
                    break;
            }

            this.Invalidate();  // 위치 변경 후 다시 그리기 요청
        }
    }
}
