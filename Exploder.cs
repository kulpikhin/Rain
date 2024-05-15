using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _radiusExplosion;
    [SerializeField] private float _forceExplosion;

    public void StartExplosion()
    {
        foreach (Figure figure in GetExplodableFigures())
        {
            if(figure.TryGetComponent(out Rigidbody rigidBody))
            {
                rigidBody.AddExplosionForce(_forceExplosion, transform.position, _radiusExplosion);
            }
        }
    }

    private List<Figure> GetExplodableFigures()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radiusExplosion);

        List<Figure> figures = new List<Figure>();

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent<Figure>(out Figure figure))
            {
                figures.Add(figure);
            }
        }

        return figures;
    }
}
