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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DaraBase
{
    public partial class Form1 : Form
    {
        private SqlConnection connection;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\source\repos\DaraBase\DaraBase\SchoolDB.mdf;Integrated Security=True";

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                MessageBox.Show("Connection Opened Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (connection == null || connection.State != System.Data.ConnectionState.Open)
            {
                MessageBox.Show("Database connection is not open.");
                return;
            }

            // Retrieve input values
            string name = textBox1.Text;
            int grade;
            if (!int.TryParse(textBox2.Text, out grade))
            {
                MessageBox.Show("Please enter a valid grade.");
                return;
            }

            // Use parameterized query
            string query = "INSERT INTO StudentTable (Name, Grade) VALUES (@Name, @Grade)";

            try
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the query
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Grade", grade);

                    // Execute query
                    command.ExecuteNonQuery();
                    MessageBox.Show("Record inserted successfully.");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Ensure the connection is closed when the form is closing
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(connection == null || connection.State != System.Data.ConnectionState.Open)
            {
                MessageBox.Show("DataBase connection is not open");
                return;
            }
            
            int id;
            if (!int.TryParse(textBox5.Text,out id))
            {
                MessageBox.Show("Please enter a valid id.");
                return;
            }
            string name = textBox3.Text;
            int grade;
            if (!int.TryParse(textBox4.Text, out grade))
            {
                MessageBox.Show("Please enter a valid grade.");
                return;
            }
            string query = "UPDATE StudentTable SET Name = @Name , Grade = @Grade WHERE ID = @ID";

            try
            {
                using(SqlCommand command = new SqlCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@ID",id);
                    command.Parameters.AddWithValue("@Name",name);
                    command.Parameters.AddWithValue("@Grade",grade);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("No record found with the specified ID.");
                    }
                }
            }
            catch(Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (connection == null || connection.State != System.Data.ConnectionState.Open)
            {
                MessageBox.Show("DataBase connection is not open");
                return;
            }
            int id;
            if (!int.TryParse(textBox5.Text, out id))
            {
                MessageBox.Show("Please enter a valid id.");
                return;
            }
            string query = "DELETE FROM StudentTable WHERE ID = @ID";

            try
            {
                using(SqlCommand command = new SqlCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@ID",id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("No record found with the specified ID.");
                    }
                }
            }
            catch(Exception ex2)
            {
                MessageBox.Show(ex2.Message);
            }
        }
    }

    public class Person
    {
        public string name { get; set; }
        public int grade { get; set; }
    }
}

