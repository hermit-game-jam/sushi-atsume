using Sushiya;
using UniRx;

namespace Gachas
{
    public class GachaDrawer
    {
        public GachaDrawer(DishHolder dishHolder)
        {
            dishHolder.AddDishObservable
                .Subscribe(_ =>
                {
                    // ガチャ引く
                });
        }
    }
}
