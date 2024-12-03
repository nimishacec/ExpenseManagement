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
    public partial class ExpenseForm : Form
    {
        private string _connectionString = "Data Source=localhost\\SQLEXPRESS;Database=ExpenseDB;User ID=User;Password=SqlServer@123;";

        private int _categoryID;
        private DateTime _startDate;
        private DateTime _endDate;
        public ExpenseForm()
        {
            InitializeComponent();
        }
        public ExpenseForm(DateTime startDate,DateTime endDate)
        {
            InitializeComponent();
            _startDate = startDate;
            _endDate = endDate;
            ViewExpenses(_startDate, _endDate);

        }
        // Constructor to receive parameters
        public ExpenseForm(int categoryID, DateTime startDate, DateTime endDate)
        {
            InitializeComponent();

            _categoryID = categoryID;
            _startDate = startDate;
            _endDate = endDate;
            FilterAndDisplayExpenses();

        }

        private void ViewExpenses(DateTime fromdate, DateTime todate)
        {
            
//            string query = @"SELECT 
//    e.ExpenseID,
//    c.CategoryName,
//    i.ItemName,   
//e.ExpenseName,
//    e.Quantity,
//    e.Price,  
//    e.ExpenseAmount,
//    e.ExpenseDate,
//    e.Description
    
// FROM 
//    Expenses e
// INNER JOIN 
//    Category c ON e.CategoryID = c.CategoryID
// INNER JOIN 
//    Items i ON e.ItemID = i.ItemID
// WHERE 
//    (@StartDate IS NULL OR CAST(e.ExpenseDate AS DATE) >= @StartDate)
//    AND (@EndDate IS NULL OR CAST(e.ExpenseDate AS DATE) <= @EndDate)
// ORDER BY 
//    e.ExpenseDate ASC";


            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetExpensesByFromAndToDate", conn))
                    {
                       cmd.CommandType= CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StartDate", _startDate);
                        cmd.Parameters.AddWithValue("@EndDate", _endDate);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        
                        dataGridViewExpense.DataSource = dt;
                        decimal totalAmount = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            totalAmount += Convert.ToDecimal(row["ExpenseAmount"]);
                        }
                        
                        lblTotalAmount.Text = "Total Expense Amount: " + totalAmount.ToString("C"); // Display as currency

                        AddEditDeleteButtons();

                       
                        dataGridViewExpense.Columns["ExpenseID"].HeaderText = "Expense ID";
                        dataGridViewExpense.Columns["ExpenseName"].HeaderText = "ExpenseName";
                        dataGridViewExpense.Columns["CategoryName"].HeaderText = "Category";
                        dataGridViewExpense.Columns["ItemName"].HeaderText = "ItemName";
                        dataGridViewExpense.Columns["Quantity"].HeaderText = "Quantity";
                        dataGridViewExpense.Columns["Price"].HeaderText = "Price";
                        dataGridViewExpense.Columns["ExpenseAmount"].HeaderText = "ExpenseAmount";
                        dataGridViewExpense.Columns["ExpenseDate"].HeaderText = "ExpenseDate";
                        dataGridViewExpense.Columns["Description"].HeaderText = "Description";
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("No expenses found for the selected criteria.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering expenses: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //private void ExpenseForm_Load(object sender, EventArgs e)
        //{
        //    FilterAndDisplayExpenses();
        //}

        private void FilterAndDisplayExpenses()
        {
            if (_categoryID == 0)
            {
                ViewExpenses(_startDate, _endDate);
            }
            else
            {
//                string query = @"SELECT 
//                e.ExpenseID,
//                c.CategoryName,
//                i.ItemName,  
//e.ExpenseName,
//                e.Quantity,
//                e.Price,  
//                e.ExpenseAmount,
//                e.ExpenseDate,
//                e.Description
                
//             FROM 
//                Expenses e
//             INNER JOIN 
//                Category c ON e.CategoryID = c.CategoryID
//             INNER JOIN 
//                Items i ON e.ItemID = i.ItemID
//             WHERE 
//                e.CategoryID = @CategoryID AND (
//        (CAST(e.ExpenseDate AS DATE) BETWEEN @StartDate AND @EndDate)
//    )
//             ORDER BY 
//                e.ExpenseDate DESC";

                try
                {
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("SP_GetExpensesByCategoryAndDate", conn))
                        {
                            cmd.CommandType= CommandType.StoredProcedure;
                            // Add parameters to prevent SQL injection
                            cmd.Parameters.AddWithValue("@CategoryID", _categoryID);
                            cmd.Parameters.AddWithValue("@StartDate", _startDate);
                            cmd.Parameters.AddWithValue("@EndDate", _endDate);

                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            // Bind the data to the DataGridView
                            dataGridViewExpense.DataSource = dt;
                            decimal totalAmount = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                totalAmount += Convert.ToDecimal(row["ExpenseAmount"]);
                            }

                            // Display total below the DataGridView
                            lblTotalAmount.Text = "Total Expense Amount: " + totalAmount.ToString("C"); // Display as currency

                            AddEditDeleteButtons();

                            // Optional: Customize column names
                            dataGridViewExpense.Columns["ExpenseID"].HeaderText = "Expense ID";
                            dataGridViewExpense.Columns["ExpenseName"].HeaderText = "ExpenseName";
                            dataGridViewExpense.Columns["CategoryName"].HeaderText = "Category";
                            dataGridViewExpense.Columns["ItemName"].HeaderText = "ItemName";
                            dataGridViewExpense.Columns["Quantity"].HeaderText = "Quantity";
                            dataGridViewExpense.Columns["Price"].HeaderText = "Price";
                            dataGridViewExpense.Columns["ExpenseAmount"].HeaderText = "ExpenseAmount";
                            dataGridViewExpense.Columns["ExpenseDate"].HeaderText = "ExpenseDate";
                            dataGridViewExpense.Columns["Description"].HeaderText = "Description";
                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("No expenses found for the selected criteria.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error filtering expenses: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private void AddEditDeleteButtons()
        {
           
                // Check if the Edit button column already exists
                if (dataGridViewExpense.Columns["EditButton"] == null)
                {
                    // Add Edit button
                    DataGridViewButtonColumn editButton = new DataGridViewButtonColumn
                    {
                        Name = "EditButton",
                        HeaderText = "Edit",
                        Text = "Edit",
                        UseColumnTextForButtonValue = true
                    };
                dataGridViewExpense.Columns.Add(editButton);
                }

                // Check if the Delete button column already exists
                if (dataGridViewExpense.Columns["DeleteButton"] == null)
                {
                    // Add Delete button
                    DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn
                    {
                        Name = "DeleteButton",
                        HeaderText = "Delete",
                        Text = "Delete",
                        UseColumnTextForButtonValue = true
                    };
                dataGridViewExpense.Columns.Add(deleteButton);
                }
            

            
        }


        private void dataGridViewExpense_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Check if the clicked cell is an Edit button
                if (dataGridViewExpense.Columns[e.ColumnIndex].HeaderText == "Edit")
                {
                    // Get the ExpenseID from the current row
                    int expenseID = Convert.ToInt32(dataGridViewExpense.Rows[e.RowIndex].Cells["ExpenseID"].Value);
                    // Open the edit form or functionality with the selected ExpenseID
                    EditExpense(expenseID);
                }

                // Check if the clicked cell is a Delete button
                if (dataGridViewExpense.Columns[e.ColumnIndex].HeaderText == "Delete")
                {
                    // Get the ExpenseID from the current row
                    int expenseID = Convert.ToInt32(dataGridViewExpense.Rows[e.RowIndex].Cells["ExpenseID"].Value);
                    // Delete the expense record
                    DeleteExpense(expenseID);
                    FilterAndDisplayExpenses();
                }
            }
        }
        private void DeleteExpense(int expenseID)
        {
            // Ask for confirmation before deleting
            var confirmationResult = MessageBox.Show("Are you sure you want to delete this expense?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmationResult == DialogResult.Yes)
            {
                // Delete the record from the database
                string deleteQuery = "DELETE FROM Expenses WHERE ExpenseID = @ExpenseID";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(deleteQuery, conn);
                    cmd.Parameters.AddWithValue("@ExpenseID", expenseID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Expense deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Indicate success and close the form
                this.DialogResult = DialogResult.OK;
              
                    // Refresh the data after editing
                    FilterAndDisplayExpenses();
                
                // Optionally, you can refresh the DataGridView to reflect the changes

            }
        }
        private void EditExpense(int expenseID)
        {
            // Fetch the expense details from the database based on ExpenseID
            string query = "SELECT e.ExpenseID, e.CategoryID, e.ItemID,e.ExpenseName, e.ExpenseAmount, e.ExpenseDate, e.Description, " +
                           "c.CategoryName, i.ItemName, e.Quantity, e.Price " +
                           "FROM Expenses e " +
                           "JOIN Category c ON e.CategoryID = c.CategoryID " +
                           "JOIN Items i ON e.ItemID = i.ItemID " +
                           "WHERE e.ExpenseID = @ExpenseID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ExpenseID", expenseID);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Get the data from the database
                    int categoryID = reader["CategoryID"]!=DBNull.Value ? Convert.ToInt32(reader["CategoryID"]): 0;
                    int itemID = reader["ItemID"] != DBNull.Value ? Convert.ToInt32(reader["ItemID"]) : 0;
                    decimal quantity = reader["Quantity"] != DBNull.Value ? Convert.ToDecimal(reader["Quantity"]) : 0;
                    decimal price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0; 
                    decimal expenseAmount = reader["ExpenseAmount"] != DBNull.Value ? Convert.ToDecimal(reader["ExpenseAmount"]) : 0;
                    string expenseDate = reader["ExpenseDate"] != DBNull.Value ? Convert.ToDateTime(reader["ExpenseDate"]).ToString("yyyy-MM-dd") : null;


                    string description = reader["Description"] != DBNull.Value ? (reader["Description"]).ToString() : null;
                    string expensename = reader["ExpenseName"] != DBNull.Value ? reader["ExpenseName"].ToString() : null;
                    // Pass the data to the EditExpenseForm (you need to create this form)
                    EditExpenseForm editForm = new EditExpenseForm(expenseID, categoryID, itemID, expensename,quantity,price,expenseAmount, expenseDate, description);
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        // Refresh the data after editing
                        FilterAndDisplayExpenses();
                    }
                }
            }
        }

    }
}
