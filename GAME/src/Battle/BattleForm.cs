using Game.BaseMonster;
using Game.Characters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Battle;

namespace WindowsFormsApp1
{
    public partial class BattleForm : Form
    {
        Character myCharacter;
        Character targetCharacter;
        Monster targetMonster;

        public BattleForm(Character character, Object target)
        {
            InitializeComponent();

            myCharacter = character;
            initBattle(target);
            setBattleStatus();
        }

        public void setBattleStatus()
        {
            // 본인 정보 업데이트
            Player1Name.Text = myCharacter.GetCharacterName();
            Player1Level.Text = myCharacter.GetCharacterLevel().ToString();
            Player1Hp.Text = myCharacter.GetCharacterHp().ToString();
            Player1Attack.Text = myCharacter.GetCharacterAttack().ToString();


            // 상대 정보 업데이트
            if(targetCharacter != null)
            {
                Player2Name.Text = targetCharacter.GetCharacterName();
                Player2Level.Text = targetCharacter.GetCharacterLevel().ToString();
                Player2Hp.Text = targetCharacter.GetCharacterHp().ToString();
                Player2Attack.Text = targetCharacter.GetCharacterAttack().ToString();
            }

            if (targetMonster != null) 
            {
                Player2Name.Text = targetMonster.MonsterName;
                Player2Level.Text = targetMonster.MonsterLevel.ToString();
                Player2Hp.Text = targetMonster.MonsterHp.ToString();
                Player2Attack.Text = targetMonster.MonsterAttackAbility.ToString();

            }
        }

        public void initBattle(Object target)
        {
            //전투 상대 확인
            if (target is Character)
            {
                targetCharacter = (Character)target;
                Player2Character.Image = Properties.Resources.Player2Character;
            }
            if (target is Monster)
            {
                targetMonster = (Monster)target;

                switch (targetMonster.MonsterName)
                {
                    case "Goblin":
                        Player2Character.Image = Properties.Resources.goblin2;
                        break;
                    case "Slime":
                        Player2Character.Image = Properties.Resources.slime;
                        break;
                    case "Scorpion":
                        Player2Character.Image = Properties.Resources.scorpion;
                        break;
                    case "Witch":
                        Player2Character.Image = Properties.Resources.wizard;
                        break;
                    case "Orc":
                        Player2Character.Image = Properties.Resources.orc;
                        break;
                }
            }
        }

        private void AttackButton_Click(object sender, EventArgs e)
        {
            selectControl myControl = new selectControl(this);
            myControl.AttackPanel.Visible = true;
            myControl.AttackPanel.Location = new Point(200, 200);

            this.Controls.Add(myControl.AttackPanel);
            myControl.AttackPanel.BringToFront();
        }

        public void selectControlButton(Panel childPanel, string selectedItem)
        {
            this.Controls.Remove(childPanel);
            childPanel.Dispose();

            if(selectedItem == "dice")
            {
                DiceControl diceControl = new DiceControl();
                diceControl.AttackPanel.Visible = true;
                diceControl.AttackPanel.Location = new Point(200, 200);

                this.Controls.Add(diceControl.AttackPanel);
                diceControl.AttackPanel.BringToFront();
            }
            else if(selectedItem == "coin")
            {
                CoinControl coinControl = new CoinControl(this);
                coinControl.AttackPanel.Visible = true;
                coinControl.AttackPanel.Location = new Point(200, 200);

                this.Controls.Add(coinControl.AttackPanel);
                coinControl.AttackPanel.BringToFront();
            }
        }
    }
}
