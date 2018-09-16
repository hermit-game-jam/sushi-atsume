using System;
using UniRx;
using UnityEngine;

namespace Sushi
{
    public class SushiSlayer : MonoBehaviour
    {
        [SerializeField] SushiCore core;
        [SerializeField] float lifetimeSeconds;

        void Start()
        {
            Observable.Timer(TimeSpan.FromSeconds(lifetimeSeconds))
                .Where(_ => core.State.AutoMovable)
                .Subscribe(_ => Destroy(gameObject))
                .AddTo(this);
        }
    }
}
