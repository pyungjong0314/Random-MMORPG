using Game.BaseMonster;
using Game.Characters;
using Game.Maps;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class TestMapForm : Form
    {

        // 리스폰 타이머
        private Timer updateTimer;

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




        // 캐릭터 및 맵에 몬스터 생성
        private Character character;
        private Map map = MapFactory.CreateMap(1);


        // 이미지 생성
        private Image characterImage = Properties.Resources.Player1Character;
        private Image coinImage = Properties.Resources.CoinFront;

        private ContextMenuStrip monsterContextMenu;
        private ToolStripMenuItem attackMenuItem;
        private Monster lastClickedMonster;


        // 모든 몬스터 PictureBox 저장하는 리스트
        public List<PictureBox> monsterPictureBoxes { get; private set; } = new List<PictureBox>();




        // 
        private void TestMapForm_Load(object sender, EventArgs e)
        {
            // 모든 몬스터 PictureBox를 가져옴
            monsterPictureBoxes = this.Controls
                .OfType<PictureBox>()
                .Where(pb => pb.Name.StartsWith("monster_"))
                .OrderBy(pb => pb.Name)
                .ToList();

                
            // 모든 몬스터에 대한 위치 설정 및 태그
            for (int i = 0; i < monsterPictureBoxes.Count && i < map.Monsters.Count; i++)
            {
                // 위치 설정
                map.Monsters[i].MonsterLocation.x = monsterPictureBoxes[i].Location.X;
                map.Monsters[i].MonsterLocation.y = monsterPictureBoxes[i].Location.Y;

                // 태그 설정
                Console.WriteLine($"{monsterPictureBoxes[i].Name} ← {map.Monsters[i].MonsterName} ({map.Monsters[i].MonsterLocation.x},{map.Monsters[i].MonsterLocation.y}).");
                monsterPictureBoxes[i].Tag = map.Monsters[i];
            }
        }


        public TestMapForm(Character InitCharacter)
        {
            InitializeComponent();
            character = InitCharacter;
            Console.WriteLine($"Initially {character.GetCharacterName()}  {character.GetCharacterLevel()} has coin : {character.GetCharacterMoney()}, exp : {character.GetCharacterExp()} ");


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

            // 💰 드롭된 코인 출력
            foreach (var coin in map.DroppedCoins)
            {
                e.Graphics.DrawImage(coinImage, coin.x, coin.y+30, 32, 32);
            }
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
            Rectangle targetRect = new Rectangle(target.x, target.y, 10, 10);

            bool isBlocked = false;

            if (!isBlocked)
            {
                character.MoveLocation(target.x - current.x, target.y - current.y);

                var pickupResult = map.PickUpCoins(character.GetCharacterLocation());
                if (pickupResult.totalAmount > 0)
                {
                    character.AquireMoney(pickupResult.totalAmount);
                    Console.WriteLine($"Finally {character.GetCharacterName()} {character.GetCharacterLevel()} has coin : {character.GetCharacterMoney()}, exp : {character.GetCharacterExp()} ");
                }
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

        private void monster_Click(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            if (p == null) return;

            lastClickedMonster = p.Tag as Monster;

            // PictureBox의 화면 좌표 구하기
            Point screenPoint = p.PointToScreen(new Point(p.Width / 2, p.Height / 2));

            // 컨텍스트 메뉴 화면 위치에 표시
            monsterContextMenu.Show(screenPoint);
        }

        private void TestForm_MouseDown(object sender, MouseEventArgs e)
        {
            // PictureBox 안에 있는 몬스터
            foreach (var pb in monsterPictureBoxes)
            {
                if (pb.Bounds.Contains(e.Location))
                {
                    Console.Write("!");
                    lastClickedMonster = pb.Tag as Monster;
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
            lastClickedMonster.MonsterGetAttack(100, character);
            this.Invalidate();
        }
    }
}
