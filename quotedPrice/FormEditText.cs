using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace quotedPrice
{
    public partial class FormEditText : frmBase
    {
        public FormEditText()
        {
            InitializeComponent();
            
        }

        public string TextValue
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }
        public BindingSource dataSource{ get; set; }
        private void OnSaveButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void OnCancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void FormEditText_Load(object sender, EventArgs e)
        {
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dataSource, "备注", true));
        }
    }
}
