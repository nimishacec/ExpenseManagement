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
    public partial class ViewExpense : Form
    {
        public ViewExpense()
        {
            InitializeComponent();
        }

        private void btnViewByCategory_Click(object sender, EventArgs e)
        {
            ViewExpenseByCategory viewExpense=new ViewExpenseByCategory();
            viewExpense.Show();
        }

        private void btnViewTotal_Click(object sender, EventArgs e)
        {
            ExpenseDetails expenseForm = new ExpenseDetails();
            expenseForm.Show();
        }
    }
}
