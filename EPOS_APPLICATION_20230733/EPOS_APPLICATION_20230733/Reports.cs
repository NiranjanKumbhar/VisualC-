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

        string LowQuantityLine;
        public Form RefToMainForm { get; set; }

        //Event Handler for Form Load Event
        private void Reports_Load(object sender, EventArgs e)
        {
            string[] Item = new string[5];
            ListViewItem itm;
            int LowCount =0, ZeroCount=0;

            //Initializing UI controls
            SummaryGroupBox.Visible     = false;
            LiveStockGroupBox.Visible   = true;
            TodaysSaleGroupBox.Visible  = false;
            LiveStockListView.View      = View.Details;

            //Adding Columns to the ListView
            LiveStockListView.Columns.Add("Product ID", 100, HorizontalAlignment.Center);
            LiveStockListView.Columns.Add("Product Category", 100, HorizontalAlignment.Center);
            LiveStockListView.Columns.Add("Product Name", 100, HorizontalAlignment.Center);
            LiveStockListView.Columns.Add("Product Quantity", 100, HorizontalAlignment.Center);
            LiveStockListView.Columns.Add("Product Price", 100, HorizontalAlignment.Center);

            //Looping on ProductList and Adding products to the ListView
            for (var i = 0; i < MainForm.ProductList.Count; i++)
            {
                Item[0] = MainForm.ProductList[i].ProductID;
                Item[1] = MainForm.ProductList[i].ProductCategory;
                Item[2] = MainForm.ProductList[i].ProductName;
                Item[3] = MainForm.ProductList[i].ProductQuantity.ToString();
                Item[4] = MainForm.ProductList[i].ProductPrice.ToString();

                //Checking if product has Zero Quantity
                if (MainForm.ProductList[i].ProductQuantity == 0)
                {
                    ZeroCount += 1;
                    LowQuantityLine += "!!!Alert!!!" + MainForm.ProductList[i].ProductName + " is OUT OF STOCK  ";
                }
                //If Product Quantity is less than 5 
                else if (MainForm.ProductList[i].ProductQuantity < 5 )
                { 
                    LowCount += 1;
                    LowQuantityLine += "!!!Caution!!! Product : " + MainForm.ProductList[i].ProductName + " has only "
                        + MainForm.ProductList[i].ProductQuantity.ToString() + " Quantity left.      ";
                }
                //Adding Item to ListViewItem
                itm  = new ListViewItem(Item);
                LiveStockListView.Items.Add(itm);
            }

            //Detailed Information about Stock
            TotalCategoriesLabel.Text   = MainForm.CategoryList.Count.ToString();
            TotalProductsLabel.Text     = MainForm.ProductList.Count.ToString();
            LowOnStockLabel.Text        = LowCount.ToString();
            OutOfStockLabel.Text        = ZeroCount.ToString();
            ReelLabel.Text              = LowQuantityLine;
            AnimateTextTimer.Start();
            

        }

        //Event Handler for LiveStockButton
        //Displaying Management Stock Report by default
        private void LiveStockButton_Click(object sender, EventArgs e)
        {
            LiveStockGroupBox.Visible   = true;
            SummaryGroupBox.Visible     =false;
            TodaysSaleGroupBox.Visible  = false;
        }

        //Event Handler for SummaryButton Click
        private void SummaryButton_Click(object sender, EventArgs e)
        {
            //Making UI changes
            SummaryGroupBox.Visible     = true;
            LiveStockGroupBox.Visible   = false;
            TodaysSaleGroupBox.Visible  = false;

            int TransactionNumber       =0,TotalItems=0;
            decimal TotalSale = 0m, LargestSale=0.0m, SmallestSale = 0.0m;

            StreamReader InputFile;
            string FileLine;
            int i;
            
            //Reading Transaction File to find sales summary
            InputFile = File.OpenText(MainForm.TRANSACTIONDATABASEFILENAME);
            while (!InputFile.EndOfStream)
            {
                //Processing transactions having 5 lines each
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

            //Detailed Informtion about Sales Summary
            TotalTransactionsLabel.Text = TransactionNumber.ToString();
            TotalItemsLabel.Text        = TotalItems.ToString();
            TotalSaleLabel.Text         = "€ " + TotalSale.ToString("n2");
            AverageSaleLabel.Text       = "€ "+(TotalSale / TransactionNumber).ToString("n2");
            LargestSaleLabel.Text       = "€ " +LargestSale.ToString("n2");
            SmallestSaleLabel.Text      = "€ " +SmallestSale.ToString("n2");
            
        }

        //Event Handler for TodaysSaleButton click
        private void TodaySaleButton_Click(object sender, EventArgs e)
        {
            //Reinitializing UI
            TodaySaleListView.Clear();
            SummaryGroupBox.Visible = false;
            LiveStockGroupBox.Visible = false;
            TodaysSaleGroupBox.Visible = true;

            string[] Item = new string[5];
            ListViewItem itm;

            //Adding Columns to the ListViewItem
            TodaySaleListView.View = View.Details;
            TodaySaleListView.Columns.Add("Product ID", 100, HorizontalAlignment.Center);
            TodaySaleListView.Columns.Add("Product Category", 100, HorizontalAlignment.Center);
            TodaySaleListView.Columns.Add("Product Name", 100, HorizontalAlignment.Center);
            TodaySaleListView.Columns.Add("Product Quantity", 100, HorizontalAlignment.Center);
            TodaySaleListView.Columns.Add("Product Price", 100, HorizontalAlignment.Center);

            //Products having quantities greater than 0 will be added to the list
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

        //Event Handler for Exit Button
        private void Exitbutton_Click(object sender, EventArgs e)
        {
            RefToMainForm.Show();
            this.Close();
        }

        //Event Handler for GenerateReportButton Click
        private void GenerateReportButton_Click(object sender, EventArgs e)
        {
            //calling generic method for generating report
            GenerateReport(true,false);
        }

        //Event Handler for DownloadLiveReportButton Click
        private void DownloadLiveReportButton_Click(object sender, EventArgs e)
        {
            GenerateReport(false,false);
        }

        //Method to Generate Reports - Text file Generation
        public void GenerateReport(bool Sales,bool Exit)
        {
            StreamWriter FileWriter;
            string FileName,FileContent;

            //If its sales report
            if (Sales == true)
                FileName = "Daily_Sales_Report_" + DateTime.Now.ToShortDateString() + ".txt";
            //if its management report
            else
                FileName = "Management_Stock_Report_" + DateTime.Now.ToShortDateString() + ".txt";

            //If file already exist, delete the previous one and write new file
            if (File.Exists(FileName))
                File.Delete(FileName);
            
            //Creating file and Starting in append text mode
            FileWriter = File.AppendText(FileName);
            
            //Writing HeaderLine
            if (Sales == true)
                FileContent = "\t\t\t\tDaily Sales Report" + Environment.NewLine;
            else
                FileContent = "\t\t\t\tManagement Stock Report" + Environment.NewLine;

            //Writing Date and Time
            FileContent += "\t\t\t\t" + DateTime.Now.ToShortDateString() + Environment.NewLine;
            FileContent += "\t\t\t\t" + DateTime.Now.ToShortTimeString() + Environment.NewLine;

            //Iterating on SoldItemProductList and writing it into file
            if (Sales==true)
            {
                for (int i = 0; i < MainForm.CategoryList.Count; i++)
                {
                    FileContent += "\n\t\t\t\tCategory: " + MainForm.CategoryList[i]+Environment.NewLine;
                    FileContent +=  string.Format("\t{0,-50}{1,-10}", "Product Name", "Quantity Sold")+Environment.NewLine;

                    for (int j = 0; j < MainForm.SaleProductIDs.Count; j++)
                    {
                        if (MainForm.CategoryList[i] == MainForm.SaleProductIDs[j].ProductCategory
                            && MainForm.SaleProductIDs[j].ProductQuantity > 0)
                            FileContent+= string.Format("\t{0,-50}{1,-10}", MainForm.SaleProductIDs[j].ProductName, MainForm.SaleProductIDs[j].ProductQuantity) + Environment.NewLine;
                    }
                }
                FileWriter.WriteLine(FileContent);
            }
            //Iterating on ProductList and writing it into file
            else
            {
                for (int i = 0; i < MainForm.CategoryList.Count; i++)
                {
                    FileContent += "\n\t\t\t\tCategory: " + MainForm.CategoryList[i] + Environment.NewLine;
                    FileContent += string.Format("\t{0,-50}{1,-10}", "Product Name", "Quantity Available") + Environment.NewLine;
                    
                    for (int j = 0; j < MainForm.ProductList.Count; j++)
                    {
                        if (MainForm.CategoryList[i] == MainForm.ProductList[j].ProductCategory)
                            FileContent += string.Format("\t{0,-50}{1,-10}", MainForm.ProductList[j].ProductName, MainForm.ProductList[j].ProductQuantity) + Environment.NewLine;
                    }
                }
                FileWriter.WriteLine(FileContent);
            }
            FileWriter.Close();
            
            //If its Exit method, popup will not be displayed
            if(!Exit)
                MessageBox.Show("Report Generated and Saved to text file successfully.\nFile Name: "+ FileName,"Downloaded" );
        }

        //For NewslineAnimation
        private void AnimateTextTimer_Tick(object sender, EventArgs e)
        {
            ReelLabel.Left -= 5;
        }

    }
}
