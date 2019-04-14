using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

//--------------------------\\
// Author: Nikola Nejedlý   \\
// Kybersoft (c) 2017       \\
// Web: http://nejedniko.tk \\
// TUL | FM:IL - Project    \\
// .NET 3.5 - support winXP \\
//--------------------------\\


namespace GifSlider
{
    public partial class Form1 : Form
    {
        double volume = 100;

        string version = "v1.0201";

        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();

        string songtoplay = "None";
        public static Slideshow slides = new Slideshow();
        Image img;
        int time = 3000;
        public static List<string> imglist = new List<string>();
        ToolTip tip = new ToolTip();
        bool playguide = true; 
        string[] options = { "Always on top", "Play song", "Next image on timer", "Stretch images", "Fit images" , "1:1 images", "No border fullscreen", "Random order", "  > Random with repetition", "Start from selected", "Prevent Windows from going IDLE ", "Play 'Help' audio"};
        

        public Form1()
        {
            InitializeComponent();
            labelversion.Text = version;
         
            if (!UacHelper.IsUacEnabled || !UacHelper.IsProcessElevated)
            {
                checkBox_associate.Enabled = false;
            }
            
            using (RegistryKey Key = Registry.ClassesRoot.OpenSubKey("GIFSlider_File"))
                if (Key != null)
                {
                    checkBox_associate.Checked = true;
                }

        }

