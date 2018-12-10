using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace quotedPrice
{
    public partial class FormProjects : frmBase
    {
        public FormProjects()
        {
            InitializeComponent();
        }

        private void FormProjects_Load(object sender, EventArgs e)
        {
            dataGridView1.MultiSelect = true;
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
           if(form.DialogResult == DialogResult.OK)
            {
                button4.PerformClick();
            }
        }

        private void OnExitButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void OnDeleteProjectButtonClick(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {

                if (DialogResult.OK == MessageBox.Show("确定要删除该项目吗？该操作不可恢复，请慎重操作！", "系统提示"
                , MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    for (int i = this.dataGridView1.SelectedRows.Count-1; i >= 0; i--)
                    {
                        this.dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i].Index);
                    }
                    gCBBindingSource.EndEdit();
                    DataTable cdt = dataSet1.GCB.GetChanges();
                    ArrayList sqlList = new ArrayList();
                    for (int i = 0; i < cdt.Rows.Count; i++)
                    {
                        if (cdt.Rows[i].RowState == DataRowState.Deleted)
                        {
                            string ProjectKey = cdt.Rows[i][0, DataRowVersion.Original].ToString();
                            sqlList.Add("Delete from PARTS where [工程关键字] = '" + ProjectKey +"'");
                            sqlList.Add("Delete from XMB where  [工程关键字] = '" + ProjectKey + "'");
                        }
                    }
                    if (gCBTableAdapter.DeleteProject(sqlList))
                    {
                        gCBTableAdapter.Update(dataSet1.GCB);
                        Utility.Info("删除成功");
                    }
                    else
                        Utility.Info("删除失败");
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
            List<String> strList = new List<string>();
            foreach (DataGridViewRow dgr in dataGridView1.SelectedRows)
            {
                strList.Add(dgr.Cells[0].Value.ToString());
            }
            var form = new FormPrint();
            form.ProjectKey = strList.ToArray();
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
