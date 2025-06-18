using Game.BaseMonster;
using Game.Characters;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.ComponentModel;
using WindowsFormsApp1.Map;
using WindowsFormsApp1.Battle;
using WindowsFormsApp1.Characters;

namespace WindowsFormsApp1.MapControls
{
    public class MapController
    {
        // 지도
        private readonly Game.Maps.Map map;
        private readonly Form form;

        // 캐릭터
        private readonly Character character;
        private Image characterImage = Properties.Resources.Player1Character_right;
        // contextMenu(캐릭터)
        private ContextMenuStrip CharacterContextMenu;
        private ToolStripMenuItem SaveContextMunu;

        // 다른 캐릭터
        private Character lastClickedOpponent;
        private Image opponentImage = Properties.Resources.Player2Character;
        private ContextMenuStrip OpponentContextMenu;
        private ToolStripMenuItem opponentAttackMenuItem;


        // contextMenu(몬스터)
        private Monster lastClickedMonster;
        private readonly ContextMenuStrip monsterContextMenu;
        private ToolStripMenuItem monsterAttackMenuItem;

        

        public MapController(Character character, Game.Maps.Map map, Form form)
        {
            this.character = character;
            this.map = map;
            this.form = form;

            monsterContextMenu = new ContextMenuStrip();
            CharacterContextMenu = new ContextMenuStrip();
            OpponentContextMenu = new ContextMenuStrip();

            InitializeMonsterContextMenu();
            InitializeCharacterContextMenu();
            InitializeOpponentContextMenu();
        }

        // 캐릭터 이동
        public void HandleMovement(Keys key)
        {
            int moveAmount = 20;
            var current = character.GetCharacterLocation();
            var target = current;

            switch (key)
            {
                case Keys.W: target = (current.x, current.y - moveAmount); break;
                case Keys.S: target = (current.x, current.y + moveAmount); break;
                case Keys.A: 
                    target = (current.x - moveAmount, current.y);
                    characterImage = Properties.Resources.Player1Character_left;
                    break;
                case Keys.D: 
                    target = (current.x + moveAmount, current.y);
                    characterImage = Properties.Resources.Player1Character_right;
                    break;
                default: return;
            }

            character.MoveLocation(target.x - current.x, target.y - current.y);

            form.Invalidate();
        }

        // 캐릭터 그리기
        public void DrawCharacter(Graphics g)
        {
            if (characterImage != null && character != null)
            {
                var loc = character.GetCharacterLocation();
                g.DrawImage(characterImage, loc.x, loc.y, 64, 64);
            }
        }

        public void DrawOpponentCharacter(Graphics g)
        {
            foreach(var opponent in map.opponentCharacters)
            {
                var loc = opponent.GetCharacterLocation();
                g.DrawImage(opponentImage, loc.x, loc.y, 64, 64);
            }
        }

        // 캐릭터 컨텍스트 메뉴 초기화
        private void InitializeCharacterContextMenu()
        {
            CharacterContextMenu.Items.Add("정보 확인하기", null, OnInfoClickedCharacter);

            SaveContextMunu = new ToolStripMenuItem("저장하기", null, OnSaveClicked);
            CharacterContextMenu.Items.Add(SaveContextMunu);
        }

        // 캐릭터 클릭
        public void ShowCharacterContextMenu(Control control, Character character, Point location)
        {
            CharacterContextMenu.Show(control, location);
        }

        // 캐릭터 정보 확인
        private void OnInfoClickedCharacter(object sender, EventArgs e)
        {

            form.Invalidate();

            var infoController = new InfoController(form);     // InfoController 인스턴스 생성
            infoController.SetCharacter(character);        // 캐릭터 정보 전달
            infoController.InfoPanel.Location = new Point(250, 50); // 위치 설정 (필요 시)
            infoController.InfoPanel.Visible = true;
            form.Controls.Add(infoController.InfoPanel);             // 폼에 직접 컨트롤 추가
            infoController.BringToFront();
        }

        // 캐릭터 저장
        private void OnSaveClicked(object sender, EventArgs e)
        {
            CharacterStorage.SaveCharacter(character);
            MessageBox.Show("캐릭터 저장 완료!");
        }

