using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using quotedPrice.DataSet1TableAdapters;

namespace quotedPrice
{
    public partial class FormPrint : frmBase
    {
        string moban = "";
        public String[] ProjectKey { get; set;}
        private List<chestInfo> chestList = new List<chestInfo>();
        public FormPrint()
        {
            InitializeComponent();
        }
        private void loadReport()
        {
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.Reset();
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            if (radioButton1.Checked)
            {
                this.reportViewer1.LocalReport.ReportPath = "Report1.rdlc";
               
            }
            else
                this.reportViewer1.LocalReport.ReportPath = "Report2.rdlc";
            projectDetailTableAdapter1.GetProjectDetails(ProjectKey, dataSet11.ProjectDetail);
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", (DataTable)dataSet11.ProjectDetail));//要和设计报表时指定的名称一致，这里是DataSet1

            //List<ReportParameter> para = new List<ReportParameter>();
            //para.Add(new ReportParameter("guizi", new string[] { "会议室装修" }));
            ReportParameter[] reportParam = new ReportParameter[1];
            reportParam[0] = new ReportParameter("guizi", "会议室装修", true);
            this.reportViewer1.LocalReport.SetParameters(reportParam);


            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.ZoomPercent = 100;
            reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            loadReport();
            //string html ="";
            /*
            if (radioButton1.Checked)
            {
                reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.ReportPath = "Report1.rdlc";
                projectDetailTableAdapter1.GetProjectDetails(ProjectKey, dataSet11.ProjectDetail);
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", (DataTable)dataSet11.ProjectDetail));//要和设计报表时指定的名称一致，这里是DataSet1

                List<ReportParameter> para = new List<ReportParameter>();
                para.Add(new ReportParameter("guizi", new string[] { "会议室装修" }));
                this.reportViewer1.LocalReport.SetParameters(para);
                //moban = Application.StartupPath + "\\moban\\criterionPrice.xlsx";
                //html = Office2HtmlHelper.ActionResult(moban);

            }
            if(radioButton2.Checked)
            {
                this.reportViewer1.LocalReport.ReportPath = "Report2.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ProjectDetailBindingSource));
                List<ReportParameter> para = new List<ReportParameter>();
                para.Add(new ReportParameter("guizi", new string[] { "会议室装修" }));
                this.reportViewer1.LocalReport.SetParameters(para);
                //moban = Application.StartupPath + "\\moban\\WankePrice.xlsx";
                //html = Office2HtmlHelper.ActionResult(moban);
            }
            */
        }
        //直接安静导出EXCEL
        private void directExportExcel(string filePath)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = reportViewer1.LocalReport.Render(
               "Excel", null, out mimeType, out encoding,
                out extension,
               out streamids, out warnings);

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
            if (MessageBox.Show("报表打印：\r\n 成功导出Excel文件: " + filePath + "\r\n 要现在打开文件: " + filePath + "吗？","信息",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(filePath);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDig = new SaveFileDialog();
            saveFileDig.Filter = "Excel Office97-2003(*.xls)|.xls|Excel Office2007及以上(*.xlsx)|*.xlsx";
            saveFileDig.FilterIndex = 0;
            saveFileDig.OverwritePrompt = true;
            saveFileDig.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (saveFileDig.ShowDialog() == DialogResult.OK)
            {
                directExportExcel(saveFileDig.FileName);
                //TemplateEngine.resolveExcel(moban, saveFileDig.FileName, chestList);
            }
        }
        public static List<chestInfo> ConvertToList(DataTable table)
        {
            if (table == null || table.Rows.Count == 0)
            {
                return null;
            }

            List<chestInfo> rows = new List<chestInfo>();
            int i = 1;
            foreach (DataRow row in table.Rows)
            {
                chestInfo chest = new chestInfo();
                chest.IncNum = i;
                chest.ChestTitle = row["工程名称"].ToString();
                chest.Group = row["项目名称"].ToString();
                chest.Parts = row["部件名称"].ToString();
                chest.Materials = row["材料"].ToString();
                chest.Color = row["颜色"].ToString();
                chest.Brand = row["品牌"].ToString();
                chest.H = row["长度"].ToString();
                chest.W = row["宽度"].ToString();
                chest.D = row["厚度"].ToString();
                chest.Count = row["数量"].ToString();
                chest.Unit = row["单位"].ToString();
                chest.Area = row["成型面积"].ToString();
                chest.Price = row["单价"].ToString();
                chest.Amount = row["金额"].ToString();
                chest.Remark = row["备注"].ToString();
                chest.Rprice = row["标准单价"].ToString();
                chest.Ramount = row["标准金额"].ToString();
                chest.total = row["项目总计"].ToString();
                rows.Add(chest);
            }

            return rows;
        }

        private void FormPrint_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“DataSet1.ProjectDetail”中。您可以根据需要移动或删除它。
            this.ProjectDetailTableAdapter.Fill(this.DataSet1.ProjectDetail);
            radioButton1.Checked = true;
            loadReport();
        }
    }
    ////IList<chestInfo> users = ModelConvertHelper<chestInfo>.ConvertToModel(dataSet11.ProjectDetail);
    public class ModelConvertHelper<T> where T : new()
    {
        public static IList<T> ConvertToModel(DataTable dt)
        {
            // 定义集合    
            IList<T> ts = new List<T>();

            // 获得此模型的类型   
            Type type = typeof(T);
            string tempName = "";

            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性      
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;  // 检查DataTable是否包含此列    

                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter      
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts;
        }
    }
}
