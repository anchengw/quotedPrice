namespace quotedPrice
{
    partial class FormProjectsDetail
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gCBBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new quotedPrice.DataSet1();
            this.projectDetailTableAdapter = new quotedPrice.DataSet1TableAdapters.ProjectDetailTableAdapter();
            this.dataGridView1 = new ProjectBudget.ScrollDataGrid(this.components);
            this.工程名称DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客户名称DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.项目名称DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.部件名称DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.材料DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.单位DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.数量DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.长度DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.宽度DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.厚度DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.成型尺寸l1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.成型尺寸l2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.成型面积DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.图层名称DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.单价DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.金额DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.项目总计 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工程总计 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.标准单价DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.标准金额DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.创建日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gCBBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 418);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 64);
            this.panel1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(90, 23);
            this.textBox1.MaxLength = 30;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(181, 21);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.OnSearchKeyChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "工程名称定位";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(277, 15);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 36);
            this.button4.TabIndex = 11;
            this.button4.Text = "刷新(&R)";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.OnFreshButtonClick);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(897, 16);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 36);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "退出(&X)";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.OnExitButtonClick);
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
            this.label1.Text = "工程项目报价一览";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gCBBindingSource
            // 
            this.gCBBindingSource.DataMember = "ProjectDetail";
            this.gCBBindingSource.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // projectDetailTableAdapter
            // 
            this.projectDetailTableAdapter.ClearBeforeFill = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.工程名称DataGridViewTextBoxColumn,
            this.客户名称DataGridViewTextBoxColumn,
            this.项目名称DataGridViewTextBoxColumn,
            this.部件名称DataGridViewTextBoxColumn,
            this.材料DataGridViewTextBoxColumn,
            this.单位DataGridViewTextBoxColumn,
            this.数量DataGridViewTextBoxColumn,
            this.长度DataGridViewTextBoxColumn,
            this.宽度DataGridViewTextBoxColumn,
            this.厚度DataGridViewTextBoxColumn,
            this.成型尺寸l1DataGridViewTextBoxColumn,
            this.成型尺寸l2DataGridViewTextBoxColumn,
            this.成型面积DataGridViewTextBoxColumn,
            this.图层名称DataGridViewTextBoxColumn,
            this.单价DataGridViewTextBoxColumn,
            this.金额DataGridViewTextBoxColumn,
            this.项目总计,
            this.工程总计,
            this.标准单价DataGridViewTextBoxColumn,
            this.标准金额DataGridViewTextBoxColumn,
            this.创建日期});
            this.dataGridView1.DataSource = this.gCBBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 40);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 21;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(984, 378);
            this.dataGridView1.TabIndex = 2;
            // 
            // 工程名称DataGridViewTextBoxColumn
            // 
            this.工程名称DataGridViewTextBoxColumn.DataPropertyName = "工程名称";
            this.工程名称DataGridViewTextBoxColumn.HeaderText = "工程名称";
            this.工程名称DataGridViewTextBoxColumn.Name = "工程名称DataGridViewTextBoxColumn";
            this.工程名称DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // 客户名称DataGridViewTextBoxColumn
            // 
            this.客户名称DataGridViewTextBoxColumn.DataPropertyName = "客户名称";
            this.客户名称DataGridViewTextBoxColumn.HeaderText = "客户名称";
            this.客户名称DataGridViewTextBoxColumn.Name = "客户名称DataGridViewTextBoxColumn";
            this.客户名称DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // 项目名称DataGridViewTextBoxColumn
            // 
            this.项目名称DataGridViewTextBoxColumn.DataPropertyName = "项目名称";
            this.项目名称DataGridViewTextBoxColumn.HeaderText = "项目名称";
            this.项目名称DataGridViewTextBoxColumn.Name = "项目名称DataGridViewTextBoxColumn";
            this.项目名称DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // 部件名称DataGridViewTextBoxColumn
            // 
            this.部件名称DataGridViewTextBoxColumn.DataPropertyName = "部件名称";
            this.部件名称DataGridViewTextBoxColumn.HeaderText = "部件名称";
            this.部件名称DataGridViewTextBoxColumn.Name = "部件名称DataGridViewTextBoxColumn";
            this.部件名称DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // 材料DataGridViewTextBoxColumn
            // 
            this.材料DataGridViewTextBoxColumn.DataPropertyName = "材料";
            this.材料DataGridViewTextBoxColumn.HeaderText = "材料";
            this.材料DataGridViewTextBoxColumn.Name = "材料DataGridViewTextBoxColumn";
            this.材料DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // 单位DataGridViewTextBoxColumn
            // 
            this.单位DataGridViewTextBoxColumn.DataPropertyName = "单位";
            this.单位DataGridViewTextBoxColumn.HeaderText = "单位";
            this.单位DataGridViewTextBoxColumn.Name = "单位DataGridViewTextBoxColumn";
            this.单位DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // 数量DataGridViewTextBoxColumn
            // 
            this.数量DataGridViewTextBoxColumn.DataPropertyName = "数量";
            this.数量DataGridViewTextBoxColumn.HeaderText = "数量";
            this.数量DataGridViewTextBoxColumn.Name = "数量DataGridViewTextBoxColumn";
            this.数量DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // 长度DataGridViewTextBoxColumn
            // 
            this.长度DataGridViewTextBoxColumn.DataPropertyName = "长度";
            this.长度DataGridViewTextBoxColumn.HeaderText = "长度";
            this.长度DataGridViewTextBoxColumn.Name = "长度DataGridViewTextBoxColumn";
            this.长度DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // 宽度DataGridViewTextBoxColumn
            // 
            this.宽度DataGridViewTextBoxColumn.DataPropertyName = "宽度";
            this.宽度DataGridViewTextBoxColumn.HeaderText = "宽度";
            this.宽度DataGridViewTextBoxColumn.Name = "宽度DataGridViewTextBoxColumn";
            this.宽度DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // 厚度DataGridViewTextBoxColumn
            // 
            this.厚度DataGridViewTextBoxColumn.DataPropertyName = "厚度";
            this.厚度DataGridViewTextBoxColumn.HeaderText = "厚度";
            this.厚度DataGridViewTextBoxColumn.Name = "厚度DataGridViewTextBoxColumn";
            this.厚度DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // 成型尺寸l1DataGridViewTextBoxColumn
            // 
            this.成型尺寸l1DataGridViewTextBoxColumn.DataPropertyName = "成型尺寸l1";
            this.成型尺寸l1DataGridViewTextBoxColumn.HeaderText = "成型尺寸l1";
            this.成型尺寸l1DataGridViewTextBoxColumn.Name = "成型尺寸l1DataGridViewTextBoxColumn";
            this.成型尺寸l1DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // 成型尺寸l2DataGridViewTextBoxColumn
            // 
            this.成型尺寸l2DataGridViewTextBoxColumn.DataPropertyName = "成型尺寸l2";
            this.成型尺寸l2DataGridViewTextBoxColumn.HeaderText = "成型尺寸l2";
            this.成型尺寸l2DataGridViewTextBoxColumn.Name = "成型尺寸l2DataGridViewTextBoxColumn";
            this.成型尺寸l2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // 成型面积DataGridViewTextBoxColumn
            // 
            this.成型面积DataGridViewTextBoxColumn.DataPropertyName = "成型面积";
            this.成型面积DataGridViewTextBoxColumn.HeaderText = "成型面积";
            this.成型面积DataGridViewTextBoxColumn.Name = "成型面积DataGridViewTextBoxColumn";
            this.成型面积DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // 图层名称DataGridViewTextBoxColumn
            // 
            this.图层名称DataGridViewTextBoxColumn.DataPropertyName = "图层名称";
            this.图层名称DataGridViewTextBoxColumn.HeaderText = "图层名称";
            this.图层名称DataGridViewTextBoxColumn.Name = "图层名称DataGridViewTextBoxColumn";
            this.图层名称DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // 单价DataGridViewTextBoxColumn
            // 
            this.单价DataGridViewTextBoxColumn.DataPropertyName = "单价";
            this.单价DataGridViewTextBoxColumn.HeaderText = "单价";
            this.单价DataGridViewTextBoxColumn.Name = "单价DataGridViewTextBoxColumn";
            this.单价DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // 金额DataGridViewTextBoxColumn
            // 
            this.金额DataGridViewTextBoxColumn.DataPropertyName = "金额";
            this.金额DataGridViewTextBoxColumn.HeaderText = "金额";
            this.金额DataGridViewTextBoxColumn.Name = "金额DataGridViewTextBoxColumn";
            this.金额DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // 项目总计
            // 
            this.项目总计.DataPropertyName = "项目总计";
            this.项目总计.HeaderText = "项目总计";
            this.项目总计.Name = "项目总计";
            this.项目总计.ReadOnly = true;
            // 
            // 工程总计
            // 
            this.工程总计.DataPropertyName = "工程总计";
            this.工程总计.HeaderText = "工程总计";
            this.工程总计.Name = "工程总计";
            this.工程总计.ReadOnly = true;
            // 
            // 标准单价DataGridViewTextBoxColumn
            // 
            this.标准单价DataGridViewTextBoxColumn.DataPropertyName = "标准单价";
            this.标准单价DataGridViewTextBoxColumn.HeaderText = "标准单价";
            this.标准单价DataGridViewTextBoxColumn.Name = "标准单价DataGridViewTextBoxColumn";
            this.标准单价DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // 标准金额DataGridViewTextBoxColumn
            // 
            this.标准金额DataGridViewTextBoxColumn.DataPropertyName = "标准金额";
            this.标准金额DataGridViewTextBoxColumn.HeaderText = "标准金额";
            this.标准金额DataGridViewTextBoxColumn.Name = "标准金额DataGridViewTextBoxColumn";
            this.标准金额DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // 创建日期
            // 
            this.创建日期.DataPropertyName = "创建日期";
            this.创建日期.HeaderText = "创建日期";
            this.创建日期.Name = "创建日期";
            this.创建日期.ReadOnly = true;
            // 
            // FormProjectsDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 482);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "FormProjectsDetail";
            this.Text = "工程项目报价一览";
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private ProjectBudget.ScrollDataGrid dataGridView1;
        //private GcysDataSet gcysDataSet;
        private System.Windows.Forms.BindingSource gCBBindingSource;
        //private GcysDataSetTableAdapters.GCBTableAdapter gCBTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工程编码DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客户地址DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 联系方式DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工程负责人DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 税前合计DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 税金DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 制单人DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 制单日期DataGridViewTextBoxColumn;
        private System.Windows.Forms.Button button4;
        private DataSet1 dataSet1;
        private DataSet1TableAdapters.ProjectDetailTableAdapter projectDetailTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn 合计金额DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 总计DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工程名称DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客户名称DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 项目名称DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 部件名称DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 材料DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 单位DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 数量DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 长度DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 宽度DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 厚度DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 成型尺寸l1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 成型尺寸l2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 成型面积DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 图层名称DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 单价DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 金额DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 项目总计;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工程总计;
        private System.Windows.Forms.DataGridViewTextBoxColumn 标准单价DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 标准金额DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 创建日期;
    }
}