        //START -- Association | .gs
        public static void SetAssociation(string Extension, string KeyName, string OpenWith, string FileDescription)
        {
            if (!UacHelper.IsUacEnabled || !UacHelper.IsProcessElevated)
                return;

            RegistryKey BaseKey;
            RegistryKey OpenMethod;
            RegistryKey Shell;
            RegistryKey CurrentUser;

            BaseKey = Registry.ClassesRoot.CreateSubKey(Extension);
            BaseKey.SetValue("", KeyName);

            OpenMethod = Registry.ClassesRoot.CreateSubKey(KeyName);
            OpenMethod.SetValue("", FileDescription);
            OpenMethod.CreateSubKey("DefaultIcon").SetValue("", "\"" + OpenWith + "\",0");
            Shell = OpenMethod.CreateSubKey("Shell");
            Shell.CreateSubKey("edit").CreateSubKey("command").SetValue("", "\"" + OpenWith + "\"" + " \"%1\"");
            Shell.CreateSubKey("open").CreateSubKey("command").SetValue("", "\"" + OpenWith + "\"" + " \"%1\"");
            BaseKey.Close();
            OpenMethod.Close();
            Shell.Close();

            CurrentUser = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\" + Extension, true);
            CurrentUser.DeleteSubKey("UserChoice", false);
            CurrentUser.Close();
            
            SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //END   -- Association | .gs

        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

        private string ReadLineF(int line, string file)
        {
            if (!File.Exists(file))
                return "";

            string stringf = "";

            List<string> lines = new List<string>();
            using (var reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                    lines.Add(reader.ReadLine());
            }

            int i = 0;
            foreach (var lin in lines)
            {
                if (i == line)
                    stringf = lin;
                i++;
            }
            return stringf;
        }

        private void UpdateSettings(string file)
        {
            if (File.Exists(file))
                File.Delete(file);

            string text = "";
            int i = 0;
            foreach (var item in checkedListBox1.Items)
            {
                if (checkedListBox1.GetItemChecked(i) == true)
                    text += "1";
                else
                    text += "0";
                i++;
            }

            using (StreamWriter outputFile = new StreamWriter(file))
            {
                outputFile.WriteLine(text);
                outputFile.WriteLine(time.ToString());
                outputFile.WriteLine(checkBox_loopmp3.Checked.ToString());
                outputFile.WriteLine(songtoplay);
                outputFile.WriteLine(volume.ToString());
            }
        }

        private void InitSettings()
        {

            try
            {
                string linefile = "";
                if (File.Exists(Application.StartupPath + "\\settings.ini"))
                {
                    linefile = ReadLineF(0, Application.StartupPath + "\\settings.ini");
                    if (Int32.TryParse(ReadLineF(1, Application.StartupPath + "\\settings.ini"), out int res))
                        time = Int32.Parse(ReadLineF(1, Application.StartupPath + "\\settings.ini"));
                }

                int i = 0;
                foreach (string opt in options)
                {
                    checkedListBox1.Items.Add(opt);

                    if (File.Exists(Application.StartupPath + "\\settings.ini"))
                    {
                        if (linefile.Substring(i, 1) == "0")
                            checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
                        else
                            checkedListBox1.SetItemCheckState(i, CheckState.Checked);
                    }

                    if (!File.Exists(Application.StartupPath + "\\settings.ini"))
                        checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
                    i++;
                }

                if (File.Exists(Application.StartupPath + "\\settings.ini"))
                {
                    linefile = ReadLineF(2, Application.StartupPath + "\\settings.ini");
                    checkBox_loopmp3.Checked = bool.Parse(linefile);
                }

                if (File.Exists(Application.StartupPath + "\\settings.ini"))
                {
                    linefile = ReadLineF(3, Application.StartupPath + "\\settings.ini");
                    songtoplay = linefile;
                    if (songtoplay == "" || songtoplay == "None")
                    {
                        slides.playsong = false;
                        songtoplay = "None";
                    }
                }

                if (File.Exists(Application.StartupPath + "\\settings.ini"))
                {
                    linefile = ReadLineF(4, Application.StartupPath + "\\settings.ini");
                    volume = double.Parse(linefile);
                }
                trackBar_volume.SetRange(0,100);
                trackBar_volume.Value = (int)volume;
                label_vol.Text = volume.ToString();
            }
            catch
            {
                MessageBox.Show("An initiation error happened. Try starting the application again. \r\nSome of your settings might get reset.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                volume = 50;
                this.Close();
            }

            if (Environment.GetCommandLineArgs() != null)
            {
                string[] s = Environment.GetCommandLineArgs();

                if (s.Length >= 2)
                {
                    if (s[1].Substring(s[1].Length - 3) == ".gs")
                        using (var reader = new StreamReader(s[1]))
                        {
                            while (!reader.EndOfStream)
                            {
                                string tmp = reader.ReadLine();
                                if (File.Exists(tmp))
                                {
                                    imglist.Add(tmp);
                                    listBox1.Items.Add(Path.GetFileName(tmp));
                                }
                            }
                        }
                    else
                    if (Directory.Exists(s[1]))
                    {
                        foreach (string f in Directory.GetFiles(s[1]))
                        {
                            if (File.Exists(f))
                                if (f.Substring(f.Length - 4).ToLower().ToLower() == ".jpg" || f.Substring(f.Length - 5).ToLower() == ".jpeg" ||
                                    f.Substring(f.Length - 4).ToLower() == ".png" || f.Substring(f.Length - 4).ToLower() == ".gif" ||
                                    f.Substring(f.Length - 4).ToLower() == ".bmp")
                                {
                                    imglist.Add(f);
                                    listBox1.Items.Add(Path.GetFileName(f));
                                }
                        }
                    }

                    foreach (string f in s)
                    {
                        if(File.Exists(f))
                        if (f.Substring(f.Length - 4).ToLower() == ".jpg" || f.Substring(f.Length - 5).ToLower() == ".jpeg" ||
                            f.Substring(f.Length - 4).ToLower() == ".png" || f.Substring(f.Length - 4).ToLower() == ".gif" ||
                            f.Substring(f.Length - 4).ToLower() == ".bmp")
                        {
                            imglist.Add(f);
                            listBox1.Items.Add(Path.GetFileName(f));
                        }
                    }
                }
            }

            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            listBox1.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitSettings();
            listBox1.SelectedIndex = -1;
            button_movedown.Enabled = false;
            button_moveup.Enabled = false;
            checkedListBox1.CheckOnClick = true;
            button_minus.Enabled = false;

            Refresh();
            if (imglist.Count == 0)
                button_start.Enabled = false;
            else
                button_start.Enabled = true;

            imageTimer.Text = time.ToString();
            ApplySettings();

            int htemp = Screen.PrimaryScreen.WorkingArea.Size.Height;
            int wtemp = Screen.PrimaryScreen.WorkingArea.Size.Width;
            this.SetDesktopLocation(wtemp / 2 - this.Width / 2, htemp / 2 - this.Height / 2);

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.White);
            Brush b = new SolidBrush(Color.Black);
            Brush b1 = new SolidBrush(Color.White);
            e.Graphics.FillRectangle(b, 316, 12, 300, 200);
            e.Graphics.DrawRectangle(p, 315, 11, 301, 201);

            Font f = new Font(FontFamily.GenericSerif, 8);

            if (img == null || img.Size.IsEmpty)
                e.Graphics.DrawString("Preview (static)", f, b1, 320, 13);
            else
                e.Graphics.DrawString("Preview", f, b1, 320, 13);

            if (img != null)
                if (!img.Size.IsEmpty)
                {
                    double h; double w;
                    int x = 0; int y = 0;
                    Point pp = new Point(img.Size.Width, img.Size.Height);

                    if (img.Size.Height > 200 || img.Size.Width > 300)
                    pp = fit(img, 300, 200);

                    w = pp.X;
                    h = pp.Y;

                    if (w < 300)
                        x = 300 / 2 - (int)(w / 2);
                    if (h < 200)
                        y = 200 / 2 - (int)(h / 2);

                    e.Graphics.DrawImage(img, 316+x, 12+y, (int)w, (int)h);
                }
        }

        private void button_plus_Click(object sender, EventArgs e)
        {
            Stream myStream;
            OpenFileDialog thisDialog = new OpenFileDialog();
            //thisDialog.InitialDirectory = "c:\\";
            thisDialog.Filter = "Image files (*.bmp, *.jpeg, *.jpg, *.png)|*.bmp;*.jpeg;*.jpg;*.png|GIF files (*.gif)|*.gif|GIFSlider files (*.gs)|*.gs|All Supported (*.gs, *.bmp, *.jpeg, *.jpg, *.png, *.gif)|*.gs; *.bmp;*.jpeg;*.jpg;*.png;*.gif";
            thisDialog.FilterIndex = 4;
            thisDialog.RestoreDirectory = false;
            thisDialog.Multiselect = true;
            thisDialog.Title = "Please Select Desired Image File(s)";

            if (thisDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (String file in thisDialog.FileNames)
                {
                    try
                    {
                        if ((myStream = thisDialog.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                if (file.Substring(file.Length - 3) == ".gs")
                                    using (var reader = new StreamReader(file))
                                    {
                                        while (!reader.EndOfStream)
                                        {
                                            string tmp = reader.ReadLine();
                                            if (File.Exists(tmp))
                                            {
                                                imglist.Add(tmp);
                                                listBox1.Items.Add(Path.GetFileName(tmp));
                                            }
                                        }
                                    }
                                else
                                {
                                    imglist.Add(file);
                                    listBox1.Items.Add(Path.GetFileName(file));
                                }
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file. Error: " + ex.Message);
                    }
                }
            }
            listBox1.SelectedIndex = imglist.Count - 1;
            listBox1.Focus();
        }

        private void button_minus_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0 && listBox1.SelectedIndex <= listBox1.Items.Count)
            {
                int index = listBox1.SelectedIndex;
                if (index == -1) return;

                string addr = imglist[index];
                imglist.RemoveAt(index);
                listBox1.Items.RemoveAt(index);

                if (index > -1)
                {
                    if(index > 0)
                        listBox1.SelectedIndex = index - 1;
                    else
                    if(index == 0 && listBox1.Items.Count > 1)
                        listBox1.SelectedIndex = index;
                }
                img = null;
            }
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            ApplySettings();
            stopMp3();
            slides.timer.Enabled = true;
            slides.TheParent = this;
            slides.time = time;
            slides.Show();
            this.Hide();
            this.Enabled = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (imglist.Count == 0)
            {
                button_minus.Enabled = false;
                button_start.Enabled = false;
                img = null;
                Refresh();
            }
            else
            {
                button_minus.Enabled = true;
                button_start.Enabled = true;
            }
            int ind = listBox1.SelectedIndex;
            if (ind == -1)
            {
                button_movedown.Enabled = false;
                button_moveup.Enabled = false;
                return;
            }

            if (ind >= imglist.Count - 1 || ind == -1)
                button_movedown.Enabled = false;
            else
                button_movedown.Enabled = true;
            if (ind == 0 || ind == -1)
                button_moveup.Enabled = false;
            else
                button_moveup.Enabled = true;

            string addr = imglist[ind];

            if (!File.Exists(addr))
            { imglist.RemoveAt(ind); listBox1.Items.RemoveAt(ind); return; }

            if(img != null)
                img.Dispose();

            try
            {
                img = Image.FromFile(addr);
            }
            catch
            {
                img = null;
            }
            
            Refresh();
        }

        private void button_clean_Click(object sender, EventArgs e)
        {
            imglist.Clear();
            listBox1.ClearSelected();
            listBox1.Items.Clear();
            button_start.Enabled = false;
            img = null;
        }
        
        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (var file in FileList) // 1. level
            {
                if (file.Substring(file.Length - 4).ToLower() == ".jpg" || file.Substring(file.Length - 5).ToLower() == ".jpeg" ||
                    file.Substring(file.Length - 4).ToLower() == ".png" || file.Substring(file.Length - 4).ToLower() == ".gif" ||
                    file.Substring(file.Length - 4).ToLower() == ".bmp")
                {
                    imglist.Add(file);
                    listBox1.Items.Add(Path.GetFileName(file));
                }
                else
                if (Directory.Exists(file))
                {
                    foreach (var ffile in Directory.GetFiles(file)) // 2. level
                    {
                        if (ffile.Substring(ffile.Length - 4).ToLower() == ".jpg" || ffile.Substring(ffile.Length - 5).ToLower() == ".jpeg" ||
                            ffile.Substring(ffile.Length - 4).ToLower() == ".png" || ffile.Substring(ffile.Length - 4).ToLower() == ".gif" ||
                            ffile.Substring(ffile.Length - 4).ToLower() == ".bmp")
                        {
                            imglist.Add(ffile);
                            listBox1.Items.Add(Path.GetFileName(ffile));
                        }
                    }

                    foreach (string dffile in Directory.GetDirectories(file))
                        if (Directory.Exists(dffile))
                            foreach (var fffile in Directory.GetFiles(dffile)) // 3. level
                            {
                                if (fffile.Substring(fffile.Length - 4).ToLower() == ".jpg" || fffile.Substring(fffile.Length - 5).ToLower() == ".jpeg" ||
                                    fffile.Substring(fffile.Length - 4).ToLower() == ".png" || fffile.Substring(fffile.Length - 4).ToLower() == ".gif" ||
                                    fffile.Substring(fffile.Length - 4).ToLower() == ".bmp")
                                {
                                    imglist.Add(fffile);
                                    listBox1.Items.Add(Path.GetFileName(fffile));
                                }
                            }
                    
                }
                else
                if (file.Substring(file.Length - 3) == ".gs")
                {
                    using (var reader = new StreamReader(file))
                    {
                        while (!reader.EndOfStream)
                        {
                            string tmp = reader.ReadLine();
                            if (File.Exists(tmp))
                            {
                                imglist.Add(tmp);
                                listBox1.Items.Add(Path.GetFileName(tmp));
                            }
                        }
                    }
                }
            }


            listBox1.SelectedIndex = imglist.Count - 1;
            listBox1.Focus();
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void button_moveup_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            int newindex = index - 1;
            if (newindex <= -1)
                return;

            List<string> intmp = listBox1.Items.OfType<string>().ToList();
            List<string> tmp = Swap(intmp, index, newindex);
            listBox1.Items.Clear();
            foreach (string item in tmp)
            {
                listBox1.Items.Add(item);
            }

            tmp = Swap(imglist, index, newindex);
            imglist = tmp;

            listBox1.SelectedIndex = newindex;
        }

