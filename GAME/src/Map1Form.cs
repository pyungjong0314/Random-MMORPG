using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game.Characters;
using Game.Maps;
// 시작 마을
namespace WindowsFormsApp1
{
    public partial class Map1Form : Form
    {
        private Character character;
        private Image characterImage = Properties.Resources.Player1Character;

        public Map1Form(Character InitCharacter)
        {
            InitializeComponent();
            character = InitCharacter;

            var current = character.GetCharacterLocation();
            int dx = 750 - current.x;
            int dy = 500 - current.y;
            character.MoveLocation(dx, dy);
        }

        private void Map1Form_Load(object sender, EventArgs e)
        {
            
        }

        private void Map1Form_KeyDown(object sender, KeyEventArgs e)
        {
            int moveAmount = 20;
            var current = character.GetCharacterLocation();
            var target = current;

            switch (e.KeyCode)
            {
                case Keys.W: target = (current.x, current.y - moveAmount); break;
                case Keys.S: target = (current.x, current.y + moveAmount); break;
                case Keys.A: target = (current.x - moveAmount, current.y); break;
                case Keys.D: target = (current.x + moveAmount, current.y); break;
            }

            character.MoveLocation(target.x - current.x, target.y - current.y);
            this.Invalidate();

            CheckPortalCollision();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.DrawImage(characterImage, character.GetCharacterLocation().x, character.GetCharacterLocation().y, 64, 64);
        }

        private void CheckPortalCollision()
        {
            var charLoc = character.GetCharacterLocation();
            Rectangle characterBounds = new Rectangle(charLoc.x, charLoc.y, 64, 64);

            Point characterCenter = new Point(
                characterBounds.X + characterBounds.Width / 2,
                characterBounds.Y + characterBounds.Height / 2);

            Point portal1Center = new Point(
                portal1.Left + portal1.Width / 2,
                portal1.Top + portal1.Height / 2);

            double distance = Math.Sqrt(
                Math.Pow(portal1Center.X - characterCenter.X, 2) +
                Math.Pow(portal1Center.Y - characterCenter.Y, 2));

            if (distance < 40)
            {
                TestMapForm testMapForm = new TestMapForm(character);
                testMapForm.Show();
                this.Close();
            }
        }


    }
}
