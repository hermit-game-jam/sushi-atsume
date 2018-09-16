using System;
using System.Collections.Generic;
using UniRx;

namespace Sushiya
{
    public class DishHolder
    {
        private readonly Queue<int> dishQueue = new Queue<int>();

        private ISubject<Unit> addDishSubject = new Subject<Unit>();
        
        /// <summary>
        /// Addが呼ばれた時に呼ばれる
        /// </summary>
        public IObservable<Unit> AddDishObservable => addDishSubject;
        
        /// <summary>
        /// 現在の皿の枚数
        /// </summary>
        public int CurrentDishCount => dishQueue.Count;

        /// <param name="sushiCode"></param>
        public void Add(int sushiCode)
        {
            dishQueue.Enqueue(sushiCode);
            addDishSubject.OnNext(Unit.Default);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="num">減らす個数</param>
        /// <returns>減った時の寿司コード</returns>
        public IEnumerable<int> Remove(int num)
        {
            var result = new List<int>();
            
            for (var i = 0; i < num; i++)
            {
                result.Add(dishQueue.Dequeue());
            }

            return result;
        }
    }
}
