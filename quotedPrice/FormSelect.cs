using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace quotedPrice
{
    public partial class FormSelect : Form
    {
        string conn = @"User ID=sa;Password=knfz@2013;Initial Catalog=luckyhouse;Data Source=192.168.1.15;";
        string id = "";
        string guizi = "";
        public DataTable gdt = null;
        public FormSelect()
        {
            InitializeComponent();
            listView1.AllowDrop = true;
            listView1.FullRowSelect = true;
            
            listViewBing();
        }
        
        private DataTable ExeSQL(string conn, string sql)
         {
            DataTable dt = new DataTable();
            using (SqlConnection sqlCnt = new SqlConnection(conn))
            {
                try
                {
                    sqlCnt.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlCnt);
                     adapter.Fill(dt);
                    return dt;
                }
                catch(Exception EX)
                {
                    return null;
                }
            }
        }
        private void listViewBing()
        {
            string sqlstr = @"select distinct a.id,a.[工程名称],b.[柜体名称] from[五金配置表_主表] a inner join[五金配置表_明细] b on a.id = b.id";
            
            DataTable dt = ExeSQL(conn, sqlstr);
            if (dt.Rows.Count == 0)
                return;
            this.listView1.BeginUpdate();
            foreach (DataRow dr in dt.Rows)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Tag = dr["id"].ToString();
                lvi.Text = dr["工程名称"].ToString();
                lvi.SubItems.Add(dr["柜体名称"].ToString());
                listView1.Items.Add(lvi);
            }
            this.listView1.EndUpdate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
              if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(guizi))
            {
                string sqlstr = @"select [iOrd],[柜体名称],[U8编码],[部品],[描述],[型号],[颜色],[规格/mm],[品牌],[数量],[单位],[标准单价] from [五金配置表_明细] where id = '{key}' and [柜体名称] = '{guizi}'";
                sqlstr = sqlstr.Replace("{key}", id).Replace("{guizi}", guizi);
                 gdt = ExeSQL(conn, sqlstr);
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("请选择要导入的五金");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                id = listView1.SelectedItems[0].Tag.ToString();
                string gongcheng = listView1.SelectedItems[0].Text;
                guizi = listView1.SelectedItems[0].SubItems[1].Text;
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(guizi))
            {
                string sqlstr = @"select [iOrd],[柜体名称],[U8编码],[部品],[描述],[型号],[颜色],[规格/mm],[品牌],[数量],[单位],[标准单价] from [五金配置表_明细] where id = '{key}' and [柜体名称] = '{guizi}'";
                sqlstr = sqlstr.Replace("{key}", id).Replace("{guizi}", guizi);
                gdt = ExeSQL(conn, sqlstr);
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
