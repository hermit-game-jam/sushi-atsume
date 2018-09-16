using System;
using Masters;
using UniRx;
using UnityEngine;

namespace Sushi
{
    public class SushiFactory : MonoBehaviour
    {
        [SerializeField] float timeSpanSeconds;
        [SerializeField] SushiHolder sushiHolder;

        void Start()
        {
            Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(timeSpanSeconds))
                .Select(_ => RandomSushiMaster())
                .Subscribe(Create)
                .AddTo(this);
        }

        void Create(SushiMaster master)
        {
            var sushiCore = master.SushiCreate(transform.position, Quaternion.identity, transform);
            sushiCore.Initialize(master, sushiHolder);
        }

        SushiMaster RandomSushiMaster()
        {
            // TODO: Slice unlocked Sushi
            return Master.Instance.SushiMaster.Values.Random();
        }
    }
}
