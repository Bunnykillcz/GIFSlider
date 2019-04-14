using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

//--------------------------\\
// Author: Nikola Nejedlý   \\
// Kybersoft (c) 2017       \\
// Web: http://nejedniko.tk \\
// TUL | FM:IL - Project    \\
// .NET 3.5 - support winXP \\
//--------------------------\\


namespace GifSlider
{
    public partial class Slideshow : Form
    {
        double scale = 1;
        public Form TheParent;
        string filenow = "";
        int count = 0;
        public int time = 3000;
        int id = 0;
        public bool prevent_idle = false;

        Image temp;

        public bool random = false;
        public bool repeatrand = false;

        public bool topm = false;

        public bool playsong = false;
        public string song;
        public bool loopsong = true;
        List<bool> random_order_checkup = new List<bool>();

        public bool nextontime = true;
        public int startat = 0;
        public Int16 imagefit = 0; // 0 - stretch; 1 - fit; 2 - 1:1

        public int vol = 0;

        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();

        public Slideshow()
        {
            InitializeComponent();
            timer.Enabled = false;
        }

        public void playMp3(string file, int vol, bool repeat)
        {
            if (!File.Exists(file))
                return;

            wplayer.URL = file;
            wplayer.settings.volume = vol;

            if (repeat)
                wplayer.settings.setMode("loop", true);
            else
                wplayer.settings.setMode("loop", false);

            wplayer.controls.play();
        }

        public void stopMp3()
        {
            wplayer.controls.stop();
        }

        private void Slideshow_Load(object sender, EventArgs e)
        {
            playMp3(song,vol,loopsong);

            if (prevent_idle)
            {
            uint fPreviousExecutionState = NativeMethods.SetThreadExecutionState(
            NativeMethods.ES_CONTINUOUS | NativeMethods.ES_SYSTEM_REQUIRED);
                if (fPreviousExecutionState == 0)
                {
                    MessageBox.Show("Something went wrong. Can't prevent Windows IDLE state. \r\nSlideshow will continue without it.","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }

            int htemp = Screen.PrimaryScreen.WorkingArea.Size.Height;
            int wtemp = Screen.PrimaryScreen.WorkingArea.Size.Width;
            this.SetDesktopLocation(wtemp / 2 - this.Width / 2, htemp / 2 - this.Height / 2);

            if (topm)
            SetWindowPos(this.Handle, HWND_TOPMOST, 100, 100, 300, 300, TOPMOST_FLAGS);

            foreach(var item in Form1.imglist)
                random_order_checkup.Add(false);

            if (startat >= 0)
                id = startat;
            else
                id = 0;

            random_order_checkup[id] = true;

            Point p = new Point(0, 0);
            pictureBox1.Location = p;
            pictureBox1.Width = this.Width;
            pictureBox1.Height = this.Height;
            
            timer.Interval = time;
            count = Form1.imglist.Count;

            updatePB();
        }

        private Point fit(Image origin, int wi, int he)
        {
            int sourceWidth = origin.Width;
            int sourceHeight = origin.Height;
            double destX = 0;
            double destY = 0;

            double nScale = 0;
            double nScaleW = 0;
            double nScaleH = 0;

            nScaleW = ((double)wi / (double)sourceWidth);
            nScaleH = ((double)he / (double)sourceHeight);
            nScale = Math.Min(nScaleH, nScaleW);
            destY = (he - sourceHeight * nScale) / 2;
            destX = (wi - sourceWidth * nScale) / 2;
            
            int destWidth = (int)Math.Round(sourceWidth * nScale);
            int destHeight = (int)Math.Round(sourceHeight * nScale);
            return new Point(destWidth,destHeight);
        }

        private void updatePB()
        {

            //imagefit | 0 - stretch; 1 - fit; 2 - 1:1
            int Wid = this.ClientSize.Width;
            int Hei = this.ClientSize.Height;
            Point p = new Point(0, 0);

            if (!(Form1.imglist.Count > 0))
                return;

            filenow = Form1.imglist[id];

            if (temp != null)
                temp.Dispose();
            try
            {
                temp = Image.FromFile(filenow);
            }
            catch
            {
                temp = null;
            }

            if (temp == null)
                return;

            if (imagefit == 0)
            {
                pictureBox1.Width = (int)(Wid * scale);
                pictureBox1.Height = (int)(Hei * scale);
                if (scale != 1)
                    p = new Point(Wid /2 - pictureBox1.Width/2, Hei/2 - pictureBox1.Height/2);
                pictureBox1.Location = p;
            }
            else if (imagefit == 1)
            {
                Point wh = fit(temp, Wid, Hei);

                pictureBox1.Height = (int)(wh.Y * scale);
                pictureBox1.Width = (int)(wh.X * scale);
                p = new Point(Wid / 2 - pictureBox1.Width / 2 , Hei / 2 - pictureBox1.Height / 2);
                pictureBox1.Location = p;

            }
            else if (imagefit == 2)
            {
                int img_w = (int)(temp.Width * scale);
                int img_h = (int)(temp.Height * scale);

                p = new Point(Wid / 2 - img_w / 2, Hei / 2 - img_h / 2);
                pictureBox1.Location = p;

                pictureBox1.Width = img_w;
                pictureBox1.Height = img_h;

            }
                pictureBox1.Image = Image.FromFile(filenow);
        }

        private void Slideshow_ResizeEnd(object sender, EventArgs e)
        {
            updatePB();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!nextontime)
                return;

            if(!random)
            {
                if (id < count - 1)
                    id++;
                else
                    id = 0;
            }

            if (random && repeatrand)
            {
                Random rr = new Random(Environment.TickCount);
                id = (int)Math.Floor((double)rr.Next(0, count));
            }
            else
            if (random && !repeatrand)
            {
                Random rr = new Random(Environment.TickCount);
                id = (int)Math.Floor((double)rr.Next(0, count));

                while (random_order_checkup[id] == true)
                    id = (int)Math.Floor((double)rr.Next(0, count));
                
                random_order_checkup[id] = true;

                bool alldone = true;
                foreach (var item in random_order_checkup)
                {
                    if (item == false)
                        alldone = false;
                }
                if (alldone)
                {
                    random_order_checkup.Clear();
                    foreach (var item in Form1.imglist)
                    {
                        random_order_checkup.Add(false);
                    }
                }
            }

            updatePB();
        }

        private void Slideshow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1.slides = new Slideshow();
            stopMp3();
            TheParent.Enabled = true;
            TheParent.Show();
        }

        private void Slideshow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return || e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
                e.Handled = true;
            }
        }

