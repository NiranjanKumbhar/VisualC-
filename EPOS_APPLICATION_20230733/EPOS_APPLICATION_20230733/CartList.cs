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

        private void CartList_Load(object sender, EventArgs e)
        {

        }

        #region Properties

        private string _ProductName;
        private int _ProductQuantity;
        private decimal _ProductTotal;
        private decimal _ProductPrice;
        private string _ProductID;
        private string _ProductCategory;


        [Category("Custom List")]
        public string ProdName
        {
            get { return ProductNameLabel.Text; }
            set { _ProductName = value; ProductNameLabel.Text = value; }
        }

        [Category("Custom List")]
        public int ProdQuantity
        {
            get { return int.Parse(QuantityTextBox.Text); }
            set { _ProductQuantity = value; QuantityTextBox.Text = value.ToString(); }
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

        private void DeleteProductButton_Click(object sender, EventArgs e)
        {
            RemoveControl();
        }

        private void RemoveControl()
        {            
            MainForm frm = (MainForm)this.FindForm();

            foreach (ProductList Ctrl in frm.ProductsFlowLayoutPanel.Controls)
            {
                if(Ctrl.ProdCat == this.ProdCat 
                && Ctrl.ProdID  == this.ProdID)
                {
                    Ctrl.ProdAddButtonStatus = true;
                    break;
                }
            }
            MainForm.TotalItems -= int.Parse(this.QuantityTextBox.Text);
            MainForm.GrandTotal -= decimal.Parse(this.ProductTotalPriceLabel.Text);
            frm.TotalItemsLabel.Text = MainForm.TotalItems.ToString();
            frm.GrandTotalLabel.Text = MainForm.GrandTotal.ToString();
            MainForm.CurrentCartProducts.Remove(this.ProdID);
            this.Parent.Controls.Remove(this);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            
                int.TryParse(QuantityTextBox.Text, out int CurrentValue);
                CurrentValue += 1;
                for (int i= 0;i< MainForm.ProductList.Count;i++)
                {
                    if (MainForm.ProductList[i].ProductCategory == this.ProdCat
                     && MainForm.ProductList[i].ProductID       == this.ProdID)
                    {
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
                            MessageBox.Show("Not enough items in inventory");
                        }
                    }
                }
            
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            try
            {
                int.TryParse(QuantityTextBox.Text, out int CurrentValue);
                CurrentValue -= 1;
                if (CurrentValue >= 1)
                {
                    MainForm.TotalItems -= 1;
                    MainForm.GrandTotal -= this._ProductPrice;
                    MainForm frm = (MainForm)this.FindForm();
                    frm.TotalItemsLabel.Text = MainForm.TotalItems.ToString();
                    frm.GrandTotalLabel.Text = MainForm.GrandTotal.ToString();
                    QuantityTextBox.Text = CurrentValue.ToString();
                    ProductTotalPriceLabel.Text = (CurrentValue * this._ProductPrice).ToString();
                }
                else
                    MessageBox.Show("You can delete the item from your cart by using delete button");
            }
            catch
            {

            }
        }

        private void QuantityTextBox_Leave(object sender, EventArgs e)
        {
            int Value = 0;
            try
            {
                int.Parse(QuantityTextBox.Text);
                //int.TryParse(QuantityTextBox.Text, out int CurrentValue);
                int CurrentValue = int.Parse(QuantityTextBox.Text);
                if (CurrentValue <= 0)
                {
                    MessageBox.Show("Zero or Less quantity not Allowed. You can use delete button to remove the product from Cart");
                    QuantityTextBox.Undo();
                    QuantityTextBox.SelectAll();
                    QuantityTextBox.Focus();

                }
                else
                {
                    for (int i = 0; i < MainForm.ProductList.Count; i++)
                    {
                        if (MainForm.ProductList[i].ProductCategory == this.ProdCat
                         && MainForm.ProductList[i].ProductID == this.ProdID)
                        {
                            if (CurrentValue <= MainForm.ProductList[i].ProductQuantity)
                            {
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
                                        //CurrentValue = CurrentValue - int.Parse(QuantityTextBox.Text);
                                        QuantityTextBox.Text = Value.ToString();
                                    }
                                }
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
                                MessageBox.Show("Not enough items in inventory");
                                QuantityTextBox.Undo();
                            }
                        }
                    }
                }
                
            }
            catch
            {
                MessageBox.Show("Only Numbers are allowed. Please enter valid quantity");
                QuantityTextBox.Focus();
                QuantityTextBox.SelectAll();
                QuantityTextBox.Undo();
            }
        }
    }
}
