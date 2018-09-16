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

            public TableSushiState(SushiCore core)
            {
                this.core = core;
            }
            void ISushiState.OnClick()
            {

            }
        }
    }
}
