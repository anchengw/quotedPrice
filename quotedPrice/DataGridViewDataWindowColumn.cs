using System;
using System.Data.Common;
using System.Windows.Forms;

namespace ProjectBudget
{
    public class DataGridViewDataWindowColumn:DataGridViewColumn
    {
        private object m_dataSoruce = null;
        private string sDisplayMember = "";
        private string sDisplayField = "";
        private string sKeyWords = "";
        private string searchSql = "";
        private DbConnection m_connection = null;


        public string SDisplayField
        {
            get { return sDisplayField; }
            set { sDisplayField = value; }
        }

        public string SDisplayMember
        {
            get { return sDisplayMember; }
            set { sDisplayMember = value; }
        }

        public string SKeyWords
        {
            get { return sKeyWords; }
            set { sKeyWords = value; }
        }


        public DataGridViewDataWindowColumn()
            : base(new DataGridViewDataWindowCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewDataWindowCell)))
                {
                    throw new InvalidCastException("²»ÊÇDataGridViewDataWindowCell");
                }
                
                base.CellTemplate = value; 
            }
        }
        private DataGridViewDataWindowCell ComboBoxCellTemplate
        {
            get
            {
                return (DataGridViewDataWindowCell)this.CellTemplate;
            }
        }
        public Object DataSource
        {
            get
            {
                return m_dataSoruce;

            }
            set
            {
                if (ComboBoxCellTemplate != value)
                {
             
                    m_dataSoruce = value;
                }
            }
        }

        public string SearchSql { get { return searchSql; } set { searchSql = value; } }

        public DbConnection Connection { get { return m_connection; } set { m_connection = value; } }
    }
}
