using Game.BaseMonster;
using Game.Monsters;
using System.Collections.Generic;
using System.Drawing;
using Game.Obstacles;
using System.Windows.Forms;
using Game.Characters;
using WindowsFormsApp1.MapControls;
using Game.MonsterManagers;
using Game.Audio;

namespace WindowsFormsApp1
{

    public partial class SecondMap : Form
    {
        Image thirdMapImg;
        private Character myCharacter;
        private MapController controller;
        public Game.Maps.Map thirdMap;

        private Bitmap backgroundBufferBitmap;
        private Bitmap monsterBuffer;

        private Monster lastClickedMonster;
        private Character lastClickedOpponent;

        public SecondMap(Character character)
        {
            InitializeComponent();
            SoundManager.PlayBgmLoop("secondmap_bgm.wav");
            myCharacter = character;
            myCharacter.MoveLocation(-10, -50);

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            List<Monster> monsters = new List<Monster>
            {
                new Orc { MonsterLocation = (140, 160) },
                new Orc { MonsterLocation = (230, 240) },
                new Orc { MonsterLocation = (340, 200) },
                new Orc { MonsterLocation = (400, 280) },
                new Orc { MonsterLocation = (200, 480) },
                new Orc { MonsterLocation = (310, 400) },
                new Orc { MonsterLocation = (560, 360) },
                new Orc { MonsterLocation = (680, 490) },
                new Orc { MonsterLocation = (600, 200) },
                new Orc { MonsterLocation = (720, 240) },

                new Witch { MonsterLocation = (440, 200) },
                new Witch { MonsterLocation = (780, 80) },
                new Witch { MonsterLocation = (780, 390) },

                new Scorpion { MonsterLocation = (1000, 100) },
                new Scorpion { MonsterLocation = (950, 500) },
                new Scorpion { MonsterLocation = (1100, 300) },
            };

            List<Obstacle> obstacles = new List<Obstacle>
            {
                new Rock { Location = (80, 100) },
                new Rock { Location = (400, 100) },
                new Rock { Location = (620, 210) },
            };

            (thirdMap, thirdMapImg) = Game.MapFactories.MapFactory.CreateMap(monsters, obstacles);

            backgroundBufferBitmap = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            using (Graphics g = Graphics.FromImage(backgroundBufferBitmap))
            {
                g.DrawImage(thirdMapImg, 0, 0);
            }

            monsterBuffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            UpdateMonsterBuffer();

            this.DoubleBuffered = true;
            controller = new MapController(character, thirdMap, this);
            this.KeyDown += ThirdMap_KeyDown;
            this.MouseClick += ThirdMap_MouseClick;
        }

        public void UpdateMonsterBuffer()
        {
            using (Graphics g = Graphics.FromImage(monsterBuffer))
            {
                g.Clear(Color.Transparent);
                foreach (var monster in thirdMap.Monsters)
                {
                    if (!monster.IsDead)
                    {
                        Image monsterImg = MonsterManager.CreateImageFromType(monster.GetType());
                        g.DrawImage(monsterImg, monster.MonsterLocation.x, monster.MonsterLocation.y, 64, 64);
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(backgroundBufferBitmap, 0, 0);
            e.Graphics.DrawImage(monsterBuffer, 0, 0);
            controller.DrawCharacter(e.Graphics);
            //DrawOpponentCharacter(e.Graphics);
        }

        private void ThirdMap_KeyDown(object sender, KeyEventArgs e)
        {
            controller.HandleMovement(e.KeyCode);
        }

        private void ThirdMap_MouseClick(object sender, MouseEventArgs e)
        {
            Monster clickedMonster = FindMonsterAtPoint(e.Location);
            Character clickedOpponent = FindOpponentAtPoint(e.Location);

            if (FindCharacterAtPoint(e.Location))
                controller.ShowCharacterContextMenu(this, myCharacter, e.Location);

            if (clickedMonster != null)
            {
                lastClickedMonster = clickedMonster;
                controller.ShowMonsterContextMenu(this, lastClickedMonster, e.Location);
            }

            if (clickedOpponent != null)
            {
                lastClickedOpponent = clickedOpponent;
                controller.ShowOpponentContextMenu(this, lastClickedOpponent, e.Location);
            }
        }

        private bool FindCharacterAtPoint(Point point)
        {
            Rectangle characterRect = new Rectangle(myCharacter.GetCharacterLocation().x, myCharacter.GetCharacterLocation().y, 64, 64);
            return characterRect.Contains(point);
        }

        private Character FindOpponentAtPoint(Point point)
        {
            foreach (var opponent in thirdMap.opponentCharacters)
            {
                Rectangle opponentRect = new Rectangle(opponent.GetCharacterLocation().x, opponent.GetCharacterLocation().y, 64, 64);
                if (opponentRect.Contains(point))
                    return opponent;
            }
            return null;
        }

        private Monster FindMonsterAtPoint(Point point)
        {
            foreach (var monster in thirdMap.Monsters)
            {
                if (monster.IsDead) continue;

                Rectangle monsterRect = new Rectangle(monster.MonsterLocation.x, monster.MonsterLocation.y, 64, 64);
                if (monsterRect.Contains(point))
                    return monster;
            }
            return null;
        }

        private void pictureBox1_Click(object sender, System.EventArgs e)
        {
            Rectangle charRect = new Rectangle(myCharacter.GetCharacterLocation().x, myCharacter.GetCharacterLocation().y, 64, 64);
            Rectangle picRect = pictureBox1.Bounds;

            bool isColliding = charRect.IntersectsWith(picRect);

            if (isColliding)
            {
                SoundManager.StopBgm();
                //FirstMap firstMap = new FirstMap(myCharacter);
                //firstMap.Show();
                this.Close();
            }
        }
    }
}