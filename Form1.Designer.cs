namespace GifSlider
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button_plus = new System.Windows.Forms.Button();
            this.button_minus = new System.Windows.Forms.Button();
            this.button_start = new System.Windows.Forms.Button();
            this.button_clean = new System.Windows.Forms.Button();
            this.button_moveup = new System.Windows.Forms.Button();
            this.button_movedown = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.imageTimer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_help = new System.Windows.Forms.Button();
            this.labelversion = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_song = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button_selectsong = new System.Windows.Forms.Button();
            this.button_savelist = new System.Windows.Forms.Button();
            this.checkBox_loopmp3 = new System.Windows.Forms.CheckBox();
            this.trackBar_volume = new System.Windows.Forms.TrackBar();
            this.checkBox_associate = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label_vol = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_volume)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.AllowDrop = true;
            this.listBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.listBox1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(628, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(268, 329);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox1_DragDrop);
            this.listBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBox1_DragEnter);
            // 
            // button_plus
            // 
            this.button_plus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_plus.Location = new System.Drawing.Point(722, 347);
            this.button_plus.Name = "button_plus";
            this.button_plus.Size = new System.Drawing.Size(25, 25);
            this.button_plus.TabIndex = 1;
            this.button_plus.Text = "+";
            this.button_plus.UseVisualStyleBackColor = true;
            this.button_plus.Click += new System.EventHandler(this.button_plus_Click);
            // 
            // button_minus
            // 
            this.button_minus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_minus.Location = new System.Drawing.Point(628, 347);
            this.button_minus.Name = "button_minus";
            this.button_minus.Size = new System.Drawing.Size(25, 25);
            this.button_minus.TabIndex = 2;
            this.button_minus.Text = "-";
            this.button_minus.UseVisualStyleBackColor = true;
            this.button_minus.Click += new System.EventHandler(this.button_minus_Click);
            // 
            // button_start
            // 
            this.button_start.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button_start.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_start.ForeColor = System.Drawing.Color.Maroon;
            this.button_start.Location = new System.Drawing.Point(756, 378);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(140, 42);
            this.button_start.TabIndex = 3;
            this.button_start.Text = "Slideshow!";
            this.button_start.UseVisualStyleBackColor = false;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // button_clean
            // 
            this.button_clean.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_clean.Location = new System.Drawing.Point(659, 347);
            this.button_clean.Name = "button_clean";
            this.button_clean.Size = new System.Drawing.Size(57, 25);
            this.button_clean.TabIndex = 4;
            this.button_clean.Text = "clean";
            this.button_clean.UseVisualStyleBackColor = true;
            this.button_clean.Click += new System.EventHandler(this.button_clean_Click);
            // 
            // button_moveup
            // 
            this.button_moveup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_moveup.Location = new System.Drawing.Point(850, 347);
            this.button_moveup.Name = "button_moveup";
            this.button_moveup.Size = new System.Drawing.Size(20, 25);
            this.button_moveup.TabIndex = 5;
            this.button_moveup.Text = "↑";
            this.button_moveup.UseVisualStyleBackColor = true;
            this.button_moveup.Click += new System.EventHandler(this.button_moveup_Click);
            // 
            // button_movedown
            // 
            this.button_movedown.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_movedown.Location = new System.Drawing.Point(876, 347);
            this.button_movedown.Name = "button_movedown";
            this.button_movedown.Size = new System.Drawing.Size(20, 25);
            this.button_movedown.TabIndex = 5;
            this.button_movedown.Text = "↓";
            this.button_movedown.UseVisualStyleBackColor = true;
            this.button_movedown.Click += new System.EventHandler(this.button_movedown_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Settings";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.checkedListBox1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(12, 25);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(289, 214);
            this.checkedListBox1.TabIndex = 8;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // imageTimer
            // 
            this.imageTimer.ForeColor = System.Drawing.Color.Black;
            this.imageTimer.Location = new System.Drawing.Point(505, 219);
            this.imageTimer.Name = "imageTimer";
            this.imageTimer.Size = new System.Drawing.Size(100, 20);
            this.imageTimer.TabIndex = 9;
            this.imageTimer.Text = "5000";
            this.imageTimer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.imageTimer_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gainsboro;
            this.label2.Location = new System.Drawing.Point(414, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Next image timer";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Gainsboro;
            this.label3.Location = new System.Drawing.Point(605, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "ms";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.button_help);
            this.panel1.Controls.Add(this.labelversion);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(0, 399);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(908, 33);
            this.panel1.TabIndex = 11;
            // 
            // button_help
            // 
            this.button_help.BackColor = System.Drawing.Color.LightGray;
            this.button_help.ForeColor = System.Drawing.Color.Black;
            this.button_help.Location = new System.Drawing.Point(67, 3);
            this.button_help.Name = "button_help";
            this.button_help.Size = new System.Drawing.Size(60, 23);
            this.button_help.TabIndex = 14;
            this.button_help.Text = "HELP!";
            this.button_help.UseVisualStyleBackColor = false;
            this.button_help.Click += new System.EventHandler(this.button_help_Click);
            // 
            // labelversion
            // 
            this.labelversion.AutoSize = true;
            this.labelversion.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelversion.ForeColor = System.Drawing.Color.Black;
            this.labelversion.Location = new System.Drawing.Point(7, 8);
            this.labelversion.Name = "labelversion";
            this.labelversion.Size = new System.Drawing.Size(54, 14);
            this.labelversion.TabIndex = 10;
            this.labelversion.Text = "v0.0003";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(133, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 14);
            this.label4.TabIndex = 10;
            this.label4.Text = "Nikola Nejedlý | TUL © 2017";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(-470, 443);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 14);
            this.label5.TabIndex = 10;
            this.label5.Text = "v0.0003";
            // 
            // textBox_song
            // 
            this.textBox_song.ForeColor = System.Drawing.Color.Black;
            this.textBox_song.Location = new System.Drawing.Point(79, 245);
            this.textBox_song.Name = "textBox_song";
            this.textBox_song.ReadOnly = true;
            this.textBox_song.Size = new System.Drawing.Size(444, 20);
            this.textBox_song.TabIndex = 9;
            this.textBox_song.Text = "None";
            this.textBox_song.Enter += new System.EventHandler(this.textBox_song_Enter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(-884, 469);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 14);
            this.label6.TabIndex = 10;
            this.label6.Text = "v0.0003";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Gainsboro;
            this.label7.Location = new System.Drawing.Point(12, 248);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Song (mp3)";
            // 
            // button_selectsong
            // 
            this.button_selectsong.Enabled = false;
            this.button_selectsong.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_selectsong.Location = new System.Drawing.Point(529, 243);
            this.button_selectsong.Name = "button_selectsong";
            this.button_selectsong.Size = new System.Drawing.Size(93, 23);
            this.button_selectsong.TabIndex = 12;
            this.button_selectsong.Text = "Select file";
            this.button_selectsong.UseVisualStyleBackColor = true;
            this.button_selectsong.Click += new System.EventHandler(this.button_selectsong_Click);
            // 
            // button_savelist
            // 
            this.button_savelist.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_savelist.Location = new System.Drawing.Point(773, 347);
            this.button_savelist.Name = "button_savelist";
            this.button_savelist.Size = new System.Drawing.Size(55, 25);
            this.button_savelist.TabIndex = 13;
            this.button_savelist.Text = "save list";
            this.button_savelist.UseVisualStyleBackColor = true;
            this.button_savelist.Click += new System.EventHandler(this.button_savelist_Click);
            // 
            // checkBox_loopmp3
            // 
            this.checkBox_loopmp3.AutoSize = true;
            this.checkBox_loopmp3.ForeColor = System.Drawing.Color.Black;
            this.checkBox_loopmp3.Location = new System.Drawing.Point(202, 271);
            this.checkBox_loopmp3.Name = "checkBox_loopmp3";
            this.checkBox_loopmp3.Size = new System.Drawing.Size(50, 17);
            this.checkBox_loopmp3.TabIndex = 14;
            this.checkBox_loopmp3.Text = "Loop";
            this.checkBox_loopmp3.UseVisualStyleBackColor = true;
            this.checkBox_loopmp3.CheckedChanged += new System.EventHandler(this.checkBox_loopmp3_CheckedChanged);
            // 
            // trackBar_volume
            // 
            this.trackBar_volume.LargeChange = 10;
            this.trackBar_volume.Location = new System.Drawing.Point(92, 271);
            this.trackBar_volume.Maximum = 100;
            this.trackBar_volume.Name = "trackBar_volume";
            this.trackBar_volume.Size = new System.Drawing.Size(104, 45);
            this.trackBar_volume.TabIndex = 15;
            this.trackBar_volume.Value = 100;
            this.trackBar_volume.ValueChanged += new System.EventHandler(this.trackBar_volume_ValueChanged);
            // 
            // checkBox_associate
            // 
            this.checkBox_associate.AutoSize = true;
            this.checkBox_associate.ForeColor = System.Drawing.Color.Black;
            this.checkBox_associate.Location = new System.Drawing.Point(12, 376);
            this.checkBox_associate.Name = "checkBox_associate";
            this.checkBox_associate.Size = new System.Drawing.Size(110, 17);
            this.checkBox_associate.TabIndex = 14;
            this.checkBox_associate.Text = "Associate .gs files";
            this.checkBox_associate.UseVisualStyleBackColor = true;
            this.checkBox_associate.CheckedChanged += new System.EventHandler(this.checkBox_associate_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Location = new System.Drawing.Point(12, 376);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(110, 17);
            this.panel2.TabIndex = 16;
            this.panel2.MouseHover += new System.EventHandler(this.checkBox_associate_MouseHover);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(12, 275);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Global volume:";
            // 
            // label_vol
            // 
            this.label_vol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_vol.AutoSize = true;
            this.label_vol.ForeColor = System.Drawing.Color.DarkGray;
            this.label_vol.Location = new System.Drawing.Point(76, 288);
            this.label_vol.Name = "label_vol";
            this.label_vol.Size = new System.Drawing.Size(25, 13);
            this.label_vol.TabIndex = 10;
            this.label_vol.Text = "000";
            this.label_vol.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(908, 432);
            this.Controls.Add(this.label_vol);
            this.Controls.Add(this.trackBar_volume);
            this.Controls.Add(this.checkBox_associate);
            this.Controls.Add(this.checkBox_loopmp3);
            this.Controls.Add(this.button_savelist);
            this.Controls.Add(this.button_selectsong);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_song);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.imageTimer);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_movedown);
            this.Controls.Add(this.button_moveup);
            this.Controls.Add(this.button_clean);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.button_minus);
            this.Controls.Add(this.button_plus);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "GIF Slider";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_volume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button_plus;
        private System.Windows.Forms.Button button_minus;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button_clean;
        private System.Windows.Forms.Button button_moveup;
        private System.Windows.Forms.Button button_movedown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.TextBox imageTimer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelversion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_song;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_selectsong;
        private System.Windows.Forms.Button button_savelist;
        private System.Windows.Forms.Button button_help;
        private System.Windows.Forms.CheckBox checkBox_loopmp3;
        private System.Windows.Forms.TrackBar trackBar_volume;
        private System.Windows.Forms.CheckBox checkBox_associate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label_vol;
    }
}

