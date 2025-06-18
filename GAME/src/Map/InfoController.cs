using Game.BaseMonster;
using Game.Characters;
using Game.MonsterManagers;
using System;
using System.Reflection.Emit;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1.Map
{
    public partial class InfoController : UserControl
    {
        private Character myCharacter;
        private Character opponent;
        private Monster myMonster; 
        private Form parentForm;

        public InfoController(Form form)
        {
            InitializeComponent();
            parentForm = form;
        }
    

        // 캐릭터를 전달받아 내부에 저장하고 UI 갱신
        public void SetCharacter(Character character)
        {
            myCharacter = character;
            UpdateCharacterInfo();
        }


        // 화면에 캐릭터 정보 출력
        private void UpdateCharacterInfo()
        {
            if (myCharacter == null) return;

            // 예: 라벨이 있다고 가정 (디자이너에서 Label 컨트롤 추가 필요)
            lbName.Text = myCharacter.GetCharacterName();
            lbLevel.Text = "Lv. " + myCharacter.GetCharacterLevel().ToString();
            lbHealth.Text = "HP: " + myCharacter.GetCharacterHp().ToString();
            lbAttack.Text = "ATK: " + myCharacter.Attack().ToString();
            lbCoin.Text = "Gold: " + myCharacter.GetCharacterMoney().ToString();
        }

        // 다른 캐릭터 정보
        public void SetOpponenet(Character character)
        {
            opponent = character;
            UpdateOpponentInfo();
        }
        // 화면에 상대 정보 출력
        private void UpdateOpponentInfo()
        {
            if (opponent == null) return;

            pbInfo.BackgroundImage = Properties.Resources.Player2Character;

            // 예: 라벨이 있다고 가정 (디자이너에서 Label 컨트롤 추가 필요)
            lbName.Text = opponent.GetCharacterName();
            lbLevel.Text = "Lv. " + opponent.GetCharacterLevel().ToString();
            lbHealth.Text = "HP: " + opponent.GetCharacterHp().ToString();
            lbAttack.Text = "ATK: " + opponent.Attack().ToString();
            lbCoin.Text = "Gold: " + opponent.GetCharacterMoney().ToString();
        }


        // 캐릭터를 전달받아 내부에 저장하고 UI 갱신
        public void SetMonster(Monster monster)
        {
            myMonster = monster;
            UpdateMonsterInfo(monster);
        }


        // 화면에 몬스터 정보 출력
        private void UpdateMonsterInfo(Monster monster)
        {
            if (myMonster == null) return;

            pbInfo.BackgroundImage = MonsterManager.CreateImageFromType(monster.GetType());

            lbName.Text = myMonster.GetName();
            lbLevel.Visible = false;
            lbLevelInfo.Visible = false;
            lbHealth.Text = "HP: " + myMonster.GetHp();
            lbAttack.Text = "ATK: " + myMonster.GetAttack();
            lbCoin.Text = "Gold: " + myMonster.GetCoinValue();

            InfoPanel.Visible = true;
        }

        private void lbClose_Click(object sender, EventArgs e)
        {
            parentForm.Controls.Remove(InfoPanel); // null 아닐 때만 제거
            this.Dispose(); // 항상 해제
        }

        private void pbInfo_Click(object sender, EventArgs e)
        {

        }
    }
}
