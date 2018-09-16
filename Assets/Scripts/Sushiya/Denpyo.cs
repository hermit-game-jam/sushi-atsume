using System.Linq;
using System.Collections.Generic;
using Masters;

namespace Sushiya
{
    public class Denpyo
    {
        private readonly Dictionary<int, int> sushiCountPairs = Master.Instance.SushiMaster.Values.ToDictionary(x => x.Code, _ => 0);
        
        public void Add(int sushiCode)
        {
            sushiCountPairs[sushiCode]++;
        }

        public int Sum()
        {
            return sushiCountPairs
                .Select(x => Master.Instance.SushiMaster.Values[x.Key].Price.Value * x.Value)
                .Sum();
        }
    }
}
