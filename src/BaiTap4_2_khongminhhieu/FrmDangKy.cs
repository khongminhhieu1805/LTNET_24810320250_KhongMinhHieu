
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BaiTap4_2_DangKyHocVien
{
    public partial class FrmDangKy : Form
    {
        
        private string connectionString = @"Data Source=WINDOWS-PC\SQLEXPRESS;Initial Catalog=QuanLyDaoTao;Integrated Security=True";

        public FrmDangKy()
        {
            InitializeComponent();
        }

        private void FrmDangKy_Load(object sender, EventArgs e)
        {
            LoadCourses();
        }

        
        private void LoadCourses()
        {
            string query = "SELECT CourseId, CourseName FROM Courses";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboCourse.DataSource = dt;
                    cboCourse.DisplayMember = "CourseName";
                    cboCourse.ValueMember = "CourseId";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải danh sách khóa học: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên học viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            
            if (cboCourse.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khóa học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string gioiTinh = radNam.Checked ? "Nam" : "Nữ";
            string courseId = cboCourse.SelectedValue.ToString();

            
            string insertQuery = @"INSERT INTO Students (FullName, Gender, BirthDate, Phone, CourseId) 
                                   VALUES (@FullName, @Gender, @BirthDate, @Phone, @CourseId)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@FullName", txtHoTen.Text.Trim());
                    cmd.Parameters.AddWithValue("@Gender", gioiTinh); cmd.Parameters.AddWithValue("@BirthDate", dtpBirthDate.Value.Date);
                    cmd.Parameters.AddWithValue("@Phone", mtxtPhone.Text.Trim());
                    cmd.Parameters.AddWithValue("@CourseId", courseId);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            string thongTin = "Họ tên: " + txtHoTen.Text.Trim() + "\n"
                                + "Giới tính: " + gioiTinh + "\n"
                                + "Ngày sinh: " + dtpBirthDate.Value.ToString("dd/MM/yyyy") + "\n"
                                + "SĐT: " + mtxtPhone.Text.Trim() + "\n"
                                + "Khóa học: " + cboCourse.Text + " (" + courseId + ")\n"
                                + "Học online: " + (chkOnline.Checked ? "Có" : "Không");

                            MessageBox.Show("Đăng ký và lưu CSDL thành công!\n\n" + thongTin, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            
                            txtHoTen.Clear();
                            mtxtPhone.Clear();
                            txtHoTen.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi lưu vào CSDL: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}