using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_2_20230733
{
    public partial class BusTour : Form
    {
        //Global Constants
        const int DESTINATIONFARE1 = 55,
                  DESTINATIONFARE2 = 50,
                  DESTINATIONFARE3 = 75,
                  DESTINATIONFARE4 = 45,
                  DESTINATIONFARE5 = 75,
                  DESTINATIONFARE6 = 99,
                  TIMEDISCOUNT1 = 20,
                  TIMEDISCOUNT2 = 10,
                  TIMEDISCOUNT3 = 5,
                  TIMEDISCOUNT6 = 25,
                  HOTELFARE1 = 100,
                  HOTELFARE2 = 75,
                  HOTELFARE3 = 55;

        const decimal  LUNCHCHARGE = 11.50m,
                       SPECIALDISCOUNT = 7.5m;

        

        public BusTour()
        {
            InitializeComponent();
        }

        public int TotalTransactions;
        public decimal TotalTripCost = 0, TotalOptionalValues = 0, AverageRevenue = 0;
        private decimal CurrentTotalBusFare,
                        CurrentTotalOptional,
                        CurrentTotalPayableLabel;
        //Event for Form Load
        //It will Initialize Main Screen and Set No.of Guest to 1
        private void BusTour_Load(object sender, EventArgs e)
        {
            //Defaulting No of Guest
            NoOfGuestTextBox.Text           = "1";
            //Hiding and Disabling Buttons
            BookButton.Enabled              = false;
            SummaryButton.Enabled           = false;
            DisplayPanel.Visible            = false;
            ModifyBookingButton.Visible     = false;
            //Hiding Display Controls
            TimeDiscountPanel.Visible       = false;
            HotelBookingGroupBox.Visible    = false;
            PackedLunchGroupBox.Visible     = false;
            SpecialDiscountPanel.Visible    = false;
            SummaryPanel.Visible            = false;

        }

        /* Event Handler for Display Button
         *1. Perform Validation on 'No.of Guest' Input, Listbox Selection and Hotel Radio Button Selection
         *2. Setting Base fare, Time Discount, Hotel Cost and Lunch cost
         *3. Calculate Discount and Final Cost
         *4. Provide choice to user if he wants to change the selection after displaying short inshorts about trip
         *5. if Yes: Booking Breakup will be displayed.  if No: Keep on the same main screen so that user can change selection
        */
        private void DisplayButton_Click(object sender, EventArgs e)
        {
            //Local Variable Declaration
            int DestinationIndex, TimeIndex, BaseFareValue = 0, DiscountPercent = 0, NoOfGuestsValue = 0,
                 FareBeforeTimeDiscount = 0, HotelPriceValue = 0, TotalHotelCharge = 0;

            decimal TimeDiscountValue = 0.0m, TotalBusFare = 0.0m, TotalLunchCharge = 0.0m, TotalPayableCharge = 0.0m,
                    SpecialDiscountValue = 0.0m;

            string HotelPreference = "", LunchPreference = "", StringMessage = "";

            DialogResult DialogRe;

            //Validating No. of Guests Input
            try
            {
                int value = int.Parse(NoOfGuestTextBox.Text);
                if (value > 0)//if value is greater than zero
                {
                    //Performing Destination and Time based Validations
                    if (DestinationListBox.SelectedIndex == -1)
                    {
                        MessageBox.Show("Please Select Destination before proceeding the booking", "Choose Destination", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (TimeListBox.SelectedIndex == -1)
                    {
                        MessageBox.Show("Please select departure time before proceeding the booking", "Choose Time", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (DestinationListBox.SelectedIndex != -1 && TimeListBox.SelectedIndex != -1)
                    {
                        //Setting Hotel Prices
                        if (Hotel1RadioButton.Checked)
                        {
                            HotelPreference = "5-Star Stay";
                            HotelPriceValue = HOTELFARE1;
                        }
                        else if (Hotel2RadioButton.Checked)
                        {
                            HotelPreference = "4-Star Stay";
                            HotelPriceValue = HOTELFARE2;
                        }
                        else if (Hotel3RadioButton.Checked)
                        {
                            HotelPreference = "3-Star Stay";
                            HotelPriceValue = HOTELFARE3;
                        }
                        else if (NoHotelRadioButton.Checked)
                            HotelPriceValue = 0;
                        else
                        {
                            //if No Radiobutton has been selected
                            MessageBox.Show("Please choose Hotel before proceeding", "Hotel Preference Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            HotelPriceValue = -1;
                        }

                        //if User has provided every input on mainscreen
                        if(HotelPriceValue != -1)
                        {
                            DestinationIndex = DestinationListBox.SelectedIndex;
                            TimeIndex = TimeListBox.SelectedIndex;

                            //Setting Destination Price
                            switch (DestinationIndex)
                            {
                                case 0: BaseFareValue = DESTINATIONFARE1; break;
                                case 1: BaseFareValue = DESTINATIONFARE2; break;
                                case 2: BaseFareValue = DESTINATIONFARE3; break;
                                case 3: BaseFareValue = DESTINATIONFARE4; break;
                                case 4: BaseFareValue = DESTINATIONFARE5; break;
                                case 5: BaseFareValue = DESTINATIONFARE6; break;
                            }

                            //Setting Time based Discount
                            switch (TimeIndex)
                            {
                                case 0: DiscountPercent = TIMEDISCOUNT1; break;
                                case 1: DiscountPercent = TIMEDISCOUNT2; break;
                                case 2: DiscountPercent = TIMEDISCOUNT3; break;
                                case 3: DiscountPercent = 0; break;
                                case 4: DiscountPercent = 0; break;
                                case 5: DiscountPercent = TIMEDISCOUNT6; break;

                            }

                            //Getting Destination Name and Time From Listbox
                            LocationLabel.Text      = DestinationListBox.Items[DestinationIndex].ToString().Substring(0, 17);
                            TimeLabel.Text          = TimeListBox.Items[TimeIndex].ToString().Substring(0, 6);
                            //Additional- Booking Status Information
                            BookingStatusLabel.Text = "Not Confirmed";

                            //Performing Calculations
                            NoOfGuestsLabel.Text            = NoOfGuestsLabel1.Text = NoOfGuestsLabel2.Text = NoOfGuestTextBox.Text;
                            NoOfGuestsValue                 = int.Parse(NoOfGuestTextBox.Text);
                            BaseFareLabel.Text              = "€ " + BaseFareValue.ToString();
                            FareBeforeTimeDiscount          = NoOfGuestsValue * BaseFareValue;
                            BaseFareTotalLabel.Text         = "€ " + FareBeforeTimeDiscount.ToString();

                            //Time Based Discount
                            if (DiscountPercent != 0)
                            {
                                TimeDiscountPanel.Visible   = true;
                                DiscountTimeLabel.Text      = DiscountPercent.ToString() +" %";
                                TimeDiscountValue           = FareBeforeTimeDiscount * (DiscountPercent / 100m);
                                TotalDiscountTimeLabel.Text = "€ " + TimeDiscountValue.ToString("n2");
                                TotalDiscountTimeLabel.ForeColor = Color.Red;
                            }
                            else
                            {
                                TimeDiscountPanel.Visible   = false;
                                TotalDiscountTimeLabel.Text = "";
                                TimeDiscountValue           = 0.0m;
                            }

                            // After Time Based Discount
                            TotalBusFare                    = FareBeforeTimeDiscount - TimeDiscountValue; //(A)
                            TotalBusFareLabel.Text          = "€ " + TotalBusFare.ToString();//(A)

                            //Hotel Selection Calculation
                            if (HotelPriceValue != 0)
                            {
                                HotelBookingGroupBox.Visible= true;
                                HotelPreferenceLabel.Text   = HotelPreference;
                                HotelPriceLabel.Text        = "€ " + HotelPriceValue.ToString();
                                TotalHotelCharge            = HotelPriceValue * NoOfGuestsValue;
                                HotelTotalLabel.Text        = "€ " + TotalHotelCharge.ToString();
                            }
                            else
                            {
                                HotelBookingGroupBox.Visible = false;
                                TotalHotelCharge            = 0;
                            }

                            //Packed Lunch Cost Calculation
                            if (LunchCheckBox.Checked)
                            {
                                PackedLunchGroupBox.Visible = true;
                                PackedLunchLabel.Text       = "€ " + LUNCHCHARGE.ToString();
                                TotalLunchCharge            = NoOfGuestsValue * LUNCHCHARGE;
                                LunchPreferenceLabel.Text   = LunchPreference;
                                PackedLunchTotalLabel.Text  = "€ " + TotalLunchCharge.ToString();
                            }
                            else
                            {
                                PackedLunchGroupBox.Visible = false;
                                TotalLunchCharge = 0.0m;
                            }

                            //Before Special Discount Calculation (B)
                            TotalOptionalCostLabel.Text = "€ " +(TotalHotelCharge + TotalLunchCharge).ToString("n2"); 
                            TotalPayableCharge          = TotalBusFare + TotalHotelCharge + TotalLunchCharge;

                            //If No.og Guests are more than 3,
                            //   Hotel and Packed Lunch are selected
                            // User will be eligible for 7.5% discount on booking price
                            if (NoOfGuestsValue >= 3
                            && HotelPriceValue != 0
                            && LunchCheckBox.Checked == true)
                            {
                                SpecialDiscountPanel.Visible    = true;
                                SpecialDiscountLabel.Text       = SPECIALDISCOUNT.ToString("n2")+ " %";
                                SpecialDiscountValue            = TotalPayableCharge * (SPECIALDISCOUNT /100m); //0.075m
                                TotalPayableCharge              = TotalPayableCharge - SpecialDiscountValue; //(B)
                                SpecialDiscountTotalLabel.Text  = "€ " + SpecialDiscountValue.ToString("n2");
                                SpecialDiscountTotalLabel.ForeColor = Color.Red ;
                            }
                            else
                                SpecialDiscountPanel.Visible = false;

                            //Final Amount ( A+ B )
                            TotalPayableLabel.Text = "€ " + TotalPayableCharge.ToString("n2");
                            TotalPayableLabel.ForeColor = Color.Green;

                            //Generating Message to for Popup based on User Selections
                            StringMessage = "\n Selected Destination   : " + LocationLabel.Text +
                                            "\n Selected Depart. Time  : " + TimeLabel.Text +
                                            "\n Provided No.of Guest   : " + NoOfGuestsLabel.Text +
                                            "\n\nBus Booking Charges  : " + TotalBusFareLabel.Text;
                                            
                            if (HotelPriceValue != 0)
                                StringMessage = StringMessage + "\nHotel Booking Charges  : " + HotelTotalLabel.Text;
                            if (LunchCheckBox.Checked)
                                StringMessage = StringMessage + "\nPacked Lunch Charges   : " + PackedLunchTotalLabel.Text;

                            StringMessage = StringMessage + "\n\nFinal Booking Cost   : " + TotalPayableLabel.Text +
                                                            "\n\nDo you want to proceed with current selection booking(Yes) or Change Selection(No)?";

                            DialogRe = MessageBox.Show(StringMessage, "Booking Cost", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            //Taking User input for Proceeding with the booking
                            if(DialogRe == DialogResult.Yes)
                            {   //If Yes, Displaying Breakup of Booking will be displayed
                                this.Text = "Booking of" + LocationLabel.Text + " for " + NoOfGuestTextBox.Text + " Guests";
                                //Setting Master Data for Current Booking
                                CurrentTotalBusFare             = TotalBusFare;
                                CurrentTotalOptional            = TotalHotelCharge + TotalLunchCharge;
                                CurrentTotalPayableLabel        = TotalPayableCharge;
                                //Making GUI Changes
                                TourCostGroupBox.Visible        = true;
                                BookingStatusLabel.ForeColor    = Color.Red;
                                HighlightPanel.Location         = new Point(2, 2);
                                DisplayPanel.Visible            = true;
                                DisplayPanel.Location           = new Point(12, 203);
                                DisplayButton.Visible           = false;
                                ModifyBookingButton.Location    = BookButton.Location;
                                ModifyBookingButton.Visible     = true;
                                BookButton.Location             = DisplayButton.Location;
                                BookButton.Enabled              = true;
                                if (BookButton.Visible == false)
                                {
                                    BookButton.Location         = DisplayButton.Location;
                                    BookButton.Visible          = true;
                                    ModifyBookingButton.Location = new Point(3,104);
                                    ModifyBookingButton.Enabled = true;
                                }
                                BookButton.Focus();
                                SelectOptionsPanel.Visible = false;
                                //Showing Tooltip on Book Button 'Click to Confirm Booking'
                                toolTip1.Show(toolTip1.GetToolTip(BookButton), BookButton);
                                toolTip1.ToolTipTitle = "Next Step";
                                
                            }
                        }
                    }
                }
                else if(value < 0)//If user provides Negative Number
                    MessageBox.Show("Please enter positive number", "Negative no. of guests not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else//if user provides value '0' in No of guests
                    MessageBox.Show("Please enter atleast 1 guest", "Atleast 1 guest required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch//if Invalid input provided
            {
                MessageBox.Show("Invalid Input Provided. Please provide a Number","Correct Input Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NoOfGuestTextBox.Focus();
                NoOfGuestTextBox.SelectAll();
            }
        }


        /* Event Hander for Book Button
         * 1. Calculate Transaction Operations such as No. of Transactions, Bus Cost, Optional Cost and Average Revenure
         * 2. Provides Success Message after Completing of Booking
        */
        private void BookButton_Click(object sender, EventArgs e)
        {
            decimal CurrentAverage          = 0.0m;
            string StringMessage            = "";

            toolTip1.Active = false;
            toolTip1.Active = true;
            //Performing Calculations
            HighlightPanel.Location         = new Point(2, BookButton.Location.Y);
            TotalTransactions               += 1;
            TotalTripCost                   += CurrentTotalBusFare;
            TotalOptionalValues             += CurrentTotalOptional;
            CurrentAverage                  = CurrentTotalPayableLabel / int.Parse(NoOfGuestsLabel.Text);
            AverageRevenue                  = (AverageRevenue + CurrentAverage) / TotalTransactions;
            //Add-on Functionality- Changing Booking Status to Confirmed and Changing Color of Text
            BookingStatusLabel.Text         = "Confirmed";
            BookingStatusLabel.ForeColor    =  Color.Green;

            //Disabling Booking Details along with Book Button and Modify Button
            if(SummaryButton.Enabled != true)
                SummaryButton.Enabled = true;//Enabling Summary Button after first Booking

            BookButton.Enabled              = ModifyBookingButton.Enabled
                                            = TourCostGroupBox.Visible
                                            = TimeDiscountPanel.Visible
                                            = FareTripTextLabel.Visible
                                            = TotalBusFareLabel.Visible
                                            = HotelBookingGroupBox.Visible
                                            = PackedLunchGroupBox.Visible
                                            = TotalOptionalTextLable.Visible
                                            = TotalOptionalCostLabel.Visible
                                            = SpecialDiscountPanel.Visible
                                            = false;
                   
            
            //Dynamically creating Message to display based on user inputs
            StringMessage = "Destination Name\t:   " + LocationLabel.Text +
                            "\nDeparture Time\t:   " + TimeLabel.Text +
                            "\nNo of Guests\t:   " + NoOfGuestTextBox.Text +
                            "\n\nBus Trip Charges \t\t\t :" + "€" + CurrentTotalBusFare.ToString();

            if (Hotel1RadioButton.Checked)
                StringMessage = StringMessage + "\n\nHotel Booking: \n5-Star Hotel Booking \t\t :" + HotelTotalLabel.Text;
            else if(Hotel2RadioButton.Checked)
                StringMessage = StringMessage + "\n\nHotel Booking: \n4-Star Hotel Booking \t\t :" + HotelTotalLabel.Text;
            else if(Hotel3RadioButton.Checked)
                StringMessage = StringMessage + "\n\nHotel Booking: \n3-Star Hotel Booking \t\t :" + HotelTotalLabel.Text;

            if (LunchCheckBox.Checked)
                StringMessage = StringMessage + "\n\nPacked Lunch Charges: \t\t :" + PackedLunchTotalLabel.Text;

            StringMessage = StringMessage + "\n\n Total Tour Charges\t\t:" + "€" + CurrentTotalPayableLabel.ToString("n2");
            MessageBox.Show(StringMessage, "Booking Successful",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);

            ClearButton.Focus();
            //Showing Clear Button Tooltip 'Click to take Next Booking'
            toolTip1.Show(toolTip1.GetToolTip(ClearButton), ClearButton);
        }

        /* Event Handler for Summary Button
         * Display Sales Insights to Salesperson
        */
        private void SummaryButton_Click(object sender, EventArgs e)
        {
            this.Text = "Summary of Mild Atlantic Bus Tour (MABT)";
            //Setting Label Texts
            TotalTransTextLabel.Text        = "Total Number of Transactions";
            TotalTripTextLabel.Text         = "Total Trip Fees";
            TotalOptionTextLabel.Text       = "Total Value of Options Chosen";
            AverageTextLabel.Text           = "Average Revenue Per Booking";
            //Setting Label Values 
            TotalTransactionsLabel.Text     = TotalTransactions.ToString();
            TotalTripFeesLabel.Text         = "€ " + TotalTripCost.ToString("n2");
            TotalValueOfOptionsLabel.Text   = "€ " + TotalOptionalValues.ToString("n2");
            AverageRevenueLabel.Text        = "€ " + AverageRevenue.ToString("n2");
            ///Performing GUI changes
            HighlightPanel.Location         = new Point(2, 214);
            DisplayPanel.Visible            = false;
            DisplayButton.Enabled           = false;
            SummaryPanel.Location           = DisplayPanel.Location;
            SummaryPanel.Visible            = true;
        }

        /* Event Handler for Clear Button
        * Re-initializing controls and clearing current booking variables for bug free results
        * Setting Title for Form
        * Removing Selection of Listboxes, Radiobuttons and Checkbox
        */
        private void ClearButton_Click(object sender, EventArgs e)
        {

            CurrentTotalBusFare = CurrentTotalOptional = CurrentTotalPayableLabel = 0;
            this.Text                           = "Mild Atlantic Bus Tours";
            HighlightPanel.Location             = new Point(2, 321);
            DestinationListBox.SelectedIndex    = -1;
            TimeListBox.SelectedIndex           = -1;
            NoOfGuestTextBox.Text               = "1";
            LunchCheckBox.Checked               = false;
            Hotel1RadioButton.Checked           = Hotel2RadioButton.Checked 
                                                = Hotel3RadioButton.Checked 
                                                = NoHotelRadioButton.Checked 
                                                = false;
            DisplayButton.Visible               = true;
            ModifyBookingButton.Visible         = false;
            DisplayButton.Enabled               = true;
            BookButton.Visible                  = true;
            BookButton.Location                 = new Point(3,104);
            BookButton.Enabled                  = false;
            SummaryPanel.Visible                = false;
            SelectOptionsPanel.Visible          = true;
            DisplayPanel.Visible                = false;
            

        }

        //Event Handler for Exit Button
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /* Event Handler for Modify Booking Button
         * 1. If User still want to change location after confirming the "Proceed Booking" Popup Message,
         *   He can retain his previous selection and can make changes as required
         */
        private void ModifyBookingButton_Click(object sender, EventArgs e)
        {
            HighlightPanel.Location         = new Point(2, ModifyBookingButton.Location.Y);
            DisplayPanel.Visible            = false;
            SelectOptionsPanel.Visible      = true;
            DisplayButton.Visible           = true;
            BookButton.Visible              = false;
            ModifyBookingButton.Visible     = true;
            ModifyBookingButton.Enabled     = false;
        }



    }
}
