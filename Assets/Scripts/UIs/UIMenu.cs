using System;
using System.Collections.Generic;
using Masters;
using Sushi;
using UniRx;
using UnityEngine;

namespace UIs
{
    public class UIMenu : MonoBehaviour
    {
        [SerializeField] SushiHolder holder;
        [SerializeField] Quaternion sushiAngle;
        [SerializeField] MenuTexts textPrefab;
        readonly Dictionary<int, Tuple<SushiCore, MenuTexts>> dict = new Dictionary<int, Tuple<SushiCore, MenuTexts>>();

        void Start()
        {
            foreach (var master in Master.Instance.SushiMaster.Values)
            {
                var sushi = CreateSushi(master);
                if (holder.TryPut(sushi.transform))
                {
                    sushi.transform.rotation = sushiAngle;
                    var text = CreateText(master);
                    text.transform.SetParent(sushi.transform.parent, false);
                    dict.Add(master.Code, Tuple.Create(sushi, text));
                }
                else
                {
                    Destroy(sushi.gameObject);
                }
            }

            Sushiya.Sushiya.Instance.SushiMenu.Unlocked
                .Subscribe(ShowModel)
                .AddTo(this);

            Sushiya.Sushiya.Instance.Denpyo.AddPriceEvent
                .Subscribe(ShowText)
                .AddTo(this);
        }

        SushiCore CreateSushi(SushiMaster master)
        {
            var sushi = master.SushiCreate(Vector3.zero, Quaternion.identity, transform);
            sushi.ChangeStateToMenu();
            if (!master.IsUnlocked)
            {
                sushi.gameObject.SetActive(false);
            }
            return sushi;
        }

        MenuTexts CreateText(SushiMaster master)
        {
            var text = Instantiate(textPrefab);
            text.Configure(master);
            return text;
        }

        void ShowModel(int code)
        {
            var t = dict[code];
            t.Item1.gameObject.SetActive(true);
        }

        void ShowText(int code)
        {
            var t = dict[code];
            t.Item2.ShowText(true);
        }
    }
}
