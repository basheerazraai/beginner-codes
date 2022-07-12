using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LogCheck
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            DataTable dt = new DataTable();
            DateTime daysago = DateTime.Today.AddDays(-3);
            string sourcePath = @"C:\log";
            string readtext = @"C:\log\read\read.log";
            string[] paths = Directory.GetFiles(sourcePath);
            FileInfo txtfile = new FileInfo(readtext);

            if (txtfile.Length > (2 * 1024 * 1024))       // ## NOTE: 2MB max file size
            {
                var lines = File.ReadLines(readtext).Skip(10).ToArray();  // ## Set to 10 lines
                File.WriteAllLines(readtext, lines);
            }

            using (StreamWriter sw = File.AppendText(readtext))
            {
                sw.WriteLine(DateTime.Now);
            }

            foreach (string file in paths)
            {
                FileInfo oFileInfo = new FileInfo(file);
                DateTime dtWriteTime = oFileInfo.LastWriteTime;
                string read = "Done Reading " + file;
                if (dtWriteTime > daysago)
                {
                    using (StreamReader sr = File.OpenText(file))
                    {
                        string msg = "Error Found in " + file;
                        string msg2 = "\nPress Yes to Copy Directory";
                        string msg_title = "Alert";
                        string s = String.Empty;
                        while ((s = sr.ReadLine()) != null)
                        {
                            if (s.Contains("Error"))
                            {
                                var selectedOption = MessageBox.Show(msg + msg2, msg_title, MessageBoxButtons.YesNo);
                                if (selectedOption == DialogResult.Yes)

                                {
                                    Clipboard.SetText(sourcePath);
                                }
                            }
                            else if (s.Contains("error"))
                            {
                                var selectedOption = MessageBox.Show(msg + msg2, msg_title, MessageBoxButtons.YesNo);
                                if (selectedOption == DialogResult.Yes)

                                {
                                    Clipboard.SetText(sourcePath);
                                }
                            }
                            else if (s.Contains("ERROR"))
                            {
                                var selectedOption = MessageBox.Show(msg + msg2, msg_title, MessageBoxButtons.YesNo);
                                if (selectedOption == DialogResult.Yes)

                                {
                                    Clipboard.SetText(file);
                                }
                            }
                        }                     
                    }
                    using (StreamWriter sw = File.AppendText(readtext))
                    {
                        sw.WriteLine(read);
                    }
                }
            }
        }
    }
}