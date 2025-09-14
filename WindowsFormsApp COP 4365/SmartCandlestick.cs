using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp_COP_4365
{
    internal class SmartCandlestick : Candlestick
    {
        //Initialize smart candlestick data variables using getter and setter object initializers
        public decimal range { get; set; }
        public decimal topPrice { get; set; }
        public decimal bottomPrice { get; set; }
        public decimal bodyRange { get; set; }
        public decimal upperTail { get; set; }
        public decimal lowerTail { get; set; }
        //Dictionary to store T/F values of each pattern
        public Dictionary<string, bool> Dictionary_Pattern = new Dictionary<string, bool>();

        //Inherit Constructor
        public SmartCandlestick(string csvLine) : base(csvLine)
        {
            ComputeExtraProperties();
            ComputePatternProperties();
        }

        //Conversion Constructor
        public SmartCandlestick(Candlestick cs)
        {
            date = cs.date;
            open = cs.open;
            close = cs.close;
            high = cs.high;
            low = cs.low;
            volume = cs.volume;
            ComputeExtraProperties();
            ComputePatternProperties();
        }

        //OPTIONAL: MAKE SCS CONSTRUCTOR FOR TEST

        /// <summary>
        /// Computes the extra properties of a smart candlestick and stores in member variables
        /// </summary>
        private void ComputeExtraProperties()
        {
            range = high - low;
            topPrice = Math.Max(open, close);
            bottomPrice = Math.Min(open, close);
            bodyRange = topPrice - bottomPrice;
            upperTail = high - topPrice;
            lowerTail = bottomPrice - low;
        }

        /// <summary>
        /// Computes the pattern properties of a smart candlestick and stores in member dictionary
        /// </summary>
        private void ComputePatternProperties()
        {
            //Bullish
            bool bullish = close > open;
            Dictionary_Pattern.Add("Bullish", bullish);

            //Bearish
            bool bearish = open > close;
            Dictionary_Pattern.Add("Bearish", bearish);

            //Neutral
            bool neutral = bodyRange < (range * 0.03m);
            Dictionary_Pattern.Add("Neutral", neutral);

            //Marubozu
            bool marubozu = bodyRange > (range * 0.96m);
            Dictionary_Pattern.Add("Marubozu", marubozu);

            //Hammer min and max range
            bool hammer = ((range * 0.20m) < bodyRange) & (bodyRange < (range * 0.33m)) & (lowerTail > range * 0.66m);
            Dictionary_Pattern.Add("Hammer", hammer);

            //Doji
            bool doji = bodyRange < (range * 0.03m);
            Dictionary_Pattern.Add("Doji", doji);

            //Dragonfly doji
            bool dragonfly_doji = doji & (lowerTail > range * 0.66m);
            Dictionary_Pattern.Add("Dragonfly Doji", dragonfly_doji);

            //Gravestone doji
            bool gravestone_doji = doji & (upperTail > range * 0.66m);
            Dictionary_Pattern.Add("Gravestone Doji", gravestone_doji);
        }
    }
}
