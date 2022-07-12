using System;
using System.Collections.Generic;
using System.Data;

namespace erecord_translate
{
    /// <summary>Oracleコマンドパラメーター群</summary>
    /// <author>T.Washida</author>
    /// <created>2016.06.01</created>
    class CommandParameters
    {
        /// <summary>パラメータ群</summary>
        public List<CommandParamItem> Params = new List<CommandParamItem>();

        /// <summary>パラメーターの取得</summary>
        /// <author>T.Washida</author>
        /// <created>2016.06.01</created>
        /// <returns>作成されたパラメーター</returns>
        public List<CommandParamItem> Get()
        {
            return Params; 
        }         

        /// <summary>パラメーターを追加</summary>
        /// <author>T.Washida</author>
        /// <created>2016.06.01</created>
        /// <param name="_name">パラメーター名</param>
        /// <param name="_value">パラメーター値</param>
        public void Add(String _name, Object _value)
        {
            CommandParamItem item = new CommandParamItem(_name, _value);
            Params.Add(item);
        }

        /// <summary>パラメーターを追加</summary>
        /// <author>T.Washida</author>
        /// <created>2016.06.01</created>
        /// <param name="_name">パラメーター名</param>
        /// <param name="_value">パラメーター値</param>
        /// <param name="_dbType">データベース型</param>
        public void Add(String _name, Object _value, DbType _dbType)
        {
            CommandParamItem item = new CommandParamItem(_name, _value, _dbType);
            Params.Add(item);
        }
    }
}
