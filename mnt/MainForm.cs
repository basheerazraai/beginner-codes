using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Maintenance
{
    public partial class MainForm : Form
    {
        public ArrayList arrayTextBox = new ArrayList();
        public ArrayList arrayLabelTextBox = new ArrayList();
        public ArrayList arrayCheckBox = new ArrayList();
        private OracleConnection oracle = null;
        private IniStream istream = null;
        private ProgramMode mode = null;
        private bool eventStop = false;
        private int modeflag;

        public Dictionary<string, string> input = new Dictionary<string, string>();

        /* コンストラクタ */
        public MainForm()
        {
            try
            {
                InitializeComponent();
                //InitialButtonStatus();
                LoadIniFIle();
                //int modeflag = 1;
                //radioButton1.Select();
                if (radioButton1.Checked == true)
                {
                    /*comboBox1.Hide();
                    label1.Hide();
                    Refresh();
                    /*DataTable dataTable = (DataTable)dataGridViewMain.DataSource;
                    if (dataTable != null)
                    {
                        dataTable.Clear();
                    }

                    dataGridViewMain.Refresh();
                    dataGridViewMain.DataSource = null;
                    listParam1.Refresh();
                    listDataDomain.Refresh();
                    try
                    {
                        CreateConnection();
                        Console.WriteLine("接続出来ました!!!");
                        //dataGridViewMain.Enabled = true;
                        mode = new ModeVP00RemtConn();   //デフォルトの動作モード
                        mode.cbsDataDomain.Apply(ref this.listDataDomain, ref this.labelListDataDomain);
                        RefreshForm();
                        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                        Console.WriteLine("こっちだ！！！");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    return;*/

                }
                else if (radioButton2.Checked == true)
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return;
        }
        

        /* Iniファイルを読み込む */
        private void LoadIniFIle()
        {
            string executableDir;

            try
            {
                executableDir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);    //実行ファイルと同じフォルダ
                istream = new IniStream(executableDir, Const.INI_FILENAME);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* 接続情報を作成する */
        private void CreateConnection()
        {
            string sid;
            string user;
            string password;

            try
            {
                sid = istream[Const.INI_SECTION_DATABASE, Const.INI_KEY_SID];
                user = istream[Const.INI_SECTION_DATABASE, Const.INI_KEY_USER];
                password = istream[Const.INI_SECTION_DATABASE, Const.INI_KEY_PASSWORD];
                oracle = new OracleConnection(sid, user, password);
                oracle.Connect();

                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ArrayList GetParamInControl(Dictionary<string, string> list)
        {
            OracleDataType type = OracleDataType.None;
            BindingFlags bf = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            FieldInfo fi = null;
            object obj = null;
            OleDbParameter p = null;
            ArrayList param = new ArrayList();

            if (list.Count <= 0)
            {
                return param;
            }
            else
            {
            }

            foreach (KeyValuePair<string, string> kvp in list)
            {
                p = new OleDbParameter();
                fi = (this.GetType()).GetField(kvp.Key, bf);

                if (fi == null)
                {
                    continue;
                }
                else
                {
                    obj = fi.GetValue(this);
                }

                type = mode.GetColumnType(kvp.Value);

                switch (type)
                {
                    case OracleDataType.CHAR:
                        p = new OleDbParameter("@" + kvp.Value, OleDbType.Char);
                        break;
                    case OracleDataType.VARCHAR2:
                        p = new OleDbParameter("@" + kvp.Value, OleDbType.VarChar);
                        break;
                    case OracleDataType.DATE:
                        break;
                    case OracleDataType.NUMBER:
                        p = new OleDbParameter("@" + kvp.Value, OleDbType.Numeric);
                        break;
                    default:
                        p = new OleDbParameter("", string.Empty);
                        break;
                }

                if (obj.GetType() == typeof(ComboBox) && mode.flagViewMode == false)
                {
                    p.Value = ((ComboBox)obj).SelectedValue.ToString(); //内部値をパラメータにする
                }
                else if (obj.GetType() == typeof(ComboBox) && mode.flagViewMode == true)
                {
                    DataRowView drv = ((ComboBox)obj).SelectedItem as DataRowView;
                    p.Value = drv.Row["Value"].ToString();    //表示値をパラメータにする
                }

                param.Add(p);
            }

            return param;
        }

        private void RefreshComboBox(int number)
        {
            OracleQuery query = new OracleQuery();

            switch (number)
            {
                case 1:
                    query = mode.GetListQuery(1);

                    if (query != null)
                    {
                        query.param = GetParamInControl(mode.cbsParam1.queryParam);
                        oracle.Execute(ref query);
                        mode.cbsParam1.AddDataTable(query.result.Tables[0]);
                    }
                    else
                    {
                    }

                    mode.cbsParam1.Apply(ref this.listParam1, ref this.labelListParam1);
                    break;
                case 2:
                    query = mode.GetListQuery(2);

                    if (query != null)
                    {
                        query.param = GetParamInControl(mode.cbsParam2.queryParam);
                        oracle.Execute(ref query);
                        mode.cbsParam2.AddDataTable(query.result.Tables[0]);
                    }
                    else
                    {
                    }

                    mode.cbsParam2.Apply(ref this.listParam2, ref this.labelListParam2);
                    break;
                case 3:
                    query = mode.GetListQuery(3);

                    if (query != null)
                    {
                        query.param = GetParamInControl(mode.cbsParam3.queryParam);
                        oracle.Execute(ref query);
                        mode.cbsParam3.AddDataTable(query.result.Tables[0]);
                    }
                    else
                    {
                    }

                    mode.cbsParam3.Apply(ref this.listParam3, ref this.labelListParam3);
                    break;
                case 4:
                    query = mode.GetListQuery(4);

                    if (query != null)
                    {
                        query.param = GetParamInControl(mode.cbsParam4.queryParam);
                        oracle.Execute(ref query);
                        mode.cbsParam4.AddDataTable(query.result.Tables[0]);
                    }
                    else
                    {
                    }

                    mode.cbsParam4.Apply(ref this.listParam4, ref this.labelListParam4);
                    break;
                default:
                    break;
            }
        }

        /* コンボボックスを再描画する */
        private void RefreshComboBox()
        {
            try
            {
                RefreshComboBox(1);
                RefreshComboBox(2);
                RefreshComboBox(3);
                RefreshComboBox(4);

                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* 他のコンボボックスを結合しているコンボボックスのみ再描画する */
        private void RefreshCombinedBox(string ownName = "")
        {
            eventStop = true;

            if (mode.cbsParam1.combined == true && ownName != Const.CONTROL_LISTPARAM1)
            {
                mode.cbsParam1.PrepareCombine();
                RefreshComboBox(1);
            }
            else
            {
            }

            if (mode.cbsParam2.combined == true && ownName != Const.CONTROL_LISTPARAM2)
            {
                mode.cbsParam2.PrepareCombine();
                RefreshComboBox(2);
            }
            else
            {
            }

            if (mode.cbsParam3.combined == true && ownName != Const.CONTROL_LISTPARAM3)
            {
                mode.cbsParam3.PrepareCombine();
                RefreshComboBox(3);
            }
            else
            {
            }

            if (mode.cbsParam4.combined == true && ownName != Const.CONTROL_LISTPARAM4)
            {
                mode.cbsParam4.PrepareCombine();
                RefreshComboBox(4);
            }
            else
            {
            }

            eventStop = false;
        }

        /* テキストボックスを再描画する */
        private void RefreshTextBox()
        {
            object obj = null;
            BindingFlags bf = BindingFlags.Default;
            FieldInfo fi = null;

            try
            {
                arrayTextBox.Clear();
                arrayLabelTextBox.Clear();

                for (int i = 0; i < mode.labelsTextBox.Length; i++)
                {
                    bf = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
                    fi = (this.GetType()).GetField(Const.CONTROL_TEXTBOX + (i + 1).ToString(), bf);

                    if (fi != null)
                    {
                        obj = fi.GetValue(this);
                        arrayTextBox.Add(obj);
                    }
                    else
                    {
                        obj = null;
                    }

                    fi = (this.GetType()).GetField(Const.LABEL_TEXTBOX + (i + 1).ToString(), bf);

                    if (fi != null)
                    {
                        obj = fi.GetValue(this);
                        arrayLabelTextBox.Add(obj);
                    }
                    else
                    {
                        obj = null;
                    }
                }

                if (arrayTextBox.Count != arrayLabelTextBox.Count)
                {
                    throw new Exception("Form is broken.(ComboBox)");
                }
                else if (arrayTextBox.Count != mode.labelsTextBox.Length)
                {
                    throw new Exception("Invalid setting.(ComboBox)");
                }
                else
                {
                    for (int i = 0; i < arrayTextBox.Count; i++)
                    {
                        if (string.IsNullOrEmpty(mode.labelsTextBox[i]) == false)
                        {
                            ((Label)arrayLabelTextBox[i]).Text = mode.labelsTextBox[i];
                            ((Label)arrayLabelTextBox[i]).Visible = true;
                            ((TextBox)arrayTextBox[i]).Text = string.Empty;
                            ((TextBox)arrayTextBox[i]).Visible = true;
                        }
                        else
                        {
                            ((Label)arrayLabelTextBox[i]).Visible = false;
                            ((TextBox)arrayTextBox[i]).Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /* チェックボックスを再描画する */
        private void RefreshCheckBox()
        {
            object obj = null;
            BindingFlags bf = BindingFlags.Default;
            FieldInfo fi = null;

            try
            {
                arrayCheckBox.Clear();

                for (int i = 0; i < mode.labelsCheckBox.Length; i++)
                {
                    bf = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
                    fi = (this.GetType()).GetField(Const.CONTROL_CHECKBOX + (i + 1).ToString(), bf);

                    if (fi != null)
                    {
                        obj = fi.GetValue(this);
                        arrayCheckBox.Add(obj);
                    }
                    else
                    {
                        obj = null;
                    }
                }

                if (arrayCheckBox.Count != mode.labelsCheckBox.Length)
                {
                    throw new Exception("Invalid setting.(CheckBox)");
                }
                else
                {
                    for (int i = 0; i < arrayCheckBox.Count; i++)
                    {
                        if (string.IsNullOrEmpty(mode.labelsCheckBox[i]) == false)
                        {
                            ((CheckBox)arrayCheckBox[i]).Text = mode.labelsCheckBox[i];
                            ((CheckBox)arrayCheckBox[i]).Visible = true;
                            ((CheckBox)arrayCheckBox[i]).Checked = false;
                        }
                        else
                        {
                            ((CheckBox)arrayCheckBox[i]).Visible = false;
                            ((CheckBox)arrayCheckBox[i]).Checked = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* ビューを検索する場合の制御 */
        private void ChangeButtonStatus()
        {
            if (mode.flagViewMode == true)
            {
                this.btnInsert.Enabled = false;
                this.btnUpdate.Enabled = false;
                this.btnDelete.Enabled = false;
            }
            else
            {
                this.btnInsert.Enabled = true;
                this.btnUpdate.Enabled = true;
                this.btnDelete.Enabled = true;
            }

            if (mode.flagActionButton == true)
            {
                this.btnAction.Enabled = true;
            }
            else
            {
                this.btnAction.Enabled = false;
            }

            this.btnAction.Text = mode.labelActionButton;
            //this.btnUpdate.Enabled  = false;
            this.btnExcel.Enabled = false;

            return;
        }

        private void InitialButtonStatus()
        {
            this.btnInsert.Enabled = false;
            this.btnUpdate.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnAction.Enabled = false;
            this.btnRefresh.Enabled = false;
            this.btnExcel.Enabled = false;
        }

        /* フォームを再描画する */
        private void RefreshForm()
        {
            try
            {
                eventStop = true;

                RefreshComboBox();
                RefreshTextBox();
                RefreshCheckBox();
                RefreshDataGrid();
                ChangeButtonStatus();

                eventStop = false;

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /* フォームの入力値を格納する */
        private void GetInputValue()
        {
            input.Clear();
            input.Add(Const.CONTROL_LISTPARAM1, this.listParam1.SelectedValue.ToString());
            input.Add(Const.CONTROL_LISTPARAM2, this.listParam2.SelectedValue.ToString());
            input.Add(Const.CONTROL_LISTPARAM3, this.listParam3.SelectedValue.ToString());
            input.Add(Const.CONTROL_LISTPARAM4, this.listParam4.SelectedValue.ToString());
            input.Add(Const.CONTROL_TEXTPARAM1, this.textParam1.Text);
            input.Add(Const.CONTROL_TEXTPARAM2, this.textParam2.Text);
            input.Add(Const.CONTROL_TEXTPARAM3, this.textParam3.Text);
            input.Add(Const.CONTROL_CHECKPARAM1, Common.GetCheckBoxStatus(this.checkParam1.Checked));
            input.Add(Const.CONTROL_CHECKPARAM2, Common.GetCheckBoxStatus(this.checkParam2.Checked));
            input.Add(Const.CONTROL_CHECKPARAM3, Common.GetCheckBoxStatus(this.checkParam3.Checked));
        }

        /* データグリッド内の値を変換する */
        private void ConvertGridValue(int columnNumber, string convertID, FieldInfo fi = null)
        {
            object obj = null;
            Object ds = null;
            DataTable dt = null;

            switch (convertID)
            {
                case Const.CONVERT_IPADDRESS_TODOT:
                    for (int i = 0; i < this.dataGridViewMain.Rows.Count - 1; i++)
                    {
                        this.dataGridViewMain.Rows[i].Cells[columnNumber].Value
                                = Common.ConvertIPv4ToDot(this.dataGridViewMain.Rows[i].Cells[columnNumber].Value.ToString());
                    }
                    break;
                default:
                    if (fi == null)
                    {
                        return;
                    }
                    else
                    {
                        obj = fi.GetValue(this);
                    }

                    if (obj.GetType() == typeof(ComboBox))
                    {
                        ds = ((ComboBox)obj).DataSource;
                        dt = (DataTable)ds;

                        for (int i = 0; i < this.dataGridViewMain.Rows.Count - 1; i++)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (this.dataGridViewMain.Rows[i].Cells[columnNumber].Value.ToString() == dr["Key"].ToString())
                                {
                                    this.dataGridViewMain.Rows[i].Cells[columnNumber].Value = dr["Value"];
                                }
                            }
                        }
                    }
                    else
                    {
                    }

                    break;
            }
        }

        /* データグリッドのデータの書き換え */
        private void ModifyGridData()
        {
            string convertID;
            BindingFlags bf = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            FieldInfo fi = null;

            for (int i = 0; i < this.dataGridViewMain.Columns.Count; i++)
            {
                convertID = string.Empty;

                if (mode.converter.TryGetValue(this.dataGridViewMain.Columns[i].HeaderText, out convertID) == false)
                {
                    continue;
                }
                else if (convertID == string.Empty)
                {
                    continue;
                }
                else
                {
                    fi = (this.GetType()).GetField(convertID, bf);
                    ConvertGridValue(i, convertID, fi);
                }
            }
        }

        /* データグリッドを再描画する */
        private void RefreshDataGrid()
        {
            OracleQuery query = null;
            DataTable dt = new DataTable();

            try
        {
            query = mode.GetSelectQuery();

            query.param = GetParamInControl(mode.selecter);

            if (string.IsNullOrEmpty(query.str) == false)
            {
                oracle.Execute(ref query);
            }
            else
            {
                return;
            }

            this.dataGridViewMain.AllowUserToAddRows = true;

            dt = query.result.Tables[0];
            this.dataGridViewMain.Rows.Clear();

            if (dt.Rows.Count > 0)
            {
                this.dataGridViewMain.Rows.Add(dt.Rows.Count);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        this.dataGridViewMain.Rows[i].Cells[j].Value = dt.Rows[i][j].ToString();
                    }
                }
            }
            else
            {
            }

            for (int i = 0; i < this.dataGridViewMain.Columns.Count; i++)
            {
                if (i < mode.labelsGridHeader.Length)
                {
                    this.dataGridViewMain.Columns[i].HeaderText = mode.labelsGridHeader[i];
                }
                else
                {
                    this.dataGridViewMain.Columns[i].HeaderText = "";
                }

                if (this.dataGridViewMain.Columns[i].HeaderText != "")
                {
                    this.dataGridViewMain.Columns[i].Visible = true;
                }
                else
                {
                    this.dataGridViewMain.Columns[i].Visible = false;
                }
            }

            ModifyGridData();

            this.dataGridViewMain.AllowUserToAddRows = false;
            this.dataGridViewMain.ClearSelection();
           }
            catch (Exception)
            {
                throw new Exception("Data selection failed.");
            }
        }

        /* データ登録時のキーの値をフォームに手入力せず自動で作り出す場合はこの関数を経由する */
        private void SetParticularKey()
        {
            OracleQuery query = new OracleQuery();

            query.mode = QueryMode.Select;
            query.str = mode.GetKeyMakeQuery();

            try
            {
                if (string.IsNullOrEmpty(query.str) == false)
                {
                    oracle.Execute(ref query);
                    mode.SetKey(query.result.Tables[0].Rows[0][0]);
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {
                throw new Exception("Key-make failed.");
            }
        }

        /* DBからデータを削除するためのパラメータをデータグリッドから取得する */
        private string[] GetRowParams(int index)
        {
            int[] numbers;
            ArrayList values = new ArrayList();

            try
            {
                numbers = mode.GetKeyColumns();

                //ToDO キー値がグリッド上で表示用の値に変換されているとエラー(実質、単一キー限定)
                for (int i = 0; i < numbers.Length; i++)
                {
                    values.Add(this.dataGridViewMain.Rows[index].Cells[numbers[i]].Value.ToString());
                }

                return (string[])values.ToArray(typeof(string));
            }
            catch
            {
                throw new Exception("Key-value does not exist.");
            }
        }

        /* 選択された行の値をテキストボックスに表示させる */
        private void FillBySelectedRowValue(int index)
        {
            object obj;
            BindingFlags bf = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            FieldInfo fi = null;

            for (int i = 0; i < this.dataGridViewMain.Columns.Count; i++)
            {
                obj = null;
                fi = null;

                for (int j = 0; j < mode.labelsTextBox.Length; j++)
                {
                    if (mode.labelsTextBox[j] == "")
                    {
                        continue;
                    }
                    else if (this.dataGridViewMain.Columns[i].HeaderText == mode.labelsTextBox[j])
                    {
                        fi = (this.GetType()).GetField(Const.CONTROL_TEXTBOX + (j + 1).ToString(), bf);
                    }

                    if (fi != null)
                    {
                        obj = fi.GetValue(this);
                    }
                    else
                    {
                        obj = null;
                    }
                }

                if (obj != null && obj.GetType() == typeof(TextBox))
                {
                    ((TextBox)obj).Text = this.dataGridViewMain[i, index].Value.ToString();
                }
                else
                {
                }
            }
        }

        /* InsertボタンをクリックしたときはデータをDBに挿入する */
        private void btnInsert_Click(object sender, EventArgs e)
        {
            OracleQuery query;

            GetInputValue();

            try
            {
                mode.SetData(input);
                SetParticularKey();
                query = mode.GetInsertQuery();

                if (query != null)
                {
                    oracle.Execute(ref query);
                }
                else
                {
                }

                MessageBox.Show("Success.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                mode.ClearData();
                RefreshDataGrid();
            }
        }

        /* UpdateボタンをクリックしたときはDB上のデータを更新する */
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            OracleQuery query;
            int index = -1;
            string[] keyValues;

            GetInputValue();

            try
            {
                foreach (DataGridViewRow dr in this.dataGridViewMain.SelectedRows)
                {
                    index = dr.Index;
                }

                mode.SetData(input);

                if (this.dataGridViewMain.RowCount == 0 || index < 0)
                {
                    return;
                }
                else
                {
                    keyValues = GetRowParams(index);
                    query = mode.GetUpdateQuery(keyValues);
                }

                if (query != null)
                {
                    oracle.Execute(ref query);
                }
                else
                {
                }

                MessageBox.Show("Success.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                mode.ClearData();
                RefreshDataGrid();
            }
        }

        /* DeleteボタンをクリックしたときはDB上のデータを削除する */
        private void btnDelete_Click(object sender, EventArgs e)
        {
            OracleQuery query;
            int index = -1;
            string[] keyValues;

            try
            {
                foreach (DataGridViewRow dr in this.dataGridViewMain.SelectedRows)
                {
                    index = dr.Index;
                }

                if (this.dataGridViewMain.RowCount == 0 || index < 0)
                {
                    return;
                }
                else
                {
                    keyValues = GetRowParams(index);
                    query = mode.GetDeleteQuery(keyValues);

                    if (query != null)
                    {
                        oracle.Execute(ref query);
                    }
                    else
                    {
                    }

                    MessageBox.Show("Success.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                RefreshDataGrid();
            }

            return;
        }

        /* Refreshボタンをクリックしたときは画面上の表を再描画する */
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /* Actionボタンをクリックしたときはビューごとの特殊動作を行う */
        private void btnAction_Click(object sender, EventArgs e)
        {
            Type type;
            if (modeflag == 1)
            {
                try
                {
                    type = this.mode.GetType();

                    if (type == typeof(ModeVP00RemtConn))
                    {
                        int index = -1;
                        int tmp = -1;
                        string ipav = string.Empty;
                        string dmnv = string.Empty;
                        string usrv = string.Empty;
                        string pwdv = string.Empty;
                        Process msdos = new Process();

                        foreach (DataGridViewRow dr in this.dataGridViewMain.SelectedRows)
                        {
                            index = dr.Index;
                        }

                        if (this.dataGridViewMain.RowCount == 0 || index < 0)
                        {
                            return;
                        }
                        else
                        {
                            tmp = mode.GetColumnNumber("ipav");
                            ipav = this.dataGridViewMain.Rows[index].Cells[tmp].Value.ToString();
                            tmp = mode.GetColumnNumber("dmnv");
                            dmnv = this.dataGridViewMain.Rows[index].Cells[tmp].Value.ToString();
                            tmp = mode.GetColumnNumber("usrv");
                            usrv = this.dataGridViewMain.Rows[index].Cells[tmp].Value.ToString();
                            tmp = mode.GetColumnNumber("pwdv");
                            pwdv = this.dataGridViewMain.Rows[index].Cells[tmp].Value.ToString();

                            if (dmnv.Length < 1)
                            {
                                dmnv = ipav;
                            }
                            else
                            {
                            }

                            msdos.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
                            msdos.StartInfo.CreateNoWindow = true;
                            msdos.StartInfo.Arguments = @"/c ";
                            msdos.StartInfo.Arguments += @" Cmdkey /generic:TERMSRV/" + ipav;
                            msdos.StartInfo.Arguments += @" /user:" + dmnv + @"\" + usrv;
                            msdos.StartInfo.Arguments += @" /pass:" + pwdv;
                            msdos.StartInfo.Arguments += @" && start mstsc /v:" + ipav;
                            msdos.StartInfo.Arguments += @" && timeout 10";
                            msdos.StartInfo.Arguments += @" && Cmdkey /delete:TERMSRV/" + ipav;
                            msdos.Start();
                            msdos.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                String ipad = dataGridViewMain.CurrentRow.Cells[3].Value.ToString();
                String dom = dataGridViewMain.CurrentRow.Cells[4].Value.ToString();
                String srv = dataGridViewMain.CurrentRow.Cells[5].Value.ToString();
                String pass = dataGridViewMain.CurrentRow.Cells[6].Value.ToString();

                Process msdos = new Process();
                msdos.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
                msdos.StartInfo.CreateNoWindow = true;
                msdos.StartInfo.Arguments = @"/c ";
                msdos.StartInfo.Arguments += @" Cmdkey /generic:TERMSRV/" + ipad;
                msdos.StartInfo.Arguments += @" /user:" + dom + @"\" + srv;
                msdos.StartInfo.Arguments += @" /pass:" + pass;
                msdos.StartInfo.Arguments += @" && start mstsc /v:" + ipad;
                msdos.StartInfo.Arguments += @" && timeout 10";
                msdos.StartInfo.Arguments += @" && Cmdkey /delete:TERMSRV/" + ipad;
                msdos.Start();
                msdos.Close();
            }

        }

        /* 表示するデータ領域を選択したときは次の動作モードを読み込む */
        private void listDataDomain_TextChanged(object sender, EventArgs e)
        {
            string domain = this.listDataDomain.SelectedValue.ToString();
            ProgramMode nextMode;

            switch (domain)
            {
                case Const.VIEW_V_P00_REMT_CONN:
                    nextMode = new ModeVP00RemtConn();
                    break;
                case Const.VIEW_V_P00_MCNT_SECT:
                    nextMode = new ModeVP00McntSect();
                    break;
                case Const.VIEW_V_P00_MCNT_MTYP:
                    nextMode = new ModeVP00McntMtyp();
                    break;
                case Const.TABLE_MST_CMPN:
                    nextMode = new ModeMstCmpn();
                    break;
                case Const.TABLE_MST_ITEQ:
                    nextMode = new ModeMstIteq();
                    break;
                case Const.TABLE_MST_SYSC:
                    nextMode = new ModeMstSysc();
                    break;
                case Const.TABLE_MST_EQRL:
                    nextMode = new ModeMstEqrl();
                    break;
                case Const.TABLE_MST_REMT_CONN:
                    nextMode = new ModeMstRemtConn();
                    break;
                case Const.TABLE_MST_SECT:
                    nextMode = new ModeMstSect();
                    break;
                case Const.TABLE_MST_MCNT:
                    nextMode = new ModeMstMcnt();
                    break;
                default:
                    return;
            }

            if (mode.GetType() != nextMode.GetType())
            {
                mode = nextMode;

                RefreshForm();
            }
            else
            {
            }

            return;
        }

        /* ExcelボタンをクリックしたときはExcelシートにデータを出力する */
        private void btnExcel_Click(object sender, EventArgs e)
        {
            Library.ExcelOutput eo = new Library.ExcelOutput();

            try
            {
                if (dataGridViewMain.RowCount == 0)
                {
                    return;
                }

                string strNowDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = strNowDate + ".xls";
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                sfd.Filter = "Microsoft Office Excel (*.xls)|*.xls;";
                sfd.FilterIndex = 1;
                sfd.RestoreDirectory = true;
                sfd.OverwritePrompt = true;
                sfd.CheckPathExists = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    eo.OutputSingleSheet(this.dataGridViewMain, sfd.FileName);
                }

                eo = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return;
        }

        /* パラメータの値を選択したときは検索条件を変えてデータを絞り込む */
        private void listParamN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender.GetType() != typeof(ComboBox))
                {
                    return;
                }
                else if (eventStop == false)
                {
                    RefreshCombinedBox(((ComboBox)sender).Name);
                    RefreshDataGrid();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /* データグリッド上のセルをクリックしたときはその列のデータを入力ボックスにコピーする */
        private void dataGridViewMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*if (mode.flagViewMode == true || this.dataGridViewMain.RowCount == 0 || e.RowIndex < 0)
            {
                return;
            }
            else
            {
                FillBySelectedRowValue(e.RowIndex);
            }*/
        }

        private void dataGridViewMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.modeflag = 1;
            comboBox1.Hide();
            label1.Hide();
            listParam1.Visible = true;
            listDataDomain.Visible = true;
            labelListParam1.Visible = true;
            labelListDataDomain.Visible = true;
            comboBox1.Hide();
            label1.Hide();
            listParam1.Refresh();
            listDataDomain.Refresh();
            
            DataTable dataTable = (DataTable)this.dataGridViewMain.DataSource;
            if (dataTable != null)
            {
                dataTable.Clear();
            }
            //RefreshForm();
            this.dataGridViewMain.Refresh();            
            
            try
            {
                CreateConnection();
                Console.WriteLine("接続出来ました!!!");
                mode = new ModeVP00RemtConn();   //デフォルトの動作モード
                RefreshForm();
                mode.cbsDataDomain.Apply(ref this.listDataDomain, ref this.labelListDataDomain);
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            listParam1.Refresh();
            listDataDomain.Refresh();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.modeflag = 2;

            listParam1.Hide();
            labelListParam1.Hide();
            listDataDomain.Hide();
            labelListDataDomain.Hide();
            comboBox1.Visible = true;
            label1.Visible = true;
            DataTable dataTable = (DataTable)this.dataGridViewMain.DataSource;
            if (dataTable != null)
            {
                dataTable.Clear();
            }
            this.dataGridViewMain.Columns.Clear();
            this.dataGridViewMain.Refresh();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            /*OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            String file = openFileDialog.FileName;*/
            String file = istream[Const.INI_SECTION_LOCAL, Const.INI_FILEPATH];
            Console.WriteLine(file);
            BindData(file);

        }

        //CSVファイルからデータを取って表示されます
        private void BindData(string path)
        {
            String name = comboBox1.Items[comboBox1.SelectedIndex].ToString();
            DataTable dt = new DataTable();
            String[] lines;
            //lines = System.IO.File.ReadAllLines(path,Encoding.GetEncoding("iso-8859-1"));
            lines = System.IO.File.ReadAllLines(path, Encoding.GetEncoding("shift-jis"));

            if (lines.Length > 0)
            {
                String firstline = lines[0];
                //String[] header = firstline.Split('\t');
                String[] header = firstline.Split(',');
                Console.WriteLine(header[0]);
                foreach (String labels in header)
                {
                    dt.Columns.Add(new DataColumn(labels));
                    Console.WriteLine(labels);
                }

                for (int i = 1; i < lines.Length; i++)
                {
                    //string[] dataWords = lines[i].Split('\t');
                    string[] dataWords = lines[i].Split(',');
                    DataRow dr = dt.NewRow();
                    int columnIndex = 0;
                    // Console.WriteLine(header[i] + dataWords[0]);
                    String cname = dataWords[0];

                    foreach (string headerWord in header)
                    {
                        if (cname == name)
                        {
                            dr[headerWord] = dataWords[columnIndex++];
                            Console.WriteLine(cname + headerWord);
                        }
                        else
                        {
                            dr[headerWord] = null;
                        }
                    }

                    if (dr.IsNull(i)) { }
                    else
                    {
                        dt.Rows.Add(dr);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    dataGridViewMain.DataSource = dt;
                }

            }
        }

        private void listDataDomain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }   
}
