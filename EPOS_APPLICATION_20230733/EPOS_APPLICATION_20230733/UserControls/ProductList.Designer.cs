
namespace EPOS_APPLICATION_20230733
{
    partial class ProductList
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
            this.ProductListPanel = new System.Windows.Forms.Panel();
            this.AddToCartButton = new System.Windows.Forms.Button();
            this.ProductPriceLabel = new System.Windows.Forms.Label();
            this.ProductNameLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ProductListPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ProductListPanel
            // 
            this.ProductListPanel.Controls.Add(this.AddToCartButton);
            this.ProductListPanel.Controls.Add(this.ProductPriceLabel);
            this.ProductListPanel.Controls.Add(this.ProductNameLabel);
            this.ProductListPanel.Controls.Add(this.pictureBox1);
            this.ProductListPanel.Location = new System.Drawing.Point(3, 3);
            this.ProductListPanel.Name = "ProductListPanel";
            this.ProductListPanel.Size = new System.Drawing.Size(138, 168);
            this.ProductListPanel.TabIndex = 0;
            // 
            // AddToCartButton
            // 
            this.AddToCartButton.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.AddToCartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddToCartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddToCartButton.ForeColor = System.Drawing.Color.Red;
            this.AddToCartButton.Image = global::EPOS_APPLICATION_20230733.Properties.Resources.cart;
            this.AddToCartButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AddToCartButton.Location = new System.Drawing.Point(26, 137);
            this.AddToCartButton.Name = "AddToCartButton";
            this.AddToCartButton.Size = new System.Drawing.Size(83, 31);
            this.AddToCartButton.TabIndex = 3;
            this.AddToCartButton.Text = "Add";
            this.AddToCartButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AddToCartButton.UseVisualStyleBackColor = true;
            this.AddToCartButton.Click += new System.EventHandler(this.AddToCartButton_Click);
            // 
            // ProductPriceLabel
            // 
            this.ProductPriceLabel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductPriceLabel.Location = new System.Drawing.Point(0, 108);
            this.ProductPriceLabel.Name = "ProductPriceLabel";
            this.ProductPriceLabel.Size = new System.Drawing.Size(131, 23);
            this.ProductPriceLabel.TabIndex = 2;
            this.ProductPriceLabel.Text = "label2";
            this.ProductPriceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProductNameLabel
            // 
            this.ProductNameLabel.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductNameLabel.Location = new System.Drawing.Point(-1, 0);
            this.ProductNameLabel.Name = "ProductNameLabel";
            this.ProductNameLabel.Size = new System.Drawing.Size(135, 49);
            this.ProductNameLabel.TabIndex = 1;
            this.ProductNameLabel.Text = "label1";
            this.ProductNameLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::EPOS_APPLICATION_20230733.Properties.Resources.Item;
            this.pictureBox1.Location = new System.Drawing.Point(36, 52);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(66, 53);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ProductList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ProductListPanel);
            this.Name = "ProductList";
            this.Size = new System.Drawing.Size(144, 171);
            this.ProductListPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ProductListPanel;
        private System.Windows.Forms.Button AddToCartButton;
        private System.Windows.Forms.Label ProductPriceLabel;
        private System.Windows.Forms.Label ProductNameLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
