using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class StoryForm : Form
    {
        private List<string> StoryTexts = new List<string>
        {
            "어느 날 밤, 하늘에서 거대한 주사위가 불타는 꼬리를 그리며 떨어졌다.\n\n" +
            "그것은 별이 아니었다. 세상의 질서를 바꾸기 위한—하늘의 심판이었다.",
            "그 주사위가 대지를 강타한 순간, 세계의 법칙은 산산조각 났고,\n\n" +
            "예측 가능했던 삶은 “운의 룰”이라는 새로운 규칙 아래 재편되었다.",
            "사람들의 공격은 주사위에 따라 요행이 되거나 무의미해졌고,\n\n" +
            "희귀한 아이템은 가장 약한 자에게 떨어지기도 했다.",
            "모든 것은 무작위, 모든 것은 불확실.",
            "그리고 그 혼돈의 틈 속에서 “운의 신”이라 불리는 존재가 깨어났다.\n\n" +
            "그는 말없이 미소 지으며 말했다.",
            "“세상은 공평하지 않아. 그렇기에, 주사위가 필요하지.”",
            "운의 신은 자신의 힘을 일곱 개의 조각으로 나누어\n\n" +
            "세계 각지에 보스 몬스터의 형상으로 뿌렸다.",
            "그 보스들은 각각 하나의 대륙을 지배했고,\n\n" +
            "그 땅에서는 오직 ‘운’만이 살아남는 무기가 되었다.",
            "그리고 이 세계의 곳곳에, 운의 전사들이 깨어나기 시작했다.",
            "누구는 행운을, 누구는 변덕을, 누구는 균형을 타고났지만\n\n" +
            "그 누구도 자신의 운명을 예측할 수는 없었다.",
            "플레이어는 이 혼돈의 세계 ‘운빨존’을 떠돌며\n\n" +
            "각 대륙의 보스를 쓰러뜨리고, 운의 파편을 하나씩 되찾는다.",
            "모든 보스가 쓰러진 순간, 하늘로 향하는 문이 열린다.\n\n" +
            "그곳은 천상 주사위 성소—운의 신이 잠든 마지막 전장.",
            "운의 신과의 최후의 전투에서 모든 전사들은 하나 되어 맞서야 한다.\n\n" +
            "단 한 번의 주사위가 세계의 운명을 결정지을지도 모른다.",
            "운이냐, 의지냐. 그 답은 지금,\n\n" +
            "당신의 손에 쥔 주사위에 달려 있다.",
        };

        private readonly Dictionary<int, Image> ImageMap = new Dictionary<int, Image>
        {
            { 0, Properties.Resources.Story1 },
            { 4, Properties.Resources.Story2 },
            { 10, Properties.Resources.Story3 },
            { 13, Properties.Resources.Story4 }
        };

        private int CurrentIndex = 0;

        public StoryForm()
        {
            InitializeComponent();
        }

        private void StoryForm_Load(object sender, EventArgs e)
        {
            ShowCurrent();
        }

        private void StoryForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                ShowNext();
            }
        }

        private void NextStoryButton_Click(object sender, EventArgs e)
        {
            ShowNext();
        }

        private void ShowCurrent()
        {
            if (ImageMap.TryGetValue(CurrentIndex, out var img))
            {
                this.BackgroundImage = img;
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }

            this.StoryText.Text = StoryTexts[CurrentIndex];
        }

        private void ShowNext()
        {
            CurrentIndex++;

            if (CurrentIndex >= StoryTexts.Count)
            {
                TestMapForm testForm = new TestMapForm();
                testForm.Show();
                this.Hide();
            }
            else
            {
                ShowCurrent();
            }
        }
    }
}
