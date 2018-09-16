using System;
using Masters;
using UniRx;
using UnityEngine;

namespace Sushi
{
    public class SushiFactory : MonoBehaviour
    {
        [SerializeField] float timeSpanSeconds;

        void Start()
        {
            Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(timeSpanSeconds))
                .Select(_ => RandomSushiMaster())
                .Subscribe(Create)
                .AddTo(this);
        }

        void Create(SushiMaster master)
        {
            var sushiCore = Instantiate(master.prefab, transform.position, Quaternion.identity);
            sushiCore.Master = master;
        }

        SushiMaster RandomSushiMaster()
        {
            // TODO: Slice unlocked Sushi
            return Master.Instance.SushiMaster.Values.Random();
        }
    }
}
