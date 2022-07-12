using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Maintenance
{
    /* 動作モードクラス */
    public abstract class ProgramMode
    {
        protected OracleDomain  table;

        /* コンボボックス関連 */
        public ComboBoxSetting  cbsDataDomain;
        public ComboBoxSetting  cbsParam1;
        public ComboBoxSetting  cbsParam2;
        public ComboBoxSetting  cbsParam3;
        public ComboBoxSetting  cbsParam4;

        /* フォームのビジュアル制御 */
        public bool     flagViewMode;       //検索対象がビューのときにtrueとする
        public bool     flagActionButton;   //特殊動作ボタンを利用する場合にtrueとする
        public string   labelActionButton;  //特殊動作ボタンのラベル

        /* テキストボックス関連 */
        public string[] labelsTextBox;      //テキストボックスのラベル

        /* チェックボックス関連 */
        public string[]  labelsCheckBox;    //チェックボックスのラベル

        /* データグリッド関連 */
        public string[]  labelsGridHeader;  //データグリッドのヘッダテキスト
        public Dictionary<string, string> converter;    //グリッド内の文字列とその文字列を変換する指定子
        public Dictionary<string, string> selecter;  //SELECT文のパラメータとそこに入れる値を持つコントロールの指定子

        /* コンストラクタ */
        public ProgramMode()
        {
            cbsDataDomain = new ComboBoxSetting();
            cbsParam1 = new ComboBoxSetting();
            cbsParam2 = new ComboBoxSetting();
            cbsParam3 = new ComboBoxSetting();
            cbsParam4 = new ComboBoxSetting();

            /* データ領域一覧 */
            cbsDataDomain.label         = "データ領域";
            cbsDataDomain.AddItem( Const.VIEW_V_P00_REMT_CONN, "リモート接続ビュー" );
            cbsDataDomain.AddItem( Const.VIEW_V_P00_MCNT_SECT, "機械台数(工程別)ビュー" );
            cbsDataDomain.AddItem( Const.VIEW_V_P00_MCNT_MTYP, "機械台数(機種別)ビュー" );
            cbsDataDomain.AddItem( Const.TABLE_MST_CMPN, "会社マスタ" );
            cbsDataDomain.AddItem( Const.TABLE_MST_ITEQ, "IT機器マスタ" );
            cbsDataDomain.AddItem( Const.TABLE_MST_SYSC, "システム分類マスタ" );
            cbsDataDomain.AddItem( Const.TABLE_MST_EQRL, "機器役割マスタ" );
            cbsDataDomain.AddItem( Const.TABLE_MST_REMT_CONN, "リモート接続マスタ" );
            cbsDataDomain.AddItem( Const.TABLE_MST_SECT, "工程マスタ" );
            cbsDataDomain.AddItem( Const.TABLE_MST_MCNT, "機械台数マスタ" );

            flagViewMode        = false;
            flagActionButton    = false;
            labelActionButton   = "Action";

            labelsTextBox   = new string[] { "", "", "" };    //ラベルが空白のテキストボックスは非表示

            labelsCheckBox = new string[] { "", "", "" };   //ラベルが空白のチェックボックスは非表示

            labelsGridHeader = new string[] { "", "", "", "", "", "", "", "", "", "" };   //ラベルが空白のグリッド列は非表示
            converter   = new Dictionary<string, string>();
            selecter    = new Dictionary<string, string>();
            
        }

        /* カラムのデータタイプを取得する */
        public OracleDataType GetColumnType( string columnName )
        {
            OracleDataType  type    = OracleDataType.None;
            
            table.columns.TryGetValue( columnName, out type );

            return type;
        }
        
        /* Insert用のクエリを取得する */
        public virtual OracleQuery GetInsertQuery()
        {
            OracleQuery     query   = new OracleQuery();
            OleDbParameter  p       = new OleDbParameter();
            int             count   = 0;
            string          targets = string.Empty;
            string          objects = string.Empty;
            string          value   = string.Empty;
            OracleDataType  type    = OracleDataType.None;

            try
            {
                if( table.IsValid() == false )
                {
                    throw new Exception( "Invalid value." );
                }
                else
                {
                }

                foreach( KeyValuePair<string, object> kvp in table.data )
                {
                    type = GetColumnType( kvp.Key );

                    if( count > 0 )
                    {
                        targets += ", ";
                        objects += ", ";
                    }
                    else
                    {
                    }

                    targets += kvp.Key;
                    objects += "?";
                    
                    p = OracleQuery.GetOracleParameter( type, kvp.Key );

                    p.Value = ( string )( kvp.Value );
                    query.param.Add( p );

                    count++;
                }

                query.mode = QueryMode.Insert;
                query.str = "INSERT INTO " + table.tableName + "( " + targets + " ) ";
                query.str += "VALUES( " + objects + ") ";
            }
            catch( Exception ex )
            {
                query.str = string.Empty;
                throw ex;
            }

            return query;
        }
        
        /* Select用のクエリを取得する */
        public virtual OracleQuery GetSelectQuery()
        {
            OracleQuery     query   = new OracleQuery();
            int             count   = 0;
            string          targets = string.Empty;

            try
            {
                foreach( KeyValuePair<string, OracleDataType> kvp in table.columns )
                {
                    if( count > 0 )
                    {
                        targets += ", ";
                    }
                    else
                    {
                    }

                    targets += kvp.Key;

                    count++;
                }

                query.mode = QueryMode.Select;
                query.str = "SELECT " + targets + " FROM " + table.tableName + " ";                
                targets = string.Empty;

                if( selecter.Count > 0 )
                {
                    count = 0;
                    query.str += " WHERE ";

                    foreach( KeyValuePair<string, string> kvp in selecter )
                    {
                        if( count > 0 )
                        {
                            query.str += "AND ";
                        }
                        else
                        {
                        }

                        query.str += kvp.Value + " = ? ";
                        count++;
                    }
                }
                else
                {
                }

                count = 0;

                for( count = 0; count < table.sort.Length; count++ )
                {
                    if( count > 0 )
                    {
                        query.str += ", ";
                    }
                    else
                    {
                        query.str += " ORDER BY ";
                    }

                    query.str += table.sort[ count ] + " ASC";
                }
            }
            catch( Exception )
            {
                query.str = string.Empty;
            }

            return query;
        }
        
        /* Update用のクエリを取得する */
        public virtual OracleQuery GetUpdateQuery( string[] keyValues )
        {
            OracleQuery     query   = new OracleQuery();
            OleDbParameter  p       = new OleDbParameter();
            int             count   = 0;
            string          targets = string.Empty;
            string          objects = string.Empty;
            string          value   = string.Empty;
            OracleDataType  type    = OracleDataType.None;

            try
            {
                if( table.IsValid() == false )
                {
                    throw new Exception( "Invalid value." );
                }
                else
                {
                }

                foreach( KeyValuePair<string, object> kvp in table.data )
                {
                    type = GetColumnType( kvp.Key );

                    if( kvp.Value.ToString().Length <= 0 )
                    {
                        continue;
                    }
                    else if( count > 0 )
                    {
                        targets += ", ";
                    }
                    else
                    {
                    }

                    targets += kvp.Key + " = ?";

                    p = OracleQuery.GetOracleParameter( type, kvp.Key );

                    p.Value = ( string )( kvp.Value );
                    query.param.Add( p );                    

                    count++;
                }

                for( int i = 0; i < table.pkey.Length; i++ )
                {
                    table.columns.TryGetValue( table.pkey[ i ], out type );

                    if( i > 0 )
                    {
                        objects += " AND ";
                    }
                    else
                    {
                    }

                    p = OracleQuery.GetOracleParameter( type, table.pkey[ i ] );
                    p.Value = keyValues[ i ];
                    query.param.Add( p );

                    objects += table.pkey[ i ] + " = ?";
                }

                query.mode = QueryMode.Insert;
                query.str = "UPDATE " + table.tableName + " SET " + targets + " ";
                query.str += " WHERE " + objects;
            }
            catch( Exception ex )
            {
                query.str = string.Empty;
                throw ex;
            }

            return query;
        }
        
        /* Delete用のクエリを取得する */
        public virtual OracleQuery GetDeleteQuery( string[] keyValues )
        {
            OracleQuery     query   = new OracleQuery();
            OleDbParameter  p       = new OleDbParameter();
            string          objects = string.Empty;
            OracleDataType  type    = OracleDataType.None;

            
            for( int i = 0; i < table.pkey.Length; i ++ )
            {
                table.columns.TryGetValue( table.pkey[ i ], out type );

                if( i > 0 )
                {
                    objects += " AND ";
                }
                else
                {
                }

                p = OracleQuery.GetOracleParameter( type, table.pkey[ i ] );

                p.Value = keyValues[ i ];
                query.param.Add( p );

                objects += table.pkey[ i ] + " = ?";
            }
            
            query.mode = QueryMode.Delete;
            query.str = "DELETE FROM " + table.tableName + " WHERE " + objects;

            return query;
        }
        
        /* コンボボックスのリスト作成用のクエリを取得する */
        public virtual OracleQuery GetListQuery( int number )
        {
            OracleQuery query   = new OracleQuery();

            query.mode = QueryMode.Select;

            switch(number)
            {
                case 1:
                    query.str = cbsParam1.queryString;
                    break;
                case 2:
                    query.str = cbsParam2.queryString;
                    break;
                case 3:
                    query.str = cbsParam3.queryString;
                    break;
                case 4:
                    query.str = cbsParam4.queryString;
                    break;
                default:
                    break;
            }

            if( string.IsNullOrEmpty( query.str ) == true )
            {
                return null;
            }
            else
            {
                return query;
            }
        }

        /* テーブルごとに設定したキー作成用のクエリを取得する　*/
        public virtual string GetKeyMakeQuery()
        {
            return table.GetKeyMakeQuery();
        }

        /* 指定したカラム名の列番号を取得する */
        public virtual int GetColumnNumber( string columnName )
        {
            int count = 0;

            foreach( KeyValuePair<string, OracleDataType> kvp in table.columns )
            {
                if( columnName == kvp.Key )
                {
                    return count;
                }
                else
                {
                    count ++;
                }
            }

            return -1;
        }

        /* 定義時にキー指定したカラムの列番号をすべて取得する */
        public virtual int[] GetKeyColumns()
        {
            int count = 0;
            ArrayList numbers = new ArrayList();            

            for( int i = 0; i< table.pkey.Length; i ++ )
            {
                count = 0;

                foreach( KeyValuePair<string, OracleDataType> kvp in table.columns )
                {
                    if( table.pkey[ i ] == kvp.Key )
                    {
                        numbers.Add( count );
                    }
                    else
                    {
                    }

                    count ++;
                }
            }

            return ( int[] )numbers.ToArray( typeof( int ) );
        }

        /* クエリをもとに作成したキーをドメインにセットする */
        public virtual void SetKey( object value )
        {
            table.SetKey( value );

            return;
        }

        /* 入力値をもとにテーブルに登録するデータを設定する */
        public virtual void SetData( Dictionary<string, string> input )
        {
            table.SetData( input );

            try
            {
                if( table.IsValid() == false )
                {
                    throw new Exception( "Invalid data." );
                }
                else
                {
                    return;
                }
            }
            catch( Exception ex )
            {
                ClearData();
                throw ex;
            }
        }

        /* 内部で保持しているデータをリセットする */
        public void ClearData()
        {
            table.data = new Dictionary<string, object>();
        }
    }
    
    /* リモート接続ビューの動作モード */
    public class ModeVP00RemtConn : ProgramMode
    {
        public ModeVP00RemtConn() : base()
        {
            table                   = new ViewVP00RemtConn();
            flagViewMode            = true;
            flagActionButton        = true;
            labelActionButton       = "Connect";
            cbsParam1.label         = "会社名";
            cbsParam1.queryString   = "SELECT cmpc, cnmv FROM mst_cmpn ORDER BY rubv ASC";
            labelsGridHeader        = new string[] { "会社名", "システム分類", "識別名", "", "", "IPアドレス", "ドメイン", "ユーザ", "パスワード", "" };
            converter = new Dictionary<string,string> 
            { 
                  { "会社名",      Const.CONTROL_LISTPARAM1 }
            };
            selecter = new Dictionary<string,string>
            {
                  { Const.CONTROL_LISTPARAM1, "cnmv" }
            };
        }
    }
    
    /* 機械台数(工程別)ビューの動作モード */
    public class ModeVP00McntSect : ProgramMode
    {
        public ModeVP00McntSect() : base()
        {
            table                   = new ViewVP00McntSect();
            flagViewMode            = true;
            cbsParam1.label         = "会社名";
            cbsParam1.queryString   = "SELECT * FROM ( SELECT '000000' AS cmpc, 'ALL' AS cnmv, '0' AS rubv FROM DUAL UNION ALL SELECT cmpc, cnmv, rubv  FROM mst_cmpn ) ORDER BY rubv ASC";
            labelsGridHeader        = new string[] { "会社名", "工程名", "台数", "", "", "", "", "", "", "" };
            converter = new Dictionary<string,string> 
            { 
                  { "会社名",      Const.CONTROL_LISTPARAM1 }
            };
            selecter = new Dictionary<string,string>
            {
                  { Const.CONTROL_LISTPARAM1, "cnmv" }
            };
        }
    }
    
    /* 機械台数(機種別)ビューの動作モード */
    public class ModeVP00McntMtyp : ProgramMode
    {
        public ModeVP00McntMtyp() : base()
        {
            table                   = new ViewVP00McntMtyp();
            flagViewMode            = true;
            cbsParam1.label         = "会社名";
            cbsParam1.queryString   = "SELECT * FROM ( SELECT '000000' AS cmpc, 'ALL' AS cnmv, '0' AS rubv FROM DUAL UNION ALL SELECT cmpc, cnmv, rubv  FROM mst_cmpn ) ORDER BY rubv ASC";
            cbsParam2.label         = "工程名";
            cbsParam2.queryString   = "SELECT sctc, snmv FROM mst_sect ORDER BY sctc ASC";  
            labelsGridHeader        = new string[] { "会社名", "工程名", "機種名", "台数", "", "", "", "", "", "" };
            converter = new Dictionary<string,string> 
            { 
                  { "会社名",  Const.CONTROL_LISTPARAM1 }
                , { "工程名",  Const.CONTROL_LISTPARAM2 }
            };
            selecter = new Dictionary<string,string>
            {
                  { Const.CONTROL_LISTPARAM1, "cnmv" }
                , { Const.CONTROL_LISTPARAM2, "snmv" }
            };
        }
    }

    /* 会社マスタの動作モード */
    public class ModeMstCmpn : ProgramMode
    {
        public ModeMstCmpn() : base()
        {
            table               = new TableMstCmpn();
            labelsTextBox       = new string[] { "会社コード", "会社名", "フリガナ" };
            labelsGridHeader    = new string[] { "会社コード", "会社名", "フリガナ", "", "", "", "", "", "", "" };
        }
    }
    
    /* システム分類マスタ(メッセージマスタ)の動作モード */
    public class ModeMstSysc : ProgramMode
    {
        public ModeMstSysc() : base()
        {
            table               = new TableMstSysc();            
            labelsTextBox       = new string[] { "システム分類", "", "" };
            labelsGridHeader    = new string[] { "", "システム分類", "", "", "", "", "", "", "", "" };            
        }

        public override OracleQuery GetSelectQuery()
        {
            OracleQuery     query   = new OracleQuery();
            
            query.mode = QueryMode.Select;
            query.str = "SELECT msgc, msgv FROM mst_mesg WHERE msgc LIKE 'MST01%'";

            return query;
        }
    }
    
    /* 機器役割マスタ(メッセージマスタ)の動作モード */
    public class ModeMstEqrl : ProgramMode
    {
        public ModeMstEqrl() : base()
        {
            table               = new TableMstEqrl();            
            labelsTextBox       = new string[] { "機器役割", "", "" };
            labelsGridHeader    = new string[] { "", "機器役割", "", "", "", "", "", "", "", "" };            
        }
        
        public override OracleQuery GetSelectQuery()
        {
            OracleQuery     query   = new OracleQuery();
            OleDbParameter  p       = new OleDbParameter();
            
            query.mode = QueryMode.Select;
            query.str = "SELECT msgc, msgv FROM mst_mesg WHERE msgc LIKE 'MST02%'";

            return query;
        }
    }    

    /* IT機器マスタの動作モード */
    public class ModeMstIteq : ProgramMode
    {
        public ModeMstIteq() : base()
        {
            table                   = new TableMstIteq();            
            cbsParam1.label         = "会社名";
            cbsParam1.queryString   = "SELECT cmpc, cnmv FROM mst_cmpn ORDER BY rubv ASC";      
            cbsParam2.label         = "システム分類";
            cbsParam2.queryString   = "SELECT msgc, msgv FROM mst_mesg WHERE msgc LIKE 'MST01%' ORDER BY msgc ASC";
            cbsParam3.label         = "機器役割";
            cbsParam3.queryString   = "SELECT msgc, msgv FROM mst_mesg WHERE msgc LIKE 'MST02%' ORDER BY msgc ASC";      
            cbsParam4.label         = "サブ役割";
            cbsParam4.queryString   = "SELECT msgc, msgv FROM mst_mesg WHERE msgc LIKE 'MST02%' ORDER BY msgc ASC";                  
            labelsTextBox           = new string[] { "識別名", "IPアドレス", "" };
            labelsGridHeader        = new string[] { "", "会社名", "システム分類", "識別名", "IPアドレス", "機器役割", "サブ役割", "", "", "" };
            converter = new Dictionary<string,string> 
            { 
                  { "会社名",       Const.CONTROL_LISTPARAM1 }
                , { "システム分類",  Const.CONTROL_LISTPARAM2 }
                , { "機器役割",       Const.CONTROL_LISTPARAM3 }
                , { "サブ役割",     Const.CONTROL_LISTPARAM4 }
                , { "IPアドレス",   Const.CONVERT_IPADDRESS_TODOT }
            };
            selecter = new Dictionary<string,string>
            {
                  { Const.CONTROL_LISTPARAM1, "cmpc" }
                , { Const.CONTROL_LISTPARAM2, "sysc" }
            };
        }
    }

    /* リモート接続マスタの動作モード */
    public class ModeMstRemtConn : ProgramMode
    {
        public ModeMstRemtConn() : base()
        {
            table                   = new TableMstRemtConn();
            cbsParam1.label         = "会社名";
            cbsParam1.queryString   = "SELECT cmpc, cnmv FROM mst_cmpn ORDER BY rubv ASC";      
            cbsParam2.label         = "システム分類";
            cbsParam2.queryString   = "SELECT msgc, msgv FROM mst_mesg WHERE msgc LIKE 'MST01%' ORDER BY msgc ASC";
            cbsParam3.label         = "設備名";
            cbsParam3.queryString   = "SELECT itec, cmpn.cnmv || ' ' || sysc.msgv || ' ' || iteq.alsv FROM mst_iteq iteq ";
            cbsParam3.queryString   += " INNER JOIN mst_cmpn cmpn ON iteq.cmpc = cmpn.cmpc ";
            cbsParam3.queryString   += " INNER JOIN mst_mesg sysc ON iteq.sysc = sysc.msgc ";
            cbsParam3.queryString   += " WHERE iteq.cmpc = ? AND iteq.sysc = ? ";
            cbsParam3.queryString   += " ORDER BY cmpn.rubv ASC, iteq.sysc ASC, iteq.alsv ASC";
            cbsParam3.queryParam.Add( Const.CONTROL_LISTPARAM1, "cmpc" );
            cbsParam3.queryParam.Add( Const.CONTROL_LISTPARAM2, "sysc" );
            cbsParam3.combined      = true;
            labelsTextBox           = new string[] { "ドメイン", "ユーザ名", "パスワード" };
            labelsGridHeader        = new string[] { "", "設備名", "ドメイン", "ユーザ名", "パスワード", "", "", "", "", "", "" };
            converter = new Dictionary<string,string> 
            { 
                  { "設備名",       Const.CONTROL_LISTPARAM3 }
            };
            selecter = new Dictionary<string,string>
            {
                /*
                  { Const.CONTROL_LISTPARAM1, "cmpc" }
                , { Const.CONTROL_LISTPARAM2, "sysc" }
                , */{ Const.CONTROL_LISTPARAM3, "itec" }
            };
        }
        
        public override OracleQuery GetSelectQuery()
        {
            OracleQuery     query   = new OracleQuery();
            
            query.mode = QueryMode.Select;
            query.str = "SELECT conn.rmtc, conn.itec, conn.dmnv, conn.usrv, conn.pwdv FROM mst_remt_conn conn";
            query.str += " INNER JOIN mst_iteq iteq ON conn.itec = iteq.itec ";
            query.str += " WHERE iteq.itec = ?";// iteq.cmpc = ? AND iteq.sysc = ? AND  ";

            return query;
        }
    }    

    /* 工程マスタの動作モード */
    public class ModeMstSect : ProgramMode
    {
        public ModeMstSect() : base()
        {
            table               = new TableMstSect();
            labelsTextBox       = new string[] { "工程コード", "工程名", "" };
            labelsGridHeader    = new string[] { "工程コード", "工程名", "", "", "", "", "", "", "", "" };
        }
    }
    
    /* 機械台数マスタの動作モード */
    public class ModeMstMcnt : ProgramMode
    {
        public ModeMstMcnt() : base()
        {
            table                   = new TableMstMcnt();            
            cbsParam1.label         = "会社名";
            cbsParam1.queryString   = "SELECT cmpc, cnmv FROM mst_cmpn ORDER BY rubv ASC";      
            cbsParam2.label         = "工程名";
            cbsParam2.queryString   = "SELECT sctc, snmv FROM mst_sect ORDER BY sctc ASC";
            labelsTextBox           = new string[] { "機種名", "台数", "" };
            labelsGridHeader        = new string[] { "", "会社名", "工程名", "機種名", "台数", "", "", "", "", "" };
            converter = new Dictionary<string,string> 
            { 
                  { "会社名",  Const.CONTROL_LISTPARAM1 }
                , { "工程名",  Const.CONTROL_LISTPARAM2 }
            };
            selecter = new Dictionary<string,string>
            {
                  { Const.CONTROL_LISTPARAM1, "cmpc" }
                , { Const.CONTROL_LISTPARAM2, "sctc" }
            };
        }
    }

}