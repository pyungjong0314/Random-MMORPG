using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game.Weapons;

namespace WindowsFormsApp1.WeaponControls
{
    public partial class WeaponControl : UserControl
    {
        StartingForm startingForm;
        PictureBox[,] weaponPictureBox = new PictureBox[4, 5];
        PictureBox selectedPictureBox = null;

        public WeaponControl(StartingForm sform, List<Weapon> weaponList)
        {
            InitializeComponent();
            startingForm = sform;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    PictureBox pb = new PictureBox();
                    pb.Size = new Size(60, 60);
                    pb.Location = new Point(40 + j * 70, 55 + i * 65);
                    pb.BorderStyle = BorderStyle.FixedSingle;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;

                    pb.Click += WeaponPictureBox_Click;

                    weaponPictureBox[i, j] = pb;

                    WeaponPanel.Controls.Add(pb);
                }
            }

            setPictureBox(weaponList);
        }

        public void setPictureBox(List<Weapon> weaponList)
        {
            int index = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (index >= weaponList.Count)
                    {
                        // 선택 불가능 표시
                        weaponPictureBox[i, j].Image = Properties.Resources.noWeapon;
                        weaponPictureBox[i, j].Tag = null;
                        weaponPictureBox[i, j].BackColor = Color.Transparent; // 연한 노란색 배경
                        continue;
                    }
                    else
                    {
                        weaponPictureBox[i, j].Image = GetImageForWeapon(weaponList[index]);
                        weaponPictureBox[i, j].Tag = weaponList[index]; // 선택 가능한 무기
                        weaponPictureBox[i, j].BackColor = Color.Transparent; // 기본 배경
                    }

                    index++;
                }
            }
        }

        private void WeaponPictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clicked = sender as PictureBox;

            // noWeapon이면 선택 막기
            if (clicked.Tag == null || clicked.Tag == null)
                return;

            // 이전 선택 해제
            if (selectedPictureBox != null)
                selectedPictureBox.BackColor = Color.Transparent;

            // 현재 선택 적용
            clicked.BackColor = Color.LightYellow;
            selectedPictureBox = clicked;
        }

        private Image GetImageForWeapon(Weapon weapon)
        {
            string name = weapon.GetWeaponName();

            if (name.Contains("용사의 검")) return Properties.Resources.sword;
            if (name.Contains("방패")) return Properties.Resources.shield;

            return Properties.Resources.sword;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selectedPictureBox == null)
                return;

            Weapon selectedWeapon = (Weapon)selectedPictureBox.Tag;
            startingForm.storeWeaponList.Remove(selectedWeapon);
            startingForm.myCharacter.AquireWeapon(selectedWeapon);

            setPictureBox(startingForm.storeWeaponList);
        }

        private void CloseLabel_Click(object sender, EventArgs e)
        {
            startingForm.Controls.Remove(this.WeaponPanel); 
            this.Dispose();
            startingForm.Focus();
        }
    }
}
