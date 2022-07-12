using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Maintenance
{
    /* コンボボックス設定用クラス */
    public class ComboBoxSetting
    {
        private Dictionary<string, string> items;
        private DataTable   dt;
        public  bool        visibleFlag;
        public  string      label;
        public  string      queryString;
        public  Dictionary<string, string> queryParam;
        public  bool        combined;
        private bool        initFlag;

        /* コンストラクタ */
        public ComboBoxSetting()
        {
            Init();
        }

        /* 設定を初期化する */
        public void Init()
        {
            items       = new Dictionary<string, string>();
            dt          = new DataTable();
            queryString = string.Empty;
            queryParam  = new Dictionary<string,string>();
            label       = "";
            combined    = false;

            items.Add( "", "" );

            initFlag    = true;
        }

        /* 表示項目を追加する */
        public void AddItem( string key, string value )
        {
            if( initFlag == true )
            {
                items.Clear();
                initFlag = false;
            }
            else
            {
            }

            items.Add( key, value );
        }

        /* コンボボックスの結合をやり直す準備 */
        public void PrepareCombine()
        {
            items.Clear();
            items.Add( "", "" );
            dt          = new DataTable();
            initFlag    = true;
        }

        /* キーと値のペアのテーブルをもとにデータを追加する */
        public void AddDataTable( DataTable kvpTable )
        {
            foreach( DataRow dr in kvpTable.Rows )
            {
                AddItem( dr[ 0 ].ToString(), dr[ 1 ].ToString() );
            }
        }

        /* 指定したコンボボックスに設定を適用する */
        public void Apply( ref ComboBox cb, ref Label lb )
        {
            //dt.Clear();
            dt.Columns.Add( "Key",      typeof( string ) );
            dt.Columns.Add( "Value",    typeof( string ) );

            foreach( KeyValuePair<string, string> kvp in items )
            {
                DataRow dr = dt.NewRow();

                dr[ "Key" ]     = kvp.Key;
                dr[ "Value" ]   = kvp.Value;

                dt.Rows.Add( dr );
            }

            cb.DataSource       = dt;
            cb.DisplayMember    = "Value";
            cb.ValueMember      = "Key";

            if( string.IsNullOrEmpty( label ) == true )
            {
                visibleFlag = false;
            }
            else
            {
                visibleFlag = true;
            }
            
            lb.Text = label;

            if( cb.Items.Count > 0 )
            {
                cb.SelectedIndex = 0;
            }
            else
            {
                cb.SelectedIndex = -1;
            }

            cb.Visible = visibleFlag;
            lb.Visible = visibleFlag;

            cb.Refresh();
            lb.Refresh();

            return;
        }      
    }
}
