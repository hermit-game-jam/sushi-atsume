using Masters;
using Sushi;
using UnityEngine;

namespace UIs
{
    public class UIMenu : MonoBehaviour
    {
        [SerializeField] SushiHolder holder;
        [SerializeField] Quaternion sushiAngle;
        [SerializeField] MenuTexts textPrefab;

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
                }
            }
        }

        GameObject CreateSushi(SushiMaster master)
        {
            var sushi = master.SushiCreate(Vector3.zero, Quaternion.identity, transform);
            sushi.ChangeStateToMenu();
            return sushi.gameObject;
        }

        GameObject CreateText(SushiMaster master)
        {
            var text = Instantiate(textPrefab);
            text.Configure(master);
            return text.gameObject;
        }
    }
}
