namespace quotedPrice
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainToolStrip = new System.Windows.Forms.MenuStrip();
            this.btnProjectDetail = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnProjectBudget = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUnits = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnQueryPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLogout = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.txtWelcome = new System.Windows.Forms.ToolStripLabel();
            this.mainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnProjectDetail,
            this.toolStripSeparator5,
            this.btnProjectBudget,
            this.toolStripSeparator4,
            this.btnUnits,
            this.toolStripSeparator3,
            this.btnQueryPrint,
            this.toolStripSeparator2,
            this.btnLogout,
            this.toolStripSeparator1,
            this.txtWelcome});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(881, 59);
            this.mainToolStrip.TabIndex = 2;
            this.mainToolStrip.Text = "toolStrip1";
            // 
            // btnProjectDetail
            // 
            this.btnProjectDetail.Image = global::quotedPrice.Properties.Resources.budget;
            this.btnProjectDetail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProjectDetail.Name = "btnProjectDetail";
            this.btnProjectDetail.Size = new System.Drawing.Size(156, 52);
            this.btnProjectDetail.Tag = "3";
            this.btnProjectDetail.Text = "工程项目报价一览";
            this.btnProjectDetail.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 55);
            // 
            // btnProjectBudget
            // 
            this.btnProjectBudget.Image = global::quotedPrice.Properties.Resources.material;
            this.btnProjectBudget.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProjectBudget.Name = "btnProjectBudget";
            this.btnProjectBudget.Size = new System.Drawing.Size(132, 52);
            this.btnProjectBudget.Tag = "3";
            this.btnProjectBudget.Text = "工程项目编辑";
            this.btnProjectBudget.Click += new System.EventHandler(this.btnProjectBudget_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 55);
            this.toolStripSeparator4.Visible = false;
            // 
            // btnUnits
            // 
            this.btnUnits.Image = global::quotedPrice.Properties.Resources.unit;
            this.btnUnits.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnits.Name = "btnUnits";
            this.btnUnits.Size = new System.Drawing.Size(132, 52);
            this.btnUnits.Tag = "1";
            this.btnUnits.Text = "计量单位维护";
            this.btnUnits.Visible = false;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 55);
            this.toolStripSeparator3.Visible = false;
            // 
            // btnQueryPrint
            // 
            this.btnQueryPrint.Image = global::quotedPrice.Properties.Resources.report;
            this.btnQueryPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnQueryPrint.Name = "btnQueryPrint";
            this.btnQueryPrint.Size = new System.Drawing.Size(108, 52);
            this.btnQueryPrint.Tag = "7";
            this.btnQueryPrint.Text = "打印导出";
            this.btnQueryPrint.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 55);
            // 
            // btnLogout
            // 
            this.btnLogout.Image = global::quotedPrice.Properties.Resources.logout;
            this.btnLogout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(108, 52);
            this.btnLogout.Tag = "7";
            this.btnLogout.Text = "退出系统";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 55);
            // 
            // txtWelcome
            // 
            this.txtWelcome.Name = "txtWelcome";
            this.txtWelcome.Size = new System.Drawing.Size(164, 52);
            this.txtWelcome.Text = "　　　欢迎使用吉屋报价系统";
            this.txtWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 522);
            this.Controls.Add(this.mainToolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Text = "报价生成器";
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton btnProjectBudget;
        private System.Windows.Forms.ToolStripButton btnUnits;
        private System.Windows.Forms.ToolStripButton btnQueryPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnLogout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel txtWelcome;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnProjectDetail;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}

