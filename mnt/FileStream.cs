using System.Runtime.InteropServices;

using System.Text;

namespace Maintenance
{
    /* Iniファイルの読み取りクラス */
    public class IniStream
    {
        [DllImport( "kernel32.dll" )]
        private static extern int GetPrivateProfileString(
            string lpApplicationName
            , string lpKeyName
            , string lpDefault
            , StringBuilder lpReturnedstring
            , int nsSize
            , string lpFileName
            );

        const int MAX_SIZE = 1024;
        string filePath;

        /* コンストラクタはIniファイルがあるフォルダのパスとファイル名をもらう */
        public IniStream( string dir, string filename )
        {
            this.filePath = dir + "\\" + filename;
        }

        public string this[ string section, string key ]
        {
            get
            {
                StringBuilder sb = new StringBuilder( MAX_SIZE );
                GetPrivateProfileString( section, key, string.Empty, sb, sb.Capacity, filePath );

                return sb.ToString();
            }
        }
    }
}
