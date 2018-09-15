using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sushiya
{
    public class Sushiya : SingletonMonoBehaviour<Sushiya>
    {   
        public DishHolder DishHolder = new DishHolder();
    }
}
