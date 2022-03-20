using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Hotel
{
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            ShowCustomers();
        }
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\55\Documents\GuestsDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowCustomers()
        {
            connection.Open();
            string Query = "Select * from CustomersTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CusData.DataSource = ds.Tables[0];
            connection.Close();
        }
        private void Reset()
        {
            CusNameT.Text = "";
            CusPhoneT.Text = "";
            CusGen.SelectedIndex = -1;
            Key = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (CusNameT.Text == "" || CusPhoneT.Text == "" || CusGen.SelectedIndex == -1)
            {
                MessageBox.Show("Missing İnformation");
            }
            else
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("insert into Customerstbl(Cusname,Cusphone,CusGen,CusDob)values(@CN,@CP,@CG,@CD)", connection);
                    cmd.Parameters.AddWithValue("@CN", CusNameT.Text);
                    cmd.Parameters.AddWithValue("@CP", CusPhoneT.Text);
                    cmd.Parameters.AddWithValue("@CG", CusGen.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@CD", CusDob.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Saved");
                    connection.Close();
                    ShowCustomers();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        int Key = 0;
        private void CusData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CusNameT.Text = CusData.SelectedRows[0].Cells[1].Value.ToString();
            CusPhoneT.Text = CusData.SelectedRows[0].Cells[2].Value.ToString();
            CusGen.Text = CusData.SelectedRows[0].Cells[3].Value.ToString();
            CusDob.Text = CusData.SelectedRows[0].Cells[4].Value.ToString();

            if (CusNameT.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt16(CusData.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void deleteb_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select Customer");
            }
            else
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("delete from Customerstbl where CusId=@CKey", connection);
                    cmd.Parameters.AddWithValue("@CKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted");
                    connection.Close();
                    ShowCustomers();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void editb_Click(object sender, EventArgs e)
        {
            if (CusNameT.Text == "" || CusPhoneT.Text == "" || CusGen.SelectedIndex == -1)
            {
                MessageBox.Show("Missing İnformation");
            }
            else
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("update Customerstbl set Cusname=@CN,Cusphone=@CP,CusGen=@CG,CusDob=@CD", connection);
                    cmd.Parameters.AddWithValue("@CN", CusNameT.Text);
                    cmd.Parameters.AddWithValue("@CP", CusPhoneT.Text);
                    cmd.Parameters.AddWithValue("@CG", CusGen.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@CD", CusDob.Value.Date);
                    cmd.Parameters.AddWithValue("@CKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Saved");
                    connection.Close();
                    ShowCustomers();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Customers_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Main Obj = new Main();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Customers Obj = new Customers();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Booking Obj = new Booking();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Log Obj = new Log();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

