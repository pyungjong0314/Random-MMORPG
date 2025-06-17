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
using WindowsFormsApp1.Battle.BattlePanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace WindowsFormsApp1
{
    public partial class BattleForm : Form
    {
        Character myCharacter;
        Character targetCharacter;
        Monster targetMonster;
        Form parentForm;

        public BattleForm(Character character, Object target, Form parentForm)
        {
            InitializeComponent();

            myCharacter = character;
            initBattle(target);
            setBattleStatus();
            this.parentForm = parentForm;
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

        // 공격 방법 선택
        public void selectControlButton(Panel childPanel, string selectedItem)
        {
            this.Controls.Remove(childPanel);
            childPanel.Dispose();

            if(selectedItem == "dice")
            {
                DiceControl diceControl = new DiceControl(this);
                diceControl.DicePanel.Visible = true;
                diceControl.DicePanel.Location = new Point(200, 200);

                this.Controls.Add(diceControl.DicePanel);
                diceControl.DicePanel.BringToFront();
            }
            else if(selectedItem == "coin")
            {
                CoinControl coinControl = new CoinControl(this);
                coinControl.CoinPanel.Visible = true;
                coinControl.CoinPanel.Location = new Point(200, 200);

                this.Controls.Add(coinControl.CoinPanel);
                coinControl.CoinPanel.BringToFront();
            }
        }

        // 방어
        public void Deffense()
        {
            this.AttackButton.Enabled = false;
            MessageBox.Show("공격을 받았습니다.");
            myCharacter.Defense(targetMonster.MonsterAttackAbility);

            if (myCharacter.GetCharacterHp() <= 0)
            {
                CharacterDie();
            }

            this.AttackButton.Enabled = true;
        }

        // 코인 공격 성공 처리
        public void CoinAttackButtonSuccess(Panel childPanel, string coin)
        {
            this.Controls.Remove(childPanel);

            SuccessFailure successFailure = new SuccessFailure();
            successFailure.SuccessFailurePanel.Visible = true;
            successFailure.SuccessFailurePanel.Location = new Point(200, 200);
            successFailure.SuccessFailureLabel.Text = "공격 성공";

            if (coin == "front")
            {
                successFailure.ResultImage.Image = Properties.Resources.CoinFront;
            }
            else if (coin == "back")
            {
                successFailure.ResultImage.Image = Properties.Resources.CoinBack;
            }

            this.Controls.Add(successFailure.SuccessFailurePanel);
            successFailure.SuccessFailurePanel.BringToFront();


            int damage = myCharacter.GetCharacterAttack() * 2;
            targetMonster.MonsterGetAttack(damage, myCharacter);

            setBattleStatus();

            if (targetMonster.IsDead)
            {
                upDateImage();
                Respawn(targetMonster);
            }
            else
            {
                Deffense();
                setBattleStatus();
            }
        }

        // 코인 공격 실패 처리
        public void CoinAttackButtonFail(Panel childPanel, string coin)
        {
            this.Controls.Remove(childPanel);

            SuccessFailure successFailure = new SuccessFailure();
            successFailure.SuccessFailurePanel.Visible = true;
            successFailure.SuccessFailurePanel.Location = new Point(200, 200);
            successFailure.SuccessFailureLabel.Text = "공격 실패";

            if (coin == "front")
            {
                successFailure.ResultImage.Image = Properties.Resources.CoinFront;
            }
            else if (coin == "back")
            {
                successFailure.ResultImage.Image = Properties.Resources.CoinBack;
            }

            this.Controls.Add(successFailure.SuccessFailurePanel);
            successFailure.SuccessFailurePanel.BringToFront();

            int damage = myCharacter.GetCharacterAttack() / 2;
            targetMonster.MonsterGetAttack(damage, myCharacter);


            setBattleStatus();

            if (targetMonster.IsDead)
            {
                upDateImage();
                Respawn(targetMonster);

            }
            else
            {
                Deffense();
                setBattleStatus();
            }
        }

        // 주사위 공격 성공
        public void DiceAttackButtonSuccess(Panel childPanel, int dice)
        {
            this.Controls.Remove(childPanel);

            SuccessFailure successFailure = new SuccessFailure();
            successFailure.SuccessFailurePanel.Visible = true;
            successFailure.SuccessFailurePanel.Location = new Point(200, 200);
            successFailure.SuccessFailureLabel.Text = "공격 성공";

            switch (dice)
            {
                case 0:
                    successFailure.ResultImage.Image = Properties.Resources.Dice1;
                    break;
                case 1:
                    successFailure.ResultImage.Image = Properties.Resources.Dice2;
                    break;
                case 2:
                    successFailure.ResultImage.Image = Properties.Resources.Dice3;
                    break;
                case 3:
                    successFailure.ResultImage.Image = Properties.Resources.Dice4;
                    break;
                case 4:
                    successFailure.ResultImage.Image = Properties.Resources.Dice5;
                    break;
                case 5:
                    successFailure.ResultImage.Image = Properties.Resources.Dice6;
                    break;
            }

            this.Controls.Add(successFailure.SuccessFailurePanel);
            successFailure.SuccessFailurePanel.BringToFront();

            int damage = myCharacter.GetCharacterAttack() * 6;
            targetMonster.MonsterGetAttack(damage, myCharacter);


            setBattleStatus();

            if (targetMonster.IsDead)
            {
                upDateImage();
                Respawn(targetMonster);
            }
            else
            {
                Deffense();
                setBattleStatus();
            }
        }

        // 주사위 공격 실패
        public void DiceAttackButtonFail(Panel childPanel, int dice)
        {
            this.Controls.Remove(childPanel);

            SuccessFailure successFailure = new SuccessFailure();
            successFailure.SuccessFailurePanel.Visible = true;
            successFailure.SuccessFailurePanel.Location = new Point(200, 200);
            successFailure.SuccessFailureLabel.Text = "공격 실패";

            switch (dice)
            {
                case 0:
                    successFailure.ResultImage.Image = Properties.Resources.Dice1;
                    break;
                case 1:
                    successFailure.ResultImage.Image = Properties.Resources.Dice2;
                    break;
                case 2:
                    successFailure.ResultImage.Image = Properties.Resources.Dice3;
                    break;
                case 3:
                    successFailure.ResultImage.Image = Properties.Resources.Dice4;
                    break;
                case 4:
                    successFailure.ResultImage.Image = Properties.Resources.Dice5;
                    break;
                case 5:
                    successFailure.ResultImage.Image = Properties.Resources.Dice6;
                    break;
            }

            this.Controls.Add(successFailure.SuccessFailurePanel);
            successFailure.SuccessFailurePanel.BringToFront();

            int damage = myCharacter.GetCharacterAttack() / 6;
            targetMonster.MonsterGetAttack(damage, myCharacter);


            setBattleStatus();

            if (targetMonster.IsDead)
            {
                upDateImage();
                Respawn(targetMonster);
            }
            else
            {
                Deffense();
                
                setBattleStatus();
            }
        }

        public void upDateImage()
        {
            if (parentForm is FirstMap firstMapForm)
            {
                firstMapForm.UpdateMonsterBuffer();
                firstMapForm.Invalidate();
                this.Close();
            }
        }

        public void Respawn(Monster monster)
        {
            Timer timer = new Timer();
            timer.Interval = 3000; // 3초

            timer.Tick += (sender, e) =>
            {
                timer.Stop();
                timer.Dispose();

                monster.IsDead = false;
                monster.SetHp(10);

                // 몬스터 버퍼 다시 그리기
                if (parentForm is FirstMap firstMapForm)
                {
                    firstMapForm.UpdateMonsterBuffer();
                    firstMapForm.Invalidate();
                }
            };

            timer.Start();
        }

        public void CharacterDie()
        {
            MainForm main = new MainForm();
            main.Show();
            this.Close();
        }
    }
}
