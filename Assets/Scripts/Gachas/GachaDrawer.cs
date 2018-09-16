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
                .Select(_ =>
                {
                    dishHolder.Remove(DrawCost);
                    return Master.Instance.SushiMaster.Lottery();
                })
                .SelectMany(gachaResult =>　UIGacha.Instance.DirectionAsObservable(gachaResult, sushiMenu.IsNewSushi(gachaResult)).Select(_ => gachaResult))
                .Subscribe(gachaResult =>
                {
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
