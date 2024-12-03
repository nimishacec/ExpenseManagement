using System;
using System.Collections;
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
    public partial class Report_Expense : Form
    {
        private string _connectionString = "Data Source=localhost\\SQLEXPRESS;Database=ExpenseDB;User ID=User;Password=SqlServer@123;";


        public Report_Expense()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePicker1.Value; // Get selected date from DateTimePicker
            DateTime endDate = dateTimePicker2.Value;   // Optional: Add another DateTimePicker for the end date

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetExpensesByDate", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters with proper handling for nulls
                        cmd.Parameters.AddWithValue("@StartDate", (object)startDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@EndDate", (object)endDate ?? DBNull.Value);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            // Load data into Crystal Report
                            ExpenseReport expenseReport = new ExpenseReport();
                            expenseReport.SetDataSource(dt);
                            crystalReportViewer1.ReportSource = expenseReport;
                            crystalReportViewer1.Refresh();
                        }
                        else
                        {
                            MessageBox.Show("No records found for the selected date range.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
