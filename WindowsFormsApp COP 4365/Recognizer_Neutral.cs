using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp_COP_4365
{
    internal class Recognizer_Neutral : Recognizer
    {
        // Inherit Constructor
        public Recognizer_Neutral() : base("Neutral", 1)
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
                // Calculate the pattern
                bool neutral = scs.bodyRange < (scs.range * 0.03m);
                // Add the calculated value to the dictionary
                scs.Dictionary_Pattern.Add(Pattern_Name, neutral);
                // Return the result
                return neutral;
            }
        }
    }
}
