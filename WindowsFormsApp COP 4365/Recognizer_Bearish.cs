using System.Collections.Generic;

namespace WindowsFormsApp_COP_4365
{
    // Class representing a bearish pattern recognizer
    internal class Recognizer_Bearish : Recognizer
    {
        // Constructor initializing pattern name and length for bearish pattern
        public Recognizer_Bearish() : base("Bearish", 1)
        {
        }

        // Method to recognize the bearish pattern for a specific candlestick
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
                // Calculate bearishness (open price greater than close price)
                bool bearish = scs.open > scs.close;

                // Add the recognition result to the dictionary for future reference
                scs.Dictionary_Pattern.Add(Pattern_Name, bearish);

                // Return the calculated result
                return bearish;
            }
        }
    }
}
