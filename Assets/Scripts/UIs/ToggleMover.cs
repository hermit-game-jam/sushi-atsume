using Sushi;
using UniRx;
using UnityEngine;

namespace UIs
{
    public class ToggleMover : MonoBehaviour, IClickable
    {
        readonly BoolReactiveProperty zooming = new BoolReactiveProperty();
        [SerializeField] Animator _animator;
        [SerializeField] AudioSource _audio;

        void Start()
        {
            zooming.SkipLatestValueOnSubscribe()
                .Subscribe(x =>
                {
                    _animator.SetBool("zooming", x);
                    _audio.Play();
                })
                .AddTo(this);
        }

        void IClickable.OnClick()
        {
            zooming.Value = !zooming.Value;
        }


    }
}
