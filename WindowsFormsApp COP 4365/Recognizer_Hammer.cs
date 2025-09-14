using System.Collections.Generic;

namespace WindowsFormsApp_COP_4365
{
    internal class Recognizer_Hammer : Recognizer
    {
        // Constructor inheriting from base class Recognizer
        public Recognizer_Hammer() : base("Hammer", 1)
        {
        }

        // Overridden abstract method Recognize
        public override bool Recognize(List<SmartCandlestick> scsList, int index)
        {
            // Retrieving SmartCandlestick object from the scsList based on the index
            SmartCandlestick scs = scsList[index];

            // Checking if the pattern has already been recognized for this SmartCandlestick
            if (scs.Dictionary_Pattern.TryGetValue(Pattern_Name, out bool value))
            {
                return value; // If recognized, return the stored value
            }
            else
            {
                // Calculating whether the candlestick pattern matches the Hammer pattern
                bool hammer = ((scs.range * 0.20m) < scs.bodyRange) & (scs.bodyRange < (scs.range * 0.33m)) & (scs.lowerTail > scs.range * 0.66m);

                // Storing the recognition result in the dictionary for future reference
                scs.Dictionary_Pattern.Add(Pattern_Name, hammer);

                // Returning the recognition result
                return hammer;
            }
        }
    }
}
