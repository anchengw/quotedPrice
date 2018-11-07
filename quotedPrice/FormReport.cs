using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace quotedPrice
{
    public partial class FormReport : Form
    {
        public FormReport()
        {
            InitializeComponent();
        }

        private void FormReport_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“DataSet1.ProjectDetail”中。您可以根据需要移动或删除它。
            this.ProjectDetailTableAdapter.Fill(this.DataSet1.ProjectDetail);

            this.reportViewer1.RefreshReport();
        }
    }
}
