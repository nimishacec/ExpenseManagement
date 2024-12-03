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

namespace ExpenseApp_Windows
{
    public partial class Login : Form
    {
        private string _connectionString = "Data Source=localhost\\SQLEXPRESS;Database=ExpenseDB;User ID=User;Password=SqlServer@123;";
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_click(object sender, EventArgs e)
        {
            string email = textUsername.Text.Trim();
            string password = textPassword.Text.Trim();
            if (ValidateUser(email, password))
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open MainForm and hide the login form
                DashBoard mainForm = new DashBoard();
                this.Hide();
                mainForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid email or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to validate login credentials against the database
        private bool ValidateUser(string email, string password)
        {
            bool isValid = false;

            // SQL query to check if the email and password match
            string query = "SELECT * FROM Login WHERE Username = @Username and Password=@Password";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Username", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    // Execute the query and retrieve the password hash from the database
                    var result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                       
                            isValid = true;
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return isValid;
        }

    

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
