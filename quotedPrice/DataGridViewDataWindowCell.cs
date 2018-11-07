using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ProjectBudget
{
    public class DataGridViewDataWindowCell:DataGridViewTextBoxCell
    {
        public override void InitializeEditingControl(int rowIndex, object
              initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            DataGridViewDataWindowEditingControl dataWindowControl =
                DataGridView.EditingControl as DataGridViewDataWindowEditingControl;
            dataWindowControl.PopupGridAutoSize = false;
            DataGridViewDataWindowColumn dataWindowColumn=
                (DataGridViewDataWindowColumn)OwningColumn;

            dataWindowControl.sDisplayMember = dataWindowColumn.SDisplayMember;//以下3句必须放在datasource设置前面
            dataWindowControl.sDisplayField = dataWindowColumn.SDisplayField;
            dataWindowControl.sKeyWords = dataWindowColumn.SKeyWords;
            dataWindowControl.SearchSql = dataWindowColumn.SearchSql;
            dataWindowControl.Connection = dataWindowColumn.Connection;

            dataWindowControl.DataSource = dataWindowColumn.DataSource;

            dataWindowControl.Text = Value == DBNull.Value ? string.Empty : Value.ToString();
            dataWindowControl.RowFilterVisible = true;  //此句必须放在datasource设置后面
        }



        [Browsable(false)]
        public override Type EditType
        {
            get
            {
                return typeof(DataGridViewDataWindowEditingControl);
            }
        }
       
        public override Type ValueType
        {
            get
            {
                return typeof(string);
            }
        }
        private DataGridViewDataWindowEditingControl EditingDataWindow
        {
            get
            {
                DataGridViewDataWindowEditingControl dataWindowControl =
                    DataGridView.EditingControl as DataGridViewDataWindowEditingControl;

                return dataWindowControl;
            }
        }

       
    }

    
}
