using System;
using System.Collections.Generic;
using Sushi;
using UniRx;

namespace Sushiya
{
    public class DishHolder
    {
        private readonly Queue<int> dishQueue = new Queue<int>();

        private readonly ISubject<int> addDishSubject = new Subject<int>();
        private readonly ISubject<IEnumerable<int>> removeDishSubject = new Subject<IEnumerable<int>>();

        /// <summary>
        /// Addが呼ばれた時に呼ばれる
        /// </summary>
        public IObservable<int> AddDishObservable => addDishSubject;
        public IObservable<IEnumerable<int>> RemoveDishObservable => removeDishSubject;

        /// <summary>
        /// 現在の皿の枚数
        /// </summary>
        public int CurrentDishCount => dishQueue.Count;

        /// <param name="sushiCode"></param>
        public void Add(int sushiCode)
        {
            dishQueue.Enqueue(sushiCode);
            addDishSubject.OnNext(sushiCode);
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

            removeDishSubject.OnNext(result);
            return result;
        }
    }
}
