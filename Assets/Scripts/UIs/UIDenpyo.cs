using TMPro;
using UniRx;
using UnityEngine;

namespace UIs
{
    public class UIDenpyo : MonoBehaviour
    {
        [SerializeField] string priceFormat;
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] Animation animation;

        void Start()
        {
            UpdateView(0, false);
            
            Sushiya.Sushiya.Instance.DenpyoAsObservable
                .SelectMany(x => x.AddPriceEvent, (denpyo, unit) => denpyo.Sum())
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
                animation.Play();
            }
        }
    }
}
