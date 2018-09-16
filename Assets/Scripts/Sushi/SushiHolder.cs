using System;
using System.Linq;
using UnityEngine;

namespace Sushi
{
    public class SushiHolder : MonoBehaviour
    {
        [Serializable]
        class Parent
        {
            [SerializeField]
            Transform transform;
            public Transform Transform => transform;

            public SushiCore SushiCore { get; set; }
        }

        [SerializeField] Parent[] parents;

        public bool TryPut(SushiCore sushiCore)
        {
            var parent = parents.FirstOrDefault(x => x.SushiCore == null);
            if (parent == null)
            {
                return false;
            }

            parent.SushiCore = sushiCore;
            sushiCore.transform.SetParent(parent.Transform, false);
            sushiCore.transform.localPosition = Vector3.zero;
            return true;
        }
    }
}
