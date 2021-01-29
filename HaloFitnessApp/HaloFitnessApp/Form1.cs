/* Student Name: Niranjan Kumbhar
 * Student ID: 20230733
 * Date:07/12/2020
 * Assignment: 3
 * Assignment: Developing ‘Halo Application’ to create a membership sales management for Health and Fitness Club
 */

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace HaloFitnessApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

        }

        //------------------------Constant Declarations---------------------
        const int INCREMENT = 2, FORMWIDTH = 954, FORMSTARTHEIGHT = 380, FORMEXPANDHEIGHT = 620, PASSWORDATTEMPTS = 3, MONTHBASEPRICE = 59,
                   RECORDLENGTH = 7, FORMSEARCHEXPANDHEIGHT = 720, MONTH1 = 0, MONTH2 = 2, MONTH3 = 3, MONTH4 = 6, MONTH5 = 7, MONTH6 = 12,
                    MONTH7 = 13, MONTH8 = 18, MONTH9 = 19, MONTH10 = 24, MONTH11 = 25, MONTH12 = 60;

        const decimal DISCOUNT1 = 0, DISCOUNT2 = 10, DISCOUNT3 = 20, DISCOUNT4 = 25, DISCOUNT5 = 33.33m, DISCOUNT6 = 40, DISCOUNT7 = 66.66m;

        const string STOREDPASSWORD = "ILuvVisualC#", NEXTTERMTEXT = "Price @ Start Next Full Term";

        //------------------------Global Variable Declarations---------------------
        int PasswordCounter, ConfirmedTerm, NextMonth, TotalMembershipTerms;
        decimal ConfirmedTermTotal, TotalMembershipFees, EnquiredPrice, SuggestedPrice;
        StreamReader InputFile;

        string DatabaseFileName = "Database.txt";


        //----------------------- FILE METHODS STARTS HERE--------------------------

        //Method for Creating file if file does not exist
        private void CreateFile()
        {
            if (!File.Exists(DatabaseFileName))
            {
                var myFile = File.Create(DatabaseFileName);
                myFile.Close();
            }
        }

        //Method for Opening file 
        private void OpenFile()
        {
            InputFile = File.OpenText(DatabaseFileName);
        }

        //Method for Closing file 
        private void CloseFile()
        {
            InputFile.Close();   
        }

        
        /* Input- Membership_ID , For Click event in Summary- True, For Search Event - True
         * Returns 'True' value if Membership ID found in file
         * 
         * Method used to check if Membership ID exist in file- Generation of Membership ID 
         * Method reused for Click Event-Details are displayed in Membership Detail Group Box
         * Method reused for Search Event-Detailes are displayed in Search Detail Group box */
        private bool MembershipIdExists(string ID, bool ClickEvent, bool SearchEvent)
        {
            bool ReturnParameter = false;
            string FileLine;
            
            try
            {
                OpenFile();
                while (!InputFile.EndOfStream)
                {
                    //Reading First line which is Membership ID in File
                    FileLine = InputFile.ReadLine();
                    
                    //Comparing Input ID with File ID
                    if (ID == FileLine)
                    {
                        ReturnParameter = true;                       
                    }
                    //For Click and Search event 
                    if (ClickEvent == true || SearchEvent == true)
                    {
                        for (int i = 0; i < RECORDLENGTH - 1; i++)
                        {
                            FileLine = InputFile.ReadLine();

                            //If its Click event in Summary- method will display information in lables and textboxes in Membership Details GroupBox
                            if (ClickEvent == true)
                            {
                                if (MembershipIDLabel.Text == "")
                                    MembershipIDLabel.Text = ID;

                                switch (i)
                                {
                                    case 0: JoinDayLabel.Text           = FileLine; break;
                                    case 1: FullNameTextBox.Text        = FileLine; break;
                                    case 2: TelephoneTextBox.Text       = FileLine; break;
                                    case 3: EmailTextBox.Text           = FileLine; break;
                                }
                            }

                            //Search Event under Search button
                            if (SearchEvent == true)
                            {
                                SearchMemIDLabel.Text = ID;
                                switch (i)
                                {
                                    case 0: SearchJoinDateLabel.Text    = FileLine; break;
                                    case 1: SearchFullNameLabel.Text    = FileLine; break;
                                    case 2: SearchTelephoneLabel.Text   = FileLine; break;
                                    case 3: SearchEmailLabel.Text       = FileLine; break;
                                    case 4: SearchMemTermLabel.Text     = FileLine; break;
                                    case 5: SearchMemFeesLabel.Text     = FileLine; break;
                                }
                            }
                        }
                    }

                    //If Membership ID is found, we are coming out of while loop
                    if(ReturnParameter==true)
                        break;
                }
                //Clsoing File
                CloseFile();
            }
            catch (Exception ex)
            {
                // Display an error message.
                MessageBox.Show(ex.Message);
            }

            return ReturnParameter;
        }

        /* Method which will return Number of Members at procesing time 
         * Method reused in LoadEvent of current form, which will check if current file have records or not.
         **/
        private int GetSummaryDetails(bool LoadEvent)
        {
            int ReturnCountRecords =0;
            string FileLine;
            try
            {
                //Opening and Reading File
                OpenFile();
                //Reading file till we get the end of stream
                while (!InputFile.EndOfStream)
                {
                    FileLine        = InputFile.ReadLine();

                    //Adding Membership ID's to Summary List Box
                    if (!LoadEvent)
                        MembershipIDListBox.Items.Add(FileLine);
                    else
                    {
                        ReturnCountRecords = 1;
                        break;
                    }
                        
                    //Counting Total Records
                    ReturnCountRecords += 1;

                    for (int i = 0; i < RECORDLENGTH - 1; i++)
                    {
                        FileLine = InputFile.ReadLine();
                        //Counting Total membership terms
                        if(i == 4)
                        {
                            TotalMembershipTerms += int.Parse(FileLine);
                        }
                        //Calculating Total Membership fees
                        else if(i == 5)
                        {
                            TotalMembershipFees += decimal.Parse(FileLine);
                        }
                    }
                }
                //Closing File
                CloseFile();
            }
            catch (Exception ex)
            {
                // Display an error message.
                MessageBox.Show(ex.Message);
            }
            return ReturnCountRecords;
        }

        private bool SearchByDate(string InputDate)
        {
            bool Found = false, NoFurtherResults = false;
            string CurrentID, FileLine;

            try
            {
                //Opening and Reading File
                OpenFile();
                //Reading file until we get end of stream
                while (!InputFile.EndOfStream)
                {
                    CurrentID = InputFile.ReadLine();
                    
                    for (int i = 0; i < RECORDLENGTH - 1; i++)
                    {
                        FileLine = InputFile.ReadLine();
                        if(i ==0)
                        {
                            //Comparing Input Date with File Date
                            if (InputDate == FileLine)
                            {
                                //Adding Membership ID to List if Date match with File 
                                DateIDListBox.Items.Add(CurrentID);
                                Found = true;
                            }
                            //Ending Sequencial Search if we have already found members for the provided date 
                            else if (Found == true)
                                NoFurtherResults = true;
                        }
                    }
                    //Exiting from Sequencial search if next record is not of same date
                    if (NoFurtherResults)
                        break;
                }
                //closing file
                CloseFile();
            }
            catch (Exception ex)
            {
                // Display an error message.
                MessageBox.Show(ex.Message);
            }

            return Found;
        }
        //---------------------------------------- FILE METHODS END HERE----------------------------------------------


        //Method to assign Discount based on Month provided 
        //Returns discount value
        private decimal GetDiscount(int Months)
        {
            Decimal Discount = 0;

            if (Months > MONTH1         && Months <= MONTH2)
                Discount = DISCOUNT1;
            else if (Months >= MONTH3   && Months <= MONTH4)
                Discount = DISCOUNT2;
            else if (Months >= MONTH5   && Months <= MONTH6)
                Discount = DISCOUNT3;
            else if (Months >= MONTH7   && Months <= MONTH8)
                Discount = DISCOUNT4;
            else if (Months >= MONTH9   && Months <= MONTH10)
                Discount = DISCOUNT5;
            else if (Months >= MONTH11  && Months <= MONTH12)
                Discount = DISCOUNT6;
            else if (Months > MONTH12)
                Discount = DISCOUNT7;

            return Discount;
        }

        //Method to Find the next Term Month
        //Returns Next Month
        private int NextTermMonth(int Months)
        {
            int NextMonth = 0;

            if (Months > MONTH1 && Months <= MONTH2)
                NextMonth = MONTH2 + 1;
            else if (Months >= MONTH3 && Months <= MONTH4)
                NextMonth = MONTH4 + 1;
            else if (Months >= MONTH5 && Months <= MONTH6)
                NextMonth = MONTH6 + 1;
            else if (Months >= MONTH7 && Months <= MONTH8)
                NextMonth = MONTH8 + 1;
            else if (Months >= MONTH9 && Months <= MONTH10)
                NextMonth = MONTH10 + 1;
            else if (Months >= MONTH11 && Months <= MONTH12)
                NextMonth = MONTH12 + 1;
            else if (Months > MONTH12)
                NextMonth = Months;


            return NextMonth;
        }

        //Method to Calculate Fees per Month based on Input Month 
        private decimal CalculatePerMonth(int CurrentMonth)
        {
            return (MONTHBASEPRICE - ((MONTHBASEPRICE * GetDiscount(CurrentMonth) / 100)));
        }

        //Method to Calculate Total fees based on number of months provided
        private decimal CalculateTotalFees(int InputMonths)
        {
            return (InputMonths * CalculatePerMonth(InputMonths));
        }
       
        //Validation for Full Name, Telephone Number and Email Address 
        //This method check if user has provided any input in Textbox or not
        private bool NotNullValue(string UserInput,int Index)
        {
            String DynamicMessage                   = "";
            bool ReturnParameter;
            if (UserInput == "")
            {
                switch (Index)
                {
                    case 1: DynamicMessage          = "Full Name";              break;
                    case 2: DynamicMessage          = "Telephone Number";       break;
                    case 3: DynamicMessage          = "Email Address";          break;
                }
                MessageBox.Show("Please Enter " + DynamicMessage + " into Membership Details", "Blank entry not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ReturnParameter = false;
            }
            else
                ReturnParameter = true;

            return ReturnParameter;
        }
       
        /* Event Handler for Form Load
           Hiding Membership Details Group box Controls, Summary Group Box Controls, Search Group Box Controls
           Initializing counter to 0
           Creating Database File if file doesnt exist
         */
        private void MainForm_Load(object sender, EventArgs e)
        {
            MembershipDetailsGroupBox.Visible       = false;
            SearchResultGroupBox.Visible            = false;
            PricingGroupBox.Visible                 = false;
            SummaryGroupBox.Visible                 = false;
            SearchGroupBox.Visible                  = false;
            ButtonPanel.Visible                     = false;
            PasswordPanel.Visible                   = true;
            this.Size                               = new Size(FORMWIDTH,FORMSTARTHEIGHT);
            PasswordCounter                         = 0;
            PitchLabel.Text                         = "";

            PasswordTextBox.Focus();
            CreateFile();
        }

        /*
         * Event Handler for Submit Button for Password
         * -Validate password 
         * -If failed, counter add 1 to the Counter
         * -If counter is equals to Allowed attempts, Application will close
         */
        private void PasswordSubmitButton_Click(object sender, EventArgs e)
        {
            if (PasswordTextBox.Text != STOREDPASSWORD)
            {
                //Increasing the count
                PasswordCounter += 1;
                if (PasswordCounter == PASSWORDATTEMPTS)
                {
                    MessageBox.Show("You have used all password attemps. Application will close now", "Invalid Passowrd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("\tInvalid Password. \n\n " + (PASSWORDATTEMPTS - PasswordCounter).ToString() + " attempt(s) remaining out of "+PASSWORDATTEMPTS, "Invalid Passowrd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PasswordTextBox.Focus();
                    PasswordTextBox.SelectAll();
                }
            }
            else
            {
                //If Password is correct, Checking for file and enabling Summary and Search Button
                //if no records present in the file, buttons will be disabled
                if(GetSummaryDetails(true) == 0)
                {
                    SummaryButton.Enabled           = false;
                    SearchButton.Enabled            = false;
                }

                //Hiding Panels and Displaying Price
                PasswordPanel.Visible               = false;
                PricingGroupBox.Visible             = true;
                ButtonPanel.Visible                 = true;
                TermTextBox.Focus();        
            }
        }


        /*
         * Event handler for Membership ID List Box -Click Event
         * Clicking on membership ID in the list box will display details of selected member
         */

        private void MembershipIDListBox_Click(object sender, EventArgs e)
        {
            MembershipIDLabel.Text                  = "";
            if (MembershipIdExists(MembershipIDListBox.SelectedItem.ToString(), true, false))
            {
                MembershipDetailsGroupBox.Visible   = true;
                ConfirmButton.Visible               = false;
                FullNameTextBox.Enabled             = false;
                TelephoneTextBox.Enabled            = false;
                EmailTextBox.Enabled                = false;
            }
        }

        /*
         * Event Handler for DateID List Box -Click Event
         * Clicking on Membership ID in the list box will display all the detailed infomation about Member
         */
        private void DateIDListBox_Click(object sender, EventArgs e)
        {
            if(MembershipIdExists(DateIDListBox.SelectedItem.ToString(),false,true))
                SearchMemberDetailsPanel.Visible    = true;

        }

        /*
         * Event Handler for Client Confirmed Text Box- Leave event
         * If user try to change term without pressing 'Proceed' Button
         */
        private void ClientConfirmedTextBox_Leave(object sender, EventArgs e)
        {
            if (ProceedButton.Enabled == false && ConfirmedTerm.ToString() != ClientConfirmedTextBox.Text)
            {
                MessageBox.Show("Confirmed Term changed from "+ ConfirmedTerm.ToString() + " to "+ ClientConfirmedTextBox.Text+ "\n Press 'Proceed' to confirm this changes","Term Changed",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                ProceedButton.Enabled               = true;
            }
        }

        /*
         * Event Handler for SearchDateTimePicker - Leave event
         * If date is selected, 
         * previous Search Detailed are cleared out 
         */
        private void SearchDateTimePicker_Leave(object sender, EventArgs e)
        {
            if(SearchMemberDetailsPanel.Visible == true)
            {
                DateIDListBox.Items.Clear();

                IDSearchLabel.Visible               = 
                DateIDListBox.Visible               = 
                SearchMemberDetailsPanel.Visible    = false;
                SearchMemIDLabel.Text               =
                SearchJoinDateLabel.Text            =
                SearchFullNameLabel.Text            =
                SearchTelephoneLabel.Text           =
                SearchEmailLabel.Text               =
                SearchIDTextBox.Text                =
                SearchMemFeesLabel.Text             = "";
            }
        }

        /*
         * Event handler for Search ID Text Box- leave event
         * Validating Input Memebership ID in Search Box
         */
        private void SearchIDTextBox_Leave(object sender, EventArgs e)
        {
            if(SearchIDTextBox.Text != "")
            {
                try
                {
                    int MembershipID                = int.Parse(SearchIDTextBox.Text);
                }
                catch
                {
                    MessageBox.Show("Please enter Number only in Membership ID Search Box");
                    SearchIDTextBox.Focus();
                    SearchIDTextBox.Text            = "";
                }
            }
        }

        /*
         * Event Handler for Find Button
         * It will perform Search based on Selected RadioButton
         * If radiobutton is not selected, it will not show Find Button
         */
        private void FindButton_Click(object sender, EventArgs e)
        {
            DateIDListBox.Items.Clear();
            SearchResultGroupBox.Visible            = false;
            if (IDRadioButton.Checked == true)
            {
                if (SearchIDTextBox.Text == "")
                {
                    MessageBox.Show("Please provide Membership ID to Search","No memebrship ID provided",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                    //Check if Member Exist
                    if (MembershipIdExists((SearchIDTextBox.Text), false, true))
                    {
                        DateIDListBox.Visible               = false;
                        SearchResultGroupBox.Visible        = true;
                        SearchMemberDetailsPanel.Visible    = true;
                        SearchDatePanel.Visible             = false;
                    }
                    else
                    {
                        MessageBox.Show("No Member exist with ID : " + SearchIDTextBox.Text + ". \nTry entering different ID.", "No Member Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        SearchIDTextBox.Focus();
                        SearchIDTextBox.SelectAll();
                    }
                }
            }
            else
            {
                String InputDate = SearchDateTimePicker.Value.ToShortDateString();
                //Check and Collected members with same Joining date as input
                if (SearchByDate(InputDate))
                {
                    DateIDListBox.Visible                   = true;
                    SearchResultGroupBox.Visible            = true;
                    SearchMemberDetailsPanel.Visible        = false;
                    int DurationMilliseconds = 2500;
                    //Displaying Tooltip for Information
                    toolTip1.Show(toolTip1.GetToolTip(DateIDListBox), DateIDListBox, DurationMilliseconds);
                }
                else
                {
                    MessageBox.Show("No Member Joined on this date. Try selecting different date.", "No Members Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SearchDateTimePicker.Focus();
                }
            }
        }

        /*
         * Event Handler for Display Button
         * It will Calculate Fees based on User provided Term input
         * Also It will validate Term provided
         */
        private void DisplayButton_Click(object sender, EventArgs e)
        {
            decimal PerMonthPrice, TotalPrice;
            int TermInput;
            if(TermTextBox.Text == "")
            {
                MessageBox.Show("Term can not be blank. Please enter input in months", "Blank Entry Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TermTextBox.Focus();
                TermTextBox.SelectAll();
            }
            else
            {
                try
                {
                    TermInput = int.Parse(TermTextBox.Text);
                    if (TermInput < 0)
                    {
                        MessageBox.Show("Please Enter Positive Number", "Negative Number not Allowed in Terms", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TermTextBox.Focus();
                        TermTextBox.SelectAll();
                    }
                    else if (TermInput == 0)
                    {
                        MessageBox.Show("Term can not be Zero. Please enter valid input", "Zero Terms not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TermTextBox.Focus();
                        TermTextBox.SelectAll();
                    }
                    else
                    {
                        //Fetching Per Month price
                        PerMonthPrice = CalculatePerMonth(TermInput);
                        //Fetching Total Fees
                        TotalPrice = CalculateTotalFees(TermInput);

                        //Assigning Details of fees
                        PricePerMonthLabel.Text = "€ " + PerMonthPrice.ToString("N2");
                        PriceFullTermLabel.Text = "€ " + TotalPrice.ToString("N2");
                        EnquiredPrice = TotalPrice;
                        
                        //Fetching Next Term based on current Term
                        NextMonth = NextTermMonth(TermInput);
                        //If Next month is more than limit
                        if(TermInput > MONTH12)
                        {
                            NextTermLabel.Text = NEXTTERMTEXT;
                            PriceNextTermLabel.Text = "N.A.";
                            PitchLabel.Text = "Kudos! You are about to avail maximum Discount of 66.66%! Confirm Soon!!";
                        }
                        else
                        {
                            TotalPrice = CalculateTotalFees(NextMonth);
                            SuggestedPrice = TotalPrice;
                            NextTermLabel.Text = NEXTTERMTEXT + " for " + NextMonth.ToString() + " Month(s)";
                            PriceNextTermLabel.Text = "€ " + TotalPrice.ToString("N2");
                            if(GetDiscount(NextMonth) == DISCOUNT7)
                                PitchLabel.Text = "Pay €" + (SuggestedPrice).ToString() + " to get  " + (NextMonth - TermInput).ToString() + " Month(s) at whooping " + GetDiscount(NextMonth).ToString() + "% Discount";
                            else
                                PitchLabel.Text = "Pay additional €" + (SuggestedPrice - EnquiredPrice).ToString() + " to get extra " + (NextMonth - TermInput).ToString() + " Month(s) at whooping " + GetDiscount(NextMonth).ToString() + "% Discount";
                        }



                        ClientConfirmedTextBox.Focus();
                    }
                }
                catch
                {
                    MessageBox.Show("Invalid input provided for Terms. Please provide number", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TermTextBox.Focus();
                    TermTextBox.SelectAll();
                }
            }
            
        }

        /*
         * Event Handler for IDRadio Button - CheckedChanged Event
         */
        private void IDRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SearchIDPanel.Visible                           = true;
            SearchDatePanel.Visible                         = false;
            DateIDListBox.Items.Clear();
            DateIDListBox.Visible                           = false;
            SearchMemberDetailsPanel.Visible                = false;
            IDSearchLabel.Visible                           = false;
            SearchResultGroupBox.Visible                    = false;
            if (FindButton.Visible != true)
                FindButton.Visible                          = true;

        }

        /*
         * Event Handler for Date Radio Button - CheckedChanged Event
         * Clearing Listbox
         */
        private void DateRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SearchIDPanel.Visible                           = false;
            SearchDatePanel.Visible                         = true;
            DateIDListBox.Items.Clear();
            DateIDListBox.Visible                           = false;
            SearchMemberDetailsPanel.Visible                = false;
            IDSearchLabel.Visible                           = false;
            SearchDateTimePicker.Visible                    = true;
            if (FindButton.Visible != true)
                FindButton.Visible                          = true;
        }

        /*
         * Event handler for Proceed Button
         * -Generating Membership ID
         * -Enabling user input fields such as Email and Full Name
         * -Calculating Total Fees for provided input in Client Confirmed Textbox
         */
        private void ProceedButton_Click(object sender, EventArgs e)
        {
            Random RandomNumberGenerator = new Random();
            if(ClientConfirmedTextBox.Text == "")
            {
                MessageBox.Show("Term can not be Blank. Please enter input in months", "Blank Entry not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClientConfirmedTextBox.Focus();
                ClientConfirmedTextBox.SelectAll();
            }
            else
            {
                try
                {
                    ConfirmedTerm = int.Parse(ClientConfirmedTextBox.Text);
                    if (ConfirmedTerm < 0)
                    {
                        MessageBox.Show("Please Enter Positive Number", "Negative Number not Allowed in Terms", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ClientConfirmedTextBox.Focus();
                        ClientConfirmedTextBox.SelectAll();
                    }
                    else if (ConfirmedTerm == 0)
                    {
                        MessageBox.Show("Term can not be Zero. Please enter valid input", "Zero Terms not allowed in Terms", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ClientConfirmedTextBox.Focus();
                        ClientConfirmedTextBox.SelectAll();
                    }
                    else
                    {
                        SearchButton.Enabled    = false;
                        SummaryButton.Enabled   = false;
                        ProceedButton.Enabled   = false;

                        //Checking if user has provided details from mentioned results
                        if (ConfirmedTerm.ToString() == TermTextBox.Text)
                            ConfirmedTermTotal  = EnquiredPrice;
                        else if (ConfirmedTerm == NextMonth)
                            ConfirmedTermTotal  = SuggestedPrice;
                        else
                            //Calculating Final Term Total based on user input
                            ConfirmedTermTotal  = CalculateTotalFees(ConfirmedTerm);


                        int MembershipNumber    = RandomNumberGenerator.Next(1, 999999);

                        //Randomly generating Membership ID until we get unique 
                        //-Comparing it with file to check if generated ID exists in file
                        while (MembershipIdExists(MembershipNumber.ToString("D6"), false, false))
                        {
                            MembershipNumber    = RandomNumberGenerator.Next(1, 999999);
                        }

                        MembershipIDLabel.Text  = MembershipNumber.ToString("D6");
                        JoinDayLabel.Text       = DateTime.Now.ToShortDateString();

                        MembershipDetailsGroupBox.Visible = true;
                        FullNameTextBox.Focus();
                    }
                }
                catch
                {
                    MessageBox.Show("Invalid input provided for Terms. Please provide number", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TermTextBox.Focus();
                    TermTextBox.SelectAll();
                }
            }  
        }

        //Event Handler for TelephoneTextBox - dynamically restricting digits to 10
        //Adding spaces after first 3 digits, 6 digits
        private void TelephoneTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)8)
            {
                if (TelephoneTextBox.Text.Length == 0 && e.KeyChar.ToString() != "0")
                    TelephoneTextBox.Text = "0" + TelephoneTextBox.Text;

                if (TelephoneTextBox.Text.Length == 3 | TelephoneTextBox.Text.Length == 7)
                    TelephoneTextBox.Text = TelephoneTextBox.Text + " ";

                if (TelephoneTextBox.Text.Length == 12)
                    e.Handled = true;
            }
            TelephoneTextBox.SelectionStart = TelephoneTextBox.Text.Length;
        }

        /*
         * Event Handler for Confirm Button
         * -Validating FUll Name, Telephone Number, Email Address
         * -
         */
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            StreamWriter FileWriter;
            string DynamicMessage;
            
            if (ProceedButton.Enabled == true)
                MessageBox.Show("Please Confirm the Term by pressing Proceed button before confirming the membership");
            else
            {
                if (NotNullValue(FullNameTextBox.Text, 1))
                {
                    //Full Name Regular Express Validation
                    //One Word is mandatory which includes Capital and small Letters
                    if (!Regex.IsMatch(FullNameTextBox.Text, @"^[A-Z a-z]*([A-Z a-z]*)+$"))
                    {
                        MessageBox.Show("Please enter valid name", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        FullNameTextBox.Focus();
                        FullNameTextBox.SelectAll();
                    }
                    else
                    {
                        if (NotNullValue(TelephoneTextBox.Text, 2))
                        {
                            //Telephone Number Reguar Expression Validation
                            //Grouping of 3 Number followed by space(not mandatory), then Next 3 numbers followed by space and then last 4 digits 
                            if (!Regex.IsMatch(TelephoneTextBox.Text, @"^(\(?\d{3}\)*\s?\d{3}\s?\d{4})$"))
                            {
                                MessageBox.Show("Please enter valid telephone Number", "Invalid Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                TelephoneTextBox.Focus();
                                TelephoneTextBox.SelectAll();
                            }
                            else
                            {
                                if (NotNullValue(EmailTextBox.Text, 3))
                                {
                                    //Email Validation by Regular Expression
                                    bool CorrectEmail = Regex.IsMatch(EmailTextBox.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

                                    if (!CorrectEmail)
                                    {
                                        MessageBox.Show("Please enter valid email address", "Invalid Email Address", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        EmailTextBox.Focus();
                                        EmailTextBox.SelectAll();
                                    }
                                    else
                                    {
                                        //If all the detais are correct, providing popup
                                        DynamicMessage = "Provided Details are as below:";
                                        DynamicMessage = DynamicMessage + "\n\nMembership Details :";
                                        DynamicMessage = DynamicMessage + "\nMembership ID      : "         + MembershipIDLabel.Text;
                                        DynamicMessage = DynamicMessage + "\nConfirmed Term     : "         + ConfirmedTerm.ToString();
                                        DynamicMessage = DynamicMessage + "\nMembership Fees  : "           + ConfirmedTermTotal.ToString();
                                        DynamicMessage = DynamicMessage + "\n\nPersonal Details :";
                                        DynamicMessage = DynamicMessage + "\nFull Name              : "     + FullNameTextBox.Text;
                                        DynamicMessage = DynamicMessage + "\nTelephone No.      : "         + TelephoneTextBox.Text;
                                        DynamicMessage = DynamicMessage + "\nEmail ID                 : "   + EmailTextBox.Text;
                                        DynamicMessage = DynamicMessage + "\n\nDo you want to cofirm the Membership Details?";

                                        //Yes No Popup  Display decision
                                        DialogResult DR = MessageBox.Show(DynamicMessage, "Do you want to Continue?", MessageBoxButtons.YesNo);
                                        if (DR == DialogResult.Yes)
                                        {
                                            try
                                            {
                                                //Saving details of membership to the Database File
                                                FileWriter = File.AppendText(DatabaseFileName);
                                                FileWriter.WriteLine(MembershipIDLabel.Text);
                                                FileWriter.WriteLine(JoinDayLabel.Text);
                                                FileWriter.WriteLine(FullNameTextBox.Text);
                                                FileWriter.WriteLine(TelephoneTextBox.Text);
                                                FileWriter.WriteLine(EmailTextBox.Text);
                                                FileWriter.WriteLine(ConfirmedTerm.ToString());
                                                FileWriter.WriteLine(ConfirmedTermTotal.ToString("N2"));
                                                FileWriter.Close();
                                                //closing File Writer

                                                if (SummaryButton.Enabled == false)
                                                    SummaryButton.Enabled = true;
                                                if (SearchButton.Enabled == false)
                                                    SearchButton.Enabled = true;

                                                //Confirmation Message
                                                DynamicMessage = "Hello, " + FullNameTextBox.Text + "!. Welcome Aboard. :) \n Your Membership ID is : " + MembershipIDLabel.Text;
                                                MessageBox.Show(DynamicMessage, "Membership Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                MembershipDetailsGroupBox.Visible = false;
                                                //Clearing Fields
                                                TermTextBox.Text                = 
                                                ClientConfirmedTextBox.Text     = 
                                                PricePerMonthLabel.Text         =
                                                PriceFullTermLabel.Text         = 
                                                PriceNextTermLabel.Text         = 
                                                FullNameTextBox.Text            =
                                                TelephoneTextBox.Text           = 
                                                EmailTextBox.Text               = "";
                                                //Enabling Buttons 
                                                SearchButton.Enabled            = 
                                                SummaryButton.Enabled           = 
                                                TermTextBox.Enabled             =
                                                ClientConfirmedTextBox.Enabled  = true;
                                            }
                                            catch (Exception ex)
                                            {
                                                // Display an error message.
                                                MessageBox.Show(ex.Message);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }   
        }

        /*
         * Event Handler for Clear Button
         * -Clearing out all input fields and Restoring the form to original position
         */
        private void ClearButton_Click(object sender, EventArgs e)
        {           
            if ((SummaryGroupBox.Visible) || (SearchGroupBox.Visible))
            {
                for (int i = 620; i > 380; i -= INCREMENT)
                {
                    this.Size = new Size(FORMWIDTH, i);
                    this.Update();
                    System.Threading.Thread.Sleep(1);
                }
            }

            MembershipDetailsGroupBox.Visible   = 
            SummaryGroupBox.Visible             = 
            SearchGroupBox.Visible              = false;

            FullNameTextBox.Enabled             =
            TelephoneTextBox.Enabled            =
            EmailTextBox.Enabled                =
            DisplayButton.Enabled               =
            ProceedButton.Enabled               =
            SummaryButton.Enabled               =
            SearchButton.Enabled                =
            TermTextBox.Enabled                 =
            ClientConfirmedTextBox.Enabled      = true;
            NextTermLabel.Text                  = NEXTTERMTEXT;

            PitchLabel.Text                     =
            TermTextBox.Text                    = 
            ClientConfirmedTextBox.Text         =
            PricePerMonthLabel.Text             =
            PriceFullTermLabel.Text             =
            PriceNextTermLabel.Text             = "";
            EnquiredPrice = SuggestedPrice      = 0;
        }

        /*
         * Event Handler for Summary Button
         * -calling GetSummaryDetails method to find out parameters
         */
        private void SummaryButton_Click(object sender, EventArgs e)
        {
            MembershipDetailsGroupBox.Visible   = false;
            DisplayButton.Enabled               = false;
            ProceedButton.Enabled               = false;
            TermTextBox.Enabled                 = false;
            ClientConfirmedTextBox.Enabled      = false;
            MembershipIDListBox.SelectedIndex = -1;

            //Getting total number of records from file
            int TotalRecords = GetSummaryDetails(false);

            if (TotalRecords == 0)
            {
                MessageBox.Show("No Data to Summarize. Try adding Members first");
            }
            else
            {
                //Calculating Summary Details
                TotalMembersLabel.Text          = TotalRecords.ToString();
                TotalMemFeesLabel.Text          = "€ " + TotalMembershipFees.ToString("N2");
                AvgTermLabel.Text               = (TotalMembershipTerms / TotalRecords).ToString();
                AvgFeeLabel.Text                = "€ " + (TotalMembershipFees / TotalRecords).ToString("N2");

                SearchGroupBox.Visible          = false;
                SummaryGroupBox.Visible         = true;

                for (int i = FORMSTARTHEIGHT; i < FORMEXPANDHEIGHT; i += INCREMENT)
                {
                    this.Size = new Size(FORMWIDTH, i);
                    this.Update();
                    System.Threading.Thread.Sleep(1);
                }
                int DurationMilliseconds        = 2500;
                toolTip1.Show(toolTip1.GetToolTip(MembershipIDListBox), MembershipIDListBox, DurationMilliseconds);
            }                       
        }       
        
        /*
         * Event Handler for Search Button
         * -Just Displaying Radio Buttons 
         * -Hiding all other controls
         */
        private void SearchButton_Click(object sender, EventArgs e)
        {
            DateIDListBox.Items.Clear();
            SearchIDTextBox.Text                = "";

            DisplayButton.Enabled               = false;
            ProceedButton.Enabled               = false;
            TermTextBox.Enabled                 = false;
            ClientConfirmedTextBox.Enabled      = false;

            SummaryGroupBox.Visible             = false;
            SearchGroupBox.Visible              = true;
            MembershipDetailsGroupBox.Visible   = false;
            SearchResultGroupBox.Visible        = false;

            IDRadioButton.Checked               = false;
            DateRadioButton.Checked             = false;
            SearchIDPanel.Visible               = false;
            SearchDateTimePicker.Visible        = false;
            FindButton.Visible = false;

            for (int i = FORMSTARTHEIGHT; i < FORMSEARCHEXPANDHEIGHT; i += INCREMENT)
            {
                this.Size = new Size(FORMWIDTH, i);
                this.Update();
                System.Threading.Thread.Sleep(1);
            }           
        }

        //Event handler for Exit button
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }   
    }
}
