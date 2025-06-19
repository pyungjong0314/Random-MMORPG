using System;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace Game.Audio
{
    public static class SoundManager
    {
        private static SoundPlayer bgmPlayer;

        public static void PlayBgmLoop(string soundFileName)
        {
            try
            {
                string fullPath = GetSoundFullPath(soundFileName);
                if (File.Exists(fullPath))
                {
                    bgmPlayer = new SoundPlayer(fullPath);
                    bgmPlayer.PlayLooping();
                }
                else
                {
                    MessageBox.Show("BGM file not found:\n" + fullPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing BGM:\n" + ex.Message);
            }
        }

        public static void PlaySoundOnce(string soundFileName)
        {
            try
            {
                string fullPath = GetSoundFullPath(soundFileName);
                if (File.Exists(fullPath))
                {
                    SoundPlayer player = new SoundPlayer(fullPath);
                    player.Play();
                }
                else
                {
                    MessageBox.Show("Sound file not found:\n" + fullPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing sound:\n" + ex.Message);
            }
        }

        public static void StopBgm()
        {
            try
            {
                bgmPlayer?.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error stopping BGM:\n" + ex.Message);
            }
        }

        private static string GetSoundFullPath(string fileName)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string soundPath = Path.Combine(baseDir, @"..\..\..\src\Resources\soundeffects", fileName);
            return Path.GetFullPath(soundPath);
        }
    }
}
