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
    public partial class ProductCategoryList : UserControl
    {
        public ProductCategoryList()
        {
            InitializeComponent();
        }

        #region Properties

        private string _CategoryName;

        [Category("Custom List")]
        public string CatName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; CategoryButton.Text = value; }
        }

        #endregion

        private void CategoryButton_Click(object sender, EventArgs e)
        {
            MainForm frm = (MainForm)this.FindForm();

            ProductList[] NewProducts = new ProductList[1000];
            int j = 0;

            frm.ProductsFlowLayoutPanel.Controls.Clear();

            for (var i = 0; i < MainForm.ProductList.Count; i++)
            {
                if (MainForm.ProductList[i].ProductCategory == this.CatName)
                {
                    NewProducts[j]              = new ProductList();

                    NewProducts[j].ProdName             = MainForm.ProductList[i].ProductName;
                    NewProducts[j].ProdPrice            = MainForm.ProductList[i].ProductPrice;
                    NewProducts[j].ProdCat              = MainForm.ProductList[i].ProductCategory;
                    NewProducts[j].ProdID               = MainForm.ProductList[i].ProductID;


                    if (MainForm.ProductList[i].ProductQuantity == 0)
                        NewProducts[j].ProdAddButtonStatus = false;
                    else
                        NewProducts[j].ProdAddButtonStatus = true;

                    if (MainForm.CurrentCartProducts.Count > 0)
                    {
                        foreach (string Cartitem in MainForm.CurrentCartProducts)
                        {
                            if(Cartitem == NewProducts[j].ProdID)
                                NewProducts[j].ProdAddButtonStatus = false;
                        } 
                    }

                    frm.ProductsFlowLayoutPanel.Controls.Add(NewProducts[j]);
                    j++;
                }
            }
        }
    }
}
