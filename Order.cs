using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanLySinhVien
{

    public partial class Order : Form
    {
        private SqlConnection conn;
        private SqlCommand commd;
        private bool admin;
        private String accountLogin;
        public Order(String accountLogin,bool ad)
        {
            InitializeComponent();
            admin = ad;
            this.accountLogin = accountLogin;
        }

        void loadData()
        {
            try
            {
                conn = new SqlConnection("Server=DESKTOP-TP0I2O9\\SQLEXPRESS; Database = QLCF;" +
                        "uid=sa;pwd=sa2008;MultipleActiveResultSets=true");
                conn.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối");
            }
            if (admin)
            {
                hoadon.Visible   = true;
                nhanvien.Visible = true;
                thucuong.Visible = true;
                loainuoc.Visible = true;
            }


            //dat event cho menu tat ca

            all.Click += new EventHandler(showWaterByType);


            //lay so luong thuc uong
            try
            {
                // xuat menu
                commd = new SqlCommand("Select * from LoaiNuoc", conn);
                SqlDataReader rd = commd.ExecuteReader();

                while (rd.Read())
                {
                    var idWater = rd.GetString(0);
                    var nameWater = rd.GetString(1);
                    ToolStripMenuItem temp = new ToolStripMenuItem();
                    temp.Name = idWater;
                    temp.Text = nameWater;
                    menuStrip2.Items.Add(temp);

                    // MessageBox.Show("gido");
                    temp.Click += new EventHandler(showWaterByType);
                }
                rd.Close();



                commd = new SqlCommand("select COUNT(manuoc)  from ThucUong", conn);
                int numwater = Convert.ToInt32(commd.ExecuteScalar());

                menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(106)))), ((int)(((byte)(113)))));
                menu.ForeColor = Color.White;


                //xây dựng mặc nhiên các thông tin chi tiết của từng thứcc uống
                showWaterByType(all, EventArgs.Empty);



                //set tablePanel line number to 0

                tableLayoutPanel1.RowCount = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy cập");
            }
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            loadData(); 


            
        }
       

        private void showWaterByType(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            var objectSender = (ToolStripMenuItem)sender;

            // xuat thong tin nuoc
            if (objectSender.Name.Equals("all"))
            {
                commd = new SqlCommand("select * from Thucuong", conn);
            }
            else 
                commd = new SqlCommand("select * from ThucUong where maloainuoc = '"+objectSender.Name+"'", conn);

            SqlDataReader rd = commd.ExecuteReader();

            int x = 0, y = 0;
            while (rd.Read())
            {
               
                TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
                PictureBox imageWaters = new PictureBox();
                Label nameWaters = new Label();
                Label priceWaters = new Label();

                tableLayoutPanel = new TableLayoutPanel();
                imageWaters = new System.Windows.Forms.PictureBox();
                nameWaters = new Label();
                priceWaters = new Label();
                nameWaters.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                nameWaters.Dock = DockStyle.Fill;
                nameWaters.TextAlign = ContentAlignment.MiddleCenter;
                priceWaters.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                priceWaters.Dock = DockStyle.Fill;
                priceWaters.TextAlign = ContentAlignment.MiddleCenter;
                imageWaters.Dock = DockStyle.Fill;
                
                //tableLayoutPanel
                tableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
                tableLayoutPanel.RowCount = 3;
                tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
                tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
                tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
                tableLayoutPanel.Size = new System.Drawing.Size(100, 110);
                tableLayoutPanel.ColumnCount = 1;
                tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));



                //image
                imageWaters.Size = new System.Drawing.Size(96, 80);
                imageWaters.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;


                if (x >= panel1.Width / 120)
                { x = 0; y++; }
                    try
                    {
                        nameWaters.Name = rd.GetString(0);
                        nameWaters.Text = rd.GetString(1);
                        priceWaters.Text = rd.GetInt32(2).ToString();
                        imageWaters.Image = Image.FromFile(rd.GetString(4));
                    }
                    catch(Exception ex)
                    {
                    imageWaters.Image = Image.FromFile("C:/Users/haohao/source/repos/QuanLyQuanNuoc/ImageWater/tra-chanh.jpg");
                    }

                    //imagewaters
                    tableLayoutPanel.Controls.Add(imageWaters, 0, 0);

                    //nameWaters
                    tableLayoutPanel.Controls.Add(nameWaters, 0, 1);

                    //priceWaters
                    tableLayoutPanel.Controls.Add(priceWaters, 0, 2);

                    tableLayoutPanel.Location = new Point(x * 120 + 30, y * 170 + 30);
                    tableLayoutPanel.Controls[0].Click += new EventHandler(addWaterIntoOrder);

                    panel1.Controls.Add(tableLayoutPanel);
                    x++;
                
            }
            rd.Close();
        }

        
       
        private void addWaterIntoOrder(object sender, EventArgs e )
        {
            if (btnNewOrder.Enabled == false)
            {


                PictureBox tmp = (PictureBox)sender;
                TableLayoutPanel temp2 = (TableLayoutPanel)tmp.Parent;


                bool checkDistinct = true;
                foreach (TableLayoutPanel layout in tableLayoutPanel1.Controls) {
                    if (layout.Controls.ContainsKey("id"+temp2.Controls[1].Name))
                    {
                        var quantity = (NumericUpDown)layout.Controls[layout.Controls.IndexOfKey("id"+temp2.Controls[1].Name) + 1];
                        quantity.Value++;
                        checkDistinct = false;
                    }
                }
                //thêm số lượng khi order đã tồn tại sản phẩm
                if (checkDistinct)
                {
                    TableLayoutPanel tableLayout = new TableLayoutPanel();
                    tableLayout.Name = tableLayoutPanel1.RowCount.ToString();
                    tableLayout.ColumnCount = 5;
                    tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
                    tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
                    tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
                    tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
                    tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
                    tableLayout.Dock = DockStyle.Fill;
                    //tableLayout.RowCount = 1;
                  //  tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));

                    Label c0 = new Label();
                    Label c2 = new Label();
                    Label c3 = new Label();
                    NumericUpDown c1 = new NumericUpDown();
                    c0.Text = temp2.Controls[1].Text;
                    c0.Name = "id" + temp2.Controls[1].Name;
                    c0.Dock = DockStyle.Fill;
                    c1.Value = 1;
                    c1.Name = "nud" + temp2.Controls[1].Name;
                    c1.Dock = DockStyle.Fill;
                    c2.Text = c3.Text = temp2.Controls[2].Text;
                    c2.Dock = DockStyle.Fill;
                    c3.Dock = DockStyle.Fill;
                    Button c4 = new Button();
                    c4.Width = 30;
                    c4.Text = "x";
                    c4.Name = "btn" + temp2.Controls[1].Name;
                    c4.Dock = DockStyle.Left;




                    //add new row
                    tableLayoutPanel1.RowCount += 1;
                    tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
                    updateLayout();


                    tableLayout.Controls.Add(c0);
                    tableLayout.Controls.Add(c1);
                    tableLayout.Controls.Add(c2);
                    tableLayout.Controls.Add(c3);
                    tableLayout.Controls.Add(c4);
                    tableLayoutPanel1.Controls.Add(tableLayout);

                    updateTotalAmount();

                    //căn chỉnh giao diện cho hợp lệ

                    c0.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
                    c0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;


                    c1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
                    c1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                    c1.ValueChanged += new EventHandler(updateTotalPrice);



                    c2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
                    c2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    c3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
                    c3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    c4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
                    c4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    c4.BackColor = System.Drawing.Color.Red;

                    c4.Click += new EventHandler(deleteProductCart);


                    
                }
            }

        }

  




        private void updateTotalPrice(object sender, EventArgs e)
        {
            var quantitychange = (NumericUpDown)sender;
            var content  = (TableLayoutPanel)quantitychange.Parent;
            var totalPrice = (Label)content.Controls[content.Controls.IndexOfKey(quantitychange.Name) + 2];
            var unitPrice = (Label)content.Controls[content.Controls.IndexOfKey(quantitychange.Name) + 1];
            totalPrice.Text = (Convert.ToInt64(unitPrice.Text) * quantitychange.Value).ToString();
            updateTotalAmount();

        }
        private void updateTotalAmount()
        {
            int totalAmount = 0;
            double totalToPay = 0;
            int discount = 0;
            try
            {
                discount = Convert.ToInt32(txtDiscount.Text);
            }
            catch(Exception e)
            {
                discount = 0;
            }
            for (int r = 0; r < tableLayoutPanel1.RowCount; r++)
            {
                var tableLayoutChild = (TableLayoutPanel)tableLayoutPanel1.Controls[r];

                totalAmount += Convert.ToInt32(tableLayoutChild.GetControlFromPosition(3, 0).Text);
                totalToPay = (double)totalAmount - (double)totalAmount * (double)discount / 100.00;
            }
            txtTotalAmount.Text = totalAmount.ToString();
            txtTotalToPay.Text =  totalToPay.ToString();
        }

        private void updateLayout()
        {
            tableLayoutPanel1.Size = new System.Drawing.Size(550, tableLayoutPanel1.RowCount * 34);

        }

        private void deleteProductCart(object sender, EventArgs e)
        {
            var btnDel = (Button)sender;
            var tableLayoutChild = (TableLayoutPanel)btnDel.Parent;
            tableLayoutPanel1.Controls.RemoveAt(tableLayoutPanel1.Controls.IndexOfKey(tableLayoutChild.Name));
            tableLayoutPanel1.RowCount--;
            updateTotalAmount();
            updateLayout();
        }






        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void thucuongMenuItem_Click(object sender, EventArgs e)
        {
            new Water().ShowDialog();
        }

        private void btnPayBill_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
        //    Image image = Resources.logo2;

          //  e.Graphics.DrawImage(image, 0, 0, image.Width, image.Height);

            e.Graphics.DrawString("Date: " + DateTime.Now.ToShortDateString(), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(25, 160));

            e.Graphics.DrawString("Table: " + txtTable.Text.Trim(), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(25, 190));

            e.Graphics.DrawString("------------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(25, 235));
            
            e.Graphics.DrawString("Item Name", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(30, 255));
            e.Graphics.DrawString("Quantity", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(380, 255));
            e.Graphics.DrawString("Unit Price", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(510, 255));
            e.Graphics.DrawString("Total Price", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(660, 255));
            e.Graphics.DrawString("------------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(25, 270));

            int yPos = 295;
            
            for (int r = 0; r < tableLayoutPanel1.RowCount; r++ )
            {
                var tableLayoutChild = (TableLayoutPanel)tableLayoutPanel1.Controls[r];
                e.Graphics.DrawString(tableLayoutChild.GetControlFromPosition(0, 0).Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(30, yPos));
                e.Graphics.DrawString(tableLayoutChild.GetControlFromPosition(1, 0).Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(400, yPos));
                e.Graphics.DrawString(tableLayoutChild.GetControlFromPosition(2, 0).Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(525, yPos));
                e.Graphics.DrawString(tableLayoutChild.GetControlFromPosition(3, 0).Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(675, yPos));

                yPos += 30;
            }
            
            e.Graphics.DrawString("------------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(25, yPos));

            e.Graphics.DrawString("Total Amount:      " + txtTotalAmount.Text.Trim() +" đ", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(550, yPos + 30));
            e.Graphics.DrawString("Discount:              " + txtDiscount.Text.Trim() + " %", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(550, yPos + 60));
            e.Graphics.DrawString("Total To Pay:      " + txtTotalToPay.Text.Trim() +" đ", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(550, yPos + 90));

        }

    

        private void newOrder_Click(object sender, EventArgs e)
        {
            btnPrint.Enabled = true;
            btnPrintPreview.Enabled = true;
            txtTable.Enabled = true;
            txtDiscount.Enabled = true;
            btnNewOrder.Enabled = false;
            btnCancel.Enabled = true;
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            

            //luu hoa don
            int IdBill = 0;
            
            commd = new SqlCommand("select * from HoaDon where Date = (select MAX(DATE) from HoaDon);", conn);

            SqlDataReader dr = commd.ExecuteReader();
            while (dr.Read())
            {
                DateTime datee = dr.GetDateTime(1);
                IdBill = Convert.ToInt32(dr.GetString(0));
            }
            //thêm hoá đơn
            IdBill++;
            string sql = "insert into hoadon(IDBill,Date,TotalAmount,account) values ('" + IdBill.ToString() + "'," +
                "getdate()," +
               txtTotalToPay.Text + "," +
                "'" + accountLogin + "')";
            commd = new SqlCommand(sql,conn);
            commd.ExecuteNonQuery();    

            //chi tiết hoá đơn
            for (int r = 0; r < tableLayoutPanel1.RowCount; r++)
            {
                var tableLayoutChild = (TableLayoutPanel)tableLayoutPanel1.Controls[r];
                commd = new SqlCommand("insert into chitiethoadon(IDBill,NameWater,Quantity,TotalPrice) values(" +
                  "'" +  IdBill.ToString() + "'," +
               "N'" +tableLayoutChild.GetControlFromPosition(0,0).Text+"',"+
                    tableLayoutChild.GetControlFromPosition(1,0).Text +","+
                    tableLayoutChild.GetControlFromPosition(3,0).Text +")",conn) ;
                commd.ExecuteNonQuery();
                
            }


            

            //set button and ....
            printDocument1.Print();
            btnNewOrder.Enabled = true;
            btnPrint.Enabled = false;
            btnPrintPreview.Enabled = false;
            txtTable.Enabled = false;
            txtTable.Clear();
            txtDiscount.Enabled = false;
            txtDiscount.Text = "0";
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowCount = 0;
            txtTotalAmount.Clear();
            btnCancel.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancel.Enabled = false;
            btnNewOrder.Enabled = true;
            btnPrint.Enabled = false;
            btnPrintPreview.Enabled = false;
            txtTable.Enabled = false;
            txtTable.Clear();
            txtDiscount.Text = "0";
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowCount = 0;
            txtTotalAmount.Clear();
            txtTotalToPay.Clear();
           
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {

            new Login().Visible = true;
            this.Visible = false;
        }

        private void nhanvienMenuItem_Click(object sender, EventArgs e)
        {
            new Tonghop("taikhoan").ShowDialog();
        }

        private void txtTable_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTotalAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void loainuoc_Click(object sender, EventArgs e)
        {
            new Tonghop("loainuoc").ShowDialog();
        }

        private void hoáĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Tonghop("Hoadon").ShowDialog();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            updateTotalAmount();
        }
    }
}
