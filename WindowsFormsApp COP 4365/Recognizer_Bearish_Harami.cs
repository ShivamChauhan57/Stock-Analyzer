using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp_COP_4365
{
    // Class representing a bearish harami pattern recognizer
    internal class Recognizer_Bearish_Harami : Recognizer
    {
        // Constructor initializing pattern name and length for bearish harami pattern
        public Recognizer_Bearish_Harami() : base("Bearish Harami", 2)
        {
        }

        // Method to recognize the bearish harami pattern for a specific candlestick
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

                    // Check conditions for bearish harami pattern
                    bool bearish = (prev.open < prev.close) & (scs.close < scs.open);
                    bool harami = (scs.topPrice < prev.topPrice) & (scs.bottomPrice > prev.bottomPrice);
                    bool bearish_harami = bearish & harami;

                    // Add the recognition result to the dictionary for future reference
                    scs.Dictionary_Pattern.Add(Pattern_Name, bearish_harami);

                    // Return the calculated result
                    return bearish_harami;
                }
            }
        }
    }
}
