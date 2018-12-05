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
    public partial class FormSelectB : Form
    {
        string conn = @"User ID=sa;Password=knfz@2013;Initial Catalog=luckyhouse;Data Source=192.168.1.15;";
        List<String> strList = new List<string>();
        public DataTable gdt = null;
        public FormSelectB()
        {
            InitializeComponent();
            listView1.AllowDrop = true;
            listView1.FullRowSelect = true;
            listView1.MultiSelect = true;
            listView2.AllowDrop = true;
            listView2.FullRowSelect = true;
            listView2.MultiSelect = true;

            this.gcbTableAdapter1.Fill(this.dataSet11.GCB);
            listViewBing();
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            List<ListViewItem> lvilist = (List<ListViewItem>)e.Data.GetData(typeof(List<ListViewItem>));
            foreach (ListViewItem lvi in lvilist)
            {
                listView2.Items.Remove(lvi);
                listView1.Items.Add(lvi);
            }
        }

        private void listView2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void listView2_DragDrop(object sender, DragEventArgs e)
        {
            List<ListViewItem> lvilist = (List <ListViewItem>)e.Data.GetData(typeof(List<ListViewItem>));
            foreach (ListViewItem lvi in lvilist)
            {
                listView1.Items.Remove(lvi);
                listView2.Items.Add(lvi);
            }
        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            ListView lv = (ListView)sender;
            if (lv.SelectedItems.Count > 0)
            {
                List<ListViewItem> lvilist = new List<ListViewItem>();
                lvilist.Clear();
                foreach (ListViewItem lvi in lv.SelectedItems)
                {
                    lvilist.Add(lvi);
                }
                this.DoDragDrop(lvilist, DragDropEffects.Move);
            }
        }

        private void listView2_ItemDrag(object sender, ItemDragEventArgs e)
        {
            ListView lv = (ListView)sender;
            if (lv.SelectedItems.Count > 0)
            {
                List<ListViewItem> lvilist = new List<ListViewItem>();
                foreach (ListViewItem lvi in lv.SelectedItems)
                {
                    lvilist.Add(lvi);
                }
                this.DoDragDrop(lvilist, DragDropEffects.Move);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                foreach (ListViewItem lvi in listView1.SelectedItems)
                {
                    listView1.Items.Remove(lvi);
                    listView2.Items.Add(lvi);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView1.Items)
            {
                listView1.Items.Remove(lvi);
                listView2.Items.Add(lvi);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                foreach (ListViewItem lvi in listView2.SelectedItems)
                {
                    listView2.Items.Remove(lvi);
                    listView1.Items.Add(lvi);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView2.Items)
            {
                listView2.Items.Remove(lvi);
                listView1.Items.Add(lvi);
            }
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
                catch
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
            strList.Clear();
            foreach(ListViewItem lvi in listView2.Items)
            {
                strList.Add(lvi.Tag.ToString());
            }
            string sqlstr = @"select ,[iOrd],[柜体名称],[U8编码],[部品],[描述],[型号],[颜色],[规格/mm],[品牌],[数量],[单位],[标准单价] from [五金配置表_明细] where id in ({key})";
            string key = String.Join("','", strList.ToArray());
            key = "'" + key + "'";
            sqlstr = sqlstr.Replace("{key}", key);
            gdt = ExeSQL(conn, sqlstr);
            this.DialogResult = DialogResult.OK;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
