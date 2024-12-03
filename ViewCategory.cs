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
    public partial class ViewCategory : Form
    {
        private string _connectionString = "Data Source=localhost\\SQLEXPRESS;Database=ExpenseDB;User ID=User;Password=SqlServer@123;";


        public ViewCategory()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int categoryId = Convert.ToInt32(cmbCategory.SelectedValue);
            EditCategory editCategory = new EditCategory(categoryId);
            editCategory.Show();    
            this.Close();
          
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

        private void button2_Click(object sender, EventArgs e)
        {
            int categoryId = Convert.ToInt32(cmbCategory.SelectedValue);
         
            var confirmationResult = MessageBox.Show("Are you sure you want to delete this category?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmationResult == DialogResult.Yes)
                {
                   
                    string deleteQuery = "DELETE FROM Category WHERE CategoryID = @CategoryID";

                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        SqlCommand cmd = new SqlCommand(deleteQuery, conn);
                        cmd.Parameters.AddWithValue("@CategoryID", categoryId);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                   MessageBox.Show("Category deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                    this.DialogResult = DialogResult.OK;
                    this.Close();                

                }
            }
        }
    }

        
    


