using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Battle
{
    public partial class selectControl : UserControl
    {
        private BattleForm parentForm;
        private string selectedMethod = "";
        private PictureBox selectedPictureBox = null;

        public selectControl(BattleForm parent)
        {
            InitializeComponent();
            
            parentForm = parent;
            
            Coin.Paint += PictureBox_Paint;
            Dice.Paint += PictureBox_Paint;

            Coin.Click += PictureBox_Click;
            Dice.Click += PictureBox_Click;
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            var clicked = sender as PictureBox;
            if (clicked == null) return;

            // 이전 선택 PictureBox가 있으면 다시 그리기(테두리 지우기)
            if (selectedPictureBox != null && selectedPictureBox != clicked)
            {
                var old = selectedPictureBox;
                selectedPictureBox = null;
                old.Invalidate();
            }

            // 새로 선택 PictureBox 지정 및 다시 그리기
            selectedPictureBox = clicked;
            selectedPictureBox.Invalidate();

            // 선택한 공격 방법도 업데이트
            if (clicked == Coin)
            {
                selectedMethod = "coin";
            }
            else if (clicked == Dice)
            {
                selectedMethod = "dice";
            }
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            var pb = sender as PictureBox;
            if (pb == null) return;

            if (pb == selectedPictureBox)
            {
                int thickness = 3;
                Color borderColor = Color.Red;

                ControlPaint.DrawBorder(e.Graphics, pb.ClientRectangle,
                    borderColor, thickness, ButtonBorderStyle.Solid,
                    borderColor, thickness, ButtonBorderStyle.Solid,
                    borderColor, thickness, ButtonBorderStyle.Solid,
                    borderColor, thickness, ButtonBorderStyle.Solid);
            }
        }

        public void ThrowButton_Click(object sender, EventArgs e)
        {
            if (!selectedMethod.Equals(""))
            {
                parentForm.selectControlButton(AttackPanel, selectedMethod);
            }
        }
    }
}