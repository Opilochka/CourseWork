using CourseWork.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class Start : Form
    {
        public static WMPLib.WindowsMediaPlayer WMP = new WMPLib.WindowsMediaPlayer(); 

        public Start()
        {
            InitializeComponent();
            Task.Factory.StartNew(() =>
            {
                var files = new string[] {
                 @"D:\Курсовая работа\1\CourseWork\Songs\Song1.wav"};

                var player = new SoundPlayer();

                while (true)
                {
                    foreach (var file in files)
                    {
                        player.SoundLocation = file;
                        player.PlaySync();
                    }
                }
            }, TaskCreationOptions.LongRunning);
           /*WMP.URL = @"D:\Курсовая работа\Game\CourseWork\Songs\Song1.mp3"; // файл музыкальный
            //WMP.settings.volume = 100; // меняя значение можно регулировать громкость
            WMP.controls.play(); // Старт*/
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            Account.AccountLife = 3;
            Account.AccountTank = 3;

            Form1 childForm = new Form1();
            childForm.Show();
            this.Hide(); 
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
