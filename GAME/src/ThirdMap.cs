using Game.BaseMonster;
using Game.Maps;
using Game.Monsters;
using System;
using System.Collections.Generic;
using System.Drawing;
using Game.MapFactories;
using Game.Obstacles;

using System.Windows.Forms;
using Game.BossMonsters;

namespace WindowsFormsApp1
{
    public partial class ThirdMap : Form
    {
        Map thirdMap;
        Image thirdMapImg;


        List<Monster> monsters = new List<Monster>
        {
            // Orc 6마리
            new Orc { MonsterLocation = (258, 27) },
            new Orc { MonsterLocation = (114, 572) },
            new Orc { MonsterLocation = (501, 406) },
            new Orc { MonsterLocation = (584, 88) },
            new Orc { MonsterLocation = (672, 658) },
            new Orc { MonsterLocation = (1017, 572) },

            // Scorpion 6마리
            new Scorpion { MonsterLocation = (61, 225) },
            new Scorpion { MonsterLocation = (182, 396) },
            new Scorpion { MonsterLocation = (397, 607) },
            new Scorpion { MonsterLocation = (421, 52) },
            new Scorpion { MonsterLocation = (779, 146) },
            new Scorpion { MonsterLocation = (727, 484) },

           
            // Witch 3마리
            new Witch { MonsterLocation = (969, 88) },
            new Witch { MonsterLocation = (1115, 174) },
            new Witch { MonsterLocation = (1315, 125) }
        };



        // 생성할 장애물 리스트
        List<Obstacle> obstacles = new List<Obstacle>
            {
            };


        public ThirdMap()
        {
            InitializeComponent();
            this.ClientSize = new Size(1500, 1000); // 내부 그릴 수 있는 영역 크기
            this.FormBorderStyle = FormBorderStyle.FixedSingle;



            (thirdMap, thirdMapImg) = Game.MapFactories.MapFactory.CreateMap(monsters, null);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(thirdMapImg, 0, 0);
        }

        private void ThirdMap_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
