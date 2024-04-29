using CourseWork.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork
{
   
    public partial class Settings : Form
    {
        [DllImport("winmm.dll")]
        public static extern int waveOutGetVolume(IntPtr hwo, out uint pdwVolume);

        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        public static string path = "File/Settings.dat";


        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {

            if (radioButton1.Checked)
            {
               Account.AccountBulets = 20;
            }
            else
            {
                if (radioButton3.Checked)
                {
                    Account.AccountBulets = 10;
                }
                else
                {
                    Account.AccountBulets = 15;
                }
            }
        }
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            Start.WMP.settings.volume = trackBar1.Value;
        }
    }
}
