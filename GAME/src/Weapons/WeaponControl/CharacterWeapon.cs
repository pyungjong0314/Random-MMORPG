using Game.Characters;
using Game.Weapons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Weapons.WeaponControl
{
    public partial class CharacterWeapon : UserControl
    {
        Form parentForm;
        Character myCharacter;
        PictureBox[,] weaponPictureBox = new PictureBox[2, 5];
        PictureBox selectedPictureBox = null;

        public CharacterWeapon(Form form, Character character)
        {
            InitializeComponent();
            myCharacter = character;
            parentForm = form;

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    PictureBox pb = new PictureBox();
                    pb.Size = new Size(60, 60);
                    pb.Location = new Point(40 + j * 70, 180 + i * 65);
                    pb.BorderStyle = BorderStyle.FixedSingle;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;

                    pb.Click += WeaponPictureBox_Click;

                    CharacterWeaponPanel.Controls.Add(pb);
                    weaponPictureBox[i, j] = pb;
                }
            }

            setPictureBox(myCharacter.characterWeapons);
            setEquipedWeapon();
        }

        public void setPictureBox(List<Weapon> weaponList)
        {
            int index = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (index >= weaponList.Count)
                    {
                        // 선택 불가능 표시
                        weaponPictureBox[i, j].Image = Properties.Resources.noWeapon;
                        weaponPictureBox[i, j].Tag = null;
                        weaponPictureBox[i, j].BackColor = Color.Transparent; // 연한 노란색 배경
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

        public void setEquipedWeapon()
        {
            if(myCharacter.characterSword != null)
            {
                EquipedSword.Image = GetImageForWeapon(myCharacter.characterSword);
            }
            if (myCharacter.characterShiled != null)
            {
                EquipedShield.Image = GetImageForWeapon(myCharacter.characterShiled);
            }
        }

        private Image GetImageForWeapon(Weapon weapon)
        {
            string name = weapon.GetWeaponName();
            int level = weapon.GetWeaponLevel();


            // 공격 레벨에 따른 검 반환
            if (name.Contains("용사의 검"))
            {
                weapon.setWeaponAttack(level * 100 + 10);  // 10, 110, 210, ..., 510

                if (level == 0) return Properties.Resources.sword_0;
                else if (level == 1) return Properties.Resources.sword_1;
                else if (level == 2) return Properties.Resources.sword_2;
                else if (level == 3) return Properties.Resources.sword_3;
                else if (level == 4) return Properties.Resources.sword_4;
                else if (level == 5) return Properties.Resources.sword_5;
            }

            // 방패 레벨에 방패 반환
            else if (name.Contains("방패"))
            {
                weapon.setWeaponDefense(level * 100 + 10);  // 10, 110, 210, ..., 510

                if (level == 0) return Properties.Resources.shield_0;
                else if (level == 1) return Properties.Resources.shield_1;
                else if (level == 2) return Properties.Resources.shield_2;
                else if (level == 3) return Properties.Resources.shield_3;
                else if (level == 4) return Properties.Resources.shield_4;
                else if (level == 5) return Properties.Resources.shield_5;
            }

            return Properties.Resources.sword;
        }

        private void CloseLabel_Click(object sender, EventArgs e)
        {
            parentForm.Controls.Remove(this.CharacterWeaponPanel);
            this.Dispose();
            parentForm.Focus();
        }

        private void equipWeapon_Click(object sender, EventArgs e)
        {
            myCharacter.EquipWeapon((Weapon)selectedPictureBox.Tag);
            setEquipedWeapon();
        }
    }
}
