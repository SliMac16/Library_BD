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

namespace Library_BD
{
    public partial class login : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=library;Integrated Security=True;Pooling=False;");
        int count = 0;
        public login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Library_Login where username ='" + textBox2.Text + "' and password ='" + textBox1.Text + "' ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            count = Convert.ToInt32(dt.Rows.Count.ToString());
            if(count == 0) 
                {
                    MessageBox.Show("Incorrect Username and/or Password");
                }
            else
                {
                this.Hide();
                MDI_user mu = new MDI_user();
                mu.Show();
                }
        }
    }
}
