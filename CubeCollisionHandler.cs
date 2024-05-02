using UnityEngine;
using UnityEngine.Events;

public class CubeCollisionHandler : MonoBehaviour
{
    public event UnityAction GroundTouched;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground ground))
        {
            GroundTouched?.Invoke();
        }
    }
}
