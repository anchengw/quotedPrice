using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using quotedPrice.DataSet1TableAdapters;

namespace quotedPrice
{
    public partial class FormPrint : Form
    {
        string moban = "";
        public DataSet1.GCBRow ProjectInfo { get; set; }
        private List<chestInfo> chestList = new List<chestInfo>();
        public FormPrint()
        {
            InitializeComponent();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            string html ="";
            if (radioButton1.Checked)
            {
                moban = Application.StartupPath + "\\moban\\criterionPrice.xlsx";
                html = Office2HtmlHelper.ActionResult(moban);
                
            }
            if(radioButton2.Checked)
            {
                moban = Application.StartupPath + "\\moban\\WankePrice.xlsx";
                html = Office2HtmlHelper.ActionResult(moban);
            }
            webBrowser1.Navigate(html);
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
                TemplateEngine.resolveExcel(moban, saveFileDig.FileName, chestList);
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
            radioButton1.Checked = true;
            moban = Application.StartupPath + "\\moban\\criterionPrice.xlsx";
            int i = projectDetailTableAdapter1.GetProjectDetails(ProjectInfo.工程关键字, dataSet11.ProjectDetail);
            //IList<chestInfo> users = ModelConvertHelper<chestInfo>.ConvertToModel(dataSet11.ProjectDetail);
           chestList = ConvertToList(dataSet11.ProjectDetail);
        }
    }
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
