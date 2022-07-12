using System;
using System.Data;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace erecord_translate
{

    class DatabaseAccess : IDisposable
    {

        #region "変数"

        /// <summary>接続情報保持</summary>
        /// <summary>実行コマンド保持</summary>
        /// <summary>トランザクション</summary>
        private static OracleConnection Connection;
        private static OracleCommand Cmd;
        private static OracleTransaction Tran;

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
                DatabaseAccess.Connect(UserId, UserPass, DataBasePath);
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
        public static void Connect(string _userName, string _password, string _dbPath)
        {

            try
            {
                DatabaseAccess.Connection = new OracleConnection
                    ("User Id=" + _userName + "; "
                    + "Password=" + _password + "; "
                    + "Data Source=" + _dbPath + ";"
                    + " Connection Timeout=120; ");
                DatabaseAccess.Connection.Open();
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
        public static void BeginTransaction()
        {
            try
            {
                DatabaseAccess.Tran = DatabaseAccess.Connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>コミット処理</summary>
        /// <author>T.Washida</author>
        /// <created>2016.06.09</created>
        public static void Commit()
        {
            try
            {
                if (DatabaseAccess.Tran != null)
                {
                    DatabaseAccess.Tran.Commit();
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
        public static void RollBack()
        {
            try
            {
                if (DatabaseAccess.Tran != null)
                {
                    DatabaseAccess.Tran.Rollback(); ;
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
                DatabaseAccess.Cmd = new OracleCommand(_query, DatabaseAccess.Connection);
                //if (_cmdParams != null)
                //{
                //    this.SetParameter(_cmdParams.Get());
                //}
                DatabaseAccess.Cmd.CommandType = CommandType.Text;
                oAdpt = new OracleDataAdapter(DatabaseAccess.Cmd);
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
        public int UpdateQuery(string _query, CommandParameters _cmdParams = null)
        {
           int cnt;
            try
            {
                DatabaseAccess.Cmd = new OracleCommand(_query, DatabaseAccess.Connection);
                if (_cmdParams != null)
                {
                    DatabaseAccess.SetParameter(_cmdParams.Get());
                }
                DatabaseAccess.Cmd.CommandType = CommandType.Text;
                cnt = DatabaseAccess.Cmd.ExecuteNonQuery();

                Console.WriteLine(ConvertParamToQuery());
                return cnt;

            }
            catch (Exception ex)
            {
                //エラークエリをログに出力
                WriteLog.LogFileWriteProc("SQL", ConvertParamToQuery(), ex.ToString());
                throw ex;
            }
        }

        ///// <summary>パラメーターセット</summary>
        ///// <author>T.Washida</author>
        ///// <created>2016.05.30</created>
        ///// <param name="_paramList">パラメーター</param>
        private static void SetParameter(List<CommandParamItem> _paramList)
        {
            try
            {
                if (DatabaseAccess.Cmd != null)
                {
                    DatabaseAccess.Cmd.Parameters.Clear();
                }

                if (_paramList == null)
                {
                    return;
                }

                foreach (CommandParamItem pram in _paramList)
                {
                    OracleParameter parameter = DatabaseAccess.Cmd.CreateParameter();

                    if (pram == null)
                    {
                        parameter.Value = (null);
                    }
                    else
                    {
                        parameter.ParameterName = pram.ParamName;
                        parameter.DbType = pram.ParamType;
                        parameter.Value = pram.ParamValue;
                    }

                    DatabaseAccess.Cmd.Parameters.Add(parameter);

                }

                DatabaseAccess.Cmd.Prepare();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>パラメーターをSQL文に代入</summary>
        /// <returns></returns>
        private static String ConvertParamToQuery()
        {
            try
            {
                String query = DatabaseAccess.Cmd.CommandText;
                //パラメータを変換する
                foreach (OracleParameter param in DatabaseAccess.Cmd.Parameters)
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

        public void Dispose()
        {
            try
            {
                if (DatabaseAccess.Connection != null)
                {
                    DatabaseAccess.Connection.Close();
                    DatabaseAccess.Connection.Dispose();
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
