using Sushi;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Players
{
    public class PlayerInputEventProvider : MonoBehaviour
    {
        void Start()
        {
            this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Select(_ => Camera.main.ScreenPointToRay(Input.mousePosition))
                .Select(Raycast)
                .Where(x => x != null)
                .Subscribe(x => x.OnClick())
                .AddTo(this);
        }

        IClickable Raycast(Ray ray)
        {
            if (Physics.Raycast(ray, out var hit))
            {
                return hit.collider?.GetComponent<IClickable>();
            }

            return null;
        }
    }
}
