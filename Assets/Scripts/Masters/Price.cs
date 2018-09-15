using System;
using System.Collections.Generic;
using System.Linq;

namespace Masters
{
    [Serializable]
    public struct Price
    {
        public int Value;

        public Price(int value)
        {
            Value = value;
        }
        
        public static Price operator +(Price price, Price arg)
        {
            return new Price(price.Value + price.Value);
        }
        
        public static Price operator *(Price price, Price arg)
        {
            return new Price(price.Value * price.Value);
        }
        
        public static Price operator +(Price price, int arg)
        {
            return new Price(price.Value + arg);
        }

        public static Price operator *(Price price, int arg)
        {
            return new Price(price.Value * arg);
        }
    }
}
