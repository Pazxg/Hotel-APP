using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hotel
{
    public partial class Booking : Form
    {
        public Booking()
        {
            InitializeComponent();
            ShowBookings();
            
        }
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\55\Documents\GuestsDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowBookings()
        {
            connection.Open();
            string Query = "Select * from BookingTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookingData.DataSource = ds.Tables[0];
            connection.Close();
        }
        private void FilterBookings()
        {
            connection.Open();
            string Query = "Select * from BookingTbl where RType= '" + RTypeCb.SelectedItem.ToString() +"'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookingData.DataSource = ds.Tables[0];
            connection.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ShowBookings();
        }

        private void RTypeCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterBookings();
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BookingData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
