namespace Maintenance
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.labelListDataDomain = new System.Windows.Forms.Label();
            this.labelTextParam1 = new System.Windows.Forms.Label();
            this.listDataDomain = new System.Windows.Forms.ComboBox();
            this.textParam1 = new System.Windows.Forms.TextBox();
            this.textParam2 = new System.Windows.Forms.TextBox();
            this.labelTextParam2 = new System.Windows.Forms.Label();
            this.labelListParam2 = new System.Windows.Forms.Label();
            this.listParam2 = new System.Windows.Forms.ComboBox();
            this.checkParam1 = new System.Windows.Forms.CheckBox();
            this.checkParam2 = new System.Windows.Forms.CheckBox();
            this.checkParam3 = new System.Windows.Forms.CheckBox();
            this.labelListParam1 = new System.Windows.Forms.Label();
            this.listParam1 = new System.Windows.Forms.ComboBox();
            this.dataGridViewMain = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnInsert = new System.Windows.Forms.Button();
            this.listParam3 = new System.Windows.Forms.ComboBox();
            this.labelListParam3 = new System.Windows.Forms.Label();
            this.textParam3 = new System.Windows.Forms.TextBox();
            this.labelTextParam3 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAction = new System.Windows.Forms.Button();
            this.listParam4 = new System.Windows.Forms.ComboBox();
            this.labelListParam4 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).BeginInit();
            this.SuspendLayout();
            // 
            // labelListDataDomain
            // 
            this.labelListDataDomain.AutoSize = true;
            this.labelListDataDomain.Location = new System.Drawing.Point(12, 8);
            this.labelListDataDomain.Name = "labelListDataDomain";
            this.labelListDataDomain.Size = new System.Drawing.Size(57, 12);
            this.labelListDataDomain.TabIndex = 80;
            this.labelListDataDomain.Text = "データ領域";
            // 
            // labelTextParam1
            // 
            this.labelTextParam1.AutoSize = true;
            this.labelTextParam1.Location = new System.Drawing.Point(226, 114);
            this.labelTextParam1.Name = "labelTextParam1";
            this.labelTextParam1.Size = new System.Drawing.Size(55, 12);
            this.labelTextParam1.TabIndex = 84;
            this.labelTextParam1.Text = "パラメータ1";
            // 
            // listDataDomain
            // 
            this.listDataDomain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listDataDomain.FormattingEnabled = true;
            this.listDataDomain.Location = new System.Drawing.Point(12, 24);
            this.listDataDomain.Name = "listDataDomain";
            this.listDataDomain.Size = new System.Drawing.Size(160, 20);
            this.listDataDomain.TabIndex = 1;
            this.listDataDomain.SelectedIndexChanged += new System.EventHandler(this.listDataDomain_SelectedIndexChanged);
            // 
            // textParam1
            // 
            this.textParam1.Location = new System.Drawing.Point(226, 130);
            this.textParam1.Name = "textParam1";
            this.textParam1.Size = new System.Drawing.Size(160, 19);
            this.textParam1.TabIndex = 5;
            // 
            // textParam2
            // 
            this.textParam2.Location = new System.Drawing.Point(406, 130);
            this.textParam2.Name = "textParam2";
            this.textParam2.Size = new System.Drawing.Size(160, 19);
            this.textParam2.TabIndex = 6;
            // 
            // labelTextParam2
            // 
            this.labelTextParam2.AutoSize = true;
            this.labelTextParam2.Location = new System.Drawing.Point(406, 114);
            this.labelTextParam2.Name = "labelTextParam2";
            this.labelTextParam2.Size = new System.Drawing.Size(55, 12);
            this.labelTextParam2.TabIndex = 85;
            this.labelTextParam2.Text = "パラメータ2";
            // 
            // labelListParam2
            // 
            this.labelListParam2.AutoSize = true;
            this.labelListParam2.Location = new System.Drawing.Point(490, 8);
            this.labelListParam2.Name = "labelListParam2";
            this.labelListParam2.Size = new System.Drawing.Size(55, 12);
            this.labelListParam2.TabIndex = 82;
            this.labelListParam2.Text = "パラメータ2";
            // 
            // listParam2
            // 
            this.listParam2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listParam2.FormattingEnabled = true;
            this.listParam2.Location = new System.Drawing.Point(492, 24);
            this.listParam2.Name = "listParam2";
            this.listParam2.Size = new System.Drawing.Size(240, 20);
            this.listParam2.TabIndex = 3;
            this.listParam2.SelectedIndexChanged += new System.EventHandler(this.listParamN_TextChanged);
            // 
            // checkParam1
            // 
            this.checkParam1.AutoSize = true;
            this.checkParam1.Location = new System.Drawing.Point(228, 169);
            this.checkParam1.Name = "checkParam1";
            this.checkParam1.Size = new System.Drawing.Size(74, 16);
            this.checkParam1.TabIndex = 11;
            this.checkParam1.Text = "パラメータ1";
            this.checkParam1.UseVisualStyleBackColor = true;
            // 
            // checkParam2
            // 
            this.checkParam2.AutoSize = true;
            this.checkParam2.Location = new System.Drawing.Point(410, 169);
            this.checkParam2.Name = "checkParam2";
            this.checkParam2.Size = new System.Drawing.Size(74, 16);
            this.checkParam2.TabIndex = 12;
            this.checkParam2.Text = "パラメータ2";
            this.checkParam2.UseVisualStyleBackColor = true;
            // 
            // checkParam3
            // 
            this.checkParam3.AutoSize = true;
            this.checkParam3.Location = new System.Drawing.Point(586, 169);
            this.checkParam3.Name = "checkParam3";
            this.checkParam3.Size = new System.Drawing.Size(74, 16);
            this.checkParam3.TabIndex = 13;
            this.checkParam3.Text = "パラメータ3";
            this.checkParam3.UseVisualStyleBackColor = true;
            // 
            // labelListParam1
            // 
            this.labelListParam1.AutoSize = true;
            this.labelListParam1.Location = new System.Drawing.Point(226, 8);
            this.labelListParam1.Name = "labelListParam1";
            this.labelListParam1.Size = new System.Drawing.Size(55, 12);
            this.labelListParam1.TabIndex = 81;
            this.labelListParam1.Text = "パラメータ1";
            // 
            // listParam1
            // 
            this.listParam1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listParam1.FormattingEnabled = true;
            this.listParam1.Location = new System.Drawing.Point(226, 24);
            this.listParam1.Name = "listParam1";
            this.listParam1.Size = new System.Drawing.Size(240, 20);
            this.listParam1.TabIndex = 2;
            this.listParam1.SelectedIndexChanged += new System.EventHandler(this.listParamN_TextChanged);
            // 
            // dataGridViewMain
            // 
            this.dataGridViewMain.AllowUserToResizeRows = false;
            this.dataGridViewMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewMain.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9});
            this.dataGridViewMain.Location = new System.Drawing.Point(14, 212);
            this.dataGridViewMain.MultiSelect = false;
            this.dataGridViewMain.Name = "dataGridViewMain";
            this.dataGridViewMain.ReadOnly = true;
            this.dataGridViewMain.RowTemplate.Height = 21;
            this.dataGridViewMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMain.Size = new System.Drawing.Size(640, 320);
            this.dataGridViewMain.TabIndex = 99;
            this.dataGridViewMain.TabStop = false;
            this.dataGridViewMain.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMain_CellClick);
            this.dataGridViewMain.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMain_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "カラム1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 63;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "カラム2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 63;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "カラム3";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 63;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "カラム4";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 63;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "カラム5";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 63;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "カラム6";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 63;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "カラム8";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 63;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "カラム9";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 63;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "カラム10";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 69;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(670, 440);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 29;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(670, 296);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 31;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(671, 469);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExcel.TabIndex = 32;
            this.btnExcel.Text = "Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(670, 212);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(75, 23);
            this.btnInsert.TabIndex = 25;
            this.btnInsert.Text = "Insert";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // listParam3
            // 
            this.listParam3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listParam3.FormattingEnabled = true;
            this.listParam3.Location = new System.Drawing.Point(226, 80);
            this.listParam3.Name = "listParam3";
            this.listParam3.Size = new System.Drawing.Size(240, 20);
            this.listParam3.TabIndex = 4;
            this.listParam3.SelectedIndexChanged += new System.EventHandler(this.listParamN_TextChanged);
            // 
            // labelListParam3
            // 
            this.labelListParam3.AutoSize = true;
            this.labelListParam3.Location = new System.Drawing.Point(228, 64);
            this.labelListParam3.Name = "labelListParam3";
            this.labelListParam3.Size = new System.Drawing.Size(55, 12);
            this.labelListParam3.TabIndex = 83;
            this.labelListParam3.Text = "パラメータ3";
            // 
            // textParam3
            // 
            this.textParam3.Location = new System.Drawing.Point(586, 130);
            this.textParam3.Name = "textParam3";
            this.textParam3.Size = new System.Drawing.Size(160, 19);
            this.textParam3.TabIndex = 7;
            // 
            // labelTextParam3
            // 
            this.labelTextParam3.AutoSize = true;
            this.labelTextParam3.Location = new System.Drawing.Point(586, 114);
            this.labelTextParam3.Name = "labelTextParam3";
            this.labelTextParam3.Size = new System.Drawing.Size(55, 12);
            this.labelTextParam3.TabIndex = 86;
            this.labelTextParam3.Text = "パラメータ3";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(670, 254);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 43;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAction
            // 
            this.btnAction.Location = new System.Drawing.Point(671, 498);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(75, 23);
            this.btnAction.TabIndex = 100;
            this.btnAction.Text = "Action";
            this.btnAction.UseVisualStyleBackColor = true;
            this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // listParam4
            // 
            this.listParam4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listParam4.FormattingEnabled = true;
            this.listParam4.Location = new System.Drawing.Point(492, 80);
            this.listParam4.Name = "listParam4";
            this.listParam4.Size = new System.Drawing.Size(240, 20);
            this.listParam4.TabIndex = 101;
            this.listParam4.SelectedIndexChanged += new System.EventHandler(this.listParamN_TextChanged);
            // 
            // labelListParam4
            // 
            this.labelListParam4.AutoSize = true;
            this.labelListParam4.Location = new System.Drawing.Point(490, 64);
            this.labelListParam4.Name = "labelListParam4";
            this.labelListParam4.Size = new System.Drawing.Size(55, 12);
            this.labelListParam4.TabIndex = 102;
            this.labelListParam4.Text = "パラメータ4";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(58, 130);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(63, 16);
            this.radioButton1.TabIndex = 106;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "ONLINE";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(58, 168);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(69, 16);
            this.radioButton2.TabIndex = 107;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "OFFLINE";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "FA Test (テスト環境)",
            "Bangladesh (バングラデシュ)"});
            this.comboBox1.Location = new System.Drawing.Point(14, 94);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(160, 20);
            this.comboBox1.TabIndex = 108;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 109;
            this.label1.Text = "会社名";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.labelListParam4);
            this.Controls.Add(this.listParam4);
            this.Controls.Add(this.btnAction);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.labelTextParam3);
            this.Controls.Add(this.textParam3);
            this.Controls.Add(this.labelListParam3);
            this.Controls.Add(this.listParam3);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dataGridViewMain);
            this.Controls.Add(this.listParam1);
            this.Controls.Add(this.labelListParam1);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.checkParam3);
            this.Controls.Add(this.checkParam2);
            this.Controls.Add(this.checkParam1);
            this.Controls.Add(this.listParam2);
            this.Controls.Add(this.labelListParam2);
            this.Controls.Add(this.labelTextParam2);
            this.Controls.Add(this.textParam2);
            this.Controls.Add(this.textParam1);
            this.Controls.Add(this.listDataDomain);
            this.Controls.Add(this.labelTextParam1);
            this.Controls.Add(this.labelListDataDomain);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Maintenance Tool";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelListDataDomain;
        private System.Windows.Forms.Label labelTextParam1;
        private System.Windows.Forms.ComboBox listDataDomain;
        private System.Windows.Forms.TextBox textParam1;
        private System.Windows.Forms.TextBox textParam2;
        private System.Windows.Forms.Label labelTextParam2;
        private System.Windows.Forms.Label labelListParam2;
        private System.Windows.Forms.ComboBox listParam2;
        private System.Windows.Forms.CheckBox checkParam1;
        private System.Windows.Forms.CheckBox checkParam2;
        private System.Windows.Forms.CheckBox checkParam3;
        private System.Windows.Forms.Label labelListParam1;
        private System.Windows.Forms.ComboBox listParam1;
        private System.Windows.Forms.DataGridView dataGridViewMain;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.ComboBox listParam3;
        private System.Windows.Forms.Label labelListParam3;
        private System.Windows.Forms.TextBox textParam3;
        private System.Windows.Forms.Label labelTextParam3;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.Button btnAction;
        private System.Windows.Forms.ComboBox listParam4;
        private System.Windows.Forms.Label labelListParam4;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
    }
}

