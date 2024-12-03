namespace ExpenseApp_Windows
{
    partial class ExpenseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.dataGridViewExpense = new System.Windows.Forms.DataGridView();
            this.expenseFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExpense)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.expenseFormBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.Location = new System.Drawing.Point(685, 392);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(119, 23);
            this.lblTotalAmount.TabIndex = 1;
            this.lblTotalAmount.Text = "TotalAmount";
            // 
            // dataGridViewExpense
            // 
            this.dataGridViewExpense.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewExpense.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewExpense.Location = new System.Drawing.Point(33, 48);
            this.dataGridViewExpense.Name = "dataGridViewExpense";
            this.dataGridViewExpense.RowHeadersWidth = 51;
            this.dataGridViewExpense.RowTemplate.Height = 24;
            this.dataGridViewExpense.Size = new System.Drawing.Size(1081, 367);
            this.dataGridViewExpense.TabIndex = 0;
            this.dataGridViewExpense.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewExpense_CellClick);
            // 
            // expenseFormBindingSource
            // 
            this.expenseFormBindingSource.DataSource = typeof(ExpenseApp_Windows.ExpenseForm);
            // 
            // ExpenseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 450);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.dataGridViewExpense);
            this.Name = "ExpenseForm";
            this.Text = "ExpenseForm";
            //this.Load += new System.EventHandler(this.ExpenseForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExpense)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.expenseFormBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.DataGridView dataGridViewExpense;
        private System.Windows.Forms.BindingSource expenseFormBindingSource;
    }
}