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

namespace Hotel
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            CountBooked();
            CountCustomers();
            CountBookings();
            GetCustomer();
        }
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\55\Documents\GuestsDb.mdf;Integrated Security=True;Connect Timeout=30");
         int free,Booked;
        int Bper, Freeper;
        private void CountBooked()
        {
            string Status = "Booked";
            connection.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count (*) from RoomTbl where Rstatus ='"+Status+"'", connection);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            free = 20 - Convert.ToInt32(dt.Rows[0][0].ToString());
            Booked = Convert.ToInt32(dt.Rows[0][0].ToString());
            Bper = (Booked / 20)*100;
            Freeper = (free / 20)*100;
            Booklb.Text = dt.Rows[0][0].ToString() + " Booked Rooms";
            Avlb.Text = free + " Free Rooms";
            Avlb1.Text = free +"";
            Bprogress.Value = Bper;
            Avprogress.Value = Freeper;
            RoomsBar.Value = Freeper;
            connection.Close();
        }
        private void CountCustomers()
        {
            connection.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count (*) from CustomersTbl", connection);
            DataTable dt = new DataTable();
            sda.Fill(dt);
          
           CustNumlb.Text = dt.Rows[0][0].ToString() + " Customers";
         
            connection.Close();
        }
        private void CountBookings()
        {
            connection.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count (*) from BookingTbl", connection);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            BookNumlb.Text = dt.Rows[0][0].ToString() + " Bookings";

            connection.Close();
        }
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel23_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }
        private void GetCustomer()
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("Select CusId from CustomersTbl", connection);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CusId" , typeof(int));
            dt.Load(rdr);
            CusIdCb.DataSource = dt;
            CusIdCb.ValueMember = "CusId";
            connection.Close();
        }
        int RoomNumber = 0;
        private void GetCusName()
        {
            connection.Open();
            string Query = "Select * from Customerstbl where CusId=" + CusIdCb.SelectedValue.ToString()+"";
            SqlCommand cmd = new SqlCommand(Query, connection);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                CusNameTb.Text = dr["CusName"].ToString();
            }
            connection.Close();
        }
        string RType;
        int RC;
        private void GetRoomType()
        {
            connection.Open();
            string Query = "Select * from Roomtbl where RId" + RoomNumber + "";
            SqlCommand cmd = new SqlCommand(Query, connection);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
               RType = dr["RType"].ToString();
                RC = Convert.ToInt32(dr["RCost"].ToString());
            }
            connection.Close();
        }
        private void Reset()
        {
            RType = "";
            RC = 0;
            RoomNumber = 0;
        }
        private void UpdateRoom() {
            string Status = "Booked";
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("update Roomtbl set RStatus=@RS where RId=@RKey", connection);
                cmd.Parameters.AddWithValue("@RS",Status);
                cmd.Parameters.AddWithValue("@RKey", RoomNumber);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Room Updated");
                connection.Close();
                Reset();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void Bookbtn_Click(object sender, EventArgs e)
        {
            if (CusNameTb.Text == "" || RoomNumber == 0)
            {
                MessageBox.Show("Select Room");
            }
            else 
            {
                try
                {
                    GetRoomType();
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("insert into Bookingtbl(CusId,CusName,RId,RNum,Rtype,BCost)values(@CI,@CN,@RI,@RN,@RT,@RC)", connection);
                    cmd.Parameters.AddWithValue("@CI", CusIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CN", CusNameTb.Text);
                    cmd.Parameters.AddWithValue("@RI", RoomNumber);
                    cmd.Parameters.AddWithValue("@RN", RoomNumber);
                    cmd.Parameters.AddWithValue("@RT", RType);
                    cmd.Parameters.AddWithValue("@RC", RC);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Booked");
                    Reset();
                    connection.Close();
                    UpdateRoom();
                   
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);    
                }
            }

        }

        private void CusNameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void CusIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCusName();
        }

        private void R1_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void R2_Paint(object sender, PaintEventArgs e)
        {
           

        }

        private void R3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void R4_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void R5_Paint(object sender, PaintEventArgs e)
        {
           

        }

        private void R6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void R7_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void R8_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void R9_Paint(object sender, PaintEventArgs e)
        {
           

        }

        private void R10_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void R11_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void R12_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void R14_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void R15_Paint(object sender, PaintEventArgs e)
        {
           

        }

        private void R16_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void R17_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void R18_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void R19_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void R20_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void CusIdCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void R1_Click(object sender, EventArgs e)
        {
            RoomNumber = 1;
        }

        private void R2_Click(object sender, EventArgs e)
        {
            RoomNumber = 2;
        }

        private void R3_Click(object sender, EventArgs e)
        {
            RoomNumber = 3;
        }

        private void R4_Click(object sender, EventArgs e)
        {
            RoomNumber = 4;
        }

        private void R5_Click(object sender, EventArgs e)
        {
            RoomNumber = 5;
        }

        private void R6_Click(object sender, EventArgs e)
        {
            RoomNumber = 6;
        }

        private void R7_Click(object sender, EventArgs e)
        {
            RoomNumber = 7;

        }

        private void R8_Click(object sender, EventArgs e)
        {
            RoomNumber = 8;
        }

        private void R9_Click(object sender, EventArgs e)
        {
            RoomNumber = 9;
        }

        private void R10_Click(object sender, EventArgs e)
        {
            RoomNumber = 10;
        }

        private void R11_Click(object sender, EventArgs e)
        {
            RoomNumber = 11;
        }

        private void R12_Click(object sender, EventArgs e)
        {
            RoomNumber = 12;
        }

        private void R13_Click(object sender, EventArgs e)
        {
            RoomNumber = 13;
        }

        private void R14_Click(object sender, EventArgs e)
        {
            RoomNumber = 14;
        }

        private void R15_Click(object sender, EventArgs e)
        {
            RoomNumber = 15;
        }

        private void R16_Click(object sender, EventArgs e)
        {
            RoomNumber = 16;
        }

        private void R17_Click(object sender, EventArgs e)
        {
            RoomNumber = 17;
        }

        private void R18_Click(object sender, EventArgs e)
        {
            RoomNumber = 18;
        }

        private void R19_Click(object sender, EventArgs e)
        {
            RoomNumber = 19;
        }

        private void R20_Click(object sender, EventArgs e)
        {
            RoomNumber = 20;
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

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Log Obj = new Log();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Main Obj = new Main();
            Obj.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
