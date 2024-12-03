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
    public partial class AddItems : Form
    {
        private string _connectionString = "Data Source=localhost\\SQLEXPRESS;Database=ExpenseDB;User ID=User;Password=SqlServer@123;";

        public AddItems()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int categoryId = Convert.ToInt32(cmbCategory.SelectedValue);
            string itemname=txtItems.Text;
            string query = "INSERT INTO Items (ItemName,CategoryID) VALUES (@ItemName, @CategoryID)";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                     
                        cmd.Parameters.AddWithValue("@ItemName", itemname);

                        cmd.Parameters.AddWithValue("@CreatedDate", System.DateTime.Now);
                        cmd.Parameters.AddWithValue("@CategoryID", categoryId);


                       
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Confirm insertion
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Items added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add Items.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadCategories()
        {

            string query = "SELECT CategoryID, CategoryName FROM Category"; 

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    
                    cmbCategory.DisplayMember = "CategoryName"; 
                    cmbCategory.ValueMember = "CategoryID";  

                 
                    cmbCategory.DataSource = dt;
                    cmbCategory.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
