using Game.BaseMonster;
using Game.Monsters;
using System.Collections.Generic;
using System.Drawing;

using Game.Obstacles;

using System.Windows.Forms;
using Game.Characters;
using WindowsFormsApp1.MapControls;
using Game.MonsterManagers;
using Game.BossMonsters;
using System.Threading.Tasks;
using System.Threading;
using System;
using GameClientLib;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Game.Maps;
using Newtonsoft.Json;


namespace WindowsFormsApp1
{
    public partial class FirstMap : Form
    {
        // 통신
        private GameWebSocketClient client;
        public GameWebSocketClient clientStatus;
        public GameWebSocketClient clientBattle;
        private Thread _networkThread;
        private bool _isRunningNetwork = false;
        public readonly object _opponentLock = new object();
        // 전투
        private CancellationTokenSource _listenCancelTokenSource;
        private Task _listenTask;

        // Map
        Image firstMapImg;
        private Character myCharacter;
        private MapController controller;
        public Game.Maps.Map firstMap;

        private Bitmap backgroundBufferBitmap;
        private Bitmap monsterBuffer;

        // 이미지 생성
        private Monster lastClickedMonster;
        private Character lastClickedOpponent;
        private Image opponentImage = Properties.Resources.Player2Character;
        bool shouldUpdateMonsterBufferServer = false;

        public FirstMap(GameWebSocketClient c, Character character)
        {
            InitializeComponent();
            myCharacter = character;
            myCharacter.MoveLocation(-10, -50);

            // 내부 그릴 수 있는 영역 크기
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // 생성할 몬스터 리스트
            List<Monster> monsters = new List<Monster>
            {
                // Slime 10마리
                new Slime { MonsterId = 0, MonsterLocation = (135, 199) },
                new Slime { MonsterId = 1, MonsterLocation = (53, 281)  },
                new Slime { MonsterId = 2, MonsterLocation = (160, 320) },
                new Slime { MonsterId = 3, MonsterLocation = (302, 281) },
                new Slime { MonsterId = 4, MonsterLocation = (157, 461) },
                new Slime { MonsterId = 5, MonsterLocation = (290, 462) },
                new Slime { MonsterId = 6, MonsterLocation = (388, 382) },
                new Slime { MonsterId = 7, MonsterLocation = (461, 485) },
                new Slime { MonsterId = 8, MonsterLocation = (507, 332) },
                new Slime { MonsterId = 9, MonsterLocation = (550, 411) },

                // Goblin 7마리
                new Goblin { MonsterId = 10, MonsterLocation = (779, 499) },
                new Goblin { MonsterId = 11, MonsterLocation = (643, 433) },
                new Goblin { MonsterId = 12, MonsterLocation = (931, 474) },
                new Goblin { MonsterId = 13, MonsterLocation = (708, 332) },
                new Goblin { MonsterId = 14, MonsterLocation = (828, 382) },
                new Goblin { MonsterId = 15, MonsterLocation = (933, 332) },
                new Goblin { MonsterId = 16, MonsterLocation = (933, 332) },
            };

            // 생성할 장애물 리스트
            List<Obstacle> obstacles = new List<Obstacle>
            {
                new Tree { Location = (109, 12) },
                new Tree { Location = (208, 12) },
                new Tree { Location = (307, 12) },
                new Tree { Location = (410, 12) },
                new Tree { Location = (509, 12) },
                new Tree { Location = (208, 84) },
                new Tree { Location = (307, 84) },
                new Tree { Location = (410, 91) },
                new Tree { Location = (509, 91) },
                new Tree { Location = (208, 163) },
                new Tree { Location = (307, 163) },
                new Tree { Location = (410, 170) },
                new Tree { Location = (509, 170) },
                new Tree { Location = (1077, 19) },
                new Tree { Location = (1077, 107) },
                new Tree { Location = (1077, 199) },
                new Tree { Location = (1077, 304) },
                new Tree { Location = (1077, 396) },
                new Tree { Location = (1077, 485) },
                new Tree { Location = (1077, 573) },

                new Rock { Location = (540, 257) },
                new Rock { Location = (639, 257) },
                new Rock { Location = (639, 180) },
                new Rock { Location = (639, 107) },
                new Rock { Location = (639, 42) },
                new Rock { Location = (718, 42) },
                new Rock { Location = (950, 42) },
                new Rock { Location = (950, 114) },
                new Rock { Location = (950, 190) },
                new Rock { Location = (950, 256) },

                new Well { Location = (786, -2) }
            };


            (firstMap, firstMapImg) = Game.MapFactories.MapFactory.CreateMap(monsters, obstacles);

            // 배경 생성
            backgroundBufferBitmap = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            using (Graphics g = Graphics.FromImage(backgroundBufferBitmap))
            {
                g.DrawImage(firstMapImg, 0, 0);
            }

            // 몬스터 생성
            monsterBuffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            UpdateMonsterBuffer();


            // 다른 캐릭터 테스트

            this.DoubleBuffered = true;

            // 키보드 입력
            controller = new MapController(character, firstMap, this);
            this.KeyDown += TestForm_KeyDown;
            this.MouseClick += FirstMap_MouseClick;

            // 이동
            client = c;
            StartNetwork();

        }


