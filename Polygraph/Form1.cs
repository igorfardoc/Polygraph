using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
using Polygraph.Properties;

namespace Polygraph
{
    public partial class Form1 : Form
    {
        bool[] used;
        int lastwrited = 1;
        int now = 0;
        DateTime start;
        string[] mass;
        int last = 0;
        int variant = 0;
        int picture = 0;
        int variantnow = 0;
        int picturenow = 0;
        bool nowstandart = true;
        bool firsttime = true;
        int len = 0;
        const int size = 2;
        Form1 form = null;
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            form = this;
        }
        public void Wait(int time)
        {
            string[] mass = new string[3];
            mass[0] = "";
            mass[2] = "";
            mass[1] = time.ToString();
            File.WriteAllLines("text.txt", mass);
            ProcessStartInfo psiOpt = new ProcessStartInfo(@"cmd.exe", @"/C my <text.txt");
            psiOpt.WindowStyle = ProcessWindowStyle.Hidden;
            psiOpt.RedirectStandardOutput = true;
            psiOpt.UseShellExecute = false;
            psiOpt.CreateNoWindow = true;
            Process procCommand = Process.Start(psiOpt);
            StreamReader srIncoming = procCommand.StandardOutput;
            procCommand.WaitForExit();
        }
        public void Start(int time)
        {
            string[] mass = new string[3];
            mass[0] = "";
            mass[2] = "";
            mass[1] = time.ToString();
            File.WriteAllLines("text.txt", mass);
            ProcessStartInfo psiOpt = new ProcessStartInfo(@"cmd.exe", @"/C my <text.txt");
            psiOpt.WindowStyle = ProcessWindowStyle.Hidden;
            psiOpt.RedirectStandardOutput = true;
            psiOpt.UseShellExecute = false;
            psiOpt.CreateNoWindow = true;
            Process procCommand = Process.Start(psiOpt);
            StreamReader srIncoming = procCommand.StandardOutput;
            procCommand.WaitForExit();
        }

