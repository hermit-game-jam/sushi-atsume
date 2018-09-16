using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gachas;

namespace Sushiya
{
    public class Sushiya : SingletonMonoBehaviour<Sushiya>
    {   
        public DishHolder DishHolder = new DishHolder();
        private GachaDrawer GachaDrawer;

        void Awake()
        {
            GachaDrawer = new GachaDrawer(DishHolder); 
        }
    }
}
