using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gachas;

namespace Sushiya
{
    public class Sushiya : SingletonMonoBehaviour<Sushiya>
    {   
        public DishHolder DishHolder = new DishHolder();
        private SushiMenu SushiMenu;
        private GachaDrawer GachaDrawer;

        void Start()
        {
            SushiMenu = new SushiMenu();
            GachaDrawer = new GachaDrawer(DishHolder); 
        }
    }
}
