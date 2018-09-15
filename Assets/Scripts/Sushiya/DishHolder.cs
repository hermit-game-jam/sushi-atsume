using System.Collections.Generic;

namespace Sushiya
{
    public class DishHolder
    {
        private readonly Queue<int> dishQueue = new Queue<int>();

        /// <param name="sushiCode"></param>
        public void Add(int sushiCode)
        {
            dishQueue.Enqueue(sushiCode);
            // TODO: 追加された時に何かのイベント発火する
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="num">減らす個数</param>
        /// <returns>減った時の寿司コード</returns>
        public IEnumerable<int> Remove(int num)
        {
            for (var i = 0; i < num; i++)
            {
                yield return dishQueue.Dequeue();
            }
        }
    }
}
