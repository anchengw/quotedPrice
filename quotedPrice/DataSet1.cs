using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace quotedPrice
{

    //GcysConnectionString
    partial class DataSet1
    {

    }
}
namespace quotedPrice.DataSet1TableAdapters
{
    partial class ProjectDetailTableAdapter
    {
        public int GetProjectDetails(string []projectKey, DataSet1.ProjectDetailDataTable table)
        {
            //WHERE GCB.工程关键字 in (@ProjectKey)
            string strSql =
                @"SELECT   GCB.工程名称, GCB.客户名称, GCB.总计 AS 工程总计, XMB.项目名称, XMB.合计金额 AS 项目总计, PARTS.部件名称, 
                PARTS.长度, PARTS.宽度, PARTS.厚度, PARTS.单位, PARTS.数量, PARTS.成型尺寸l1, PARTS.成型尺寸l2, 
                PARTS.成型面积, PARTS.材料, PARTS.图层名称, PARTS.单价, PARTS.金额, PARTS.标准单价, PARTS.标准金额, 
                GCB.创建日期
                FROM      ((GCB INNER JOIN
                XMB ON GCB.工程关键字 = XMB.工程关键字) INNER JOIN
                PARTS ON GCB.工程关键字 = PARTS.工程关键字 AND XMB.项目关键字 = PARTS.项目关键字)
                WHERE GCB.工程关键字 in (@ProjectKey)
                ORDER BY XMB.序号";
            string key = String.Join("','", projectKey);
            key = "'" + key + "'";
            strSql = strSql.Replace("@ProjectKey", key);
            var comm = new OleDbCommand(strSql, Connection);          
            //comm.Parameters.Add(new OleDbParameter("@ProjectKey", OleDbType.WChar, 10) { Value = key });
            Adapter.SelectCommand = comm;
            if (ClearBeforeFill)
                table.Clear();
            return Adapter.Fill(table);
        }
    }
    public partial class GCBTableAdapter
    {       
        public int GetProject(string projectKey, DataSet1.GCBDataTable table)
        {
            Adapter.SelectCommand = new OleDbCommand("select * from GCB Where [工程关键字] = :ProjectKey"
                , Connection);
            Adapter.SelectCommand.Parameters.Add(":ProjectKey", OleDbType.WChar, 10);
            Adapter.SelectCommand.Parameters[0].Value = projectKey;
            if (this.ClearBeforeFill)
                table.Clear();
            return Adapter.Fill(table);
        }
        //产生工程关键字
        public string GetNewProjectKey()
        {
            var comm = new OleDbCommand("SELECT COUNT(1) + 1 AS NewCount FROM GCB WHERE (DATEDIFF('d', [创建日期], NOW) = 0)", Connection);
            Connection.Open();
            var o = comm.ExecuteScalar();
            Connection.Close();
            return string.Format("{0}{1:X2}", DateTime.Now.ToString("yyyyMMdd"), o);
        }
        //取项目表的工程关键字
        public int GetSubProjects(string projectKey, DataSet1.XMBDataTable table)
        {
            Adapter.SelectCommand = new OleDbCommand("select * from XMB Where [工程关键字] = :ProjectKey order by [序号]"
                , Connection);
            Adapter.SelectCommand.Parameters.Add(":ProjectKey", OleDbType.WChar, 10);
            Adapter.SelectCommand.Parameters[0].Value = projectKey;
            if (this.ClearBeforeFill)
                table.Clear();
            return Adapter.Fill(table);
        }
        //取部件表的工程关键字
        public int GetSubProjectsDetail(string projectKey, DataSet1.PARTSDataTable table)
        {
            Adapter.SelectCommand = new OleDbCommand("select * from PARTS Where [工程关键字] = :ProjectKey order by [序号]"
                , Connection);
            Adapter.SelectCommand.Parameters.Add(":ProjectKey", OleDbType.WChar, 10);
            Adapter.SelectCommand.Parameters[0].Value = projectKey;
            if (this.ClearBeforeFill)
                table.Clear();
            return Adapter.Fill(table);
        }
        //产生项目关键字
        public string GetSubProjectKey()
        {
            var rand = new Random();
            var comm = new OleDbCommand("select 1 from XMB where [项目关键字] = :SubKey", Connection);
            var param = comm.CreateParameter();
            comm.Parameters.Add(param);
            param.ParameterName = ":SubKey";
            param.OleDbType = OleDbType.WChar;
            param.Size = 10;
            Connection.Open();
            string str = string.Empty;
            while (true)
            {
                long d = long.MaxValue;
                while (d > 0xFFFFFFFFFF)
                {
                    d = (long)(rand.NextDouble() * 10e13);
                }
                str = d.ToString("X10");

                param.Value = str;
                using (var dr = comm.ExecuteReader())
                {
                    if (!dr.HasRows)
                        break;
                }
            }
            Connection.Close();
            return str;
        }
        //生成子项目（部件）关键字
        public string GetNewSubProjectDetailId()
        {
            var rand = new Random();
            var comm = new OleDbCommand("select 1 from PARTS where [关键字] = :SubKey", Connection);
            var param = comm.CreateParameter();
            comm.Parameters.Add(param);
            param.ParameterName = ":SubKey";
            param.OleDbType = OleDbType.WChar;
            param.Size = 10;
            Connection.Open();
            string str = string.Empty;
            while (true)
            {
                long d = long.MaxValue;
                while (d > 0xFFFFFFFFFF)
                {
                    d = (long)(rand.NextDouble() * 10e13);
                }
                str = d.ToString("X10");

                param.Value = str;
                using (var dr = comm.ExecuteReader())
                {
                    if (!dr.HasRows)
                        break;
                }
            }
            Connection.Close();
            return str;
        }
        public bool DeleteProject(ArrayList ALSql)
        {
            if (Connection.State != ConnectionState.Open)
            {
                try
                {
                    Connection.Open();
                }
                catch
                {
                    throw new Exception("数据库无法连接");
                }
            }
            bool state = false;
            OleDbTransaction transaction = null;
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                transaction = Connection.BeginTransaction();
                cmd.Transaction = transaction;
                cmd.Connection = Connection;
                cmd.CommandType = CommandType.Text;
                for (int i = 0; i < ALSql.Count; i++)
                {
                    cmd.CommandText = ALSql[i].ToString();
                    cmd.ExecuteNonQuery();
                }
                transaction.Commit();
                state = true;
            }
            catch
            {
                state = false;
                transaction.Rollback();
            }
            finally
            {
                Connection.Close();
            }
            return state;
        }
        public int DeleteProject(DataSet1.GCBRow row)
        {
            var comm = new OleDbCommand("Delete from PARTS where [工程关键字] = :ProjectKey", Connection);
            var comm2 = new OleDbCommand("Delete from XMB where  [工程关键字] = :ProjectKey", Connection);
            var param = new OleDbParameter(":ProjectKey", OleDbType.WChar, 10) { Value = row.工程关键字 };
            var param2 = new OleDbParameter(":ProjectKey", OleDbType.WChar, 10) { Value = row.工程关键字 };
            comm.Parameters.Add(param);
            comm2.Parameters.Add(param2);
            Connection.Open();
            comm2.ExecuteNonQuery();
            comm.ExecuteNonQuery();
            Connection.Close();
            row.Delete();
            return Update(row);
        }

        public int SearchProject(string strSql, DataSet1.GCBDataTable table)
        {
            Adapter.SelectCommand = new OleDbCommand(strSql, Connection);
            if (this.ClearBeforeFill)
                table.Clear();
            return Adapter.Fill(table);
        }
    }
}
