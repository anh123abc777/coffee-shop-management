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
    public partial class Tonghop : Form
    {
        private SqlConnection conn;
        private SqlCommand commd;
        private String nameTable;
        SqlDataAdapter sqldata = null;
        //Đối tượng hiển thị dữ liệu lên Form
        DataTable datatable = null;
        public Tonghop(String nameTable)
        {
            InitializeComponent();
            this.nameTable = nameTable;
        }
        void connDatabase()
        {
            try
            {
                conn = new SqlConnection("Server=DESKTOP-TP0I2O9\\SQLEXPRESS; Database = QLCF;" +
                        "uid=sa;pwd=sa2008;MultipleActiveResultSets=true");
                conn.Open();
                
            }
            catch (Exception e)
            {

            }


        }
        void loadData()
        {

            try
            {
                sqldata = new SqlDataAdapter("select * from " + nameTable, conn);
                datatable = new DataTable();
                datatable.Clear();
                sqldata.Fill(datatable);
               
                //Đưa dữ liệu lên DataGridView
                this.dta.DataSource = datatable;

                dta.AllowUserToDeleteRows = true;
                //Thay đổi độ rộng cột
                dta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dta.AutoResizeColumns();
              

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối");
            }
        }
        private void DetailBill(object sender, EventArgs e)
        {
            string idBill;
            try
            {
               
                 idBill = dta.SelectedRows[dta.SelectedRows.Count - 1].Cells[0].FormattedValue.ToString();
            }
            catch
            {
                 idBill = dta.SelectedCells[dta.SelectedCells.Count - 1].OwningRow.Cells[0].FormattedValue.ToString();
            }
            string sql = "select * from Chitiethoadon where IDBill = '" + idBill + "'";
            new Tonghop("Chitiethoadon where IDBill = '" + idBill + "'").ShowDialog();


        }

        private void Form5_Load(object sender, EventArgs e)
        {
            connDatabase();
            loadData();
            if (!nameTable.Equals("loainuoc") && !nameTable.Equals("taikhoan") )
            {
                dta.ReadOnly = true;
                Button btnDetail = new Button();
                btnDetail.Text = "Chi Tiết";
                

                btnDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(106)))), ((int)(((byte)(113)))));
                btnDetail.FlatStyle = FlatStyle.Flat;
                btnDetail.Font = new System.Drawing.Font("Constantia", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btnDetail.ForeColor = System.Drawing.Color.White;

                btnDetail.UseVisualStyleBackColor = false;
                btnDetail.Name = "btnDetail";
                tableLayoutPanel1.ColumnCount = 4;
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0.25F));
                tableLayoutPanel1.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0.25F);
                tableLayoutPanel1.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0.25F);
                tableLayoutPanel1.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 0.25F);
                tableLayoutPanel1.Controls.Add(btnDetail, 0, 0);
                btnDetail.Size = new System.Drawing.Size(100, 36);
                btnDetail.Click += new EventHandler(DetailBill);
                //    btnSave.Text = "Chi Tiết";
                //   btnSave.Click -= btnSave_Click;
                //  btnSave.Click += new EventHandler(DetailBill);
                if (!nameTable.Equals("Hoadon"))
                {
                    btnSave.Text = "aduvjp";
                    tableLayoutPanel1.Controls.RemoveAt(3);
                    tableLayoutPanel1.Controls.RemoveAt(0);
                    tableLayoutPanel1.Controls.RemoveAt(0);
                    tableLayoutPanel1.ColumnCount = 1;
                    tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(tableLayoutPanel1.Width / 2 - tableLayoutPanel1.Controls[0].Width/2, 0, 0, 0); ;
                }

            }

        }

        private void dta_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void dta_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void dta_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String[] KeyNotDel = new string[] { };
            int numKeyNotDel = 0;
            commd = new SqlCommand("select * from " + nameTable, conn);
            SqlDataReader dr = commd.ExecuteReader();
            try
            {
                bool checkContain;
                while (dr.Read())
                {
                    checkContain = false;

                    foreach (DataGridViewRow rw in dta.Rows)
                    {

                        if (rw.Cells[0].FormattedValue.Equals(dr.GetString(0)))
                        {


                            checkContain = true;
                            break;
                        }
                        // if (checkContain)
                        //   break;
                    }
                    if (!checkContain)
                    {
                        commd = new SqlCommand("delete from " + nameTable +
                            " where " + dr.GetName(0) + "='" + dr.GetString(0) + "';", conn);
                        commd.ExecuteNonQuery();

                    }
                }
                dr.Close();
            }
            catch (Exception del)
            {
                //KeyNotDel[numKeyNotDel] = dr.GetString(0);
                //numKeyNotDel++;
                MessageBox.Show("Không thể bỏ trống, do có tồn tại thực đơn cho loại nước này");
            }

            foreach (DataGridViewRow row in dta.Rows)
            {
                //nếu bảng rỗng thì dừng 

                if(row.Index == dta.Rows.Count - 1)
                {
                    break;
                }

                //ô rỗng k thể update hay insert
                bool isEmptyCell = false;
                for (int i = 0; i < row.Cells.Count; i++)
                {
                
                    if (row.Cells[i].Value == null || row.Cells[i].Value == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[i].Value.ToString()))
                    {
                        if (row.Cells[i] is DataGridViewCheckBoxCell)
                        {
                            row.Cells[i].Value = false;
                        }
                        else
                        {

                            MessageBox.Show("Không thể bỏ trống khi thêm");
                            isEmptyCell = true;
                            break;
                        }
                        
                    }
                }


                if (!isEmptyCell)
                {


                    commd = new SqlCommand("select * from " + nameTable, conn);
                    dr = commd.ExecuteReader();
                    String[] key = new String[] { };
                    int indexKey = 0;
                    while (dr.Read())
                    {
                        key = new string[indexKey+1];
                        key[indexKey] = dr.GetString(0);
                        indexKey++;
                    }

                    String keyOfTable = (key.Length<row.Index)?key[row.Index]:""; 
                    //update
                    String sql = "update " + nameTable + " set ";
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        if (i != 0)
                        {
                            sql += ",";
                        }
                        sql += dr.GetName(i) + "=N'" + row.Cells[i].FormattedValue + "' ";
                    }
                    sql += " where " + dr.GetName(0) + "='" +row.Cells[0].FormattedValue + "';";
                    commd = new SqlCommand(sql, conn);
                    int numRowAffected =  commd.ExecuteNonQuery();
                    if (numRowAffected == 0)
                    {
                        //insert
                        
                        sql = "insert into " + nameTable + "(";
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            if (i != 0)
                            {
                                sql += ",";
                            }
                            sql += dr.GetName(i);
                        }
                        sql += ") values (";
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            if (i != 0) sql += ",";
                            sql += "N'" + row.Cells[i].FormattedValue + "'";
                        }
                        sql += ");";

                        commd = new SqlCommand(sql, conn);
                        commd.ExecuteNonQuery();

                        dr.Close();
                    }

                }
            }

            btnSave.Enabled = false;
            loadData();
        }
        private void updateAfterDelete()
        {
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
           
            loadData();
            btnSave.Enabled = false;
 

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
