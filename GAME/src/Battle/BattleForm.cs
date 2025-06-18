using Game.BaseMonster;
using Game.Characters;
using Game.Monsters;
using System;
using System.Drawing;
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
                
                // 캐릭터 죽은 로직
                if (parentForm is FirstMap firstMapForm)
                {
                    firstMapForm.firstMap.opponentCharacters.Remove(targetCharacter);
                }
            }
        }





        public void DropCoin(Monster monster)
        {
            PictureBox coinBox = new PictureBox();
            coinBox.Image = Properties.Resources.CoinFront;
            coinBox.SizeMode = PictureBoxSizeMode.StretchImage;
            coinBox.Size = new Size(32, 32);
            coinBox.BackColor = Color.Transparent;
            coinBox.Location = new Point(monster.MonsterLocation.x, monster.MonsterLocation.y);

            // 클릭 이벤트 추가: 코인을 주움
            coinBox.Click += (s, e) =>
            {
                int coinValue = monster.MonsterCoinValue;
                myCharacter.AquireMoney(coinValue);

                parentForm.Invoke(new Action(() =>
                {
                    // 코인 제거
                    parentForm.Controls.Remove(coinBox);
                    coinBox.Dispose();

                    // 라벨 생성
                    Label coinLabel = new Label();
                    coinLabel.Text = $"+{coinValue}";
                    coinLabel.Font = new Font("Arial", 14, FontStyle.Bold);
                    coinLabel.ForeColor = Color.Gold;
                    coinLabel.BackColor = Color.Transparent;
                    coinLabel.AutoSize = true;
                    coinLabel.Location = new Point(monster.MonsterLocation.x, monster.MonsterLocation.y - 20);

                    parentForm.Controls.Add(coinLabel);
                    coinLabel.BringToFront();

                    // 1초 후 라벨 제거
                    Timer labelTimer = new Timer();
                    labelTimer.Interval = 1000;
                    labelTimer.Tick += (sender2, e2) =>
                    {
                        labelTimer.Stop();
                        labelTimer.Dispose();

                        parentForm.Invoke(new Action(() =>
                        {
                            parentForm.Controls.Remove(coinLabel);
                            coinLabel.Dispose();
                        }));
                    };
                    labelTimer.Start();
                }));
            };

            // 폼에 코인 추가
            parentForm?.Invoke(new Action(() =>
            {
                parentForm.Controls.Add(coinBox);
                coinBox.BringToFront();
            }));

            // 자동 제거 타이머
            Timer removeTimer = new Timer();
            removeTimer.Interval = 10000;
            removeTimer.Tick += (s, e) =>
            {
                removeTimer.Stop();
                removeTimer.Dispose();

                parentForm?.Invoke(new Action(() =>
                {
                    if (coinBox.Parent != null)
                    {
                        parentForm.Controls.Remove(coinBox);
                        coinBox.Dispose();
                    }
                }));
            };
            removeTimer.Start();
        }

        public void ShowExpLabel(Monster monster)
        {
            int expValue = monster.MonsterExperience;
            Label expLabel = new Label();
            expLabel.Text = $"+{expValue} XP";
            expLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            expLabel.ForeColor = Color.DodgerBlue;
            expLabel.BackColor = Color.Transparent;
            expLabel.AutoSize = true;
            expLabel.Location = new Point(monster.MonsterLocation.x, monster.MonsterLocation.y - 40);

            parentForm.Invoke(new Action(() =>
            {
                parentForm.Controls.Add(expLabel);
                expLabel.BringToFront();
            }));

            Timer expTimer = new Timer();
            expTimer.Interval = 3000;
            expTimer.Tick += (s, e) =>
            {
                expTimer.Stop();
                expTimer.Dispose();

                parentForm.Invoke(new Action(() =>
                {
                    parentForm.Controls.Remove(expLabel);
                    expLabel.Dispose();
                }));
            };
            expTimer.Start();
        }

        public void AttackMonster(int damage)
        {
            targetMonster.MonsterGetAttack(damage, myCharacter);
            setBattleStatus();

            if (targetMonster.IsDead)
            {
                ShowExpLabel(targetMonster); // 경험치 라벨 표시
                DropCoin(targetMonster); // ← 코인 드랍
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
                damage = DefenseCharacter();
            }

            if (targetMonster != null)
            {
                damage = DefenseMonster();
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

        public int DefenseCharacter()
        {
            // 상대 캐릭터로 이미지 변경
            myCharacter.Defense(targetCharacter.Attack());
            GetMotionImage(targetCharacter);

            return targetCharacter.GetCharacterAttack();
        }


        public void GetMotionImage(object target)
        {
            Type type = target.GetType();

            if (typeof(Game.BaseMonster.Monster).IsAssignableFrom(type))
            {
                Player2Character.BackColor = Color.Transparent;

                if (type == typeof(Goblin)) Player2Character.Image = Properties.Resources.goblin_attack;
                else if (type == typeof(Slime))  Player2Character.Image = Properties.Resources.slime_attack;
                else if (type == typeof(Scorpion)) Player2Character.Image = Properties.Resources.scorpion_attack;
                else if (type == typeof(Witch)) Player2Character.Image = Properties.Resources.wizard_attack;
                else if (type == typeof(Orc)) Player2Character.Image =  Properties.Resources.orc_attack;
            }
            else if (typeof(Game.Characters.Character).IsAssignableFrom(type))
            {
                // @ 캐릭터 이미지 수정할 것
                Player2Character.Image = Properties.Resources.AttackPlayer2;
            }

            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += (s, e2) =>
            {
                initBattle(target);
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }


        public int DefenseMonster()
        {
            GetMotionImage(targetMonster);
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

        private void Player2Character_Click(object sender, EventArgs e)
        {

        }
    }
}
