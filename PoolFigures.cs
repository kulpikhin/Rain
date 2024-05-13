using UnityEngine;
using UnityEngine.Pool;

public class PoolFigures<F> : MonoBehaviour where F : Figure
{
    [SerializeField] private F _figurePrefab;

    private int _poolCapacity = 35;
    private int _poolMaxSize = 35;
    private ObjectPool<F> _poolFigures;

    private void Awake()
    {
        _poolFigures = new ObjectPool<F>(
        createFunc: () => Instantiate(_figurePrefab),
        actionOnGet: (figure) => ActionOnGet(figure),
        actionOnRelease: (figure) => ActionOnRelease(figure),
        actionOnDestroy: (figure) => Destroy(figure),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    public F GetFigure()
    {
        return _poolFigures.Get();
    }

    private void TempActionOnRelease(Figure figure)
    {
        ActionOnRelease(figure as F);
    }

    private void ActionOnRelease(F figure)
    {
        figure.WorkDone -= TempActionOnRelease;
        _poolFigures.Release(figure);
        figure.gameObject.SetActive(false);
    }

    private F ActionOnGet(F figure)
    {
        figure.WorkDone += TempActionOnRelease;
        figure.gameObject.SetActive(true);
        return figure;
    }
}
