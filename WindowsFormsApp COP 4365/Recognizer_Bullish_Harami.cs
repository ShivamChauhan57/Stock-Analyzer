using System.Collections.Generic;

namespace WindowsFormsApp_COP_4365
{
    internal class Recognizer_Bullish_Harami : Recognizer
    {
        // Constructor for Bullish Harami Recognizer
        public Recognizer_Bullish_Harami() : base("Bullish Harami", 2)
        {
        }

        // Method to recognize the Bullish Harami pattern
        public override bool Recognize(List<SmartCandlestick> scsList, int index)
        {
            // Retrieve the candlestick at the given index
            SmartCandlestick scs = scsList[index];
            // Check if the pattern has already been recognized for this candlestick
            if (scs.Dictionary_Pattern.TryGetValue(Pattern_Name, out bool value))
            {
                return value; // Return the previously recognized pattern value
            }
            else
            {
                // Calculate the offset for the previous candlestick
                int offset = Pattern_Length / 2;
                // Check if the index is within bounds to calculate the pattern
                if (index < offset)
                {
                    // If out of bounds, add false to the dictionary and return false
                    scs.Dictionary_Pattern.Add(Pattern_Name, false);
                    return false;
                }
                else
                {
                    // Retrieve the previous candlestick for comparison
                    SmartCandlestick prev = scsList[index - offset];
                    // Calculate conditions for Bullish Harami pattern
                    bool bullish = (prev.open > prev.close) & (scs.close > scs.open);
                    bool harami = (scs.topPrice < prev.topPrice) & (scs.bottomPrice > prev.bottomPrice);
                    bool bullish_harami = bullish & harami;
                    // Add the recognized pattern to the dictionary and return the result
                    scs.Dictionary_Pattern.Add(Pattern_Name, bullish_harami);
                    return bullish_harami;
                }
            }
        }
    }
}
