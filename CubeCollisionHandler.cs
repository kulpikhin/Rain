using UnityEngine;
using UnityEngine.Events;
using System;

public class CubeCollisionHandler : MonoBehaviour
{
    public event Action<Floor> GroundTouched;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Floor floor))
        {
            GroundTouched?.Invoke(floor);
        }
    }
}
