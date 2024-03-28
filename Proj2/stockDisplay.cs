using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using CsvHelper.Configuration;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections;
using System.Reflection;
using static Proj2.stockDisplay;

namespace Proj2
{
    public partial class stockDisplay : Form
    {
        List<string> day = new List<string>(); /// Initializes the list day to check the comboBox
        List<string> week = new List<string>(); /// Initializes the list day to check the comboBox
        List<string> month = new List<string>(); /// Initializes the list day to check the comboBox
        public SqlConnection connection; /// Public variable to hold a connection to a SQL Server database
        public SqlCommand command; /// Public variable to hold a SQL command to execute against a SQL Server database
        public SqlDataAdapter adapter; /// Public variable to hold a data adapter to fill a dataset and update a SQL Server database
        public static stockDisplay instance; /// Public static variable to hold an instance of the stockDisplay class
        string fix; /// A string variable used for fixing formatting issues
        public static double maxval; /// Public static double to hold the maximum value for a stock being displayed
        public static double minval; /// Public static double to hold the minimum value for a stock being displayed
        public static DataSet ds; /// Public static DataSet to hold data retrieved from a SQL Server database
        public static List<aCandlestick> values = null; /// Public static list that stores values of candlesticks
        public static List<aCandlestick> checker = null; /// Public static list that stores values of candlesticks
        public static List<multiCandlesticks> checker2 = null; /// Public static list that stores values of multicandlesticks
        public stockDisplay()
        {
            InitializeComponent();
            instance = this; /// Sets the current instance of the class to 'this'
            DirectoryInfo stockdata = new DirectoryInfo(@"Stock Data\"); /// Creates a new instance of the DirectoryInfo class, pointing to the 'Stock Data' directory
            FileInfo[] stocks = stockdata.GetFiles("*.csv"); /// Gets an array of FileInfo objects of all the files ending with '.csv' in the 'Stock Data' directory
            /// Loops through each file in the 'stocks' array and check if it's a daily, weekly, or monthly stock data file
            foreach (FileInfo stock in stocks)
            {
                if (stock.Name.Contains("-Day"))
                {
                    /// Removes the '-Day.csv' part of the filename and adds the resulting string to the 'day' List
                    var replaced = stock.Name.Replace("-Day.csv", "");
                    day.Add(replaced);
                }
                else if (stock.Name.Contains("-Week"))
                {
                    /// Removes the '-Week.csv' part of the filename and adds the resulting string to the 'week' 
                    var replaced = stock.Name.Replace("-Week.csv", "");
                    week.Add(replaced);
                }
                else if (stock.Name.Contains("-Month"))
                {
                    /// Removes the '-Month.csv' part of the filename and adds the resulting string to the 'month' 
                    var replaced = stock.Name.Replace("-Month.csv", "");
                    month.Add(replaced);
                }
                else
                {
                    /// If the file does not have a suffix of '-Day.csv', '-Week.csv', or '-Month.csv', skip it
                    continue;
                }
            }
            /// Loops through each stock in the 'day' List and add it to the 'stockTicker' combobox as this is the default format
            foreach (var i in day)
            {
                stockTicker.Items.Add(i.ToString());
            }
            stockTicker.Text = day[0]; /// Set the text of the 'stockTicker' combobox to the first stock in the 'day' List
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            string location = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName; /// This line retrieves the current working directory and navigates to its parent directory twice to get the solution directory
            connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + location + "\\Database1.mdf;Integrated Security=True"); /// Sets the SQL connection with the Database1
            string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + location + "\\Database1.mdf;Integrated Security=True"; /// This is so it can initialize it again to clear it 
            string clearQuery = "EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL' " + "EXEC sp_MSForEachTable 'DELETE FROM ?' " + "EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'"; /// This hard clears the data
            /// The using functions are set to reset the data entirely so fresh data can be inputted
            using (SqlConnection connection = new SqlConnection(conn))
            {
                using (SqlCommand command = new SqlCommand(clearQuery, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            string period; /// type of range
            var ticker = stockTicker.Text; /// Sets the ticker to any stock symbol is available
            /// the if statements try to figure out which dates the user wants and assigns that to period
            if (dailyButton.Checked == true)
            {
                period = "Day";
            }
            else if (weeklyButton.Checked == true)
            {
                period = "Week";
            }
            else if (monthlyButton.Checked == true)
            {
                period = "Month";
            }
            else
            {
                period = "Day";
            }
            /// Gets the stock name by combining the ticker symbol and the period
            string stockname = ticker + '-' + period;

            /// Constructs the file path to the CSV file that contains the stock data
            var filename = "Stock Data\\" + stockname + ".csv";

            /// Gets the start and end dates from the date pickers
            DateTime startdate = startDatePicker.Value;
            DateTime enddate = endDatePicker.Value;

            /// Sets the minimum and maximum dates to the extreme values initially
            DateTime minDate = DateTime.MaxValue;
            DateTime maxDate = DateTime.MinValue;

            /// Creates lists to store the data from the CSV file
            List<String> dates = new List<String>();
            List<String> datesfix = new List<String>();
            List<decimal> open = new List<decimal>();
            List<decimal> high = new List<decimal>();
            List<decimal> low = new List<decimal>();
            List<decimal> close = new List<decimal>();
            List<decimal> volume = new List<decimal>();

            /// Creates variables for iterating through the data and validating the dates
            DateTime dateList = new DateTime();
            DateTime start;
            DateTime end;
            bool validDates = false;

            /// Create lists to store the fixed and reverted dates
            var datesreverted = new List<String>();
            var datesfixed = new List<DateTime>();

            /// Creates a variable to hold a formatted date string
            string formatteddate;

            /// Opens the CSV file for reading
            using (var csvreader = new StreamReader(filename))
            {
                /// Creates a CSV reader to parse the data
                using (var indexing = new CsvReader(csvreader, CultureInfo.InvariantCulture))
                {
                    /// Reads the data into a list of sorted objects
                    values = indexing.GetRecords<aCandlestick>().ToList();
                    /// Loops through each row in the data
                    foreach (var value in values)
                    {
                        /// Converts the date string to a DateTime object
                        DateTime date = DateTime.Parse(value.Date);
                        /// Formats the date as a string in the desired format
                        formatteddate = date.ToString("M/d/yyyy");

                        // Convert the formatted date string to a DateTime object
                        dateList = DateTime.ParseExact(formatteddate, "M/d/yyyy", CultureInfo.InvariantCulture); /// parses dates and converts it to that format
                        datesfix.Add(formatteddate); /// Adds the formatted dates to the datesfix list
                        var dateString = value.GetType().GetProperty("Date").GetValue(value, null).ToString(); /// Parses the dates column
                        dates.Add(dateString); /// Adds the value of each date found in values to the dates list
                        var openString = value.GetType().GetProperty("Open").GetValue(value, null).ToString(); /// Parses the open column
                        if (decimal.TryParse(openString, out decimal data)) /// Add the extracted data to the list
                        {
                            open.Add(data); /// Adds the value of each open found in values to the open list
                        }
                        var highString = value.GetType().GetProperty("High").GetValue(value, null).ToString(); /// Parses the high column
                        if (decimal.TryParse(highString, out decimal data1)) /// Add the extracted data to the list
                        {
                            high.Add(data1); /// Adds the value of each high found in values to the high list
                        }
                        var lowString = value.GetType().GetProperty("Low").GetValue(value, null).ToString(); /// Parses the low column
                        if (decimal.TryParse(lowString, out decimal data2)) /// Add the extracted data to the list
                        {
                            low.Add(data2); /// Adds the value of each low found in values to the low list
                        }
                        var closeString = value.GetType().GetProperty("Close").GetValue(value, null).ToString(); /// Parses the close column
                        if (decimal.TryParse(closeString, out decimal data3)) /// Add the extracted data to the list
                        {
                            close.Add(data3); /// Adds the value of each close found in values to the close list
                        }
                        var volumeString = value.GetType().GetProperty("Volume").GetValue(value, null).ToString(); /// Parses the volume column
                        if (decimal.TryParse(volumeString, out decimal data4)) /// Add the extracted data to the list
                        {
                            volume.Add(data4); /// Adds the value of each volume found in values to the volume list
                        }
                        /// Finds the min and max date
                        if (dateList < minDate)
                        {
                            minDate = dateList;
                        }
                        if (dateList > maxDate)
                        {
                            maxDate = dateList;
                        }
                    }
                }
            }
            while (!validDates)
            {
                /// Gets the start and end date strings from the respective DateTimePicker controls
                string startDate = startDatePicker.Value.ToShortDateString();
                string endDate = endDatePicker.Value.ToShortDateString();
                /// Parses the date strings into DateTime objects
                start = DateTime.ParseExact(startDate, "M/d/yyyy", CultureInfo.InvariantCulture);
                end = DateTime.ParseExact(endDate, "M/d/yyyy", CultureInfo.InvariantCulture);
                /// Checks if the dates are valid and within the range of available data
                if (startdate < minDate || enddate > maxDate || start > end)
                {
                    /// Displays an error message if the dates are not valid and exit the method
                    MessageBox.Show($"Date must be between {minDate.ToShortDateString()} and {maxDate.ToShortDateString()}. Also make sure you did not make the start date after the end date or vice versa the end date before the start date");
                    return;
                }
                else
                {
                    /// If the dates are valid, exit the while loop
                    validDates = true;
                }
            }
            /// Converts the values list into a dictionary with keys as DateTime objects
            var sortedDict = values.ToDictionary(x => DateTime.Parse(x.Date), x => x);
            /// Creates a list of dates between the start and end dates
            var datesList = Enumerable.Range(0, (int)(enddate - startdate).TotalDays + 1).Select(x => startdate.AddDays(x)).ToList();
            /// Loops through each date in the dates list and check if it exists in the dictionary
            foreach (var daters in datesList)
            {
                if (sortedDict.ContainsKey(daters))
                {
                    /// If the date exists in the dictionary, add it to the fixed dates list
                    datesfixed.Add(daters);
                }
                else
                {
                    /// If the date does not exist in the dictionary, find the closest date and add it to the fixed dates list
                    var closestDate = sortedDict.Keys.LastOrDefault(x => x <= daters);
                    if (closestDate == default)
                    {
                        /// If there is no closest date, add the current date to the fixed dates list
                        datesfixed.Add(daters);
                    }
                }
            }
            /// Convert the fixed dates list to a list of date strings
            for (int i = 0; i < datesfixed.Count; i++)
            {
                fix = datesfixed[i].ToShortDateString();
                datesreverted.Add(fix);
            }
            string query = "INSERT INTO [StockTable] (Date, [Open], High, Low, [Close], Volume) VALUES (@date, @open, @high, @low, @close, @volume)"; /// Initializes a query to establish an insertion to the StockTable
            connection.Open(); /// Opens the connection to the SQL table
            List<decimal> maxlist = new List<decimal>(); /// Creates a list to store the maximum stock values
            List<decimal> minlist = new List<decimal>(); /// Creates a list to store the minimum stock values
            List<decimal> open1 = new List<decimal>(); /// Creates a list to store the stock open values
            List<decimal> high1 = new List<decimal>(); /// Creates a list to store the stock high values
            List<decimal> low1 = new List<decimal>(); /// Creates a list to store the stock low values
            List<decimal> close1 = new List<decimal>(); /// Creates a list to store the stock close values
            List<decimal> volume1 = new List<decimal>(); /// Creates a list to store the stock volume values
            int starting = 0; /// Sets the starting index of the data to zero
            if(datesfixed.Count == 0)
            {
                MessageBox.Show("Please input a date range with data available in it"); /// Display an error message if there is no data for the selected date range
                return; /// Returns to make you do it again
            }
            for (int j = 0; j < datesfix.Count; j++) /// Loops through the dates to find the index where the data starts
            {
                if (datesreverted[0].Equals(datesfix[j]))
                {

                    starting = j;
                }
            }
            int track = starting; /// Sets the tracking index to the starting index
            while (track < dates.Count) /// Loops through the data to retrieve the necessary values for the selected date range
            {
                open1.Add(open[track]); /// Adds the stock open value for the current index to the open1 list
                high1.Add(high[track]); /// Adds the stock high value for the current index to the high1 list
                low1.Add(low[track]); /// Adds the stock low value for the current index to the low1 list
                close1.Add(close[track]); /// Adds the stock close value for the current index to the close1 list
                volume1.Add(volume[track]); /// Adds the stock volume value for the current index to the volume1 list
                track++; /// Moves to the next index
            }
            checker = new List<aCandlestick>();
            checker2 = new List<multiCandlesticks>();
            for (int i = 0; i < datesreverted.Count; i++) /// Loops through the data to insert it into the database
            {
                command = new SqlCommand(query, connection); /// Creates a SQL command object
                command.Parameters.AddWithValue("@date", datesreverted[i]); /// Adds the date parameter to the command object
                command.Parameters.AddWithValue("@open", open1[i]); /// Adds the open parameter to the command object
                command.Parameters.AddWithValue("@high", high1[i]); /// Adds the high parameter to the command object
                command.Parameters.AddWithValue("@low", low1[i]); /// Adds the low parameter to the command object
                command.Parameters.AddWithValue("@close", close1[i]); /// Adds the close parameter to the command object
                command.Parameters.AddWithValue("@volume", volume1[i]); /// Adds the volume parameter to the command object
                /// try and catch for adding different values into those to check for pattenrs
                try
                {
                    aCandlestick cs = new aCandlestick { Date = datesreverted[i], Open = open1[i], High = high1[i], Low = low1[i], Close = close1[i] };
                    checker.Add(cs);
                    multiCandlesticks ms = new multiCandlesticks { candlesticks = checker, index = i};
                    checker2.Add(ms);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error adding aCandlestick object to checker: " + ex.Message); /// Handle the exception here, e.g. log it or display an error message.
                }
                maxlist.Add(high1[i]); /// Adds the high value to the maxlist
                minlist.Add(low1[i]); /// Adds the low value to the minlist
                command.ExecuteNonQuery(); /// Executes the command
            }
            maxval = decimal.ToDouble(maxlist.Max()); /// Gets the maximum value from the maxlist
            minval = decimal.ToDouble(minlist.Min()); /// Gets the minimum value from the minlist
            command = new SqlCommand("SELECT * FROM [StockTable]", connection); /// create a SQL command to select all data from the StockTable
            adapter = new SqlDataAdapter(command); /// Creates a SqlDataAdapter object using the command
            ds = new DataSet(); /// Creates a new DataSet object
            adapter.Fill(ds); /// Fills the DataSet with
            int o = 0; /// Allows the rows to add at each index
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                row["Date"] = datesreverted[o]; /// Ensures the dates are correct in each row
                row["Open"] = open1[o]; /// Ensures the opens are correct in each row
                row["High"] = high1[o]; /// Ensures the highs are correct in each row 
                row["Low"] = low1[o]; /// Ensures the lows are correct in each row 
                row["Close"] = close1[o]; /// Ensures the closes are correct in each row
                row["Volume"] = volume1[o]; /// Ensures the volumes are correct in each row
                o++; /// Goes to next index
            }
            connection.Close(); /// Closes the connection with the SQL table
            /// Loop through the list of dates in reverse order
            chartDisplay chart = new chartDisplay(); /// Creates a chartDisplay form to initialize the chart
            chart.ShowDialog(); /// Initiliazes the chart form
        }



        /// Defines an event handler for the Form1_Load event of the form
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dailyButton_CheckedChanged(object sender, EventArgs e)
        {
            stockTicker.Items.Clear(); /// Clears the items in the stockTicker ComboBox control

            /// Loops through the items in the day List<string> and add them to the stockTicker control
            for (int i = 0; i < day.Count; i++)
            {
                stockTicker.Items.Add(day[i]);
            }

            /// If there are no items in the day List, show a message box with a warning message
            if (day.Count == 0)
            {
                MessageBox.Show("There is no data present in this period");
            }
            /// Otherwise, set the text of the stockTicker control to the first item in the day List
            else
            {
                stockTicker.Text = day[0];
            }
        }

        private void weeklyButton_CheckedChanged(object sender, EventArgs e)
        {
            stockTicker.Items.Clear(); /// Clears the items in the stockTicker ComboBox control
            for (int i = 0; i < week.Count; i++) /// Loop through the items in the week List<string> and add them to the stockTicker control
            {
                stockTicker.Items.Add(week[i]);
            }
            if (week.Count == 0) /// If there are no items in the week List, show a message box with a warning message
            {
                MessageBox.Show("There is no data present in this period");
            } 
            else /// Otherwise, set the text of the stockTicker control to the first item in the week List
            {
                stockTicker.Text = week[0];
            }
        }

        private void monthlyButton_CheckedChanged(object sender, EventArgs e)
        {
            stockTicker.Items.Clear(); /// Clear the items in the stockTicker ComboBox control
            for (int i = 0; i < month.Count; i++) /// Loop through the items in the month List<string> and add them to the stockTicker control
            {
                stockTicker.Items.Add(month[i]);
            } 
            if (month.Count == 0) /// If there are no items in the month List and the monthlyButton is checked, show a message box with a warning message
            {
                if (monthlyButton.Checked)
                {
                    MessageBox.Show("There is no data present in this period");
                }
            } 
            else /// Otherwise, set the text of the stockTicker control to the first item in the month List
            {
                stockTicker.Text = month[0];
            }
        }
    }
    }

