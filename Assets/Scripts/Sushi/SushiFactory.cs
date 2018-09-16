using System;
using UniRx;
using UnityEngine;

namespace Sushi
{
    public class SushiFactory : MonoBehaviour
    {
        // TODO: Use SushiMaster
        [SerializeField] SushiCore prefab;
        [SerializeField] float timeSpanSeconds;

        void Start()
        {
            Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(timeSpanSeconds))
                .Subscribe(_ => Create())
                .AddTo(this);
        }

        void Create()
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
