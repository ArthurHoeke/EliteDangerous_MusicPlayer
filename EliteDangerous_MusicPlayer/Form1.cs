using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using Microsoft.Win32;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text;
using System.IO;
using System.Net;
using System.Collections.Specialized;
using System.Drawing;

namespace EliteDangerous_MusicPlayer
{
    public partial class Form1 : Form
    {
        private Thread demoThread = null;
        public string sFileName;
        public string[] arrAllFilePaths;
        public string[] arrAllFileNames;
        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        public Boolean textRotation;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ThreadProcSafe()
        {
            while (true)
            {
                if (Program.MenuStatus == true)
                {
                    if (this.InvokeRequired)
                        this.Invoke(new MethodInvoker(delegate
                        {
                            //this.Hide();
                            this.Size = new Size(399, 37);
                        }));
                    else
                    {
                        //this.Hide();
                        this.Size = new Size(399, 37);
                    }
                }
                else
                {
                    if (this.InvokeRequired)
                        this.Invoke(new MethodInvoker(delegate
                        {
                            //this.Show();
                            this.Size = new Size(399, 240);
                        }));
                    else
                    {
                        //this.Show();
                        this.Size = new Size(399, 240);
                    }
                }

                if (textRotation == true)
                {
                    if(label1.Location.X >= 360) {
                        if (this.InvokeRequired)
                            this.Invoke(new MethodInvoker(delegate
                            {
                                label1.Location = new Point(-label1.Text.Length + -550, 7);
                            }));
                        else
                        {
                            label1.Location = new Point(-label1.Text.Length, 7);
                        }
                    }
                    else
                    {
                        if (this.InvokeRequired)
                            this.Invoke(new MethodInvoker(delegate
                            {
                                label1.Location = new Point(label1.Location.X + 1, 7);
                            }));
                        else
                        {
                            label1.Location = new Point(label1.Location.X + 1, 7);
                        }
                    }
                }
                System.Threading.Thread.Sleep(15);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.demoThread = new Thread(new ThreadStart(this.ThreadProcSafe));
            this.demoThread.Start();
            this.TopMost = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Opacity = 0.9;
            wplayer.settings.volume = 1;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "MP3 Files (*.mp3)|*.mp3";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = true;
            choofdlog.Title = "Select the song(s) you want to import";

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                sFileName = choofdlog.FileName;
                arrAllFilePaths = choofdlog.FileNames; //TODO: Inplaats van compleet een nieuwe array aan te maken de array informatie toevoegen
                arrAllFileNames = choofdlog.SafeFileNames;//TODO: Kijk boven
                //Start song add loop
                for (int i = 0; i < arrAllFileNames.Length; i++)
                {
                    Button song = new Button();
                    song.FlatStyle = FlatStyle.Flat;
                    Color color = Color.FromArgb(254, 111, 0);
                    song.BackColor = color;
                    song.Width = 265;
                    song.Height = 50;
                    song.Font = new Font("Nexa Light", 8);
                    song.Text = arrAllFileNames[i].Replace(".mp3", "");
                    song.Location = new Point(0, playlist.Controls.Count * 50);
                    playlist.Controls.Add(song);
                    song.Click += new EventHandler(this.musicButton_Handler);
                }
            }
        }

        private void musicButton_Handler(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            for (int i = 0; i < arrAllFileNames.Length; i++)
            {
                if (clickedButton.Text == arrAllFileNames[i].Replace(".mp3", ""))
                {
                    wplayer.controls.stop();
                    wplayer.URL = arrAllFilePaths[i];
                    wplayer.controls.play();
                    label1.Text = arrAllFileNames[i].Replace(".mp3", "");
                    textRotation = true;
                    label1.Focus();
                }
            }
        }

        private void pictureBox5_Click(object sender, System.EventArgs e)
        {
            wplayer.controls.pause();
            label1.Text = "ELITE DANGEROUS MUSIC PLAYER";
            textRotation = false;
            label1.Location = new Point(38, 7);
        }

        private void pictureBox4_Click(object sender, System.EventArgs e)
        {
            wplayer.controls.play();
            label1.Text = wplayer.currentMedia.name.Replace(".mp3", "");
            textRotation = true;
        }


        //TODO: Instead of repeating the same function over and over make a loop (used this way just for test purposes)
        private void volume1_Click(object sender, System.EventArgs e)
        {
            volume2.BackColor = Color.FromArgb(107, 46, 0);
            volume3.BackColor = Color.FromArgb(107, 46, 0);
            volume4.BackColor = Color.FromArgb(107, 46, 0);
            volume5.BackColor = Color.FromArgb(107, 46, 0);
            wplayer.settings.volume = 6;
        }

        private void volume2_Click(object sender, System.EventArgs e)
        {
            volume2.BackColor = Color.FromArgb(254, 111, 0);
            volume3.BackColor = Color.FromArgb(107, 46, 0);
            volume4.BackColor = Color.FromArgb(107, 46, 0);
            volume5.BackColor = Color.FromArgb(107, 46, 0);
            wplayer.settings.volume = 12;
        }

        private void volume3_Click(object sender, System.EventArgs e)
        {
            volume2.BackColor = Color.FromArgb(254, 111, 0);
            volume3.BackColor = Color.FromArgb(254, 111, 0);
            volume4.BackColor = Color.FromArgb(107, 46, 0);
            volume5.BackColor = Color.FromArgb(107, 46, 0);
            wplayer.settings.volume = 24;
        }

        private void volume4_Click(object sender, System.EventArgs e)
        {
            volume2.BackColor = Color.FromArgb(254, 111, 0);
            volume3.BackColor = Color.FromArgb(254, 111, 0);
            volume4.BackColor = Color.FromArgb(254, 111, 0);
            volume5.BackColor = Color.FromArgb(107, 46, 0);
            wplayer.settings.volume = 36;
        }

        private void volume5_Click(object sender, System.EventArgs e)
        {
            volume2.BackColor = Color.FromArgb(254, 111, 0);
            volume3.BackColor = Color.FromArgb(254, 111, 0);
            volume4.BackColor = Color.FromArgb(254, 111, 0);
            volume5.BackColor = Color.FromArgb(254, 111, 0);
            wplayer.settings.volume = 48;
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox9_Click(object sender, System.EventArgs e)
        {
            if (Program.MenuStatus == true)
            {
                Program.MenuStatus = false;
                pictureBox9.Image = EliteDangerous_MusicPlayer.Properties.Resources.minus;
            }
            else
            {
                Program.MenuStatus = true;
                pictureBox9.Image = EliteDangerous_MusicPlayer.Properties.Resources.plus;
            }
        }
    }
}
