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
    public partial class ViewItems : Form
    {
        private string _connectionString = "Data Source=localhost\\SQLEXPRESS;Database=ExpenseDB;User ID=User;Password=SqlServer@123;";

        public ViewItems()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int categoryId = Convert.ToInt32(cmbCategory.SelectedValue);
            int ItemId = Convert.ToInt32(cmbItem.SelectedValue);

            var confirmationResult = MessageBox.Show("Are you sure you want to delete this Item?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmationResult == DialogResult.Yes)
                {

                    string deleteQuery = "DELETE FROM Items WHERE CategoryID = @CategoryID and ItemID=@ItemID";

                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        SqlCommand cmd = new SqlCommand(deleteQuery, conn);
                        cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                         cmd.Parameters.AddWithValue("@ItemID", ItemId);

                    conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Item deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();

                }
            }
        
    

        private void button1_Click(object sender, EventArgs e)
        {
            int categoryId = Convert.ToInt32(cmbCategory.SelectedValue);
            int ItemId = Convert.ToInt32(cmbItem.SelectedValue);
            EditItems editCategory = new EditItems(categoryId,ItemId);
            editCategory.Show();
            this.Close();
        }
        private void LoadCategories()
        {

            string query = "SELECT CategoryID, CategoryName FROM Category"; // SQL Query to get categories

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Populate the ComboBox with Category Names (you can store IDs in ValueMember)
                    cmbCategory.DisplayMember = "CategoryName"; // What will be displayed
                    cmbCategory.ValueMember = "CategoryID";   // What will be used as the value

                    // Bind the ComboBox to the data
                    cmbCategory.DataSource = dt;
                    cmbCategory.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadItems(int CategoryID)
        {

            string query = "SELECT ItemID, ItemName FROM Items where CategoryID=@CategoryID"; // SQL Query to get categories

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("No items found for the selected category.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbItem.DataSource = null;
                            return;
                        }

                        cmbItem.DisplayMember = "ItemName"; // What will be displayed
                        cmbItem.ValueMember = "ItemID";   // What will be used as the value

                        // Bind the ComboBox to the data
                        cmbItem.DataSource = dt;
                        cmbItem.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbCategory.SelectedIndex != -1 && cmbCategory.SelectedValue != null)
            {
                int selectedCategoryID = Convert.ToInt32(cmbCategory.SelectedValue);
                LoadItems(selectedCategoryID);
            }

        }
        
    }
}

