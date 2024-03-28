using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Schema;

namespace Proj2
{
    public partial class chartDisplay : Form
    {
        List<DateTime> dates = new List<DateTime>();   /// Creates an empty List of DateTime objects named dates
        public chartDisplay()
        {
            InitializeComponent();
        }
        private void chartDisplay_Load(object sender, EventArgs e)
        {
            initializeRecognizers();
            stockChart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0;   /// Hides the major grid lines on the x-axis
            stockChart.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0;   /// Hides the major grid lines on the y-axis
            stockChart.Series["Stock"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;   /// Sets the value type for the x-axis of the "Stock" series to Date
            stockChart.Series["Stock"].CustomProperties = "PriceDownColor=Red,PriceUpColor=Green";   /// Sets custom properties for the "Stock" series, specifying the colors of price bars
            stockChart.Series["Stock"]["OpenCloseStyle"] = "Triangle";   /// Sets the style of the open and close markers for the "Stock" series to a triangle
            stockChart.Series["Stock"]["ShowOpenClose"] = "Both";   /// Specifies that both the open and close markers should be shown on the price bars of the "Stock" series
            stockChart.DataManipulator.IsStartFromFirst = true;   /// Specifies that when the chart is being data-bound, the data should start from the first data point in the data source
            stockChart.Series["Stock"].XValueMember = "Date";   /// Specifies that the x-values of the "Stock" series should come from the "Date" column of the data source
            stockChart.Series["Stock"].YValueMembers = "High,Low,Open,Close";   /// Specifies that the y-values of the "Stock" series should come from the "High", "Low", "Open", and "Close" columns of the data source
            DataTable dt = stockDisplay.ds.Tables[0];   /// Retrieves the first table in the data set stockDisplay.ds and assigns it to a DataTable variable named dt, to be used as the data source for the "Stock" series on the chart
            int index = 0;   /// Initializes an index variable to 0
            dates = new List<DateTime>();
            foreach (DataRow row in dt.Rows)   /// Iterates through each row in the DataTable dt
            {
                DateTime date = DateTime.Parse(row["Date"].ToString());   /// Retrieves the date value from the "Date" column of the current row and converts it to a DateTime object
                double high = double.Parse(row["High"].ToString());   /// Retrieves the high value from the "High" column of the current row and converts it to a double
                double low = double.Parse(row["Low"].ToString());   /// Retrieves the low value from the "Low" column of the current row and converts it to a double
                double open = double.Parse(row["Open"].ToString());   /// Retrieves the open value from the "Open" column of the current row and converts it to a double
                double close = double.Parse(row["Close"].ToString());   /// Retrieves the close value from the "Close" column of the current row and converts it to a double
                stockChart.Series["Stock"].Points.AddXY(index + 1, high, low, open, close);   /// Adds a data point to the "Stock" series on the chart, with x-value equal to index + 1 and y-values equal to the high, low, open, and close values retrieved from the current row
                dates.Add(date);   /// Adds the current date to the dates List
                index++;   /// Increments the index variable by 1 for the next iteration of the loop
            }
            index = 0; /// Reset index variable to zero
            foreach (DateTime date in dates) /// Iterate through each date in the 'dates' list
            {
                /// If a point with the same date exists, create a custom label for the X axis at the current index position
                if (stockChart.Series["Stock"].Points.Any(p => DateTime.FromOADate(p.XValue).Date == date.Date)) 
                {
                    CustomLabel label = new CustomLabel();
                    label.FromPosition = index - 0.5;
                    label.ToPosition = index + 0.5;
                    label.Text = date.ToShortDateString();
                    stockChart.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(label);
                }
                index++; /// Increment index variable
            }

            stockChart.DataSource = dt; /// Sets the stockCharts data to be imported from the dt DataTable
            stockChart.ChartAreas["ChartArea1"].AxisY.Maximum = stockDisplay.maxval; /// Sets the maximum Y the graph shows to the maximum value in the data
            stockChart.ChartAreas["ChartArea1"].AxisY.Minimum = stockDisplay.minval; /// Sets the minimum Y the graph shows to the minimum value in the data
            stockChart.ChartAreas["ChartArea1"].AxisX.IntervalType = DateTimeIntervalType.Days; /// Axis will display the dates with a daily interval
            stockChart.ChartAreas["ChartArea1"].AxisX.IntervalType = DateTimeIntervalType.Auto; /// This will just set it in case it isn't a daily interval and will automatically determine it
            stockChart.Visible = true; /// Displays the stockChart for the user to see
        }
        List<aRecognizer> recognizers = new List<aRecognizer>();
        private List<aRecognizer> initializeRecognizers()
        {
            /// initialzes the combobox with different patterns that can be recognized in the candlesticks
            aRecognizer recognizer = null;
            recognizer = new Recognizer_Neutral();
            recognizers.Add(recognizer);
            recognizer = new Recognizer_Dragonfly();
            recognizers.Add(recognizer);
            recognizer = new Recognizer_Gravestone();
            recognizers.Add(recognizer);
            recognizer = new Recognizer_LongLegged();
            recognizers.Add(recognizer);
            recognizer = new Recognizer_SpinningTop();
            recognizers.Add(recognizer);
            recognizer = new Recognizer_WhiteMarubozu();
            recognizers.Add(recognizer);
            recognizer = new Recognizer_BlackMarubozu();
            recognizers.Add(recognizer);
            recognizer = new Recognizer_BullishHammer();
            recognizers.Add(recognizer);
            recognizer = new Recognizer_BearishHammer();
            recognizers.Add(recognizer);
            recognizer = new Recognizer_BullishInvertedHammer();
            recognizers.Add(recognizer);
            recognizer = new Recognizer_BearishInvertedHammer();
            recognizers.Add(recognizer);
            recognizer = new Recognizer_BullishEngulfing();
            recognizers.Add(recognizer);
            recognizer = new Recognizer_BearishEngulfing();
            recognizers.Add(recognizer);
            recognizer = new Recognizer_BullishHarami();
            recognizers.Add(recognizer);
            recognizer = new Recognizer_BearishHarami();
            recognizers.Add(recognizer);
            recognizer = new Recognizer_MorningStar();
            recognizers.Add(recognizer);
            recognizer = new Recognizer_EveningStar();
            recognizers.Add(recognizer);

            patternBox.Items.Clear(); /// if the code is rerun it clears it so it cannot add additional of the same patterns
            /// for each of the recognizers it adds them to the combobox
            foreach(aRecognizer r in recognizers)
            {
                patternBox.Items.Add(r.patternName);
            }
            /// now the patternbox is able to be used
            patternBox.Enabled = true;
            return recognizers;
        }

        private void DrawRectangle(Chart chart, int seriesIndex, int dataPointIndex, Color color)
        {
            Series series = chart.Series[seriesIndex]; /// The series is set to the Stock series in the chart
                                                       /// the if statement declares that if the datapointindex is less than 0 or greater than or equal to the number of data points in the series the method returns
            if (dataPointIndex < 0 || dataPointIndex >= chart.Series[seriesIndex].Points.Count)
            {
                return;
            }
            DataPoint point = series.Points[dataPointIndex]; /// Sets the datapointindex to a index of a date found
            Console.WriteLine(dataPointIndex);
            /// Gets the high, low, open, and close values of the data point.
            double high = point.YValues[0];
            double low = point.YValues[1];
            double open = point.YValues[2];
            double close = point.YValues[3];

            /// Gets the chart area where the series is displayed.
            ChartArea chartArea = chart.ChartAreas[series.ChartArea];

            /// Creates a new rectangle annotation with the appropriate properties.
            RectangleAnnotation rectangle = new RectangleAnnotation();
            rectangle.AxisX = chartArea.AxisX;
            rectangle.AxisY = chartArea.AxisY;
            rectangle.BackColor = Color.Transparent;
            rectangle.ForeColor = color;
            rectangle.LineColor = color;
            rectangle.LineWidth = 2;

            /// Calculates the X, Y, width, and height values of the rectangle.
            double calc = 0.3;
            double x = point.XValue - calc;
            double y = Math.Max(open, close);
            double width = 2 * calc;
            double height = Math.Abs(open - close);
            rectangle.X = x;
            rectangle.Y = y;

            /// If the height is smaller than 15% of the maximum or minimum value, adjust it accordingly.
            if (height <= stockDisplay.maxval * 0.15 && height <= stockDisplay.minval * 0.15)
            {
                rectangle.Height = height + ((stockDisplay.maxval - stockDisplay.minval) * 0.15);
            }
            else
            {
                rectangle.Height = height;
            }
            rectangle.Width = width; /// Sets rectangle width to the width we set
            rectangle.IsSizeAlwaysRelative = false; /// It will not automatically be adjusted when the size of the parent object changes
            rectangle.AllowSelecting = true; /// ALlows the user to select the rectangle

            /// Sets the tooltip text of the rectangle to include the series name and date of the data point.
            rectangle.ToolTip = series.Name + " - " + DateTime.FromOADate(point.XValue).ToShortDateString();

            /// Adds the rectangle annotation to the chart annotations collection.
            chart.Annotations.Add(rectangle);
        }

        private void patternBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            /// initializes all the values to annotate patterns
            aRecognizer recognizer = recognizers[patternBox.SelectedIndex];
            DataPoint a = new DataPoint();
            List<int> matchingi = recognizer.GetMatchingIndices(stockDisplay.checker);
            List<int> matchingj = recognizer.GetMatchingIndex(stockDisplay.checker2);
            if (matchingi != null)
            {
                Console.WriteLine(matchingi.Count);
                /// loops through and finds all of the patterns recognized for each recognizer
                foreach (int i in matchingi)
                {
                    Console.WriteLine($"Pattern {recognizer.patternName} found at index {stockDisplay.checker[i].Date}");
                    Console.WriteLine(stockDisplay.checker[i].Date);
                    if (DateTime.TryParseExact(stockDisplay.checker[i].Date, "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dl)
                        || DateTime.TryParseExact(stockDisplay.checker[i].Date, "MM/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dl)
                        || DateTime.TryParseExact(stockDisplay.checker[i].Date, "M/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dl)
                        || DateTime.TryParseExact(stockDisplay.checker[i].Date, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dl))
                    {
                        int dataPointIndex = dates.FindIndex(date => date.Date == dl);
                        Console.WriteLine(dataPointIndex);
                        if (dataPointIndex >= 0)
                        {
                            /// Annotates the chart
                            updateChart();
                            DrawRectangle(stockChart, 0, dataPointIndex, Color.Violet);
                        }
                        
                        else
                        {
                            Console.WriteLine($"Data point not found for date {stockDisplay.checker[i].Date}"); /// if no data points are found
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Unable to parse date {stockDisplay.checker[i].Date}"); /// if the date is unable to be parsed
                    }

                }
                
            }
            if (matchingj != null)
            {
                Console.WriteLine(matchingj.Count);
                /// loops through and finds all of the patterns recognized for each recognizer
                foreach (int j in matchingj)
                {
                    Console.WriteLine($"Pattern {recognizer.patternName} found at index {stockDisplay.checker2[j].candlesticks[j].Date}");
                    if (DateTime.TryParseExact(stockDisplay.checker2[j].candlesticks[j].Date, "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dl)
                        || DateTime.TryParseExact(stockDisplay.checker2[j].candlesticks[j].Date, "MM/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dl)
                        || DateTime.TryParseExact(stockDisplay.checker2[j].candlesticks[j].Date, "M/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dl)
                        || DateTime.TryParseExact(stockDisplay.checker2[j].candlesticks[j].Date, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dl))
                    {
                        int dataPointIndex = dates.FindIndex(date => date.Date == dl);
                        Console.WriteLine(dataPointIndex);
                        if (dataPointIndex >= 0)
                        {
                            /// Annotates the chart
                            updateChart();
                            DrawRectangle(stockChart, 0, dataPointIndex, Color.Violet);
                        }

                        else
                        {
                            Console.WriteLine($"Data point not found for date {stockDisplay.checker2[j].candlesticks[j].Date}"); /// if no data points are found
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Unable to parse date {stockDisplay.checker2[j].candlesticks[j].Date}"); /// if the date is unable to be parsed
                    }
                }
            }
        }
        private void updateChart()
        {
            stockChart.Series["Stock"].Points.Clear(); /// reupdates the chart
            stockChart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0;   /// Hides the major grid lines on the x-axis
            stockChart.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0;   /// Hides the major grid lines on the y-axis
            stockChart.Series["Stock"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;   /// Sets the value type for the x-axis of the "Stock" series to Date
            stockChart.Series["Stock"].CustomProperties = "PriceDownColor=Red,PriceUpColor=Green";   /// Sets custom properties for the "Stock" series, specifying the colors of price bars
            stockChart.Series["Stock"]["OpenCloseStyle"] = "Triangle";   /// Sets the style of the open and close markers for the "Stock" series to a triangle
            stockChart.Series["Stock"]["ShowOpenClose"] = "Both";   /// Specifies that both the open and close markers should be shown on the price bars of the "Stock" series
            stockChart.DataManipulator.IsStartFromFirst = true;   /// Specifies that when the chart is being data-bound, the data should start from the first data point in the data source
            stockChart.Series["Stock"].XValueMember = "Date";   /// Specifies that the x-values of the "Stock" series should come from the "Date" column of the data source
            stockChart.Series["Stock"].YValueMembers = "High,Low,Open,Close";   /// Specifies that the y-values of the "Stock" series should come from the "High", "Low", "Open", and "Close" columns of the data source
            DataTable dt = stockDisplay.ds.Tables[0];   /// Retrieves the first table in the data set stockDisplay.ds and assigns it to a DataTable variable named dt, to be used as the data source for the "Stock" series on the chart
            int index = 0;   /// Initializes an index variable to 0
            dates = new List<DateTime>();
            foreach (DataRow row in dt.Rows)   /// Iterates through each row in the DataTable dt
            {
                DateTime date = DateTime.Parse(row["Date"].ToString());   /// Retrieves the date value from the "Date" column of the current row and converts it to a DateTime object
                double high = double.Parse(row["High"].ToString());   /// Retrieves the high value from the "High" column of the current row and converts it to a double
                double low = double.Parse(row["Low"].ToString());   /// Retrieves the low value from the "Low" column of the current row and converts it to a double
                double open = double.Parse(row["Open"].ToString());   /// Retrieves the open value from the "Open" column of the current row and converts it to a double
                double close = double.Parse(row["Close"].ToString());   /// Retrieves the close value from the "Close" column of the current row and converts it to a double
                stockChart.Series["Stock"].Points.AddXY(index + 1, high, low, open, close);   /// Adds a data point to the "Stock" series on the chart, with x-value equal to index + 1 and y-values equal to the high, low, open, and close values retrieved from the current row
                dates.Add(date);   /// Adds the current date to the dates List
                index++;   /// Increments the index variable by 1 for the next iteration of the loop
            }
            index = 0; /// Reset index variable to zero
            foreach (DateTime date in dates) /// Iterate through each date in the 'dates' list
            {
                /// If a point with the same date exists, create a custom label for the X axis at the current index position
                if (stockChart.Series["Stock"].Points.Any(p => DateTime.FromOADate(p.XValue).Date == date.Date))
                {
                    CustomLabel label = new CustomLabel();
                    label.FromPosition = index - 0.5;
                    label.ToPosition = index + 0.5;
                    label.Text = date.ToShortDateString();
                    stockChart.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(label);
                }
                index++; /// Increment index variable
            }

            stockChart.DataSource = dt; /// Sets the stockCharts data to be imported from the dt DataTable
            stockChart.ChartAreas["ChartArea1"].AxisY.Maximum = stockDisplay.maxval; /// Sets the maximum Y the graph shows to the maximum value in the data
            stockChart.ChartAreas["ChartArea1"].AxisY.Minimum = stockDisplay.minval; /// Sets the minimum Y the graph shows to the minimum value in the data
            stockChart.ChartAreas["ChartArea1"].AxisX.IntervalType = DateTimeIntervalType.Days; /// Axis will display the dates with a daily interval
            stockChart.ChartAreas["ChartArea1"].AxisX.IntervalType = DateTimeIntervalType.Auto; /// This will just set it in case it isn't a daily interval and will automatically determine it
        }

        private void clearAnnotations_Click(object sender, EventArgs e)
        {
            stockChart.Annotations.Clear(); /// Clears the annotations present
        }
    }
}
