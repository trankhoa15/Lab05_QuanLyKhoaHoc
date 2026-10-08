using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab05_QuanLySinhVien
{
    public partial class Form1 : Form
    {
        private Dictionary<string, decimal> dsKhoaHoc = new Dictionary<string, decimal>();
        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;
            btnDangKy.Click += btnDangKy_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnThoat.Click += btnThoat_Click;
            cboKhoaHoc.SelectedIndexChanged += TinhTongTien;
            numSoThang.ValueChanged += TinhTongTien;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "ĐĂNG KÝ KHÓA HỌC";

            dsKhoaHoc.Add("C# WinForms cơ bản", 800000m);
            dsKhoaHoc.Add("SQL Server cơ bản", 700000m);
            dsKhoaHoc.Add("Web Frontend cơ bản", 750000m);
            dsKhoaHoc.Add("Lập trình Python cơ bản", 650000m);

            foreach (var khoaHoc in dsKhoaHoc.Keys)
            {
                cboKhoaHoc.Items.Add(khoaHoc);
            }
            if (cboKhoaHoc.Items.Count > 0)
            {
                cboKhoaHoc.SelectedIndex = 0;
            }
            radOnline.Checked = true;
            numSoThang.Minimum = 1;
            numSoThang.Maximum = 12;
            numSoThang.Value = 1;
            CapNhatHienThiTongTien();
        }

        private void TinhTongTien(object sender, EventArgs e)
        {
            CapNhatHienThiTongTien();
        }


        private void CapNhatHienThiTongTien()
        {
            if (cboKhoaHoc.SelectedItem != null)
            {
                string tenKhoaHoc = cboKhoaHoc.SelectedItem.ToString();
                decimal hocPhiMotThang = dsKhoaHoc[tenKhoaHoc];
                decimal soThang = numSoThang.Value;

                decimal tongTien = hocPhiMotThang * soThang;

                lblTongTien.Text = $"{tongTien:N0} VNĐ";
            }
        }


        private void btnDangKy_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên học viên.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }

            if (cboKhoaHoc.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn một khóa học.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhoaHoc.Focus();
                return;
            }

            string hoTen = txtHoTen.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string ngaySinh = dtpNgaySinh.Value.ToString("dd/MM/yyyy");
            string khoaHoc = cboKhoaHoc.SelectedItem.ToString();
            string hinhThuc = radOnline.Checked ? "Online" : "Trực tiếp";
            string soThang = numSoThang.Value.ToString();
            string nhanEmail = chkNhanEmail.Checked ? "Có" : "Không";
            string tongTien = lblTongTien.Text;

            // Xây dựng nội dung thông báo[cite: 5]
            string thongBao = "PHIẾU ĐĂNG KÝ KHÓA HỌC\n";
            thongBao += "--------------------------------------\n";
            thongBao += $"Họ tên: {hoTen}\n";
            thongBao += $"Số điện thoại: {sdt}\n";
            thongBao += $"Ngày sinh: {ngaySinh}\n";
            thongBao += $"Khóa học: {khoaHoc}\n";
            thongBao += $"Hình thức học: {hinhThuc}\n";
            thongBao += $"Số tháng: {soThang}\n";
            thongBao += $"Nhận email thông báo: {nhanEmail}\n";
            thongBao += "--------------------------------------\n";
            thongBao += $"TỔNG TIỀN: {tongTien}";

            // Hiển thị phiếu đăng ký bằng MessageBox[cite: 5]
            MessageBox.Show(thongBao, "Thông tin đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 5.3. Nút Làm mới[cite: 5]
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            // Xóa họ tên và số điện thoại[cite: 5]
            txtHoTen.Clear();
            txtSDT.Clear();

            // Đưa ngày sinh về ngày hiện tại[cite: 5]
            dtpNgaySinh.Value = DateTime.Now;

            // Bỏ chọn nhận email[cite: 5]
            chkNhanEmail.Checked = false;

            // Chọn lại khóa học đầu tiên[cite: 5]
            if (cboKhoaHoc.Items.Count > 0)
                cboKhoaHoc.SelectedIndex = 0;

            // Chọn lại hình thức Online[cite: 5]
            radOnline.Checked = true;

            // Đưa số tháng về 1[cite: 5]
            numSoThang.Value = 1;

            // Đưa con trỏ về ô họ tên[cite: 5]
            txtHoTen.Focus();
        }

        // 5.4. Nút Thoát[cite: 5]
        private void btnThoat_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận trước khi thoát[cite: 5]
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Chỉ đóng Form nếu người dùng chọn Yes[cite: 5]
            if (result == DialogResult.Yes)
            {
                this.Close();
            }

        }
    }
}
