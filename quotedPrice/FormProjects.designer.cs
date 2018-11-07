namespace quotedPrice
{
    partial class FormProjects
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button7 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gCBBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new quotedPrice.DataSet1();
            this.gCBTableAdapter = new quotedPrice.DataSet1TableAdapters.GCBTableAdapter();
            this.dataGridView1 = new ProjectBudget.ScrollDataGrid(this.components);
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客户名称DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.税率 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.总计 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.创建日期DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.备注DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gCBBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 418);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 64);
            this.panel1.TabIndex = 0;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(450, 15);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 36);
            this.button7.TabIndex = 12;
            this.button7.Text = "添加备注";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(897, 15);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 36);
            this.button4.TabIndex = 11;
            this.button4.Text = "刷新(&R)";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.OnFreshButtonClick);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(612, 14);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 36);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "退出(&X)";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.OnExitButtonClick);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(531, 15);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 36);
            this.button5.TabIndex = 8;
            this.button5.Text = "打印(&P)";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.OnPrintProjectButtonClick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(800, 15);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 36);
            this.button3.TabIndex = 10;
            this.button3.Text = "工程复制";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.OnCopyProjectButtonClick);
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.Color.Red;
            this.button2.Location = new System.Drawing.Point(369, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 36);
            this.button2.TabIndex = 5;
            this.button2.Text = "删除(&D)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OnDeleteProjectButtonClick);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(288, 14);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 36);
            this.button6.TabIndex = 6;
            this.button6.Text = "添加(&A)";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.OnCreateProjectButtonClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(207, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 36);
            this.button1.TabIndex = 6;
            this.button1.Text = "详细浏览";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnViewDetailButtonClick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(90, 23);
            this.textBox1.MaxLength = 30;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.OnSearchKeyChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "工程名称定位";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("楷体", 24F);
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(984, 40);
            this.label1.TabIndex = 1;
            this.label1.Text = "工程项目编辑";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gCBBindingSource
            // 
            this.gCBBindingSource.DataMember = "GCB";
            this.gCBBindingSource.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gCBTableAdapter
            // 
            this.gCBTableAdapter.ClearBeforeFill = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.客户名称DataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.税率,
            this.总计,
            this.创建日期DataGridViewTextBoxColumn,
            this.备注DataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.gCBBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 40);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 21;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(984, 378);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnProjectGridCellDoubleClick);
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "工程名称";
            this.dataGridViewTextBoxColumn2.HeaderText = "工程名称";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // 客户名称DataGridViewTextBoxColumn
            // 
            this.客户名称DataGridViewTextBoxColumn.DataPropertyName = "客户名称";
            this.客户名称DataGridViewTextBoxColumn.HeaderText = "客户名称";
            this.客户名称DataGridViewTextBoxColumn.Name = "客户名称DataGridViewTextBoxColumn";
            this.客户名称DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "客户地址";
            this.dataGridViewTextBoxColumn3.HeaderText = "客户地址";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "联系方式";
            this.dataGridViewTextBoxColumn4.HeaderText = "联系方式";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "工程负责人";
            this.dataGridViewTextBoxColumn5.HeaderText = "工程负责人";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "税前合计";
            this.dataGridViewTextBoxColumn6.HeaderText = "税前合计";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "税金";
            this.dataGridViewTextBoxColumn7.HeaderText = "税金";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // 税率
            // 
            this.税率.DataPropertyName = "税率";
            this.税率.HeaderText = "税率";
            this.税率.Name = "税率";
            this.税率.ReadOnly = true;
            // 
            // 总计
            // 
            this.总计.DataPropertyName = "总计";
            this.总计.HeaderText = "总计";
            this.总计.Name = "总计";
            this.总计.ReadOnly = true;
            // 
            // 创建日期DataGridViewTextBoxColumn
            // 
            this.创建日期DataGridViewTextBoxColumn.DataPropertyName = "创建日期";
            this.创建日期DataGridViewTextBoxColumn.HeaderText = "创建日期";
            this.创建日期DataGridViewTextBoxColumn.Name = "创建日期DataGridViewTextBoxColumn";
            this.创建日期DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // 备注DataGridViewTextBoxColumn
            // 
            this.备注DataGridViewTextBoxColumn.DataPropertyName = "备注";
            this.备注DataGridViewTextBoxColumn.HeaderText = "备注";
            this.备注DataGridViewTextBoxColumn.Name = "备注DataGridViewTextBoxColumn";
            this.备注DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // FormProjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 482);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "FormProjects";
            this.Text = "工程项目编辑";
            this.Load += new System.EventHandler(this.FormProjects_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gCBBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button6;
        private ProjectBudget.ScrollDataGrid dataGridView1;
        //private GcysDataSet gcysDataSet;
        private System.Windows.Forms.BindingSource gCBBindingSource;
        //private GcysDataSetTableAdapters.GCBTableAdapter gCBTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工程名称DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客户地址DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 联系方式DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工程负责人DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 总计DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 税前合计DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 税金DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 制单人DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 制单日期DataGridViewTextBoxColumn;
        private System.Windows.Forms.Button button4;
        private DataSet1 dataSet1;
        private DataSet1TableAdapters.GCBTableAdapter gCBTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn 联系人DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客户名称DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn 税率;
        private System.Windows.Forms.DataGridViewTextBoxColumn 总计;
        private System.Windows.Forms.DataGridViewTextBoxColumn 创建日期DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 备注DataGridViewTextBoxColumn;
        private System.Windows.Forms.Button button7;
    }
}