using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp_COP_4365
{
    // Class representing a bearish engulfing pattern recognizer
    internal class Recognizer_Bearish_Engulfing : Recognizer
    {
        // Constructor initializing pattern name and length for bearish engulfing pattern
        public Recognizer_Bearish_Engulfing() : base("Bearish Engulfing", 2)
        {
        }

        // Method to recognize the bearish engulfing pattern for a specific candlestick
        public override bool Recognize(List<SmartCandlestick> scsList, int index)
        {
            // Get the candlestick at the specified index
            SmartCandlestick scs = scsList[index];

            // Check if the pattern has already been recognized for this candlestick
            if (scs.Dictionary_Pattern.TryGetValue(Pattern_Name, out bool value))
            {
                // Return the existing recognition result
                return value;
            }
            else
            {
                // Calculate offset to find the previous candlestick
                int offset = Pattern_Length / 2;

                // Check if index is within bounds
                if (index < offset)
                {
                    // If index is out of bounds, add false to dictionary and return false
                    scs.Dictionary_Pattern.Add(Pattern_Name, false);
                    return false;
                }
                else
                {
                    // Get the previous candlestick
                    SmartCandlestick prev = scsList[index - offset];

                    // Check conditions for bearish engulfing pattern
                    bool bearish = (prev.open < prev.close) & (scs.close < scs.open);
                    bool engulfing = (scs.topPrice > prev.topPrice) & (scs.bottomPrice < prev.bottomPrice);
                    bool bearish_engulfing = bearish & engulfing;

                    // Add the recognition result to the dictionary for future reference
                    scs.Dictionary_Pattern.Add(Pattern_Name, bearish_engulfing);

                    // Return the calculated result
                    return bearish_engulfing;
                }
            }
        }
    }
}
