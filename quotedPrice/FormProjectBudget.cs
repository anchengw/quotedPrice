using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace quotedPrice
{
    public partial class FormProjectBudget : Form
    {
        public FormProjectBudget()
        {
            InitializeComponent();
            openFileDialog.InitialDirectory = "c:\\";//注意这里写路径时要用c:\\而不是c:\
            openFileDialog.Filter = "Excel文件|*.xls;*.xlsx|所有文件 (*.*)|*.*";
            openFileDialog.FileName = "";
            openFileDialog.RestoreDirectory = true;
        }

        public bool CopyProject { get; set; }

        public string ProjectKey { get; set; }

        private void FormProjectBudget_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ProjectKey))
            {
                ProjectKey = gCBTableAdapter.GetNewProjectKey();
                dataSet1.GCB.AddGCBRow(ProjectKey, ProjectKey, string.Empty, string.Empty, string.Empty,
                                 string.Empty, 
                                  DateTime.Now, String.Empty, 0.0, 0.0, 0.0, 16.00);
            }
            else
            {
                if (CopyProject)
                {
                    var projectTable = new DataSet1.GCBDataTable();
                    gCBTableAdapter.GetProject(ProjectKey, projectTable);
                    var subProjectTable = new DataSet1.XMBDataTable();
                    gCBTableAdapter.GetSubProjects(ProjectKey, subProjectTable);
                    var detailTabel = new DataSet1.PARTSDataTable();
                    gCBTableAdapter.GetSubProjectsDetail(ProjectKey, detailTabel);
                    ProjectKey = gCBTableAdapter.GetNewProjectKey();

                    for (int i = 0; i < projectTable.Count; i++)
                    {
                        dataSet1.GCB.AddGCBRow(ProjectKey,
                        projectTable[i].工程名称, projectTable[i].客户名称, projectTable[i].客户地址,
                        projectTable[i].联系方式, projectTable[i].工程负责人,
                        DateTime.Now, " ", projectTable[i].税前合计, projectTable[i].税金, projectTable[i].总计,
                        projectTable[i].税率);
                    }
                    for (int i = 0; i < subProjectTable.Count; i++)
                    {
                        var subDetail = detailTabel.Select("项目关键字 = '" + subProjectTable[i].项目关键字 + "'");

                        subProjectTable[i].项目关键字 = gCBTableAdapter.GetSubProjectKey();
                        dataSet1.XMB.AddXMBRow(subProjectTable[i].项目关键字,
                            ProjectKey,"", subProjectTable[i].序号, subProjectTable[i].项目名称, subProjectTable[i].合计金额,
                             subProjectTable[i].统计标志, subProjectTable[i].楼层, subProjectTable[i].房间);

                        foreach (DataSet1.PARTSRow row in subDetail)
                        {
                            row.工程关键字 = ProjectKey;
                            row.项目关键字 = subProjectTable[i].项目关键字;
                            row.关键字 = gCBTableAdapter.GetNewSubProjectDetailId();
                            dataSet1.PARTS.AddPARTSRow(gCBTableAdapter.GetNewSubProjectDetailId(),
                                subProjectTable[i].项目关键字, ProjectKey,row.序号,row.颜色, row.部件名称,row.长度,row.宽度,
                                row.厚度,row.单位,row.数量,row.成型尺寸l1,row.成型尺寸l2,row.成型面积,row.材料,
                                row.图层名称,row.单价,row.金额,row.标准单价,row.标准金额,row.备注,row.品牌);
                        }
                    }
                }
                else
                {
                    gCBTableAdapter.GetProject(ProjectKey, dataSet1.GCB);
                    gCBTableAdapter.GetSubProjects(ProjectKey, dataSet1.XMB);
                    gCBTableAdapter.GetSubProjectsDetail(ProjectKey, dataSet1.PARTS);
                }
            }
            Text += string.Format("创建日期:{0:yyyy年M月d日}",dataSet1.GCB.Rows[0]["创建日期"]);
        }

        private void OnGridDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("输入数字格式不正确！");
        }

        private void OnCreateSubProjectButtonClick(object sender, EventArgs e)
        {
            dataSet1.XMB.AddXMBRow(gCBTableAdapter.GetSubProjectKey(), ProjectKey,"", (dataSet1.XMB.Rows.Count + 1).ToString("X"),
                string.Empty, 0, "1", "1","101");
        }

        private void OnSaveButtonClick(object sender, EventArgs e)
        {
            if (ValidateProjectInfo())
            {
                //CaculateFloorSum(false);
                CaculateTotalPrice();
                gCBDataSource.EndEdit();
                gCBTableAdapter.Update(dataSet1.GCB);
                new DataSet1TableAdapters.XMBTableAdapter().Update(dataSet1.XMB);
                new DataSet1TableAdapters.PARTSTableAdapter().Update(dataSet1.PARTS);
                DialogResult = DialogResult.OK;
            }
        }
        //验证工程
        private bool ValidateProjectInfo()
        {
            if (dataSet1.GCB.Rows.Count < 1)
            {
                Utility.Warning("未找到工程信息，请您确认操作！");
                return false;
            }
            var project = dataSet1.GCB.Rows[0] as DataSet1.GCBRow;
            if (project == null)
            {
                Utility.Warning("工程信息为空，请重新添加！");
                return false;
            }
            if (project.Is工程名称Null() || string.IsNullOrEmpty(project.工程名称))
            {
                Utility.Warning("请输入工程名称");
                return false;
            }
            if (project.Is客户名称Null() || string.IsNullOrEmpty(project.客户名称))
            {
                Utility.Warning("请输入客户名称！");
                return false;
            }
            if (project.Is联系方式Null() || string.IsNullOrEmpty(project.联系方式))
            {
                Utility.Warning("请输入联系方式！");
                return false;
            }
            if (project.Is备注Null() || string.IsNullOrEmpty(project.备注))
                project.备注 = " ";
            project.工程关键字 = string.IsNullOrEmpty(ProjectKey) ? ProjectKey = gCBTableAdapter.GetNewProjectKey() : ProjectKey;
            return ValidateSubProjectInfo(project);//验证项目
        }
        //验证项目
        private bool ValidateSubProjectInfo(DataSet1.GCBRow project)
        {
            if (dataSet1.XMB.Count < 1)
            {
                Utility.Warning("请添加项目！");
                return false;
            }
            foreach (DataSet1.XMBRow row in dataSet1.XMB.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    continue;
                row.工程关键字 = ProjectKey;
                if (string.IsNullOrEmpty(row.项目关键字))
                {
                    row.项目关键字 = gCBTableAdapter.GetSubProjectKey();
                }
                if (string.IsNullOrEmpty(row.项目名称))
                {
                    Utility.Warning("项目名称不能为空！");
                    return false;
                }
                if (row.Is序号Null() || string.IsNullOrEmpty(row.序号))
                    row.序号 = " ";
                bool b = ValidateSubProjectDetail(row);
                if (!b)
                    return false;
            }
            return true;
        }
        //验证子项目　部件
        private bool ValidateSubProjectDetail(DataSet1.XMBRow row)
        {
            if (dataSet1.PARTS.Count < 1)
            {
                Utility.Warning("请添加项目明细项！");
                return false;
            }
            foreach (DataSet1.PARTSRow dr in dataSet1.PARTS.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                    continue;
                dr.工程关键字 = ProjectKey;
                if (string.IsNullOrEmpty(dr.项目关键字))
                    dr.项目关键字 = row.项目关键字;
                if (string.IsNullOrEmpty(dr.关键字))
                    dr.关键字 = gCBTableAdapter.GetNewSubProjectDetailId();
                if (dr.Is序号Null())
                    dr.序号 = " ";
            }
            return true;
        }
        //取当前项目
        private DataSet1.XMBRow GetCurrentGczbRow()
        {
            var rv = XMBBindingSource.Current as DataRowView;
            if (rv != null)
            {
                return rv.Row as DataSet1.XMBRow;
            }
            return null;
        }
        //添加子项目部件
        private void OnGCZBMXAddingNew(object sender, System.ComponentModel.AddingNewEventArgs e)
        {
            var zbRow = GetCurrentGczbRow();

            if (zbRow == null)
            {
                MessageBox.Show("请先添加工程项目!");
                return;
            }
            var source = sender as BindingSource;
            if (source != null)
            {
                e.NewObject = dataSet1.PARTS.AddPARTSRow(
                    gCBTableAdapter.GetNewSubProjectDetailId(),
                    zbRow.项目关键字,
                    ProjectKey,
                    (dataSet1.PARTS.Rows.Count + 1).ToString("00"),
                    string.Empty,
                    string.Empty,
                    0f,
                    0f,
                    0f,
                    string.Empty,
                    0d,
                    0f,
                    0f,
                    0f,
                    string.Empty,
                    string.Empty,//图层名称
                    1d,
                    0d,
                    0d,
                    0d,
                    string.Empty,
                    string.Empty);
            }
        }

       
        //计算子项目部件的合计金额 更新项目金额
        private void FreshSubProjectTotalPrice()
        {
            double sum = 0;
            for (int i = 0; i < PARTSSource.Count; i++)
            {
                var row = ((DataRowView)PARTSSource[i]).Row as DataSet1.PARTSRow;
                if (row != null && row.RowState != DataRowState.Detached && row.RowState != DataRowState.Deleted)
                {
                    sum += row.Is金额Null() ? 0 : row.金额;
                }
            }
            //更新项目金额
            if (XMBBindingSource.Current != null)
            {
                var zbRow = ((DataRowView)XMBBindingSource.Current).Row as DataSet1.XMBRow;
                if (zbRow != null && zbRow.统计标志 == "1")
                    zbRow.合计金额 = sum;
            }
        }
        //计算项目的合计金额 更新工程金额
        private void CaculateTotalPrice()
        {
            double sum = 0;
            for (int i = 0; i < XMBBindingSource.Count; i++)
            {
                var row = ((DataRowView)XMBBindingSource[i]).Row as DataSet1.XMBRow;
                if (row != null && row.RowState != DataRowState.Detached && row.RowState != DataRowState.Deleted && row.统计标志 == "1")
                {
                    sum += row.Is合计金额Null() ? 0 : row.合计金额;
                }
            }
            var gcRow = ((DataRowView)gCBDataSource.Current).Row as DataSet1.GCBRow;
            if (gcRow != null)
            {
                gcRow.BeginEdit();
                gcRow.税前合计 = sum;
                gcRow.EndEdit();
            }
            //CaculateFloorSum(false);
        }
        //计算楼层的合计金额
        private void CaculateFloorSum(bool append)
        {
            var floorSum = new Dictionary<string, double>();
            for (int i = 0; i < XMBBindingSource.Count; i++)
            {
                var row = ((DataRowView)XMBBindingSource[i]).Row as DataSet1.XMBRow;
                if (row != null && row.RowState != DataRowState.Detached && row.RowState != DataRowState.Deleted && row.统计标志 == "1")
                {
                    if (!floorSum.ContainsKey(row.楼层))
                        floorSum.Add(row.楼层, 0);
                    floorSum[row.楼层] += row.合计金额;
                }
            }
            foreach (var dict in floorSum)
            {
                var floor = dataSet1.XMB.Select("楼层='" + dict.Key + "' and  统计标志 = '0'");
                if (floor.Length < 1)
                {
                    if (append)
                        dataSet1.XMB.AddXMBRow(gCBTableAdapter.GetSubProjectKey(), ProjectKey, string.Empty, string.Empty, string.Format("{0}层合计", dict.Key), dict.Value,"0", dict.Key,"101");
                }
                else
                {
                    floor[0]["合计金额"] = dict.Value;
                }
            }
        }

        private void OnSubDetailCurrentChanged(object sender, EventArgs e)
        {

        }
        //删除明细项
        private void OnDeleteSubDetailButtonClick(object sender, EventArgs e)
        {
            if (PARTSSource.Current != null)
                PARTSSource.RemoveCurrent();
        }
        //添加明细项
        private void OnAddSubDetailButtonClick(object sender, EventArgs e)
        {

        }
        //删除项目同时删除项目明细
        private void OnDeleteSubProjectButtonClick(object sender, EventArgs e)
        {
            if (Utility.Ask("确定要删除吗？"))
            {
                if (XMBBindingSource.Current != null)
                {
                    var row = ((DataRowView)XMBBindingSource.Current).Row as DataSet1.XMBRow;
                    if (row != null)
                    {
                        foreach (var item in dataSet1.PARTS.Select("项目关键字='" + row.项目关键字 + "'"))
                        {
                            item.Delete();
                        }
                        row.Delete();
                    }
                }
            }
        }
        //计算税金和含税额
        private void CaculateOverallMoney(object sender, EventArgs e)
        {
            var gcRow = ((DataRowView)gCBDataSource.Current).Row as DataSet1.GCBRow;
            if (gcRow != null)
            {
                gcRow.税金 = gcRow.税前合计 * gcRow.税率 / 100;
                gcRow.总计 = gcRow.税金 + gcRow.税前合计;
            }
        }
        //实现两个DataGridView之间互动
        private void OnSubProjectCurrentChange(object sender, EventArgs e)
        {
            var rowView = XMBBindingSource.Current as DataRowView;
            if (rowView != null)
            {
                var row = rowView.Row as DataSet1.XMBRow;
                if (row != null)
                {
                    PARTSSource.Filter = string.Format("项目关键字='{0}'", row.项目关键字);
                }
            }

        }

        private void OnSubProjectRowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid == null || grid.DataSource == null)
                return;
            var source = grid.DataSource as BindingSource;
            if (source == null || source.Current == null)
                return;
            var row = source.Current as DataRowView;
            if (row == null)
                return;
            var rowItem = row.Row as DataSet1.XMBRow;
            if (rowItem == null)
                return;
            if (rowItem.Is项目名称Null() || string.IsNullOrEmpty(rowItem.项目名称))
            {
                if (rowItem.RowState == DataRowState.Added)
                    rowItem.Delete();
                else
                    MessageBox.Show("请输入项目名称!");
            }
            FreshSubProjectTotalPrice();
        }

        private void OnSubProjectRowValidated(object sender, DataGridViewCellEventArgs e)
        {
            CaculateTotalPrice();
        }

        private void OnCreateFloorSumButtonClick(object sender, EventArgs e)
        {
            CaculateFloorSum(true);
        }

        private void OnSubDetailRowValidated(object sender, DataGridViewCellEventArgs e)
        {
            FreshSubProjectTotalPrice();
            CaculateTotalPrice();
        }
        private void OnSubDetailRowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid == null || grid.DataSource == null)
                return;
            var source = grid.DataSource as BindingSource;
            if (source == null || source.Current == null)
                return;
            var row = source.Current as DataRowView;
            if (row == null)
                return;
            var rowItem = row.Row as DataSet1.PARTSRow;
            if (rowItem == null)
                return;

            if (rowItem.Is部件名称Null() || string.IsNullOrEmpty(rowItem.部件名称))
            {
                if (rowItem.RowState == DataRowState.Added)
                    rowItem.Delete();
                else
                    MessageBox.Show("请输入部件名称");
            }
            if (rowItem.RowState != DataRowState.Detached && rowItem.RowState != DataRowState.Deleted)
                rowItem.金额 = rowItem.数量 * rowItem.单价;
        }
        private void OnTaxRateChanged(object sender, EventArgs e)
        {
            CaculateTotalPrice();
            CaculateOverallMoney(null, null);
        }
        //导入板材
        private void button6_Click(object sender, EventArgs e)
        {           
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                OleDbHelper.GetExcelTables("");
            }
        }
        //导入五金
        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
            }
        }
    }
}
