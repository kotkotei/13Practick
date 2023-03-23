using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
using System.Timers;
using System.Management;
using System.Diagnostics;

namespace Работа_с_кмс

{
    public partial class Form1 : Form

    {
        private DateTime _dateTime2;

        public Form1()
        {

            InitializeComponent();
            {

            }

        }
        int blockTime = 0;
        DateTime mShutdownTime;


        private void button1_Click(object sender, EventArgs e)
        {
            var psi = new ProcessStartInfo("shutdown", "/s /t 0");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            Process.Start(psi);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("cmd", "/r shutdown -r -t 10");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process shutDown = new Process();
            int shutdownTimeInSeconds = 60; // this will shutdown in 10 minutes, use DateTime and TimeSpan functions to calculate the exact time you need    
            shutDown.StartInfo.FileName = "shutdown.exe";
            shutDown.StartInfo.Arguments = string.Format("-r -t {0}", shutdownTimeInSeconds);

            shutDown.Start();

            if (!timer1.Enabled)
            {
                _dateTime2 = DateTime.Now.AddMinutes(1);
            }
            timer1.Enabled = !timer1.Enabled;
            button1.Text = timer1.Enabled ? "Stop" : "Start";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var dateTime = DateTime.Now;
            if (dateTime < _dateTime2)
            {
                var timeSpan = (_dateTime2 - dateTime);

                label4.Text =

                    string.Format("\r{0:00}:{1:00}:{2:00}", (int)timeSpan.TotalHours, (int)timeSpan.TotalMinutes,
                                  (int)timeSpan.TotalSeconds);
            }
            else
            {
                button1_Click(null, EventArgs.Empty);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mShutdownTime = DateTime.Now.AddSeconds(20);//через 20 сек
            label4.Visible = true;
            label4.Text = "";
            timer2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now < mShutdownTime)
            {
                TimeSpan ts = mShutdownTime - DateTime.Now;
                label4.Text = "Выключение произойдёт через: " + ts.Minutes + " минут " + ts.Seconds + " секунд";
            }
            else
            {
                var psi = new ProcessStartInfo("shutdown", "/s /t 0");
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                Process.Start(psi);
            }
        }
    }
}
    
   

