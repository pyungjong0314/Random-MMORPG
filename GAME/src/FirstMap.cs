using Game.BaseMonster;
using Game.Maps;
using Game.Monsters;
using System;
using System.Collections.Generic;
using System.Drawing;
using Game.MapFactories;
using Game.Obstacles;

using System.Windows.Forms;
using Game.Characters;
using WindowsFormsApp1.MapControls;

namespace WindowsFormsApp1
{
    public partial class FirstMap : Form
    {
        Map firstMap;
        Image firstMapImg;
        private Character myCharacter;
        private MapController controller;
        private Bitmap bufferBitmap;

        // 이미지 생성
        private Monster lastClickedMonster;

        public FirstMap(Character character)
        {
            InitializeComponent();
            myCharacter = character; 
            // 내부 그릴 수 있는 영역 크기
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // 생성할 몬스터 리스트
            List<Monster> monsters = new List<Monster>
            {
                // Slime 10마리
                new Slime { MonsterLocation = (135, 199) },
                new Slime { MonsterLocation = (53, 281) },
                new Slime { MonsterLocation = (160, 320) },
                new Slime { MonsterLocation = (302, 281) },
                new Slime { MonsterLocation = (157, 461) },
                new Slime { MonsterLocation = (290, 462) },
                new Slime { MonsterLocation = (388, 382) },
                new Slime { MonsterLocation = (461, 485) },
                new Slime { MonsterLocation = (507, 332) },
                new Slime { MonsterLocation = (550, 411) },

                // Goblin 6마리
                new Goblin { MonsterLocation = (643, 433) },
                new Goblin { MonsterLocation = (779, 499) },
                new Goblin { MonsterLocation = (931, 474) },
                new Goblin { MonsterLocation = (708, 332) },
                new Goblin { MonsterLocation = (828, 382) },
                new Goblin { MonsterLocation = (933, 332) }
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

            // 키보드 입력
            controller = new MapController(character, firstMap, this);
            this.KeyDown += TestForm_KeyDown;

            this.MouseClick += FirstMap_MouseClick;
            this.DoubleBuffered = true;

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            if (bufferBitmap == null)
            {
                // bufferBitmap 생성 및 맵 이미지 한 번 그리기
                bufferBitmap = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
                using (Graphics g = Graphics.FromImage(bufferBitmap))
                {
                    g.DrawImage(firstMapImg, 0, 0);
                }
            }

            // 버퍼 비트맵을 화면에 그리기
            e.Graphics.DrawImage(bufferBitmap, 0, 0);
            controller.DrawCharacter(e.Graphics);
        }


        // 키보드 입력에 따라 캐릭터를 이동시키고 충돌 여부를 판단하는 메서드
        private void TestForm_KeyDown(object sender, KeyEventArgs e)
        {
            controller.HandleMovement(e.KeyCode);
        }

        // 몬스터 클릭
        private void FirstMap_MouseClick(object sender, MouseEventArgs e)
        {
            Monster clickedMonster = FindMonsterAtPoint(e.Location);
            if (clickedMonster == null) return;

            lastClickedMonster = clickedMonster;
            controller.ShowMonsterContextMenu(this, lastClickedMonster, e.Location);
        }

        private Monster FindMonsterAtPoint(Point point)
        {
            foreach (var monster in firstMap.Monsters)
            {
                Rectangle monsterRect = new Rectangle(monster.MonsterLocation.x, monster.MonsterLocation.y, 64, 64);
                if (monsterRect.Contains(point))
                {
                    return monster;
                }
            }
            return null;
        }
    }
}
