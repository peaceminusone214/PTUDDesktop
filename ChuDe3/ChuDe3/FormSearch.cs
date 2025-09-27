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
    public partial class FormSearch : Form
    {
        public string MSSV { get; private set; }
        public string Ten { get; private set; }
        public string Lop { get; private set; }

        public FormSearch(List<string> dsLop)
        {
            InitializeComponent();
            cbLop.Items.Add(""); // Cho phép bỏ trống (tìm tất cả lớp)
            cbLop.Items.AddRange(dsLop.ToArray());
            cbLop.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            MSSV = txtMSSV.Text.Trim();
            Ten = txtTen.Text.Trim();
            Lop = cbLop.Text;   // Lấy từ ComboBox
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
