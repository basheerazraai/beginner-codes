using System;
using System.Collections;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Library
{
    using Excel = Microsoft.Office.Interop.Excel;

    /* Microsoft Officeと連携するクラス */
    class OfficeTools
    {
        protected const int EXCEL_MAX_COLUMNS = 255;
        protected const int EXCEL_MAX_ROWS = 65534;

        public OfficeTools()
        {
        }
    }

    /* エクセル出力のクラス */
    class ExcelOutput : OfficeTools
    {
        public ExcelOutput()
        {
        }

        /* DataGridViewからヘッダを取得する */
        private int GetHeaderCollection( ref ArrayList list, ref DataGridView dgv )
        {
            try
            {
                for( int i = 0; i < dgv.ColumnCount; i++ )
                {
                    if( dgv.Columns[ i ].Visible == true )
                    {
                        list.Add( ( object )( dgv.Columns[ i ].HeaderText.ToString() ) );
                    }
                }
            }
            catch( Exception )
            {
                return -1;
            }

            return 0;
        }

        /* 新規のExcelブックにシートを1枚作ってデータを出力する */
        public int OutputSingleSheet( DataGridView dgv, string filePath )
        {
            {
                System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;

                int i = 0;
                int j = 0;
                int totalColumns = dgv.Columns.Count;
                int totalRows = dgv.Rows.Count;

                /* 列数制限 */
                if( totalColumns > EXCEL_MAX_COLUMNS )
                {
                    totalColumns = EXCEL_MAX_COLUMNS;
                }
                else
                {
                }

                /* 行数制限 */
                if( totalRows > EXCEL_MAX_ROWS )
                {
                    totalRows = EXCEL_MAX_ROWS;
                }
                else
                {
                }

                Excel.Application   xlApp       = new Excel.Application();
                Excel.Workbooks     xlBooks     = xlApp.Workbooks;
                Excel.Workbook      xlBook      = xlBooks.Add( System.Reflection.Missing.Value );
                Excel.Sheets        xlSheets    = xlBook.Worksheets;
                Excel.Worksheet     xlSheet     = ( Excel.Worksheet )xlSheets.get_Item( 1 );
                Excel.Range         xlRange     = null;

                try
                {
                    xlApp.DisplayAlerts = false;
                    xlApp.Visible = false;

                    ArrayList alHeader = new ArrayList();
                    string[] arrayHeader;

                    Object[,] sheetData = new Object[ totalRows, totalColumns ];

                    /* もとのDataGridViewからヘッダ情報を取得 */
                    if( GetHeaderCollection( ref alHeader, ref dgv ) != 0 )
                    {
                        MessageBox.Show( "Header data collapsed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );

                        return -1;
                    }
                    else
                    {
                        arrayHeader = ( string[] )( alHeader.ToArray( typeof( string ) ) );
                    }

                    /* シートの最初の列にヘッダ情報を転写 */
                    for( i = 0; i < arrayHeader.Length; i++ )
                    {
                        xlSheet.Cells[ 1, i + 1 ] = arrayHeader[ i ];
                    }

                    /* データ部のセルの値を転写 */
                    for( i = 0; i < totalColumns; i++ )
                    {
                        for( j = 0; j < totalRows; j++ )
                        {
                            try
                            {
                                if( dgv.Columns[ i ].Visible == true )
                                {
                                    sheetData[ j, i ] = dgv[ i, j ].FormattedValue;
                                }
                            }
                            catch( Exception )
                            {
                                return -1;
                            }
                        }
                    }

                    xlRange = xlSheet.get_Range( ( Object )( xlSheet.Cells[ 2, 1 ] ), ( Object )( xlSheet.Cells[ 2, 1 ] ) );
                    xlRange = xlRange.get_Resize( totalRows, totalColumns );
                    xlRange.Value = sheetData;
                    xlSheet.Cells.EntireColumn.AutoFit();

                    xlRange = xlSheet.get_Range( ( Object )( xlSheet.Cells[ 2, 1 ] ), ( Object )( xlSheet.Cells[ 2, 1 ] ) );
                    xlRange = xlRange.get_Resize( 1, 1 );
                    xlRange.Activate();
                    xlRange.Application.ActiveWindow.FreezePanes = true;

                    try
                    {
                        xlBook.SaveAs( filePath, Excel.XlFileFormat.xlWorkbookNormal );
                    }
                    catch( Exception )
                    {
                        return -1;
                    }

                    return 0;
                }
                finally
                {
                    /* Excelを閉じる(呼び出し元の画面を閉じるまでプロセスが落ちない？) */
                    xlBook.Close();
                    xlApp.Quit();

                    Marshal.ReleaseComObject( xlRange );
                    xlRange = null;

                    Marshal.ReleaseComObject( xlSheet );
                    xlSheet = null;

                    Marshal.ReleaseComObject( xlSheets );
                    xlSheets = null;

                    Marshal.ReleaseComObject( xlBook );
                    xlBook = null;

                    Marshal.ReleaseComObject( xlBooks );
                    xlBooks = null;

                    Marshal.ReleaseComObject( xlApp );
                    xlApp = null;
                }
            }
        }
    }
}
