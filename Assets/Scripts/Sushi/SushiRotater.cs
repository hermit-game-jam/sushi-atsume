using Sushi;
using UnityEngine;

public class SushiRotater : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] SushiCore core;

    void Update()
    {
        if (core.State.Rotatable)
        {
            transform.rotation *= Quaternion.Euler(0, speed * Time.deltaTime, 0);
        }
    }
}
