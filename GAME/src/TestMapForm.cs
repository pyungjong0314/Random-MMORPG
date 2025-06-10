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

namespace WindowsFormsApp1
{
    public partial class TestMapForm : Form
    {
        private Character character;
        private Map map = MapFactory.CreateMap(1);

        private Image characterImage = Properties.Resources.Player1Character;
        private Image goblinImage = Properties.Resources.goblin2;
        private Image scorpionImage = Properties.Resources.scorpion;
        private Image wizardImage = Properties.Resources.wizard;

        private ContextMenuStrip monsterContextMenu;
        private ToolStripMenuItem attackMenuItem;
        private Point lastClickedMonsterLocation;

        public TestMapForm(Character InitCharacter)
        {
            InitializeComponent();
            character = InitCharacter;

            this.Invalidate();


            InitializeMonsterContextMenu();
            this.MouseDown += TestForm_MouseDown;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.DrawImage(characterImage, character.GetCharacterLocation().x, character.GetCharacterLocation().y, 64, 64);

            foreach (var monsterLocation in map.MonsterLocations["first_base_monster"])
            {
                e.Graphics.DrawImage(goblinImage, monsterLocation.x, monsterLocation.y, 80, 80);
            }

            foreach (var monsterLocation in map.MonsterLocations["second_base_monster"])
            {
                e.Graphics.DrawImage(wizardImage, monsterLocation.x, monsterLocation.y, 100, 100);
            }
        }


        private void TestForm_KeyDown(object sender, KeyEventArgs e)
        {
            int moveAmount = 5;  // 한 번에 이동할 픽셀 수

            switch (e.KeyCode)
            {
                // 상호작용
                case Keys.Space:
                    break;
                case Keys.W:
                    character.MoveLocation(0, -moveAmount);
                    break;
                case Keys.S:
                    character.MoveLocation(0, moveAmount);
                    break;
                case Keys.A:
                    character.MoveLocation(-moveAmount, 0);
                    break;
                case Keys.D:
                    character.MoveLocation(moveAmount, 0);
                    break;
            }

            this.Invalidate();  // 위치 변경 후 다시 그리기 요청
        }




        /*
         * Click
         */

        // 초기화 (폼 생성자 또는 Load 시 호출)
        private void InitializeMonsterContextMenu()
        {
            monsterContextMenu = new ContextMenuStrip();

            // 메뉴 항목 추가
            monsterContextMenu.Items.Add("정보 확인하기", null, OnInfoClicked);

            attackMenuItem = new ToolStripMenuItem("공격하기", null, OnAttackClicked);
            monsterContextMenu.Items.Add(attackMenuItem);

            // ContextMenu 열릴 때마다 거리 계산
            monsterContextMenu.Opening += MonsterContextMenu_Opening;
        }


        // MouseDown에서 몬스터 클릭 감지
        private void TestForm_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (var location in map.MonsterLocations["first_base_monster"])
            {
                Rectangle monsterRect = new Rectangle(location.x, location.y, 64, 64);

                if (monsterRect.Contains(e.Location))
                {
                    // 클릭한 몬스터 위치 저장
                    lastClickedMonsterLocation = new Point(location.x, location.y);

                    // ContextMenu 표시
                    monsterContextMenu.Show(this, e.Location);
                    break;
                }
            }
        }

        // ContextMenu 열릴 때 거리 확인
        private void MonsterContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 현재 캐릭터 위치 가져오기
            Point characterPosition = new Point(character.GetCharacterLocation().x, character.GetCharacterLocation().y);

            // 거리 계산
            double distance = Math.Sqrt(Math.Pow(lastClickedMonsterLocation.X - characterPosition.X, 2) +
                                        Math.Pow(lastClickedMonsterLocation.Y - characterPosition.Y, 2));

            double attackRange = 60; // 공격 가능 거리 설정 (원하는 값으로 수정 가능)

            // 공격 가능 여부에 따라 메뉴 활성/비활성화
            attackMenuItem.Enabled = distance <= attackRange;
        }

        // "정보 확인하기" 클릭 시 처리
        private void OnInfoClicked(object sender, EventArgs e)
        {
            MessageBox.Show("몬스터 정보 표시");
        }

        // "공격하기" 클릭 시 처리
        private void OnAttackClicked(object sender, EventArgs e)
        {
            BattleForm battleForm = new BattleForm();
            battleForm.Show();
        }
    }
}
