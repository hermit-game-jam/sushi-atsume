using System;
using UniRx;
using UnityEngine;

namespace Sushi
{
    public class SushiSlayer : MonoBehaviour
    {
        [SerializeField] float lifetimeSeconds;

        void Start()
        {
            Observable.Timer(TimeSpan.FromSeconds(lifetimeSeconds))
                .Subscribe(_ => Destroy(gameObject))
                .AddTo(this);
        }
    }
}
