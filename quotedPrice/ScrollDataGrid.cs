using System.ComponentModel;
using System.Windows.Forms;

namespace ProjectBudget
{
    public partial class ScrollDataGrid : DataGridView
    {
        public ScrollDataGrid()
        {
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
        }

        public ScrollDataGrid(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if(SelectedRows.Count > 0)
            {
                var srn = SelectedRows[0].Index;
                srn += e.Delta < 0 ? 1 : -1;
                if (srn > Rows.Count - 1) srn = Rows.Count - 1;
                if (srn < 0) srn = 0;
                SelectedRows[0].Selected = false;
                Rows[srn].Selected = true;
                Rows[srn].Cells[0].Selected = true;
            }
        }
    }
}
