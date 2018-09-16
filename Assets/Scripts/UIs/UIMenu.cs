using Masters;
using Sushi;
using UnityEngine;

namespace UIs
{
    public class UIMenu : MonoBehaviour
    {
        [SerializeField] SushiHolder holder;

        void Start()
        {
            foreach (var master in Master.Instance.SushiMaster.Values)
            {
                var sushi = Create(master);
                holder.TryPut(sushi.transform);
            }
        }

        GameObject Create(SushiMaster master)
        {
            var sushi = master.SushiCreate(Vector3.zero, Quaternion.identity, transform);
            sushi.ChangeStateToMenu();
            return sushi.gameObject;
        }
    }
}
