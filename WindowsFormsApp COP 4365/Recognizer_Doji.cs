using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp_COP_4365
{
    internal class Recognizer_Doji : Recognizer
    {
        // Constructor for the Doji Recognizer
        public Recognizer_Doji() : base("Doji", 1)
        {
        }

        // Method to recognize the Doji pattern
        public override bool Recognize(List<SmartCandlestick> scsList, int index)
        {
            // Retrieve the candlestick at the specified index
            SmartCandlestick scs = scsList[index];

            // Check if the Doji pattern has already been recognized for this candlestick
            if (scs.Dictionary_Pattern.TryGetValue(Pattern_Name, out bool value))
            {
                return value; // Return the previously recognized value
            }
            else
            {
                // Calculate whether the candlestick qualifies as a Doji
                bool doji = scs.bodyRange < (scs.range * 0.03m);

                // Add the recognition result to the dictionary for future reference
                scs.Dictionary_Pattern.Add(Pattern_Name, doji);

                // Return the result of the Doji recognition
                return doji;
            }
        }
    }
}
