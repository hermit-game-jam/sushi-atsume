using System;
using Masters;
using UnityEngine;
using UniRx;

namespace Sushi
{
    public class SushiCore : MonoBehaviour, IClickable
    {
        ISushiState state;
        public ISushiState State => state;

        SushiMaster Master { get; set; }
        SushiHolder Holder { get; set; }

        private ISubject<bool> onLaneSushiClick = new Subject<bool>();
        public IObservable<bool> OnLaneSushiClick => onLaneSushiClick;
        
        private ISubject<int> onTableSushiClick = new Subject<int>();
        public IObservable<int> OnTableSushiClick => onTableSushiClick;
        
        private ISubject<Unit> onEmptySushiClick = new Subject<Unit>();
        public IObservable<Unit> OnEmptySushiClick => onEmptySushiClick;

        void Awake()
        {
            ChangeState(new LaneSushiState(this));
        }

        public void Initialize(SushiMaster master, SushiHolder holder)
        {
            Master = master;
            Holder = holder;
        }

        public void ChangeState(ISushiState state)
        {
            this.state = state;
        }

        void IClickable.OnClick()
        {
            state.OnClick();
        }

        class LaneSushiState : ISushiState
        {
            readonly SushiCore core;
            public bool AutoMovable => true;

            public LaneSushiState(SushiCore core)
            {
                this.core = core;
            }

            void ISushiState.OnClick()
            {
                var putSucceed = core.Holder.TryPut(core);
                
                if (putSucceed)
                {
                    core.ChangeState(new TableSushiState(core));
                }
                
                core.onLaneSushiClick.OnNext(putSucceed);
            }
        }

        class TableSushiState : ISushiState
        {
            readonly SushiCore core;
            public bool AutoMovable => false;
            int SushiLife = 5;

            public TableSushiState(SushiCore core)
            {
                this.core = core;
            }
            void ISushiState.OnClick()
            {
                SushiLife--;
                core.onTableSushiClick.OnNext(SushiLife);
                if (SushiLife <= 0)
                {
                    core.ChangeState(new EmptySushiState(core));
                }
            }
        }
        
        class EmptySushiState : ISushiState
        {
            readonly SushiCore core;
            public bool AutoMovable => false;

            public EmptySushiState(SushiCore core)
            {
                this.core = core;
            }
            void ISushiState.OnClick()
            {
                core.onEmptySushiClick.OnNext(Unit.Default);
                Sushiya.Sushiya.Instance.DishHolder.Add(core.Master.Code);
                Destroy(core.gameObject);
            }
        }
    }
}
