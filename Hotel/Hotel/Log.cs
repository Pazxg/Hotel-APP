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

namespace Hotel
{
    public partial class Log : Form
    {
        public Log()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Users obj = new Users();
            obj.Show();
            this.Hide();
        }
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\55\Documents\GuestsDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void button1_Click(object sender, EventArgs e)
        {
            if(UlogT.Text == " " || UpassT.Text == " ")
            {
                MessageBox.Show("Enter Username and Password");
            }
            else
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("Select Count (*) from Userstbl where Uname='"+UlogT.Text+"' and UPass='" + UpassT.Text + "'", connection);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if(dt.Rows[0][0].ToString() == "1")
                    {
                        Main Obj = new Main();
                        Obj.Show();
                        this.Hide();
                        connection.Close();
                    }
                    else
                    {
                        MessageBox.Show("Wrong Username or password.");
                    }
                    connection.Close();
                }
                catch(Exception)
                {
                    throw;
                }
            }
        }
    }
}
