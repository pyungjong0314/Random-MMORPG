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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace WindowsFormsApp1
{
    public partial class BattleForm : Form
    {
        Character myCharacter;
        Character targetCharacter;
        Monster targetMonster;
        public Form parentForm;

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
            if (targetCharacter != null)
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

        private void DefenseButton_Click(object sender, EventArgs e)
        {
            int damage = Deffense();
            setBattleStatus();

            DamageLabel.Text = damage.ToString();
            DamageLabel.Visible = true;

            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += (s, e2) =>
            {
                DamageLabel.Visible = false;
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }

        // 공격 방법 선택
        public void selectControlButton(Panel childPanel, string selectedItem)
        {
            this.Controls.Remove(childPanel);
            childPanel.Dispose();

            if (selectedItem == "dice")
            {
                DiceControl diceControl = new DiceControl(this);
                diceControl.DicePanel.Visible = true;
                diceControl.DicePanel.Location = new Point(200, 200);

                this.Controls.Add(diceControl.DicePanel);
                diceControl.DicePanel.BringToFront();
            }
            else if (selectedItem == "coin")
            {
                CoinControl coinControl = new CoinControl(this);
                coinControl.CoinPanel.Visible = true;
                coinControl.CoinPanel.Location = new Point(200, 200);

                this.Controls.Add(coinControl.CoinPanel);
                coinControl.CoinPanel.BringToFront();
            }
        }

        // 코인 공격 성공 처리
        public void CoinAttackButtonSuccess(Panel childPanel, string coin)
        {
            this.Controls.Remove(childPanel);
            int damage = myCharacter.GetCharacterAttack() * 2;

            SuccessFailure successFailure = new SuccessFailure(this, damage);
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
        }

        // 코인 공격 실패 처리
        public void CoinAttackButtonFail(Panel childPanel, string coin)
        {
            this.Controls.Remove(childPanel);
            int damage = myCharacter.GetCharacterAttack() / 2;

            SuccessFailure successFailure = new SuccessFailure(this, damage);
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
        }

        // 주사위 공격 성공
        public void DiceAttackButtonSuccess(Panel childPanel, int dice)
        {
            this.Controls.Remove(childPanel);
            int damage = myCharacter.GetCharacterAttack() * 6;

            SuccessFailure successFailure = new SuccessFailure(this, damage);
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
        }

        // 주사위 공격 실패
        public void DiceAttackButtonFail(Panel childPanel, int dice)
        {
            this.Controls.Remove(childPanel);

            int damage = myCharacter.GetCharacterAttack() / 6;

            SuccessFailure successFailure = new SuccessFailure(this, damage);
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
        }

        public void upDateImage()
        {
            if (parentForm is FirstMap firstMapForm)
            {
                firstMapForm.UpdateMonsterBuffer();
                firstMapForm.Invalidate();
            }
        }

        public void Respawn(Monster monster)
        {
            Timer timer = new Timer();
            timer.Interval = 10000; // 10초

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


        // 공격
        public void Attack(int damage)
        {
            if (targetCharacter != null)
            {
                AttackCharacter(damage);
            }

            if (targetMonster != null)
            {
                AttackMonster(damage);
            }
        }

        public void AttackCharacter(int damage)
        {
            targetCharacter.Defense(damage);
            setBattleStatus();

            if(targetCharacter.GetCharacterHp() <= 0)
            {
                PlayerVictory();
                upDateImage();
            }
            // 캐릭터 죽은 로직
            if (parentForm is FirstMap firstMapForm)
            {
                firstMapForm.firstMap.opponentCharacters.Remove(targetCharacter);
            }
        }

        public void AttackMonster(int damage)
        {
            targetMonster.MonsterGetAttack(damage, myCharacter);
            setBattleStatus();

            if (targetMonster.IsDead)
            {
                PlayerVictory();
                upDateImage();
                Respawn(targetMonster);
            }
        }


        // 방어
        public int Deffense()
        {
            int damage = 0;
            if(targetCharacter != null)
            {
                damage = DeffenseCharacter();
            }

            if (targetMonster != null)
            {
                damage = DeffenseMonster();
            }

            // 여기가 캐릭터 뒤짐
            if (myCharacter.GetCharacterHp() <= 0)
            {
                VictoryDefeat victoryDefeat = new VictoryDefeat(this);
                victoryDefeat.VictoryDefeatPanel.Visible = true;
                victoryDefeat.VictoryDefeatPanel.Location = new Point(200, 200);
                victoryDefeat.PlayerImage.Image = Properties.Resources.tombstone;
                victoryDefeat.NameLabel.Text = myCharacter.GetCharacterName();
                victoryDefeat.VictoryDefeatLabel.Text = "패배";

                this.Controls.Add(victoryDefeat.VictoryDefeatPanel);
                victoryDefeat.VictoryDefeatPanel.BringToFront();
            }

            this.AttackButton.Enabled = true;
            this.DefenseButton.Enabled = false;

            return damage;
        }

        public int DeffenseCharacter()
        {
            myCharacter.Defense(targetCharacter.Attack());

            return targetCharacter.GetCharacterAttack();
        }


        public int DeffenseMonster()
        {
            myCharacter.Defense(targetMonster.MonsterAttackAbility);

            return targetMonster.MonsterAttackAbility;
        }

        public void PlayerVictory()
        {
            VictoryDefeat victoryDefeat = new VictoryDefeat(this);
            victoryDefeat.VictoryDefeatPanel.Visible = true;
            victoryDefeat.VictoryDefeatPanel.Location = new Point(200, 200);
            victoryDefeat.PlayerImage.Image = Properties.Resources.trophy;
            victoryDefeat.NameLabel.Text = myCharacter.GetCharacterName();

            this.Controls.Add(victoryDefeat.VictoryDefeatPanel);
            victoryDefeat.VictoryDefeatPanel.BringToFront();
        }
    }
}
