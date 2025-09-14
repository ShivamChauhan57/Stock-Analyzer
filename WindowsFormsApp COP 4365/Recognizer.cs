using System.Collections.Generic;

namespace WindowsFormsApp_COP_4365
{
    // Abstract class representing a pattern recognizer
    internal abstract class Recognizer
    {
        // Name of the pattern
        public string Pattern_Name;

        // Length of the pattern
        public int Pattern_Length;

        // Constructor initializing pattern name and length
        protected Recognizer(string pN, int pL)
        {
            Pattern_Name = pN;
            Pattern_Length = pL;
        }

        // Abstract method to recognize a pattern
        public abstract bool Recognize(List<SmartCandlestick> scsList, int index);

        // Method to recognize all patterns in a list of candlesticks
        public void Recognize_All(List<SmartCandlestick> scsList)
        {
            // Loop through each candlestick in the list
            for (int i = 0; i < scsList.Count; i++)
            {
                // Call the Recognize method for the current candlestick
                Recognize(scsList, i);
            }
        }
    }
}
