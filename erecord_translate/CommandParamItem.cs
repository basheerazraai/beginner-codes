using System;
using System.Data;

namespace erecord_translate
{
    /// <summary>Oracleコマンドアイテム</summary>
    /// <author>T.Washida</author>
    /// <created>2016.05.30</created>
    class CommandParamItem
    {
        #region "変数"

        /// <summary>パラメータ名</summary>
        /// <summary>パラメータ値</summary>
        /// <summary>パラメータタイプ</summary>
        public String ParamName = "";
        public Object ParamValue = null;
        public DbType ParamType = DbType.Object;

        #endregion 

        #region "コンストラクタ"

            /// <summary>コンストラクタ(確実に型の判別ができる場合)</summary>
            /// <author>T.Washida</author>
            /// <created>2016.05.30</created>
            /// <param name="_name">パラメーター名</param>
            /// <param name="_value">パラメーター値</param>
            public CommandParamItem(String _name, Object _value)
            {
                ParamName = _name;
                ParamValue = _value;
                ParamType = GetDbType(_value);
            }
            
            /// <summary>コンストラクタ</summary>            
            /// <author>T.Washida</author>
            /// <created>2016.05.30</created>
            /// <param name="_name">パラメーター名</param>
            /// <param name="_value">パラメーター値</param>
            /// <param name="_type">タイプ</param>
            public CommandParamItem(String _name, Object _value, DbType _type)
            {
                ParamName = _name;
                ParamValue = _value;
                ParamType = _type;
            }

        #endregion 

        #region "メソッド"

            /// <summary>値からDbTypeを判定</summary>
            /// <author>T.Washida</author>
            /// <created>2016.05.30</created>
            /// <param name="_value">判定する値</param>
            /// <returns>DbType型を返却する</returns>
            private DbType GetDbType(object _value)
            {
                if (_value is Byte) return DbType.Byte;
                else if (_value is Byte[]) return DbType.Binary;
                else if (_value is Char || _value is Char[]) return DbType.String;
                else if (_value is DateTime) return DbType.DateTime;
                else if (_value is Decimal) return DbType.Decimal;
                else if (_value is Double) return DbType.Double;
                else if (_value is float) return DbType.Single;
                else if (_value is Int16) return DbType.Int16;
                else if (_value is Int32) return DbType.Int32;
                else if (_value is Int64) return DbType.Int64;
                else if (_value is Single) return DbType.Single;
                else if (_value is string) return DbType.String;
                else if (_value is Boolean) return DbType.Boolean;
                else return DbType.String;
            }

        #endregion 
        
    }
}
