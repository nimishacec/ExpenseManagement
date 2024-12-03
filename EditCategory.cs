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
    public partial class EditCategory : Form
    {
        private string _connectionString = "Data Source=localhost\\SQLEXPRESS;Database=ExpenseDB;User ID=User;Password=SqlServer@123;";
        private int CategoryId;
        public EditCategory()
        {
            InitializeComponent();
        }
        public EditCategory(int categoryID)
        {
            CategoryId = categoryID;
            InitializeComponent();
            LoadCategory(categoryID);
        }
        private void LoadCategory(int categoryID)
        {
            string query = "SELECT *  FROM Category where CategoryID=@CategoryID";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CategoryID", categoryID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtCategory.Text = reader["CategoryName"].ToString();
                            txtDescrptn.Text = reader["Description"].ToString();
                        }
                        else
                        {
                            txtCategory.Text = "Category not found!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string categoryname=txtCategory.Text;
            string description=txtDescrptn.Text;
            string query = "Update Category set CategoryName= @CategoryName,Description=@Description where CategoryID=@CategoryID";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        cmd.Parameters.AddWithValue("@CategoryName", categoryname);                    
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@CategoryID", CategoryId);
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();                        
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Category edited successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to edit Category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
