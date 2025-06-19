using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game.Audio; 

namespace WindowsFormsApp1.Battle
{
    public partial class DieForm : Form
    {
        public DieForm()
        {
            SoundManager.PlaySoundOnce("dead_bgm.wav");
            InitializeComponent();
        }
    }
}
