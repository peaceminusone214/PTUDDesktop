using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChuDe3
{
    public partial class FormAddMonHoc : Form
    {
        public string MonHocMoi { get; private set; }
        public FormAddMonHoc()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMonHoc.Text))
            {
                MessageBox.Show("Vui lòng nhập tên môn học.", "Thông báo");
                return;
            }
            MonHocMoi = txtMonHoc.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
