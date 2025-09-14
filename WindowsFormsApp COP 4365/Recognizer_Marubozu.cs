using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp_COP_4365
{
    internal class Recognizer_Marubozu : Recognizer
    {
        // Constructor to initialize the Recognizer for Marubozu pattern with name "Marubozu" and pattern length 1
        public Recognizer_Marubozu() : base("Marubozu", 1)
        {
        }

        // Overridden method to recognize the Marubozu pattern
        public override bool Recognize(List<SmartCandlestick> scsList, int index)
        {
            // Retrieve the SmartCandlestick object at the specified index
            SmartCandlestick scs = scsList[index];

            // Check if the pattern has already been recognized for this candlestick
            if (scs.Dictionary_Pattern.TryGetValue(Pattern_Name, out bool value))
            {
                return value; // Return the existing recognition result
            }
            else
            {
                // Calculate whether the candlestick is a Marubozu or not
                bool marubozu = scs.bodyRange > (scs.range * 0.96m);

                // Add the recognition result to the dictionary for future reference
                scs.Dictionary_Pattern.Add(Pattern_Name, marubozu);

                // Return the recognition result
                return marubozu;
            }
        }
    }
}
