
namespace EPOS_APPLICATION_20230733
{
    partial class Reports
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ExitButton = new System.Windows.Forms.Button();
            this.GenerateTodaysReportButton = new System.Windows.Forms.Button();
            this.TodaySaleButton = new System.Windows.Forms.Button();
            this.SummaryButton = new System.Windows.Forms.Button();
            this.LiveStockButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SummaryGroupBox = new System.Windows.Forms.GroupBox();
            this.AverageSaleLabel = new System.Windows.Forms.Label();
            this.TotalItemsLabel = new System.Windows.Forms.Label();
            this.TotalSaleLabel = new System.Windows.Forms.Label();
            this.TotalTransactionsLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LiveStockGroupBox = new System.Windows.Forms.GroupBox();
            this.TotalCategoriesLabel = new System.Windows.Forms.Label();
            this.TotalProductsLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LiveStockListView = new System.Windows.Forms.ListView();
            this.TodaysSaleGroupBox = new System.Windows.Forms.GroupBox();
            this.TodaySaleListView = new System.Windows.Forms.ListView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TodaySaleLabel = new System.Windows.Forms.Label();
            this.DownloadLiveReportButton = new System.Windows.Forms.Button();
            this.LargestSaleLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.SmallestSaleLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SummaryGroupBox.SuspendLayout();
            this.LiveStockGroupBox.SuspendLayout();
            this.TodaysSaleGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(161, 772);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.ExitButton);
            this.panel2.Controls.Add(this.TodaySaleButton);
            this.panel2.Controls.Add(this.SummaryButton);
            this.panel2.Controls.Add(this.LiveStockButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(161, 772);
            this.panel2.TabIndex = 2;
            // 
            // ExitButton
            // 
            this.ExitButton.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitButton.Location = new System.Drawing.Point(3, 407);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(155, 94);
            this.ExitButton.TabIndex = 4;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.Exitbutton_Click);
            // 
            // GenerateTodaysReportButton
            // 
            this.GenerateTodaysReportButton.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerateTodaysReportButton.Location = new System.Drawing.Point(462, 700);
            this.GenerateTodaysReportButton.Name = "GenerateTodaysReportButton";
            this.GenerateTodaysReportButton.Size = new System.Drawing.Size(148, 50);
            this.GenerateTodaysReportButton.TabIndex = 3;
            this.GenerateTodaysReportButton.Text = "Download Report";
            this.GenerateTodaysReportButton.UseVisualStyleBackColor = true;
            this.GenerateTodaysReportButton.Click += new System.EventHandler(this.GenerateReportButton_Click);
            // 
            // TodaySaleButton
            // 
            this.TodaySaleButton.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TodaySaleButton.Location = new System.Drawing.Point(3, 307);
            this.TodaySaleButton.Name = "TodaySaleButton";
            this.TodaySaleButton.Size = new System.Drawing.Size(155, 94);
            this.TodaySaleButton.TabIndex = 2;
            this.TodaySaleButton.Text = "Today\'s Sale";
            this.TodaySaleButton.UseVisualStyleBackColor = true;
            this.TodaySaleButton.Click += new System.EventHandler(this.TodaySaleButton_Click);
            // 
            // SummaryButton
            // 
            this.SummaryButton.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SummaryButton.Location = new System.Drawing.Point(2, 207);
            this.SummaryButton.Name = "SummaryButton";
            this.SummaryButton.Size = new System.Drawing.Size(155, 94);
            this.SummaryButton.TabIndex = 1;
            this.SummaryButton.Text = "Summary";
            this.SummaryButton.UseVisualStyleBackColor = true;
            this.SummaryButton.Click += new System.EventHandler(this.SummaryButton_Click);
            // 
            // LiveStockButton
            // 
            this.LiveStockButton.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LiveStockButton.Location = new System.Drawing.Point(3, 107);
            this.LiveStockButton.Name = "LiveStockButton";
            this.LiveStockButton.Size = new System.Drawing.Size(155, 94);
            this.LiveStockButton.TabIndex = 0;
            this.LiveStockButton.Text = "Live Stock";
            this.LiveStockButton.UseVisualStyleBackColor = true;
            this.LiveStockButton.Click += new System.EventHandler(this.LiveStockButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 103);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(155, 94);
            this.button2.TabIndex = 1;
            this.button2.Text = "Summary";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 94);
            this.button1.TabIndex = 0;
            this.button1.Text = "Live Stock";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // SummaryGroupBox
            // 
            this.SummaryGroupBox.Controls.Add(this.SmallestSaleLabel);
            this.SummaryGroupBox.Controls.Add(this.label12);
            this.SummaryGroupBox.Controls.Add(this.LargestSaleLabel);
            this.SummaryGroupBox.Controls.Add(this.label10);
            this.SummaryGroupBox.Controls.Add(this.AverageSaleLabel);
            this.SummaryGroupBox.Controls.Add(this.TotalItemsLabel);
            this.SummaryGroupBox.Controls.Add(this.TotalSaleLabel);
            this.SummaryGroupBox.Controls.Add(this.TotalTransactionsLabel);
            this.SummaryGroupBox.Controls.Add(this.label6);
            this.SummaryGroupBox.Controls.Add(this.label5);
            this.SummaryGroupBox.Controls.Add(this.label4);
            this.SummaryGroupBox.Controls.Add(this.label3);
            this.SummaryGroupBox.Location = new System.Drawing.Point(179, 3);
            this.SummaryGroupBox.Name = "SummaryGroupBox";
            this.SummaryGroupBox.Size = new System.Drawing.Size(983, 761);
            this.SummaryGroupBox.TabIndex = 1;
            this.SummaryGroupBox.TabStop = false;
            this.SummaryGroupBox.Text = "Summary";
            // 
            // AverageSaleLabel
            // 
            this.AverageSaleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AverageSaleLabel.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AverageSaleLabel.Location = new System.Drawing.Point(388, 217);
            this.AverageSaleLabel.Name = "AverageSaleLabel";
            this.AverageSaleLabel.Size = new System.Drawing.Size(193, 23);
            this.AverageSaleLabel.TabIndex = 7;
            this.AverageSaleLabel.Text = "label";
            this.AverageSaleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TotalItemsLabel
            // 
            this.TotalItemsLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TotalItemsLabel.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalItemsLabel.Location = new System.Drawing.Point(388, 169);
            this.TotalItemsLabel.Name = "TotalItemsLabel";
            this.TotalItemsLabel.Size = new System.Drawing.Size(193, 23);
            this.TotalItemsLabel.TabIndex = 6;
            this.TotalItemsLabel.Text = "label";
            this.TotalItemsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TotalSaleLabel
            // 
            this.TotalSaleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TotalSaleLabel.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalSaleLabel.Location = new System.Drawing.Point(388, 121);
            this.TotalSaleLabel.Name = "TotalSaleLabel";
            this.TotalSaleLabel.Size = new System.Drawing.Size(193, 23);
            this.TotalSaleLabel.TabIndex = 5;
            this.TotalSaleLabel.Text = "label";
            this.TotalSaleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TotalTransactionsLabel
            // 
            this.TotalTransactionsLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TotalTransactionsLabel.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalTransactionsLabel.Location = new System.Drawing.Point(388, 73);
            this.TotalTransactionsLabel.Name = "TotalTransactionsLabel";
            this.TotalTransactionsLabel.Size = new System.Drawing.Size(193, 23);
            this.TotalTransactionsLabel.TabIndex = 4;
            this.TotalTransactionsLabel.Text = "label";
            this.TotalTransactionsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(264, 218);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 19);
            this.label6.TabIndex = 3;
            this.label6.Text = "Average Sale";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(248, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 19);
            this.label5.TabIndex = 2;
            this.label5.Text = "Total Items Sold";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(285, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 19);
            this.label4.TabIndex = 1;
            this.label4.Text = "Total Sale";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(231, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "Total Transactions";
            // 
            // LiveStockGroupBox
            // 
            this.LiveStockGroupBox.Controls.Add(this.DownloadLiveReportButton);
            this.LiveStockGroupBox.Controls.Add(this.TotalCategoriesLabel);
            this.LiveStockGroupBox.Controls.Add(this.TotalProductsLabel);
            this.LiveStockGroupBox.Controls.Add(this.label2);
            this.LiveStockGroupBox.Controls.Add(this.label1);
            this.LiveStockGroupBox.Controls.Add(this.LiveStockListView);
            this.LiveStockGroupBox.Location = new System.Drawing.Point(164, 24);
            this.LiveStockGroupBox.Name = "LiveStockGroupBox";
            this.LiveStockGroupBox.Size = new System.Drawing.Size(998, 748);
            this.LiveStockGroupBox.TabIndex = 0;
            this.LiveStockGroupBox.TabStop = false;
            this.LiveStockGroupBox.Text = "Live Stock";
            // 
            // TotalCategoriesLabel
            // 
            this.TotalCategoriesLabel.AutoSize = true;
            this.TotalCategoriesLabel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalCategoriesLabel.Location = new System.Drawing.Point(282, 614);
            this.TotalCategoriesLabel.Name = "TotalCategoriesLabel";
            this.TotalCategoriesLabel.Size = new System.Drawing.Size(16, 18);
            this.TotalCategoriesLabel.TabIndex = 5;
            this.TotalCategoriesLabel.Text = "0";
            this.TotalCategoriesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TotalProductsLabel
            // 
            this.TotalProductsLabel.AutoSize = true;
            this.TotalProductsLabel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalProductsLabel.Location = new System.Drawing.Point(780, 614);
            this.TotalProductsLabel.Name = "TotalProductsLabel";
            this.TotalProductsLabel.Size = new System.Drawing.Size(16, 18);
            this.TotalProductsLabel.TabIndex = 4;
            this.TotalProductsLabel.Text = "0";
            this.TotalProductsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(642, 614);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Total Products";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(137, 614);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Total Categories";
            // 
            // LiveStockListView
            // 
            this.LiveStockListView.HideSelection = false;
            this.LiveStockListView.Location = new System.Drawing.Point(139, 23);
            this.LiveStockListView.Name = "LiveStockListView";
            this.LiveStockListView.Size = new System.Drawing.Size(697, 570);
            this.LiveStockListView.TabIndex = 0;
            this.LiveStockListView.UseCompatibleStateImageBehavior = false;
            // 
            // TodaysSaleGroupBox
            // 
            this.TodaysSaleGroupBox.Controls.Add(this.GenerateTodaysReportButton);
            this.TodaysSaleGroupBox.Controls.Add(this.TodaySaleLabel);
            this.TodaysSaleGroupBox.Controls.Add(this.label7);
            this.TodaysSaleGroupBox.Controls.Add(this.TodaySaleListView);
            this.TodaysSaleGroupBox.Location = new System.Drawing.Point(170, 12);
            this.TodaysSaleGroupBox.Name = "TodaysSaleGroupBox";
            this.TodaysSaleGroupBox.Size = new System.Drawing.Size(986, 754);
            this.TodaysSaleGroupBox.TabIndex = 2;
            this.TodaysSaleGroupBox.TabStop = false;
            this.TodaysSaleGroupBox.Text = "Today\'s Sale";
            // 
            // TodaySaleListView
            // 
            this.TodaySaleListView.HideSelection = false;
            this.TodaySaleListView.Location = new System.Drawing.Point(196, 30);
            this.TodaySaleListView.Name = "TodaySaleListView";
            this.TodaySaleListView.Size = new System.Drawing.Size(701, 616);
            this.TodaySaleListView.TabIndex = 0;
            this.TodaySaleListView.UseCompatibleStateImageBehavior = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::EPOS_APPLICATION_20230733.Properties.Resources.LOGO;
            this.pictureBox1.Location = new System.Drawing.Point(4, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(153, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(196, 667);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 19);
            this.label7.TabIndex = 1;
            this.label7.Text = "Today\'s Sale:";
            // 
            // TodaySaleLabel
            // 
            this.TodaySaleLabel.AutoSize = true;
            this.TodaySaleLabel.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TodaySaleLabel.Location = new System.Drawing.Point(330, 667);
            this.TodaySaleLabel.Name = "TodaySaleLabel";
            this.TodaySaleLabel.Size = new System.Drawing.Size(18, 19);
            this.TodaySaleLabel.TabIndex = 2;
            this.TodaySaleLabel.Text = "S";
            // 
            // DownloadLiveReportButton
            // 
            this.DownloadLiveReportButton.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownloadLiveReportButton.Location = new System.Drawing.Point(403, 673);
            this.DownloadLiveReportButton.Name = "DownloadLiveReportButton";
            this.DownloadLiveReportButton.Size = new System.Drawing.Size(148, 50);
            this.DownloadLiveReportButton.TabIndex = 6;
            this.DownloadLiveReportButton.Text = "Download Report";
            this.DownloadLiveReportButton.UseVisualStyleBackColor = true;
            this.DownloadLiveReportButton.Click += new System.EventHandler(this.DownloadLiveReportButton_Click);
            // 
            // LargestSaleLabel
            // 
            this.LargestSaleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LargestSaleLabel.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LargestSaleLabel.Location = new System.Drawing.Point(388, 265);
            this.LargestSaleLabel.Name = "LargestSaleLabel";
            this.LargestSaleLabel.Size = new System.Drawing.Size(193, 23);
            this.LargestSaleLabel.TabIndex = 9;
            this.LargestSaleLabel.Text = "label";
            this.LargestSaleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(269, 266);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 19);
            this.label10.TabIndex = 8;
            this.label10.Text = "Largest Sale";
            // 
            // SmallestSaleLabel
            // 
            this.SmallestSaleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SmallestSaleLabel.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SmallestSaleLabel.Location = new System.Drawing.Point(388, 313);
            this.SmallestSaleLabel.Name = "SmallestSaleLabel";
            this.SmallestSaleLabel.Size = new System.Drawing.Size(193, 23);
            this.SmallestSaleLabel.TabIndex = 11;
            this.SmallestSaleLabel.Text = "label";
            this.SmallestSaleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(264, 314);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 19);
            this.label12.TabIndex = 10;
            this.label12.Text = "Smallest Sale";
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 772);
            this.Controls.Add(this.LiveStockGroupBox);
            this.Controls.Add(this.TodaysSaleGroupBox);
            this.Controls.Add(this.SummaryGroupBox);
            this.Controls.Add(this.panel1);
            this.Name = "Reports";
            this.Text = "Reports";
            this.Load += new System.EventHandler(this.Reports_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.SummaryGroupBox.ResumeLayout(false);
            this.SummaryGroupBox.PerformLayout();
            this.LiveStockGroupBox.ResumeLayout(false);
            this.LiveStockGroupBox.PerformLayout();
            this.TodaysSaleGroupBox.ResumeLayout(false);
            this.TodaysSaleGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button TodaySaleButton;
        private System.Windows.Forms.Button SummaryButton;
        private System.Windows.Forms.Button LiveStockButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox LiveStockGroupBox;
        private System.Windows.Forms.Button GenerateTodaysReportButton;
        private System.Windows.Forms.ListView LiveStockListView;
        private System.Windows.Forms.Label TotalCategoriesLabel;
        private System.Windows.Forms.Label TotalProductsLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox SummaryGroupBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label TotalTransactionsLabel;
        private System.Windows.Forms.Label AverageSaleLabel;
        private System.Windows.Forms.Label TotalItemsLabel;
        private System.Windows.Forms.Label TotalSaleLabel;
        private System.Windows.Forms.GroupBox TodaysSaleGroupBox;
        private System.Windows.Forms.ListView TodaySaleListView;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label TodaySaleLabel;
        private System.Windows.Forms.Button DownloadLiveReportButton;
        private System.Windows.Forms.Label SmallestSaleLabel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label LargestSaleLabel;
        private System.Windows.Forms.Label label10;
    }
}