        private async void FirstMap_Load(object sender, EventArgs e)
        {
            try
            {
                await InitializeClients();
                Console.WriteLine("Clients initialized");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Init error: " + ex.Message);
            }
        }

        public async Task InitializeClients()
        {
            // 전투 클라이언트
            clientBattle = new GameWebSocketClient();
            await clientBattle.ConnectAsync();
            // 대기 클라이언트
            clientStatus = new GameWebSocketClient();
            await clientStatus.ConnectAsync();

            StartListening();
        }

        // 통신

        // 전투 대기
        private CancellationTokenSource listenCts;
        private Task listeningTask;

        public void StartListening()
        {
            if (listenCts != null && !listenCts.IsCancellationRequested)
                return; // 이미 실행 중

            listenCts = new CancellationTokenSource();
            listeningTask = Task.Run(async () =>
            {
                try
                {
                    while (!listenCts.Token.IsCancellationRequested)
                    {
                        var msg = await clientStatus.ReceiveOnce();
                        ProcessMessage(msg);
                    }
                }
                catch (OperationCanceledException)
                {
                    // 취소됨
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Listening error: " + ex.Message);
                }
            }, listenCts.Token);
        }

        public void StopListening()
        {
            if (listenCts != null)
            {
                listenCts.Cancel();
                listenCts = null;
            }
            if (listeningTask != null)
            {
                listeningTask.Wait();
                listeningTask = null;
            }
        }

        private void ProcessMessage(string msg)
        {
            // 메시지 파싱 및 처리 예시
            var response = JsonConvert.DeserializeObject<CombatResponse>(msg);
            Console.WriteLine($"Received cmd: {response.cmd}, status: {response.status}, message: {response.message}");
            // 추가 로직 삽입 가능
        }

        public async Task StartBattle(Character battleOpponent)
        {
            if (clientBattle == null) throw new InvalidOperationException("Battle client is not initialized");

            string myUid = myCharacter.GetCharacterId().ToString();
            string targetUid = battleOpponent.GetCharacterId().ToString();

            await clientBattle.CmdPvPRequest(myUid, targetUid);
            Console.WriteLine($"Sending PvPRequest: {myUid} vs {targetUid}");

            Console.WriteLine("Waiting for PvP accept (cmd 115)...");
            var acceptMsg = await clientBattle.WaitForCmd(115);
            Console.WriteLine("PvP accepted: " + acceptMsg);

            // 이후 전투 진행 로직 추가 가능
        }


        // 움직임

        public void StartNetwork()
        {
            if (_isRunningNetwork) return;

            _isRunningNetwork = true;
            _networkThread = new Thread(() => NetworkLoop().GetAwaiter().GetResult());
            _networkThread.IsBackground = true;
            _networkThread.Start();
        }

        public void StopNetwork()
        {
            _isRunningNetwork = false;
            _networkThread?.Join();
        }

        private async Task NetworkLoop()
        {
            int frame = 0;

            while (_isRunningNetwork)
            {
                try
                {
                    // 1. 좌표 전송
                    var loc = myCharacter.GetCharacterLocation();
                    await client.CmdMoveAsync(myCharacter.GetCharacterId().ToString(), 2, loc.x, loc.y);

                    // 2. 상태 요청 (10프레임에 1번 = 약 1초마다)
                    var response = await client.CmdAllAsync(2);
                    Console.WriteLine($"[CmdAll] 유저 수: {response.body.players?.Count}");


                    //if(frame % 10 == 0)
                    {
                        if (response.body.monsters != null)
                        {
                            Console.WriteLine($"[CmdAll] 몬스터 데이터: {string.Join(",", response.body.monsters)}");

                            for (int i = 0; i < firstMap.Monsters.Count; i++)
                            {
                                if (firstMap.Monsters[i].IsDead == response.body.monsters[i])
                                {
                                    firstMap.Monsters[i].IsDead = !response.body.monsters[i];
                                    shouldUpdateMonsterBufferServer = true;
                                }
                            }
                        }

                        lock (_opponentLock)
                        {
                            firstMap.opponentCharacters.Clear();

                            foreach (var p in response.body.players)
                            {
                                if (p.characterId == myCharacter.GetCharacterId())
                                    continue;

                                var opponent = new Character.Builder()
                                    .SetCharacterId(p.characterId)
                                    .SetCharacterName(p.characterName)
                                    .SetCharacterLevel(p.characterLevel)
                                    .SetCharacterExp(p.characterExp)
                                    .SetCharacterMoney(p.characterMoney)
                                    .SetCharacterMapId(p.characterMapId)
                                    .SetCharacterLocation(p.characterLocation.x, p.characterLocation.y)
                                    .SetCharacterHp(p.characterHp)
                                    .SetCharacterAttack(p.characterAttack)
                                    .Build();

                                firstMap.opponentCharacters.Add(opponent);
                            }
                        }
                    }

                    frame++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[NetworkLoop] 예외 발생: {ex.Message}");
                    break;
                }

                await Task.Delay(100); // 0.1초마다 실행
            }
        }


