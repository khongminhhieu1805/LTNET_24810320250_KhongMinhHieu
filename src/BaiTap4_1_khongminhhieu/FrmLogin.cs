
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BaiTap4_1_LoginForm
{
    public partial class FrmLogin : Form
    {
        
        private string connectionString = @"Data Source=WINDOWS-PC\SQLEXPRESS;Initial Catalog=QuanLyDaoTao;Integrated Security=True";

        public FrmLogin()
        {
            InitializeComponent();
            this.AcceptButton = btnLogin; 
            this.CancelButton = btnExit;   
            txtPassword.UseSystemPasswordChar = true; 
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            
            if (chkShowPassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                errorProvider.SetError(txtUsername, "Vui lòng nhập tên đăng nhập");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider.SetError(txtPassword, "Vui lòng nhập mật khẩu");
                return;
            }

            
            string query = "SELECT FullName FROM Users WHERE Username = @Username AND Password = @Password";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());

                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            MessageBox.Show("Đăng nhập thành công!\nXin chào: " + result.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}