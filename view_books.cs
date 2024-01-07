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
    public partial class view_books : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=library;Integrated Security=True;Pooling=False;");
        public view_books()
        {
            InitializeComponent();
        }

        private void view_books_Load(object sender, EventArgs e)
        {
            display();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM books where book_title like('%"+ textBox1.Text +"%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = 0;
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM books where book_author like('%" + textBox2.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                dataGridView1.DataSource = dt;

                con.Close();
                if (i == 0)
                {
                    MessageBox.Show("no books found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = 0;
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM books where book_publication like('%" + textBox3.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                i = Convert.ToInt32(dt.Rows.Count.ToString());  
                dataGridView1.DataSource = dt;

                con.Close();
                if (i == 0)
                {
                    MessageBox.Show("no books found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panel4.Visible = true;
            int i;
            i =Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM books where id=" + i + "";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                
                foreach(DataRow row in dt.Rows) 
                { 
                    Title.Text = row["book_title"].ToString();
                    author.Text = row["book_author"].ToString();
                    publication.Text = row["book_publication"].ToString();
                    purchaseDate.Value = Convert.ToDateTime(row["book_purchaseDate"].ToString());
                    price.Text = row["book_price"].ToString();
                    quantity.Text = row["book_quantity"].ToString();

                }

                con.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            try 
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                //date nie dziala
                cmd.CommandText = "UPDATE books set book_title='" + Title.Text + "',book_author='" + author.Text + "',book_publication='" + publication.Text + "',book_purchaseDate='" + purchaseDate.Value + "',book_price='"+ price.Text + "',book_quantity='"+ quantity.Text + "' WHERE id=" + i + " ";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Updated");
                panel4.Visible = false;
                display();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void display()
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM books";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
