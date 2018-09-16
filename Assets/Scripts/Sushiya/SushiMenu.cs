using System;
using System.Collections.Generic;
using System.Linq;
using Masters;
using UniRx;

namespace Sushiya
{
    public class SushiMenu
    {
        readonly Denpyo denpyo;

        HashSet<int> unlockedSushiCodes = new HashSet<int>(Master.Instance.SushiMaster.Values
            .Where(x => x.IsUnlocked)
            .Select(x => x.Code));

        public IEnumerable<int> UnlockedSushiCodes => unlockedSushiCodes;
        public IEnumerable<int> AteSushiCodes => unlockedSushiCodes.Where(x => denpyo.Contains(x));

        readonly ISubject<int> unlocked = new Subject<int>();
        public IObservable<int> Unlocked => unlocked;

        public SushiMenu(Denpyo denpyo)
        {
            this.denpyo = denpyo;
        }

        public void Unlock(int sushiCode)
        {
            if (unlockedSushiCodes.Contains(sushiCode)) { return; }

            unlockedSushiCodes.Add(sushiCode);
            unlocked.OnNext(sushiCode);
        }
    }
}
