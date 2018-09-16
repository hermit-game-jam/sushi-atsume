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
        private Denpyo Denpyo;
        private GachaDrawer GachaDrawer;

        void Start()
        {
            SushiMenu = new SushiMenu();
            Denpyo = new Denpyo();
            GachaDrawer = new GachaDrawer(DishHolder); 
        }
    }
}
