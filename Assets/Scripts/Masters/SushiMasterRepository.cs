using System.Collections.Generic;
using UnityEngine;

namespace Masters
{
    [CreateAssetMenu(menuName = "Assets/Create SushiMasterRepository")]
    public class SushiMasterRepository : ScriptableObject
    {
        public List<SushiMaster> Values;
    }
}
