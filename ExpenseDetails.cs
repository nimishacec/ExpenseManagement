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
    public partial class ExpenseDetails : Form
    {
        private string _connectionString = "Data Source=localhost\\SQLEXPRESS;Database=ExpenseDB;User ID=User;Password=SqlServer@123;";

        private int _categoryID;
        private DateTime _startDate;
        private DateTime _endDate;
        public ExpenseDetails()
        {
            InitializeComponent();
        }

        //public ExpenseDetails()
        //{
        //    InitializeComponent();

        //    //_categoryID = categoryID;
        //    //_startDate = startDate;
        //    //_endDate = endDate;
        //}


 //       private void ExpenseDetails_Load(object sender, EventArgs e)
 //       {
 //           //DateTime fromdate=FromDate.Value.Date;
 //           //DateTime todate=ToDate.Value.Date;
 //           FilterAndDisplayExpenses(null,DBNull);
 //       }

 //       private void FilterAndDisplayExpenses( DateTime fromdate, DateTime todate)
 //       {
 //           // End Date

 //           // Validation: Start Date must not be greater than End Date


 //           string query = @"SELECT 
 //   e.ExpenseID,
 //   c.CategoryName,
 //   i.ItemName,              
 //   e.Quantity,
 //   e.Price,  
 //   e.ExpenseAmount,
 //   e.ExpenseDate,
 //   e.Description
    
 //FROM 
 //   Expenses e
 //INNER JOIN 
 //   Category c ON e.CategoryID = c.CategoryID
 //INNER JOIN 
 //   Items i ON e.ItemID = i.ItemID
 //WHERE 
 //   (@StartDate IS NULL OR CAST(e.ExpenseDate AS DATE) >= @StartDate)
 //   AND (@EndDate IS NULL OR CAST(e.ExpenseDate AS DATE) <= @EndDate)
 //ORDER BY 
 //   e.ExpenseDate DESC";


 //           try
 //           {
 //               using (SqlConnection conn = new SqlConnection(_connectionString))
 //               {
 //                   using (SqlCommand cmd = new SqlCommand(query, conn))
 //                   {
 //                       // Add parameters to prevent SQL injection
 //                       cmd.Parameters.AddWithValue("@CategoryID", _categoryID);
 //                       cmd.Parameters.AddWithValue("@StartDate", _startDate);
 //                       cmd.Parameters.AddWithValue("@EndDate", _endDate);

 //                       SqlDataAdapter da = new SqlDataAdapter(cmd);
 //                       DataTable dt = new DataTable();
 //                       da.Fill(dt);

 //                       // Bind the data to the DataGridView
 //                       dataGridView.DataSource = dt;
 //                       decimal totalAmount = 0;
 //                       foreach (DataRow row in dt.Rows)
 //                       {
 //                           totalAmount += Convert.ToDecimal(row["ExpenseAmount"]);
 //                       }

 //                       // Display total below the DataGridView
 //                       lblTotalExpense.Text = "Total Expense Amount: " + totalAmount.ToString("C"); // Display as currency

 //                       AddEditDeleteButtons();

 //                       // Optional: Customize column names
 //                       dataGridView.Columns["ExpenseID"].HeaderText = "Expense ID";
 //                       dataGridView.Columns["CategoryName"].HeaderText = "Category";
 //                       dataGridView.Columns["ItemName"].HeaderText = "ItemName";
 //                       dataGridView.Columns["ItemName"].HeaderText = "Quantity";
 //                       dataGridView.Columns["ItemName"].HeaderText = "Price";
 //                       dataGridView.Columns["ExpenseAmount"].HeaderText = "ExpenseAmount";
 //                       dataGridView.Columns["ExpenseDate"].HeaderText = "ExpenseDate";
 //                       dataGridView.Columns["Description"].HeaderText = "Description";
 //                       if (dt.Rows.Count == 0)
 //                       {
 //                           MessageBox.Show("No expenses found for the selected criteria.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
 //                       }
 //                   }
 //               }
 //           }
 //           catch (Exception ex)
 //           {
 //               MessageBox.Show("Error filtering expenses: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
 //           }

 //       }
 //       private void AddEditDeleteButtons()
 //       {

 //           // Check if the Edit button column already exists
 //           if (dataGridView.Columns["EditButton"] == null)
 //           {
 //               // Add Edit button
 //               DataGridViewButtonColumn editButton = new DataGridViewButtonColumn
 //               {
 //                   Name = "EditButton",
 //                   HeaderText = "Edit",
 //                   Text = "Edit",
 //                   UseColumnTextForButtonValue = true
 //               };
 //               dataGridView.Columns.Add(editButton);
 //           }

 //           // Check if the Delete button column already exists
 //           if (dataGridView.Columns["DeleteButton"] == null)
 //           {
 //               // Add Delete button
 //               DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn
 //               {
 //                   Name = "DeleteButton",
 //                   HeaderText = "Delete",
 //                   Text = "Delete",
 //                   UseColumnTextForButtonValue = true
 //               };
 //               dataGridView.Columns.Add(deleteButton);
 //           }



 //       }


 //       private void dataGridViewExpense_CellClick(object sender, DataGridViewCellEventArgs e)
 //       {
 //           if (e.RowIndex >= 0)
 //           {
 //               // Check if the clicked cell is an Edit button
 //               if (dataGridView.Columns[e.ColumnIndex].HeaderText == "Edit")
 //               {
 //                   // Get the ExpenseID from the current row
 //                   int expenseID = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["ExpenseID"].Value);
 //                   // Open the edit form or functionality with the selected ExpenseID
 //                   EditExpense(expenseID);
 //               }

 //               // Check if the clicked cell is a Delete button
 //               if (dataGridView.Columns[e.ColumnIndex].HeaderText == "Delete")
 //               {
 //                   // Get the ExpenseID from the current row
 //                   int expenseID = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["ExpenseID"].Value);
 //                   // Delete the expense record
 //                   DeleteExpense(expenseID);
 //                   FilterAndDisplayExpenses();
 //               }
 //           }
 //       }
 //       private void DeleteExpense(int expenseID)
 //       {
 //           // Ask for confirmation before deleting
 //           var confirmationResult = MessageBox.Show("Are you sure you want to delete this expense?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

 //           if (confirmationResult == DialogResult.Yes)
 //           {
 //               // Delete the record from the database
 //               string deleteQuery = "DELETE FROM Expenses WHERE ExpenseID = @ExpenseID";

 //               using (SqlConnection conn = new SqlConnection(_connectionString))
 //               {
 //                   SqlCommand cmd = new SqlCommand(deleteQuery, conn);
 //                   cmd.Parameters.AddWithValue("@ExpenseID", expenseID);

 //                   conn.Open();
 //                   cmd.ExecuteNonQuery();
 //               }
 //               MessageBox.Show("Expense deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

 //               // Indicate success and close the form
 //               this.DialogResult = DialogResult.OK;

 //               // Refresh the data after editing
 //               FilterAndDisplayExpenses();

 //               // Optionally, you can refresh the DataGridView to reflect the changes

 //           }
 //       }
 //       private void EditExpense(int expenseID)
 //       {
 //           // Fetch the expense details from the database based on ExpenseID
 //           string query = "SELECT e.ExpenseID, e.CategoryID, e.ItemID,e.ExpenseName, e.ExpenseAmount, e.ExpenseDate, e.Description, " +
 //                          "c.CategoryName, i.ItemName, e.Quantity, e.Price " +
 //                          "FROM Expenses e " +
 //                          "JOIN Category c ON e.CategoryID = c.CategoryID " +
 //                          "JOIN Items i ON e.ItemID = i.ItemID " +
 //                          "WHERE e.ExpenseID = @ExpenseID";

 //           using (SqlConnection conn = new SqlConnection(_connectionString))
 //           {
 //               SqlCommand cmd = new SqlCommand(query, conn);
 //               cmd.Parameters.AddWithValue("@ExpenseID", expenseID);
 //               conn.Open();

 //               SqlDataReader reader = cmd.ExecuteReader();
 //               if (reader.Read())
 //               {
 //                   // Get the data from the database
 //                   int categoryID = reader.GetInt32(reader.GetOrdinal("CategoryID"));
 //                   int itemID = reader.GetInt32(reader.GetOrdinal("ItemID"));
 //                   decimal quantity = reader.GetDecimal(reader.GetOrdinal("Quantity"));
 //                   decimal price = reader.GetDecimal(reader.GetOrdinal("Price"));
 //                   decimal expenseAmount = reader.GetDecimal(reader.GetOrdinal("ExpenseAmount"));
 //                   DateTime expenseDate = reader.GetDateTime(reader.GetOrdinal("ExpenseDate"));
 //                   string description = reader.GetString(reader.GetOrdinal("Description"));
 //                   string expensename = reader.GetString(reader.GetOrdinal("ExpenseName"));
 //                   // Pass the data to the EditExpenseForm (you need to create this form)
 //                   EditExpenseForm editForm = new EditExpenseForm(expenseID, categoryID, itemID, expensename, quantity, price, expenseAmount, expenseDate, description);
 //                   if (editForm.ShowDialog() == DialogResult.OK)
 //                   {
 //                       // Refresh the data after editing
 //                       FilterAndDisplayExpenses();
 //                   }
 //               }
 //           }
 //       }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fromdate = FromDate.Value.Date;
            DateTime todate = ToDate.Value.Date;
            if (fromdate != null && todate != null)
            {
                ExpenseForm expenseForm = new ExpenseForm(fromdate, todate);
                expenseForm.Show();
            }
            else
            {
                ExpenseForm expenseForm= new ExpenseForm();
                expenseForm.Show();
            }
        }
    }
}
