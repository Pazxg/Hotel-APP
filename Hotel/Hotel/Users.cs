using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;



namespace Hotel
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            ShowUsers();
        }
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\55\Documents\GuestsDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void ShowUsers() 
        {
            connection.Open();
            string Query = "Select * from UsersTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query,connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UsersData.DataSource = ds.Tables[0];
            connection.Close();
        }
        private void saveb_Click(object sender, EventArgs e)
        {
            if (UnameT.Text == "" || UphoneT.Text == "" || UpasswordT.Text == "")
            {
                MessageBox.Show("Missing İnformation");
            }
            else
            {
                try
                {   connection.Open();
                    SqlCommand cmd = new SqlCommand("insert into Userstbl(Uname,Uphone,Upass)values(@UN,@UP,@UPA)",connection);
                    cmd.Parameters.AddWithValue("@UN", UnameT.Text);
                    cmd.Parameters.AddWithValue("@UP", UphoneT.Text);
                    cmd.Parameters.AddWithValue("@UPA",UpasswordT.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Saved");
                    connection.Close();
                    ShowUsers();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void UsersData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UnameT.Text = UsersData.SelectedRows[0].Cells[1].Value.ToString();
            UphoneT.Text = UsersData.SelectedRows[0].Cells[2].Value.ToString();
            UpasswordT.Text = UsersData.SelectedRows[0].Cells[3].Value.ToString();
            if (UnameT.Text == "")
            {
                Key=0;
            }
            else
            {
                Key = Convert.ToInt16(UsersData.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void editb_Click(object sender, EventArgs e)
        {
            if (UnameT.Text == "" || UphoneT.Text == "" || UpasswordT.Text == "")
            {
                MessageBox.Show("Missing İnformation");
            }
            else
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("update Userstbl Set Uname=@UN,Uphone=@UP,Upass=@UPA where UId=@Key", connection);
                    cmd.Parameters.AddWithValue("@UN", UnameT.Text);
                    cmd.Parameters.AddWithValue("@UP", UphoneT.Text);
                    cmd.Parameters.AddWithValue("@UPA", UpasswordT.Text);
                    cmd.Parameters.AddWithValue("@Key", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Updated");
                    connection.Close();
                    ShowUsers();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }
        private void Reset()
        {
            UnameT.Text = "";
            UphoneT.Text = "";
            UpasswordT.Text = "";
            Key = 0;
        }

        private void deleteb_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select User");
            }
            else
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("delete from Userstbl where UId=@UKey", connection);
                    cmd.Parameters.AddWithValue("@UKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted");
                    connection.Close();
                    ShowUsers();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
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
