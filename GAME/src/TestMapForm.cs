using Game.BaseMonster;
using Game.Characters;
using Game.Maps;
using Game.Monsters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class TestMapForm : Form
    {
        private void TestMapForm_Load(object sender, EventArgs e)
        {

        }

        private Timer updateTimer;



        // 리스폰 타이머
        private void StartUpdateTimer()
        {
            updateTimer = new Timer();
            updateTimer.Interval = 1000; // 1초마다 호출
            updateTimer.Tick += (s, e) =>
            {
                map.Update();
                this.Invalidate(); // 리스폰된 몬스터를 생성
            };
            updateTimer.Start();
        }


        // 캐릭터 및 맵 생성
        private Character character;
        private Map map = MapFactory.CreateMap(1);


        // 캐릭터 이미지 생성
        private Image characterImage = Properties.Resources.Player1Character;
        private Image goblinImage = Properties.Resources.goblin2;
        private Image scorpionImage = Properties.Resources.scorpion;
        private Image wizardImage = Properties.Resources.wizard;

        private ContextMenuStrip monsterContextMenu;
        private ToolStripMenuItem attackMenuItem;
        private Monster lastClickedMonster;


        public TestMapForm(Character InitCharacter)
        {
            InitializeComponent();
            character = InitCharacter;

            InitializeMonsterContextMenu();
            this.MouseDown += TestForm_MouseDown;
            this.KeyDown += TestForm_KeyDown;
            this.DoubleBuffered = true; // 깜빡임 방지

            StartUpdateTimer(); // ← 리스폰 활성화!

        }



        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // 캐릭터 출력
            e.Graphics.DrawImage(characterImage, character.GetCharacterLocation().x, character.GetCharacterLocation().y, 64, 64);

            // 몬스터 출력
            foreach (var monster in map.Monsters)
            {
                Image image = GetMonsterImage(monster);
                var loc = monster.MonsterLocation;
                e.Graphics.DrawImage(image, loc.x, loc.y, 80, 80);
            }
        }

        private Image GetMonsterImage(Monster m)
        {
            if (m is Goblin) return goblinImage;
            if (m is Scorpion) return scorpionImage;
            if (m is Witch) return wizardImage;
            return goblinImage;
        }

        private void TestForm_KeyDown(object sender, KeyEventArgs e)
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

            // 캐릭터가 몬스터와 겹치지 않도록 충돌 감지를 위한 사각형 영역을 설정함
            Rectangle targetRect = new Rectangle(target.x, target.y, 30, 30);

            bool isBlocked = false;
            foreach (var monster in map.Monsters)
            {
                Rectangle monsterRect = new Rectangle(monster.MonsterLocation.x, monster.MonsterLocation.y, 80, 80);
                if (targetRect.IntersectsWith(monsterRect))
                {
                    isBlocked = true;
                    break;
                }
            }

            if (!isBlocked)
            {
                character.MoveLocation(target.x - current.x, target.y - current.y);
                this.Invalidate();
            }
        }


        private void InitializeMonsterContextMenu()
        {
            monsterContextMenu = new ContextMenuStrip();

            monsterContextMenu.Items.Add("정보 확인하기", null, OnInfoClicked);

            attackMenuItem = new ToolStripMenuItem("공격하기", null, OnAttackClicked);
            monsterContextMenu.Items.Add(attackMenuItem);

            monsterContextMenu.Opening += MonsterContextMenu_Opening;
        }

        private void TestForm_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (var monster in map.Monsters)
            {
                Rectangle monsterRect = new Rectangle(monster.MonsterLocation.x, monster.MonsterLocation.y, 80, 80);
                if (monsterRect.Contains(e.Location))
                {
                    lastClickedMonster = monster;
                    monsterContextMenu.Show(this, e.Location);
                    break;
                }
            }
        }

        private void MonsterContextMenu_Opening(object sender, CancelEventArgs e)
        {
            Point characterPosition = new Point(character.GetCharacterLocation().x, character.GetCharacterLocation().y);
            Point monsterPosition = new Point(lastClickedMonster.MonsterLocation.x, lastClickedMonster.MonsterLocation.y);

            double distance = Math.Sqrt(Math.Pow(monsterPosition.X - characterPosition.X, 2) +
                                        Math.Pow(monsterPosition.Y - characterPosition.Y, 2));

            attackMenuItem.Enabled = distance <= 60;
        }

        private void OnInfoClicked(object sender, EventArgs e)
        {
            MessageBox.Show($"{lastClickedMonster.MonsterName} - HP: {lastClickedMonster.MonsterHp}");
        }

        private void OnAttackClicked(object sender, EventArgs e)
        {
            /*lastClickedMonster.MonsterGetAttack(100);
            if (lastClickedMonster.MonsterHp <= 0)
            {
                map.RemoveMonster(lastClickedMonster);
            }*/

            //PictureBox target = sender as PictureBox;
            //BattleForm battleForm = new BattleForm(target.Tag);
            BattleForm battleForm = new BattleForm(character, map.Monsters[0]);
            battleForm.Show();

            this.Invalidate();
        }
    }
}
