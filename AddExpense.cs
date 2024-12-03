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
    public partial class AddExpense : Form
    {
        private string _connectionString = "Data Source=localhost\\SQLEXPRESS;Database=ExpenseDB;User ID=User;Password=SqlServer@123;";
        public AddExpense()
        {
            InitializeComponent();
            LoadCategories();
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

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
        
        private void button1_Click(object sender, EventArgs e)
        {
            
                // Validate input
                if (string.IsNullOrWhiteSpace(cmbItem.SelectedItem.ToString())
                   || string.IsNullOrWhiteSpace(txtAmount.Text)|| string.IsNullOrWhiteSpace(txtExpenseName.Text)||
                    cmbCategory.SelectedIndex == -1)
                {
                    MessageBox.Show("Please fill all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
                {
                    MessageBox.Show("Please enter a valid amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get input values
                int categoryId = Convert.ToInt32(cmbCategory.SelectedValue);
                int itemId = Convert.ToInt32(cmbItem.SelectedValue);
                DateTime date = dateTimePicker1.Value;
            string description=txtDescrptn.Text;
            decimal quantity=decimal.Parse(txtQuantity.Text);
            decimal price=decimal.Parse(txtPrice.Text);
            string expensename=txtExpenseName.Text;
                // Connection string
               

                // SQL query to insert the expense
                string query = "INSERT INTO Expenses (CategoryID, ItemID,ExpenseName, ExpenseAmount, ExpenseDate, Description,CreatedDate,Quantity,Price) " +
                               "VALUES (@CategoryID, @ItemID,@ExpenseName, @Amount, @ExpenseDate,@Description,@CreatedDate,@Quantity,@Price)";

                try
                {
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Add parameters
                            cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                            cmd.Parameters.AddWithValue("@ItemID", itemId);
                        cmd.Parameters.AddWithValue("@ExpenseName", expensename);
                        cmd.Parameters.AddWithValue("@Amount", amount);
                            cmd.Parameters.AddWithValue("@ExpenseDate", date.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@CreatedDate", System.DateTime.Now);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Price", price);

                        // Open connection and execute query
                        conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();

                            // Confirm insertion
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Expense added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                            {
                                MessageBox.Show("Failed to add expense.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Optionally, reset fields after submission
                //txtItemName.Clear();
                //txtAmount.Clear();
                //cmbCategory.SelectedIndex = -1;
                //dtpDate.Value = DateTime.Now;
            
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

        private void cmbCategory_SelectedIndexChanged_1(object sender, EventArgs e)
        {
          
            if (cmbCategory.SelectedIndex != -1 && cmbCategory.SelectedValue != null)
            {
                int selectedCategoryID = Convert.ToInt32(cmbCategory.SelectedValue);
                LoadItems(selectedCategoryID);
            }
            
        }
    }
}
