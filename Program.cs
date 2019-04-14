using System;
using System.IO;
using System.Windows.Forms;

namespace GifSlider
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Create embedded necessary files
            if (!File.Exists(Application.StartupPath + "\\Guide.mp3"))
            {
                byte[] bytesInStream = Properties.Resources.Guide;
                FileStream fileStream = File.Create(Application.StartupPath + "\\Guide.mp3", bytesInStream.Length);
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
            if (!File.Exists(Application.StartupPath + "\\Interop.WMPDXMLib.dll"))
            {
                byte[] bytesInStream = Properties.Resources.Interop_WMPDXMLib;
                FileStream fileStream = File.Create(Application.StartupPath + "\\Interop.WMPDXMLib.dll", bytesInStream.Length);
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
            if (!File.Exists(Application.StartupPath + "\\Interop.WMPLib.dll"))
            {
                byte[] bytesInStream = Properties.Resources.Interop_WMPLib;
                FileStream fileStream = File.Create(Application.StartupPath + "\\Interop.WMPLib.dll", bytesInStream.Length);
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
            //-------

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch (Exception e)
            {
                string log = e.Message + " |src: " + e.Source + "\r\nDetail: " + e.InnerException + "; "+e.TargetSite;
                /*using (StreamWriter logger = new StreamWriter("error_log.txt"))
                {
                    logger.WriteLine(log);
                }*/
                MessageBox.Show("A startup error happened. Try starting the application again. \r\nSome necessary files might have been created.\r\n\r\nError Message: " + log, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
