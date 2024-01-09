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
    public partial class issue_book : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=library;Integrated Security=True;Pooling=False;");
        public issue_book()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            int i = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM student where student_index='" + indexBox.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);   

            i = Convert.ToInt32(dt.Rows.Count.ToString());

            if(i == 0)
            {
                MessageBox.Show("Student does not exist");
            }
            else
            {
                    foreach(DataRow dr in dt.Rows)
                    {
                    nameBox.Text = dr["student_name"].ToString();
                    departmentBox.Text = dr["student_department"].ToString();
                    phoneBox.Text = dr["student_phone"].ToString();
                }
            }
            


        }

        private void issue_book_Load(object sender, EventArgs e)
        {
            CheckCon();
            listBox1.Visible = false;

        }

        private void titleBox_KeyUp(object sender, KeyEventArgs e)
        {
            int count = 0;

            if(e.KeyCode != Keys.Enter) 
            {
                listBox1.Items.Clear();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM books where book_title like('%" + titleBox.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

                count = Convert.ToInt32(dt.Rows.Count.ToString());

                if(count > 0)
                {
                    listBox1.Visible = true;
                    foreach(DataRow dr in dt.Rows) 
                    {
                        listBox1.Items.Add(dr["book_title"].ToString());
                    }
                }
            }

        }

        public void CheckCon()
        {
            if (con.State == ConnectionState.Open) 
            { 
                con.Close(); 
            }
            con.Open();
        }

        private void titleBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
            {
                listBox1.Focus();

                //listBox1.SelectedIndex = 0;
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
            
                titleBox.Text = listBox1.SelectedItem.ToString();

                listBox1.Visible = false;

            
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            titleBox.Text = listBox1.SelectedItem.ToString();

            listBox1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int q = 0;
                SqlCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "SELECT * from books where book_title='"+ titleBox.Text +"'";
                cmd2.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd2);
                adapter.Fill(dt);

                foreach(DataRow dr in dt.Rows)
                {
                    q = Convert.ToInt32(dr["book_available"].ToString());
                }

                if (q > 0)
                {




                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT into issue_book values('" + indexBox.Text + "','" + nameBox.Text + "','" + departmentBox.Text + "','" + phoneBox.Text + "','" + titleBox.Text + "','" + dateTimePicker1.Value + "')";
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Success");

                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "UPDATE books set book_available=book_available-1 where book_title='" + titleBox.Text + "'";
                    cmd1.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Book is unavailable");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
    }
}
