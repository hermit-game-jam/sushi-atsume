using UnityEngine;

namespace Sushi
{
    public class SushiMover : MonoBehaviour
    {
        [SerializeField] SushiCore core;
        [SerializeField] float speed;

        void Update()
        {
            if (core.State.AutoMovable)
            {
                transform.position = transform.position + Vector3.left * speed * Time.deltaTime;
            }
        }
    }
}
