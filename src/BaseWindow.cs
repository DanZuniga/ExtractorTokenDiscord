using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DiscordToken
{
    public partial class BaseWindow : Form
    {
        // Fields
        static string localStorage;
        static FileInfo[] ldbFiles;

        public BaseWindow()
        {
            InitializeComponent();
        }

        private void Window_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            if (pictureBox3.Image != Properties.Resources.hide)
            {
                textBox1.UseSystemPasswordChar = true;
            }
        }
        
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        
        private void ControlBox_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        

        private void pictureBox3_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.UseSystemPasswordChar == false)
            {
                pictureBox3.Image = Properties.Resources.show;
                textBox1.UseSystemPasswordChar = true;
            }
        }
        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            if (textBox1.UseSystemPasswordChar == true)
            {
                pictureBox3.Image = Properties.Resources.hide;
                textBox1.UseSystemPasswordChar = false;
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CopyToClipboard_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "YOUR-TOKEN-DISCORD")
            {
                ControlBox.BackColor = Color.FromArgb(237, 66, 69);
                panel1.BackColor = Color.FromArgb(237, 66, 69);
                Program.Error(panel1, textBox1, 2);
                ControlBox.BackColor = Color.FromArgb(88, 101, 242);
                return;
            }
            Program.Clean(textBox1);
            Clipboard.SetText(textBox1.Text);
            button2.BackColor = Color.FromArgb(59, 165, 93);
            ControlBox.BackColor = Color.FromArgb(59, 165, 93);
            button2.FlatAppearance.BorderSize = 0;
            button2.Text = "Done";
        }

        private void CopyToClipboard_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(88, 101, 242);
            ControlBox.BackColor = Color.FromArgb(88, 101, 242);
            button2.FlatAppearance.BorderSize = 1;
            button2.Text = "Copy to clipboard";
        }

        private void ExtractToken_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(88, 101, 242);
            ControlBox.BackColor = Color.FromArgb(59, 165, 93);
            string appdata = Program.Env("%appdata%");
            appdata = appdata += "\\discord";
            localStorage = appdata += "\\Local Storage\\leveldb";
            ldbFiles = Program.List(localStorage, "*.ldb");
            foreach (FileInfo file in ldbFiles)
            {
                string[] Alllines = File.ReadAllLines(file.FullName);
                int lines = Alllines.Length;
                int index = 9198 - 9199;
                while (++index < lines)
                {
                    if (Alllines[index].Contains(">oken") && Alllines[index].Contains("https://discordapp.com"))
                    {
                        textBox1.UseSystemPasswordChar = true;
                        this.textBox1.Text = Alllines[index].Split('"', '"')[1];
                        break;
                    }
                }
            }
            ControlBox.BackColor = Color.FromArgb(88, 101, 242);
        }

        private void OpenSource_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/DanZuniga/ExtractorTokenDiscord");
        }
    }
}
