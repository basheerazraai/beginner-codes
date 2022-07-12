using System;
using System.Data;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
namespace amazon_translate_window
{

    /// <summary>データベース制御クラス</summary>
    class DatabaseAccess : IDisposable
    {

        #region "変数"

        /// <summary>接続情報保持</summary>
        private OracleConnection Connection;

        /// <summary>実行コマンド保持</summary>
        OracleCommand Cmd;

        /// <summary>トランザクション</summary>
        OracleTransaction Tran;

        #endregion

        #region "コンストラクタ"

        /// <summary>コンストラクタ/DB接続</summary>
        //public DatabaseAccess()
        //{
        //    try
        //    {
        //        this.Connect(Setting.UserId, Setting.UserPass, Setting.DataBasePath);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        // 引数指定コンストラクタ 水島
        public DatabaseAccess(string UserId, string UserPass, string DataBasePath)
        {
            try
            {
                this.Connect(UserId, UserPass, DataBasePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region "DB接続関連"

        /// <summary>DB接続</summary>
        /// <author>T.Washida</author>
        /// <created>2016.05.30</created>
        /// <param name="_userName">ユーザー名</param>
        /// <param name="_password">パスワード</param>
        /// <param name="_dbPath">データベースパス</param>
        private void Connect(string _userName, string _password, string _dbPath)
        {
            try
            {
                this.Connection = new OracleConnection
                    ("User Id=" + _userName + "; "
                    + "Password=" + _password + "; "
                    + "Data Source=" + _dbPath + ";"
                    + " Connection Timeout=120; ");
                this.Connection.Open();
            }
            catch (Exception ex)
            {
                // Log.SetErrReason("データベース接続に失敗しました。");
                throw ex;
            }
        }

        #endregion

        #region "SQL実行関連"

        /// <summary>トランザクション開始処理</summary>
        /// <author>T.Washida</author>
        /// <created>2016.05.30</created>
        public void BeginTransaction()
        {
            try
            {
                this.Tran = this.Connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>コミット処理</summary>
        /// <author>T.Washida</author>
        /// <created>2016.06.09</created>
        public void Commit()
        {
            try
            {
                if (this.Tran != null)
                {
                    this.Tran.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>ロールバック</summary>        
        /// <author>T.Washida</author>
        /// <created>2016.06.09</created>
        public void RollBack()
        {
            try
            {
                if (this.Tran != null)
                {
                    this.Tran.Rollback(); ;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>Select文実行</summary>
        /// <author>T.Washida</author>
        /// <created>2016.05.30</created>
        /// <param name="_query">SQL文</param>
        /// <param name="_cmdParams">パラメーター</param>
        /// <returns>抽出結果</returns>
        public DataTable SelectQuery(string _query)
        {
            DataTable dt;
            OracleDataAdapter oAdpt;
            try
            {
                this.Cmd = new OracleCommand(_query, this.Connection);
                //if (_cmdParams != null)
                //{
                //    this.SetParameter(_cmdParams.Get());
                //}
                this.Cmd.CommandType = CommandType.Text;
                oAdpt = new OracleDataAdapter(this.Cmd);
                dt = new DataTable();
                oAdpt.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                // Log.SetErrRecord(ConvertParamToQuery());
                throw ex;
            }
        }

        /// <summary>Update文実行</summary>
        /// <author>T.Washida</author>
        /// <created>2016.05.30</created>
        /// <param name="_query">SQL文</param>
        /// <param name="_cmdParams">パラメーター</param>
        /// <returns>更新レコード数</returns>
        //public int UpdateQuery(string _query, CommandParameters _cmdParams = null)
        //{
        //    int cnt;
        //    try
        //    {
        //        this.Cmd = new OracleCommand(_query, this.Connection);
        //        if (_cmdParams != null)
        //        {
        //            this.SetParameter(_cmdParams.Get());
        //        }
        //        this.Cmd.CommandType = CommandType.Text;
        //        cnt = this.Cmd.ExecuteNonQuery();

        //        Console.WriteLine(ConvertParamToQuery());
        //        return cnt;

        //    }
        //    catch (Exception ex)
        //    {
        //        //エラークエリをログに出力
        //        Log.SetErrRecord(ConvertParamToQuery());
        //        throw ex;
        //    }
        //}

        ///// <summary>パラメーターセット</summary>
        ///// <author>T.Washida</author>
        ///// <created>2016.05.30</created>
        ///// <param name="_paramList">パラメーター</param>
        //private void SetParameter(List<CommandParamItem> _paramList)
        //{
        //    try
        //    {
        //        if (this.Cmd != null)
        //        {
        //            this.Cmd.Parameters.Clear();
        //        }

        //        if (_paramList == null)
        //        {
        //            return;
        //        }

        //        foreach (CommandParamItem pram in _paramList)
        //        {
        //            OracleParameter parameter = this.Cmd.CreateParameter();

        //            if (pram == null)
        //            {
        //                parameter.Value = (null);
        //            }
        //            else
        //            {
        //                parameter.ParameterName = pram.ParamName;
        //                parameter.DbType = pram.ParamType;
        //                parameter.Value = pram.ParamValue;
        //            }

        //            this.Cmd.Parameters.Add(parameter);

        //        }

        //        this.Cmd.Prepare();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>パラメーターをSQL文に代入</summary>
        /// <returns></returns>
        private String ConvertParamToQuery()
        {
            try
            {
                String query = this.Cmd.CommandText;
                //パラメータを変換する
                foreach (OracleParameter param in this.Cmd.Parameters)
                {
                    query = query.Replace(param.ParameterName, "'" + param.Value.ToString() + "'");
                }

                return query;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region "IDisposable実装"

        /// <summary>解放処理</summary>
        /// <author>T.Washida</author>
        /// <created>2016.05.30</created>
        public void Dispose()
        {
            try
            {
                if (this.Connection != null)
                {
                    this.Connection.Close();
                    this.Connection.Dispose();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
