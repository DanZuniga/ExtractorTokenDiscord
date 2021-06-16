using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace DiscordToken
{
    static class Program
    {
        public static string Env(string folder)
        {
            return Environment.ExpandEnvironmentVariables(folder);
        }
        public static FileInfo[] List(string directory, string filter)
        {
            return new DirectoryInfo(directory).GetFiles(filter);
        }
        public static void Clean(TextBox textbox)
        {
            if (textbox.Text.Contains(" "))
            {
                textbox.Text.Replace(" ", "");
            }
        }
        public static void Error(Panel WinForm, TextBox complement, int movement)
        {
            TimeOut(50);
            LeftMove(WinForm, complement, movement);
            TimeOut(50);
            RightMove(WinForm, complement, movement);
            TimeOut(50);
            LeftMove(WinForm, complement, movement);
            TimeOut(50);
            RightMove(WinForm, complement, movement);
            TimeOut(50);
            LeftMove(WinForm, complement, movement);
            TimeOut(50);
            RightMove(WinForm, complement, movement);
        }
        private static void LeftMove(Panel WinForm, TextBox complement, int movement)
        {
            WinForm.Left -= movement;
            complement.Left -= movement;
            TimeOut(25);
            WinForm.Left += movement * 2;
            complement.Left += movement * 2;
        }

        private static void RightMove(Panel WinForm, TextBox complement, int movement)
        {
            WinForm.Left += movement;
            complement.Left += movement;
            TimeOut(25);
            WinForm.Left -= movement * 2;
            complement.Left -= movement * 2;
        }
        private static void TimeOut(int milliseconds)
        {
            System.Threading.Thread.Sleep(milliseconds);
        }

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BaseWindow());
        }
    }
    
}
