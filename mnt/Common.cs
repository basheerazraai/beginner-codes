using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace Maintenance
{
    /* 共通利用できる関数のクラス */
    class Common
    {
        /* IPアドレスを0埋めからドット区切りに変換 */
        public static string ConvertIPv4ToDot( string original )
        {
            string tmp      = string.Empty;
            string output   = string.Empty;

            if( original.Length != 12 )
            {
                return original;
            }
            else
            {
                for( int i = 0; i < 4; i ++ )
                {
                    if( i > 0 )
                    {
                        output += ".";
                    }

                    tmp = original.Substring( i * 3, 3 );

                    if( tmp == "000" )
                    {
                        output += "0";
                    }
                    else if( tmp.Substring( 0, 2 ) == "00" )
                    {
                        output += tmp.Substring( 2, 1 );
                    }
                    else if( tmp.Substring( 0, 1 ) == "0" )
                    {
                        output += tmp.Substring( 1, 2 );
                    }
                    else
                    {
                        output += tmp;
                    }
                }
            }

            return output;
        }
        
        /* IPアドレスをドット区切りから0埋めに変換 */
        public static string ConvertIPv4ToZero( string original )
        {
            string[] tmp    = new string[4];
            string   output = string.Empty;

            if( original.Length < 7 || original.Length > 15 )
            {
                return original;
            }
            else
            {
                for( int i = 0; i < original.Length; i++ )
                {
                    if( original[ i ] != '.' && ( original[ i ] < '0' || original[ i ] > '9' ) )
                    {
                        return original;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            tmp = original.Split( '.' );

            if( tmp.Length != 4 )
            {
                return original;
            }
            else
            {
                for( int i = 0; i < tmp.Length; i++ )
                {
                    output += tmp[ i ].PadLeft( 3, '0' );
                }
            }

            return output;
        }

        /* 入力から2値のどちらかを判断して返す(string入力) */
        private static string DualStateString( string input, string negative, string positive )
        {
            switch( input )
            {
                case "0":
                    return negative;
                case "1":
                    return positive;
                default:
                    return "Error";
            }
        }
        
        /* 入力から2値のどちらかを判断して返す(bool入力) */
        private static string DualStateString( bool input, string negative, string positive )
        {
            switch( input )
            {
                case false:
                    return negative;
                case true:
                    return positive;
                default:
                    return "Error";
            }
        }
        
        /* 表示したい文字列に応じて2値判断の関数いろいろ */
        public static string GetCheckBoxStatus( bool isChecked )
        {
            return DualStateString( isChecked, "0", "1" );
        }
    }
}
