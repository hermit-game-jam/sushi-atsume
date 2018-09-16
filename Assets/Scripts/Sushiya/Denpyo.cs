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

        private ISubject<Unit> addPriceEvent = new Subject<Unit>();
        /// <summary>
        /// Addが呼ばれた時に呼ばれる
        /// </summary>
        public IObservable<Unit> AddPriceEvent => addPriceEvent;
        
        public void Add(int sushiCode)
        {
            sushiCountPairs[sushiCode]++;
            addPriceEvent.OnNext(Unit.Default);
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
