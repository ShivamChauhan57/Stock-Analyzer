
using System.Collections.Generic;

namespace WindowsFormsApp_COP_4365
{
    internal class Recognizer_Dragonfly_Doji : Recognizer
    {
        // Inherit Constructor
        public Recognizer_Dragonfly_Doji() : base("Dragonfly Doji", 1)
        {
        }

        // Abstract Method
        public override bool Recognize(List<SmartCandlestick> scsList, int index)
        {
            // Return existing value or calculate
            SmartCandlestick scs = scsList[index];
            if (scs.Dictionary_Pattern.TryGetValue(Pattern_Name, out bool value))
            {
                return value;
            }
            else
            {
                // Calculate dragonfly and doji conditions
                bool dragonfly = scs.lowerTail > (scs.range * 0.66m);
                bool doji = scs.bodyRange < (scs.range * 0.03m);
                // Determine if it's a dragonfly doji
                bool dragonfly_doji = dragonfly & doji;
                // Add the pattern to the dictionary and return the result
                scs.Dictionary_Pattern.Add(Pattern_Name, dragonfly_doji);
                return dragonfly_doji;
            }
        }
    }
}
