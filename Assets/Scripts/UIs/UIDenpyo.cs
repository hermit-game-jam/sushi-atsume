﻿using TMPro;
using UniRx;
using UnityEngine;

namespace UIs
{
    public class UIDenpyo : MonoBehaviour
    {
        [SerializeField] string priceFormat;
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] Animation _animation;

        void Start()
        {
            UpdateView(0, false);

            Sushiya.Sushiya.Instance.DenpyoAsObservable
                .SelectMany(x => x.AddPriceEvent, (denpyo, _) => denpyo.Sum())
                .Subscribe(sumPrice =>
                {
                    UpdateView(sumPrice, true);
                });
        }

        void UpdateView(int price, bool playAnimation)
        {
            text.text = string.Format(priceFormat, price);

            if (playAnimation)
            {
                _animation.Play();
            }
        }
    }
}
