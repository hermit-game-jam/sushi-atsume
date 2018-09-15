using System;

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
    }
}
