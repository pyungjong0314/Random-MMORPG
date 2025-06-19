using Game.BaseMonster;
using Game.Characters;
using Game.Maps;
using Game.Weapons;
using GameClientLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Characters;
using WindowsFormsApp1.Map;
using WindowsFormsApp1.MapControls;
using WindowsFormsApp1.WeaponControls;
using WindowsFormsApp1.Weapons.WeaponControl;
using Game.Audio;

namespace WindowsFormsApp1
{
    public partial class StartingForm : Form
    {
        // 통신
        private GameWebSocketClient client;
        private Thread _sendThread;
        private bool _isRunning = false;

        // 캐릭터
        public Character myCharacter;

        // Map 이동
        private MapController controller;
        Game.Maps.Map map = new Game.Maps.Map();

        // 상점
        public List<Weapon> storeWeaponList = new List<Weapon>();
        private ContextMenuStrip StoreContextMenu;
        private ToolStripMenuItem StoreContextMunuItem;

        public StartingForm(GameWebSocketClient c, Character character)
        {
            InitializeComponent();
            InitializeStoreContextMenu();
            SoundManager.PlayBgmLoop("startingmap_bgm.wav");


            // 통신
            client = c;

            // 캐릭터 위치 설정
            myCharacter = character;
            var current = character.GetCharacterLocation();
            int dx = 500 - current.x;
            int dy = 250 - current.y;
            character.MoveLocation(dx, dy);

            // 무기 설정
            setStoreWeaponList();

            // 이벤트 핸들러
            controller = new MapController(character, map, this);
            this.MouseClick += FirstMap_MouseClick;
            this.KeyDown += StartingForm_KeyDown;

            MoveStart();
        }

        // 통신 (이동)
        public void MoveStart()
        {
            _isRunning = true;
            _sendThread = new Thread(() => SendLoopAsync().GetAwaiter().GetResult());
            _sendThread.IsBackground = true;
            _sendThread.Start();
        }

        public void MoveStop()
        {
            _isRunning = false;
            _sendThread?.Join();
        }

        private async Task SendLoopAsync()
        {
            while (_isRunning)
            {
                try
                {
                    var loc = myCharacter.GetCharacterLocation();

                    await client.CmdMoveAsync(myCharacter.GetCharacterId().ToString(), 1, loc.x, loc.y);
                    Console.WriteLine($"[좌표 전송] x: {loc.x}, y: {loc.y}");

                    await Task.Delay(100);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("좌표 전송 중 예외 발생: " + ex.Message);
                    break;
                }
            }
        }


        // 통신 (몬스터 리스트)

    protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // 3. 캐릭터 그리기
            controller.DrawCharacter(e.Graphics);
        }

        private void StartingForm_KeyDown(object sender, KeyEventArgs e)
        {
            controller.HandleMovement(e.KeyCode);
        }

        private void FirstMap_MouseClick(object sender, MouseEventArgs e)
        {
            // 캐릭터 클릭
            if (FindCharacterAtPoint(e.Location))
                controller.ShowCharacterContextMenu(this, myCharacter, e.Location);
        }

        private bool FindCharacterAtPoint(Point point)
        {
            Rectangle characterRect = new Rectangle(myCharacter.GetCharacterLocation().x, myCharacter.GetCharacterLocation().y, 64, 64);
            if (characterRect.Contains(point))
                return true;

            return false;
        }

        // 포탈 이동
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Rectangle charRect = new Rectangle(myCharacter.GetCharacterLocation().x, myCharacter.GetCharacterLocation().y, 64, 64);
            Rectangle picRect = pictureBox1.Bounds;

            bool isColliding = charRect.IntersectsWith(picRect);

            if (isColliding)
            {
                FirstMap firstmap = new FirstMap(client, myCharacter);
                MoveStop();
                SoundManager.StopBgm();
                firstmap.Show();
                this.Close();
            }
        }

        // 상점 코드
        private void setStoreWeaponList()
        {
            Weapon s1 = WeaponFactory.WeaponCreate(0);
            Weapon s2 = WeaponFactory.WeaponCreate(0);
            Weapon s3 = WeaponFactory.WeaponCreate(0);
            Weapon s4 = WeaponFactory.WeaponCreate(0);
            Weapon s5 = WeaponFactory.WeaponCreate(0);
            Weapon s6 = WeaponFactory.WeaponCreate(0);

            Weapon w1 = WeaponFactory.WeaponCreate(1);
            Weapon w2 = WeaponFactory.WeaponCreate(1);
            Weapon w3 = WeaponFactory.WeaponCreate(1);
            Weapon w4 = WeaponFactory.WeaponCreate(1);
            Weapon w5 = WeaponFactory.WeaponCreate(1);
            Weapon w6 = WeaponFactory.WeaponCreate(1);

            storeWeaponList.Add(s1);
            storeWeaponList.Add(s2);
            storeWeaponList.Add(s3);
            storeWeaponList.Add(s4);
            storeWeaponList.Add(s5);
            storeWeaponList.Add(s6);
            storeWeaponList.Add(w1);
            storeWeaponList.Add(w2);
            storeWeaponList.Add(w3);
            storeWeaponList.Add(w4);
            storeWeaponList.Add(w5);
            storeWeaponList.Add(w6);
        }

        private void InitializeStoreContextMenu()
        {
            StoreContextMenu = new ContextMenuStrip();
            StoreContextMenu.Items.Add("구매하기", null, OnClickedBuyWeapon);

            StoreContextMunuItem = new ToolStripMenuItem("강화하기", null, OnClckedWeaponUpgrade);
            StoreContextMenu.Items.Add(StoreContextMunuItem);
        }

        // 컨텍스트 메뉴 표시

        private void OnClickedBuyWeapon(object sender, EventArgs e)
        {
            WeaponControl weaponBuyPanel = new WeaponControl(this, myCharacter, storeWeaponList);
            weaponBuyPanel.WeaponPanel.Location = new Point(300, 100);
            weaponBuyPanel.WeaponPanel.Size = new Size(420, 380);

            weaponBuyPanel.WeaponPanel.Visible = true;
            this.Controls.Add(weaponBuyPanel.WeaponPanel);

            weaponBuyPanel.WeaponPanel.BringToFront();
        }

        private void OnClckedWeaponUpgrade(object sender, EventArgs e)
        {
            WeaponUpgradeControl weaponUpgrad = new WeaponUpgradeControl(this, myCharacter);
            weaponUpgrad.WeaponUpgradePanel.Location = new Point(300, 100);
            weaponUpgrad.WeaponUpgradePanel.Size = new Size(420, 380);

            weaponUpgrad.WeaponUpgradePanel.Visible = true;
            this.Controls.Add(weaponUpgrad.WeaponUpgradePanel);

            weaponUpgrad.WeaponUpgradePanel.BringToFront();
        }

        private void StorePictureBox_Click(object sender, EventArgs e)
        {
            Point menuLocation = new Point(StorePictureBox.Left, StorePictureBox.Bottom);
            StoreContextMenu.Show(this, menuLocation);
        }
    }
}
