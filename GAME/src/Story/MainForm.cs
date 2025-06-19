using Game.Characters;
using Game.Weapons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Characters;
using WindowsFormsApp1.WeaponControls;
using WindowsFormsApp1.Weapons.WeaponControl;
using Game.Audio;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            SoundManager.PlayBgmLoop("mainscreen_bgm.wav");
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            NameForm nameForm = new NameForm();
            nameForm.Show();
            this.Hide();
        }

        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            try
            {
                Character loadCharacter = CharacterStorage.LoadCharacter();
                MessageBox.Show($"캐릭터 '{loadCharacter.GetCharacterName()}' 로드 완료!");

                // 예시: 게임 화면으로 넘어가기
                //StartingForm map1Form = new StartingForm(loadCharacter);
                //map1Form.Show();
                //this.Hide();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("저장된 캐릭터 파일이 없습니다. 새로 시작해 주세요.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"캐릭터 로딩 중 오류 발생: {ex.Message}");
            }
        }

    }
}
