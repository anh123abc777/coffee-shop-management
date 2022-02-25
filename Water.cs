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
    public partial class Water : Form
    {
        private SqlConnection conn;
        private SqlCommand commd;
        public Water()
        {
            InitializeComponent();
        }
        void loadData()
        {
            try
            {
                conn = new SqlConnection("Server=DESKTOP-TP0I2O9\\SQLEXPRESS; Database = QLCF;" +
                        "uid=sa;pwd=sa2008;MultipleActiveResultSets=true");
                conn.Open();
            }
            catch(Exception e)
            {

            }
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            loadData();
            //menu item tat ca
            all.Click += new EventHandler(showWaterbyType);



            //cot loai san pham
            commd = new SqlCommand("select count(*) from loainuoc", conn);
            int numWaterType = Convert.ToInt32(commd.ExecuteScalar());
            string[] itemofLoai = new string[numWaterType];
            try
            {
                commd = new SqlCommand("select * from  loainuoc", conn);
                SqlDataReader dr = commd.ExecuteReader();
                int index = 0;
                while (dr.Read())
                {
                    itemofLoai[index] = dr.GetString(1);
                    
                    //create menu
                    ToolStripMenuItem menuItem = new ToolStripMenuItem();
                    menuItem.Name = dr.GetString(0);
                    menuItem.Text = itemofLoai[index];
                    menuItem.Click += new EventHandler(showWaterbyType);
                    menuStrip1.Items.Add(menuItem);

                    index++;
                }
                dr.Close();
            }
            catch (Exception exxx) { }
            var a = new DataGridViewComboBoxColumn();
            a.HeaderText = ("Loại sản phẩm");
            a.Items.AddRange(itemofLoai);
            
            

            var dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            a.DefaultCellStyle = dataGridViewCellStyle2;
            a.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            dta.Columns.Add(a);
            
            showWaterbyType(all,EventArgs.Empty);
           

            
        }
     
        private void showWaterbyType(object sender, EventArgs e)
        {
            
            dta.Rows.Clear();
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                item.BackColor = Color.Transparent;
                item.ForeColor = Color.Black;
            }
            var objectSender = (ToolStripMenuItem)sender;
            objectSender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(106)))), ((int)(((byte)(113)))));
            objectSender.ForeColor = Color.White;
            dta.Name = objectSender.Text;
            try
            {
                dta.Columns[0].ReadOnly = true;
                if (objectSender.Name.Equals("all"))
                {
                    commd = new SqlCommand("select MaNuoc,TenNuoc,Gia,TenLoaiNuoc,image " +
                    "from LoaiNuoc ln,ThucUong tu where ln.MaLoaiNuoc = tu.MaLoaiNuoc", conn);
                    dta.Columns[4].ReadOnly = false;
                }
                else
                {
                    commd = new SqlCommand("select MaNuoc,TenNuoc,Gia,TenLoaiNuoc,image " +
                        "from LoaiNuoc ln,ThucUong tu where ln.MaLoaiNuoc = tu.MaLoaiNuoc and " +
                        "tu.maloainuoc = '" + objectSender.Name + "'", conn);
                                    dta.Columns[4].ReadOnly = true;

                }
                SqlDataReader dr = commd.ExecuteReader();


                while (dr.Read())
                {

                    DataGridViewRow gido = new DataGridViewRow();
                    DataGridViewTextBoxCell dgvTenNuoc = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell dgvMaNuoc = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell dgvGia = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell dgvImage = new DataGridViewTextBoxCell();

                    dgvMaNuoc.Value = dr.GetString(0);
                    dgvTenNuoc.Value = dr.GetString(1);
                    dgvGia.Value = dr.GetInt32(2);
                    dgvImage.Value = dr.GetString(4);
                    

                    gido.Cells.Add(dgvMaNuoc);
                    gido.Cells.Add(dgvTenNuoc);
                    gido.Cells.Add(dgvGia);
                    gido.Cells.Add(dgvImage);
                    

                    dta.Rows.Add(gido);
                    dta.Rows[dta.Rows.IndexOf(gido)].Cells[4].Value = dr.GetString(3);
                   /* if (!objectSender.Name.Equals("all"))
                    {
                        dgvMaNuoc.ReadOnly = true;
                        gido.Cells[4].ReadOnly = true;
                    }*/


                }
                dr.Close();
                btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối");
            }
        }

      

        private void dta_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            dta.Rows.Clear();
            foreach(ToolStripMenuItem item in menuStrip1.Items)
            {
                if (item.BackColor == System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(106)))), ((int)(((byte)(113))))))
                    showWaterbyType(item, EventArgs.Empty);
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dta.Name.Equals("Tất cả"))
                {
                    commd = new SqlCommand("delete from thucuong", conn);
                    commd.ExecuteNonQuery();
                }
                else
                {
                    foreach (ToolStripMenuItem item in menuStrip1.Items)
                    {
                        if (item.BackColor == System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(106)))), ((int)(((byte)(113))))))
                        {
                            commd = new SqlCommand("delete from thucuong where maloainuoc='" + item.Name+"'", conn);
                            commd.ExecuteNonQuery();
                            break;
                        }
                    }
                }
            }
            catch(Exception del)
            {
                MessageBox.Show("Lỗi truy cập");
            }
            bool check = true;
            foreach (DataGridViewRow row in dta.Rows)
            {
                if (row.Index == dta.Rows.Count - 1)
                {
                    break;
                }
                bool isEmptyCell = false;
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    if (row.Cells[i].Value == null || row.Cells[i].Value == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[i].Value.ToString()))
                    {
                        MessageBox.Show("Không được bỏ trống khi thêm");
                        isEmptyCell = true;
                        break;
                    }
                }
                if (!isEmptyCell)
                {
                    try
                    {
                        commd = new SqlCommand("select maloainuoc from loainuoc where tenloainuoc = N'" + row.Cells[4].FormattedValue + "'", conn);
                        String temp = Convert.ToString(commd.ExecuteScalar());
                        commd = new SqlCommand("insert into ThucUong(manuoc,tennuoc,gia,maloainuoc,image) " +
                        "values ('" + row.Cells[0].FormattedValue + "'," +
                        "N'" + row.Cells[1].FormattedValue + "'," +
                        "" + Convert.ToInt32(row.Cells[2].FormattedValue) + "," +
                        "'" + temp + "'," +
                        "'" + row.Cells[3].FormattedValue + "');", conn);
                        commd.ExecuteNonQuery();
                        
                    }
                    catch (Exception ex)
                    {
                        check = false;
                        //MessageBox.Show("lỗi khi thực hiện truy vấn tại dòng"+(row.Index+1).ToString());
                    }
                }
            }
            if (check)
            {
                MessageBox.Show("Cập nhật thành công");
            }
            else
            {
                MessageBox.Show("cập nhật thất bại");
            }
            btnSave.Enabled = false;
        }

        private void dta_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void dta_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            btnSave.Enabled = true;
        }

  

        private void dta_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var objectSender = (DataGridView)sender;
            if (objectSender.CurrentCell.ColumnIndex == 3)
            {
              
                var fd = new System.Windows.Forms.OpenFileDialog();
                fd.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";
                fd.ShowDialog();
                objectSender.CurrentCell.Value = fd.FileName;
            }
        }

        bool checkSelectMenuItem(ToolStripItem sender)
        {
            return(
            sender.BackColor == System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(106)))), ((int)(((byte)(113)))))
            )?true : false;
            

        }
        private void dta_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            var objectSender = (DataGridView)sender;
            int indexMenuItemSelected = 0;
            for (indexMenuItemSelected = 0; indexMenuItemSelected < menuStrip1.Items.Count; indexMenuItemSelected++)
            {
                if (checkSelectMenuItem(menuStrip1.Items[indexMenuItemSelected]))
                {
                    break;
                }
            }
            if (!dta.Name.Equals("Tất cả"))
            {
                objectSender.CurrentRow.Cells[4].Value = menuStrip1.Items[indexMenuItemSelected].Text;
                autoSetIDWater(menuStrip1.Items[indexMenuItemSelected].Text, objectSender.CurrentRow.Index);
                //IDNewWater = objectSender.Rows[objectSender.CurrentRow.Index - 1].Cells[0].Value.ToString();
                //indexIDNewWater = Convert.ToInt32(IDNewWater.Substring(-1,3));
                //indexIDNewWater++;
                //IDNewWater = IDNewWater.Substring(-3,IDNewWater.Length-3) + indexIDNewWater.ToString();
                //MessageBox.Show(IDNewWater)



            }
            else
            {

                objectSender.CurrentRow.Cells[4].Value = objectSender.Rows[objectSender.CurrentRow.Index - 1].Cells[4].FormattedValue.ToString();
                autoSetIDWater(objectSender.CurrentRow.Cells[4].FormattedValue.ToString(), objectSender.CurrentRow.Index);
               
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dta_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is ComboBox)
            {
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.SelectedIndexChanged -= LastColumnComboSelectionChanged;
                comboBox.SelectedIndexChanged += LastColumnComboSelectionChanged;
            }
        }
        private void LastColumnComboSelectionChanged(object sender, EventArgs e)
        {
            DataGridViewComboBoxEditingControl comboBox = (DataGridViewComboBoxEditingControl)sender;
            autoSetIDWater(comboBox.Text,comboBox.EditingControlRowIndex);
    
        }
        private void autoSetIDWater(String nameTypeofWater, int indexCurrentRow)
        {
            //IDWater = IDTypeWater + indexIDWater 

            String IDLastWater  ="",IDTypeWater ="";
            int indexIDNewWater;
            commd = new SqlCommand("select maloainuoc from loainuoc where tenloainuoc =N'" + nameTypeofWater + "'", conn);
            IDTypeWater = Convert.ToString(commd.ExecuteScalar());
            try
            {

                for (int i = dta.Rows.Count-3;i>=0;i--)
                {
                    if (dta.Rows[i].Cells[4].FormattedValue.Equals(nameTypeofWater))
                    {
                        IDLastWater = dta.Rows[i].Cells[0].FormattedValue.ToString();

                    break;
                    }
                } 
                indexIDNewWater = Convert.ToInt32(IDLastWater.Substring(IDTypeWater.Length)) + 1;

        }
            catch
            {
                
                indexIDNewWater = 100;

            }
            String IDNewWater = IDTypeWater + indexIDNewWater.ToString();
            dta.Rows[indexCurrentRow].Cells[0].Value = IDNewWater;
        }
    }
}