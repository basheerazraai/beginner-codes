using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Maintenance
{
    /* Oracleに投げるクエリのクラス */
    public class OracleQuery
    {
        public string       str     { get; set; }
        public ArrayList    param;
        public DataSet      result;
        public QueryMode    mode    { get; set; }

        /* コンストラクタ */
        public OracleQuery()
        {
            str     = string.Empty;
            param   = new ArrayList();
            result  = new DataSet();
            mode    = QueryMode.None;
        }

        /* 文字列をカラムのデータ型にあわせてパラメータ化する */
        public static OleDbParameter GetOracleParameter( OracleDataType type, string key )
        {
            switch( type )
            {
                case OracleDataType.CHAR:
                    return new OleDbParameter( "@" + key, OleDbType.VarChar );
                case OracleDataType.VARCHAR2:
                    return new OleDbParameter( "@" + key, OleDbType.Char );
                case OracleDataType.DATE:
                    return new OleDbParameter( "", string.Empty );
                case OracleDataType.NUMBER:
                    return new OleDbParameter( "@" + key, OleDbType.Numeric );
                default:
                    return new OleDbParameter( "", string.Empty );
            }
        }
    }

    /* Oracle接続とクエリの実行を行うクラス */
    public class OracleConnection
    {
        private string sid;
        private string user;
        private string password;
        private OleDbConnection     conn    = null;
        private OleDbCommand        command = null;
        private OleDbDataAdapter    adapter = null;

        /* コンストラクタ */
        public OracleConnection()
        {
        }

        public OracleConnection( string s, string u, string p )
        {
            this.sid        = s;
            this.user       = u;
            this.password   = p;
        }        

        /* 接続を確立する*/
        public void Connect()
        {
            string profile = String.Empty;

            profile = "Provider=OraOLEDB.Oracle";
            profile += "; Data Source=" + sid;
            profile += "; User ID="     + user;
            profile += "; Password="    + password;
            
            try
            {
                conn = new OleDbConnection( profile );
                adapter = new OleDbDataAdapter();
            }
            catch( Exception ex )
            {
                throw ex;
            }

            return;
        }

        /* クエリを実行する */
        public int Execute( ref OracleQuery query )
        {
            DataSet data = new DataSet();

            try
            {
                command = new OleDbCommand( query.str, conn );

                for( int i = 0; i < query.param.Count; i++ )
                {
                    command.Parameters.Add( ( OleDbParameter )( query.param[ i ] ) );
                }

                conn.Open();
                
                if( conn.State != ConnectionState.Open )
                {
                    throw new Exception( "Connection failed." );
                }
                else
                {
                    switch( query.mode )
                    {
                        case QueryMode.None :
                            throw new Exception( "Query mode is invalid." );
                        case QueryMode.Select :
                            adapter.SelectCommand = command;
                            adapter.Fill( query.result );
                            return query.result.Tables[ 0 ].Rows.Count;
                        case QueryMode.Insert :
                        case QueryMode.Update :
                        case QueryMode.Delete :
                            return command.ExecuteNonQuery();
                        default :
                            throw new Exception( "Query is null" );  
                    }
                }
            }
            catch( Exception ex )
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }

    /* OracleDB用のデータドメインのひな型 */
    public partial class OracleDomain
    {
        public string                                   tableName { get; set; }
        public Dictionary< string, OracleDataType >     columns;
        public string[]                                 pkey;
        public string[]                                 sort;
        public Dictionary< string, Object >             data;
        public OracleQuery                              query;        

        /* コンストラクタ */
        protected OracleDomain()
        {
            tableName   = String.Empty;
            columns     = new Dictionary<string, OracleDataType>();
            pkey        = new string[] {};
            sort        = new string[] {};
            data        = new Dictionary<string, Object>();
            query       = new OracleQuery();
        }

        /* ドメインが保持しているデータを取得する */
        protected string GetValue( string key )
        {
            Object obj = null;

            data.TryGetValue( key, out obj );

            return ( string )obj;
        }

        /* ドメインにセットする値を画面上の表示値からDB上の内部値に変換する */
        private Object ConvertSetValue( string key, Object obj )
        {
            switch( key )
            {
                case "ipac":
                    return Common.ConvertIPv4ToZero( ( string )obj );
                default :
                    return obj;
            }
        }

        /* ドメインの指定したキーに値をセットする */
        protected void SetValue( string key, Object value )
        {
            if( columns.ContainsKey( key ) == false )
            {
                throw new Exception( "Column " + key + " does not exist." );
            }
            else
            {
                data.Add( key, ConvertSetValue( key, value ) );

                return;
            }
        }
        
        /* ドメインにセットされた値の整合性をチェックする */
        public virtual bool IsValid() 
        {
            foreach( KeyValuePair<string, Object> kvp in data )
            {
                if( IsValid( kvp.Key ) == false )   //カラム単位のチェック
                {
                    return false;
                }
                else
                {
                    continue;
                }
            }

            return true; 
        }

        public virtual string GetKeyMakeQuery() { return string.Empty; }
        public virtual void SetKey( Object value ) { SetValue( pkey[ 0 ], value ); }
        public virtual void SetData( Dictionary<string, string> input ) {}
    }
}
