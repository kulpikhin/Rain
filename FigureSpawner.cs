using System;
using UnityEngine;

public class FigureSpawner<T> : MonoBehaviour where T : Figure
{
    [SerializeField] PoolFigures<T> _poolFigures;

    public event Action<Figure> FigureSpawned;

    protected T Spawn(Vector3 position)
    {
        T figure = _poolFigures.GetFigure() as T;
        figure.transform.position = position;
        FigureSpawned?.Invoke(figure);
        return figure;
    }
}