        private void Slideshow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                if (id > 0)
                    id--;
                else
                    id = count - 1;

                updatePB();
                timer.Stop();
                timer.Start();
                e.Handled = true;
            }
            else
            if (e.KeyCode == Keys.Right)
            {
                if (id < count - 1)
                    id++;
                else
                    id = 0;

                updatePB();
                timer.Stop();
                timer.Start();
                e.Handled = true;
            }
            else
            if (e.KeyCode == Keys.Up)
            {
                if (scale < 2)
                    scale += 0.05;
                else
                    scale = 2;

                updatePB();
                e.Handled = true;
            }
            else
            if (e.KeyCode == Keys.Down)
            {
                if (scale > 0.1)
                    scale -= 0.05;
                else
                    scale = 0.1;

                updatePB();
                e.Handled = true;
            }
        }

        private void Slideshow_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Slideshow_Resize(object sender, EventArgs e)
        {
            updatePB();
        }

        private void Slideshow_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }


        // --------------------------------------------------------------------------------------- || StayOnTop

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        protected override void WndProc(ref Message m)
        {
            const int RESIZE_HANDLE_SIZE = 10;

            switch (m.Msg)
            {
                case 0x0084/*NCHITTEST*/ :
                    base.WndProc(ref m);

                    if ((int)m.Result == 0x01/*HTCLIENT*/)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
                        if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)12/*HTTOP*/ ;
                            else
                                m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                        }
                        else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)10/*HTLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)2/*HTCAPTION*/ ;
                            else
                                m.Result = (IntPtr)11/*HTRIGHT*/ ;
                        }
                        else
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)16/*HTBOTTOMLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)15/*HTBOTTOM*/ ;
                            else
                                m.Result = (IntPtr)17/*HTBOTTOMRIGHT*/ ;
                        }
                    }
                    return;
            }
            base.WndProc(ref m);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000; 
                return cp;
            }
        }

        // ----------------------------------------------------------------------------------------- || Prevent IDLE

        internal static class NativeMethods
        {
            [DllImport("kernel32.dll")]
            public static extern uint SetThreadExecutionState(uint esFlags);
            public const uint ES_CONTINUOUS = 0x80000000;
            public const uint ES_SYSTEM_REQUIRED = 0x00000001;
        }
        
    }
}
