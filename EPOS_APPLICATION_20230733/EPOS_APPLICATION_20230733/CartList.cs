using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace EPOS_APPLICATION_20230733
{
    public partial class CartList : UserControl
    {
        public CartList()
        {
            InitializeComponent();
        }

        #region Properties
        //User Control Properties for cart item
        private decimal _ProductTotal;
        private decimal _ProductPrice;
        private string _ProductID;
        private string _ProductCategory;

        [Category("Custom List")]
        public string ProdName
        {
            get { return ProductNameLabel.Text; }
            set { ProductNameLabel.Text = value; }
        }

        [Category("Custom List")]
        public int ProdQuantity
        {
            get { return int.Parse(QuantityTextBox.Text); }
            set { QuantityTextBox.Text = value.ToString(); }
        }

        [Category("Custom List")]
        public decimal ProdPrice
        {
            get { return _ProductPrice; }
            set { _ProductPrice = value; ProductPriceLabel.Text = value.ToString(); }
        }

        [Category("Custom List")]
        public decimal ProdTotal
        {
            get { return _ProductTotal; }
            set { _ProductTotal = value; ProductTotalPriceLabel.Text = value.ToString(); }
        }

        [Category("Custom List")]
        public string ProdID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }

        [Category("Custom List")]
        public string ProdCat
        {
            get { return _ProductCategory; }
            set { _ProductCategory = value; }
        }
        #endregion

        //Event Handler for Delete Product Button
        //Removed One Product from Cart
        //Recalculate total cart price
        private void DeleteProductButton_Click(object sender, EventArgs e)
        {
            RemoveControl();
        }
        //Method to remove one usercontrol from Cart
        private void RemoveControl()
        {            
            MainForm frm = (MainForm)this.FindForm();

            //Enabling the AddtoCart Button
            foreach (ProductList Ctrl in frm.ProductsFlowLayoutPanel.Controls)
            {
                if(Ctrl.ProdCat == this.ProdCat 
                && Ctrl.ProdID  == this.ProdID)
                {
                    Ctrl.ProdAddButtonStatus = true;
                    break;
                }
            }
            //Recalculating Items and Cart Value
            MainForm.TotalItems -= int.Parse(this.QuantityTextBox.Text);
            MainForm.GrandTotal -= decimal.Parse(this.ProductTotalPriceLabel.Text);
            frm.TotalItemsLabel.Text = MainForm.TotalItems.ToString();
            frm.GrandTotalLabel.Text = MainForm.GrandTotal.ToString();
            MainForm.CurrentCartProducts.Remove(this.ProdID);
            //Removes Product
            this.Parent.Controls.Remove(this);
        }

        //Event Handler for Add Button in Cart
        private void AddButton_Click(object sender, EventArgs e)
        {
                //Conveting CUrrent quantity to integer
                int.TryParse(QuantityTextBox.Text, out int CurrentValue);
                //Adding 1 to current quantity
                CurrentValue += 1;
                //Finding the product in List in conventional Way using sequencial search
                //We can use List Find methods as well in order to find and return us the direct item from list
                for (int i= 0;i< MainForm.ProductList.Count;i++)
                {
                    //Matching product selected from list, if Found, Adding one product to the cart 
                    if (MainForm.ProductList[i].ProductCategory == this.ProdCat
                     && MainForm.ProductList[i].ProductID       == this.ProdID)
                    {
                        //Check if Stock is available
                        if(CurrentValue <= MainForm.ProductList[i].ProductQuantity)
                        {
                            MainForm.TotalItems += 1;
                            MainForm.GrandTotal += this._ProductPrice;
                            MainForm frm = (MainForm)this.FindForm();
                            frm.TotalItemsLabel.Text = MainForm.TotalItems.ToString();
                            frm.GrandTotalLabel.Text = MainForm.GrandTotal.ToString();
                            QuantityTextBox.Text = CurrentValue.ToString();
                            ProductTotalPriceLabel.Text = (CurrentValue * this._ProductPrice).ToString();
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Requested Quantities of "+ this.ProdName + " are not available in inventory",
                                "Not Enough Items Available",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            QuantityTextBox.Focus();
                        }
                    }
                }
            
        }

        //Event Handler for Remove Button in Cart
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            //Converting Quantity to Interger
            int.TryParse(QuantityTextBox.Text, out int CurrentValue);
            //Decreasing one quantity from current Quantity
            CurrentValue -= 1;
            
            if (CurrentValue >= 1)
            {
                //Recalculating Totals and total Items
                MainForm.TotalItems -= 1;
                MainForm.GrandTotal -= this._ProductPrice;
                MainForm frm = (MainForm)this.FindForm();
                frm.TotalItemsLabel.Text = MainForm.TotalItems.ToString();
                frm.GrandTotalLabel.Text = MainForm.GrandTotal.ToString();
                QuantityTextBox.Text = CurrentValue.ToString();
                ProductTotalPriceLabel.Text = (CurrentValue * this._ProductPrice).ToString();
            }
            else
                MessageBox.Show("You can delete this item from your cart by using Delete button(at the end)",
                                "Quantity can not be Zero", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Event Handler for QuantityTextBox change
        //If user directly enters value of quantity
        private void QuantityTextBox_Leave(object sender, EventArgs e)
        {
            int Value = 0;
            try
            {
                int CurrentValue =  int.Parse(QuantityTextBox.Text);
                //checking if input is less than zero
                if (CurrentValue <= 0)
                {
                    MessageBox.Show("Zero or Less quantity not Allowed. You can use delete button to remove the product from Cart");
                    QuantityTextBox.Undo();
                    QuantityTextBox.SelectAll();
                    QuantityTextBox.Focus();

                }
                //if input is greater than 0
                else
                {
                    for (int i = 0; i < MainForm.ProductList.Count; i++)
                    {
                        //finding the product
                        if (MainForm.ProductList[i].ProductCategory == this.ProdCat
                         && MainForm.ProductList[i].ProductID == this.ProdID)
                        {
                            //checking the available stock quantity
                            if (CurrentValue <= MainForm.ProductList[i].ProductQuantity)
                            {
                                //Rechecking the previous quantity
                                QuantityTextBox.Undo();
                                
                                if(QuantityTextBox.Text != CurrentValue.ToString())
                                {
                                    Value = CurrentValue;
                                    try
                                    {
                                        CurrentValue = CurrentValue - int.Parse(QuantityTextBox.Text);
                                        QuantityTextBox.Text = Value.ToString();
                                    }
                                    catch
                                    {
                                        QuantityTextBox.Text = Value.ToString();
                                    }
                                }

                                //Calculating Items and Totals using provided quantity
                                MainForm.TotalItems += CurrentValue;
                                MainForm.GrandTotal += (CurrentValue * this._ProductPrice);
                                MainForm frm = (MainForm)this.FindForm();
                                frm.TotalItemsLabel.Text = MainForm.TotalItems.ToString();
                                frm.GrandTotalLabel.Text = MainForm.GrandTotal.ToString();
                                ProductTotalPriceLabel.Text = (Value * this._ProductPrice).ToString();
                                break;
                            }
                            else
                            {
                                MessageBox.Show("Requested Quantities of " + this.ProdName + " are not available in inventory",
                               "Not Enough Items Available", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                QuantityTextBox.Undo();
                                QuantityTextBox.Focus();
                            }
                        }
                    }
                }
                
            }
            catch
            {
                MessageBox.Show("Only Numbers are allowed in quantity. Please enter valid quantity",
                                "Invalid Input",MessageBoxButtons.OK,MessageBoxIcon.Error);
                QuantityTextBox.Focus();
                QuantityTextBox.SelectAll();
                QuantityTextBox.Undo();
            }
        }
    }
}
