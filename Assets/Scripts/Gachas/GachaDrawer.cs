using Sushiya;
using UniRx;

namespace Gachas
{
    public class GachaDrawer
    {
        public static int DrawCost = 5;

        public GachaDrawer(DishHolder dishHolder)
        {
            dishHolder.AddDishObservable
                .Where(_ => dishHolder.CurrentDishCount == DrawCost)
                .Subscribe(_ =>
                {
                    dishHolder.Remove(DrawCost);
                    // ガチャ引く
                });
        }
    }
}
