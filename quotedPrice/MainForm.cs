using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace quotedPrice
{
    public partial class MainForm : frmBase
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private const int WM_GETTEXT = 0xD;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_GETTEXT)
            {
                if (!(mainToolStrip.Items[0] is ToolStripButton))
                    mainToolStrip.Items[0].Visible = false;
            }
            base.WndProc(ref m);
        }
        private void ShowMdiChild(Type formType)
        {
            foreach (var child in MdiChildren)
            {
                if (child.GetType().Equals(formType))
                {
                    child.Activate();
                    return;
                }
            }
            var form = Activator.CreateInstance(formType) as Form;
            if (form != null)
            {
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
        }
        private void btnProjectBudget_Click(object sender, EventArgs e)
        {
            ShowMdiChild(typeof(FormProjects));
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("确定要退出系统吗？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                Application.Exit();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ShowMdiChild(typeof(FormProjectsDetail));
        }
    }
}
