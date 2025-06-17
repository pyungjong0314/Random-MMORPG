using Game.BaseMonster;
using Game.Characters;
using Game.Maps;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using WindowsFormsApp1.MapControls;

namespace WindowsFormsApp1
{
    public partial class TestMapForm : Form
    {
        // 캐릭터 및 맵에 몬스터 생성
        private Character character;
        private Map map = MapFactory.CreateMap(1);
        private MapController controller;
        

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

            controller = new MapController(character, map, this);

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

            this.KeyDown += TestForm_KeyDown;       // 보드 입력(예: W/A/S/D 이동) 이벤트를 연결
            this.DoubleBuffered = true; // 깜빡임 방지
        }

        // 캐릭터 출력
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // 캐릭터 출력
            e.Graphics.DrawImage(characterImage, character.GetCharacterLocation().x, character.GetCharacterLocation().y, 64, 64);
        }


        // 키보드 입력에 따라 캐릭터를 이동시키고 충돌 여부를 판단하는 메서드
        private void TestForm_KeyDown(object sender, KeyEventArgs e)
        {
            controller.HandleMovement(e.KeyCode);
        }

        // 몬스터 PictureBox를 클릭했을 때 컨텍스트 메뉴를 표시하는 메서드
        private void monster_Click(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            if (p == null) return;

            lastClickedMonster = p.Tag as Monster;

            // PictureBox 중심 좌표 (클라이언트 기준)
            Point screenPoint = p.PointToScreen(new Point(p.Width / 2, p.Height / 2));
            Point clientPoint = this.PointToClient(screenPoint);

            controller.ShowMonsterContextMenu(this, lastClickedMonster, clientPoint);
        }

    }
}