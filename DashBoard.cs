using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseApp_Windows
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddExpense addExpense = new AddExpense();
            addExpense.Show();
        }

        private void btnViewExpense_Click(object sender, EventArgs e)
        {
            ViewExpense viewExpense = new ViewExpense();
            viewExpense.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Report_Expense expenseReport = new Report_Expense();
            expenseReport.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddCategory expense = new AddCategory();
            expense.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddItems addItems = new AddItems();
            addItems.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ViewCategory viewCategory = new ViewCategory();
            viewCategory.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ViewItems viewItems = new ViewItems();
            viewItems.Show();
        }
    }
}
