
namespace EPOS_APPLICATION_20230733
{
    partial class ProductCategoryList
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
            this.CategoryButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CategoryButton
            // 
            this.CategoryButton.Font = new System.Drawing.Font("Century Gothic", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoryButton.Image = global::EPOS_APPLICATION_20230733.Properties.Resources.tag;
            this.CategoryButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CategoryButton.Location = new System.Drawing.Point(3, 3);
            this.CategoryButton.Name = "CategoryButton";
            this.CategoryButton.Size = new System.Drawing.Size(124, 48);
            this.CategoryButton.TabIndex = 0;
            this.CategoryButton.Text = "Category 1";
            this.CategoryButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CategoryButton.UseVisualStyleBackColor = true;
            this.CategoryButton.Click += new System.EventHandler(this.CategoryButton_Click);
            // 
            // ProductCategoryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CategoryButton);
            this.Name = "ProductCategoryList";
            this.Size = new System.Drawing.Size(130, 65);           
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button CategoryButton;
    }
}
