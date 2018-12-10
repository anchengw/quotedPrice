using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace quotedPrice
{
    public partial class FormProjectsDetail : frmBase
    {
        public FormProjectsDetail()
        {
            InitializeComponent();
        }

        private void FormProjects_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“dataSet1.ProjectDetail”中。您可以根据需要移动或删除它。
            this.projectDetailTableAdapter.Fill(this.dataSet1.ProjectDetail);
            gCBBindingSource.Sort = "创建日期 desc";
        }

        private void OnSearchKeyChanged(object sender, EventArgs e)
        {
            string strKey = textBox1.Text;
            if (!string.IsNullOrEmpty(strKey))
            {
                gCBBindingSource.Filter = "工程名称 like '" + strKey + "%'";
            }
            else
            {
                gCBBindingSource.Filter = string.Empty;
            }
        }
        
        private void OnExitButtonClick(object sender, EventArgs e)
        {
            Close();
        }
        private void OnFreshButtonClick(object sender, EventArgs e)
        {
            this.projectDetailTableAdapter.Fill(this.dataSet1.ProjectDetail);
            gCBBindingSource.Sort = "创建日期 desc";
        }

        private void OnPrintProjectButtonClick(object sender, EventArgs e)
        {
            var row = gCBBindingSource.Current as DataRowView;
            if (row != null)
            {
               // var project = row.Row as GcysDataSet.GCBRow;
               // if (project != null)
               // {
                    //var form = new FormPrintProject();
               //     form.ProjectInfo = project;
               //     form.Show(this);
               // }
            }
        }
       
    }
}
