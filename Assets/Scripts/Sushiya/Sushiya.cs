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

        void Start()
        {
            GachaDrawer = new GachaDrawer(DishHolder); 
        }
    }
}
