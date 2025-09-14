using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp_COP_4365
{
    // Class representing a bullish pattern recognizer
    internal class Recognizer_Bullish : Recognizer
    {
        // Constructor initializing pattern name and length for bullish pattern
        public Recognizer_Bullish() : base("Bullish", 1)
        {
        }

        // Method to recognize the bullish pattern for a specific candlestick
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
                // Calculate if the candlestick is bullish
                bool bullish = scs.close > scs.open;

                // Add the recognition result to the dictionary for future reference
                scs.Dictionary_Pattern.Add(Pattern_Name, bullish);

                // Return the calculated result
                return bullish;
            }
        }
    }
}
