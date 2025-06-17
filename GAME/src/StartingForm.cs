using Game.BaseMonster;
using Game.Characters;
using Game.Maps;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.MapControls;

namespace WindowsFormsApp1
{
    public partial class StartingForm : Form
    {
        private Character myCharacter;
        private MapController controller;
        Game.Maps.Map map = new Game.Maps.Map();

        public StartingForm(Character character)
        {
            InitializeComponent();

            // 캐릭터 위치 설정
            myCharacter = character;
            var current = character.GetCharacterLocation();
            int dx = 500 - current.x;
            int dy = 250 - current.y;
            character.MoveLocation(dx, dy);

            // 이벤트 핸들러
            controller = new MapController(character, map, this);
            this.MouseClick += FirstMap_MouseClick;
            this.KeyDown += StartingForm_KeyDown;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // 3. 캐릭터도 그리기
            controller.DrawCharacter(e.Graphics);
        }

        private void StartingForm_KeyDown(object sender, KeyEventArgs e)
        {
            controller.HandleMovement(e.KeyCode);
        }

        private void FirstMap_MouseClick(object sender, MouseEventArgs e)
        {
            // 캐릭터 클릭
            if (FindCharacterAtPoint(e.Location))
                controller.ShowCharacterContextMenu(this, myCharacter, e.Location);
        }

        private bool FindCharacterAtPoint(Point point)
        {
            Rectangle characterRect = new Rectangle(myCharacter.GetCharacterLocation().x, myCharacter.GetCharacterLocation().y, 64, 64);
            if (characterRect.Contains(point))
                return true;

            return false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Rectangle charRect = new Rectangle(myCharacter.GetCharacterLocation().x, myCharacter.GetCharacterLocation().y, 64, 64);
            Rectangle picRect = pictureBox1.Bounds;

            bool isColliding = charRect.IntersectsWith(picRect);

            if (isColliding)
            {
                FirstMap firstmap = new FirstMap(myCharacter);
                firstmap.Show();
                this.Close();
            }
        }
    }
}