        private void button_movedown_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            int newindex = index + 1;
            if (newindex >= listBox1.Items.Count)
                return;

            List<string> intmp = listBox1.Items.OfType<string>().ToList();
            List<string> tmp = Swap(intmp, index, newindex);
            listBox1.Items.Clear();
            foreach (string item in tmp)
            {
                listBox1.Items.Add(item);
            }

            tmp = Swap(imglist, index, newindex);
            imglist = tmp;

            listBox1.SelectedIndex = newindex;
        }

        private List<string> Swap(List<string> l, int index1, int index2)
        {
            string A = l[index1];
            string B = l[index2];
            l[index1] = B;
            l[index2] = A;

            return l;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplySettings();
            checkedListBox1.ClearSelected();
        }

        private void ApplySettings()
        {

            if (checkedListBox1.GetItemChecked(0)) // topmost
            {
                slides.TopMost = true;
                slides.topm = true;
            }
            else
            {
                slides.TopMost = false;
                slides.topm = false;
            }

            if (checkedListBox1.GetItemChecked(1)) // play song
            {
                button_selectsong.Enabled = true;
                checkBox_loopmp3.Enabled = true;
                trackBar_volume.Enabled = true;
                textBox_song.Text = songtoplay;

                if (File.Exists(songtoplay) && songtoplay.Substring(songtoplay.Length - 4) != ".mp3")
                {
                    MessageBox.Show("Your file is not mp3, aborting.", "File not mp3", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (File.Exists(songtoplay))
                {
                    slides.playsong = true;
                    slides.song = songtoplay;
                }
                else
                {
                    //MessageBox.Show("Your mp3 file was not found and the desired mp3 will not play.","File not found",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    slides.playsong = false;
                }

            }
            else
            {
                slides.playsong = false;
                textBox_song.Text = "None";
                button_selectsong.Enabled = false;
                checkBox_loopmp3.Enabled = false;
                trackBar_volume.Enabled = false;
            }

            if (checkedListBox1.GetItemChecked(2)) // next image on timer
                slides.nextontime = true;
            else
                slides.nextontime = false;

            if (checkedListBox1.SelectedIndex == 3 || (!checkedListBox1.GetItemChecked(3) && !checkedListBox1.GetItemChecked(4) && !checkedListBox1.GetItemChecked(5))) // stretch
            {
                checkedListBox1.SetItemCheckState(3, CheckState.Checked);
                checkedListBox1.SetItemCheckState(4, CheckState.Unchecked); //fit
                checkedListBox1.SetItemCheckState(5, CheckState.Unchecked); //1:1
            }

            if (checkedListBox1.SelectedIndex == 4) //fit
            {
                checkedListBox1.SetItemCheckState(4, CheckState.Checked);
                checkedListBox1.SetItemCheckState(3, CheckState.Unchecked); //stretch
                checkedListBox1.SetItemCheckState(5, CheckState.Unchecked); //1:1
            }

            if (checkedListBox1.SelectedIndex == 5) //1:1
            {
                checkedListBox1.SetItemCheckState(5, CheckState.Checked);
                checkedListBox1.SetItemCheckState(3, CheckState.Unchecked); //stretch
                checkedListBox1.SetItemCheckState(4, CheckState.Unchecked); //fit
            }

            if (checkedListBox1.GetItemChecked(3)) slides.imagefit = 0;
            if (checkedListBox1.GetItemChecked(4)) slides.imagefit = 1;
            if (checkedListBox1.GetItemChecked(5)) slides.imagefit = 2;

            if (checkedListBox1.GetItemChecked(6)) // no border fullscreen
            {
                slides.FormBorderStyle = FormBorderStyle.None;
                slides.WindowState = FormWindowState.Maximized;
            }
            else
            {
                slides.FormBorderStyle = FormBorderStyle.Sizable;
                slides.WindowState = FormWindowState.Normal;
            }

            if (checkedListBox1.GetItemChecked(7)) // Random order
            {
                slides.random = true;
                checkedListBox1.SetItemCheckState(7, CheckState.Checked);
            }
            else
            {
                slides.random = false;
                slides.repeatrand = false;
                checkedListBox1.SetItemCheckState(8, CheckState.Unchecked);
                checkedListBox1.SetItemCheckState(7, CheckState.Unchecked);
            }

            if (checkedListBox1.GetItemChecked(8)) // Random order w repetition
            {
                slides.random = true;
                slides.repeatrand = true;
                checkedListBox1.SetItemCheckState(7, CheckState.Checked);
                checkedListBox1.SetItemCheckState(8, CheckState.Checked);
            }
            else
            {
                slides.repeatrand = false;
                checkedListBox1.SetItemCheckState(8, CheckState.Unchecked);
            }

            if (checkedListBox1.GetItemChecked(9) && listBox1.Items.Count > 0) // Start from selected
            {
                slides.startat = listBox1.SelectedIndex;
            }
            else
            {
                slides.startat = 0;
                if (listBox1.Items.Count == 0)
                    listBox1.SelectedIndex = -1;
                else
                    listBox1.SelectedIndex = 0;
            }

            if (checkedListBox1.GetItemChecked(10)) // Stop from sleeping/IDLE
            {
                slides.prevent_idle = true;
            }
            else
            {
                slides.prevent_idle = false;
            }

            if (checkedListBox1.GetItemChecked(11)) // play Guide audio
            {
                playguide = true;
            }
            else
            {
                playguide = false;
                stopMp3();
            }

            slides.vol = (int)volume;

        }

        private void imageTimer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return || e.KeyChar == (char)Keys.Enter)
            {
                int t = time;
                int ts = time;
                if (Int32.TryParse(imageTimer.Text, out ts))
                    if (ts >= 300)
                        t = Int32.Parse(imageTimer.Text);
                time = t;
                imageTimer.Text = time.ToString();
                e.Handled = true;
                this.ActiveControl = null;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateSettings(Application.StartupPath + "\\settings.ini");
        }
        
        private void textBox_song_Enter(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void button_selectsong_Click(object sender, EventArgs e)
        {
            Stream myStream;
            OpenFileDialog thisDialog = new OpenFileDialog();
            thisDialog.Filter = "MP3 files (*.mp3)|*.mp3";
            thisDialog.FilterIndex = 1;
            thisDialog.RestoreDirectory = false;
            thisDialog.Multiselect = false;
            thisDialog.Title = "Please Select Desired MP3 File";

            if (thisDialog.ShowDialog() == DialogResult.OK)
            {
                String file = thisDialog.FileName;
                try
                {
                    if ((myStream = thisDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            songtoplay = file;
                            textBox_song.Text = songtoplay;
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file. Error: " + ex.Message);
                }
            }
        }

        private void button_savelist_Click(object sender, EventArgs e)
        {
            if (imglist.Count <= 0)
            {
                MessageBox.Show("You cannot save an empty list. Sorry.", "List is empty",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog thisDialog = new SaveFileDialog();
            thisDialog.Filter = "GIFSlider files (*.gs)|*.gs";
            thisDialog.FilterIndex = 1;
            thisDialog.RestoreDirectory = true;
            thisDialog.Title = "Save as ... ";

            if (thisDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter outputFile = new StreamWriter(thisDialog.FileName))
                    {
                    foreach (var file in imglist)
                                outputFile.WriteLine(file);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not write file. Error: " + ex.Message);
                }
            }
        }

        private void button_help_Click(object sender, EventArgs e)
        {
            stopMp3();

            if(playguide)
                if (File.Exists(Application.StartupPath + "\\Guide.mp3"))
                    playMp3(Application.StartupPath + "\\Guide.mp3",(int)volume,false);

            MessageBox.Show("HELP ? Did I hear a call for help? \r\n" +
                    "Hello, friend ! Call me a MessageBoxInformationGuide™. I will be your guide for today (or any time you click that button). \r\n" +
                    "I am very pleased to introduce you to this beautiful piece of application. It's an app that makes slideshows! " +
                    "Not usual slideshows, but very - to the detail - adjustable slideshows! And you are about to experience the magic! \r\n" +
                    "First, I would suggest clicking the '+' button on the right side just below the blue-ish listbox. Or you can also " +
                    "drop your files onto that listbox, that works too! \r\n" +
                    "Your next step should probably be looking to the left and adjusting the s*BEEP*t out of your slideshow. \r\n\r\n " +
                    "* OMG, JERRY!! THIS IS A FAMILY FRIENDLY SHOW! WE HAD TO BEEP YOU OUT!!! * \r\n\r\n" +
                    "Sorry for that there. Didn't mean to be rude. Let's continue our journey. Since you have probably set everything up that you" +
                    " needed, you are ready to set up the timer. In the middle below the preview box. What that does is that it let's you control " +
                    "for how long every image stays up before changing to the next one. Set up your desired delay and press Enter to confirm your " +
                    "sweet numbers. Don't forget that it's in miliseconds and we won't allow less than 300. And now, you are ready " +
                    "to start your slideshow by pressing the button 'Slideshow!'. \r\n" +
                    "\r\nBut keep in mind that you can control the slideshow by arrow keys and exit by ESC in any setup.\r\n\r\n" +
                    "I hope that helped you. You can call me back any time you please. MessageBoxInformationGuide™ out. PEACE!",
                    "A friend needs help!", MessageBoxButtons.OK, MessageBoxIcon.None);
        }
        
        private void checkBox_loopmp3_CheckedChanged(object sender, EventArgs e)
        {
            slides.loopsong = checkBox_loopmp3.Checked;
        }

        private void trackBar_volume_ValueChanged(object sender, EventArgs e)
        {
            volume = trackBar_volume.Value;
            label_vol.Text = volume.ToString();
        }
        
        private void checkBox_associate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_associate.Checked)
                SetAssociation(".gs", "GIFSlider_File", Application.ExecutablePath, "GIFSlider File");
            else
            {
                Registry.ClassesRoot.DeleteSubKeyTree("GIFSlider_File");
                SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
            }
        }

        private void checkBox_associate_MouseHover(object sender, EventArgs e)
        {
            Point pos = this.PointToClient(MousePosition);
            tip.Show("You need to start this application under Admin privilegies to do this", this, pos.X,pos.Y,2000);
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
            return new Point(destWidth, destHeight);
        }

        private void playMp3(string file, int vol, bool repeat)
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

        private void stopMp3()
        {
            wplayer.controls.stop();
        }
    }
}
