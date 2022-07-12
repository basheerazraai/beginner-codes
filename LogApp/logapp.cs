using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> s1 = System.IO.Directory.GetFiles(@"C:\log", "*.*").ToList<string>();
            DateTime dt = DateTime.Now.AddDays(3);
            foreach (String s in s1)
            {
                var initialFileSize = new FileInfo(s).Length;
                var lastReadLength = initialFileSize - 1024;
                if (lastReadLength < 0) lastReadLength = 0;
                int nextByte;
                String text;

                DateTime lastWriteTime = File.GetLastWriteTime(s);

                if(lastWriteTime < dt)
                {
                    try
                    {
                        var fileSize = new FileInfo(s).Length;
                        if (fileSize > lastReadLength)
                        {
                            var sizediff = lastReadLength - fileSize;
                            //Console.WriteLine(sizediff+ "\t" + fileSize +"\t" + lastReadLength);
                            using (var fs = new FileStream(s, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            {
                                fs.Seek(sizediff, SeekOrigin.End);

                                while ((nextByte = fs.ReadByte()) > 0)
                                {
                                    Console.Write(Convert.ToChar(nextByte));
                                    Debug.Write(Convert.ToChar(nextByte));
                                }
                                Console.WriteLine();
                            }
                        }
                    }
                    catch { }

                    Thread.Sleep(1000);

                    using (StreamReader sr = new StreamReader(s))
                    {
                        text = sr.ReadToEnd();
                        int index = text.IndexOf("error", 0);
                        Debug.Write(index);
                        if (index != -1)
                        {
                            Debug.Write("Error Found");
                        }
                    }
                    try
                    {
                        using (StreamWriter sw2 = new StreamWriter(@"C:\log\read\SearchedFiles.txt", true))
                        {
                            sw2.WriteLine(s + "\t" + lastWriteTime);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

                double a = 3.14 * 1000 * 1000;
                Debug.Write(a);
                
            }
        }            
    }
}
