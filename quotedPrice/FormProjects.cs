using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace quotedPrice
{
    public partial class FormProjects : Form
    {
        public FormProjects()
        {
            InitializeComponent();
        }

        private void FormProjects_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“dataSet1.GCB”中。您可以根据需要移动或删除它。
            this.gCBTableAdapter.Fill(this.dataSet1.GCB);
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

        private void OnViewDetailButtonClick(object sender, EventArgs e)
        {
            EditCurrentProject(false);
        }

        private void EditCurrentProject(bool copy)
        {
            var rowView = gCBBindingSource.Current as DataRowView;
            if (rowView != null)
            {
                var row = rowView.Row as DataSet1.GCBRow;
                if (row != null)
                {
                    var form = new FormProjectBudget() { ProjectKey = row.工程关键字, CopyProject = copy };
                    form.ShowDialog(this);
                }
            }
        }

        private void OnProjectGridCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditCurrentProject(false);
        }

        private void OnCreateProjectButtonClick(object sender, EventArgs e)
        {
           var form = new FormProjectBudget() { ProjectKey = string.Empty};
           form.ShowDialog(this);
        }

        private void OnExitButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void OnDeleteProjectButtonClick(object sender, EventArgs e)
        {
            if(DialogResult.OK == MessageBox.Show("确定要删除该项目吗？该操作不可恢复，请慎重操作！", "系统提示"
                , MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                var rowView = gCBBindingSource.Current as DataRowView;
                if (rowView != null)
                {
                    var row = rowView.Row as DataSet1.GCBRow;
                    if (row != null && gCBTableAdapter.DeleteProject(row) > 0)
                    {
                       Utility.Info("删除成功");
                    }
                }
            }
        }
        private void OnCopyProjectButtonClick(object sender, EventArgs e)
        {
            EditCurrentProject(true);
        }

        private void OnFreshButtonClick(object sender, EventArgs e)
        {
            this.gCBTableAdapter.Fill(this.dataSet1.GCB);
            gCBBindingSource.Sort = "创建日期 desc";
        }

        private void OnPrintProjectButtonClick(object sender, EventArgs e)
        {
            var row = gCBBindingSource.Current as DataRowView;
            /*
            if (row != null)
            {
               var project = row.Row as DataSet1.GCBRow;
               if (project != null)
               {
                    var form = new FormPrint();
                    form.ProjectInfo = project;
                    form.Show(this);
               }
            }*/
            var form = new FormReport();
            form.Show(this);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var form = new FormEditText() { dataSource= gCBBindingSource };
            form.ShowDialog(this);
            if (form.DialogResult == DialogResult.OK)
            {
                gCBBindingSource.EndEdit();
                gCBTableAdapter.Update(dataSet1.GCB);
            }
        }
    }
}
