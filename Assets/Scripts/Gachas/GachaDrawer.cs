using System;
using Sushiya;
using UniRx;
using Masters;

namespace Gachas
{
    public class GachaDrawer : IDisposable
    {
        private static readonly int DrawCost = 5;

        private readonly CompositeDisposable disposables = new CompositeDisposable();
        
        public GachaDrawer(DishHolder dishHolder, SushiMenu sushiMenu)
        {
            dishHolder.AddDishObservable
                .Where(_ => dishHolder.CurrentDishCount >= DrawCost)
                .Subscribe(_ =>
                {
                    dishHolder.Remove(DrawCost);

                    var gachaResult = Master.Instance.SushiMaster.Lottery();
                    sushiMenu.Unlock(gachaResult);
                })
                .AddTo(disposables);
        }

        public void Dispose()
        {
            disposables.Dispose();
        }
    }
}
