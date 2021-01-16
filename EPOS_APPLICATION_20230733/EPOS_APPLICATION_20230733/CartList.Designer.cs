
namespace EPOS_APPLICATION_20230733
{
    partial class CartList
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ProductNameLabel = new System.Windows.Forms.Label();
            this.QuantityTextBox = new System.Windows.Forms.TextBox();
            this.AddButton = new System.Windows.Forms.Button();
            this.ProductPriceLabel = new System.Windows.Forms.Label();
            this.ProductTotalPriceLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DeleteProductButton = new System.Windows.Forms.Button();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProductNameLabel
            // 
            this.ProductNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductNameLabel.Location = new System.Drawing.Point(12, 15);
            this.ProductNameLabel.Name = "ProductNameLabel";
            this.ProductNameLabel.Size = new System.Drawing.Size(179, 20);
            this.ProductNameLabel.TabIndex = 1;
            this.ProductNameLabel.Text = "label";
            // 
            // QuantityTextBox
            // 
            this.QuantityTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantityTextBox.Location = new System.Drawing.Point(328, 12);
            this.QuantityTextBox.Name = "QuantityTextBox";
            this.QuantityTextBox.Size = new System.Drawing.Size(49, 28);
            this.QuantityTextBox.TabIndex = 2;
            this.QuantityTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.QuantityTextBox.Leave += new System.EventHandler(this.QuantityTextBox_Leave);
            // 
            // AddButton
            // 
            this.AddButton.FlatAppearance.BorderSize = 0;
            this.AddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddButton.Image = global::EPOS_APPLICATION_20230733.Properties.Resources.plus;
            this.AddButton.Location = new System.Drawing.Point(383, 11);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(30, 30);
            this.AddButton.TabIndex = 3;
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // ProductPriceLabel
            // 
            this.ProductPriceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductPriceLabel.Location = new System.Drawing.Point(206, 15);
            this.ProductPriceLabel.Name = "ProductPriceLabel";
            this.ProductPriceLabel.Size = new System.Drawing.Size(80, 20);
            this.ProductPriceLabel.TabIndex = 5;
            this.ProductPriceLabel.Text = "label";
            this.ProductPriceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProductTotalPriceLabel
            // 
            this.ProductTotalPriceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductTotalPriceLabel.Location = new System.Drawing.Point(419, 15);
            this.ProductTotalPriceLabel.Name = "ProductTotalPriceLabel";
            this.ProductTotalPriceLabel.Size = new System.Drawing.Size(109, 20);
            this.ProductTotalPriceLabel.TabIndex = 6;
            this.ProductTotalPriceLabel.Text = "label";
            this.ProductTotalPriceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.DeleteProductButton);
            this.panel1.Controls.Add(this.ProductTotalPriceLabel);
            this.panel1.Controls.Add(this.ProductPriceLabel);
            this.panel1.Controls.Add(this.AddButton);
            this.panel1.Controls.Add(this.QuantityTextBox);
            this.panel1.Controls.Add(this.ProductNameLabel);
            this.panel1.Controls.Add(this.RemoveButton);
            this.panel1.Location = new System.Drawing.Point(3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 49);
            this.panel1.TabIndex = 7;
            // 
            // DeleteProductButton
            // 
            this.DeleteProductButton.FlatAppearance.BorderSize = 0;
            this.DeleteProductButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteProductButton.Image = global::EPOS_APPLICATION_20230733.Properties.Resources.delete_2;
            this.DeleteProductButton.Location = new System.Drawing.Point(534, 3);
            this.DeleteProductButton.Name = "DeleteProductButton";
            this.DeleteProductButton.Size = new System.Drawing.Size(45, 43);
            this.DeleteProductButton.TabIndex = 4;
            this.DeleteProductButton.UseVisualStyleBackColor = true;
            this.DeleteProductButton.Click += new System.EventHandler(this.DeleteProductButton_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.FlatAppearance.BorderSize = 0;
            this.RemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemoveButton.ForeColor = System.Drawing.Color.Red;
            this.RemoveButton.Image = global::EPOS_APPLICATION_20230733.Properties.Resources.minus;
            this.RemoveButton.Location = new System.Drawing.Point(292, 12);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(30, 30);
            this.RemoveButton.TabIndex = 0;
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // CartList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "CartList";
            this.Size = new System.Drawing.Size(593, 56);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Label ProductNameLabel;
        private System.Windows.Forms.TextBox QuantityTextBox;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button DeleteProductButton;
        private System.Windows.Forms.Label ProductPriceLabel;
        private System.Windows.Forms.Label ProductTotalPriceLabel;
        private System.Windows.Forms.Panel panel1;
    }
}
