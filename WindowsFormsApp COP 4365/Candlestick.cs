using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp_COP_4365
{
    internal class Candlestick
    {
        //Initialize candlestick data variables using getter and setter object initializers
        public decimal open { get; set; }
        public decimal high { get; set; }
        public decimal low { get; set; }
        public decimal close { get; set; }
        public ulong volume { get; set; }
        public DateTime date { get; set; }

        //Default Constructor
        public Candlestick()
        {
        }

        //String Line Constructor
        public Candlestick(string csvLine)
        {
            //Initializes delimiters and splits data into array based on them
            char[] seperators = new char[] { ',', ' ', '"' };
            string[] subs = csvLine.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

            //Get date string to send to DateTime.Parse
            string dateString = subs[0];
            //Parse data
            date = DateTime.Parse(dateString);

            //Create temporary variable for each entry then parse data value and store on successful parse
            decimal temp;
            bool success = decimal.TryParse(subs[1], out temp);
            if (success) open = temp;

            success = decimal.TryParse(subs[2], out temp);
            if (success) high = temp;

            success = decimal.TryParse(subs[3], out temp);
            if (success) low = temp;

            success = decimal.TryParse(subs[4], out temp);
            if (success) close = temp;

            ulong tempVolume;
            success = ulong.TryParse(subs[6], out tempVolume);
            if (success) volume = tempVolume;
        }
    }
}
