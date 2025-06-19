using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game.Characters;
using GameClientLib;

// 플레이어 이름 입력 화면
namespace WindowsFormsApp1
{
    public partial class NameForm : Form
    {
        private Character newCharacter;
        private GameWebSocketClient client;

        public NameForm()
        {
            InitializeComponent();
        }

        private async void NameSetButton_Click(object sender, EventArgs e)
        {
            // 통신 부분
            try
            {
                client = new GameWebSocketClient();
                await client.ConnectAsync();
                string uid = await client.CmdConnectAsync();
                Console.WriteLine(uid);

                newCharacter = CharacterFactory.CharacterCreate(NameTextBox.Text);
                newCharacter.characterId = Convert.ToInt32(uid);

                MessageBox.Show($"캐릭터 이름이 {newCharacter.GetCharacterName()} 으로 설정되었습니다.", "캐릭터 생성 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await client.CmdSendCharacterAsync(newCharacter);

                StoryForm storyForm = new StoryForm();
                storyForm.Show();
                this.Hide();

                storyForm.FormClosed += (s, args) =>
                {
                    // storyForm 닫히면 Map1Form 열기 
                    StartingForm map1Form = new StartingForm(client, newCharacter);
                    map1Form.Show();
                };

            }
            catch (Exception ex)
            {
                MessageBox.Show("에러 발생: " + ex.Message);
            }
        }
    }
}
