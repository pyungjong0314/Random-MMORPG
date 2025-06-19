using Game.BaseMonster;
using Game.Characters;
using Game.Monsters;
using GameClientLib;
using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.Battle;
using WindowsFormsApp1.Battle.BattlePanel;
using WindowsFormsApp1.Properties;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using Game.Audio;

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


            DamageLabel.Font = new Font("궁서", 22, FontStyle.Bold);

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
            Player1Attack.Text = myCharacter.Attack().ToString();


            // 상대 정보 업데이트
            if (targetCharacter != null)
            {
                Player2Name.Text = targetCharacter.GetCharacterName();
                Player2Level.Text = targetCharacter.GetCharacterLevel().ToString();
                Player2Hp.Text = targetCharacter.GetCharacterHp().ToString();
                Player2Attack.Text = targetCharacter.Attack().ToString();
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
                
            DamageLabel.Location = new Point(Player2Character.Location.X+30, Player2Character.Location.Y - 30);

            this.Controls.Add(myControl.AttackPanel);
            myControl.AttackPanel.BringToFront();
        }

        private void DefenseButton_Click(object sender, EventArgs e)
        {
            int damage = Deffense();
            setBattleStatus();

            DamageLabel.Text = "-" + damage.ToString();
            DamageLabel.Location = new Point(Player1Character.Location.X+30, Player1Character.Location.Y - 30);
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
            int damage = myCharacter.Attack() * 2;

            SuccessFailure successFailure = new SuccessFailure(this, damage);
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
        }

        // 코인 공격 실패 처리
        public void CoinAttackButtonFail(Panel childPanel, string coin)
        {
            this.Controls.Remove(childPanel);
            int damage = myCharacter.Attack() / 2;

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
            int damage = myCharacter.Attack() * 6;

            SuccessFailure successFailure = new SuccessFailure(this, damage);
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
        }

        // 주사위 공격 실패
        public void DiceAttackButtonFail(Panel childPanel, int dice)
        {
            this.Controls.Remove(childPanel);

            int damage = myCharacter.Attack() / 6;

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

                //monster.IsDead = false;
                monster.SetHp(10);

                // 몬스터 버퍼 다시 그리기
                if (parentForm is FirstMap firstMap)
                {
                    firstMap.UpdateMonsterBuffer();
                    firstMap.Invalidate();
                    firstMap.clientBattle.CmdMonsterRespwanAsync(2, monster.MonsterId);
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
            BlinkImage(Player2Character); // ← 내 캐릭터 깜빡이기


            setBattleStatus();

            if(targetCharacter.GetCharacterHp() <= 0)
            {
                PlayerVictory();
                upDateImage();
                ShowDeadImage(targetCharacter);

                // 캐릭터 죽은 로직
                if (parentForm is FirstMap firstMapForm)
                {
                    lock (firstMapForm._opponentLock)
                    {
                        firstMapForm.firstMap.opponentCharacters.Remove(targetCharacter);
                    }
                }
            }
        }

        public void ShowDeadImage(object target)
        {
            if (target == null) return;

            PictureBox deadImage = new PictureBox();
            deadImage.SizeMode = PictureBoxSizeMode.StretchImage;
            deadImage.Size = new Size(64, 64);
            deadImage.BackColor = Color.Transparent;

            Point location;

            if (target is Monster monster)
            {
                location = new Point(monster.MonsterLocation.x, monster.MonsterLocation.y);

                if (monster is Slime) deadImage.Image = Properties.Resources.slime_dead;
                else if (monster is Goblin) deadImage.Image = Properties.Resources.goblin_dead;
                else if (monster is Scorpion) deadImage.Image = Properties.Resources.scorpion_dead;
                else if (monster is Witch) deadImage.Image = Properties.Resources.wizard_dead;
                else if (monster is Orc) deadImage.Image = Properties.Resources.orc_dead;
            }
            else if (target is Character character)
            {
                location = new Point(character.characterLocation.x, character.characterLocation.y+20);
                deadImage.Image = Properties.Resources.character_dead;
            }
            else return;

            deadImage.Location = location;

            parentForm?.Invoke(new Action(() =>
            {
                parentForm.Controls.Add(deadImage);
                deadImage.BringToFront();
            }));

            Timer removeTimer = new Timer();
            removeTimer.Interval = 3000;
            removeTimer.Tick += (s, e) =>
            {
                removeTimer.Stop();
                removeTimer.Dispose();

                parentForm?.Invoke(new Action(() =>
                {
                    if (deadImage != null && !deadImage.IsDisposed)
                    {
                        parentForm.Controls.Remove(deadImage);
                        deadImage.Dispose();
                    }
                }));
            };
            removeTimer.Start();
        }

        public void AttackMonster(int damage)
        {
            targetMonster.MonsterGetAttack(damage, myCharacter);
            BlinkImage(Player2Character);

            setBattleStatus();

            if (targetMonster.IsDead)
            {
                ShowDeadImage(targetMonster);
                ShowExpLabel(targetMonster); // 경험치 라벨 표시
                DropCoin(targetMonster); // ← 코인 드랍
                //PlayerVictory();
                this.Close();
                upDateImage();
                Respawn(targetMonster);

                // 죽었다고 알려주기
                if (parentForm is FirstMap firstMap)
                {
                    firstMap.clientBattle.CmdMonsterKillAsync(2, targetMonster.MonsterId);
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
            coinBox.Location = new Point(monster.MonsterLocation.x, monster.MonsterLocation.y-20);

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
                    coinLabel.Text = $"+{coinValue} Coins";
                    coinLabel.Font = new Font("Arial", 14, FontStyle.Bold);
                    coinLabel.ForeColor = Color.Gold;
                    coinLabel.BackColor = Color.Transparent;
                    coinLabel.AutoSize = true;
                    coinLabel.Location = new Point(monster.MonsterLocation.x, monster.MonsterLocation.y);

                    parentForm.Controls.Add(coinLabel);
                    coinLabel.BringToFront();

                    // 1초 후 라벨 제거
                    Timer labelTimer = new Timer();
                    labelTimer.Interval = 1000;
                    labelTimer.Tick += (sender2, e2) =>
                    {
                        labelTimer.Stop();
                        labelTimer.Dispose();

                        if (parentForm != null && !parentForm.IsDisposed && parentForm.IsHandleCreated)
                        {
                            parentForm.Invoke(new Action(() =>
                            {
                                parentForm.Controls.Remove(coinLabel);
                                coinLabel.Dispose();
                            }));
                        }
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
                    if (coinBox?.IsDisposed == false && coinBox.Parent != null)
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
            expLabel.ForeColor = Color.Blue;
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

        // 방어
        public int Deffense()
        {
            int damage = 0;
            // 여기에 소켓 통신

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
            BlinkImage(Player1Character); // ← 내 캐릭터 깜빡이기

            return targetCharacter.GetCharacterAttack();
        }


        private void BlinkImage(PictureBox target, int blinkCount = 3, int interval = 100)
        {
            int count = 0;
            Timer blinkTimer = new Timer();
            blinkTimer.Interval = interval;

            blinkTimer.Tick += (s, e) =>
            {
                target.Visible = !target.Visible;
                count++;

                if (count >= blinkCount * 2)
                {
                    blinkTimer.Stop();
                    blinkTimer.Dispose();
                    target.Visible = true;
                }
            };

            blinkTimer.Start();
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
            myCharacter.Defense(targetMonster.MonsterAttackAbility);
            GetMotionImage(targetMonster);
            BlinkImage(Player1Character);

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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
