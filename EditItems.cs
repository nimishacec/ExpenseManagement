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
    public partial class EditItems : Form
    {
        private string _connectionString = "Data Source=localhost\\SQLEXPRESS;Database=ExpenseDB;User ID=User;Password=SqlServer@123;";
        private int CategoryId;
        private int ItemId;
        public EditItems()
        {
            InitializeComponent();
        }
        public EditItems(int categoryId,int itemId)
        {
            CategoryId = categoryId;
            ItemId =itemId;
            InitializeComponent();
            LoadItems(categoryId,ItemId);
        }
        private void LoadItems(int categoryId,int ItemID)
        {
            string query = "SELECT *  FROM Items where CategoryID=@CategoryID and ItemID=@ItemID";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                    cmd.Parameters.AddWithValue("@ItemID", ItemID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtItem.Text = reader["ItemName"].ToString();
                            
                        }
                        else
                        {
                            txtItem.Text = "Category not found!";
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
            
                string itemname = txtItem.Text;
             
                string query = "Update Items set ItemName= @ItemName where CategoryID=@CategoryID and ItemID=@ItemID";

                try
                {
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {

                            cmd.Parameters.AddWithValue("@ItemName", itemname);
                            cmd.Parameters.AddWithValue("@ItemID", ItemId);
                            cmd.Parameters.AddWithValue("@CategoryID", CategoryId);
                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Items edited successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Failed to edit Item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

