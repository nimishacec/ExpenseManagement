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
    public partial class ViewExpenseByCategory : Form
    {
        private string _connectionString = "Data Source=localhost\\SQLEXPRESS;Database=ExpenseDB;User ID=User;Password=SqlServer@123;";

        public ViewExpenseByCategory()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbCategory.SelectedValue == null)
            {
                MessageBox.Show("Please select a valid category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (FromDate.Value.Date > ToDate.Value.Date)
            {
                MessageBox.Show("Start date cannot be later than the end date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int categoryID = Convert.ToInt32(cmbCategory.SelectedValue); // Selected CategoryID
            string startDate = FromDate.Value.ToString("yyyy-MM-dd"); // Start Date
            string endDate = ToDate.Value.ToString("yyyy-MM-dd"); // End Date
           
            // Retrieve the selected category ID, start date, and end date
          

            // Open ExpenseForm and pass the values
            ExpenseForm expenseForm = new ExpenseForm(categoryID, FromDate.Value.Date, ToDate.Value.Date);
            expenseForm.Show();

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
     

    }
}
