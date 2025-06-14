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





        

        // 타이머 업데이트하기 (리스폰 여부 확인)
        private void StartUpdateTimer()
        {
            PictureBox pb = new PictureBox();


            updateTimer = new Timer();
            updateTimer.Interval = 1000; // 1초마다 호출
            updateTimer.Tick += (s, e) =>
            {
                pb = map.Update();
                
                if (pb != null)
                {
                    this.Controls.Add(pb);
                }
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



        // 테스트 맵 로딩
        private void TestMapForm_Load(object sender, EventArgs e)
        {

            // 1. 모든 몬스터 PictureBox를 가져옴
            monsterPictureBoxes = this.Controls
                .OfType<PictureBox>()
                .Where(pb => pb.Name.StartsWith("monster_"))
                .OrderBy(pb => pb.Name)
                .ToList();


            // 2. 모든 몬스터 객체와 사진 매핑
            for (int i = 0; i < monsterPictureBoxes.Count && i < map.Monsters.Count; i++)
            {
                map.Monsters[i].MonsterLocation.x = monsterPictureBoxes[i].Location.X;
                map.Monsters[i].MonsterLocation.y = monsterPictureBoxes[i].Location.Y;

                monsterPictureBoxes[i].Tag = map.Monsters[i];
                map.Monsters[i].SetForm(this); // ← 현재 폼 전달
            }

        }




        // 캐릭터 기능 초기화 
        public TestMapForm(Character InitCharacter)
        {
            InitializeComponent();
            character = InitCharacter;

            InitializeMonsterContextMenu();
            this.KeyDown += TestForm_KeyDown;       // 보드 입력(예: W/A/S/D 이동) 이벤트를 연결
            this.DoubleBuffered = true; // 깜빡임 방지

            StartUpdateTimer(); // ← 리스폰 활성화!
        }



        // 캐릭터 출력 및 코인 드랍하기
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


        // 키보드 입력에 따라 캐릭터를 이동시키고 충돌 여부를 판단하는 메서드
        private void TestForm_KeyDown(object sender, KeyEventArgs e)
        {
            // 캐릭터 이동 속도 설정
            int moveAmount = 20;

            // 캐릭터 충돌 감지 영역 크기 설정 (가로, 세로)
            int collisionWidth = 10;
            int collisionHeight = 10;

            var current = character.GetCharacterLocation();
            var target = current;

            // 키 입력(WASD)에 따라 이동 목표 좌표 설정
            switch (e.KeyCode)
            {
                case Keys.W: target = (current.x, current.y - moveAmount); break;
                case Keys.S: target = (current.x, current.y + moveAmount); break;
                case Keys.A: target = (current.x - moveAmount, current.y); break;
                case Keys.D: target = (current.x + moveAmount, current.y); break;
            }

            // 이동할 영역을 사각형으로 정의하여 충돌 체크에 사용
            Rectangle targetRect = new Rectangle(target.x, target.y, collisionWidth, collisionHeight);

            // 해당 위치에 보이는 PictureBox가 있으면 이동 차단
            bool isBlocked = this.Controls
                .OfType<PictureBox>()
                .Any(pb => pb.Visible && pb.Bounds.IntersectsWith(targetRect));

            // 이동 가능하면 위치 이동 및 코인 습득 처리
            if (!isBlocked)
            {
                character.MoveLocation(target.x - current.x, target.y - current.y);

                // 캐릭터 위치에서 코인 습득
                var pickupResult = map.PickUpCoins(character.GetCharacterLocation());
                if (pickupResult.totalAmount > 0)
                {
                    character.AquireMoney(pickupResult.totalAmount);
                    Console.WriteLine($"Finally {character.GetCharacterName()} {character.GetCharacterLevel()}lvl has coin: {character.GetCharacterMoney()}, exp: {character.GetCharacterExp()}");
                }

                // 화면 다시 그리기
                this.Invalidate();
            }
        }





        /* ContextMenu 관련 코드 */
        // 몬스터 우클릭 시 사용할 컨텍스트 메뉴 생성
        private void InitializeMonsterContextMenu()
        {
            monsterContextMenu = new ContextMenuStrip();

            monsterContextMenu.Items.Add("정보 확인하기", null, OnInfoClicked);

            attackMenuItem = new ToolStripMenuItem("공격하기", null, OnAttackClicked);
            monsterContextMenu.Items.Add(attackMenuItem);

            monsterContextMenu.Opening += MonsterContextMenu_Opening;
        }


        // 몬스터 체력 정보 출력
        private void OnInfoClicked(object sender, EventArgs e)
        {
            MessageBox.Show($"{lastClickedMonster.MonsterName} - HP: {lastClickedMonster.MonsterHp}");
        }


        // 몬스터 공격
        private void OnAttackClicked(object sender, EventArgs e)
        {
            lastClickedMonster.MonsterGetAttack(100, character);
            this.Invalidate();
        }


        // 몬스터 PictureBox를 클릭했을 때 컨텍스트 메뉴를 표시하는 메서드
        private void monster_Click(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            if (p == null) return;

            lastClickedMonster = p.Tag as Monster;

            // PictureBox 중심 좌표를 화면 기준으로 변환
            Point screenPoint = p.PointToScreen(new Point(p.Width / 2, p.Height / 2));

            // 해당 위치에 컨텍스트 메뉴 표시
            monsterContextMenu.Show(screenPoint);
        }


        // 일정 거리에서 몬스터 공격 기능 활성화
        private void MonsterContextMenu_Opening(object sender, CancelEventArgs e)
        {
            Point characterPosition = new Point(character.GetCharacterLocation().x, character.GetCharacterLocation().y);
            Point monsterPosition = new Point(lastClickedMonster.MonsterLocation.x, lastClickedMonster.MonsterLocation.y);

            double distance = Math.Sqrt(Math.Pow(monsterPosition.X - characterPosition.X, 2) +
                                        Math.Pow(monsterPosition.Y - characterPosition.Y, 2));

            attackMenuItem.Enabled = distance <= 60;
        }

    }
}
