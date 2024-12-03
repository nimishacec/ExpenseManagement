namespace ExpenseApp_Windows
{
    partial class ViewExpense
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
            this.btnViewByCategory = new System.Windows.Forms.Button();
            this.btnViewTotal = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnViewByCategory
            // 
            this.btnViewByCategory.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnViewByCategory.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewByCategory.ForeColor = System.Drawing.Color.White;
            this.btnViewByCategory.Location = new System.Drawing.Point(223, 127);
            this.btnViewByCategory.Name = "btnViewByCategory";
            this.btnViewByCategory.Size = new System.Drawing.Size(248, 51);
            this.btnViewByCategory.TabIndex = 0;
            this.btnViewByCategory.Text = "ViewByCategory";
            this.btnViewByCategory.UseVisualStyleBackColor = false;
            this.btnViewByCategory.Click += new System.EventHandler(this.btnViewByCategory_Click);
            // 
            // btnViewTotal
            // 
            this.btnViewTotal.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnViewTotal.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewTotal.ForeColor = System.Drawing.Color.White;
            this.btnViewTotal.Location = new System.Drawing.Point(223, 285);
            this.btnViewTotal.Name = "btnViewTotal";
            this.btnViewTotal.Size = new System.Drawing.Size(248, 59);
            this.btnViewTotal.TabIndex = 1;
            this.btnViewTotal.Text = "ViewTotalExpense";
            this.btnViewTotal.UseVisualStyleBackColor = false;
            this.btnViewTotal.Click += new System.EventHandler(this.btnViewTotal_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(316, 217);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "OR";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(257, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "View Expenses";
            // 
            // ViewExpense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnViewTotal);
            this.Controls.Add(this.btnViewByCategory);
            this.Name = "ViewExpense";
            this.Text = "ViewExpense";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnViewByCategory;
        private System.Windows.Forms.Button btnViewTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}