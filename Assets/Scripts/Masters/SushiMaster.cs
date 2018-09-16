using System;
using Sushi;
using UnityEngine;

namespace Masters
{
    [Serializable]
    public class SushiMaster
    {
        public string Name;
        public string FlavorText;
        public SushiRarity Rarity;
        public int Code;
        public Price Price;
        public SushiCore prefab;
        public GameObject netaPrefab;
        public GameObject dishPrefab;
        public GameObject ricePrefab;
        public bool IsUnlocked;
        public int GachaWeight;

        public enum SushiRarity
        {
            UltraRare,
            SuperRare,
            Rare,
            Uncommon,
            Common,
        };

        public SushiCore SushiCreate(Vector3 position, Quaternion rotation, Transform parent)
        {
            var core = UnityEngine.Object.Instantiate(prefab, position, rotation, parent);
            var dish = UnityEngine.Object.Instantiate(dishPrefab, Vector3.zero, Quaternion.identity, core.transform);
            dish.transform.localPosition = Vector3.zero;
            var rice = UnityEngine.Object.Instantiate(ricePrefab, Vector3.zero, Quaternion.identity, core.transform);
            rice.transform.localPosition = Vector3.zero;
            var neta = UnityEngine.Object.Instantiate(netaPrefab, Vector3.zero, Quaternion.identity, core.transform);
            neta.transform.localPosition = Vector3.zero;
            return core;
        }
    }
}
