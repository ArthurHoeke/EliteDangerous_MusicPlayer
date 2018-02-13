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

namespace EliteDangerous_MusicPlayer
{
    static class Program
    {
        public static Boolean MenuStatus = false;
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);
        static Form1 form1;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread t = new Thread(menuController);
            t.Start();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void menuController()
        {
            while (true)
            {
                if (GetAsyncKeyState(0x2D) != 0)
                {
                    if (MenuStatus == true)
                    {
                        MenuStatus = false;
                    }
                    else
                    {
                        MenuStatus = true;
                    }
                    System.Threading.Thread.Sleep(1000);
                }
                System.Threading.Thread.Sleep(1);
            }
        }
    }
}
