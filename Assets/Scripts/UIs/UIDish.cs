using System.Collections.Generic;
using System.Linq;
using Masters;
using UniRx;
using UnityEngine;

namespace UIs
{
    public class UIDish : MonoBehaviour
    {
        [SerializeField] float dishHeight;

        List<GameObject> stack = new List<GameObject>();

        void Start()
        {
            Sushiya.Sushiya.Instance.DishHolder.AddDishObservable
                .Select(CreateDish)
                .Subscribe(x =>
                {
                    stack.Add(x);
                    Relocation(stack);
                })
                .AddTo(this);

            Sushiya.Sushiya.Instance.DishHolder.RemoveDishObservable
                .Select(x => x.Count())
                .ObserveOn(Scheduler.MainThreadEndOfFrame)
                .Subscribe(c =>
                {
                    foreach (var g in stack.Take(c))
                    {
                        Destroy(g);
                    }

                    stack = stack.Skip(c).ToList();
                    Relocation(stack);
                })
                .AddTo(this);
        }

        GameObject CreateDish(int sushiCode)
        {
            var master = Master.Instance.SushiMaster.Values.Single(x => x.Code == sushiCode);
            return master.CreateDish(Vector3.zero, Quaternion.identity, transform);
        }

        void Relocation(IEnumerable<GameObject> stack)
        {
            float h = 0;
            foreach (var dish in stack)
            {
                dish.transform.localPosition = Vector3.up * h;
                h += dishHeight;
            }
        }
    }
}
