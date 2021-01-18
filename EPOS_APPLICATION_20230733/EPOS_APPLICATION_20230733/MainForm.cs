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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        //Global Variables required for Program Scope
        public static List<Product> ProductList { get; private set; }
        public static List<Product> SaleProductIDs { get; private set; }
        public static List<String> CurrentCartProducts { get; set; }
        public static int TotalItems { get; set; }
        public static Decimal GrandTotal { get; set; }
        public static Decimal TodaysSaleTotal { get; set; }


        //Files required during program execution
        public const string TRANSACTIONDATABASEFILENAME = "Transactions.txt";
        const string PRODUCTDATABASEFILENAME            = "Products.txt";
        const string CATEGORYDATABASEFILENAME           = "Category.txt";
        
        //One Product contains RECORDLENGTH
        const int RECORDLENGTH = 5;

        //Product IDs used during Program Execution
        public List<string> ProductIDs;
        public static List<string> CategoryList { get; private set; }
        public string[,] Transactions;

        /*
         * Product Class for storing Product Details
         */
        public class Product
        {
            public string   ProductCategory { get; set; }
            public string   ProductID { get; set; }
            public string   ProductName { get; set; }
            public decimal  ProductPrice { get; set; }
            public int      ProductQuantity { get; set; }
        }

        
        /*
         Event Handler for checking Existing Product ID from file
        */
        private bool ExistProductID_Check(String GenratedProductID)
        {
            bool ReturnValue = false;

            //ProductIDs Stored at the start of Program
            if(ProductIDs.Count > 0)
            {
                foreach (string ProductID in ProductIDs)
                {
                    if (ProductID == GenratedProductID)
                    {
                        ReturnValue = true;
                        break;
                    }
                }
            }
            return ReturnValue;
        }

        //====================== START OF FORM EVENTS  =========================
        /*
         * Event Handler for Form Load Event
         * Dynamically adding Categories to the UI 
         * Fetching Product Stock file and building UI components
         * Fetching textfiles and storing data into the lists
         */
        private void MainForm_Load(object sender, EventArgs e)
        {
            RealTimer.Start();
            //Making UI changes
            SearchPanel.Visible = false;
            InventoryGroupBox.Visible = false;
            ChangeButtonBorder(1);

            StreamReader InputFile;
            string FileLine;

            //Initializing Lists for Application Use
            CategoryList        = new List<string>();   //Category List   
            ProductList         = new List<Product>();  //Main Stock Product List
            SaleProductIDs      = new List<Product>();  //Sold Stock Product List
            CurrentCartProducts = new List<string>();   //Current Cart List for Enable/Disable purpose
            ProductIDs          = new List<string>();   //Product ID's of all products in the Inventory

            //Initializing Maximum categories to 50
            ProductCategoryList[] NewCatList = new ProductCategoryList[50];

            //Reading Category File and fetching Categories 
            InputFile = File.OpenText(CATEGORYDATABASEFILENAME);
            while (!InputFile.EndOfStream)
            {
                FileLine = InputFile.ReadLine();
                //Initialzing CategoryList
                CategoryList.Add(FileLine);
                //Initialzing ProductCategoryList UserControl 
                NewCatList[1] = new ProductCategoryList
                {
                    CatName = FileLine
                };
                //Adding Categories to the CategoryFlowLayoutPanel to show various categories
                CategoryFlowLayoutPanel.Controls.Add(NewCatList[1]);
            }
            InputFile.Close();

            //if Categories found, fetching products according to categories
            if (CategoryList.Count > 0)
            {
                
                //Reading Product Stock file
                InputFile = File.OpenText(PRODUCTDATABASEFILENAME);
                while (!InputFile.EndOfStream)
                {
                    //Creating two Sets, 
                    //one for Storing Original Stock
                    //One for storing Sold stock in application Run
                    Product p1 = new Product();
                    Product p2 = new Product();
                    for (int i = 0; i < RECORDLENGTH; i++)
                    {
                        FileLine = InputFile.ReadLine();
                        switch (i)
                        {
                            case 0:
                                p1.ProductID = FileLine;
                                p2.ProductID = FileLine;
                                ProductIDs.Add(p1.ProductID);
                                break;

                            case 1:
                                p1.ProductCategory = FileLine;
                                p2.ProductCategory = FileLine;
                                break;

                            case 2:
                                p1.ProductName = FileLine;
                                p2.ProductName = FileLine;
                                break;

                            case 3:
                                p1.ProductPrice = decimal.Parse(FileLine);
                                p2.ProductPrice = decimal.Parse(FileLine);
                                break;

                            case 4:
                                p1.ProductQuantity = int.Parse(FileLine);
                                p2.ProductQuantity = 0;
                                break;
                        }
                    }
                    //Adding products to two seperate lists
                    ProductList.Add(p1);
                    SaleProductIDs.Add(p2);
                }
                InputFile.Close();
                toolTip1.Show("Select Desired Category to view Products",SaleButton);
            }
            else
                MessageBox.Show("No Categories found in the Inventory. Try adding Categories first","No Categories to List",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        //Event Handler for Form Closing 
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            EndOfTheDayStock();
        }

        //====================== END OF FORM EVENTS  =========================


        //====================== START OF UI FUNCATIONALITY BUTTONS  =========================

        /*
         * Event Handler for Sale Button 
         * Displaying Sales Window 
         * Displaying Categories to choose
         */
        private void BookButton_Click(object sender, EventArgs e)
        {
            SalesPanel.Visible = true;
            SearchPanel.Visible = false;
            InventoryGroupBox.Visible = false;
            CategoryList.Clear();
            CategoryFlowLayoutPanel.Controls.Clear();
            ProductsFlowLayoutPanel.Controls.Clear();
            CartFlowLayoutPanel.Controls.Clear();
            TotalItemsLabel.Text = GrandTotalLabel.Text = "";
            MainForm_Load(sender, e);
            ChangeButtonBorder(1);
        }

        /*
         * Event Handler for Search Button 
         * # Fetching Transactions and Storing them into Arry
         */
        private void SearchButton_Click(object sender, EventArgs e)
        {
            DialogResult DR;
            StreamReader InputFile;
            string FileLine;

            int i, j = 0;
            bool Search = true;
            //Using Array to store transactions
            //To demonstrate Array Use in Application
            //Instead, We could have used List to store transaction since we don't know how many transaction will be in file
            Transactions = new string[10000, 5];

            //Initializing UI Controls
            DateSearchRadioButton.Checked = TransIDSearchRadioButton.Checked = false;
            DateSearchGroupBox.Visible = false;
            TranIDSearchGroupBox.Visible = false;
            FindButton.Visible = false;
            ItemFoundPanel.Visible = false;
            FoundTransactionListView.Items.Clear();
            IDGroupBox.Visible = false;
            //Clearing preious Search Results
            FoundIDListBox.Items.Clear();

            //Checking if Cart contains any items
            //User will be asked to continue or not
            if (CartFlowLayoutPanel.Controls.Count != 0)
            {
                DR = MessageBox.Show("Cart will be cleared. Do you want to exit without check out?", "Caution", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (DR == DialogResult.No)
                {
                    Search = false;
                }
                else
                    CartFlowLayoutPanel.Controls.Clear();
            }

            //Allowed to Process Search functionality
            if (Search)
            {
                ChangeButtonBorder(3);
                //Reading Transaction File
                InputFile = File.OpenText(TRANSACTIONDATABASEFILENAME);
                //Fetching records from file and Storing them into [n][5] Array
                while (!InputFile.EndOfStream)
                {
                    for (i = 0; i < 5; i++)
                    {
                        FileLine = InputFile.ReadLine();
                        Transactions[j, i] = FileLine;
                    }
                    j++;
                }

                SalesPanel.Visible = false;
                SearchPanel.Visible = true;
                InventoryGroupBox.Visible = false;
            }
        }

        /*
         * Event Handler for Report Button 
         * # Display New Report Form
         */
        private void ReportsButton_Click(object sender, EventArgs e)
        {
            Reports Repo = new Reports();
            Repo.RefToMainForm = this;
            this.Hide();
            Repo.Show();
        }

        /*
         * Event Handler for Invetory Button 
         */
        private void InventoryButton_Click(object sender, EventArgs e)
        {
            DialogResult DR;
            bool Success = true;

            //Initializing UI Controls
            AddProductRadioButton.Checked =
            AddCategoryRadioButton.Checked =
            ModifyProductRadioButton.Checked =
            CategoryManagementPanel.Visible =
            ProductManagementPanel.Visible = false;
            ProductComboBox.SelectedIndex = -1;
            CategoryNameTextBox.Text =
            ProductNameTextBox.Text =
            ProductPriceTextBox.Text =
            ProductQuantityTextBox.Text = "";

            //Checking if Cart contains any items
            if (CartFlowLayoutPanel.Controls.Count != 0)
            {
                DR = MessageBox.Show("Cart will be cleared. Do you want to exit without check out?", "Caution", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (DR == DialogResult.No)
                {
                    Success = false;
                }
                else
                    CartFlowLayoutPanel.Controls.Clear();
            }

            //Continuing to Inventory Management
            if (Success)
            {
                ChangeButtonBorder(2);
                SalesPanel.Visible = false;
                InventoryGroupBox.Visible = true;
                SearchPanel.Visible = false;
                InventoryGroupBox.Location = SalesPanel.Location;
                ProductCategoryListBox.Items.Clear();

                //Assigning Categories to display in Product Category List Box
                foreach (String category in CategoryList)
                {
                    ProductCategoryListBox.Items.Add(category);
                }
            }
        }

        /*
         * Event Handler for Exit Button 
         * Performing End of Day Events
         * 1. Writing stock to File
         * 2. Generating Reports
         */
        private void ExitButton_Click(object sender, EventArgs e)
        {
            EndOfTheDayStock();
            this.Close();
        }

        //====================== END OF UI FUNCATIONALITY BUTTONS  =========================



        //====================== START OF SALE FUNCATIONALITY METHODS  =========================
        /*
         Event Handler for Order Button in Sale Windows
        */
        private void OrderButton_Click(object sender, EventArgs e)
        {
            StreamWriter FileWriter;
            string Reciept = "", CurrentLine, Seperator = "_", Message ="";

            //Generating Dynamic Confirmation Message Based on Cart Items
            Message = "Please Confirm the below order:\n\n";
            foreach (CartList CartItems in CartFlowLayoutPanel.Controls)
            {
                Message = Message + "\nProduct Name: " + CartItems.ProdName +
                                    "\nQuantity(s)      : " + CartItems.ProdQuantity  +Environment.NewLine;
            }
            Message = Message + "\n\nYou will have to pay €" + GrandTotal.ToString() +" for "+TotalItems.ToString()+" items.";
            Message = Message + "\n\nDo you want to confirm the order?";

            DialogResult DR = MessageBox.Show(Message, "Confirm the Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(DR == DialogResult.Yes)
            {

                foreach (CartList CartItems in CartFlowLayoutPanel.Controls)
                {
                    int i;
                    // Modifying Original Stock ProductList
                    // Substracting Cart Quantities from stock
                    for (i = 0; i < ProductList.Count; i++)
                    {
                        if (ProductList[i].ProductQuantity > 0)
                        {
                            if (CartItems.ProdID == ProductList[i].ProductID)
                            {
                                ProductList[i].ProductQuantity -= CartItems.ProdQuantity;
                                break;
                            }
                        }
                    }
                    // Adding quantities into Sold Product List
                    //Adding Cart Quantities from stock
                    for (i = 0; i < SaleProductIDs.Count; i++)
                    {
                        if (CartItems.ProdID == SaleProductIDs[i].ProductID)
                        {
                            SaleProductIDs[i].ProductQuantity += CartItems.ProdQuantity;
                            break;
                        }
                    }
                    CurrentLine = CartItems.ProdCat + Seperator + CartItems.ProdName + Seperator + CartItems.ProdQuantity + Seperator + CartItems.ProdTotal;
                    if (Reciept == "")
                        Reciept = CurrentLine;
                    else
                        Reciept = Reciept + Seperator + CurrentLine;
                }
                TodaysSaleTotal += GrandTotal;
                //Writing current Transaction to file
                try
                {
                    FileWriter = File.AppendText(TRANSACTIONDATABASEFILENAME);
                    FileWriter.WriteLine(TransactionIDLabel.Text);
                    FileWriter.WriteLine(DateTime.Today.ToShortDateString());
                    FileWriter.WriteLine(TotalItems.ToString());
                    FileWriter.WriteLine(GrandTotal.ToString());
                    FileWriter.WriteLine(Reciept);
                    FileWriter.Close();

                    MessageBox.Show("Transaction " + TransactionIDLabel.Text + " completed successfully");

                    CartFlowLayoutPanel.Controls.Clear();
                    ProductsFlowLayoutPanel.Controls.Clear();
                    CurrentCartProducts.Clear();
                    TotalItems = 0;
                    GrandTotal = 0;
                    TotalItemsLabel.Text = "";
                    GrandTotalLabel.Text = "";
                }
               catch
                {
                    MessageBox.Show("Some Error Occured while Completing your order. Try again Later","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                } 
            }
        }

        /*
         Event Handler for Clear Button in Sale Windows
        */
        private void ClearButton_Click(object sender, EventArgs e)
        {
            CartFlowLayoutPanel.Controls.Clear();
            ProductsFlowLayoutPanel.Controls.Clear();
            CurrentCartProducts.Clear();
            TotalItems = 0;
            GrandTotal = 0;
            TotalItemsLabel.Text = "";
            GrandTotalLabel.Text = "";
        }

        //Event Handler for adding item to CartFlowLayout in Sale Windows
        private void CartFlowLayoutPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            //If first Item is added, Generating Transaction ID
            if (CartFlowLayoutPanel.Controls.Count == 1)
            {
                TransactionIDLabel.Text = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                OrderButton.Visible = true;
                ClearButton.Visible = true;
            }
        }

        //Event Handler for Removing item From CartFlowLayout in Sale Windows
        private void CartFlowLayoutPanel_ControlRemoved(object sender, ControlEventArgs e)
        {
            //If all the items are removed from list, removing transaction ID and Hiding button
            if (CartFlowLayoutPanel.Controls.Count == 0)
            {
                TransactionIDLabel.Text = " ";
                TotalItemsLabel.Text = "";
                GrandTotalLabel.Text = "";
                OrderButton.Visible = false;
                ClearButton.Visible = false;
                TotalItems = 0;
                GrandTotal = 0;
            }
        }

        //====================== END OF SALE FUNCATIONALITY METHODS  =========================



        // ======================  START OF SEARCH FUNCTIONALITY METHODS  =========================

        //Event handler for Date Search Radio Button- UI changes will be made
        private void DateSearchRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            DateSearchGroupBox.Visible = true;
            TranIDSearchGroupBox.Visible = false;
            IDGroupBox.Visible = false;
            ItemFoundPanel.Visible = false;
            FindButton.Visible = true;
        }

        //Event handler for Transaction ID Search Radio Button- UI changes will be made
        private void TransIDSearchRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            DateSearchGroupBox.Visible = false;
            TranIDSearchGroupBox.Visible = true;
            FindButton.Visible = true;
            IDGroupBox.Visible = false;
            ItemFoundPanel.Visible = false;
        }

        //Event Handler for Find Button of Search Window
        private void FindButton_Click(object sender, EventArgs e)
        {
            //Clearing previous search
            FoundIDListBox.Items.Clear();
            string[] ItemList = { };
            string[] Item = new string[4];
            bool Found = false;
            int j = 0;

            //If Date Radio Button is selected
            if (DateSearchRadioButton.Checked == true)
            {
                for (int i = 0; i < Transactions.Length; i++)
                {
                    //Checking for Transaction date stored at [n][1st] index, comparing with user provided date
                    if (Transactions[i, 1] == SearchDateTimePicker.Value.ToShortDateString())
                    {
                        //If found, adding ID into List
                        FoundIDListBox.Items.Add(Transactions[i, 0]);
                        Found = true;
                    }
                    else if (Transactions[i, 1] == null)
                    {
                        break;
                    }
                }

                //If Found, results are displayed
                if (Found == true)
                {
                    IDGroupBox.Visible = true;
                }
                else
                {
                    IDGroupBox.Visible = false;
                    ItemFoundPanel.Visible = false;
                    MessageBox.Show("No Transactions found for the date :" + SearchDateTimePicker.Value.ToShortDateString());

                }

            }
            //If Transaction ID Radio Button is selected
            else if (TransIDSearchRadioButton.Checked == true)
            {
                if (TransactionIDSearchTextBox.Text != "")
                {
                    for (int i = 0; i < Transactions.Length; i++)
                    {
                        //Looking for particular Transaction ID, comapring user input
                        if (Transactions[i, 0] == TransactionIDSearchTextBox.Text)
                        {
                            //If Found, Sotring the result into ItemList and displaying Date and ID
                            TransIDFoundLabel.Text = Transactions[i, 0];
                            DateFoundLabel.Text = Transactions[i, 1];
                            TotalFoundLabel.Text = Transactions[i, 3];
                            ItemList = Transactions[i, 4].Split('_');
                            Found = true;
                            break;
                        }
                        else if (Transactions[i, 1] == null)
                        {
                            break;
                        }
                    }

                    //If Found, Splitting Cart Items from ItemList and Adding them to ListView
                    if (Found == true)
                    {
                        ListViewItem itm;
                        ItemFoundPanel.Visible = true;
                        FoundTransactionListView.Visible = true;
                        FoundTransactionListView.View = View.Details;
                        //Adding Columns Dynamically
                        FoundTransactionListView.Columns.Add("Product Category", 150, HorizontalAlignment.Center);
                        FoundTransactionListView.Columns.Add("Product Name", 150, HorizontalAlignment.Center);
                        FoundTransactionListView.Columns.Add("Product Quantity", 120, HorizontalAlignment.Center);
                        FoundTransactionListView.Columns.Add("Product Price (€)", 100, HorizontalAlignment.Center);

                        //Adding Cart Item lines Dynamically by processing the ItemList
                        for (int i = 0; i < ItemList.Length; i++)
                        {
                            Item[j] = ItemList[i];
                            if (j == 3)
                                j = 0;
                            else
                                j++;

                            if (i % 3 == 0 && i != 0)
                            {
                                itm = new ListViewItem(Item);
                                FoundTransactionListView.Items.Add(itm);
                            }
                        }
                    }
                    else
                    {
                        IDGroupBox.Visible = false;
                        ItemFoundPanel.Visible = false;
                        MessageBox.Show("No Transaction found with Transaction ID: " + TransactionIDSearchTextBox.Text);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter Transaction ID to search particular transaction");
                    TransactionIDLabel.Focus();
                }
            }
        }

        //Event Handler for FoundIDListBox Check Event
        //After ID seletion, cart items to that particular transaction will be displayed in Listview
        private void FoundIDListBox_Click(object sender, EventArgs e)
        {
            if (FoundIDListBox.SelectedIndex != -1)
            {

                ListViewItem itm;
                int j = 0;
                string[] ItemList = { };
                string[] Item = new string[4];
                FoundTransactionListView.Clear();
                for (int i = 0; i < Transactions.Length; i++)
                {
                    //Searching into array using Trasaction ID 
                    //Generating Listview Items 
                    //Displaying Particular Transaction Details
                    if (Transactions[i, 0] == FoundIDListBox.SelectedItem.ToString())
                    {
                        TransIDFoundLabel.Text = Transactions[i, 0];
                        DateFoundLabel.Text = Transactions[i, 1];
                        TotalFoundLabel.Text = Transactions[i, 3];
                        ItemList = Transactions[i, 4].Split('_');

                        FoundTransactionListView.View = View.Details;
                        ItemFoundPanel.Visible = true;
                        FoundTransactionListView.Visible = true;
                        //Dynamically Adding Columns
                        FoundTransactionListView.Columns.Add("Product Category", 140, HorizontalAlignment.Center);
                        FoundTransactionListView.Columns.Add("Product Name", 150, HorizontalAlignment.Center);
                        FoundTransactionListView.Columns.Add("Product Quantity", 100, HorizontalAlignment.Center);
                        FoundTransactionListView.Columns.Add("Product Price (€)", 100, HorizontalAlignment.Center);

                        for (i = 0; i < ItemList.Length; i++)
                        {
                            Item[j] = ItemList[i];
                            if (j == 3)
                                j = 0;
                            else
                                j++;

                            if (j == 0 && Item[3] != null)
                            {
                                itm = new ListViewItem(Item);
                                FoundTransactionListView.Items.Add(itm);
                                Item[3] = null;
                            }
                        }
                        break;
                    }
                    else if (Transactions[i, 0] == null)
                    {
                        break;
                    }
                }
            }
        }

        // ======================  END OF SEARCH FUNCTIONALITY METHODS  =========================


        /* 
         * ======================  START OF INVENTORY MANAGEMENT METHODS  =========================
         * 1.AddProductButton_Click
         * 2.CategoryButton_Click
         * 3.ModifyButton_Click
        */

        /*
         * Event Handler for Adding New Product to Inventory
         */
        private void AddProductButton_Click(object sender, EventArgs e)
        {
            Product Prod = new Product();
            bool DuplicateProduct = false;
            StreamWriter FileWriter;
            FileWriter = File.AppendText(PRODUCTDATABASEFILENAME);

            if (ProductNameTextBox.Text == "")
            {
                MessageBox.Show("Please enter New Product Name");
                ProductNameTextBox.Focus();
                ProductNameTextBox.SelectAll();
            }
            else
            {
                if (ProductCategoryListBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select Product Category from the list");
                    ProductCategoryListBox.Focus();
                }
                else
                {
                    if (ProductPriceTextBox.Text == "")
                    {
                        MessageBox.Show("Please enter Product Price");
                        ProductPriceTextBox.Focus();
                        ProductPriceTextBox.SelectAll();
                    }
                    else
                    {
                        try
                        {
                            decimal.TryParse(ProductPriceTextBox.Text, out decimal ProductPrice);
                            if (ProductPrice <= 0)
                            {
                                MessageBox.Show("Product Price should be greater than Zero");
                                ProductPriceTextBox.Focus();
                                ProductPriceTextBox.SelectAll();
                            }
                            else
                            {
                                if (ProductQuantityTextBox.Text == "")
                                {
                                    MessageBox.Show("Please enter Product Quantity");
                                    ProductQuantityTextBox.Focus();
                                    ProductQuantityTextBox.SelectAll();
                                }
                                else
                                {
                                    try
                                    {
                                        int.TryParse(ProductQuantityTextBox.Text, out int ProductQuantity);
                                        if (ProductQuantity <= 0)
                                        {
                                            MessageBox.Show("Product Quantity should be greater than Zero");
                                            ProductQuantityTextBox.Focus();
                                            ProductQuantityTextBox.SelectAll();
                                        }
                                        else
                                        {
                                            foreach (Product p in ProductList)
                                            {
                                                if (p.ProductCategory == ProductCategoryListBox.SelectedItem.ToString()
                                                && p.ProductName == ProductNameTextBox.Text)
                                                {
                                                    MessageBox.Show("Same Product Already Exist in the same Category");
                                                    ProductNameTextBox.Focus();
                                                    ProductNameTextBox.SelectAll();
                                                    DuplicateProduct = true;
                                                    break;
                                                }
                                            }

                                            if (DuplicateProduct == false)
                                            {
                                                Random RandomNumberGenerator = new Random();

                                                int ProductNumber = RandomNumberGenerator.Next(1, 999999);
                                                string CheckProductID = ProductCategoryListBox.SelectedItem.ToString().Substring(0, 4) + ProductNumber.ToString("D6");
                                                while (ExistProductID_Check(CheckProductID))
                                                {
                                                    ProductNumber = RandomNumberGenerator.Next(1, 999999);
                                                    CheckProductID = ProductCategoryListBox.SelectedItem.ToString().Substring(0, 4) + ProductNumber.ToString("D6");
                                                }
                                                Prod.ProductID = ProductCategoryListBox.SelectedItem.ToString().Substring(0, 4) + ProductNumber.ToString("D6");
                                                FileWriter.WriteLine(Prod.ProductID);
                                                Prod.ProductCategory = ProductCategoryListBox.SelectedItem.ToString();
                                                FileWriter.WriteLine(Prod.ProductCategory);
                                                Prod.ProductName = ProductNameTextBox.Text;
                                                FileWriter.WriteLine(Prod.ProductName);
                                                Prod.ProductPrice = decimal.Parse(ProductPriceTextBox.Text);
                                                FileWriter.WriteLine(Prod.ProductPrice);
                                                Prod.ProductQuantity = int.Parse(ProductQuantityTextBox.Text);
                                                FileWriter.WriteLine(Prod.ProductQuantity);
                                                ProductList.Add(Prod);
                                                FileWriter.Close();
                                                MessageBox.Show("Product '" + Prod.ProductName + "' Added Successfully to the Category '" + Prod.ProductCategory + "'");

                                                ProductCategoryListBox.SelectedIndex = -1;
                                                ProductNameTextBox.Text = ProductPriceTextBox.Text = ProductQuantityTextBox.Text = CheckProductID = "";
                                            }
                                        }
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                        }
                        catch
                        {

                        }
                    }
                }
            }


        }

        /*
        * Event Handler for Adding Category to the Invetory
        */
        private void CategoryButton_Click(object sender, EventArgs e)
        {
            StreamWriter FileWriter;
            FileWriter = File.AppendText(CATEGORYDATABASEFILENAME);
            if (CategoryNameTextBox.Text == "")
            {
                MessageBox.Show("Please enter Category Name");
                CategoryNameTextBox.Focus();
                CategoryNameTextBox.SelectAll();
            }
            else
            {
                if (CategoryList.Contains(CategoryNameTextBox.Text))
                {
                    MessageBox.Show("Category already Exist");
                    CategoryNameTextBox.Focus();
                    CategoryNameTextBox.SelectAll();
                }
                else
                {
                    CategoryList.Add(CategoryNameTextBox.Text);
                    FileWriter.WriteLine(CategoryNameTextBox.Text);
                    FileWriter.Close();
                    MessageBox.Show("Category Added Successfully to the Database");
                    CategoryNameTextBox.Text = "";
                }
            }
        }

        /*
        * Event Handler for Modifying Product Details
        */
        private void ModifyButton_Click(object sender, EventArgs e)
        {
            if (ProductComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select one product from the drop down list.", "Manual Entry Restricted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ProductComboBox.Focus();
            }
            else
            {
                String ProdName = ProductComboBox.SelectedItem.ToString();

                try
                {
                    var result = from Prod in ProductList
                                 where Prod.ProductName == ProdName
                                 select Prod;

                    foreach (var prod in result)
                    {
                        prod.ProductPrice = decimal.Parse(ProductPriceTextBox.Text);
                        prod.ProductQuantity = int.Parse(ProductQuantityTextBox.Text);
                    }
                    EndOfTheDayStock();
                    MessageBox.Show("Product Updated Successfully", "Updation Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ProductNameTextBox.Text = "";
                    ProductCategoryListBox.SelectedItem = -1;
                    ProductPriceTextBox.Text = "";
                    ProductQuantityTextBox.Text = "";
                    ProductComboBox.SelectedIndex = -1;
                }
                catch
                {
                    MessageBox.Show("Error Occured while updating the product. Please try again later");
                }
            }
        }

        //Event Handler for Add Category Radiobutton
        private void AddCategoryRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //Making UI Changes
            ProductManagementPanel.Visible = false;
            CategoryManagementPanel.Visible = true;
        }

        //Event Handler for Add Product Radiobutton
        private void AddProductRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ProductManagementPanel.Visible = true;
            CategoryManagementPanel.Visible = false;

            ProductComboBox.Items.Clear();
            ComboBoxPanel.Visible = false;
            //Making UI Changes
            ProductCategoryPanel.Enabled = true;
            ProductNameTextBox.Enabled = true;
            AddProductButton.Visible = true;
            ModifyButton.Visible = false;
        }

        //Event Handler for Modify Product Radiobutton
        private void ModifyProductRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ProductComboBox.Items.Clear();
            ProductManagementPanel.Visible = true;
            CategoryManagementPanel.Visible = false;

            ComboBoxPanel.Visible = true;
            ProductCategoryPanel.Enabled = false;
            ProductNameTextBox.Enabled = false;
            AddProductButton.Visible = false;
            ModifyButton.Visible = true;

            //Adding Dynamic Products into Product ComboBox
            foreach (Product Prod in ProductList)
            {
                ProductComboBox.Items.Add(Prod.ProductName);
            }

            toolTip1.Show(toolTip1.GetToolTip(ProductComboBox),ProductComboBox,25000);
        }

        //Event Handler for ProductComboBox, If Item is selected, quantity and price is displayed in Textboxes
        private void ProductComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProductComboBox.SelectedIndex != -1)
            {
                String ProdName = ProductComboBox.SelectedItem.ToString();
                //Searching for Product in Product List
                var result = from Prod in ProductList
                             where Prod.ProductName == ProdName
                             select Prod;

                foreach (var prod in result)
                {
                    ProductNameTextBox.Text = prod.ProductName;
                    ProductCategoryListBox.SelectedItem = prod.ProductCategory;
                    ProductPriceTextBox.Text = prod.ProductPrice.ToString();
                    ProductQuantityTextBox.Text = prod.ProductQuantity.ToString();
                }
            }
        }

        // ======================  END OF INVENTORY MANAGEMENT METHODS  =========================


        //Writing end of stock to the file and generating Reports
        private void EndOfTheDayStock()
        {
            string NewFileContent="";
            
            foreach(Product Prod in ProductList)
            {
                    if(NewFileContent == "")
                        NewFileContent = Prod.ProductID + Environment.NewLine;
                    else
                        NewFileContent = NewFileContent + Prod.ProductID + Environment.NewLine;

                NewFileContent = NewFileContent + Prod.ProductCategory + Environment.NewLine;
                NewFileContent = NewFileContent + Prod.ProductName + Environment.NewLine;
                NewFileContent = NewFileContent + Prod.ProductPrice + Environment.NewLine;
                NewFileContent = NewFileContent + Prod.ProductQuantity + Environment.NewLine;
            }
            File.WriteAllText(PRODUCTDATABASEFILENAME, NewFileContent);

            Reports Repo = new Reports();
            Repo.GenerateReport(false,true);
            Repo.GenerateReport(true,true);
        }

        //Changing Butter border Color
        private void ChangeButtonBorder(int Index)
        {
            switch(Index)
            {
                case 1: 
                    SaleButton.FlatAppearance.BorderSize = 1;
                    SaleButton.FlatAppearance.BorderColor = Color.Red;
                    InventoryButton.FlatAppearance.BorderSize = 0;
                    SearchButton.FlatAppearance.BorderSize = 0;
                    ReportsButton.FlatAppearance.BorderSize = 0;
                    break;
                case 2:
                    SaleButton.FlatAppearance.BorderSize = 0;
                    InventoryButton.FlatAppearance.BorderSize = 1;
                    InventoryButton.FlatAppearance.BorderColor = Color.Red;
                    SearchButton.FlatAppearance.BorderSize = 0;
                    ReportsButton.FlatAppearance.BorderSize = 0;
                    break;
                case 3:
                    SaleButton.FlatAppearance.BorderSize = 0;
                    InventoryButton.FlatAppearance.BorderSize = 0;
                    SearchButton.FlatAppearance.BorderSize = 1;
                    SearchButton.FlatAppearance.BorderColor = Color.Red;
                    ReportsButton.FlatAppearance.BorderSize = 0;
                    break;
            }
        }

        private void RealTimer_Tick(object sender, EventArgs e)
        {
            TimeLabel.Text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
