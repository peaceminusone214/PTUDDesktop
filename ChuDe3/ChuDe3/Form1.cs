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
    public partial class Form1 : Form
    {
        private StudentManager manager;
        private ContextMenuStrip contextMenuMonHoc;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var contextMenu = new ContextMenuStrip();
            contextMenu.RenderMode = ToolStripRenderMode.Professional;
            contextMenu.BackColor = Color.White;

            var deleteItem = new ToolStripMenuItem("Xoá sinh viên");
            deleteItem.Click += mnuXoa_Click;

            contextMenu.Items.Add(deleteItem);
            lvDssv.ContextMenuStrip = contextMenu;
            // Chọn loại storage bạn muốn
            manager = new StudentManager(new JsonStudentStorage("students.json"));
            // manager = new StudentManager(new TxtStudentStorage("students.txt"));
            // manager = new StudentManager(new XmlStudentStorage("students.xml"));
            LoadListView(manager.GetAll());

            // Cấu hình ListView
            lvDssv.View = View.Details;
            lvDssv.FullRowSelect = true;
            lvDssv.CheckBoxes = true;
            lvDssv.Columns.Add("MSSV", 80);
            lvDssv.Columns.Add("Họ tên lót", 120);
            lvDssv.Columns.Add("Tên", 80);
            lvDssv.Columns.Add("Ngày sinh", 90);
            lvDssv.Columns.Add("CMND", 90);
            lvDssv.Columns.Add("Địa chỉ", 150);
            lvDssv.Columns.Add("Giới tính", 70);
            lvDssv.Columns.Add("Lớp", 80);
            lvDssv.Columns.Add("SĐT", 90);
            lvDssv.Columns.Add("Môn học", 200);
        }
        private void LoadListView(List<Student> students)
        {
            lvDssv.Items.Clear();
            foreach (var st in students.Where(s => s != null))
            {
                var item = new ListViewItem(st.MSSV);
                item.SubItems.Add(st.HoTenLot);
                item.SubItems.Add(st.Ten);
                item.SubItems.Add(st.NgaySinh.ToString("dd/MM/yyyy"));
                item.SubItems.Add(st.CMND);
                item.SubItems.Add(st.DiaChi);
                item.SubItems.Add(st.GioiTinhNam ? "Nam" : "Nữ");
                item.SubItems.Add(st.Lop);
                item.SubItems.Add(st.SoDT);
                item.SubItems.Add(string.Join(",", st.MonHoc));

                // Gắn object Student vào Tag
                item.Tag = st;

                lvDssv.Items.Add(item);
            }
        }
        private Student GetStudentFromForm()
        {
            // Kiểm tra nhập đủ thông tin
            if (string.IsNullOrWhiteSpace(mtbMSSV.Text) ||
                string.IsNullOrWhiteSpace(txtHoTenLot.Text) ||
                string.IsNullOrWhiteSpace(txtTen.Text) ||
                string.IsNullOrWhiteSpace(mtbCMND.Text) ||
                string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                string.IsNullOrWhiteSpace(mtbSoDT.Text) ||
                cbLop.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return null;
            }

            // Lấy môn học
            var monHoc = new List<string>();
            foreach (var item in clbMonHoc.CheckedItems)
                monHoc.Add(item.ToString());

            return new Student
            {
                MSSV = mtbMSSV.Text,
                HoTenLot = txtHoTenLot.Text,
                Ten = txtTen.Text,
                NgaySinh = dtpNgaySinh.Value,
                CMND = mtbCMND.Text,
                DiaChi = txtDiaChi.Text,
                GioiTinhNam = rdNam.Checked,
                Lop = cbLop.SelectedItem.ToString(),
                SoDT = mtbSoDT.Text,
                MonHoc = monHoc
            };
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(mtbMSSV.Text) ||
                string.IsNullOrWhiteSpace(txtHoTenLot.Text) ||
                string.IsNullOrWhiteSpace(txtTen.Text) ||
                string.IsNullOrWhiteSpace(cbLop.Text) ||
                string.IsNullOrWhiteSpace(mtbCMND.Text) ||
                string.IsNullOrWhiteSpace(mtbSoDT.Text) ||
                clbMonHoc.CheckedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!");
                return;
            }

            var sv = GetStudentFromForm();
            manager.AddOrUpdate(sv);
            LoadListView(manager.GetAll());
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var st = GetStudentFromForm();
            if (st == null) return;

            manager.AddOrUpdate(st);
            LoadListView(manager.GetAll());
        }

        private void lvDssv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvDssv.SelectedItems.Count > 0)
            {
                var item = lvDssv.SelectedItems[0];
                var st = item.Tag as Student;
                if (st != null)
                {
                    mtbMSSV.Text = st.MSSV;
                    txtHoTenLot.Text = st.HoTenLot;
                    txtTen.Text = st.Ten;
                    dtpNgaySinh.Value = st.NgaySinh;
                    mtbCMND.Text = st.CMND;
                    txtDiaChi.Text = st.DiaChi;
                    rdNam.Checked = st.GioiTinhNam;
                    rdNu.Checked = !st.GioiTinhNam;
                    cbLop.Text = st.Lop;
                    mtbSoDT.Text = st.SoDT;

                    // clear chọn môn
                    for (int i = 0; i < clbMonHoc.Items.Count; i++)
                    {
                        clbMonHoc.SetItemChecked(i, st.MonHoc.Contains(clbMonHoc.Items[i].ToString()));
                    }
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void mnuXoa_Click(object sender, EventArgs e)
        {
            // Lấy danh sách item được chọn qua checkbox hoặc bôi xanh
            var itemsToDelete = new List<ListViewItem>();

            foreach (ListViewItem item in lvDssv.Items)
            {
                if (item.Checked || item.Selected)
                    itemsToDelete.Add(item);
            }

            if (itemsToDelete.Count == 0)
            {
                MessageBox.Show("Hãy chọn ít nhất một sinh viên cần xoá");
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xoá {itemsToDelete.Count} sinh viên?",
                "Xác nhận xoá",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                List<string> mssvList = new List<string>();
                foreach (var item in itemsToDelete)
                {
                    if (item.Tag is Student st)
                        mssvList.Add(st.MSSV);
                }

                // Gọi manager để xóa
                manager.Delete(mssvList);

                // Load lại danh sách
                LoadListView(manager.GetAll());
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            // Lấy danh sách lớp hiện có từ combobox Form1
            var dsLop = new List<string>();
            foreach (var item in cbLop.Items)
            {
                dsLop.Add(item.ToString());
            }

            using (var frm = new FormSearch(dsLop))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var result = manager.Search(
                        string.IsNullOrEmpty(frm.MSSV) ? null : frm.MSSV,
                        string.IsNullOrEmpty(frm.Ten) ? null : frm.Ten,
                        string.IsNullOrEmpty(frm.Lop) ? null : frm.Lop
                    );
                    LoadListView(result);
                }
            }
        }

        private void thêmMônToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new FormAddMonHoc())
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    string mon = f.MonHocMoi;
                    if (!string.IsNullOrWhiteSpace(mon))
                    {
                        if (!clbMonHoc.Items.Contains(mon))
                        {
                            clbMonHoc.Items.Add(mon);
                        }
                        else
                        {
                            MessageBox.Show("Môn học đã tồn tại.", "Thông báo");
                        }
                    }
                }
            }
        }

        private void xóaMônToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clbMonHoc.SelectedItem != null)
            {
                clbMonHoc.Items.Remove(clbMonHoc.SelectedItem);
            }
            else
            {
                MessageBox.Show("Hãy chọn môn muốn xóa.", "Thông báo");
            }
        }
    }
}
