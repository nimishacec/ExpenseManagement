using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseApp_Windows
{
    public partial class AddCategory : Form
    {
        private string _connectionString = "Data Source=localhost\\SQLEXPRESS;Database=ExpenseDB;User ID=User;Password=SqlServer@123;";

        public AddCategory()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string category=txtCategory.Text;
            string description=txtDescrptn.Text;
            string query = "INSERT INTO Category (CategoryName,Description) VALUES (@CategoryName, @CategoryDescription)";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters
                        cmd.Parameters.AddWithValue("@CategoryName", category);
                       
                        cmd.Parameters.AddWithValue("@CreatedDate", System.DateTime.Now);
                        cmd.Parameters.AddWithValue("@CategoryDescription", description);
                       

                        // Open connection and execute query
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Confirm insertion
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Category added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add Category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDescrptn_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
