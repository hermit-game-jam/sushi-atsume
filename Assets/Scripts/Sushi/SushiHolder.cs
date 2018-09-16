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

            public Transform Child { get; set; }
        }

        [SerializeField] Parent[] parents;

        public bool TryPut(Transform child)
        {
            var parent = parents.FirstOrDefault(x => x.Child == null);
            if (parent == null)
            {
                return false;
            }

            parent.Child = child;
            child.SetParent(parent.Transform, false);
            child.localPosition = Vector3.zero;
            child.localRotation = Quaternion.identity;
            child.localScale = Vector3.one;
            return true;
        }
    }
}