        // 이미지 처리
        public void UpdateMonsterBuffer()
        {
            using (Graphics g = Graphics.FromImage(monsterBuffer))
            {
                g.Clear(Color.Transparent);
                foreach (var monster in firstMap.Monsters)
                {
                    if (!monster.IsDead)
                    {
                        Image monsterImg = MonsterManager.CreateImageFromType(monster.GetType());
                        g.DrawImage(monsterImg, monster.MonsterLocation.x, monster.MonsterLocation.y, 64, 64);
                    }
                }
            }
            this.Invalidate();
        }

        public void DrawOpponentCharacter(Graphics g)
        {
            lock (_opponentLock)
            {
                foreach (var opponent in firstMap.opponentCharacters)
                {
                    var loc = opponent.GetCharacterLocation();
                    g.DrawImage(opponentImage, loc.x, loc.y, 64, 64);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // 1. 배경 + 장애물만 포함된 이미지 (firstMapImg)
            e.Graphics.DrawImage(backgroundBufferBitmap, 0, 0);

            // 2. 살아있는 몬스터는 따로 다시 그림
            if (shouldUpdateMonsterBufferServer)
            {
                UpdateMonsterBuffer();
                shouldUpdateMonsterBufferServer = false;
            }
            e.Graphics.DrawImage(monsterBuffer, 0, 0);

            // 3. 캐릭터도 그리기
            controller.DrawCharacter(e.Graphics);

            // 4. 상대 캐릭터
            DrawOpponentCharacter(e.Graphics);
        }


        // 키보드 입력에 따라 캐릭터를 이동 (충돌 여부를 판단)
        private void TestForm_KeyDown(object sender, KeyEventArgs e)
        {
            controller.HandleMovement(e.KeyCode);
        }

        // 몬스터 클릭
        private void FirstMap_MouseClick(object sender, MouseEventArgs e)
        {
            Monster clickedMonster = FindMonsterAtPoint(e.Location);
            Character clickedOpponent = FrindOpponentAtPoint(e.Location);

            // 캐릭터 클릭
            if (FindCharacterAtPoint(e.Location))
                controller.ShowCharacterContextMenu(this, myCharacter, e.Location);
                

            // 몬스터 클릭
            if (clickedMonster != null)
            {
                lastClickedMonster = clickedMonster;
                controller.ShowMonsterContextMenu(this, lastClickedMonster, e.Location);
            }

            if(clickedOpponent != null)
            {
                lastClickedOpponent = clickedOpponent;
                controller.ShowOpponentContextMenu(this, lastClickedOpponent, e.Location);
            }
        }

        // 주인공 위치 찾기
        private bool FindCharacterAtPoint(Point point)
        {
            Rectangle characterRect = new Rectangle(myCharacter.GetCharacterLocation().x, myCharacter.GetCharacterLocation().y, 64, 64);
            if(characterRect.Contains(point))
                return true;
            
            return false;
        }

        // 맵에 존재하는 몬스터 위치 찾기
        private Character FrindOpponentAtPoint(Point point)
        {
            lock (_opponentLock)
            {
                foreach (var opponent in firstMap.opponentCharacters)
                {
                    Rectangle opponentRect = new Rectangle(opponent.GetCharacterLocation().x, opponent.GetCharacterLocation().y, 64, 64);
                    if (opponentRect.Contains(point))
                    {
                        return opponent;
                    }
                }
            }
            return null;
        }

        // 맵에 존재하는 몬스터 위치 찾기
        private Monster FindMonsterAtPoint(Point point)
        {
            foreach (var monster in firstMap.Monsters)
            {
                if (monster.IsDead)
                    continue;

                Rectangle monsterRect = new Rectangle(monster.MonsterLocation.x, monster.MonsterLocation.y, 64, 64);
                if (monsterRect.Contains(point))
                {
                    return monster;
                }
            }
            return null;
        }

        // 시작 마을 포탈
        private void pictureBox1_Click(object sender, System.EventArgs e)
        {
            Rectangle charRect = new Rectangle(myCharacter.GetCharacterLocation().x, myCharacter.GetCharacterLocation().y, 64, 64);
            Rectangle picRect = pictureBox1.Bounds;

            bool isColliding = charRect.IntersectsWith(picRect);

            if (isColliding)
            {
                // 통신 종료
                clientBattle.CmdRemoveAsync(myCharacter.GetCharacterId().ToString());
                StopNetwork();

                StartingForm starttmap = new StartingForm(client, myCharacter);
                starttmap.Show();
                this.Close();
            }
        }

        private void pictureBox2_Click(object sender, System.EventArgs e)
        {

        }

    }
}
