/* Student Name: Niranjan Kumbhar
 * Student ID: 20230733
 * Date:19/10/2020
 * Assignment: 1
 * Assignment: Simple Table Service App
 * Application that allows “Sult Bar & Restaurant” staff to take table orders from bar customers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_1_20230733
{
    public partial class MainForm : Form
    {
        //Culture Declared Publicly to Get the currency in Based on Current Country(Ireland). 
        public CultureInfo Cult_Ireland = new CultureInfo("en-IE");

        //Constants declared for Pizza Prices
        public const decimal  Deci_pizza1 = 9,
                      Deci_pizza2 = 11.50m,
                      Deci_pizza3 = 12.79m,
                      Deci_surcharge = 2.49m;

        //Constant declared for Pizza Names
        public const string Str_pizza1 = "Margherita Pizza",
                     Str_pizza2 = "Pepperoni Pizza",
                     Str_pizza3 = "Ham Pineapple Pizza" ;

        //Paramters for calculating total Transactions and Pizza Count 
        private int Int_tot_pizza = 0,
                    Int_total_trans = 0;

        //Parameter for Calulating Total Receipt Value
        private Decimal Deci_total_receipt = 0.0m;

        public MainForm()
        {
            InitializeComponent();
            //At the initial Stage while loading form, assigning Pizza Names and Pizza Values
            Label_Pizza1_Price.Text = "@ " + Deci_pizza1.ToString("C2",Cult_Ireland);
            Label_Pizza2_Price.Text = "@ " + Deci_pizza2.ToString("C2",Cult_Ireland);
            Label_Pizza3_Price.Text = "@ " + Deci_pizza3.ToString("C2",Cult_Ireland);
            Label_Pizza1_Name.Text = Str_pizza1 ;
            Label_Pizza2_Name.Text = Str_pizza2 ;
            Label_Pizza3_Name.Text = Str_pizza3 ;


            //Displaying Top Panel Picture 
            Picture_Sult_Panel.Visible = true;
            Picture_Sult_Panel.Location = Panel_Start.Location;
            //Hiding Bottom Sult Logo Picture
            Picture_Sult_Logo.Visible = false;
            //Textbox Name and Table Number panel will take dynamic postion after Picture panel at top
            Panel_Start.Location = new Point(Picture_Sult_Panel.Location.X, Picture_Sult_Panel.Size.Height + 10);

        }

        //Method for Validating Pizza Quantity
        //If the pizza quantity is invalid, Message box with error will be shown and boolean value will be returned "True" if found error
        private bool Check_Char(string Str_input, int Int_index)
        {
            string Str_Pizza_name = "";
            bool Bool_flag = false;
            int Int_pizza_value;
            
            //Converting String input into Integer Value
            try
            {
                Int_pizza_value = int.Parse(Str_input);
            }
            catch
            {   
                //Based on Pizza Index, Displaying Error Message
                switch (Int_index)
                {
                    case 1:
                        Str_Pizza_name = Str_pizza1;
                        break;
                    case 2:
                        Str_Pizza_name = Str_pizza2;
                        break;
                    case 3:
                        Str_Pizza_name = Str_pizza3;
                        break;
                }

                MessageBox.Show("Please enter numerical data for " + Str_Pizza_name, "Enter Valid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //if there is an error, True boolean value will be returned
                Bool_flag = true;
            }

            return Bool_flag;
        }

        //Method for Converting the string into integer 
        //If textbox contains space, value will be converted to 0
        private int Check_Not_Null(string str_value)
        {
            if (str_value == "")
                return 0;
            else 
                return int.Parse(str_value);
        }

         //Method for calculating Total Pizza Count
         //Pizza Count will be calculated for current order
         //Then, Total Pizza Count will be stored in global varaible INT_TOT_PIZZA
        private string Calculate_total_pizza()
        {
            int int_curr_total;

            int_curr_total = ((Check_Not_Null(TextBox_Pizza1_Quantity.Text)) +
                                            (Check_Not_Null(TextBox_Pizza2_Quantity.Text)) +
                                            (Check_Not_Null(TextBox_Pizza3_Quantity.Text)));

            Int_tot_pizza += int_curr_total;
            return int_curr_total.ToString();
        }

        //Method for calculating total Receipt count 
        //Pizza Receipt will be calculated for current order
        //Then, Total Receipt count will be stored in gloabal variable DBL_TOTAL_RECEIPT
        private decimal Calculate_total_on_order()
        {
            decimal Deci_current_receipt;

            Deci_current_receipt = Check_Not_Null(TextBox_Pizza1_Quantity.Text) * Deci_pizza1 +
                                  Check_Not_Null(TextBox_Pizza2_Quantity.Text) * Deci_pizza2 +
                                  Check_Not_Null(TextBox_Pizza3_Quantity.Text) * Deci_pizza3 + Deci_surcharge;

            Deci_total_receipt += Deci_current_receipt;
            //Incrementing Total Transaction Count
            Int_total_trans += 1;
            return Deci_current_receipt;
        }

        // Event Handler to validate Table Number and display the error message when the user enters invalid value for table Number
        //if input is invalid, previous input will be cleared out and Table Number texbox will be focussed
        private void TextBox_Table_No_Leave(object sender, EventArgs e)
        {
            if ((TextBox_Table_No.Text.All(char.IsDigit)) == false)
            {
                MessageBox.Show("Please enter Correct Table Number.", "Error" , MessageBoxButtons.OK ,MessageBoxIcon.Error);
                TextBox_Table_No.Text = "";
                TextBox_Table_No.Focus();
                TextBox_Table_No.SelectAll();
            }
            else
                Button_Start.Focus();
        }

        // Event Handler to validate TextBox Pizza(Margherita) Quantity and display the error message when the user enters invalid for Pizza Quantity
        //if input is invalid, previous pizza quantity will be cleared out and current textbox is highlighted
        private void TextBox_Pizza1_Quantity_Leave(object sender, EventArgs e)
        {
            if ( TextBox_Pizza1_Quantity.Text != "")
            {
                if (Check_Char(TextBox_Pizza1_Quantity.Text, 1))
                {
                    TextBox_Pizza1_Quantity.Text = "";
                    TextBox_Pizza1_Quantity.Focus();
                    TextBox_Pizza1_Quantity.SelectAll();
                }
            }
        }


        // Event Handler to validate TextBox Pizza(Pepperoni) Quantity and display the error message when the user enters invalid for Pizza Quantity
        //if input is invalid, previous pizza quantity will be cleared out and current textbox is highlighted
        private void TextBox_Pizza2_Quantity_Leave(object sender, EventArgs e)
        {
            if (TextBox_Pizza2_Quantity.Text != "")
            {
                if (Check_Char(TextBox_Pizza2_Quantity.Text, 2))
                {
                    TextBox_Pizza2_Quantity.Text = "";
                    TextBox_Pizza2_Quantity.Focus();
                    TextBox_Pizza2_Quantity.SelectAll();
                }
            }
        }

        // Event Handler to validate TextBox Pizza(Pepperoni) Quantity and display the error message when the user enters invalid for Pizza Quantity
        //if input is invalid, previous pizza quantity will be cleared out and current textbox is highlighted
        private void TextBox_Pizza3_Quantity_Leave(object sender, EventArgs e)
        {
            if (TextBox_Pizza3_Quantity.Text != "")
            {
                if (Check_Char(TextBox_Pizza3_Quantity.Text, 3))
                {
                    TextBox_Pizza3_Quantity.Text = "";
                    TextBox_Pizza3_Quantity.Focus();
                    TextBox_Pizza3_Quantity.SelectAll();
                }
            }
        }

        /* Event Handler for Start Button:
           Validate if Server Name and Table Number is entered or not
           if Yes, GUI changes will be made*/
        private void Button_Start_Click(object sender, EventArgs e)
        {   
            if (TextBox_Server_Name.Text == "")
            {
                MessageBox.Show("Please enter server name before taking order.", "Server Name Required", MessageBoxButtons.OK,MessageBoxIcon.Error);
                TextBox_Server_Name.Focus();
            }  
            else if (TextBox_Table_No.Text == "")
            {
                MessageBox.Show("Please enter table number before taking order.", "Table Name Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextBox_Table_No.Focus();
            }
            else
            {
                //Defaulting Pizza Quantity textboxes to 0
                TextBox_Pizza1_Quantity.Text = TextBox_Pizza2_Quantity.Text = TextBox_Pizza3_Quantity.Text = "0";
                //Displaying Sult Logo at bottom
                Picture_Sult_Logo.Visible = true;
                //Hiding Sult Title Picture located at top
                Picture_Sult_Panel.Visible = false;
                //Current Panel will be getting hidden
                Panel_Start.Visible = false;
                //Pizza Menu Will be shown along with Multiple Buttons Panel, Location will be taken from Sult Title Picture
                GroupBox_Pizza_Menu.Location = new Point(Picture_Sult_Panel.Location.X, Picture_Sult_Panel.Location.Y);
                GroupBox_Pizza_Menu.Visible = true;
                //Button such as Order, Clear will be shown as part of Panel, Location will be dynamically calculated from Pizza Menu
                Panel_Controls.Location = new Point(GroupBox_Pizza_Menu.Location.X , (GroupBox_Pizza_Menu.Location.Y + GroupBox_Pizza_Menu.Size.Height + 4));
                Panel_Controls.Visible = true;
                //Pizza 1 Quantity will have the focus 
                TextBox_Pizza1_Quantity.Focus();
                //Form Title will be changed
                MainForm.ActiveForm.Text = TextBox_Server_Name.Text + " @ Table Number " + TextBox_Table_No.Text;

                
            }
        }

        /*  Event Handler For Order Button:
         *  Displaying tooltip for Clear Button
            If Pizza quantity is 0 or Null, system would throw an Exclamatory message guiding user to add pizza quantity
            If Summary Button Clicked before Order button, then system will check if Summary groupbox is open or not. If yes, Summary Groupbox will be hidded.
         */
        private void Button_Order_Click(object sender, EventArgs e)
        {
            if (GroupBox_Company_Summary.Visible == true)
            {
                GroupBox_Company_Summary.Visible = false;
                GroupBox_Pizza_Menu.Visible = true;
                toolTip1.Hide(Button_Exit);
                MainForm.ActiveForm.Text = TextBox_Server_Name.Text + " @ Table Number " + TextBox_Table_No.Text;
            }
            else
            {
                //Check if Textbox quantity is having null or 0 value
                if (Check_Not_Null(TextBox_Pizza1_Quantity.Text) == 0  &
                Check_Not_Null(TextBox_Pizza2_Quantity.Text) == 0 &
                Check_Not_Null(TextBox_Pizza3_Quantity.Text) == 0 )
                {
                    MessageBox.Show("Please select atleast 1 pizza quantity before Confirming the order", "Pizza Quantity Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    //Added Messagebox to take user input if weather user wants to change his inputs or wants to confirm the order
                    DialogResult dialogResult = MessageBox.Show(Str_pizza1 + " : " + Check_Not_Null(TextBox_Pizza1_Quantity.Text).ToString()
                                                                   + Environment.NewLine + Str_pizza2 + " : " + Check_Not_Null(TextBox_Pizza2_Quantity.Text).ToString()
                                                                   + Environment.NewLine + Str_pizza3 + " : " + Check_Not_Null(TextBox_Pizza3_Quantity.Text).ToString()
                                                                   + Environment.NewLine + Environment.NewLine + "*Service charge of €2.49 will be added to the bill"
                                                                   , TextBox_Server_Name.Text + ", Confirm the Pizza order @ Table" + TextBox_Table_No.Text, MessageBoxButtons.YesNo);
                    //if user press Yes to Confirm order
                    if (dialogResult == DialogResult.Yes)
                    {
                        //Making Order Details Groupbox Visible
                        GroupBox_Table_Order.Visible = true;
                        //Locating Order Details groupbox dynamically after Button Panel
                        GroupBox_Table_Order.Location = new Point(Panel_Controls.Location.X, Panel_Controls.Location.Y + Panel_Controls.Size.Height + 20);
                        //Fetching and Calculating Values
                        TextBox_summary_name.Text = TextBox_Server_Name.Text;
                        TextBox_summary_pizza.Text = Calculate_total_pizza();
                        TextBox_summary_receipt.Text = Calculate_total_on_order().ToString("C2", Cult_Ireland);
                        //Disabling Order Button 
                        Button_Order.Enabled = false;
                        //Disabling Pizza Menu
                        GroupBox_Pizza_Menu.Enabled = false;
                        //Displaying ToolTip on Clear Button
                        toolTip1.Show(toolTip1.GetToolTip(Button_Clear), Button_Clear);
                    }
                }
            }
        }

        /*  Event Handler For Clear Button:
            Clearing all the quantity textboxes and defaulting them to 0
            Clearing the Server Name and Table Number
            Clearing Summary Textboxes
         */
        private void Button_Clear_Click(object sender, EventArgs e)
        {
            toolTip1.Hide(Button_Clear);
            toolTip1.Hide(Button_Exit);
            GroupBox_Pizza_Menu.Visible = false;
            Panel_Controls.Visible = false;
            GroupBox_Table_Order.Visible = false;
            GroupBox_Company_Summary.Visible = false;
            Panel_Start.Visible = true;
            Picture_Sult_Panel.Visible = true;
            Picture_Sult_Logo.Visible = false;
            Button_Order.Enabled = true ;
            GroupBox_Pizza_Menu.Enabled = true;

            //Reverting Title of the Form
            MainForm.ActiveForm.Text = "Welcome to Sult";
            TextBox_Server_Name.Text = TextBox_Table_No.Text = "";
            TextBox_Pizza1_Quantity.Text = TextBox_Pizza2_Quantity.Text = TextBox_Pizza3_Quantity.Text = "0";
            TextBox_summary_name.Text = TextBox_summary_pizza.Text = TextBox_summary_receipt.Text = "";
            //Setting Input focus on Server Name
            TextBox_Server_Name.Focus();

        }

        /*  Event Handler For Summary Button:
            Displaying Tooltip for Exit Button
            Accessing and Assigning calculated values after summary button click
         */
        private void Button_Summary_Click(object sender, EventArgs e)
        {
            toolTip1.Hide(Button_Clear);
            toolTip1.Show(toolTip1.GetToolTip(Button_Exit), Button_Exit);
            GroupBox_Table_Order.Visible = false;
            GroupBox_Pizza_Menu.Visible = false;
            GroupBox_Company_Summary.Visible = true;
            GroupBox_Company_Summary.Location = GroupBox_Table_Order.Location;

            //Total Company Transactions
            TextBox_tot_comp_trans.Text = Int_total_trans.ToString();
            //Total Number of Pizza's
            TextBox_tot_num_pizza.Text = Int_tot_pizza.ToString();
            //Total Company Receipts
            TextBox_tot_comp_receipt.Text = Deci_total_receipt.ToString("C2", Cult_Ireland);
            //Calculating Average Transaction Value
            TextBox_avg_trans_val.Text = (Deci_total_receipt / Int_total_trans).ToString("C2", Cult_Ireland);
            MainForm.ActiveForm.Text = "Sult Company Summary Data";
        }

        /*  Event Handler For Exit Button:*/
        private void Button_Exit_Click(object sender, EventArgs e)
        {
            //Closing Application since we are using only one Form 
            MainForm.ActiveForm.Close();
        }
    }
}
