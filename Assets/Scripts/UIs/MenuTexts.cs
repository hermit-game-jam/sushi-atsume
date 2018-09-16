using Masters;
using TMPro;
using UnityEngine;

namespace UIs
{
    public class MenuTexts : MonoBehaviour
    {
        [SerializeField] TextMeshPro title;
        [SerializeField] TextMeshPro text;

        public void Configure(SushiMaster master)
        {
            title.text = master.Name;
            text.text = master.FlavorText;
        }
    }
}
