using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WindowsFormsApp1.Battle
{
    public partial class CoinControl : UserControl
    {

        private BattleForm parentForm;
        private string selectedMethod = "";
        private PictureBox selectedPictureBox = null;
        Random rand = new Random();

        public CoinControl(BattleForm parent)
        {
            InitializeComponent();

            parentForm = parent;

            CoinFront.Paint += PictureBox_Paint;
            CoinBack.Paint += PictureBox_Paint;

            CoinFront.Click += PictureBox_Click;
            CoinBack.Click += PictureBox_Click;
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

            // 선택한 동전 업데이트
            if (clicked == CoinFront)
            {
                selectedMethod = "front";
            }
            else if (clicked == CoinBack)
            {
                selectedMethod = "back";
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

        private void ThrowButton_Click(object sender, EventArgs e)
        {
            string coinResult = "";
            switch (rand.Next(2))
            {
                case 0:
                    coinResult = "front";
                    break;
                case 1:
                    coinResult = "back";
                    break;
            }

            if (selectedMethod.Equals(coinResult))
            {
                MessageBox.Show("성공");
            }
            else
            {
                MessageBox.Show("실패");
            }
        }
    }
}
