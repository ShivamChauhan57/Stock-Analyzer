using System;
using System.Collections.Generic; 
using System.ComponentModel; 
using System.Drawing;
using System.IO;
using System.Linq; 
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp_COP_4365 // Namespace declaration
{
    public partial class Form_StockViewer : Form // Declaring the Form_StockViewer class
    {
        // Declaring class variables
        private List<SmartCandlestick> candlesticks = null; // List to hold candlestick data
        private BindingList<SmartCandlestick> boundCandlesticks = null; // Binding list for data binding
        private DateTime startDate = new DateTime(2022, 1, 1); // Start date for filtering
        private DateTime endDate = DateTime.Now; // End date for filtering
        private Dictionary<string, Recognizer> Dictionary_Recognizer; // Dictionary to hold pattern recognizers
        private double chartMax; // Maximum value for chart axis
        private double chartMin; // Minimum value for chart axis

        // Constructor for the form
        public Form_StockViewer()
        {
            InitializeComponent(); // Initialize form components
            InitializeRecognizer(); // Initialize pattern recognizers

            candlesticks = new List<SmartCandlestick>(1024); // Initialize candlesticks list
            dateTimePicker_startDate.Value = startDate; // Set default start date
            dateTimePicker_endDate.Value = endDate; // Set default end date
        }

        // Constructor with parameters for stock path, start date, and end date
        public Form_StockViewer(string stockPath, DateTime start, DateTime end)
        {
            InitializeComponent(); // Initialize form components
            InitializeRecognizer(); // Initialize pattern recognizers

            dateTimePicker_startDate.Value = startDate = start; // Set start date based on parameter
            dateTimePicker_endDate.Value = endDate = end; // Set end date based on parameter

            candlesticks = goReadFile(stockPath); // Read stock data from file
            filterList(); // Filter candlesticks based on dates
            displayCandlesticks(); // Display filtered candlesticks
        }

        // Event handler for "Open File" button click
        private void button_openFile_Click(object sender, EventArgs e)
        {
            Text = "Opening File..."; // Set form title
            openFileDialog_stockPick.ShowDialog(); // Open file dialog
        }

        // Event handler for "Update" button click
        private void button_Update_Click(object sender, EventArgs e)
        {
            if ((candlesticks.Count != 0) & (startDate <= endDate))
            {
                filterList(); // Filter candlesticks based on dates
                displayCandlesticks(); // Display filtered candlesticks
            }
        }

        // Event handler for file selection in open file dialog
        private void openFileDialog_stockPick_FileOk(object sender, CancelEventArgs e)
        {
            int numberOfFiles = openFileDialog_stockPick.FileNames.Count(); // Get number of selected files
            for (int i = 0; i < numberOfFiles; ++i) // Loop through selected files
            {
                string pathName = openFileDialog_stockPick.FileNames[i]; // Get file path
                string ticker = Path.GetFileNameWithoutExtension(pathName); // Get file name without extension

                Form_StockViewer form_StockViewer; // Declare new form object
                if (i == 0) // If it's the first file
                {
                    form_StockViewer = this; // Set form to current form
                    readAndDisplayStock(); // Read and display stock data
                    form_StockViewer.Text = "Parent: " + ticker; // Set form title
                }
                else // If it's not the first file
                {
                    form_StockViewer = new Form_StockViewer(pathName, startDate, endDate); // Create new form with file path and dates
                    form_StockViewer.Text = "Child: " + ticker; // Set form title
                }

                form_StockViewer.Show(); // Show form
                form_StockViewer.BringToFront(); // Bring form to front
            }
        }

        // Read stock data from file and return list of candlesticks
        private List<SmartCandlestick> goReadFile(string filename)
        {
            this.Text = Path.GetFileName(filename); // Set form title to file name
            const string referenceString = "Date,Open,High,Low,Close,Adj Close,Volume"; // Define reference string

            List<SmartCandlestick> list = new List<SmartCandlestick>(); // Initialize list to hold candlesticks
            using (StreamReader sr = new StreamReader(filename)) // Open file for reading
            {
                string line = sr.ReadLine(); // Read first line
                if (line == referenceString) // If it matches reference string
                {
                    while ((line = sr.ReadLine()) != null) // Loop through lines
                    {
                        SmartCandlestick cs = new SmartCandlestick(line); // Create new candlestick object
                        list.Add(cs); // Add candlestick to list
                    }
                }
                else // If file format is incorrect
                { Text = "Bad File: " + Path.GetFileName(filename); } // Set form title to indicate bad file
            }

            foreach (Recognizer r in Dictionary_Recognizer.Values) // Loop through pattern recognizers
            {
                r.Recognize_All(list); // Perform pattern recognition on candlesticks
            }

            return list; // Return list of candlesticks
        }

        // Read stock data from open file dialog and display
        private void goReadFile()
        {
            candlesticks = goReadFile(openFileDialog_stockPick.FileName); // Read stock data from file
            boundCandlesticks = new BindingList<SmartCandlestick>(candlesticks); // Create binding list from candlesticks
        }

        // Filter list of candlesticks based on start and end dates
        private List<SmartCandlestick> filterList(List<SmartCandlestick> list, DateTime start, DateTime end)
        {
            List<SmartCandlestick> filter = new List<SmartCandlestick>(list.Count); // Initialize filtered list
            foreach (SmartCandlestick cs in list) // Loop through candlesticks
            {
                if ((cs.date >= start) & (cs.date <= end)) // If candlestick date is within range
                { filter.Add(cs); } // Add candlestick to filtered list
            }
            return filter; // Return filtered list
        }

        // Filter candlesticks based on start and end dates
        private void filterList()
        {
            List<SmartCandlestick> filterCandlesticks = filterList(candlesticks, startDate, endDate); // Filter candlesticks
            boundCandlesticks = new BindingList<SmartCandlestick>(filterCandlesticks); // Create binding list from filtered candlesticks
        }

        // Display candlesticks from a binding list
        private void displayCandlesticks(BindingList<SmartCandlestick> bindList)
        {
            normalizeChart(); // Normalize chart axis

            chart_OHLCV.Annotations.Clear(); // Clear existing annotations

            chart_OHLCV.DataSource = bindList; // Set data source for chart
            chart_OHLCV.DataBind(); // Bind data to chart
        }

        // Display candlesticks
        private void displayCandlesticks()
        {
            displayCandlesticks(boundCandlesticks); // Call overloaded displayCandlesticks method
        }

        // Normalize chart axis based on candlestick data
        private void normalizeChart(BindingList<SmartCandlestick> bindList)
        {
            decimal min = 1000000000, max = 0; // Initialize minimum and maximum values
            foreach (SmartCandlestick c in bindList) // Loop through candlesticks
            {
                if (c.low < min) { min = c.low; } // Update minimum value if necessary
                if (c.high > max) { max = c.high; } // Update maximum value if necessary
            }

            // Set chart axis minimum and maximum values
            chartMin = chart_OHLCV.ChartAreas["ChartArea_OHLC"].AxisY.Minimum = Math.Floor(Decimal.ToDouble(min) * 0.98);
            chartMax = chart_OHLCV.ChartAreas["ChartArea_OHLC"].AxisY.Maximum = Math.Ceiling(Decimal.ToDouble(max) * 1.02);
        }

        // Normalize chart axis
        private void normalizeChart()
        {
            normalizeChart(boundCandlesticks); // Call overloaded normalizeChart method
        }

        // Read and display stock data
        private void readAndDisplayStock()
        {
            goReadFile(); // Read stock data
            filterList(); // Filter candlesticks
            displayCandlesticks(); // Display filtered candlesticks
        }

        // Initialize pattern recognizers
        private void InitializeRecognizer()
        {
            Dictionary_Recognizer = new Dictionary<string, Recognizer>(); // Initialize pattern recognizer dictionary

            // Create instances of various pattern recognizers and add to dictionary
            Recognizer r = new Recognizer_Bullish();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            r = new Recognizer_Bearish();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            r = new Recognizer_Neutral();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            r = new Recognizer_Marubozu();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            r = new Recognizer_Hammer();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            r = new Recognizer_Doji();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            r = new Recognizer_Dragonfly_Doji();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            r = new Recognizer_Gravestone_Doji();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            r = new Recognizer_Bullish_Engulfing();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            r = new Recognizer_Bearish_Engulfing();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            r = new Recognizer_Bullish_Harami();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            r = new Recognizer_Bearish_Harami();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            r = new Recognizer_Peak();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            r = new Recognizer_Valley();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);

            comboBox_Patterns.Items.AddRange(Dictionary_Recognizer.Keys.ToArray()); // Add pattern names to combo box
        }

        // Event handler for start date value change
        private void dateTimePicker_startDate_ValueChanged(object sender, EventArgs e)
        {
            startDate = dateTimePicker_startDate.Value; // Update start date
        }

        // Event handler for end date value change
        private void dateTimePicker_endDate_ValueChanged(object sender, EventArgs e)
        {
            endDate = dateTimePicker_endDate.Value; // Update end date
        }

        // Event handler for combo box selection change
        private void comboBox_Patterns_SelectedIndexChanged(object sender, EventArgs e)
        {
            chart_OHLCV.Annotations.Clear(); // Clear existing annotations
            if (boundCandlesticks != null) // If candlesticks are available
            {
                for (int i = 0; i < boundCandlesticks.Count; i++) // Loop through candlesticks
                {
                    SmartCandlestick scs = boundCandlesticks[i]; // Get current candlestick
                    DataPoint point = chart_OHLCV.Series[0].Points[i]; // Get corresponding data point on chart

                    string selected = comboBox_Patterns.SelectedItem.ToString(); // Get selected pattern name
                    if (scs.Dictionary_Pattern[selected]) // If candlestick matches selected pattern
                    {
                        int length = Dictionary_Recognizer[selected].Pattern_Length; // Get pattern length
                        if (length > 1) // If pattern length is greater than 1
                        {
                            if (i == 0 | ((i == boundCandlesticks.Count() - 1) & length == 3)) // If it's the first or last candlestick
                            {
                                continue; // Skip processing
                            }
                            RectangleAnnotation rectangle = new RectangleAnnotation(); // Create rectangle annotation
                            rectangle.SetAnchor(point); // Set anchor point

                            double Ymax, Ymin; // Declare variables for maximum and minimum Y values
                            double width = (90.0 / boundCandlesticks.Count()) * length; // Calculate rectangle width
                            if (length == 2) // If pattern length is 2
                            {
                                Ymax = (int)(Math.Max(scs.high, boundCandlesticks[i - 1].high)); // Calculate maximum Y value
                                Ymin = (int)(Math.Min(scs.low, boundCandlesticks[i - 1].low)); // Calculate minimum Y value
                                rectangle.AnchorOffsetX = ((width / length) / 2 - 0.25) * (-1); // Adjust X offset
                            }
                            else // If pattern length is not 2
                            {
                                Ymax = (int)(Math.Max(scs.high, Math.Max(boundCandlesticks[i + 1].high, boundCandlesticks[i - 1].high))); // Calculate maximum Y value
                                Ymin = (int)(Math.Min(scs.low, Math.Min(boundCandlesticks[i + 1].low, boundCandlesticks[i - 1].low))); // Calculate minimum Y value
                            }
                            double height = 40.0 * (Ymax - Ymin) / (chartMax - chartMin); // Calculate rectangle height
                            rectangle.Height = height; rectangle.Width = width; // Set rectangle dimensions
                            rectangle.Y = Ymax; // Set rectangle Y position
                            rectangle.BackColor = Color.Transparent; // Set background color
                            rectangle.LineWidth = 2; // Set line width
                            rectangle.LineDashStyle = ChartDashStyle.Dash; // Set line dash style
                            chart_OHLCV.Annotations.Add(rectangle); // Add rectangle annotation to chart
                        }

                        ArrowAnnotation arrow = new ArrowAnnotation(); // Create arrow annotation
                        arrow.AxisX = chart_OHLCV.ChartAreas[0].AxisX; // Set X axis
                        arrow.AxisY = chart_OHLCV.ChartAreas[0].AxisY; // Set Y axis
                        arrow.Width = 0.5; // Set arrow width
                        arrow.Height = 0.5; // Set arrow height
                        arrow.SetAnchor(point); // Set anchor point
                        chart_OHLCV.Annotations.Add(arrow); // Add arrow annotation to chart
                    }
                }
            }
        }

        // Event handler for form load
        private void Form_Project_2_Load(object sender, EventArgs e)
        {

        }

        // Event handler for start date label click
        private void label_startDate_Click(object sender, EventArgs e)
        {

        }
    }
}