        public void Maximize()
        {
            foreach (Control c in Form1.ActiveForm.Controls)
            {
                c.Top = c.Top * 2;// + c.Height / 2;
                c.Left = c.Left * 2;// + c.Width / 2;
                c.Width *= 2;
                c.Height *= 2;
                //c.Top -= c.Height / 4;
                //c.Left -= c.Width / 4;
            }
        }
        public void Do(object sender, KeyPressEventArgs e)
        {
            label1.Text = "";
            Form1.ActiveForm.KeyPress -= Do;
            if (!firsttime)
            {
                int d = (int)(DateTime.Now - start).TotalMilliseconds;
                Form1.ActiveForm.Refresh();
                Thread.Sleep(new Random().Next(500, 2000));
                if (now < len)
                {
                    if (e.KeyChar == File.ReadAllLines("variants\\" + variant.ToString() + "\\" + now.ToString() + ".txt")[0].Split('\t')[1][0])
                    {
                        mass[lastwrited++] = "Time: " + d.ToString() + " Varnum: " + (now % len).ToString() + " Picnum: standart";
                    }
                    else
                    {
                        mass[lastwrited++] = "Time: " + d.ToString() + " Varnum: " + (now % len).ToString() + " Picnum: standart" + " WRONG ANSWER";
                    }
                }
                else
                {
                    if (e.KeyChar == File.ReadAllLines("variants\\" + variant.ToString() + "\\" + (now % len).ToString() + ".txt")[0].Split('\t')[1][0])
                    {
                        mass[lastwrited++] = "Time: " + d.ToString() + " Varnum: " + (now % len).ToString() + " Picnum: " + (now % len).ToString();
                    }
                    else
                    {
                        mass[lastwrited++] = "Time: " + d.ToString() + " Varnum: " + (now % len).ToString() + " Picnum: " + (now % len).ToString() + " WRONG ANSWER";
                    }
                }
                if (lastwrited == len * 2 + 1)
                {
                    int a = Convert.ToInt32(File.ReadAllLines("config1.txt")[0]);
                    string[] mass1 = new string[1];
                    mass1[0] = (a + 1).ToString();
                    File.WriteAllLines("config1.txt", mass1);
                    File.WriteAllLines("log" + a.ToString() + ".txt", mass);
                    label1.Text = "Press to start again";
                    for (int i = 0; i < 2 * len; i++)
                    {
                        used[i] = false;
                    }
                    lastwrited = 1;
                    now = 0;
                    firsttime = true;
                    mass = new string[len * 2 + 1];
                    mass[0] = "Var: " + variant.ToString() + " Pic: " + picture.ToString();
                    Form1.ActiveForm.KeyPress += Do;
                    return;
                }
            }
            else
            {
                Form1.ActiveForm.Refresh();
                firsttime = false;
                Thread.Sleep(new Random().Next(1000, 2000));
            }
            int n = new Random().Next(0, 2 * len);
            while (used[n])
            {
                n = new Random().Next(0, 2 * len);
            }
            used[n] = true;
            now = n;
            if(now < len)
            {
                pictureBox1.Image = Image.FromFile("pictures\\" + picture.ToString() + "\\standart.jpg");
                Form1.ActiveForm.Refresh();
                pictureBox1.Visible = true;
                Form1.ActiveForm.Refresh();
                Thread.Sleep(20);
                pictureBox1.Image = null;
                pictureBox1.Visible = false;
                Form1.ActiveForm.Refresh();
                Thread.Sleep((new Random()).Next(100, 300));
                label1.Text = File.ReadAllLines("variants\\" + variant.ToString() + "\\" + (now).ToString() + ".txt")[0].Split('\t')[0];
                Form1.ActiveForm.Refresh();
                start = DateTime.Now;
            }
            else
            {
                pictureBox1.Image = Image.FromFile("pictures\\" + picture.ToString() + "\\" + (now % len).ToString() + ".jpg");
                Form1.ActiveForm.Refresh();
                pictureBox1.Visible = true;
                Form1.ActiveForm.Refresh();
                Thread.Sleep(20);
                pictureBox1.Image = null;
                pictureBox1.Visible = false;
                Form1.ActiveForm.Refresh();
                Thread.Sleep((new Random()).Next(100, 300));
                label1.Text = File.ReadAllLines("variants\\" + variant.ToString() + "\\" + (now % len).ToString() + ".txt")[0].Split('\t')[0];
                Form1.ActiveForm.Refresh();
                start = DateTime.Now;
            }
            Form1.ActiveForm.KeyPress += Do;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            mass = File.ReadAllLines("config.txt")[0].Split('\t');
            variant = Convert.ToInt32(mass[0]);
            picture = Convert.ToInt32(mass[1]);
            len = Convert.ToInt32(mass[2]);
            mass = new string[len * 2 + 1];
            used = new bool[len * 2];
            for (int i = 0; i < 2 * len; i++)
            {
                used[i] = false;
            }
            mass[0] = "Var: " + variant.ToString() + " Pic: " + picture.ToString();
            //Form1.ActiveForm.KeyPress += KeyPressedStart;
            Form1.ActiveForm.KeyPress += Do;
            Maximize();
            label1.Text = "Press to start";
        }
        public void KeyPressedStart(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.S)
            {
                label1.Text = "";
                Form1.ActiveForm.KeyPress -= KeyPressedStart;
                Form1.ActiveForm.Refresh();
                if (nowstandart)
                {
                    Thread.Sleep((new Random()).Next(1000, 2000));
                    if (variantnow < len)
                    {
                        label1.Text = File.ReadAllLines("variants\\" + variant.ToString() + "\\" + variantnow++.ToString() + ".txt")[0].Split('\t')[0];
                        Form1.ActiveForm.KeyPress += KeyPressedEnd;
                        Form1.ActiveForm.Refresh();
                        start = DateTime.Now;
                    }
                    else
                    {
                        nowstandart = false;
                    }
                }
                if (!nowstandart)
                {
                    if (variantnow < len * 2)
                    {
                        Thread.Sleep((new Random()).Next(1500, 2500));
                        pictureBox1.Image = Image.FromFile("pictures\\" + picture.ToString() + "\\" + picturenow++.ToString() + ".jpg");
                        Form1.ActiveForm.Refresh();
                        pictureBox1.Visible = true;
                        Form1.ActiveForm.Refresh();
                        Thread.Sleep(10);
                        pictureBox1.Image = null;
                        pictureBox1.Visible = false;
                        Form1.ActiveForm.Refresh();
                        Thread.Sleep(100);
                        label1.Text = File.ReadAllLines("variants\\" + variant.ToString() + "\\" + (variantnow++ - len).ToString() + ".txt")[0].Split('\t')[0];
                        Form1.ActiveForm.KeyPress += KeyPressedEnd;
                        Form1.ActiveForm.Refresh();
                        start = DateTime.Now;
                    }
                }
            }
        }
        public void KeyPressedEnd(object sender, KeyPressEventArgs e)
        {
            int d = (int)(DateTime.Now - start).TotalMilliseconds;
            label1.Text = "";
            Form1.ActiveForm.KeyPress -= KeyPressedEnd;
            Form1.ActiveForm.Refresh();
            if (nowstandart)
            {
                if (e.KeyChar == File.ReadAllLines("variants\\" + variant.ToString() + "\\" + (variantnow - 1).ToString() + ".txt")[0].Split('\t')[1][0])
                {
                    mass[variantnow] = "Time: " + d.ToString() + " Varnum: " + (variantnow - 1).ToString() + " Picnum: -";
                }
                else
                {
                    mass[variantnow] = "Time: " + d.ToString() + " Varnum: " + (variantnow - 1).ToString() + " Picnum: -" + " WRONG ANSWER";
                }
            }
            else
            {
                if (e.KeyChar == File.ReadAllLines("variants\\" + variant.ToString() + "\\" + (variantnow - 1 - len).ToString() + ".txt")[0].Split('\t')[1][0])
                {
                    mass[variantnow] = "Time: " + d.ToString() + " Varnum: " + (variantnow - 1 - len).ToString() + " Picnum: " + (picturenow - 1).ToString();
                }
                else
                {
                    mass[variantnow] = "Time: " + d.ToString() + " Varnum: " + (variantnow - 1 - len).ToString() + " Picnum: " + (picturenow - 1).ToString() + " WRONG ANSWER";
                }
                if (variantnow == len * 2)
                {
                    int a = Convert.ToInt32(File.ReadAllLines("config1.txt")[0]);
                    string[] mass1 = new string[1];
                    mass1[0] = (a + 1).ToString();
                    File.WriteAllLines("config1.txt", mass1);
                    File.WriteAllLines("log" + a.ToString() + ".txt", mass);
                    label1.Text = "END";
                    variantnow = 0;
                    picturenow = 0;
                    nowstandart = true;
                    mass = new string[len * 2 + 1];
                    mass[0] = "Var: " + variant.ToString() + " Pic: " + picture.ToString();
                }
            }
            Form1.ActiveForm.KeyPress += KeyPressedStart;
        }
    }
}
