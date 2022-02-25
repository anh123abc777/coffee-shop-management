using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanLySinhVien
{
    public partial class Login : Form
    {
        private SqlConnection conn;
        private SqlCommand commd;
        public Login()
        {
            InitializeComponent();
    
        }
        void loadData()
        {
            try
            {
                conn = new SqlConnection("Server=DESKTOP-TP0I2O9\\SQLEXPRESS; Database = QLCF;" +
                        "uid=sa;pwd=sa2008");
                conn.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối");
            }
           
        }
        void checkLogin()
        {
            commd = new SqlCommand("select * from taikhoan",conn);
            SqlDataReader dr = commd.ExecuteReader();
            bool isExist = false;
            while (dr.Read())
            {
                if (txtuser.Text.Equals(dr.GetString(0)) && txtpassword.Text.Equals(dr.GetString(1)))
                {
                    isExist = true;
                    if (dr.GetBoolean(2))
                    {
                        new Order(dr.GetString(0),true).Visible = true;
                        this.Hide();
                        break;
                    }
                    else
                    {
                        new Order(dr.GetString(0),false).Visible = true;
                        this.Hide();
                        break;
                    }
                    
                }
            }
            dr.Close();
            if (!isExist)
            {
                MessageBox.Show("Tài khoản không tồn tại hoặc sai mật khẩu, tài khoản");
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkLogin();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
