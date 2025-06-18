using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Remoting.Lifetime;
using System.Windows.Forms;
using Game.Characters;

namespace WindowsFormsApp1.Weapon
{
    public partial class WebFormControl : UserControl
    {

        // 5x5 무기 슬롯용 PictureBox 배열
        PictureBox[,] pictureBoxes = new PictureBox[5, 5];



        // 무기 리스트를 받아 초기화
        public WebFormControl()
        {
            // 무기 25개 생성
            List<Game.Characters.Weapon> weapons = new List<Game.Characters.Weapon>();

            Random rand = new Random();

            for (int i = 0; i < 25; i++)
            {
                int type = rand.Next(0, 2); // 0: Sword, 1: Shield
                weapons.Add(WeaponFactory.WeaponCreate(type));
            }

            InitializeGrid(weapons); // 그리드 구성
        }

        // 무기 리스트를 기반으로 PictureBox를 초기화
        private void InitializeGrid(List<Game.Characters.Weapon> weapons)
        {
            int index = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (index >= weapons.Count) return;

                    // PictureBox 생성
                    PictureBox pb = new PictureBox
                    {
                        Size = new Size(64, 64),
                        Location = new Point(j * 70, i * 70),
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };

                    Game.Characters.Weapon weapon = weapons[index];
                    pb.Image = GetImageForWeapon(weapon);
                    pb.Tag = weapon;

                    // 화면에 추가
                    pictureBoxes[i, j] = pb; // PictureBox와 배열 링킹
                    this.Controls.Add(pb);

                    index++;
                }
            }
        }

        // 무기 이름에 따라 이미지 반환
        private Image GetImageForWeapon(Game.Characters.Weapon weapon)
        {
            string name = weapon.GetWeaponName();

            if (name.Contains("용사의 검")) return Properties.Resources.sword;
            if (name.Contains("방패")) return Properties.Resources.shield;

            return Properties.Resources.sword; // 기본 이미지
        }
    }
}
