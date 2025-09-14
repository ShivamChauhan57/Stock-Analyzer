using System.Collections.Generic;

namespace WindowsFormsApp_COP_4365
{
    internal class Recognizer_Bullish_Engulfing : Recognizer
    {
        // Constructor inheriting from base class
        public Recognizer_Bullish_Engulfing() : base("Bullish Engulfing", 2)
        {
        }

        // Implementation of abstract method from base class
        public override bool Recognize(List<SmartCandlestick> scsList, int index)
        {
            // Retrieve candlestick at the specified index
            SmartCandlestick scs = scsList[index];

            // Check if the pattern recognition result is already calculated and stored
            if (scs.Dictionary_Pattern.TryGetValue(Pattern_Name, out bool value))
            {
                return value;
            }
            else
            {
                // Calculate offset for pattern recognition
                int offset = Pattern_Length / 2;

                // If the index is out of bounds, return false
                if (index < offset)
                {
                    scs.Dictionary_Pattern.Add(Pattern_Name, false);
                    return false;
                }
                else
                {
                    // Calculate bullish engulfing pattern
                    SmartCandlestick prev = scsList[index - offset];
                    bool bullishOpen = (prev.open > prev.close) & (scs.close > scs.open);
                    bool engulfing = (scs.topPrice > prev.topPrice) & (scs.bottomPrice < prev.bottomPrice);
                    bool bullish_engulfing = bullishOpen & engulfing;

                    // Store the pattern recognition result in the dictionary
                    scs.Dictionary_Pattern.Add(Pattern_Name, bullish_engulfing);
                    return bullish_engulfing;
                }
            }
        }
    }
}
