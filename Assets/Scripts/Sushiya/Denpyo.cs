using System;
using System.Linq;
using System.Collections.Generic;
using Masters;
using UniRx;

namespace Sushiya
{
    public class Denpyo
    {
        private readonly Dictionary<int, int> sushiCountPairs = Master.Instance.SushiMaster.Values.ToDictionary(x => x.Code, _ => 0);

        private readonly ISubject<int> addPriceEvent = new Subject<int>();
        /// <summary>
        /// Addが呼ばれた時に呼ばれる
        /// </summary>
        public IObservable<int> AddPriceEvent => addPriceEvent;

        public void Add(int sushiCode)
        {
            sushiCountPairs[sushiCode]++;
            addPriceEvent.OnNext(sushiCode);
        }

        public int Sum()
        {
            return sushiCountPairs
                .Select(x => Master.Instance.SushiMaster.Values[x.Key].Price.Value * x.Value)
                .Sum();
        }

        public bool Contains(int sushiCode)
        {
            return sushiCountPairs[sushiCode] != 0;
        }
    }
}
