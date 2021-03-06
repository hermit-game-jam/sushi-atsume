﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gachas;
using UniRx;

namespace Sushiya
{
    public class Sushiya : SingletonMonoBehaviour<Sushiya>
    {   
        public DishHolder DishHolder = new DishHolder();
        public SushiMenu SushiMenu;
        public Denpyo Denpyo;
        private GachaDrawer GachaDrawer;

        private ISubject<Denpyo> _denpyo = new AsyncSubject<Denpyo>();
        public IObservable<Denpyo> DenpyoAsObservable => _denpyo;

        void Start()
        {
            Denpyo = new Denpyo();
            SushiMenu = new SushiMenu(Denpyo);
            GachaDrawer = new GachaDrawer(DishHolder, SushiMenu);
            _denpyo.OnNext(Denpyo);
            _denpyo.OnCompleted();
        }
    }
}
