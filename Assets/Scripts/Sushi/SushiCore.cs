using Masters;
using UnityEngine;

namespace Sushi
{
    public class SushiCore : MonoBehaviour, IClickable
    {
        ISushiState state;
        public ISushiState State => state;

        SushiMaster Master { get; set; }
        SushiHolder Holder { get; set; }

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
                if (core.Holder.TryPut(core))
                {
                    core.ChangeState(new TableSushiState(core));
                }
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
                Sushiya.Sushiya.Instance.DishHolder.Add(core.Master.Code);
                Destroy(core.gameObject);
            }
        }
    }
}
