using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maintenance
{
    /* リモート接続ビューの定義 */
    class ViewVP00RemtConn : OracleDomain
    {
        public ViewVP00RemtConn()
        {
            tableName = Const.VIEW_V_P00_REMT_CONN;

            columns = new Dictionary<string, OracleDataType>()
                {
                      { "cnmv", OracleDataType.CHAR }
                    , { "sysv", OracleDataType.CHAR }
                    , { "alsv", OracleDataType.CHAR }
                    , { "rl1v", OracleDataType.CHAR }
                    , { "rl2v", OracleDataType.CHAR }
                    , { "ipav", OracleDataType.CHAR }
                    , { "dmnv", OracleDataType.CHAR }
                    , { "usrv", OracleDataType.CHAR }
                    , { "pwdv", OracleDataType.CHAR }
                };

            pkey = new string[] {};
            sort = new string[] { "ipav", "usrv" };
        }       
    }
    
    /* 機械台数(工程別)ビューの定義 */
    class ViewVP00McntSect : OracleDomain
    {
        public ViewVP00McntSect()
        {
            tableName = Const.VIEW_V_P00_MCNT_SECT;

            columns = new Dictionary<string, OracleDataType>()
                {
                      { "cnmv", OracleDataType.CHAR }
                    , { "snmv", OracleDataType.CHAR }
                    , { "cntn", OracleDataType.NUMBER }
                };

            pkey = new string[] {};
            sort = new string[] { "cnmv", "snmv" };
        }       
    }
    
    /* 機械台数(機種別)ビューの定義 */
    class ViewVP00McntMtyp : OracleDomain
    {
        public ViewVP00McntMtyp()
        {
            tableName = Const.VIEW_V_P00_MCNT_MTYP;

            columns = new Dictionary<string, OracleDataType>()
                {
                      { "cnmv", OracleDataType.CHAR }
                    , { "snmv", OracleDataType.CHAR }
                    , { "mtpv", OracleDataType.CHAR }
                    , { "cntn", OracleDataType.NUMBER }
                };

            pkey = new string[] {};
            sort = new string[] { "cnmv", "snmv", "mtpv" };
        }       
    }

    /* 会社マスタの定義 */
    class TableMstCmpn : OracleDomain
    {
        public TableMstCmpn()
        {
            tableName = Const.TABLE_MST_CMPN;

            columns = new Dictionary<string, OracleDataType>()
                {
                      { "cmpc", OracleDataType.CHAR }
                    , { "cnmv", OracleDataType.CHAR }
                    , { "rubv", OracleDataType.CHAR }
                };

            pkey = new string[] { "cmpc" };
            sort = new string[] { "cmpc" };
        }

        public override void SetData( Dictionary<string, string> input )
        {
            string tmp;

            input.TryGetValue( Const.CONTROL_TEXTPARAM1, out tmp );
            SetValue( "cmpc", tmp );
            input.TryGetValue( Const.CONTROL_TEXTPARAM2, out tmp );
            SetValue( "cnmv", tmp );
            input.TryGetValue( Const.CONTROL_TEXTPARAM3, out tmp );
            SetValue( "rubv", tmp );
        }
    }

    /* システム分類マスタ(メッセージマスタ)の定義 */
    class TableMstSysc : OracleDomain
    {
        public TableMstSysc()
        {
            tableName = Const.TABLE_MST_MESG;

            columns = new Dictionary<string, OracleDataType>()
                {
                      { "msgc", OracleDataType.CHAR }
                    , { "msgv", OracleDataType.VARCHAR2 }
                };

            pkey = new string[] { "msgc" };
            sort = new string[] { "msgc" };
        }

        public override string GetKeyMakeQuery()
        {
            string str = string.Empty;

            str += "SELECT 'MST01' || TO_CHAR( numn + 1, 'FM00000' ) AS keyc ";
            str += "FROM ";
            str += "( SELECT TO_NUMBER( SUBSTR( LPAD( COALESCE( MAX( msgc ), '0' ), 10, '0' ), 6, 5 ) ) AS numn ";
            str += " FROM mst_mesg WHERE msgc LIKE 'MST01%' ) ";

            return str;
        }
        
        public override void SetData( Dictionary<string, string> input )
        {
            string tmp;

            input.TryGetValue( Const.CONTROL_TEXTPARAM1, out tmp );
            SetValue( "msgv", tmp );
        }
    }
    
    /* 機器役割マスタ(メッセージマスタ)の定義 */
    class TableMstEqrl : OracleDomain
    {
        public TableMstEqrl()
        {
            tableName = Const.TABLE_MST_MESG;

            columns = new Dictionary<string, OracleDataType>()
                {
                      { "msgc", OracleDataType.CHAR }
                    , { "msgv", OracleDataType.VARCHAR2 }
                };

            pkey = new string[] { "msgc" };
            sort = new string[] { "msgc" };
        }

        public override string GetKeyMakeQuery()
        {
            string str = string.Empty;

            str += "SELECT 'MST02' || TO_CHAR( numn + 1, 'FM00000' ) AS keyc ";
            str += "FROM ";
            str += "( SELECT TO_NUMBER( SUBSTR( LPAD( COALESCE( MAX( msgc ), '0' ), 10, '0' ), 6, 5 ) ) AS numn ";
            str += " FROM mst_mesg WHERE msgc LIKE 'MST02%' ) ";

            return str;
        }
        
        public override void SetData( Dictionary<string, string> input )
        {
            string tmp;

            input.TryGetValue( Const.CONTROL_TEXTPARAM1, out tmp );
            SetValue( "msgv", tmp );
        }
    }

    /* IT機器マスタの定義 */
    class TableMstIteq : OracleDomain
    {
        public TableMstIteq()
        {
            tableName = Const.TABLE_MST_ITEQ;

            columns = new Dictionary<string, OracleDataType>()
                {
                      { "itec", OracleDataType.CHAR }
                    , { "cmpc", OracleDataType.CHAR }
                    , { "sysc", OracleDataType.CHAR }
                    , { "alsv", OracleDataType.VARCHAR2 }
                    , { "ipac", OracleDataType.CHAR }
                    , { "rl1c", OracleDataType.CHAR }
                    , { "rl2c", OracleDataType.CHAR }
                };

            pkey = new string[] { "itec" };
        }       

        public override string GetKeyMakeQuery()
        {
            string str = string.Empty;
            string cmpc = ( string )( GetValue( "cmpc" ) );

            str += "SELECT '" + cmpc + "' || TO_CHAR( numn + 1, 'FM0000' ) AS keyc ";
            str += "FROM ";
            str += "( SELECT TO_NUMBER( SUBSTR( LPAD( COALESCE( MAX( itec ), '0' ), 10, '0' ), 7, 4 ) ) AS numn ";
            str += " FROM mst_iteq WHERE itec LIKE '" + cmpc +"%' ) ";

            return str;
        }
        
        public override void SetData( Dictionary<string, string> input )
        {
            string tmp;

            input.TryGetValue( Const.CONTROL_LISTPARAM1, out tmp );
            SetValue( "cmpc", tmp );
            input.TryGetValue( Const.CONTROL_LISTPARAM2, out tmp );
            SetValue( "sysc", tmp );
            input.TryGetValue( Const.CONTROL_LISTPARAM3, out tmp );
            SetValue( "rl1c", tmp );
            input.TryGetValue( Const.CONTROL_LISTPARAM4, out tmp );
            SetValue( "rl2c", tmp );
            input.TryGetValue( Const.CONTROL_TEXTPARAM1, out tmp );
            SetValue( "alsv", tmp );
            input.TryGetValue( Const.CONTROL_TEXTPARAM2, out tmp );
            SetValue( "ipac", tmp );
        }
    }
    
    /* リモート接続マスタの定義 */
    class TableMstRemtConn : OracleDomain
    {
        public TableMstRemtConn()
        {
            tableName = Const.TABLE_MST_REMT_CONN;

            columns = new Dictionary<string, OracleDataType>()
                {
                      { "rmtc", OracleDataType.CHAR }
                    , { "itec", OracleDataType.CHAR }
                    , { "dmnv", OracleDataType.VARCHAR2 }
                    , { "usrv", OracleDataType.VARCHAR2 }
                    , { "pwdv", OracleDataType.VARCHAR2 }
                };

            pkey = new string[] { "rmtc" };
        }       
        
        public override string GetKeyMakeQuery()
        {
            string str = string.Empty;
            string itec = ( string )( GetValue( "itec" ) );

            str += "SELECT '" + itec + "' || TO_CHAR( numn + 1, 'FM00' ) AS keyc ";
            str += "FROM ";
            str += "( SELECT TO_NUMBER( SUBSTR( LPAD( COALESCE( MAX( rmtc ), '0' ), 12, '0' ), 11, 2 ) ) AS numn ";
            str += " FROM mst_remt_conn WHERE rmtc LIKE '" + itec +"%' ) ";

            return str;
        }

        public override void SetData( Dictionary<string, string> input )
        {
            string tmp;

            input.TryGetValue( Const.CONTROL_LISTPARAM3, out tmp );
            SetValue( "itec", tmp );
            input.TryGetValue( Const.CONTROL_TEXTPARAM1, out tmp );
            SetValue( "dmnv", tmp );
            input.TryGetValue( Const.CONTROL_TEXTPARAM2, out tmp );
            SetValue( "usrv", tmp );
            input.TryGetValue( Const.CONTROL_TEXTPARAM3, out tmp );
            SetValue( "pwdv", tmp );
        }
    }
    
    /* 会社マスタの定義 */
    class TableMstSect : OracleDomain
    {
        public TableMstSect()
        {
            tableName = Const.TABLE_MST_SECT;

            columns = new Dictionary<string, OracleDataType>()
                {
                      { "sctc", OracleDataType.CHAR }
                    , { "snmv", OracleDataType.CHAR }
                };

            pkey = new string[] { "sctc" };
            sort = new string[] { "sctc" };
        }

        public override void SetData( Dictionary<string, string> input )
        {
            string tmp;

            input.TryGetValue( Const.CONTROL_TEXTPARAM1, out tmp );
            SetValue( "sctc", tmp );
            input.TryGetValue( Const.CONTROL_TEXTPARAM2, out tmp );
            SetValue( "snmv", tmp );
        }
    }

    /* 機械台数マスタの定義 */
    class TableMstMcnt : OracleDomain
    {
        public TableMstMcnt()
        {
            tableName = Const.TABLE_MST_MCNT;

            columns = new Dictionary<string, OracleDataType>()
                {
                      { "keyc", OracleDataType.CHAR }
                    , { "cmpc", OracleDataType.CHAR }
                    , { "sctc", OracleDataType.CHAR }
                    , { "mtpv", OracleDataType.CHAR }
                    , { "cntn", OracleDataType.NUMBER }
                };

            pkey = new string[] { "keyc" };
        }       
        
        public override string GetKeyMakeQuery()
        {
            string str = string.Empty;
            string cmpc = ( string )( GetValue( "cmpc" ) );
            string sctc = ( string )( GetValue( "sctc" ) );
            string mtpv = ( ( string )( GetValue( "mtpv" ) ) ).PadLeft( 12, '0' );

            str += "SELECT '" + cmpc + sctc + mtpv + "' AS keyc ";
            str += "FROM DUAL";

            return str;
        }

        public override void SetData( Dictionary<string, string> input )
        {
            string tmp;
            
            input.TryGetValue( Const.CONTROL_LISTPARAM1, out tmp );
            SetValue( "cmpc", tmp );
            input.TryGetValue( Const.CONTROL_LISTPARAM2, out tmp );
            SetValue( "sctc", tmp );
            input.TryGetValue( Const.CONTROL_TEXTPARAM1, out tmp );
            SetValue( "mtpv", tmp );
            input.TryGetValue( Const.CONTROL_TEXTPARAM2, out tmp );
            SetValue( "cntn", tmp );
        }
    }
    
    /* OracleDBのデータドメインのうち、カラムごとの入力値の制約の部分 */
    public partial class OracleDomain
    {        
        /* ドメインにセットされた値の整合性をチェックする(カラム単位) */
        protected virtual bool IsValid( string key )
        {
            switch( key )
            {
                case "ipac" :
                    if( GetValue( key ).Length != 12 )
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case "itec":
                    if( GetValue( key ).Length <= 0 )
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case "mtpv":
                    if( GetValue( key ).Length > 12 )
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                default :
                    return true;
            }
        }
    }
}
