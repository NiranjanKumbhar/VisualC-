using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EPOS_APPLICATION_20230733
{
    public partial class ProductList : UserControl
    {
        
        public ProductList()
        {
            InitializeComponent();
        }


        #region Properties

        private string _ProductName;
        private string _ProductCat;
        private string _ProductID;
        private decimal _ProductPrice;
        private Image _ProductIcon;
        private bool _OutOfStock;
        private bool _ProdAddButtonEnable;


        [Category("Custom List")]
        public string ProdName
        {
            get { return _ProductName; }
            set { _ProductName = value; ProductNameLabel.Text = value; }
        }

        [Category("Custom List")]
        public string ProdCat
        {
            get { return _ProductCat; }
            set { _ProductCat = value; }
        }

        [Category("Custom List")]
        public string ProdID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }

        [Category("Custom List")]
        public decimal ProdPrice
        {
            get { return _ProductPrice; }
            set { _ProductPrice = value; ProductPriceLabel.Text = "€" + value.ToString(); }
        }



        [Category("Custom List")]
        public Image ProdImage
        {
            get { return _ProductIcon; }
            set { _ProductIcon = value; pictureBox1.Image = value; }
        }

        [Category("Custom List")]
        public bool ProdAddButtonStatus
        {
            get { return _ProdAddButtonEnable; }
            set { _ProdAddButtonEnable = value; AddToCartButton.Enabled = value; }
        }


        [Category("Custom List")]
        public bool ProdOutofStock
        {
            get { return _OutOfStock; }
            set { _OutOfStock = value; AddToCartButton.Enabled = value; }
        }

        #endregion

        private void AddToCartButton_Click(object sender, EventArgs e)
        {
            MainForm frm = (MainForm)this.FindForm();

            CartList[] NewCartItem = new CartList[1];

            for (var i = 0; i < MainForm.ProductList.Count; i++)
            {
                if (MainForm.ProductList[i].ProductCategory == this.ProdCat
                    && MainForm.ProductList[i].ProductID == this.ProdID)
                {
                    NewCartItem[0] = new CartList();
                    NewCartItem[0].ProdName = MainForm.ProductList[i].ProductName;
                    NewCartItem[0].ProdCat = MainForm.ProductList[i].ProductCategory;
                    NewCartItem[0].ProdID = MainForm.ProductList[i].ProductID;
                    NewCartItem[0].ProdPrice = MainForm.ProductList[i].ProductPrice;
                    NewCartItem[0].ProdTotal = MainForm.ProductList[i].ProductPrice;
                    NewCartItem[0].ProdQuantity = 1;
                    MainForm.GrandTotal += MainForm.ProductList[i].ProductPrice;
                    MainForm.TotalItems += 1;
                    frm.CartFlowLayoutPanel.Controls.Add(NewCartItem[0]);
                    frm.TotalItemsLabel.Text = MainForm.TotalItems.ToString();
                    frm.GrandTotalLabel.Text = MainForm.GrandTotal.ToString();
                    MainForm.CurrentCartProducts.Add(ProdID);
                    break;
                }
            }
            AddToCartButton.Enabled = false;
        }
    }
}
