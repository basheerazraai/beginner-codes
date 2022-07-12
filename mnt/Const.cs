using System.Data;

namespace Maintenance
{
    public class Const
    {
        /* このあたりは基本的に変更は不要 */
        public const string INI_SECTION_DATABASE    = "DATABASE";
        public const string INI_KEY_SID             = "SID";
        public const string INI_KEY_USER            = "USER";
        public const string INI_KEY_PASSWORD        = "PASSWORD";
        public const string INI_SECTION_LOCAL       = "LOCALFILE";
        public const string INI_FILEPATH            = "FILENAME";  
        public const string INI_FILENAME            = "Maintenance.ini";
        public const string CONTROL_TEXTBOX         = "textParam";
        public const string LABEL_TEXTBOX           = "labelTextParam";
        public const string CONTROL_CHECKBOX        = "checkParam";
        public const string CONTROL_LISTPARAM1      = "listParam1";
        public const string CONTROL_LISTPARAM2      = "listParam2";
        public const string CONTROL_LISTPARAM3      = "listParam3";
        public const string CONTROL_LISTPARAM4      = "listParam4";
        public const string CONTROL_TEXTPARAM1      = "textParam1";
        public const string CONTROL_TEXTPARAM2      = "textParam2";
        public const string CONTROL_TEXTPARAM3      = "textParam3";
        public const string CONTROL_TEXTPARAMH      = "textParamHidden";
        public const string CONTROL_CHECKPARAM1     = "checkParam1";
        public const string CONTROL_CHECKPARAM2     = "checkParam2";
        public const string CONTROL_CHECKPARAM3     = "checkParam3";

        /* テーブル定義を追加するときに編集する */
        public const string TABLE_MST_CMPN          = "mst_cmpn";
        public const string TABLE_MST_ITEQ          = "mst_iteq";
        public const string TABLE_MST_SYSC          = "mst_sysc";
        public const string TABLE_MST_EQRL          = "mst_eqrl";
        public const string TABLE_MST_MESG          = "mst_mesg";
        public const string TABLE_MST_REMT_CONN     = "mst_remt_conn";
        public const string TABLE_MST_SECT          = "mst_sect";
        public const string TABLE_MST_MCNT          = "mst_mcnt";
        public const string VIEW_V_P00_REMT_CONN    = "v_p00_remt_conn";
        public const string VIEW_V_P00_MCNT_SECT    = "v_p00_mcnt_sect";
        public const string VIEW_V_P00_MCNT_MTYP    = "v_p00_mcnt_mtyp";

        /* 変換動作を新たに用意するときに編集する */
        public const string CONVERT_IPADDRESS_TODOT     = "ipaddress_todot";
        public const string CONVERT_IPADDRESS_TOZERO    = "ipaddress_tozero";
        public const string CONVERT_LISTPARAM_JOIN      = "listparam_join";
    }

    /* クエリ種類の列挙型 */
    public enum QueryMode
    {
          None
        , Select
        , Insert
        , Update
        , Delete
    }

    public enum OracleDataType
    {
          None
        , CHAR
        , VARCHAR2
        , NUMBER
        , DATE
    }
}