        // 다른 캐릭터 컨텍스트 메뉴 초기화
        private void InitializeOpponentContextMenu()
        {
            OpponentContextMenu.Items.Add("정보 확인하기", null, OnInfoClickedOpponent);
            opponentAttackMenuItem = new ToolStripMenuItem("공격하기", null, OnAttackOpponentClicked);
            OpponentContextMenu.Items.Add(opponentAttackMenuItem);
        }

        public void ShowOpponentContextMenu(Control control, Character character, Point location)
        {
            lastClickedOpponent = character;
            OpponentContextMenu.Show(control, location);
            OpponentContextMenu.Opening += OpponentContextMenu_Opening;
        }

        // 캐릭터 정보 확인
        private void OnInfoClickedOpponent(object sender, EventArgs e)
        {
            form.Invalidate();

            var infoController = new InfoController(form);     // InfoController 인스턴스 생성
            infoController.SetOpponenet(lastClickedOpponent);        // 캐릭터 정보 전달
            infoController.InfoPanel.Location = new Point(250, 50); // 위치 설정 (필요 시)
            infoController.InfoPanel.Visible = true;
            form.Controls.Add(infoController.InfoPanel);             // 폼에 직접 컨트롤 추가
            infoController.BringToFront();
        }

        // 캐릭터 공격 처리
        private void OnAttackOpponentClicked(object sender, EventArgs e)
        {
            form.Invalidate();

            var battleForm = new BattleForm(character, lastClickedOpponent, form);
            battleForm.Show();
        }

        // 컨텍스트 메뉴 열릴 때 거리 기반 공격 메뉴 활성화
        private void OpponentContextMenu_Opening(object sender, CancelEventArgs e)
        {
            Point characterPosition = new Point(character.GetCharacterLocation().x, character.GetCharacterLocation().y);
            Point opponentPosition = new Point(lastClickedOpponent.GetCharacterLocation().x, lastClickedOpponent.GetCharacterLocation().y);

            double distance = Math.Sqrt(Math.Pow(opponentPosition.X - characterPosition.X, 2) +
                                        Math.Pow(opponentPosition.Y - characterPosition.Y, 2));

            opponentAttackMenuItem.Enabled = distance <= 60;
        }

        // 몬스터 컨텍스트 메뉴 초기화
        private void InitializeMonsterContextMenu()
        {
            monsterContextMenu.Items.Add("정보 확인하기", null, OnInfoClickedMonster);

            monsterAttackMenuItem = new ToolStripMenuItem("공격하기", null, OnAttackClicked);
            monsterContextMenu.Items.Add(monsterAttackMenuItem);

            monsterContextMenu.Opening += MonsterContextMenu_Opening;
        }

        // 폼에서 몬스터 우클릭 시 호출
        public void ShowMonsterContextMenu(Control control, Monster monster, Point location)
        {
            lastClickedMonster = monster;
            monsterContextMenu.Show(control, location);
        }

        // 몬스터 정보 확인
        private void OnInfoClickedMonster(object sender, EventArgs e)
        {
/*            // 몬스터 
            MessageBox.Show($"{lastClickedMonster.MonsterName} - HP: {lastClickedMonster.MonsterHp}");*/

            form.Invalidate();

            var infoController = new InfoController(form);     
            infoController.SetMonster(lastClickedMonster);       
            infoController.InfoPanel.Location = new Point(250, 50); 
            infoController.InfoPanel.Visible = true;
            form.Controls.Add(infoController.InfoPanel);           
            infoController.BringToFront();

        }

        // 몬스터 공격 처리
        private void OnAttackClicked(object sender, EventArgs e)
        {
            form.Invalidate();

            var battleForm = new BattleForm(character, lastClickedMonster, form);
            battleForm.Show();
        }

        // 컨텍스트 메뉴 열릴 때 거리 기반 공격 메뉴 활성화
        private void MonsterContextMenu_Opening(object sender, CancelEventArgs e)
        {
            Point characterPosition = new Point(character.GetCharacterLocation().x, character.GetCharacterLocation().y);
            Point monsterPosition = new Point(lastClickedMonster.MonsterLocation.x, lastClickedMonster.MonsterLocation.y);

            double distance = Math.Sqrt(Math.Pow(monsterPosition.X - characterPosition.X, 2) +
                                        Math.Pow(monsterPosition.Y - characterPosition.Y, 2));

            monsterContextMenu.Enabled = distance <= 60;
        }
    }
}
