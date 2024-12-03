using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseApp_Windows
{
    public partial class EditExpenseForm : Form
    {
        private int _expenseID;
        private string _connectionString = "Data Source=localhost\\SQLEXPRESS;Database=ExpenseDB;User ID=User;Password=SqlServer@123;";

        public EditExpenseForm()
        {
            InitializeComponent();

        }
        public EditExpenseForm(int expenseID, int categoryID, int itemID,string expensename, decimal quantity, decimal price, decimal expenseAmount, string expenseDate, string description)
        {
            InitializeComponent();
            _expenseID = expenseID;

            // Pre-fill the form with the existing values
           
            cmbCategory.SelectedValue = categoryID;
            LoadCategory(categoryID);
            LoadItem(itemID);
            cmbItem.SelectedValue = itemID;
           
            txtExpenseName.Text = expensename;
            txtQuantity.Text = quantity.ToString();
            txtPrice.Text = price.ToString();
            txtAmount.Text = expenseAmount.ToString();
            if (!string.IsNullOrEmpty(expenseDate))
            {
                dtpExpenseDate.Value = DateTime.ParseExact(expenseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            else
            {
                // Handle the case where expenseDate is null or empty, if needed
                dtpExpenseDate.Value = DateTime.Now; // Or assign a default date
            }

          //  dtpExpenseDate.Value = expenseDate.;
            txtDescrptn.Text = description;
           
        }
        private void EditExpenseForm_Load(object sender, EventArgs e)
        {
        }
           
        private void LoadCategory(int categoryID)
        {
            string query = "SELECT CategoryID, CategoryName FROM Category where CategoryID=@CategoryID";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CategoryID", categoryID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbCategory.DisplayMember = "CategoryName";
                    cmbCategory.ValueMember = "CategoryID";
                    cmbCategory.DataSource = dt;
                   // cmbCategory.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private void LoadItem(int categoryID)
        {
            string query = "SELECT ItemID, ItemName FROM Items WHERE ItemID = @CategoryID";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CategoryID", categoryID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbItem.DisplayMember = "ItemName";
                    cmbItem.ValueMember = "ItemID";
                    cmbItem.DataSource = dt;
                   // cmbItem.SelectedIndex = -1;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        //{
           

        //        if (cmbCategory.SelectedIndex != -1 && cmbCategory.SelectedValue != null)
        //        {
        //            int selectedCategoryID = Convert.ToInt32(cmbCategory.SelectedValue);
        //            LoadItem(selectedCategoryID);
        //        }

            
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            // Retrieve the new data from the form controls
            int categoryID = Convert.ToInt32(cmbCategory.SelectedValue);
            int itemID = Convert.ToInt32(cmbItem.SelectedValue);
            string expenseName = txtExpenseName.Text;
            decimal quantity = Convert.ToDecimal(txtQuantity.Text);
            decimal price = Convert.ToDecimal(txtPrice.Text);
            decimal amount = Convert.ToDecimal(txtAmount.Text);
            DateTime expenseDate = dtpExpenseDate.Value.Date;
            string description = txtDescrptn.Text;

            // Update the record in the database
            string updateQuery = "UPDATE Expenses SET ExpenseName=@ExpenseName,Quantity=@Quantity,Price=@Price, ExpenseAmount = @ExpenseAmount, ExpenseDate = @ExpenseDate, Description = @Description WHERE ExpenseID = @ExpenseID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@ExpenseID", _expenseID);
                cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                cmd.Parameters.AddWithValue("@ItemID", itemID);
                cmd.Parameters.AddWithValue("@ExpenseName", expenseName);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@ExpenseAmount", amount);
                cmd.Parameters.AddWithValue("@ExpenseDate", expenseDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@Description", description);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            // Close the edit form after saving
            MessageBox.Show("Expense updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Indicate success and close the form
            this.DialogResult = DialogResult.OK;
            this.Close();

        }
    }

}
