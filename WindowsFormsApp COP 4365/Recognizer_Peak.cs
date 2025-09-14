using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp_COP_4365
{
    internal class Recognizer_Peak : Recognizer
    {
        // Constructor to initialize the Recognizer with the pattern name "Peak" and pattern length 3
        public Recognizer_Peak() : base("Peak", 3)
        {
        }

        // Method to recognize the "Peak" pattern
        public override bool Recognize(List<SmartCandlestick> scsList, int index)
        {
            // Retrieve the SmartCandlestick at the current index
            SmartCandlestick scs = scsList[index];

            // Check if the pattern has already been recognized and stored in the dictionary
            if (scs.Dictionary_Pattern.TryGetValue(Pattern_Name, out bool value))
            {
                return value; // If yes, return the recognized value
            }
            else
            {
                // If the pattern hasn't been recognized yet, calculate it

                // Calculate the offset, which is half of the pattern length
                int offset = Pattern_Length / 2;

                // Check if the current index is within the valid range to calculate the pattern
                if ((index < offset) | (index == scsList.Count() - offset))
                {
                    // If the current index is near the beginning or end of the list, mark the pattern as false
                    scs.Dictionary_Pattern.Add(Pattern_Name, false);
                    return false;
                }
                else
                {
                    // If the current index is within the valid range

                    // Retrieve the SmartCandlestick before and after the current index
                    SmartCandlestick prev = scsList[index - offset];
                    SmartCandlestick next = scsList[index + offset];

                    // Check if the current candlestick is a peak
                    bool peak = (scs.high > prev.high) & (scs.high > next.high);

                    // Store the recognized pattern in the dictionary
                    scs.Dictionary_Pattern.Add(Pattern_Name, peak);

                    // Return the recognized pattern
                    return peak;
                }
            }
        }
    }
}
