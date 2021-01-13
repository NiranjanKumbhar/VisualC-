using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EPOS_APPLICATION_20230733
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            string[] Item = new string[5];
            ListViewItem itm;

            SummaryGroupBox.Visible     = false;
            LiveStockGroupBox.Visible   = true;
            TodaysSaleGroupBox.Visible  = false;
            LiveStockListView.View      = View.Details;

            LiveStockListView.Columns.Add("Product ID", 100, HorizontalAlignment.Center);
            LiveStockListView.Columns.Add("Product Category", 100, HorizontalAlignment.Center);
            LiveStockListView.Columns.Add("Product Name", 100, HorizontalAlignment.Center);
            LiveStockListView.Columns.Add("Product Quantity", 100, HorizontalAlignment.Center);
            LiveStockListView.Columns.Add("Product Price", 100, HorizontalAlignment.Center);

            for (var i = 0; i < MainForm.ProductList.Count; i++)
            {
                Item[0] = MainForm.ProductList[i].ProductID;
                Item[1] = MainForm.ProductList[i].ProductCategory;
                Item[2] = MainForm.ProductList[i].ProductName;
                Item[3] = MainForm.ProductList[i].ProductQuantity.ToString();
                Item[4] = MainForm.ProductList[i].ProductPrice.ToString();
                
                itm     = new ListViewItem(Item);
                LiveStockListView.Items.Add(itm);
            }
            TotalCategoriesLabel.Text   = MainForm.CategoryList.Count.ToString();
            TotalProductsLabel.Text     = MainForm.ProductList.Count.ToString();
        }

        private void LiveStockButton_Click(object sender, EventArgs e)
        {
            LiveStockGroupBox.Visible   = true;
            SummaryGroupBox.Visible     =false;
            TodaysSaleGroupBox.Visible  = false;
        }

        private void SummaryButton_Click(object sender, EventArgs e)
        {
            SummaryGroupBox.Visible     = true;
            LiveStockGroupBox.Visible   = false;
            TodaysSaleGroupBox.Visible  = false;


            int TransactionNumber       =0,TotalItems=0;
            decimal TotalSale = 0m, LargestSale=0.0m, SmallestSale = 0.0m;

            StreamReader InputFile;
            string FileLine;
            int i;
            
            InputFile = File.OpenText(MainForm.TRANSACTIONDATABASEFILENAME);
            while (!InputFile.EndOfStream)
            {
                for (i = 0; i < 5; i++)
                {
                    FileLine = InputFile.ReadLine();
                    if(i==2)
                    {
                        TotalItems += int.Parse(FileLine);
                        TransactionNumber += 1;
                    }
                    else if(i==3)
                    {
                        TotalSale += decimal.Parse(FileLine);
                        if (LargestSale < decimal.Parse(FileLine))
                            LargestSale = decimal.Parse(FileLine);

                        if (SmallestSale == 0.0m)
                            SmallestSale = decimal.Parse(FileLine);

                        if (SmallestSale > decimal.Parse(FileLine))
                            SmallestSale = decimal.Parse(FileLine);
                    }
                }
            }

            TotalTransactionsLabel.Text = TransactionNumber.ToString();
            TotalSaleLabel.Text     = "€ " + TotalSale.ToString("n2");
            TotalItemsLabel.Text    = TotalItems.ToString();
            AverageSaleLabel.Text   = "€ "+(TotalSale / TransactionNumber).ToString("n2");
            LargestSaleLabel.Text   = "€ " +LargestSale.ToString();
            SmallestSaleLabel.Text  = "€ " +SmallestSale.ToString();
            
        }

        private void TodaySaleButton_Click(object sender, EventArgs e)
        {
            SummaryGroupBox.Visible = false;
            LiveStockGroupBox.Visible = false;
            TodaysSaleGroupBox.Visible = true;

            string[] Item = new string[5];
            ListViewItem itm;

            TodaySaleListView.View = View.Details;
            TodaySaleListView.Columns.Add("Product ID", 100, HorizontalAlignment.Center);
            TodaySaleListView.Columns.Add("Product Category", 100, HorizontalAlignment.Center);
            TodaySaleListView.Columns.Add("Product Name", 100, HorizontalAlignment.Center);
            TodaySaleListView.Columns.Add("Product Quantity", 100, HorizontalAlignment.Center);
            TodaySaleListView.Columns.Add("Product Price", 100, HorizontalAlignment.Center);

            for (var i = 0; i < MainForm.SaleProductIDs.Count; i++)
            {
                if(MainForm.SaleProductIDs[i].ProductQuantity > 0)
                {
                    Item[0] = MainForm.SaleProductIDs[i].ProductID;
                    Item[1] = MainForm.SaleProductIDs[i].ProductCategory;
                    Item[2] = MainForm.SaleProductIDs[i].ProductName;
                    Item[3] = MainForm.SaleProductIDs[i].ProductQuantity.ToString();
                    Item[4] = MainForm.SaleProductIDs[i].ProductPrice.ToString();

                    itm = new ListViewItem(Item);
                    TodaySaleListView.Items.Add(itm);
                }
            }
            TodaySaleLabel.Text = MainForm.TodaysSaleTotal.ToString() ;
        }

        private void Exitbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GenerateReportButton_Click(object sender, EventArgs e)
        {
            GenerateReport(true);
        }

        private void DownloadLiveReportButton_Click(object sender, EventArgs e)
        {
            GenerateReport(false);
        }

        public void GenerateReport(bool Sales)
        {
            StreamWriter FileWriter;

            string FileName;

            if (Sales == true)
                FileName = "Daily_Sales_Report_" + DateTime.Now.ToShortDateString() + ".txt";
            else
                FileName = "Daily_Stock_Report_" + DateTime.Now.ToShortDateString() + ".txt";

            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }

            FileWriter = File.AppendText(FileName);
            
            if (Sales == true)
                FileWriter.WriteLine("\n\t\tDaily Sales Report "+ DateTime.Now.ToShortDateString());
            else
                FileWriter.WriteLine("\n\t\tDaily Stock Report " + DateTime.Now.ToShortDateString());

            if(Sales==true)
            {
                for (int i = 0; i < MainForm.CategoryList.Count; i++)
                {
                    FileWriter.WriteLine("\n\t\t\tCategory: " + MainForm.CategoryList[i]);
                    for (int j = 0; j < MainForm.SaleProductIDs.Count; j++)
                    {
                        if (MainForm.CategoryList[i] == MainForm.SaleProductIDs[j].ProductCategory)
                        {
                            FileWriter.WriteLine("\n\t"     + MainForm.SaleProductIDs[j].ProductName +
                                                 "     \t"  + MainForm.SaleProductIDs[j].ProductQuantity);
                        }
                    }
                    FileWriter.WriteLine("\n");
                }
            }
            else
            {
                for (int i = 0; i < MainForm.CategoryList.Count; i++)
                {
                    FileWriter.WriteLine("\n\t\t\tCategory: " + MainForm.CategoryList[i]);
                    for (int j = 0; j < MainForm.ProductList.Count; j++)
                    {
                        if (MainForm.CategoryList[i] == MainForm.ProductList[j].ProductCategory)
                        {
                            FileWriter.WriteLine("\n\t"     + MainForm.ProductList[j].ProductName +
                                                 "     \t"  + MainForm.ProductList[j].ProductQuantity);
                        }
                    }
                    FileWriter.WriteLine("\n");
                }
            }
            FileWriter.Close();
            if(Sales == true)
                MessageBox.Show("Report Generated and Saved to text file successfully.\nFile Name: " + FileName, "Downloaded");
            else
                MessageBox.Show("Report Generated and Saved to text file successfully.\nFile Name: "+ FileName,"Downloaded" );
        }
    }
}
