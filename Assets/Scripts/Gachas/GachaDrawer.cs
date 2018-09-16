using System;
using Sushiya;
using UniRx;

namespace Gachas
{
    public class GachaDrawer : IDisposable
    {
        private static readonly int DrawCost = 5;

        private readonly CompositeDisposable disposables = new CompositeDisposable();
        
        public GachaDrawer(DishHolder dishHolder)
        {
            dishHolder.AddDishObservable
                .Where(_ => dishHolder.CurrentDishCount == DrawCost)
                .Subscribe(_ =>
                {
                    dishHolder.Remove(DrawCost);
                    // ガチャ引く
                })
                .AddTo(disposables);
        }

        public void Dispose()
        {
            disposables.Dispose();
        }
    }
}
