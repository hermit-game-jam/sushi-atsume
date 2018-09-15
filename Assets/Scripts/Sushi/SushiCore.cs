using UnityEngine;

namespace Sushi
{
    public class SushiCore : MonoBehaviour, IClickable
    {
        ISushiState state;
        public ISushiState State => state;

        void Awake()
        {
            ChangeState(new LaneSushiState(this));
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
                core.ChangeState(new TableSushiState(core));
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
