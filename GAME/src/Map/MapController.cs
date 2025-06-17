using Game.BaseMonster;
using Game.Characters;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.ComponentModel;

namespace WindowsFormsApp1.MapControls
{
    public class MapController
    {
        // 캐릭터
        private readonly Character character;
        private Image characterImage = Properties.Resources.Player1Character;
        // 지도
        private readonly Game.Maps.Map map;
        private readonly Form form;
        // contextMenu
        private readonly ContextMenuStrip monsterContextMenu;
        private ToolStripMenuItem attackMenuItem;
        // 클릭 몬스터
        private Monster lastClickedMonster;

        public MapController(Character character, Game.Maps.Map map, Form form)
        {
            this.character = character;
            this.map = map;
            this.form = form;

            monsterContextMenu = new ContextMenuStrip();
            InitializeMonsterContextMenu();
        }

        // 캐릭터 이동 및 코인 습득 처리
        public void HandleMovement(Keys key)
        {
            int moveAmount = 20;
            var current = character.GetCharacterLocation();
            var target = current;

            switch (key)
            {
                case Keys.W: target = (current.x, current.y - moveAmount); break;
                case Keys.S: target = (current.x, current.y + moveAmount); break;
                case Keys.A: target = (current.x - moveAmount, current.y); break;
                case Keys.D: target = (current.x + moveAmount, current.y); break;
                default: return;
            }

            character.MoveLocation(target.x - current.x, target.y - current.y);

            var pickupResult = map.PickUpCoins(character.GetCharacterLocation());
            if (pickupResult.totalAmount > 0)
            {
                character.AquireMoney(pickupResult.totalAmount);
                Console.WriteLine($"Finally {character.GetCharacterName()} {character.GetCharacterLevel()}lvl has coin: {character.GetCharacterMoney()}, exp: {character.GetCharacterExp()}");
            }

            form.Invalidate();
        }

        // 몬스터 컨텍스트 메뉴 초기화
        private void InitializeMonsterContextMenu()
        {
            monsterContextMenu.Items.Add("정보 확인하기", null, OnInfoClicked);

            attackMenuItem = new ToolStripMenuItem("공격하기", null, OnAttackClicked);
            monsterContextMenu.Items.Add(attackMenuItem);

            monsterContextMenu.Opening += MonsterContextMenu_Opening;
        }

        // 폼에서 몬스터 우클릭 시 호출
        public void ShowMonsterContextMenu(Control control, Monster monster, Point location)
        {
            lastClickedMonster = monster;
            monsterContextMenu.Show(control, location);
        }

        // 몬스터 정보 확인
        private void OnInfoClicked(object sender, EventArgs e)
        {
            MessageBox.Show($"{lastClickedMonster.MonsterName} - HP: {lastClickedMonster.MonsterHp}");
        }

        // 몬스터 공격 처리
        private void OnAttackClicked(object sender, EventArgs e)
        {
            lastClickedMonster.MonsterGetAttack(100, character);
            form.Invalidate();

            var battleForm = new BattleForm(character, lastClickedMonster);
            battleForm.Show();
        }

        // 컨텍스트 메뉴 열릴 때 거리 기반 공격 메뉴 활성화
        private void MonsterContextMenu_Opening(object sender, CancelEventArgs e)
        {
            Point characterPosition = new Point(character.GetCharacterLocation().x, character.GetCharacterLocation().y);
            Point monsterPosition = new Point(lastClickedMonster.MonsterLocation.x, lastClickedMonster.MonsterLocation.y);

            double distance = Math.Sqrt(Math.Pow(monsterPosition.X - characterPosition.X, 2) +
                                        Math.Pow(monsterPosition.Y - characterPosition.Y, 2));

            attackMenuItem.Enabled = distance <= 60;
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
    }
}
