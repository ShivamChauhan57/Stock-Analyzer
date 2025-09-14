using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp_COP_4365
{
    internal class Recognizer_Gravestone_Doji : Recognizer
    {
        //Inherit Constructor
        public Recognizer_Gravestone_Doji() : base("Gravestone Doji", 1)
        {
        }

        //Abstract Method
        public override bool Recognize(List<SmartCandlestick> scsList, int index)
        {
            //Return existing value or calculate
            SmartCandlestick scs = scsList[index];
            if (scs.Dictionary_Pattern.TryGetValue(Pattern_Name, out bool value))
            {
                return value;
            }
            else
            {
                bool gravestone = scs.upperTail > (scs.range * 0.66m);
                bool doji = scs.bodyRange < (scs.range * 0.03m);
                bool gravestone_doji = gravestone & doji;
                scs.Dictionary_Pattern.Add(Pattern_Name, gravestone_doji);
                return gravestone_doji;
            }
        }
    }
}
