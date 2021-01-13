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

        public static List<Product> ProductList { get; private set; }
        public static List<Product> SaleProductIDs { get; private set; }
        public static List<String> CurrentCartProducts { get; set; }
        public static int TotalItems { get; set; }
        public static Decimal GrandTotal { get; set; }
        public static Decimal TodaysSaleTotal { get; set; }


        public const string TRANSACTIONDATABASEFILENAME = "Transactions.txt";
        const string PRODUCTDATABASEFILENAME            = "Products.txt";
        const string CATEGORYDATABASEFILENAME           = "Category.txt";
        
        const int RECORDLENGTH = 5;

        public List<string> ProductIDs;
        public static List<string> CategoryList { get; private set; }
        public string[,] Transactions;

        public class Product
        {
            public string ProductCategory { get; set; }
            public string ProductID { get; set; }
            public string ProductName { get; set; }
            public decimal ProductPrice { get; set; }
            public int ProductQuantity { get; set; }
        } 

        private bool ExistProductID_Check(String GenratedProductID)
        {
            bool ReturnValue = false;

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
                if(ProductCategoryListBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select Product Category from the list");
                    ProductCategoryListBox.Focus();
                }
                else
                {
                    if(ProductPriceTextBox.Text == "")
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
                               if(ProductQuantityTextBox.Text == "")
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
                                            foreach( Product p in ProductList)
                                            {
                                                if(p.ProductCategory == ProductCategoryListBox.SelectedItem.ToString()
                                                && p.ProductName == ProductNameTextBox.Text)
                                                {
                                                    MessageBox.Show("Same Product Already Exist in the same Category");
                                                    ProductNameTextBox.Focus();
                                                    ProductNameTextBox.SelectAll();
                                                    DuplicateProduct = true;
                                                    break;
                                                }
                                            }
                                            
                                            if(DuplicateProduct == false)
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
                                                MessageBox.Show("Product '"+ Prod.ProductName + "' Added Successfully to the Category '" + Prod.ProductCategory +"'");

                                                ProductCategoryListBox.SelectedIndex = -1;
                                                ProductNameTextBox.Text = ProductPriceTextBox.Text = ProductQuantityTextBox.Text = CheckProductID ="";
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            SearchPanel.Visible = false;
            InventoryGroupBox.Visible = false;
            ChangeButtonBorder(1);

            StreamReader InputFile;
            string FileLine;

            CategoryList        = new List<string>();
            ProductList         = new List<Product>();
            SaleProductIDs      = new List<Product>();
            CurrentCartProducts = new List<string>();
            ProductIDs          = new List<string>();

            ProductCategoryList[] NewCatList = new ProductCategoryList[10];

            InputFile = File.OpenText(CATEGORYDATABASEFILENAME);
            while (!InputFile.EndOfStream)
            {
                FileLine = InputFile.ReadLine();
                CategoryList.Add(FileLine);

                NewCatList[1] = new ProductCategoryList
                {
                    CatName = FileLine
                };
                CategoryFlowLayoutPanel.Controls.Add(NewCatList[1]);
            }

            InputFile.Close();

            if (CategoryList.Count > 0)
            {
                InputFile = File.OpenText(PRODUCTDATABASEFILENAME);
                while (!InputFile.EndOfStream)
                {
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
                    ProductList.Add(p1);
                    SaleProductIDs.Add(p2);
                }
                InputFile.Close();
            }
        }

        private void OrderButton_Click(object sender, EventArgs e)
        {
            StreamWriter FileWriter;
            string Reciept = "", CurrentLine, Seperator = "_", Message ="";
            Message = "Please Confirm the below order:\n\n";
            foreach (CartList CartItems in CartFlowLayoutPanel.Controls)
            {
                Message = Message + "\n" + CartItems.ProdName + "          \t" + CartItems.ProdQuantity + " item";
            }
            Message = Message + "\n\nYou will have to pay €" + GrandTotal.ToString() +" for "+TotalItems.ToString()+" items.";
            Message = Message + "\n\nDo you want to confirm the order?";

            DialogResult DR = MessageBox.Show(Message, "Confirm the Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(DR == DialogResult.Yes)
            {
                foreach (CartList CartItems in CartFlowLayoutPanel.Controls)
                {
                    int i;
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
                    MessageBox.Show("Some Error Occured while Completing your order. Try again Later");
                }

                
                
            }
            

        }

        private void CartFlowLayoutPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            if(CartFlowLayoutPanel.Controls.Count == 1)
            {
                TransactionIDLabel.Text = DateTime.Now.Year.ToString() +DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() +DateTime.Now.Hour.ToString() +DateTime.Now.Minute.ToString()+ DateTime.Now.Second.ToString();
                OrderButton.Visible = true;
                ClearButton.Visible = true;
            }
        }

        private void CartFlowLayoutPanel_ControlRemoved(object sender, ControlEventArgs e)
        {
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
                if(CategoryList.Contains(CategoryNameTextBox.Text))
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            EndOfTheDayStock();
        }

        private void AddCategoryRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ProductManagementPanel.Visible = false;
            CategoryManagementPanel.Visible = true;
        }

        private void AddProductRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ProductManagementPanel.Visible = true;
            CategoryManagementPanel.Visible = false;

            ProductComboBox.Items.Clear();
            ComboBoxPanel.Visible = false;


            ProductCategoryPanel.Enabled = true;
            ProductNameTextBox.Enabled = true;
            AddProductButton.Visible = true;
            ModifyButton.Visible = false;
        }

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

            foreach (Product Prod in ProductList)
            {
                ProductComboBox.Items.Add(Prod.ProductName);
            }
        }



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
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            EndOfTheDayStock();
            this.Close();
        }

        

        private void InventoryButton_Click(object sender, EventArgs e)
        {
            DialogResult DR;
            bool Success = true;

            AddProductRadioButton.Checked = AddCategoryRadioButton.Checked = ModifyProductRadioButton.Checked = false;
            CategoryManagementPanel.Visible = false;
            ProductManagementPanel.Visible = false;
            CategoryNameTextBox.Text = "";
            ProductComboBox.SelectedIndex = -1;
            ProductNameTextBox.Text = ProductPriceTextBox.Text = ProductQuantityTextBox.Text = "";

            if (CartFlowLayoutPanel.Controls.Count != 0)
            {
                DR=MessageBox.Show("Cart will be cleared. Do you want to exit without check out?", "Caution",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
                if(DR == DialogResult.No)
                {
                    Success = false;
                }
            }

            if(Success)
            {
                ChangeButtonBorder(2);
                SalesPanel.Visible = false;
                InventoryGroupBox.Visible = true;
                SearchPanel.Visible = false;
                InventoryGroupBox.Location = SalesPanel.Location;
                ProductCategoryListBox.Items.Clear();
                foreach (String category in CategoryList)
                {
                    ProductCategoryListBox.Items.Add(category);
                }
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            DialogResult DR;
            StreamReader InputFile;
            string FileLine;
            int i, j = 0;
            bool Search = true;
            Transactions = new string[10000, 5];

            DateSearchRadioButton.Checked = TransIDSearchRadioButton.Checked = false;
            DateSearchGroupBox.Visible = false;
            TranIDSearchGroupBox.Visible = false;
            ItemFoundPanel.Visible = false;
            FoundTransactionListView.Items.Clear();
            IDGroupBox.Visible = false;
            FoundIDListBox.Items.Clear();

            if (CartFlowLayoutPanel.Controls.Count != 0)
            {
                DR = MessageBox.Show("Cart will be cleared. Do you want to exit without check out?", "Caution", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (DR == DialogResult.No)
                {
                    Search = false;
                }
            }

            if (Search)
            {
                ChangeButtonBorder(3);
                InputFile = File.OpenText(TRANSACTIONDATABASEFILENAME);
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

        private void DateSearchRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            DateSearchGroupBox.Visible = true;
            TranIDSearchGroupBox.Visible = false;
        }

        private void TransIDSearchRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            DateSearchGroupBox.Visible = false;
            TranIDSearchGroupBox.Visible = true;
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            string[] ItemList = { };
            string[] Item = new string[4];
            bool Found = false;
            int j = 0;
            if(DateSearchRadioButton.Checked == true)
            {
                for(int i = 0;i< Transactions.Length; i++)
                {
                    if (Transactions[i,1] == SearchDateTimePicker.Value.ToShortDateString())
                    {
                        FoundIDListBox.Items.Add(Transactions[i, 0]);
                        Found = true;
                    }
                    else if(Transactions[i, 1]== null)
                    {
                        break;
                    }
                }

                if(Found == true)
                {
                    IDGroupBox.Visible = true;
                }
            }
            else if(TransIDSearchRadioButton.Checked == true)
            {
                for (int i = 0; i < Transactions.Length; i++)
                {
                    if (Transactions[i, 0] == TransactionIDSearchTextBox.Text)
                    {
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

                if (Found == true)
                {
                    ListViewItem itm;
                    ItemFoundPanel.Visible = true;
                    FoundTransactionListView.Visible = true;
                    FoundTransactionListView.View = View.Details;
                    FoundTransactionListView.Columns.Add("Product Category",100, HorizontalAlignment.Center);
                    FoundTransactionListView.Columns.Add("Product Name"    ,100, HorizontalAlignment.Center);
                    FoundTransactionListView.Columns.Add("Product Quantity",100, HorizontalAlignment.Center);
                    FoundTransactionListView.Columns.Add("Product Price"   ,100, HorizontalAlignment.Center);

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

            }
        }

        private void FoundIDListBox_Click(object sender, EventArgs e)
        {
            ListViewItem itm;
            int j = 0;
            string[] ItemList = { };
            string[] Item = new string[4];
            FoundTransactionListView.Clear();
            for (int i = 0; i < Transactions.Length; i++)
            {
                if (Transactions[i, 0] == FoundIDListBox.SelectedItem.ToString())
                {
                    TransIDFoundLabel.Text = Transactions[i, 0];
                    DateFoundLabel.Text = Transactions[i, 1];
                    TotalFoundLabel.Text = Transactions[i, 3];
                    ItemList = Transactions[i, 4].Split('_');

                    FoundTransactionListView.View = View.Details;
                    ItemFoundPanel.Visible = true;
                    FoundTransactionListView.Visible = true;
                    FoundTransactionListView.Columns.Add("Product Category", 100, HorizontalAlignment.Center);
                    FoundTransactionListView.Columns.Add("Product Name", 100, HorizontalAlignment.Center);
                    FoundTransactionListView.Columns.Add("Product Quantity", 100, HorizontalAlignment.Center);
                    FoundTransactionListView.Columns.Add("Product Price", 100, HorizontalAlignment.Center);

                    for ( i = 0; i < ItemList.Length; i++)
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
                    break;
                }
                else if (Transactions[i, 0] == null)
                {
                    break;
                }
            }
        }

        private void ReportsButton_Click(object sender, EventArgs e)
        {
            Reports Repo = new Reports();
            Repo.ShowDialog();
        }

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
            MainForm_Load(sender,e);
            ChangeButtonBorder(1);
        }


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

        private void ProductComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ProductComboBox.SelectedIndex != -1)
            {
                String ProdName = ProductComboBox.SelectedItem.ToString();

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

        private void ModifyButton_Click(object sender, EventArgs e)
        {
            if (ProductComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select one product from the drop down list.","Manual Entry Restricted",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
    }
}
