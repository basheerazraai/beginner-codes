using System;
using System.IO;
using System.Reflection;

namespace erecord_translate
{

    class WriteLog
    {
        public static string mLogDir = null;
        public static string mLogFilePort = null;
        public static string mLogFile = null;
        public static int mLogSaveDays = 0;
        private static object lockObject = 1;

        //ログ書き込み処理
        public static bool LogFileWriteProc(string pLogSyubetu, string pFuncName, string pLogMess)
        {
            lock (lockObject)
            {
                bool bret = true;

                try
                {
                    string strLogFileName = "", strWriteBuff;
                    string strLogFileSerchName = "";
                    string strLogDelFileName = "";

                    StreamWriter streamWriter;
                    if (mLogDir != null || mLogFilePort != null || mLogFile != null)
                        strLogFileName = mLogDir + "\\" + DateTime.Now.ToString("yyyyMMdd") + "-" + mLogFilePort + "-" + mLogFile;
                    else
                    {
                        //Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase MyApp = new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase();
                        //strLogFileName = Path.Combine(MyApp.Info.DirectoryPath, DateTime.Now.ToString("yyyyMMdd") + "-" + MyApp.Info.AssemblyName + "-" + "ErrLog.txt");
                        Assembly myAssembly = Assembly.GetEntryAssembly();
                        strLogFileName = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yyyyMMdd") + "-" + myAssembly.GetName().Name + "-" + "Log.txt");
                    }
                    strLogFileSerchName = mLogDir + "\\" + "*" + "-" + mLogFilePort + mLogFile;
                    strLogDelFileName = mLogDir + "\\" + DateTime.Now.AddDays(-1 * mLogSaveDays).ToString("yyyyMMdd") + "-" + mLogFilePort + mLogFile;
                    //ログファイルフォルダが無い場合作成する
                    try
                    {
                        if (System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(strLogFileSerchName)) == false)
                        {
                            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(strLogFileSerchName));
                        }
                    }
                    catch (Exception ex)
                    {
                        strLogFileName = DateTime.Now.ToString("yyyyMMdd") + "-" + "ErrLog.txt";
                        pLogSyubetu = "E";
                        pFuncName = "WriteLogFile.LogFileWriteProc";
                        pLogMess = "LogFileWrite.LogFileWriteProc:CreateLogDirectoryError";
                        //                        return false;
                    }

                    //過去ログファイルの削除
                    try
                    {
                        string[] files = Directory.GetFiles(System.IO.Path.GetDirectoryName(strLogFileSerchName), System.IO.Path.GetFileName(strLogFileSerchName));
                        foreach (string s in files)
                        {
                            if (string.Compare(s, strLogDelFileName) < 0)
                            {
                                File.Delete(s);

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        strLogFileName = DateTime.Now.ToString("yyyyMMdd") + "-" + "ErrLog.txt";
                        pLogSyubetu = "E";
                        pFuncName = "WriteLogFile.LogFileWriteProc";
                        pLogMess = "LogFileDeleteError";
                    }

                    // ファイルへの出力
                    try
                    {
                        streamWriter =
                            new System.IO.StreamWriter(strLogFileName, true, System.Text.Encoding.GetEncoding(1200));
                    }
                    catch
                    {
                        streamWriter =
                            new System.IO.StreamWriter(strLogFileName, true, System.Text.Encoding.GetEncoding(932));
                    }

                    strWriteBuff = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " || " +
                        pLogSyubetu + " || " +
                        pFuncName + " || " +
                        pLogMess;

                    streamWriter.WriteLine(strWriteBuff);
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
                catch
                {
                    bret = false;
                }

                return bret;
            }
        }
    }
}