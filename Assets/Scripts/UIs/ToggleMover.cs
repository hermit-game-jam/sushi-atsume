using Sushi;
using UniRx;
using UnityEngine;

namespace UIs
{
    public class ToggleMover : MonoBehaviour, IClickable
    {
        readonly BoolReactiveProperty zooming = new BoolReactiveProperty();
        [SerializeField] Animator _animator;

        void Start()
        {
            zooming.Subscribe(x => _animator.SetBool("zooming", x))
                .AddTo(this);
        }

        void IClickable.OnClick()
        {
            zooming.Value = !zooming.Value;
        }


    }
}
